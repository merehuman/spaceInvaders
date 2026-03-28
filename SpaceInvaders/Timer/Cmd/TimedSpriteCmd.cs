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
            this.name = name;
        }

        override public void Execute(float deltaTime)
        {
            SpriteBatch pSB_Aliens = SpriteBatchMan.Find(SpriteBatch.Name.Aliens);
            Debug.Assert(pSB_Aliens != null);

            SpriteGame pSprite = null;
            switch (name)
            {
                case SpriteGame.Name.Crab1:
                    pSprite = SpriteGameMan.Find(SpriteGame.Name.Crab1);
                    break;

                case SpriteGame.Name.Octopus1:
                    pSprite = SpriteGameMan.Find(SpriteGame.Name.Octopus1);
                    break;

                case SpriteGame.Name.Squid1:
                    pSprite = SpriteGameMan.Find(SpriteGame.Name.Squid1);
                    break;

                case SpriteGame.Name.UFO1:
                    pSprite = SpriteGameMan.Find(SpriteGame.Name.UFO1);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            Debug.Assert(pSprite != null);
            if (!pSprite.HasSpriteNode())
            {
                pSB_Aliens.Attach(pSprite);
            }
        }

        SpriteGame.Name name;
    }
}

// --- End of File ---
