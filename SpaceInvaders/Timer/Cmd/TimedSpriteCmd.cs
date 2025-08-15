//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    public class TimedSpriteCmd : Command
    {
        public TimedSpriteCmd(SpriteGame.Name name)
        {
            this.pSB_Invaders = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(this.pSB_Invaders != null);

            this.name = name;
        }

        override public void Execute(float deltaTime)
        {
            GameObject pGameObj = null;
            switch (name)
            {
                case SpriteGame.Name.Crab1:
                    pSB_Invaders.Attach(SpriteGameMan.Find(SpriteGame.Name.Crab1));
                    break;

                case SpriteGame.Name.Octopus1:
                    pSB_Invaders.Attach(SpriteGameMan.Find(SpriteGame.Name.Octopus1));
                    break;

                case SpriteGame.Name.Squid1:
                    pSB_Invaders.Attach(SpriteGameMan.Find(SpriteGame.Name.Squid1));
                    break;

                case SpriteGame.Name.UFO1:
                    pSB_Invaders.Attach(SpriteGameMan.Find(SpriteGame.Name.UFO1));
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

        }


        SpriteBatch pSB_Invaders;
        SpriteGame.Name name;
    }
}

// --- End of File ---
