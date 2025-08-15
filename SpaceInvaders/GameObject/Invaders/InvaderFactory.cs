//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class InvaderFactory
    {
        public InvaderFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name pSpriteBatchBox)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);
            this.pSpriteBatchBox = SpriteBatchMan.Find(pSpriteBatchBox);
            Debug.Assert(this.pSpriteBatchBox != null);
        }


        public GameObject Create(GameObject.Name name, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            //ltn - owned by gameobject? or each indiv class?
            switch (name)
            {
                case GameObject.Name.Crab:
                    pGameObj = new Crab(SpriteGame.Name.Crab, posX, posY);
                    break;

                case GameObject.Name.Octopus:
                    pGameObj = new Octopus(SpriteGame.Name.Octopus, posX, posY);
                    break;

                case GameObject.Name.Squid:
                    pGameObj = new Squid(SpriteGame.Name.Squid, posX, posY);
                    break;

                case GameObject.Name.InvaderGrid:
                    pGameObj = new InvaderGrid();
                    break;

                case GameObject.Name.InvaderColumn:
                    pGameObj = new InvaderColumn();
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the gameObjectManager
            Debug.Assert(pGameObj != null);
            
            pGameObj.ActivateSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pSpriteBatchBox);

            return pGameObj;
        }


        // Data: ---------------------

        SpriteBatch pSpriteBatch;
        SpriteBatch pSpriteBatchBox;
    }
}

// --- End of File ---
