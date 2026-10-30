using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.LastDefender.Scripts.Core.Signals
{
    internal class ChangedCastleHealthSignal
    {
        public int Health { get; private set; }

        public ChangedCastleHealthSignal(int health)
        {
            Health = health;
        }

    }
}
