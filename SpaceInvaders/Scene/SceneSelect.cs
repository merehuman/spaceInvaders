//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System.Diagnostics;

namespace SE456
{
    public class SceneSelect : SceneState
    {
        public SceneSelect()
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

            TextureMan.Add(Texture.Name.Consolas36pt, "consolas36pt.t.azul");
            GlyphMan.AddXml("Consolas36pt.xml", Glyph.Name.Consolas36pt, Texture.Name.Consolas36pt);

            //------------------------------------------------------
            // Load the Textures
            //------------------------------------------------------
            {
                TextureMan.Add(Texture.Name.Invaders, "SpaceInvaders_ROM.t.azul");
            }

            //------------------------------------------------------
            // Glyphs
            //------------------------------------------------------
            {
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 65, Texture.Name.Invaders, 3, 36, 5, 8); // .A
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 66, Texture.Name.Invaders, 11, 36, 5, 8); // .B
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 67, Texture.Name.Invaders, 19, 36, 5, 8); // .C
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 68, Texture.Name.Invaders, 27, 36, 5, 8); // .D
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 69, Texture.Name.Invaders, 35, 36, 5, 8); // .E
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 70, Texture.Name.Invaders, 43, 36, 5, 8); // .F
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 71, Texture.Name.Invaders, 51, 36, 5, 8); // .G
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 72, Texture.Name.Invaders, 59, 36, 5, 8); // .H
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 73, Texture.Name.Invaders, 67, 36, 5, 8); // .I
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 74, Texture.Name.Invaders, 75, 36, 5, 8); // .J
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 75, Texture.Name.Invaders, 83, 36, 5, 8); // .K
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 76, Texture.Name.Invaders, 91, 36, 5, 8); // .L
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 77, Texture.Name.Invaders, 99, 36, 5, 8); // .M
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 78, Texture.Name.Invaders, 3, 46, 5, 8); // .N
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 79, Texture.Name.Invaders, 11, 46, 5, 8); // .O
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 80, Texture.Name.Invaders, 19, 46, 5, 8); // .P
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 81, Texture.Name.Invaders, 27, 46, 5, 8); // .Q
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 82, Texture.Name.Invaders, 35, 46, 5, 8); // .R
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 83, Texture.Name.Invaders, 43, 46, 5, 8); // .S
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 84, Texture.Name.Invaders, 51, 46, 5, 8); // .T
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 85, Texture.Name.Invaders, 59, 46, 5, 8); // .U
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 86, Texture.Name.Invaders, 67, 46, 5, 8); // .V
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 87, Texture.Name.Invaders, 75, 46, 5, 8); // .W
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 88, Texture.Name.Invaders, 83, 46, 5, 8); // .X
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 89, Texture.Name.Invaders, 91, 46, 5, 8); // .Y
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 90, Texture.Name.Invaders, 99, 46, 5, 8); // .Z
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 48, Texture.Name.Invaders, 3, 56, 5, 8); // 0
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 49, Texture.Name.Invaders, 11, 56, 5, 8); // 1
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 50, Texture.Name.Invaders, 19, 56, 5, 8); // 2
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 51, Texture.Name.Invaders, 27, 56, 5, 8); // 3
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 52, Texture.Name.Invaders, 35, 56, 5, 8); // 4
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 53, Texture.Name.Invaders, 43, 56, 5, 8); // 5
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 54, Texture.Name.Invaders, 51, 56, 5, 8); // 6
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 55, Texture.Name.Invaders, 59, 56, 5, 8); // 7
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 56, Texture.Name.Invaders, 67, 56, 5, 8); // 8
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 57, Texture.Name.Invaders, 75, 56, 5, 8); // 9
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 60, Texture.Name.Invaders, 83, 56, 5, 8); // <
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 62, Texture.Name.Invaders, 91, 56, 5, 8); // >
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 32, Texture.Name.Invaders, 99, 56, 1, 8); // Space
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 61, Texture.Name.Invaders, 107, 56, 5, 8); // =
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 42, Texture.Name.Invaders, 115, 56, 5, 8); // *
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 63, Texture.Name.Invaders, 123, 56, 5, 8); // ?
                GlyphMan.Add(Glyph.Name.SpaceInvaders, 45, Texture.Name.Invaders, 131, 56, 5, 8); // -
            }

            //------------------------------------------------------
            // Create Images
            //------------------------------------------------------
            ImageMan.Add(Image.Name.UFO, Texture.Name.Invaders, 99, 4, 16, 7);
            ImageMan.Add(Image.Name.Squid, Texture.Name.Invaders, 61, 3, 8, 8);
            ImageMan.Add(Image.Name.Crab, Texture.Name.Invaders, 33, 3, 11, 8);
            ImageMan.Add(Image.Name.Octopus, Texture.Name.Invaders, 3, 3, 12, 8);

            //-------------------------------------------------------
            // Create Sprites
            //-------------------------------------------------------
            SpriteGameMan.Add(SpriteGame.Name.UFO1, Image.Name.UFO, 236.0f, 300.0f, 48, 21);
            SpriteGameMan.Add(SpriteGame.Name.Squid1, Image.Name.Squid, 236.0f, 250.0f, 36.0f, 25.0f);
            SpriteGameMan.Add(SpriteGame.Name.Crab1, Image.Name.Crab, 236.0f, 200.0f, 28.0f, 25.0f);
            SpriteGameMan.Add(SpriteGame.Name.Octopus1, Image.Name.Octopus, 236.0f, 150.0f, 24.0f, 25.0f, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            SpriteBatch pSB_Aliens = SpriteBatchMan.Add(SpriteBatch.Name.Aliens, 100);

            //------------------------------------------------------
            // Sounds
            //------------------------------------------------------
            // start up the engine
            sndEngine = new IrrKlang.ISoundEngine();
            theme = sndEngine.AddSoundSourceFromFile("theme.wav");
           
        }

        private void LoadOnEntry()
        {
            // play a sound file
            sndEngine.SoundVolume = 0.2f;
            //sndEngine.Play2D(theme, false, false, false);

            //------------------------------------------------------
            // Create Fonts
            //------------------------------------------------------
            {
                //TOP
                Font pFont;
                pFont = FontMan.Add(Font.Name.Score1, SpriteBatch.Name.Texts, "S C O R E < 1 >", Glyph.Name.SpaceInvaders, 26, 740);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;

                pFont = FontMan.Add(Font.Name.HiScore, SpriteBatch.Name.Texts, "H I - S C O R E", Glyph.Name.SpaceInvaders, 265, 740);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;

                pFont = FontMan.Add(Font.Name.Score2, SpriteBatch.Name.Texts, "S C O R E < 2 >", Glyph.Name.SpaceInvaders, 503, 740);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;

                pFont = FontMan.Add(Font.Name.ActualScore, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 52, 700);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;

                pFont = FontMan.Find(Font.Name.ActualHiScore);
                if (pFont == null)
                {
                    pFont = FontMan.Add(Font.Name.ActualHiScore, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 302, 700);
                    pFont.poSpriteFont.sx = 3.0f;
                    pFont.poSpriteFont.sy = 3.0f;
                }

                //pFont = FontMan.Add(Font.Name.ActualHiScore, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 302, 700);
                //pFont.poSpriteFont.sx = 3.0f;
                //pFont.poSpriteFont.sy = 3.0f;

                pFont = FontMan.Add(Font.Name.Actual2Score, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 557, 700);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;

                //BOTTOM
                pFont = FontMan.Add(Font.Name.Credits, SpriteBatch.Name.Texts, "C R E D I T S", Glyph.Name.SpaceInvaders, 475, 30);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;
                pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);

                pFont = FontMan.Add(Font.Name.Player, SpriteBatch.Name.Texts, "0 0", Glyph.Name.SpaceInvaders, 608, 30);
                pFont.poSpriteFont.sx = 3.0f;
                pFont.poSpriteFont.sy = 3.0f;
                pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);

            }

            TimedCharacterFactory.Install("P  L  A  Y", 3.0f, 0.30f, 306, 550, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("S  P  A  C  E         I  N  V  A  D  E  R  S", 5.0f, 0.10f, 193, 475, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("* S  C  O  R  E    A  D  V  A  N  C  E    T  A  B  L  E *", 6.0f, 0.10f, 140, 350, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("=  ?   M Y S T E R Y", 8.0f, 0.10f, 265, 300, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("=  3 0   P O I N T S", 10.0f, 0.10f, 265, 250, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("=  2 0   P O I N T S", 13.0f, 0.10f, 265, 200, 0.9f, 0.9f, 0.9f);
            TimedCharacterFactory.Install("=  1 0   P O I N T S", 16.0f, 0.10f, 265, 150, 0.2f, 0.8f, 0.2f);

            TimedSpriteCmd pTimedCmd = new TimedSpriteCmd(SpriteGame.Name.UFO1);
            TimerEventMan.Add(TimerEvent.Name.TimedSprite, pTimedCmd, 6.5f);

            TimedSpriteCmd pTimedCmd2 = new TimedSpriteCmd(SpriteGame.Name.Squid1);
            TimerEventMan.Add(TimerEvent.Name.TimedSprite, pTimedCmd2, 9.5f);

            TimedSpriteCmd pTimedCmd3 = new TimedSpriteCmd(SpriteGame.Name.Crab1);
            TimerEventMan.Add(TimerEvent.Name.TimedSprite, pTimedCmd3, 12.5f);

            TimedSpriteCmd pTimedCmd4 = new TimedSpriteCmd(SpriteGame.Name.Octopus1);
            TimerEventMan.Add(TimerEvent.Name.TimedSprite, pTimedCmd4, 15.5f);
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
                // Fire off the timer events
                TimerEventMan.Update(Simulation.GetTotalTime());

                ScoreManager.UpdateScoreDisplay();

                // Update the sound
                sndEngine.Update();

                // Do the collision checks
                ColPairMan.Process();

                // walk through all objects and push to flyweight
                GameObjectNodeMan.Update();

                // Delete any objects here...
                DelayedObjectMan.Process();
            }
        }

        public override void Draw()
        {
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
            FontMan.SetActive(this.poFontMan);

            ScoreManager.UpdateScoreDisplay();

            // Update timer since last pause
            float t0 = GlobalTimer.GetTime();
            float t1 = this.TimeAtPause;
            float delta = t0 - t1;
            TimerEventMan.PauseUpdate(delta);

            TimerEventMan.ClearAll();

            this.LoadOnEntry();
        }
        public override void Leaving()
        {
            //this.TimeAtPause = TimerEventMan.GetCurrTime();
            this.TimeAtPause = 0.0f;

            //FontMan.Remove(FontMan.Find(Font.Name.TimedCharacter));

            //TimerEventMan.ClearAll();

            sndEngine.StopAllSounds();
    
           // FontMan.RemoveAll();

           // FontMan.Dump();
          //  TimerEventMan.Dump();
            
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public FontMan poFontMan;

        IrrKlang.ISoundEngine sndEngine = null;
        IrrKlang.ISound music = null;
        IrrKlang.ISoundSource theme = null;

    }
}

// --- End of File ---
