//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the instance
            SpaceInvaders pGame = new SpaceInvaders();
            Debug.Assert(pGame != null);

            // Start the game
            pGame.Run();
        }
    }

}

// --- End of File ---
