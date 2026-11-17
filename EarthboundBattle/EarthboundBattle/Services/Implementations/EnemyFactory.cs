using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthboundBattle.Core.Models;

namespace EarthboundBattle.Services.Implementations;

public static class EnemyFactory
{
    private static readonly Random _random = new();

    public static Enemy GetRandomEnemy()
    {
        var enemies = new List<Enemy>
        {
            new Enemy("Coil Snake", maxHp: 35, offense: 14, defense: 4, expReward: 8, cashReward: 15),
            new Enemy("Spiteful Crow", maxHp: 45, offense: 18, defense: 6, expReward: 12, cashReward: 25),
            new Enemy("Runaway Dog", maxHp: 60, offense: 22, defense: 8, expReward: 18, cashReward: 30),

            new Enemy("Skate Punk", maxHp: 75, offense: 24, defense: 10, expReward: 28, cashReward: 45),
            new Enemy("New Age Retro Hippie", maxHp: 85, offense: 25, defense: 12, expReward: 35, cashReward: 60),
            new Enemy("Ramblin' Evil Mushroom", maxHp: 70, offense: 26, defense: 9, expReward: 32, cashReward: 40),
            new Enemy("Gruff Goat", maxHp: 95, offense: 27, defense: 14, expReward: 42, cashReward: 55),

            new Enemy("Territorial Oak", maxHp: 130, offense: 30, defense: 18, expReward: 65, cashReward: 80),
            new Enemy("Starman Junior", maxHp: 145, offense: 34, defense: 16, expReward: 90, cashReward: 110),
            new Enemy("Frankystein Mark II", maxHp: 180, offense: 32, defense: 14, expReward: 85, cashReward: 120)
        };

        int index = _random.Next(enemies.Count);
        return enemies[index];
    }
}