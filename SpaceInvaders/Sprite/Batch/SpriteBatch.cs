//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;

namespace SE456
{
    public class SpriteBatch : DLink
    {
        //------------------------------------
        // Enum
        //------------------------------------
        public enum Name
        {
            PacMan,
            AngryBirds,
            Box,
            Texts,
            Boxes,
            Shields,
            Misc,

            ReserveShips,

            Invaders,
            Aliens,
   
            Uninitialized
        }

        //------------------------------------
        // Constructors
        //------------------------------------
        public SpriteBatch()
            : base()
        {
            this.DrawEnable = true;
            this.mName = SpriteBatch.Name.Uninitialized;
            this.priority = 0;

            this.poSpriteNodeMan = new SpriteNodeMan();
            Debug.Assert(this.poSpriteNodeMan != null);
        }

        //------------------------------------
        // Methods
        //------------------------------------
        public void Set(SpriteBatch.Name _name, float _priority, int _reserveNum = 3, int _reserveGrow = 1)
        {
			Debug.Assert(_name != SpriteBatch.Name.Uninitialized);

            this.DrawEnable = true;
            this.mName = _name;
            this.priority = _priority;
            this.poSpriteNodeMan.Set(_name, _reserveNum, _reserveGrow);
        }

        public void SetName(SpriteBatch.Name _name)
        {
            this.mName = _name;
        }

        public SpriteNodeMan GetSpriteNodeMan()
        {
            return this.poSpriteNodeMan;
        }

        public SpriteNode Attach(GameObject pGameObj)
        {
            Debug.Assert(pGameObj != null);
            SpriteNode pNode = this.poSpriteNodeMan.Attach(pGameObj.pSpriteProxy);


            // Initialize SpriteBatchNode
            pNode.Set(pGameObj.pSpriteProxy, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);

            return pNode;
        }

        public SpriteNode Attach(SpriteBase _pNode)
        {
            SpriteNode pNode = this.poSpriteNodeMan.Attach(_pNode);

            // Initialize SpriteBatchNode
            pNode.Set(_pNode, this.poSpriteNodeMan);

            // Back pointer
            this.poSpriteNodeMan.SetSpriteBatch(this);

            return pNode;
        }

		//------------------------------------
		// Override
		//------------------------------------
		override public Enum GetName()
		{
			return this.mName;
		}
        override public float GetPriority()
        {
            return this.priority;
        }
        override public void Wash()
        {
            this.DrawEnable = true;
            this.mName = Name.Uninitialized;

            base.baseWash();
        }
        override public void Dump()
        {
            // we are using HASH code as its unique identifier 
            Debug.WriteLine("   {0} ({1})", this.GetName(), this.GetHashCode());

            // Data:
            Debug.WriteLine("   Name: {0} ({1})", this.GetName(), this.GetHashCode());

			base.baseDump();
		}
        public void SetDrawEnable(bool status)
        {
            this.DrawEnable = status;
        }
        public bool GetDrawEnable()
        {
            return this.DrawEnable;
        }
        //------------------------------------
        // Data
        //------------------------------------
        private bool DrawEnable;
        public Name mName;
        public float priority;
        private readonly SpriteNodeMan poSpriteNodeMan;
    }
}

// --- End of File ---
