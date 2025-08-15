//-----------------------------------------------------------------------------
// copyright 2025, jessa gillespie, all rights reserved.
//----------------------------------------------------------------------------- 
using System;
using System.Data;
using System.Diagnostics;
using IrrKlang;

namespace SE456
{
    public class ScenePlay : SceneState
    {
        public ScenePlay()
        {
            this.Initialize();
        }

        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            this.poFontMan = new FontMan(3, 1);
            FontMan.SetActive(this.poFontMan);

            //------------------------------------------------------
            // Sound Set-up
            //------------------------------------------------------
            {
                // start up the engine
                sndEngine = new IrrKlang.ISoundEngine();

                // Resident loads
                sndVader0 = sndEngine.AddSoundSourceFromFile("fastinvader1.wav");
                sndVader1 = sndEngine.AddSoundSourceFromFile("fastinvader2.wav");
                sndVader2 = sndEngine.AddSoundSourceFromFile("fastinvader3.wav");
                sndVader3 = sndEngine.AddSoundSourceFromFile("fastinvader4.wav");

                //other sounds
                explosion = sndEngine.AddSoundSourceFromFile("explosion.wav");
                invaderKilled = sndEngine.AddSoundSourceFromFile("invaderkilled.wav");
                shoot = sndEngine.AddSoundSourceFromFile("shoot.wav");
                ufoHighPitch = sndEngine.AddSoundSourceFromFile("ufo_highpitch.wav");
                ufoLowPitch = sndEngine.AddSoundSourceFromFile("ufo_lowpitch.wav");
            }


            //------------------------------------------------------
            // Create Images
            //------------------------------------------------------
            {
                ImageMan.Add(Image.Name.Crab, Texture.Name.Invaders, 33, 3, 11, 8);
                ImageMan.Add(Image.Name.Crab1, Texture.Name.Invaders, 47, 3, 11, 8);
                ImageMan.Add(Image.Name.Octopus, Texture.Name.Invaders, 3, 3, 12, 8);
                ImageMan.Add(Image.Name.Octopus1, Texture.Name.Invaders, 18, 3, 12, 8);
                ImageMan.Add(Image.Name.Squid, Texture.Name.Invaders, 61, 3, 8, 8);
                ImageMan.Add(Image.Name.Squid1, Texture.Name.Invaders, 72, 3, 8, 8);

                ImageMan.Add(Image.Name.Missile, Texture.Name.Invaders, 3, 29, 1, 4);
                ImageMan.Add(Image.Name.Ship, Texture.Name.Invaders, 3, 14, 13, 8);
                ImageMan.Add(Image.Name.Wall, Texture.Name.Invaders, 40, 185, 20, 10);
                ImageMan.Add(Image.Name.UFO, Texture.Name.Invaders, 99, 4, 16, 7);

                ImageMan.Add(Image.Name.BombStraight, Texture.Name.Invaders, 66, 26, 1, 7);
                ImageMan.Add(Image.Name.BombZigZag, Texture.Name.Invaders, 18, 26, 3, 7);
                ImageMan.Add(Image.Name.BombCross, Texture.Name.Invaders, 48, 27, 3, 6);

                ImageMan.Add(Image.Name.Brick, Texture.Name.Invaders, 114, 35, 1, 1);

                ImageMan.Add(Image.Name.ExplosionInvader, Texture.Name.Invaders, 83, 3, 13, 8);
                ImageMan.Add(Image.Name.ExplosionShip, Texture.Name.Invaders, 38, 14, 16, 8);
                ImageMan.Add(Image.Name.ExplosionShip2, Texture.Name.Invaders, 20, 14, 15, 8);
                ImageMan.Add(Image.Name.ExplosionUFO, Texture.Name.Invaders, 116, 4, 16, 7);
                ImageMan.Add(Image.Name.ExplosionRoof, Texture.Name.Invaders, 7, 25, 8, 8);
                ImageMan.Add(Image.Name.ExplosionBomb, Texture.Name.Invaders, 86, 25, 6, 8);
            }

