using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthboundBattle.Core.Abstract;

namespace EarthboundBattle.Core.Models;

public class Enemy : Entity
{
    public int ExpReward { get; private set; }
    public int CashReward { get; private set; }

    public Enemy(string name, int maxHp, int offense, int defense, int expReward, int cashReward)
        : base(name, maxHp, offense, defense)
    {
        ExpReward = expReward;
        CashReward = cashReward;
    }
}