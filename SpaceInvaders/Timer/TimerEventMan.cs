//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class TimerEventMan : ManBase
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private TimerEventMan(int reserveNum, int reserveGrow)
                : base(new DLinkMan(), new DLinkMan(), reserveNum, reserveGrow)   
        {
            // initialize derived data here
            this.poNodeCompare = new TimerEvent();
            this.mCurrTime = 0.0f;
        }

        //----------------------------------------------------------------------
        // Static Methods
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TimerEventMan(reserveNum, reserveGrow);
            }

        }
        public static void Destroy(bool bPrintEnable = false)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
            if (bPrintEnable)
            {
                TimerEventMan.DumpStats();
            }
        }

        public static TimerEvent Add(TimerEvent.Name timeName, Command pCommand, float deltaTimeToTrigger)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            TimerEvent pNode = (TimerEvent)pMan.baseAdd(deltaTimeToTrigger); //add in order
            Debug.Assert(pNode != null);

            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTimeToTrigger >= 0.0f);

            pNode.Set(timeName, pCommand, deltaTimeToTrigger);
            return pNode;
        }

        public static TimerEvent Find(TimerEvent.Name name)
        {
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // Compare functions only compares two Nodes

            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;

            TimerEvent pData = (TimerEvent)pMan.baseFind(pMan.poNodeCompare);
            return pData;
        }

        public static void Remove(TimerEvent pImage)
        {
            Debug.Assert(pImage != null);

            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseRemove(pImage);
        }
        public static void Dump()
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");

            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDump();

        }
        public static void DumpStats()
        {
            Debug.WriteLine("\n   ------ TimerEvent Man: ------");

            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            pMan.baseDumpStats();

            Debug.WriteLine("   ------------\n");
        }
        public static void PauseUpdate(float delta)
        {
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // walk the list
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            // Update the times
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                TimerEvent pEvent = (TimerEvent)pIt.Current();
                pEvent.triggerTime += delta;
            }

        }
        public static void Update(float totalTime)
        {
           // Debug.WriteLine("Time: {0}", totalTime);
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.mCurrTime = totalTime;

            // walk through the list and execute
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            TimerEvent pNode = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted then its an early out
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                pNode = (TimerEvent)pIt.Current();
                if (pMan.mCurrTime > pNode.triggerTime)
                {
                    // call it
                    pNode.Process();

                    Debug.WriteLine("TimerEvent: {0} processed at time: {1}", pNode.name, pMan.mCurrTime);

                    // remove from list
                    pIt.Erase(pMan);
                }
            }

        }
        public static float GetCurrTime()
        {
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // return time
            return pMan.mCurrTime;
        }

        public static void ClearAll()
        {
            // Get the instance
            TimerEventMan pMan = TimerEventMan.privGetInstance();
            Debug.Assert(pMan != null);

            // walk the list
            Iterator pIt = pMan.baseGetIterator();
            Debug.Assert(pIt != null);

            // remove all nodes
            for (pIt.First(); !pIt.IsDone(); pIt.Next())
            {
                TimerEvent pEvent = (TimerEvent)pIt.Current();
                pIt.Erase(pMan);
            }
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerEventMan privGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected NodeBase derivedCreateNode()
        {
            NodeBase pNodeBase = new TimerEvent();
            Debug.Assert(pNodeBase != null);

            return pNodeBase;
        }

        //----------------------------------------------------------------------
        // Data: unique data for this manager 
        //----------------------------------------------------------------------
        private readonly TimerEvent poNodeCompare;
        private static TimerEventMan pInstance = null;
        protected float mCurrTime;
    }
}

// --- End of File ---

