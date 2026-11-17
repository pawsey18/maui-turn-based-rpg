using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthboundBattle.Core.Interfaces;
using EarthboundBattle.Core.Models;
using EarthboundBattle.Services.Interfaces;

namespace EarthboundBattle.Services.Implementations;

public class BattleService : IBattleService
{
    private readonly ILogger _logger;
    private readonly Random _random = new();

    public PartyMember CurrentHero { get; private set; } = null!;
    public Enemy CurrentEnemy { get; private set; } = null!;

    public bool IsBattleOver => CurrentHero?.IsFainted == true || CurrentEnemy?.IsFainted == true;
    public string BattleStatusMessage { get; private set; } = string.Empty;

    public BattleService(ILogger logger)
    {
        _logger = logger;
    }

    public void StartBattle(PartyMember hero, Enemy enemy)
    {
        CurrentHero = hero;
        CurrentEnemy = enemy;
        BattleStatusMessage = $"A wild {CurrentEnemy.Name} approaches!";
        _logger.Log(BattleStatusMessage);
    }

    public void ExecuteHeroAttack()
    {
        if (IsBattleOver) return;

        int damage = CurrentHero.Offense + _random.Next(-2, 3);
        CurrentEnemy.TakeDamage(damage);

        BattleStatusMessage = $"{CurrentHero.Name} attacks {CurrentEnemy.Name} for {damage} damage!";
        _logger.Log(BattleStatusMessage);

        CheckBattleEnd();
    }

    public void ExecuteHeroPsi()
    {
        if (IsBattleOver) return;

        int ppCost = 6;
        if (CurrentHero.CurrentPp < ppCost)
        {
            BattleStatusMessage = $"{CurrentHero.Name} doesn't have enough PP for PK Rockin'!";
            _logger.Log(BattleStatusMessage);
            return;
        }

        CurrentHero.ConsumePp(ppCost);
        int psiDamage = CurrentHero.Offense * 2 + _random.Next(5, 12);
        CurrentEnemy.TakeDamage(psiDamage);

        BattleStatusMessage = $"{CurrentHero.Name} cast PK Rockin' for {psiDamage} damage!";
        _logger.Log(BattleStatusMessage);

        CheckBattleEnd();
    }

    public void ExecuteEnemyTurn()
    {
        if (IsBattleOver) return;

        int damage = CurrentEnemy.Offense + _random.Next(-1, 3);
        CurrentHero.TakeDamage(damage);

        BattleStatusMessage = $"{CurrentEnemy.Name} attacks back for {damage} damage!";
        _logger.Log(BattleStatusMessage);

        CheckBattleEnd();
    }

    private void CheckBattleEnd()
    {
        if (CurrentEnemy.IsFainted)
        {
            BattleStatusMessage = $"{CurrentEnemy.Name} was defeated! You won {CurrentEnemy.ExpReward} EXP!";
            _logger.Log(BattleStatusMessage);
        }
        else if (CurrentHero.IsFainted)
        {
            BattleStatusMessage = $"{CurrentHero.Name} has succumbed to the battle... PK Starstorm fades.";
            _logger.Log(BattleStatusMessage);
        }
    }
}