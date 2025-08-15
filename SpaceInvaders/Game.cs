//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 

using System;
using System.Diagnostics;

namespace SE456
{
	public class SpaceInvaders : Azul.Game
	{

        SceneContext pSceneContext = null;


        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
		{
			// Game Window Device setup
            this.SetWindowName("Space Invaders");
            this.SetWidthHeight(672, 768);
			this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
		}

		//-----------------------------------------------------------------------------
		// Game::LoadContent()
		//		Allows you to load all content needed for your engine,
		//	    such as objects, graphics, etc.
		//-----------------------------------------------------------------------------
		public override void LoadContent()
		{
			//-----------------------------------
			// Load Managers
			//-----------------------------------

            Simulation.Create();
            TextureMan.Create();
            ImageMan.Create();
            SpriteGameMan.Create();

            //---------------------------------------------------------------------------------------------------------
            // REWORK - SpriteBatchMan
            //    It's a singleton... but it holds an actual instance pointer to the SpriteBatchMan instance
            //    this way scene can own their unique sprite batch man independent of other scenes
            //
            // SpriteBatchMan.Create(3, 1); 
            //                    
            // Once you understand this... you have to do this for many other systems
            //---------------------------------------------------------------------------------------------------------

			SpriteBatchMan.Create();
            TimerEventMan.Create();
            SpriteBoxMan.Create();
            SpriteGameProxyMan.Create();
            GameObjectNodeMan.Create(); 
            ColPairMan.Create();
            GlyphMan.Create();
            FontMan.Create();
            GhostMan.Create();

            // Create the context
            pSceneContext = SceneContext.GetInstance();

            this.ResetTime();
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        bool oneKeyCurr;
        bool oneKeyPrev;
        bool twoKeyCurr;
        bool twoKeyPrev;
        bool threeKeyCurr;
        bool threeKeyPrev;
        public override void Update()
		{

            GlobalTimer.Update(this.GetTime());


            oneKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_1);
            if (oneKeyCurr == true && oneKeyPrev == false)
            {
                pSceneContext.SetState(SceneContext.Scene.Select);
            }
            oneKeyPrev = oneKeyCurr;


            twoKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_2);
            if (twoKeyCurr == true && twoKeyPrev == false)
            {
                pSceneContext.SetState(SceneContext.Scene.Play);
            }
            twoKeyPrev = twoKeyCurr;

            threeKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_3);
            if (threeKeyCurr == true && threeKeyPrev == false)
            {
                pSceneContext.SetState(SceneContext.Scene.Over);
            }
            threeKeyPrev = threeKeyCurr;


            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_T) == true)
            {
                SpriteBatch pSB = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
                Debug.Assert(pSB != null);
                pSB.SetDrawEnable(false);
            }

            if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_Y) == true)
            {
                SpriteBatch pSB = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
                Debug.Assert(pSB != null);
                pSB.SetDrawEnable(true);
            }

            // Update the scene
            pSceneContext.GetState().Update(this.GetTime());


        }

		//-----------------------------------------------------------------------------
		// Game::Draw()
		//		This function is called once per frame
		//	    Use this for draw graphics to the screen.
		//      Only do rendering here
		//-----------------------------------------------------------------------------
		public override void Draw()
		{
            //SpriteBatchMan.Draw();
            // Draw the scene
            pSceneContext.GetState().Draw();


		}

		//-----------------------------------------------------------------------------
		// Game::UnLoadContent()
		//       unload content (resources loaded above)
		//       unload all content that was loaded before the Engine Loop started
		//-----------------------------------------------------------------------------
		public override void UnLoadContent()
		{
            GhostMan.Destroy();            
            FontMan.Destroy();
            GlyphMan.Destroy();
            ColPairMan.Destroy();
            GameObjectNodeMan.Destroy();
            SpriteGameProxyMan.Destroy();
            TimerEventMan.Destroy();
            SpriteBoxMan.Destroy();
			SpriteBatchMan.Destroy();
			SpriteGameMan.Destroy();
			ImageMan.Destroy();
			TextureMan.Destroy();
		}

		public override void DisplayHeader()
		{
			Console.Write(this.Header());
		}

		public override void DisplayFooter()
		{
			Console.Write(this.Footer());
		}

	}
}

// --- End of File ---
