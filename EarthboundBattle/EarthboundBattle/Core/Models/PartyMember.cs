using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EarthboundBattle.Core.Abstract;
using EarthboundBattle.Core.Interfaces;

namespace EarthboundBattle.Core.Models;

public class PartyMember : Entity, IPsiUser
{
    public int MaxPp { get; private set; }
    public int CurrentPp { get; private set; }

    public PartyMember(string name, int maxHp, int maxPp, int offense, int defense)
        : base(name, maxHp, offense, defense)
    {
        MaxPp = maxPp;
        CurrentPp = maxPp;
    }

    public void ConsumePp(int ppCost)
    {
        CurrentPp -= ppCost;
        if (CurrentPp < 0)
        {
            CurrentPp = 0;
        }
    }

    public void RestorePp(int ppAmount)
    {
        CurrentPp += ppAmount;
        if (CurrentPp > MaxPp)
        {
            CurrentPp = MaxPp;
        }
    }
}