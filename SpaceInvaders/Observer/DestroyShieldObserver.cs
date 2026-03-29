//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System.Diagnostics;

namespace SE456
{
    /// <summary>
    /// Alien_Shield pair still notifies here; shield removal on overlap was removed from ShieldFactory.
    /// </summary>
    class DestroyShieldObserver : ColObserver
    {
        public override void Notify()
        {
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.DestroyShieldObserver;
        }
    }
}