            //------------------------------------------------------    
            // Create Sprites
            //------------------------------------------------------
            {
                // --- BoxSprites ---

                SpriteBoxMan.Add(SpriteBox.Name.Box1, 550.0f, 500.0f, 50.0f, 150.0f, new Azul.Color(1.0f, 1.0f, 1.0f, 1.0f));
                SpriteBoxMan.Add(SpriteBox.Name.Box2, 550.0f, 100.0f, 50.0f, 100.0f);

                // --- Game Sprites ---
                SpriteGameMan.Add(SpriteGame.Name.Octopus, Image.Name.Octopus, 86.0f, 400.0f, 36.0f, 25.0f);
                SpriteGameMan.Add(SpriteGame.Name.Crab, Image.Name.Crab, 100.0f, 100.0f, 28.0f, 25.0f);
                SpriteGameMan.Add(SpriteGame.Name.Squid, Image.Name.Squid, 100.0f, 100.0f, 24.0f, 25.0f);

                SpriteGameMan.Add(SpriteGame.Name.Missile, Image.Name.Missile, 0, 0, 6, 24);
                SpriteGameMan.Add(SpriteGame.Name.Ship, Image.Name.Ship, 336, 100, 39, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.Wall, Image.Name.Wall, 400, 900, 700, 30);

                SpriteGameMan.Add(SpriteGame.Name.UFO, Image.Name.UFO, 236.0f, 300.0f, 48, 21, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));

                SpriteGameMan.Add(SpriteGame.Name.ReserveShip1, Image.Name.Ship, 88, 33, 39, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.ReserveShip2, Image.Name.Ship, 133, 33, 39, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.ReserveShip3, Image.Name.Ship, 178, 33, 39, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

                SpriteGameMan.Add(SpriteGame.Name.Brick, Image.Name.Brick, 1, 1, 6, 6, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.BombStraight, Image.Name.BombStraight, 0, 0, 4, 16, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.BombZigZag, Image.Name.BombZigZag, 0, 0, 6, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.BombCross, Image.Name.BombCross, 0, 0, 6, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

                SpriteGameMan.Add(SpriteGame.Name.ExplosionInvader, Image.Name.ExplosionInvader, 0, 0, 36, 25);
                SpriteGameMan.Add(SpriteGame.Name.ExplosionShip, Image.Name.ExplosionShip, 0, 0, 39, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.ExplosionShip2, Image.Name.ExplosionShip2, 0, 0, 39, 24, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.ExplosionUFO, Image.Name.ExplosionUFO, 0, 0, 48, 21, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.ExplosionRoof, Image.Name.ExplosionRoof, 0, 0, 24, 24, new Azul.Color(1.0f, 0.0f, 0.0f, 1.0f));
                SpriteGameMan.Add(SpriteGame.Name.ExplosionBomb, Image.Name.ExplosionBomb, 0, 0, 24, 24);
            }

            //------------------------------------------------------
            // Input
            //------------------------------------------------------
            {
                InputSubject pInputSubject;
                pInputSubject = InputMan.GetArrowRightSubject();
                pInputSubject.Attach(new MoveRightObserver());

                pInputSubject = InputMan.GetArrowLeftSubject();
                pInputSubject.Attach(new MoveLeftObserver());

                pInputSubject = InputMan.GetSpaceSubject();
                pInputSubject.Attach(new ShootObserver(sndEngine, shoot));

                Simulation.SetState(Simulation.State.Realtime);

            }
        }

        private void LoadOnEntry()
        {
            // play a sound file
            sndEngine.SoundVolume = 0.0f;
            sndEngine.Play2D(sndVader0, false, false, false);

            SpriteBatch pSB_Text;
            if (SpriteBatchMan.Find(SpriteBatch.Name.Texts) == null)
            {
                pSB_Text = SpriteBatchMan.Add(SpriteBatch.Name.Texts, 200);
            }
            else
            {
                pSB_Text = SpriteBatchMan.Find(SpriteBatch.Name.Texts);
            }

            SpriteBatch pSB_Invaders = SpriteBatchMan.Add(SpriteBatch.Name.Invaders, 100);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Add(SpriteBatch.Name.Boxes, 100);
            SpriteBatch pSB_Shields = SpriteBatchMan.Add(SpriteBatch.Name.Shields, 100);
            SpriteBatch reserveShips = SpriteBatchMan.Add(SpriteBatch.Name.ReserveShips, 100);

            //------------------------------------------------------
            // Create Fonts
            //------------------------------------------------------
            {
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

                    pFont = FontMan.Add(Font.Name.ActualHiScore, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 302, 700);
                    pFont.poSpriteFont.sx = 3.0f;
                    pFont.poSpriteFont.sy = 3.0f;

                    pFont = FontMan.Add(Font.Name.Actual2Score, SpriteBatch.Name.Texts, "0 0 0 0", Glyph.Name.SpaceInvaders, 557, 700);
                    pFont.poSpriteFont.sx = 3.0f;
                    pFont.poSpriteFont.sy = 3.0f;

                    //BOTTOM
                    pFont = FontMan.Add(Font.Name.Reserve, SpriteBatch.Name.Texts, "3", Glyph.Name.SpaceInvaders, 25, 30);
                    pFont.poSpriteFont.sx = 3.0f;
                    pFont.poSpriteFont.sy = 3.0f;
                    pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);

                    pFont = FontMan.Add(Font.Name.Credits, SpriteBatch.Name.Texts, "C R E D I T S", Glyph.Name.SpaceInvaders, 475, 30);
                    pFont.poSpriteFont.sx = 3.0f;
                    pFont.poSpriteFont.sy = 3.0f;
                    pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);

