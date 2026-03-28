//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace SE456
{
    public class InvaderGrid : Composite
    {
        public InvaderGrid()
            : base()
        {
            this.name = Name.InvaderGrid;

            this.poColObj.pColSprite.SetColor(0, 0, 1);

            this.delta = 1.0f;

            level = 1;
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction
            //if (collided == false)
            //{
            //    this.collided = true;
            //}
            other.VisitGroup(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdGroup vs MissileGroup
            //Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            // BirdGroup vs ShipRoot
            //Debug.WriteLine("         collide:  {0} <-> {1}", s.name, this.name);

            // ShipRoot vs Columns
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }

        public override void VisitShip(Ship s)
        {
            GameObject pGameObj = (GameObject)IteratorForwardComposite.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }

        public override void Update() //THIS HAD A UNION IN IT TO I NEED TO BRING BACK THIS METHOD???
        {
            //Debug.WriteLine("update: {0}", this);
            base.BaseUpdateBoundingBox(this);

            // proof its working
            //this.poColObj.poColRect.width -= 40.0f;

            base.Update();
        }

        public void MoveGrid()
        {
            if (collided && !prevCollided)
            {
                y = 20.0f;
                //collided = false;
            }

            prevCollided = collided;

            float movementSpeed = 4.0f + ((16.0f - 4.0f) * (55 - GetTotalInvaders()) / 55) * speed;

            IteratorForwardComposite pFor = new IteratorForwardComposite(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += movementSpeed * delta;
                pGameObj.y -= y;

                pNode = pFor.Next();
            }

            y = 0.0f;

            if (collided)
            {
                collided = false;
            }
        }

        public float GetDelta()
        {
            return this.delta;
        }

        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }
        public void SetY(float inY)
        {
            this.y = inY;
        }

        public static void DecreaseInvaderCount()
        {
            if (totalInvaders > 0)
            {
                totalInvaders--;
            }

            else if (totalInvaders == 1)
            {
                ScoreManager.ResetScore();
            }
            else
            {
                ScoreManager.ResetScore();
            }
        }

        private void ClearAllColumns()
        {
            GameObject pColumn = (GameObject)IteratorForwardComposite.GetChild(this);
            while (pColumn != null)
            {
                GameObject pNextCol = (GameObject)IteratorForwardComposite.GetSibling(pColumn);

                GameObject pInv = (GameObject)IteratorForwardComposite.GetChild(pColumn);
                while (pInv != null)
                {
                    GameObject pNextInv = (GameObject)IteratorForwardComposite.GetSibling(pInv);
                    pInv.Remove();
                    pInv = pNextInv;
                }

                pColumn.Remove();
                pColumn = pNextCol;
            }
        }

        public void RestoreInitialFormation()
        {
            InvaderFactory IF = new InvaderFactory(SpriteBatch.Name.Invaders, SpriteBatch.Name.Boxes);

            totalInvaders = 55;
            level = 1;
            speed = 1.0f;
            collided = false;
            prevCollided = false;
            delta = 1.0f;

            ClearPendingVictory();

            ClearAllColumns();

            for (int i = 0; i < 11; i++)
            {
                float st = 86.0f;

                InvaderColumn pNewColumn = (InvaderColumn)IF.Create(GameObject.Name.InvaderColumn);
                this.Add(pNewColumn);

                pNewColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 400.0f));
                pNewColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 450.0f));
                pNewColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 500.0f));
                pNewColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 550.0f));
                pNewColumn.Add(IF.Create(GameObject.Name.Squid, st + i * 50.0f, 600.0f));
            }
        }

        /// <summary>Set when the third wave is fully cleared (level becomes 4). ScenePlay consumes via PopPendingVictory().</summary>
        private static bool pendingVictory = false;

        public static bool PopPendingVictory()
        {
            if (!pendingVictory)
            {
                return false;
            }
            pendingVictory = false;
            return true;
        }

        private static void ClearPendingVictory()
        {
            pendingVictory = false;
        }

        public static void NewLevel()
        {
            level++;

            InvaderFactory IF = new InvaderFactory(SpriteBatch.Name.Invaders, SpriteBatch.Name.Boxes);

            InvaderGrid pGrid = (InvaderGrid)GameObjectNodeMan.Find(GameObject.Name.InvaderGrid);
            if (pGrid == null)
            {
                pGrid = (InvaderGrid)IF.Create(GameObject.Name.InvaderGrid);
                GameObjectNodeMan.Attach(pGrid);
            }

            pGrid.ClearAllColumns();

            // Levels 2 and 3 spawn new waves; after clearing the third wave, level is 4 → victory (no new formation).
            if (level > 3)
            {
                pendingVictory = true;
                return;
            }

            totalInvaders = 55;

            for (int i = 0; i < 11; i++)
            {
                float st = 86.0f;

                InvaderColumn pColumn = (InvaderColumn)IF.Create(GameObject.Name.InvaderColumn);
                pGrid.Add(pColumn);

                pColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 350.0f));
                pColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 400.0f));
                pColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 450.0f));
                pColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 500.0f));
                pColumn.Add(IF.Create(GameObject.Name.Squid, st + i * 50.0f, 550.0f));
            }

            pGrid.SetSpeed(1.5f);
        }

        public static int GetTotalInvaders()
        {
            return totalInvaders;
        }

        public void SetCollided(bool value)
        {
            collided = value;
        }

        public bool GetCollided()
        {
            return collided;
        }

        public void SetSpeed(float inSpeed)
        {
            speed = inSpeed;
        }


        // Data: ---------------
        private float delta;
        private float x = 4.0f;
        private float y = 0.0f;
        private static int totalInvaders = 55;  //need to make this data drivens
        private bool collided = false;
        private bool prevCollided = false;
        private static int level = 1;
        private static float speed = 1.0f;
    }

}

// --- End of File ---
