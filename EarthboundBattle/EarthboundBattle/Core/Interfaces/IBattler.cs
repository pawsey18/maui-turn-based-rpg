namespace EarthboundBattle.Core.Interfaces;

public interface IBattler
{
    string Name { get; }
    int MaxHp { get; }
    int CurrentHp { get; }
    int Offense { get; }
    int Defense { get; }
    bool IsFainted { get; }

    void TakeDamage(int damageAmount);
    void Heal(int healAmount);
}
