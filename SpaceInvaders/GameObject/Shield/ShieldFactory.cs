//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    class ShieldFactory
    {
        private ShieldFactory()
        {
            this.pSpriteBatch = null;
            this.pCollisionSpriteBatch = null;
            this.pTree = null;
        }
        private void privSet(SpriteBatch.Name spriteBatchName, SpriteBatch.Name collisionSpriteBatch, Composite pTree)
        {
            this.pSpriteBatch = SpriteBatchMan.Find(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pCollisionSpriteBatch = SpriteBatchMan.Find(collisionSpriteBatch);
            Debug.Assert(this.pCollisionSpriteBatch != null);

            Debug.Assert(pTree != null);
            this.pTree = pTree;
        }
        private void privSetParent(GameObject pParentNode)
        {
            // OK being null
            Debug.Assert(pParentNode != null);
            this.pTree = (Composite)pParentNode;
        }
        ~ShieldFactory()
        {
        }
        private GameObject privCreate(ShieldCategory.Type type, GameObject.Name gameName, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pShield = null;

            GameObjectNode pGameObjNode = GhostMan.Find(gameName);
            if (pGameObjNode != null)
            {
                pShield = pGameObjNode.pGameObj;
                GhostMan.Remove(pGameObjNode);

                //GhostMan.Dump();

                switch (type)
                {
                    case ShieldCategory.Type.Brick:
                    case ShieldCategory.Type.LeftTop1:
                    case ShieldCategory.Type.LeftTop0:
                    case ShieldCategory.Type.LeftBottom:
                    case ShieldCategory.Type.RightTop1:
                    case ShieldCategory.Type.RightTop0:
                    case ShieldCategory.Type.RightBottom:
                        ((ShieldBrick)pShield).Resurrect(posX,posY);
                        break;

                    case ShieldCategory.Type.Root:
                        Debug.Assert(false);
                        break;

                    case ShieldCategory.Type.Grid:
                        ((ShieldGrid)pShield).Resurrect(posX, posY);
                        break;

                    case ShieldCategory.Type.Column:
                        ((ShieldColumn)pShield).Resurrect(posX, posY); ;
                        break;

                    default:
                        // something is wrong
                        Debug.Assert(false);
                        break;
                }
            }
            else
            {
            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop1:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop1, posX, posY);
                    break;

                case ShieldCategory.Type.LeftTop0:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftTop0, posX, posY);
                    break;

                case ShieldCategory.Type.LeftBottom:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_LeftBottom, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop1:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop1, posX, posY);
                    break;

                case ShieldCategory.Type.RightTop0:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightTop0, posX, posY);
                    break;

                case ShieldCategory.Type.RightBottom:
                    pShield = new ShieldBrick(gameName, SpriteGame.Name.Brick_RightBottom, posX, posY);
                    break;

                case ShieldCategory.Type.Root:
                    Debug.Assert(false);
                    break;

                case ShieldCategory.Type.Grid:
                    pShield = new ShieldGrid(gameName, SpriteGame.Name.NullObject, posX, posY);
                    break;

                case ShieldCategory.Type.Column:
                    pShield = new ShieldColumn(gameName, SpriteGame.Name.NullObject, posX, posY);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }
            }

            // add to the tree
            //this.pTree.Add(pShield);

            // Attached to Group
            pShield.ActivateSprite(this.pSpriteBatch);
            pShield.ActivateCollisionSprite(this.pCollisionSpriteBatch);

            return pShield;
        }

        public static GameObject CreateShields(float posX = 0.0f, float posY = 0.0f)
        {
            ShieldFactory pFactory = ShieldFactory.privInstance();

            ShieldRoot pShieldRoot = (ShieldRoot)GameObjectNodeMan.Find(GameObject.Name.ShieldRoot);

            if (pShieldRoot == null)
            {
                pShieldRoot = new ShieldRoot(GameObject.Name.ShieldRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
                GameObjectNodeMan.Attach(pShieldRoot);
            }
            pFactory.privSet(SpriteBatch.Name.Shields, SpriteBatch.Name.Boxes, pShieldRoot);

            GameObject pGrid;
            for (int shields = 0; shields < 4; shields++)
            {
                // create a grid
                pGrid = pFactory.privCreate(ShieldCategory.Type.Grid, GameObject.Name.ShieldGrid);
                pShieldRoot.Add(pGrid);

                int columns = 11;
                int rows = 8;
                int numBricks = 0;
                float start_x = posX;
                float start_y = posY;
                float brickWidth = 6.0f;
                float brickHeight = 6.0f;

                float shieldPosX = posX + (shields * 150.0f);
                float shieldPosY = posY;
                GameObject pColumn;

                for (int col = 0; col < columns; ++col)
                {
                    //pFactory.privSetParent(pGrid);
                    pColumn = pFactory.privCreate(ShieldCategory.Type.Column, GameObject.Name.ShieldColumn_0 + col);
                    pGrid.Add(pColumn);
                    //pFactory.privSetParent(pColumn);

                    start_x = shieldPosX + (col * brickWidth);
                    start_y = shieldPosY;

                    if (col == 0 || col == 10)
                    {
                        numBricks = 6;
                    }
                    else if (col == 1 || col == 9)
                    {
                        numBricks = 7;
                    }
                    else if (col == 2 || col == 8)
                    {
                        numBricks = 8;
                    }
                    else if (col == 3 || col == 7)
                    {
                        numBricks = 7;
                        start_y += brickHeight;
                    }
                    else if (col == 4 || col == 6)
                    {
                        numBricks = 6;
                        start_y += (brickHeight * 2);
                    }
                    else if (col == 5)
                    {
                        numBricks = 6;
                        start_y += (brickHeight * 2);
                    }

                    for (int row = 0; row < numBricks; row++)
                    {
                        //pFactory.privSetParent(pColumn);
                        pColumn.Add(pFactory.privCreate(ShieldCategory.Type.Brick, GameObject.Name.ShieldBrick, start_x, start_y));
                        start_y += brickHeight;
                    }
                }
            }

            return pShieldRoot;
        }

        private static ShieldFactory privInstance()
        {
            if(pInstance == null)
            {
                ShieldFactory.pInstance = new ShieldFactory();
            }

            Debug.Assert(pInstance != null);

            return pInstance;
        }

        // Data: ---------------------
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;

        private static ShieldFactory pInstance = null;
    }
}

// --- End of File ---