                    pFont = FontMan.Add(Font.Name.Player, SpriteBatch.Name.Texts, "0 0", Glyph.Name.SpaceInvaders, 608, 30);
                    pFont.poSpriteFont.sx = 3.0f;
                    pFont.poSpriteFont.sy = 3.0f;
                    pFont.poSpriteFont.SetColor(0.0f, 1.0f, 0.0f);

                }
            }

            //------------------------------------------------------
            // Create Invaders
            //------------------------------------------------------
            InvaderFactory IF = new InvaderFactory(SpriteBatch.Name.Invaders, SpriteBatch.Name.Boxes);
            InvaderGrid pGrid;
            if (GameObjectNodeMan.Find(GameObject.Name.InvaderGrid) == null)
            {
                pGrid = (InvaderGrid)IF.Create(GameObject.Name.InvaderGrid);
                GameObjectNodeMan.Attach(pGrid);

                for (int i = 0; i < 11; i++)
                {
                    float st = 86.0f;

                    InvaderColumn pColumn = (InvaderColumn)IF.Create(GameObject.Name.InvaderColumn);
                    pGrid.Add(pColumn);

                    pColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 400.0f));
                    pColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 450.0f));
                    pColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 500.0f));
                    pColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 550.0f));
                    pColumn.Add(IF.Create(GameObject.Name.Squid, st + i * 50.0f, 600.0f));
                }
            }
            else
            {
                pGrid = (InvaderGrid)GameObjectNodeMan.Find(GameObject.Name.InvaderGrid);
            }
            //InvaderGrid pGrid = (InvaderGrid)IF.Create(GameObject.Name.InvaderGrid);
            //GameObjectNodeMan.Attach(pGrid);

            //for (int i = 0; i < 11; i++)
            //{
            //    float st = 86.0f;

            //    InvaderColumn pColumn = (InvaderColumn)IF.Create(GameObject.Name.InvaderColumn);
            //    pGrid.Add(pColumn);

            //    pColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 400.0f));
            //    pColumn.Add(IF.Create(GameObject.Name.Octopus, st + i * 50.0f, 450.0f));
            //    pColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 500.0f));
            //    pColumn.Add(IF.Create(GameObject.Name.Crab, st + i * 50.0f, 550.0f));
            //    pColumn.Add(IF.Create(GameObject.Name.Squid, st + i * 50.0f, 600.0f));
            //}

            //------------------------------------------------------
            // Create Reserve Ships
            //------------------------------------------------------
            {
                reserveShips.Attach(SpriteGameMan.Find(SpriteGame.Name.ReserveShip1));
                reserveShips.Attach(SpriteGameMan.Find(SpriteGame.Name.ReserveShip2));
                reserveShips.Attach(SpriteGameMan.Find(SpriteGame.Name.ReserveShip3));
            }

            //------------------------------------------------------
            // Create Bombs
            //------------------------------------------------------
            BombRoot pBombRoot;
            if (GameObjectNodeMan.Find(GameObject.Name.BombRoot) == null)
            {
                pBombRoot = new BombRoot(GameObject.Name.BombRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);

                GameObjectNodeMan.Attach(pBombRoot);
            }
            else
            {
                pBombRoot = (BombRoot)GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            }
            //BombRoot pBombRoot = new BombRoot(GameObject.Name.BombRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            ////pBombRoot.ActivateCollisionSprite(pSB_Boxes);

            //GameObjectNodeMan.Attach(pBombRoot);


            //------------------------------------------------------
            // Create Missile
            //------------------------------------------------------
            // Missile Root
            MissileGroup pMissileGroup;
            if (GameObjectNodeMan.Find(GameObject.Name.MissileGroup) == null)
            {
                pMissileGroup = new MissileGroup();
                pMissileGroup.ActivateSprite(pSB_Invaders);
                pMissileGroup.ActivateCollisionSprite(pSB_Boxes);

                GameObjectNodeMan.Attach(pMissileGroup);
            }
            else
            {
                pMissileGroup = (MissileGroup)GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            }
            //MissileGroup pMissileGroup = new MissileGroup();
            //pMissileGroup.ActivateSprite(pSB_Invaders);
            //pMissileGroup.ActivateCollisionSprite(pSB_Boxes);

            //GameObjectNodeMan.Attach(pMissileGroup);


            //------------------------------------------------------
            // Walls
            //------------------------------------------------------
            WallGroup pWallGroup;
            if (GameObjectNodeMan.Find(GameObject.Name.WallGroup) == null)
            {
                pWallGroup = new WallGroup(GameObject.Name.WallGroup, SpriteGame.Name.NullObject, 0.0f, 0.0f);
                pWallGroup.ActivateSprite(pSB_Invaders);
                pWallGroup.ActivateCollisionSprite(pSB_Boxes);

                WallTop pWallTop = new WallTop(GameObject.Name.WallTop, SpriteGame.Name.NullObject, 336, 716.5f, 672, 103);
                pWallTop.ActivateCollisionSprite(pSB_Boxes);

                WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, SpriteGame.Name.NullObject, 336, 50, 672, 3);
                pWallBottom.ActivateCollisionSprite(pSB_Invaders);
                //pWallBottom.ActivateSprite(pSB_Invaders);

                WallRight pWallRight = new WallRight(GameObject.Name.WallRight, SpriteGame.Name.NullObject, 660, 350, 24, 580);
                pWallRight.ActivateCollisionSprite(pSB_Boxes);

                WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, SpriteGame.Name.NullObject, 12, 350, 24, 580);
                pWallLeft.ActivateCollisionSprite(pSB_Boxes);

                // Add to the composite the children
                pWallGroup.Add(pWallTop);
                pWallGroup.Add(pWallBottom);
                pWallGroup.Add(pWallRight);
                pWallGroup.Add(pWallLeft);

                GameObjectNodeMan.Attach(pWallGroup);
            }
            else
            {
                pWallGroup = (WallGroup)GameObjectNodeMan.Find(GameObject.Name.WallGroup);
            }

            //WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            //pWallGroup.ActivateSprite(pSB_Invaders);
            //pWallGroup.ActivateCollisionSprite(pSB_Boxes);

            //WallTop pWallTop = new WallTop(GameObject.Name.WallTop, SpriteGame.Name.NullObject, 336, 716.5f, 672, 103);
            //pWallTop.ActivateCollisionSprite(pSB_Boxes);

            //WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, SpriteGame.Name.NullObject, 336, 50, 672, 3);
            //pWallBottom.ActivateCollisionSprite(pSB_Invaders);
            ////pWallBottom.ActivateSprite(pSB_Invaders);

            //WallRight pWallRight = new WallRight(GameObject.Name.WallRight, SpriteGame.Name.NullObject, 660, 350, 24, 580);
            //pWallRight.ActivateCollisionSprite(pSB_Boxes);

            //WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, SpriteGame.Name.NullObject, 12, 350, 24, 580);
            //pWallLeft.ActivateCollisionSprite(pSB_Boxes);

            //// Add to the composite the children
            //pWallGroup.Add(pWallTop);
            //pWallGroup.Add(pWallBottom);
            //pWallGroup.Add(pWallRight);
            //pWallGroup.Add(pWallLeft);

            //GameObjectNodeMan.Attach(pWallGroup);


            //------------------------------------------------------
            // Bumpers
            //------------------------------------------------------
            BumperRoot pBumperRoot;
            if (GameObjectNodeMan.Find(GameObject.Name.BumperRoot) == null)
            {
                pBumperRoot = new BumperRoot(GameObject.Name.BumperRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);

                BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, SpriteGame.Name.NullObject, 647, 100, 50, 100);
                pBumperRight.ActivateCollisionSprite(pSB_Boxes);

                BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, SpriteGame.Name.NullObject, 25, 100, 50, 100);
                pBumperLeft.ActivateCollisionSprite(pSB_Boxes);

                // Add to the composite the children
                pBumperRoot.Add(pBumperRight);
                pBumperRoot.Add(pBumperLeft);

                GameObjectNodeMan.Attach(pBumperRoot);
            }
            else
            {
                pBumperRoot = (BumperRoot)GameObjectNodeMan.Find(GameObject.Name.BumperRoot);
            }
            //BumperRoot pBumperRoot = new BumperRoot(GameObject.Name.BumperRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            //pWallGroup.ActivateSprite(pSB_Boxes);

            //BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, SpriteGame.Name.NullObject, 647, 100, 50, 100);
            //pBumperRight.ActivateCollisionSprite(pSB_Boxes);

            //BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, SpriteGame.Name.NullObject, 25, 100, 50, 100);
            //pBumperLeft.ActivateCollisionSprite(pSB_Boxes);

            //// Add to the composite the children
            //pBumperRoot.Add(pBumperRight);
            //pBumperRoot.Add(pBumperLeft);

            //GameObjectNodeMan.Attach(pBumperRoot);


            //------------------------------------------------------
            // Create Ships
            //------------------------------------------------------
            ShipRoot pShipRoot = new ShipRoot(GameObject.Name.ShipRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
            GameObjectNodeMan.Attach(pShipRoot);

            if (ShipMan.getInstance() == null)
            {
                ShipMan.Create();
            }
            else
            {
                ShipMan.Reset();
            }


            //------------------------------------------------------
            // Create Shields
            //------------------------------------------------------
            //GameObject pShieldRoot = ShieldFactory.CreateShields(75.0f, 150.0f);
            GameObject pShieldRoot;
            if (GameObjectNodeMan.Find(GameObject.Name.ShieldRoot) == null)
            {
                pShieldRoot = ShieldFactory.CreateShields(75.0f, 150.0f);
                GameObjectNodeMan.Attach(pShieldRoot);
            }
            else
            {
                pShieldRoot = GameObjectNodeMan.Find(GameObject.Name.ShieldRoot);

            }

            //------------------------------------------------------
            // Create Animations/commands
            //------------------------------------------------------
            {
                pDrum = new Drum((InvaderGrid)pGrid, 0.7f, 0.05f, 4.0f, 16.0f);
                pDrum.Update();

                // Create an animation sprite
                AnimationCmd pAnimCmd = new AnimationCmd(SpriteGame.Name.Squid, pDrum);
                AnimationCmd pAnimCmd2 = new AnimationCmd(SpriteGame.Name.Crab, pDrum);
                AnimationCmd pAnimCmd3 = new AnimationCmd(SpriteGame.Name.Octopus, pDrum);

                // attach several images to cycle
                pAnimCmd.Attach(Image.Name.Squid);
                pAnimCmd.Attach(Image.Name.Squid1);
                pAnimCmd2.Attach(Image.Name.Crab);
                pAnimCmd2.Attach(Image.Name.Crab1);
                pAnimCmd3.Attach(Image.Name.Octopus);
                pAnimCmd3.Attach(Image.Name.Octopus1);

                // add AnimationSprite to timer
                TimerEventMan.Add(TimerEvent.Name.Animation, pAnimCmd, pDrum.GetBeat());
                TimerEventMan.Add(TimerEvent.Name.Animation, pAnimCmd3, pDrum.GetBeat());
                TimerEventMan.Add(TimerEvent.Name.Animation, pAnimCmd2, pDrum.GetBeat());
            
            }

            //------------------------------------------------------
            // Grid commands
            //------------------------------------------------------
            {
                GridCmd pGridCmd = new GridCmd(pGrid, pDrum);
                TimerEventMan.Add(TimerEvent.Name.Grid, pGridCmd, pDrum.GetBeat());

                SndCmd pSndCmd = new SndCmd(sndEngine, sndVader0, sndVader1, sndVader2, sndVader3, pDrum);
                TimerEventMan.Add(TimerEvent.Name.Snd, pSndCmd, pDrum.GetBeat());
            }

            //------------------------------------------------------
            // UFO commands
            //------------------------------------------------------
            GameObject pUFORoot;
            if (GameObjectNodeMan.Find(GameObject.Name.UFORoot) == null)
            {
                pUFORoot = new UFORoot(GameObject.Name.UFORoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
                GameObjectNodeMan.Attach(pUFORoot);
            }
            else
            {
                pUFORoot = GameObjectNodeMan.Find(GameObject.Name.UFORoot);
            }
            
            {
                float time = (float)pRandom.Next(100, 10000) / 1000.0f + 5.0f;
                TimerEventMan.Add(TimerEvent.Name.UFOSpawn, new UFOSpawnEvent(pRandom, sndEngine, ufoLowPitch, ufoHighPitch), time);
            }

            //------------------------------------------------------
            // Bomb commands
            //------------------------------------------------------
            {
                for (int i = 0; i < 10; i++)
                {
                    float time = (float)pRandom.Next(100, 10000) / 1000.0f + 2.0f;
                    Debug.WriteLine("set--->time: {0} ", time);
                    TimerEventMan.Add(TimerEvent.Name.BombSpawn, new BombSpawnEvent(pRandom), time);
                }
            }


            //------------------------------------------------------
            // Create Explosion
            //------------------------------------------------------
            {
                // Create the explosion animation
                //ExplosionAnimCmd pExplosionAnimCmd = new ExplosionAnimCmd(SpriteGame.Name.Explosion, 0.5f);
                //TimerEventMan.Add(TimerEvent.Name.Explosion, pExplosionAnimCmd, 0.5f);

                GameObject pExplosionRoot;
                if (GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot) == null)
                {
                    pExplosionRoot = new ExplosionRoot(GameObject.Name.ExplosionRoot, SpriteGame.Name.NullObject, 0.0f, 0.0f);
                    GameObjectNodeMan.Attach(pExplosionRoot);
                }
                else
                {
                    pExplosionRoot = GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
                }
            }

            //------------------------------------------------------
            // Create Collision Pairs
            //------------------------------------------------------
            {
                // associate in a collision pair
                ColPair pColPair = ColPairMan.Add(ColPair.Name.Alien_Wall, pGrid, pWallGroup);
                pColPair.Attach(new GridObserver());

                // Missile Wall a collision pair
                pColPair = ColPairMan.Add(ColPair.Name.Missile_Wall, pMissileGroup, pWallGroup);
                //roof observer here
                //ExplosionRoot explosionRoot = (ExplosionRoot)GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
                //Explosion explosion = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.ExplosionRoof, this.x, this.y);
                //explosion.ActivateSprite(SpriteBatchMan.Find(SpriteBatch.Name.Invaders));
                //explosion.ActivateCollisionSprite(SpriteBatchMan.Find(SpriteBatch.Name.Boxes));
                //explosionRoot.Add(explosion);
                //TimerEventMan.Add(TimerEvent.Name.Explosion, new ExplosionAnimCmd(explosion), 0.1f);

                pColPair.Attach(new RemoveMissileObserver());
                pColPair.Attach(new ShipReadyObserver());

                //Missile vs Shield
                pColPair = ColPairMan.Add(ColPair.Name.Misslie_Shield, pMissileGroup, pShieldRoot);
                pColPair.Attach(new RemoveMissileObserver());
                pColPair.Attach(new RemoveBrickObserver());
                pColPair.Attach(new ShipReadyObserver());
                pColPair.Attach(new SndObserver(sndEngine, explosion));

                // Missile vs Alien
                pColPair = ColPairMan.Add(ColPair.Name.Alien_Missile, pMissileGroup, pGrid);
                pColPair.Attach(new RemoveMissileObserver());
                pColPair.Attach(new RemoveInvaderObserver());
                pColPair.Attach(new ShipReadyObserver());
                pColPair.Attach(new SndObserver(sndEngine, invaderKilled));

                //Bomb vs Shield
                pColPair = ColPairMan.Add(ColPair.Name.Bomb_Shield, pBombRoot, pShieldRoot);
                pColPair.Attach(new RemoveBombObserver());
                pColPair.Attach(new RemoveBrickObserver());
                pColPair.Attach(new SndObserver(sndEngine, explosion));

                // Bomb vs Bottom
                pColPair = ColPairMan.Add(ColPair.Name.Bomb_Wall, pBombRoot, pWallGroup);
                pColPair.Attach(new RemoveBombObserver());

                // Bomb vs Missile
                pColPair = ColPairMan.Add(ColPair.Name.Bomb_Missile, pBombRoot, pMissileGroup);
                pColPair.Attach(new RemoveBombObserver());
                pColPair.Attach(new RemoveMissileObserver());
                pColPair.Attach(new ShipReadyObserver());
                pColPair.Attach(new SndObserver(sndEngine, explosion));

                // Bomb vs Ship
                pColPair = ColPairMan.Add(ColPair.Name.Bomb_Ship, pBombRoot, pShipRoot);
                pColPair.Attach(new RemoveBombObserver());
                pColPair.Attach(new ShipRemoveObserver());
                pColPair.Attach(new ShipReadyObserver());
                pColPair.Attach(new SndObserver(sndEngine, explosion)); // explode??

                // Bumper vs Ship
                pColPair = ColPairMan.Add(ColPair.Name.Bumper_Ship, pBumperRoot, pShipRoot);
                pColPair.Attach(new ShipMoveObserver());

                // Missile vs UFO
                pColPair = ColPairMan.Add(ColPair.Name.Missile_UFO, pMissileGroup, pUFORoot);
                pColPair.Attach(new RemoveMissileObserver());
                pColPair.Attach(new RemoveUFOObserver());
                pColPair.Attach(new ShipReadyObserver());
                pColPair.Attach(new SndObserver(sndEngine, invaderKilled));

                //Alien vs Ship
                pColPair = ColPairMan.Add(ColPair.Name.Alien_Ship, pGrid, pShipRoot);
                pColPair.Attach(new ShipRemoveObserver());
                pColPair.Attach(new ShipReadyObserver());
                pColPair.Attach(new SndObserver(sndEngine, explosion));

            }
        }



        public override void Update(float systemTime)
        {
            if (ShipRemoveObserver.count == 0)
            {
                // Switch to Game Over Scene
                SceneContext pSceneContext = SceneContext.GetInstance();
                pSceneContext.SetState(SceneContext.Scene.Over);
            }
            // Single Step, Free running...
            Simulation.Update(systemTime);

            // Input
            InputMan.Update();

            //add toggles for speed, motion, and sound

            // Run based on simulation stepping
            if (Simulation.GetTimeStep() > 0.0f)
            {
                // Fire off the timer events
                TimerEventMan.Update(Simulation.GetTotalTime());

                // Update the grid
                pDrum.Update();

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
            // update SpriteBatchMan()
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
            // Need a better way to do this
            this.TimeAtPause = GlobalTimer.GetTime();

            ScoreManager.ResetScore();

            //BombRoot bombRoot = (BombRoot)GameObjectNodeMan.Find(GameObject.Name.BombRoot);
            //bombRoot.RemoveActiveBombs();
            //MissileGroup missileGroup = (MissileGroup)GameObjectNodeMan.Find(GameObject.Name.MissileGroup);
            //missileGroup.RemoveActiveMissiles();

            //ExplosionRoot explosionRoot = (ExplosionRoot)GameObjectNodeMan.Find(GameObject.Name.ExplosionRoot);
            //explosionRoot.Remove();

            TimerEventMan.ClearAll();

            sndEngine.StopAllSounds();

        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        public FontMan poFontMan;
        public Drum pDrum;

        IrrKlang.ISoundEngine sndEngine = null;
        IrrKlang.ISound music = null;
        IrrKlang.ISoundSource sndVader0 = null;
        IrrKlang.ISoundSource sndVader1 = null;
        IrrKlang.ISoundSource sndVader2 = null;
        IrrKlang.ISoundSource sndVader3 = null;
        IrrKlang.ISoundSource explosion = null;
        IrrKlang.ISoundSource invaderKilled = null;
        IrrKlang.ISoundSource shoot = null;
        IrrKlang.ISoundSource ufoHighPitch = null;
        IrrKlang.ISoundSource ufoLowPitch = null;

        readonly Random pRandom = new Random();

        bool lastKeyE = false;
        int count;
        int animCount;

    }
}

// --- End of File ---
