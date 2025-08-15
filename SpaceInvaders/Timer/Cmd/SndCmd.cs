//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
    class SndCmd : Command
    {
        public SndCmd(IrrKlang.ISoundEngine sndEngine, IrrKlang.ISoundSource snd0, IrrKlang.ISoundSource snd1, IrrKlang.ISoundSource snd2, IrrKlang.ISoundSource snd3, Drum pDrum)
        {
            Debug.Assert(sndEngine != null);
            this.pSndEngine = sndEngine;

            Debug.Assert(snd0 != null);
            Debug.Assert(snd1 != null);
            Debug.Assert(snd2 != null);
            Debug.Assert(snd3 != null);
            Debug.Assert(pDrum != null);
            this.pSnd0 = snd0;
            this.pSnd1 = snd1;
            this.pSnd2 = snd2;
            this.pSnd3 = snd3;
            this.pDrum = pDrum;
            this.count = 0;
        }

        public override void Execute(float deltaTime)
        {
            //Debug.WriteLine("SndCmd c:{0}", count);
            pSndEngine.SoundVolume = 0.2f;

            switch (count)
            {
                case 0:
                    pSndEngine.Play2D(pSnd0, false, false, false);
                    break;

                case 1:
                    pSndEngine.Play2D(pSnd1, false, false, false);
                    break;

                case 2:
                    pSndEngine.Play2D(pSnd2, false, false, false);
                    break;

                case 3:
                    pSndEngine.Play2D(pSnd3, false, false, false);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            this.count++;
            if (this.count > 3)
            {
                this.count = 0;
            }

            float newDeltaTime = pDrum.GetBeat();

            TimerEventMan.Add(TimerEvent.Name.Snd, this, newDeltaTime);
        }

        public int getCount()
        {
            return this.count;
        }

        private IrrKlang.ISoundEngine pSndEngine;
        private IrrKlang.ISoundSource pSnd0;
        private IrrKlang.ISoundSource pSnd1;
        private IrrKlang.ISoundSource pSnd2;
        private IrrKlang.ISoundSource pSnd3;
        int count;
        private Drum pDrum;


    }
}

// --- End of File ---
