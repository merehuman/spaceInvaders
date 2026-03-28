//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;
using Azul;

namespace SE456
{
    public class ShipRemoveObserver : ColObserver
    {
        private static SndObserver pExplosionSnd;

        //Set once from ScenePlay (same clip as missile/bomb shield SndObservers). SndObserver on Alien_Ship would fire every overlap frame, so reserve-life hits call PlaySound here instead
        public static void SetExplosionSound(SndObserver explosionSnd)
        {
            Debug.Assert(explosionSnd != null);
            pExplosionSnd = explosionSnd;
        }

        private static void PlayExplosionOnce()
        {
            if (pExplosionSnd != null)
            {
                pExplosionSnd.PlaySound();
            }
        }

        private static bool RemoveReserveShipSprite(SpriteGame.Name name)
        {
            SpriteGame sprite = SpriteGameMan.Find(name);
            if (sprite == null)
            {
                return false;
            }

            if (!sprite.HasSpriteNode())
            {
                return false;
            }

            SpriteNode pNode = sprite.GetSpriteNode();
            SpriteBatchMan.Remove(pNode);
            SpriteGameMan.Remove(sprite);
            return true;
        }


        private static int invulnFramesRemaining = 0;

        private const int InvulnFramesAfterHit = 90;

        public static void TickInvulnerability()
        {
            if (invulnFramesRemaining > 0)
            {
                invulnFramesRemaining--;
            }
        }

        public static void ResetForNewGame()
        {
            count = 3;
            invulnFramesRemaining = 0;
        }

        private static void QueueShipExplosionTimers(Ship ship)
        {
            TimerEventMan.Add(TimerEvent.Name.Sample1, new SampleCmd(ship), 0.1f);
            TimerEventMan.Add(TimerEvent.Name.Sample2, new SampleCmd(ship), 0.3f);
            TimerEventMan.Add(TimerEvent.Name.Sample3, new SampleCmd(ship), 0.5f);
        }

        public override void Notify()
        {
            if (invulnFramesRemaining > 0)
            {
                return;
            }

            Ship ship = ShipMan.GetShip();

            if (count == 3)
            {
                if (RemoveReserveShipSprite(SpriteGame.Name.ReserveShip3))
                {
                    count--;
                    invulnFramesRemaining = InvulnFramesAfterHit;
                    QueueShipExplosionTimers(ship);
                    Font font = FontMan.Find(Font.Name.Reserve);
                    if (font != null)
                    {
                        font.UpdateMessage("2");
                    }
                }
            }
            else if (count == 2)
            {
                if (RemoveReserveShipSprite(SpriteGame.Name.ReserveShip2))
                {
                    count--;
                    invulnFramesRemaining = InvulnFramesAfterHit;
                    PlayExplosionOnce();
                    QueueShipExplosionTimers(ship);
                    Font font = FontMan.Find(Font.Name.Reserve);
                    if (font != null)
                    {
                        font.UpdateMessage("1");
                    }
                }
            }
            else if (count == 1)
            {
                if (RemoveReserveShipSprite(SpriteGame.Name.ReserveShip1))
                {
                    count--;
                    invulnFramesRemaining = InvulnFramesAfterHit;
                    PlayExplosionOnce();
                    QueueShipExplosionTimers(ship);
                    Font font = FontMan.Find(Font.Name.Reserve);
                    if (font != null)
                    {
                        font.UpdateMessage("0");
                    }
                }
            }
        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipRemoveObserver;
        }

        public static int count = 3;
    }

    // data
}

// --- End of File ---
