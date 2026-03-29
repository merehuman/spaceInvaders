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

        /// <summary>Queued during Notify; applied after ColPairMan.Process so multiple Alien_Ship hits in one pass all see invuln==0 before it is applied. Bomb_Ship never queues this (see QueuePostCollisionInvulnerability).</summary>
        private static int pendingInvulnAfterCollisions = 0;

        /// <summary>At most one Alien_Ship life loss per collision pass (invuln is deferred like bombs, so this replaces same-frame invuln blocking).</summary>
        private static bool alienShipHitThisCollisionPass = false;

        private const int InvulnFramesAfterHit = 90;

        public static void TickInvulnerability()
        {
            if (invulnFramesRemaining > 0)
            {
                invulnFramesRemaining--;
            }
        }

        /// <summary>Call once immediately before ColPairMan.Process().</summary>
        public static void BeginCollisionProcessing()
        {
            alienShipHitThisCollisionPass = false;
        }

        /// <summary>Call once immediately after ColPairMan.Process().</summary>
        public static void ApplyPendingInvulnerabilityAfterCollisions()
        {
            if (pendingInvulnAfterCollisions > 0)
            {
                invulnFramesRemaining = pendingInvulnAfterCollisions;
                pendingInvulnAfterCollisions = 0;
            }
        }

        /// <summary>Long post-hit invuln is for Alien_Ship overlap (avoids draining multiple lives per frame). Bomb hits must not set it or consecutive bombs are ignored for ~90 frames.</summary>
        private static void QueuePostCollisionInvulnerability()
        {
            ColPair active = ColPairMan.GetActiveColPair();
            if (active != null && active.name == ColPair.Name.Bomb_Ship)
            {
                return;
            }
            pendingInvulnAfterCollisions = InvulnFramesAfterHit;
        }

        public static void ResetForNewGame()
        {
            count = 3;
            invulnFramesRemaining = 0;
            pendingInvulnAfterCollisions = 0;
            alienShipHitThisCollisionPass = false;
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

            if (count <= 0)
            {
                return;
            }

            ColPair activePair = ColPairMan.GetActiveColPair();
            if (activePair != null && activePair.name == ColPair.Name.Alien_Ship && alienShipHitThisCollisionPass)
            {
                return;
            }

            Ship ship = ShipMan.GetShip();

            if (count == 3)
            {
                if (RemoveReserveShipSprite(SpriteGame.Name.ReserveShip3))
                {
                    count--;
                    QueuePostCollisionInvulnerability();
                    if (activePair != null && activePair.name == ColPair.Name.Alien_Ship)
                    {
                        alienShipHitThisCollisionPass = true;
                    }
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
                    QueuePostCollisionInvulnerability();
                    if (activePair != null && activePair.name == ColPair.Name.Alien_Ship)
                    {
                        alienShipHitThisCollisionPass = true;
                    }
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
                    QueuePostCollisionInvulnerability();
                    if (activePair != null && activePair.name == ColPair.Name.Alien_Ship)
                    {
                        alienShipHitThisCollisionPass = true;
                    }
                    PlayExplosionOnce();
                    // Game over — do not queue SampleCmd timers; they can fire after scene change and stall or leave the shared Ship sprite wrong.
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
