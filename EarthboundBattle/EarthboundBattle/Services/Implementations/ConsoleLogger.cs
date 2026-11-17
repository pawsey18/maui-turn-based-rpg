using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarthboundBattle.Core.Interfaces;

namespace EarthboundBattle.Services.Implementations;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        // For now, write to console or debug output. 
        System.Diagnostics.Debug.WriteLine($"[BATTLE] {message}");
    }
}