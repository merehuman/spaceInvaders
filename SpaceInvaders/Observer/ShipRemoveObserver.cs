//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Diagnostics;
using Azul;

namespace SE456
{
    public class ShipRemoveObserver : ColObserver
    {
        public override void Notify()
        {
            Ship ship = ShipMan.GetShip();

            TimerEventMan.Add(TimerEvent.Name.Sample1, new SampleCmd(ship), 0.1f);
            TimerEventMan.Add(TimerEvent.Name.Sample2, new SampleCmd(ship), 0.3f);
            TimerEventMan.Add(TimerEvent.Name.Sample3, new SampleCmd(ship), 0.5f);


            if (count == 3)
            {
                SpriteGame sprite = SpriteGameMan.Find(SpriteGame.Name.ReserveShip3);
                SpriteGameMan.Remove(sprite);
                count--;
                Font font = FontMan.Find(Font.Name.Reserve);
                font.UpdateMessage("2");
            }
            else if (count == 2)
            {
                SpriteGame sprite = SpriteGameMan.Find(SpriteGame.Name.ReserveShip2);
                SpriteGameMan.Remove(sprite);
                count--;
                Font font = FontMan.Find(Font.Name.Reserve);
                font.UpdateMessage("1");
            }
            else if (count == 1)
            {
                SpriteGame sprite = SpriteGameMan.Find(SpriteGame.Name.ReserveShip1);
                SpriteGameMan.Remove(sprite);
                count--;
                Font font = FontMan.Find(Font.Name.Reserve);
                font.UpdateMessage("0");
                //trigger game over???

                // Switch to Game Over Scene
                //SceneContext pSceneContext = SceneContext.GetInstance();
                //pSceneContext.SetState(SceneContext.Scene.Over);
            }
            else
            {
                //Debug.Assert(false);
            }
        }

        override public void Dump()
        {
            Debug.Assert(false);
        }
        override public System.Enum GetName()
        {
            return Name.ShipRemoveObserver;
        }

        public static int count = 3;
    }

    // data
}

// --- End of File ---
