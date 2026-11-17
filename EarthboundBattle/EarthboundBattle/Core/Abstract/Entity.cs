using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EarthboundBattle.Core.Interfaces;

namespace EarthboundBattle.Core.Abstract;

public abstract class Entity : IBattler
{
    public string Name { get; protected set; }
    public int MaxHp { get; protected set; }
    public int CurrentHp { get; protected set; }
    public int Offense { get; protected set; }
    public int Defense { get; protected set; }

    public bool IsFainted => CurrentHp <= 0;

    protected Entity(string name, int maxHp, int offense, int defense)
    {
        Name = name;
        MaxHp = maxHp;
        CurrentHp = maxHp; // Start fully healed at the beginning
        Offense = offense;
        Defense = defense;
    }

    public virtual void TakeDamage(int damageAmount)
    {
        // Basic EarthBound-style damage reduction using Defense
        int actualDamage = Math.Max(1, damageAmount - (Defense / 2));
        CurrentHp -= actualDamage;

        if (CurrentHp < 0)
        {
            CurrentHp = 0;
        }
    }

    public virtual void Heal(int healAmount)
    {
        CurrentHp += healAmount;
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }
    }
}