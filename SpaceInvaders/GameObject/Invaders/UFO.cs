//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class UFO : InvaderCategory
    {
        public UFO(SpriteGame.Name spriteName, float posX, float posY, float direction)
        : base(GameObject.Name.Crab, spriteName, posX, posY)
        {
            this.poColObj.pColSprite.SetColor(1, 0, 0);
            this.delta = 3.0f;
            this.direction = direction;
        }
        override public Enum GetName()
        {
            return InvaderCategory.Type.UFO;
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an GreenBird
            // Call the appropriate collision reaction            
            other.VisitUFO(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallTop
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs WallTop
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            //   Debug.WriteLine("update: {0}", this);
            base.Update();
            this.x += delta * direction;
        }

        public void ChangeDirection()
        {
            this.direction *= -1.0f;
        }

        // Data: ---------------
        public float delta;
        public float direction;

    }
}

// --- End of File ---
