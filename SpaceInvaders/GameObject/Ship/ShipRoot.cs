//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class ShipRoot : Composite
    {
        public ShipRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.poColObj.pColSprite.SetColor(0, 0, 1);
        }

        ~ShipRoot()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitShipRoot(this);
        }

        public override void VisitBumperRoot(BumperRoot b)
        {
            //Debug.WriteLine("collide: {0} with {1}", this, b);

            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(b, pGameObj);
        }

        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs ShieldRoot
            ColPair.Collide((GameObject)IteratorForwardComposite.GetChild(b), this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Missile vs ShieldRoot
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void VisitGroup(InvaderGrid b)
        {
            ColPair.Collide((GameObject)IteratorForwardComposite.GetChild(b), this);
        }

        public override void VisitColumn(InvaderColumn b)
        {
            ColPair.Collide((GameObject)IteratorForwardComposite.GetChild(b), this);
        }

        public override void VisitCrab(Crab b)
        {
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void VisitOctopus(Octopus b)
        {
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void VisitSquid(Squid b)
        {
            ColPair.Collide(b, (GameObject)IteratorForwardComposite.GetChild(this));
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }


        // Data: ---------------


    }
}

// --- End of File ---

