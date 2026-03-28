//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    public class SndObserver : ColObserver
    {
        public SndObserver(IrrKlang.ISoundEngine pEng, IrrKlang.ISoundSource _pSndSrc)
        {
            Debug.Assert(pEng != null);
            this.pSndEngine = pEng;

            Debug.Assert(_pSndSrc != null);
            this.pSndSrc = _pSndSrc;
        }

        public void PlaySound()
        {
            Debug.Assert(pSndEngine != null);
            Debug.Assert(pSndSrc != null);
            pSndEngine.SoundVolume = 0.2f;
            pSndEngine.Play2D(pSndSrc, false, false, false);
        }

        public override void Notify()
        {
            //Debug.WriteLine(" Snd_Observer: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            PlaySound();
        }



        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.SoundObserver;
        }


        // Data
        IrrKlang.ISoundEngine pSndEngine;
        IrrKlang.ISoundSource pSndSrc;
    }
}

// --- End of File ---
