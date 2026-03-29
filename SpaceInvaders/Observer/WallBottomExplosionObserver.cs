//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System.Diagnostics;

namespace SE456
{
    /// <summary>
    /// Bomb_Wall: ExplosionShip flash when an invader bomb hits the bottom wall (same pair as RemoveBombObserver).
    /// </summary>
    class WallBottomExplosionObserver : ColObserver
    {
        public override void Notify()
        {
            Bomb bomb = null;

            if (pSubject.pObjA is Bomb && pSubject.pObjB is WallBottom)
            {
                bomb = (Bomb)pSubject.pObjA;
            }
            else if (pSubject.pObjB is Bomb && pSubject.pObjA is WallBottom)
            {
                bomb = (Bomb)pSubject.pObjB;
            }
            else if (pSubject.pObjA is Bomb && pSubject.pObjB is WallTop)
            {
                bomb = (Bomb)pSubject.pObjA;
            }
            else if (pSubject.pObjB is Bomb && pSubject.pObjA is WallTop)
            {
                bomb = (Bomb)pSubject.pObjB;
            }
            else
            {
                return;
            }

            Debug.Assert(bomb != null);

            bool hitWallBottom = (pSubject.pObjA is WallBottom) || (pSubject.pObjB is WallBottom);
            const float bottomWallExplosionRaiseY = 8.0f;

            const float referenceBombColHeight = 24.0f;
            float h = bomb.poColObj.poColRect.height;
            float heightAlignY = (referenceBombColHeight - h) * 0.5f;
            float yExplosion = hitWallBottom
                ? (bomb.y + bottomWallExplosionRaiseY + heightAlignY)
                : bomb.y;

            // Select/Over use a different SpriteBatchMan — Invaders batch is missing; skip (same as ScenePlay early-exit patterns).
            SpriteBatch pSBInvaders = SpriteBatchMan.Find(SpriteBatch.Name.Invaders);
            SpriteBatch pSBBoxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            if (pSBInvaders == null || pSBBoxes == null)
            {
                return;
            }

            ExplosionRoot explosionRoot = (ExplosionRoot)GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
            if (explosionRoot == null)
            {
                return;
            }

            // SpriteGame.ExplosionShip → Image.Name.ExplosionShip (ScenePlay.Initialize)
            Explosion explosion = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.ExplosionShip, bomb.x, yExplosion);
            explosion.ActivateSprite(pSBInvaders);
            explosion.ActivateCollisionSprite(pSBBoxes);
            explosion.poColObj.pColSprite.SetColor(0.0f, 1.0f, 0.0f, 1.0f);
            if (explosion.pSpriteProxy != null && explosion.pSpriteProxy.pRealSprite != null)
            {
                explosion.pSpriteProxy.pRealSprite.SwapColor(0.0f, 1.0f, 0.0f, 1.0f);
            }
            explosion.Update();
            explosionRoot.Add(explosion);
            TimerEventMan.Add(TimerEvent.Name.Explosion, new ExplosionAnimCmd(explosion), 0.1f);
        }

        public override void Dump()
        {
            Debug.Assert(false);
        }

        public override System.Enum GetName()
        {
            return Name.WallBottomExplosionObserver;
        }
    }
}
