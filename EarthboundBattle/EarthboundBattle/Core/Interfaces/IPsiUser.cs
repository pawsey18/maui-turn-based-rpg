using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthboundBattle.Core.Interfaces;

public interface IPsiUser
{
    int MaxPp { get; }
    int CurrentPp { get; }

    void ConsumePp(int ppCost);
    void RestorePp(int ppAmount);
}