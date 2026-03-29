//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System.Diagnostics;

namespace SE456
{
    class WallRoofExplosionObserver : ColObserver
    {
        public override void Notify()
        {
            GameObject a = pSubject.pObjA;
            GameObject b = pSubject.pObjB;
            Missile m = null;
            if (a is Missile && b is WallTop)
            {
                m = (Missile)a;
            }
            else if (b is Missile && a is WallTop)
            {
                m = (Missile)b;
            }
            else
            {
                return;
            }

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

            // SpriteGame.ExplosionRoof → Image.Name.ExplosionRoof (ScenePlay.Initialize)
            Explosion explosion = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.ExplosionRoof, m.x, m.y);
            explosion.ActivateSprite(pSBInvaders);
            explosion.ActivateCollisionSprite(pSBBoxes);
            explosion.poColObj.pColSprite.SetColor(1.0f, 0.0f, 0.0f);
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
            return Name.WallRoofExplosionObserver;
        }
    }
}
