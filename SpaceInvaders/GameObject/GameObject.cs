//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    abstract public class GameObject : Component
    {
        public enum Name
        {
            InvaderGrid,
            InvaderColumn,

            ShipRoot,
            Ship,
            ReserveShip,

            UFO,
            UFORoot,

            Missile,
            MissileGroup,

            WallGroup,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,

            BumperRoot,
            BumperRight,
            BumperLeft,

            Bomb,
            BombRoot,
            ExplosionRoot,
            Explosion,

            ShieldRoot,
            ShieldGrid,
            ShieldColumn_0,
            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,
            ShieldBrick,

            Octopus,
            Squid,
            Crab,

            NullObject,
            Uninitialized
        }

        protected GameObject(Component.Container type, 
                                GameObject.Name gameName, 
                                SpriteGame.Name proxyName)
            :base(type)
        {
            this.name = gameName;
            this.x = 0.0f;
            this.y = 0.0f;
            
            this.bMarkForDeath = false;
            this.spriteName = proxyName;
            // BUG... this needs to be ADD not FIND  (10 hrs to find that)
            SpriteGameProxy pProxy = SpriteGameProxyMan.Add(proxyName);
            Debug.Assert(pProxy != null);

            this.pSpriteProxy = pProxy;

            this.poColObj = new ColObject(this.pSpriteProxy);
            Debug.Assert(this.poColObj != null);
        }

        protected GameObject(Component.Container type, 
                                GameObject.Name gameName, 
                                SpriteGame.Name _spriteName,
                                float _x, 
                                float _y)
            :base(type)
        {
            this.name = gameName;
            this.x = _x;
            this.y = _y;

            this.bMarkForDeath = false;
            this.spriteName = _spriteName;
            this.pSpriteProxy = SpriteGameProxyMan.Add(this.spriteName);

            // Owned by the gameobject... don't delete it
            // its going on the ghostMan
            this.poColObj = new ColObject(this.pSpriteProxy);
            Debug.Assert(this.poColObj != null);
        }
        override public void Resurrect()
        {
            this.bMarkForDeath = false;

            Debug.Assert(this.pSpriteProxy != null);
            Debug.Assert(this.poColObj != null);

            //this.pSpriteProxy = SpriteProxyMan.Add(this.spriteName);

            //// poColObj is still valid... don't renew it
            //// Need more work on this
            //// the new is temporary.. need a "update" to reset ColObject
            //// for now call new
            // this.poColObj = new ColObject(this.pSpriteProxy);

            this.poColObj.Resurrect(this.pSpriteProxy);

            Debug.Assert(this.poColObj != null);
            base.Resurrect();
        }

        ~GameObject()
        {

        }
        public virtual void Remove()
        {
            // Remove from SpriteBatch (skip if never attached — wrong active SpriteBatchMan)
            Debug.Assert(this.pSpriteProxy != null);
            if (this.pSpriteProxy.HasSpriteNode())
            {
                SpriteNode pSpriteNode = this.pSpriteProxy.GetSpriteNode();
                SpriteBatchMan.Remove(pSpriteNode);
            }

            // 1) Don't Need to remove it from ProxySpriteMan
            // Since it is being recycled on the GhostMan

            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);
            if (this.poColObj.pColSprite.HasSpriteNode())
            {
                SpriteNode pSpriteNode = this.poColObj.pColSprite.GetSpriteNode();
                SpriteBatchMan.Remove(pSpriteNode);
            }

            // 2) Don't Need to remove it from BoxSpriteMan
            // Since it is being recycled on the GhostMan

            // Remove from GameObjectMan
            GameObjectNodeMan.Remove(this);

            // future is now
            GhostMan.Attach(this);

        }
        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            ColRect ColTotal = this.poColObj.poColRect;

            // Get the first child
            pNode = (GameObject)IteratorForwardComposite.GetChild(pNode);

            if (pNode != null)
            {
            // Initialized the union to the first block
            ColTotal.Set(pNode.poColObj.poColRect);
  
            // loop through sliblings
            while (pNode != null)
            {
                ColTotal.Union(pNode.poColObj.poColRect);
  
                // go to next sibling
                pNode = (GameObject)IteratorForwardComposite.GetSibling(pNode);
            }
  
            this.x = this.poColObj.poColRect.x;
            this.y = this.poColObj.poColRect.y;
  
            //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
            }
            else if (this.type == Component.Container.COMPOSITE)
            {
                // No children: do not keep the previous union (collision matches reality; stale union was fixed earlier).
                ColTotal.Set(0.0f, 0.0f, 0.0f, 0.0f);
                this.x = 0.0f;
                this.y = 0.0f;
            }
        }

        public virtual void Update()
        {
            Debug.Assert(this.pSpriteProxy != null);
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.pSpriteProxy.x = this.x;
            this.pSpriteProxy.y = this.y;

            this.poColObj.UpdatePos(this.x, this.y);
            this.poColObj.pColSprite.Update();
        }
        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.poColObj != null);
            privDetachSpriteBaseIfBatched(this.poColObj.pColSprite);
            pSpriteBatch.Attach(this.poColObj.pColSprite);
        }
        public void ActivateSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            privDetachSpriteBaseIfBatched(this.pSpriteProxy);
            pSpriteBatch.Attach(this.pSpriteProxy);
        }

        private static void privDetachSpriteBaseIfBatched(SpriteBase pSprite)
        {
            if (pSprite != null && pSprite.HasSpriteNode())
            {
                SpriteBatchMan.Remove(pSprite.GetSpriteNode());
            }
        }
        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.poColObj != null);
            Debug.Assert(this.poColObj.pColSprite != null);

            this.poColObj.pColSprite.SetColor(red, green, blue);
        }
		override public void Dump()
		{
            // Data:
            Debug.WriteLine("");
            Debug.WriteLine("\tGameObject: --------------");
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.pSpriteProxy != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.pSpriteProxy.name);
                if (this.pSpriteProxy.pRealSprite == null)
                {
                    Debug.WriteLine("\t\t    pRealSprite: null");
                }
                else
                {
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pSpriteProxy.pRealSprite.GetName());
            }
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);

			base.baseDump();
        
		}

        override public Enum GetName()
        {
            return this.name;
        }


        public ColObject GetColObject()
        {
            Debug.Assert(this.poColObj != null);
            return this.poColObj;
        }

        // ------------------------------------
        // Data: 
        // ------------------------------------

        public GameObject.Name name;
        public SpriteGame.Name spriteName;
        public float x;
        public float y;
        
        public bool bMarkForDeath;

        public SpriteGameProxy pSpriteProxy;
        public ColObject poColObj;
    }

}

// --- End of File ---
