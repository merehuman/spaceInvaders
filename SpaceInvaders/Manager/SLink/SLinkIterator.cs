//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;

namespace SE456
{
    public class SLinkIterator : Iterator
    {
        public SLinkIterator(  )
            : base()
        {
            this.pStart = null;

            this.privInitialize(this.pStart);
        }

        public void Reset( SLink pHead )
        {
            this.pStart = pHead;

            this.privInitialize(this.pStart);
        }
        override public NodeBase Next()
        {
            SLink pLink = (SLink)this.pCurr;

            if (pLink != null)
            {
                pLink = pLink.pNext;
                this.pCurr = (NodeBase)pLink;
            }

            if (pLink == null)
            {
                bDone = true;
            }

            return (NodeBase)pLink;

        }
        override public bool IsDone()
        {
            return bDone;
        }

        override public NodeBase First()
        {
            this.privInitialize(this.pStart);

            return this.pFirst;
        }

        override public NodeBase Current()
        {
            return this.pCurr;
        }

        private void privInitialize(NodeBase _pStart)
        {
            this.pFirst = _pStart;
            this.pCurr = _pStart;

            if (_pStart == null)
            {
                this.bDone = true;
            }
            else
            {
                this.bDone = false;
            }
        }

        // ------------------------
        // Data:
        // ------------------------
        NodeBase pStart;
        NodeBase pFirst;
        NodeBase pCurr;
        bool bDone;
    }
}

// --- End of File ---

