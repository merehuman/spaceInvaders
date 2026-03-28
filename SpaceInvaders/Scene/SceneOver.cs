//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SE456
{
    public class SceneOver : SceneState
    {
        private static bool pendingWinMode = false;
        private bool sessionIsWin;

        public static void SetNextEntryAsWin(bool win)
        {
            pendingWinMode = win;
        }

        public SceneOver()
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poFontMan = new FontMan(3, 1);
            FontMan.SetActive(this.poFontMan);

            SpriteBatch pSB_Texts = SpriteBatchMan.Add(SpriteBatch.Name.Texts);
            Font pFont;


            //pFont = FontMan.Add(Font.Name.GameOver, SpriteBatch.Name.Texts, "G A M E    O V E R", Glyph.Name.SpaceInvaders, 210, 600);
            //pFont.poSpriteFont.sx = 5.0f;
            //pFont.poSpriteFont.sy = 5.0f;
            //pFont.poSpriteFont.SetColor(1.0f, 0.0f, 0.0f);

            pFont = FontMan.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "H I    S C O R E", Glyph.Name.SpaceInvaders, 270, 500);
            pFont.poSpriteFont.sx = 3.0f;
            pFont.poSpriteFont.sy = 3.0f;

            pFont = FontMan.Add(Font.Name.ActualHiScore, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 300, 450);
            pFont.poSpriteFont.sx = 3.0f;
            pFont.poSpriteFont.sy = 3.0f;

            pFont = FontMan.Add(Font.Name.Start, SpriteBatch.Name.Texts, "P R E S S    1    T 0    R E S T A R T", Glyph.Name.SpaceInvaders, 185, 200);
            pFont.poSpriteFont.sx = 3.0f;   
            pFont.poSpriteFont.sy = 3.0f;
            pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);
        }

        private void LoadOnEntry()
        {
            FontMan.RemoveAllTimedCharacterFonts();

            sessionIsWin = pendingWinMode;
            pendingWinMode = false;

            string title = sessionIsWin ? "Y O U    W I N" : "G A M E    O V E R";
            TimedCharacterFactory.Install(title, 0.0f, 0.30f, 200, 550, 0.9f, 0.9f, 0.9f);
        }
        public override void Update(float systemTime)
        {
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // Fire off the timer events (TimedCharacter title, etc.)
                TimerEventMan.Update(Simulation.GetTotalTime());

                // Do the collision checks
                //ColPairMan.Process();

                // walk through all objects and push to flyweight
                //GameObjectNodeMan.Update();

                // Delete any objects here...
                //DelayedObjectMan.Process();
                // Do not run ColPairMan / GameObjectNodeMan here: KEY_3 or game-over can switch to this
                // scene while the play tree still exists, but this scene's SpriteBatchMan has no Invaders/
                // Boxes batches — missile/bomb timers and object updates can call ActivateSprite(null).
                // Delayed collision observers are flushed in ScenePlay.Leaving() while Play's batches exist.
            }
            Font pFont = FontMan.Find(Font.Name.TimedCharacter);
            if (pFont != null)
            {
                pFont.poSpriteFont.sx = 5.0f;
                pFont.poSpriteFont.sy = 5.0f;
                if (sessionIsWin)
                {
                    pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);
                }
                else
                {
                    pFont.poSpriteFont.SetColor(1.0f, 0.0f, 0.0f);
                }
            }
        }
        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }
        public override void Entering()
        {
            Simulation.SetState(Simulation.State.Realtime);

            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            FontMan.SetActive(this.poFontMan);

            ScoreManager.UpdateScoreDisplay();

            TimerEventMan.ClearAll();

            this.LoadOnEntry();
        }
        public override void Leaving()
        {
            // Any RemoveInvaderObserver left from an incomplete single Process() on Play.Leaving must
            // run before SceneSelect (3→1): SceneSelect.Update Process() uses Select's SpriteBatchMan.
            SceneContext.GetInstance().GetPlayScene().FlushDelayedCollisionObservers();

            // update SpriteBatchMan()
            this.TimeAtPause = TimerEventMan.GetCurrTime();
        }
        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public FontMan poFontMan;
    }
}

// --- End of File ---
