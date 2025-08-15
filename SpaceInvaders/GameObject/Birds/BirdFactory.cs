//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class BirdFactory
    {

        public BirdFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pSpriteBoxBatch = SpriteBatchMan.Find(boxSpriteBatchName);
            Debug.Assert(this.pSpriteBoxBatch != null);
        }

        public GameObject Create(GameObject.Name name, BirdCategory.Type type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case BirdCategory.Type.Green:
                    pGameObj = new BirdGreen(SpriteGame.Name.GreenBird, posX, posY);
                    break;

                case BirdCategory.Type.Red:
                    pGameObj = new BirdRed(SpriteGame.Name.RedBird, posX, posY);
                    break;

                case BirdCategory.Type.White:
                    pGameObj = new BirdWhite(SpriteGame.Name.WhiteBird, posX, posY);
                    break;

                case BirdCategory.Type.Yellow:
                    pGameObj = new BirdYellow(SpriteGame.Name.YellowBird, posX, posY);
                    break;

                case BirdCategory.Type.Grid:
                    pGameObj = new BirdGrid();
                    break;

                case BirdCategory.Type.Column:
                    pGameObj = new BirdColumn();
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }


            // Attached to Group
            pGameObj.ActivateSprite(this.pSpriteBatch);;
            pGameObj.ActivateCollisionSprite(this.pSpriteBoxBatch);
            return pGameObj;
        }

        // Data: ---------------------

        private readonly SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pSpriteBoxBatch;
    }
}

// --- End of File ---
