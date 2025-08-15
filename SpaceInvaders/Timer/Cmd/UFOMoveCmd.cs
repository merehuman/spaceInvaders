//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class UFOMoveCmd : Command
    {
        public UFOMoveCmd(GameObject root)
        {
            Debug.Assert(root != null);
            this.pRoot = root;

            this.count = 0;
            this.direction = 1.0f;
        }

        public override void Execute(float deltaTime)
        {
            //Debug.WriteLine(" {0} time:{1} ", this.pString, TimerEventMan.GetCurrTime());
            if (pRoot != null)
            {
                IteratorForwardComposite pFor = new IteratorForwardComposite(pRoot);

                Component pNode = pFor.First();
                while (!pFor.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;

                    if (pGameObj.x >= 672.0f)
                    {
                        direction = -1.0f;
                        //pGameObj.x -= 20.0f;
                        //this.pRoot = null;
                    }
                    else if (pGameObj.x <= 0.0f)
                    {
                        direction = 1.0f;
                        //pGameObj.x += 20.0f;
                        //this.pRoot = null;
                    }

                    pGameObj.x += 20.0f * direction;

                    pNode = pFor.Next();
                }

                TimerEventMan.Add(TimerEvent.Name.UFOMove, this, deltaTime + 0.1f);
            }

        }

        public int getCount()
        {
            return count;
        }

        private GameObject pRoot;
        int count;
        float direction;

    }
}

// --- End of File ---
