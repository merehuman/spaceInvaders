//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//-----------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace SE456
{
    public class UFOSpawnEvent : Command
    {
        public UFOSpawnEvent(Random pRandom, IrrKlang.ISoundEngine sndEngine, IrrKlang.ISoundSource low, IrrKlang.ISoundSource high)
        {
            this.pUFORoot = GameObjectNodeMan.Find(GameObject.Name.UFORoot);
            Debug.Assert(this.pUFORoot != null);

            this.pSB_Invaders = SpriteBatchMan.Find(SpriteBatch.Name.Invaders);
            Debug.Assert(this.pSB_Invaders != null);

            this.pSB_Boxes = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            Debug.Assert(this.pSB_Boxes != null);

            this.pRandom = pRandom;

            this.sndEngine = sndEngine;
            this.low = low;
            this.high = high;
        }

        override public void Execute(float deltaTime)
        {
            //Debug.WriteLine("event: {0}", deltaTime);

            // Create missile
            float side = this.pRandom.Next(1,3);
            float value = 0.0f;

            if (side == 1)
            {
                value = 0.0f;
                direction = 1.0f;
                current = low;
            }
            else
            {
                value = 672.0f;
                direction = -1.0f;
                current = high;
            }


            UFO pUFO = (UFO)GameObjectNodeMan.Find(GameObject.Name.UFO);

            if (pUFO == null)
            {
                pUFO = new UFO(SpriteGame.Name.UFO, value, 650, direction);
            }
            else
            {
                pUFO.x = value;
                pUFO.y = 650;
                pUFO.ChangeDirection();
            }

            sndEngine.SoundVolume = 0.2f;
            sndEngine.Play2D(current, false, false, false);

            pUFO.ActivateCollisionSprite(pSB_Boxes);
            pUFO.ActivateSprite(pSB_Invaders);

            // Attach the missile to the missile root
            GameObject pUFORoot = GameObjectNodeMan.Find(GameObject.Name.UFORoot);
            Debug.Assert(pUFORoot != null);

            // Add to GameObject Tree - {update and collisions}
            pUFORoot.Add(pUFO);

            float time = (float)pRandom.Next(100, 10000) / 1000.0f + 10.0f;
            TimerEventMan.Add(TimerEvent.Name.UFOSpawn, this, time);
        }


        GameObject pUFORoot;
        SpriteBatch pSB_Invaders;
        SpriteBatch pSB_Boxes;
        Random pRandom;
        float direction;
        IrrKlang.ISoundEngine sndEngine = null;
        IrrKlang.ISoundSource low = null;
        IrrKlang.ISoundSource high = null;
        IrrKlang.ISoundSource current = null;
    }
}

// --- End of File ---
