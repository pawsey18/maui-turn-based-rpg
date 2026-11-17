using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthboundBattle.Core.Models;

namespace EarthboundBattle.Services.Interfaces;

public interface IBattleService
{
    PartyMember CurrentHero { get; }
    Enemy CurrentEnemy { get; }
    bool IsBattleOver { get; }
    string BattleStatusMessage { get; }

    void StartBattle(PartyMember hero, Enemy enemy);
    void ExecuteHeroAttack();
    void ExecuteHeroPsi();
    void ExecuteEnemyTurn();
}
