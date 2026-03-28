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
        bool spaceMenuCurr;
        bool spaceMenuPrev;
        bool enterMenuCurr;
        bool enterMenuPrev;
        bool pKeyCurr;
        bool pKeyPrev;

        /// <summary>
        /// Align menu edge state with the keyboard so leaving/entering scenes does not
        /// treat a held key as a new press (e.g. Space still down from shooting).
        /// </summary>
        private void SyncMenuKeyStateFromKeyboard()
        {
            spaceMenuCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_SPACE);
            spaceMenuPrev = spaceMenuCurr;
            enterMenuCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_EXECUTE_BUTTON);
            enterMenuPrev = enterMenuCurr;
            pKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_P);
            pKeyPrev = pKeyCurr;
        }

        private void TryGoToPlay(bool usedSpaceToStart)
        {
            if (pSceneContext.IsPlayScene())
            {
                return;
            }

            pSceneContext.SetState(SceneContext.Scene.Play);
            if (usedSpaceToStart)
            {
                InputMan.AbsorbSpacePressed();
            }
            SyncMenuKeyStateFromKeyboard();
        }

        public override void Update()
		{

            GlobalTimer.Update(this.GetTime());

            oneKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_1);
            if (oneKeyCurr == true && oneKeyPrev == false)
            {
                pSceneContext.SetState(SceneContext.Scene.Select);
                SyncMenuKeyStateFromKeyboard();
            }
            oneKeyPrev = oneKeyCurr;


            twoKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_2);
            if (twoKeyCurr == true && twoKeyPrev == false)
            {
                TryGoToPlay(false);
            }
            twoKeyPrev = twoKeyCurr;

            threeKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_3);
            if (threeKeyCurr == true && threeKeyPrev == false)
            {
                SceneOver.SetNextEntryAsWin(false);
                pSceneContext.SetState(SceneContext.Scene.Over);
                SyncMenuKeyStateFromKeyboard();
            }
            threeKeyPrev = threeKeyCurr;

            // start play from title / game over: re-check scene so KEY_2 same frame does not run menu keys as if still on menu
            if (!pSceneContext.IsPlayScene())
            {
                spaceMenuCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_SPACE);
                if (spaceMenuCurr == true && spaceMenuPrev == false)
                {
                    TryGoToPlay(true);
                }
                spaceMenuPrev = spaceMenuCurr;

                enterMenuCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_EXECUTE_BUTTON);
                if (enterMenuCurr == true && enterMenuPrev == false)
                {
                    TryGoToPlay(false);
                }
                enterMenuPrev = enterMenuCurr;

                pKeyCurr = Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_P);
                if (pKeyCurr == true && pKeyPrev == false)
                {
                    TryGoToPlay(false);
                }
                pKeyPrev = pKeyCurr;
            }


            // if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_T) == true)
            // {
            //     SpriteBatch pSB = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            //     Debug.Assert(pSB != null);
            //     pSB.SetDrawEnable(false);
            // }

            // if (Azul.Keyboard.KeyPressed(Azul.AZUL_KEY.KEY_Y) == true)
            // {
            //     SpriteBatch pSB = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            //     Debug.Assert(pSB != null);
            //     pSB.SetDrawEnable(true);
            // }

            SpriteBatch pSB = SpriteBatchMan.Find(SpriteBatch.Name.Boxes);
            if (pSB != null)
            {
                pSB.SetDrawEnable(false);
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
