//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Data.Common;
using System.Diagnostics;

namespace SE456
{
    class BombSpawnEvent : Command
    {
        public BombSpawnEvent(Random pRandom)
        {
            this.pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(this.pBombRoot != null);

            this.pSB_Invaders = SpriteBatchMan.Find(SpriteBatch.Name.Invaders);
            Debug.Assert(this.pSB_Invaders != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;
        }

        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);
            GameObject pGameObj = null;

            UFORoot uFORoot = (UFORoot)GameObjectNodeMan.Find(GameObject.Name.UFORoot);
            UFO uFO = (UFO)IteratorForwardComposite.GetChild(uFORoot);

            if (uFO != null && pRandom.Next(0, 10) == 0) // 1 in 10 chance
            {
                pGameObj = uFO;
            }
            else
            {
                InvaderGrid pGrid = (InvaderGrid)GameObjectNodeMan.Find(GameObject.Name.InvaderGrid);
                if (pGrid == null)
                {
                    return;
                }

                //get columns
                GameObject pColumns = (GameObject)IteratorForwardComposite.GetChild(pGrid); //list of columns
                if (pColumns == null)
                {
                    return;
                }

                // get random column
                pGameObj = pColumns;
                int randomColumn = pRandom.Next(0, 11);
                for (int i = 0; i < randomColumn; i++)
                {
                    pGameObj = (GameObject)IteratorForwardComposite.GetSibling(pGameObj);
                    if (pGameObj == null)
                    {
                        return;
                    }
                }

                GameObject pChild = (GameObject)IteratorForwardComposite.GetChild(pGameObj);
                while (pChild != null)
                {
                    pGameObj = pChild;
                    pChild = (GameObject)IteratorForwardComposite.GetSibling(pChild);
                }
            }

            float x = pGameObj.x;
            float y = pGameObj.y;

            if (x < 30.0f || x > 642.0f)
            {
                return;
            }

            int randBomb = pRandom.Next(0, 3);
            Bomb pBomb = null;
            switch (randBomb)
            {
                case 0:
                    pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombStraight, new FallStraight(), x, y);
                    break;
                case 1:
                    pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombZigZag, new FallZigZag(), x, y);
                    break;
                case 2:
                    pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.BombCross, new FallDagger(), x, y);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            pBomb.ActivateCollisionSprite(this.pSB_Boxes);
            pBomb.ActivateSprite(this.pSB_Invaders);

            // Attach the missile to the Bomb root
            GameObject pBombRoot = GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            Debug.Assert(pBombRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pBombRoot.Add(pBomb);

            // add another timer event back on
            //float time = (float)pRandom.Next(100, 10000) / 1000.0f + 2.0f;
            float time = 11.0f;
            float newDeltaTime = time + deltaTime;
            Debug.WriteLine("set--->time: {0} ", newDeltaTime);
            TimerEventMan.Add(TimerEvent.Name.BombSpawn, new BombSpawnEvent(pRandom), newDeltaTime);

            //for (int i = 0; i < 10; i++)
            //{
            //    //float time = (float)pRandom.Next(100, 10000) / 1000.0f + 1.0f;
            //    TimerEventMan.Add(TimerEvent.Name.BombSpawn, new BombSpawnEvent(pRandom), time);
            //}
        }

        GameObject pBombRoot;
        SpriteBatch pSB_Invaders;
        SpriteBatch pSB_Boxes;
        Random pRandom;
    }
}

// --- End of File ---
