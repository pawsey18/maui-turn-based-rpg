using EarthboundBattle.Core.Models;
using EarthboundBattle.Services.Implementations;
using EarthboundBattle.Services.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EarthboundBattle.ViewModels;

public class BattleViewModel : INotifyPropertyChanged
{
    private readonly IBattleService _battleService;

    // UI Bindable Properties
    private string _heroName = "Ness";
    public string HeroName
    {
        get => _heroName;
        private set { _heroName = value; OnPropertyChanged(); }
    }

    private int _heroHp;
    public int HeroHp
    {
        get => _heroHp;
        private set { _heroHp = value; OnPropertyChanged(); }
    }

    private int _heroMaxHp;
    public int HeroMaxHp
    {
        get => _heroMaxHp;
        private set { _heroMaxHp = value; OnPropertyChanged(); }
    }

    private int _heroPp;
    public int HeroPp
    {
        get => _heroPp;
        private set { _heroPp = value; OnPropertyChanged(); }
    }

    private int _heroMaxPp;
    public int HeroMaxPp
    {
        get => _heroMaxPp;
        private set { _heroMaxPp = value; OnPropertyChanged(); }
    }

    private string _enemyName = string.Empty;
    public string EnemyName
    {
        get => _enemyName;
        private set { _enemyName = value; OnPropertyChanged(); }
    }

    private int _enemyHp;
    public int EnemyHp
    {
        get => _enemyHp;
        private set { _enemyHp = value; OnPropertyChanged(); }
    }

    private int _enemyMaxHp;
    public int EnemyMaxHp
    {
        get => _enemyMaxHp;
        private set { _enemyMaxHp = value; OnPropertyChanged(); }
    }

    private string _statusMessage = "A strange battle is brewing...";
    public string StatusMessage
    {
        get => _statusMessage;
        private set { _statusMessage = value; OnPropertyChanged(); }
    }

    public bool IsBattleOver => _battleService.IsBattleOver;

    // Commands bound to UI buttons
    public ICommand AttackCommand { get; }
    public ICommand PsiCommand { get; }
    public ICommand ResetBattleCommand { get; }

    public BattleViewModel(IBattleService battleService)
    {
        _battleService = battleService;

        AttackCommand = new Command(ExecuteAttack);
        PsiCommand = new Command(ExecutePsi);
        ResetBattleCommand = new Command(StartNewBattle);

        StartNewBattle();
    }

    private void StartNewBattle()
    {
        var hero = new PartyMember("Ness", maxHp: 120, maxPp: 35, offense: 24, defense: 12);
        var enemy = EnemyFactory.GetRandomEnemy();

        _battleService.StartBattle(hero, enemy);
        UpdateUiFromService();
    }

    private void ExecuteAttack()
    {
        if (_battleService.IsBattleOver) return;

        // 1. Hero turn
        _battleService.ExecuteHeroAttack();
        UpdateUiFromService();

        // 2. Enemy counter-turn (if enemy survived)
        if (!_battleService.IsBattleOver)
        {
            Task.Delay(400).ContinueWith(_ =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _battleService.ExecuteEnemyTurn();
                    UpdateUiFromService();
                });
            });
        }
    }

    private void ExecutePsi()
    {
        if (_battleService.IsBattleOver) return;

        // 1. Hero PSI turn
        _battleService.ExecuteHeroPsi();
        UpdateUiFromService();

        // 2. Enemy counter-turn (if enemy survived)
        if (!_battleService.IsBattleOver)
        {
            Task.Delay(400).ContinueWith(_ =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _battleService.ExecuteEnemyTurn();
                    UpdateUiFromService();
                });
            });
        }
    }

    private void UpdateUiFromService()
    {
        HeroName = _battleService.CurrentHero.Name;
        HeroHp = _battleService.CurrentHero.CurrentHp;
        HeroMaxHp = _battleService.CurrentHero.MaxHp;
        HeroPp = _battleService.CurrentHero.CurrentPp;
        HeroMaxPp = _battleService.CurrentHero.MaxPp;

        EnemyName = _battleService.CurrentEnemy.Name;
        EnemyHp = _battleService.CurrentEnemy.CurrentHp;
        EnemyMaxHp = _battleService.CurrentEnemy.MaxHp;
        StatusMessage = _battleService.BattleStatusMessage;

        OnPropertyChanged(nameof(IsBattleOver));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}