using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Breaking_Bad
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont basic;
        Random rnd;
        Song intro;
        SoundEffect money;
        SoundEffect bossGrunt;
        SoundEffect grunt;
        SoundEffect bomb;
        SoundEffect hector;
        int players = 2;
        int xPos;
        FallingStuff[] crystals;
        FallingStuff[] chicken;
        Player walter;
        Player jesse;
        Player walt2;
        Player jesse2;
        Player saul;
        Player [] sauls;
        Player gus;
        Texture2D gusNormal;
        Texture2D gusDying;
        FallingStuff explosion;
        FallingStuff wmoney;
        FallingStuff jmoney;
        FallingStuff wshot;
        FallingStuff jshot;
        Texture2D healthBar;
        int bossHealth = 100;
        enum GameState { Menu, Info, Level1, Level2, level2screen, Level3, level3screen, GameOver, Win, wWin, jWin};
        GameState currentGameState;
        Texture2D menu;
        Texture2D info;
        Texture2D infoblink;
        Texture2D p1info;
        Texture2D p1infoblink;
        Texture2D p2info;
        Texture2D p2infoblink;
        Texture2D level1;
        Texture2D level3;
        Texture2D level2screen;
        Texture2D level3screen;
        Texture2D wWin;
        Texture2D jWin;
        Background lvl2p1;
        Background lvl2p2;
        Background lvl2p3;
        Texture2D gameOver;
        FallingStuff van;
        FallingStuff cactus;
        double time;
        int timer;
        int score1 = 0;
        int score2 = 0;
        int life1 = 5;
        int life2 = 5;
        int killCount= 1;
        int vanish = 0;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 40);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rnd = new Random();
            lvl2p1 = new Background();
            lvl2p2 = new Background();
            lvl2p3 = new Background();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentGameState = GameState.Menu;
            healthBar = Content.Load<Texture2D>(@"Drops\healthbar");
            intro = Content.Load<Song>(@"Sounds\bbintro");
            MediaPlayer.Play(intro);
            MediaPlayer.IsRepeating = true;
            money = Content.Load<SoundEffect>(@"Sounds\male_money");
            bossGrunt = Content.Load<SoundEffect>(@"Sounds\boss_grunt");
            grunt = Content.Load<SoundEffect>(@"Sounds\grunt");
            bomb = Content.Load<SoundEffect>(@"Sounds\bomb");
            hector = Content.Load<SoundEffect>(@"Sounds\Hector_bell");
            basic = Content.Load<SpriteFont>(@"Font\basic");
            explosion = new FallingStuff(Content.Load<Texture2D>(@"Drops\explosion"), graphics);
            explosion.alive = true;
            explosion.visible = false;
            explosion.Scale = 1.5f;
            explosion.position = new Vector2(275, 150);
            menu = Content.Load<Texture2D>(@"Backgrounds\intro2");
            p1info = Content.Load<Texture2D>(@"Backgrounds\p1info");
            p1infoblink = Content.Load<Texture2D>(@"Backgrounds\p1infos");
            p2info = Content.Load<Texture2D>(@"Backgrounds\p2info");
            p2infoblink = Content.Load<Texture2D>(@"Backgrounds\p2infos");
            level1 = Content.Load<Texture2D>(@"Backgrounds\lvl1");
            lvl2p1.LoadContent(this.Content, @"Backgrounds\lvl2");
            lvl2p1.Position = new Vector2(0, 0);
            lvl2p2.LoadContent(this.Content, @"Backgrounds\lvl2");
            lvl2p2.Position = new Vector2(lvl2p1.Position.X + lvl2p1.Size.Width, 0);
            lvl2p3.LoadContent(this.Content, @"Backgrounds\lvl2");
            lvl2p3.Position = new Vector2(lvl2p2.Position.X + lvl2p2.Size.Width, 0);
            level3 = Content.Load<Texture2D>(@"Backgrounds\lvl1");
            level2screen = Content.Load<Texture2D>(@"Backgrounds\level2screen");
            level3screen = Content.Load<Texture2D>(@"Backgrounds\level3screen");
            wWin = Content.Load<Texture2D>(@"Backgrounds\waltscreen");
            jWin = Content.Load<Texture2D>(@"Backgrounds\jessescreen");
            gameOver = Content.Load<Texture2D>(@"Backgrounds\gameover1");
            van = new FallingStuff(Content.Load<Texture2D>(@"Backgrounds\rv"), graphics);
            van.position = new Vector2(0, 200);
            cactus = new FallingStuff(Content.Load<Texture2D>(@"Drops\cactus2"), graphics);
            cactus.position = new Vector2(700, 270);
            cactus.Scale = .3f;
            cactus.speed = 8;
            walter = new Player(Content.Load<Texture2D>(@"Players\walttraysheet"));
            walter.SourceRectangle = new Rectangle(0, 0, 100, 122);
            walter.position = new Vector2(50, 350);
            walter.framesize = new Point(100, 122);
            walter.currentframe = new Point(0, 0);
            walter.sheetsize = new Point(4, 1);
            walter.visible = false;
            jesse = new Player(Content.Load<Texture2D>(@"Players\jessetraysheet"));
            jesse.SourceRectangle = new Rectangle(0, 0, 100, 122);
            jesse.position = new Vector2(500, 350);
            jesse.framesize = new Point(100, 122);
            jesse.currentframe = new Point(0, 0);
            jesse.sheetsize = new Point(4, 1);
            jesse.visible = false;
            walt2 = new Player(Content.Load<Texture2D>(@"Players\waltspritesheet"));
            walt2.SourceRectangle = new Rectangle(0, 0, 100, 122);
            walt2.gposition = new Vector2(620, 350);
            walt2.position = new Vector2(620, 350);
            walt2.framesize = new Point(100, 122);
            walt2.currentframe = new Point(0, 0);
            walt2.sheetsize = new Point(5, 1);
            walt2.visible = false;
            jesse2 = new Player(Content.Load<Texture2D>(@"Players\jessespritesheet"));
            jesse2.SourceRectangle = new Rectangle(0, 0, 100, 122);
            jesse2.gposition = new Vector2(700, 350);
            jesse2.position = new Vector2(700, 350);
            jesse2.framesize = new Point(100, 122);
            jesse2.currentframe = new Point(0, 0);
            jesse2.sheetsize = new Point(5, 1);
            jesse2.visible = false;
            wmoney = new FallingStuff(Content.Load<Texture2D>(@"Drops\money"), graphics);
            wmoney.speed = 4;
            wmoney.alive = true;
            wmoney.visible = false;
            wmoney.Scale = .5f;
            jmoney = new FallingStuff(Content.Load<Texture2D>(@"Drops\money"), graphics);
            jmoney.position = jesse2.position;
            jmoney.speed = 4;
            jmoney.alive = true;
            jmoney.visible = false;
            jmoney.Scale = .5f;
            wshot = new FallingStuff(Content.Load<Texture2D>(@"Drops\bullet1"), graphics);
            wshot.speed = 8;
            wshot.alive = true;
            wshot.visible = false;
            //wshot.Scale = .5f;
            jshot = new FallingStuff(Content.Load<Texture2D>(@"Drops\bullet1"), graphics);
            jshot.speed = 8;
            jshot.alive = true;
            jshot.visible = false;
            saul = new Player(Content.Load<Texture2D>(@"Players\saulspritesheet"));
            saul.SourceRectangle = new Rectangle(0, 0, 100, 122);
            saul.position = new Vector2(50, 350);
            saul.framesize = new Point(100, 122);
            saul.currentframe = new Point(0, 0);
            saul.sheetsize = new Point(4, 1);
            saul.visible = false;
            sauls = new Player[5];
            for (int i = 0; i < sauls.Length; i++)
            {
                sauls[i] = new Player(Content.Load<Texture2D>(@"Players\mikespritesheet"));
                if (i % 2 == 0)
                {
                    sauls[i].direct = 1;
                    sauls[i].position = new Vector2(0, 350);
                }
                else
                {
                    sauls[i].direct = 2;
                    sauls[i].position = new Vector2(700, 350);
                }
                sauls[i].speed = 5;
                sauls[i].SourceRectangle = new Rectangle(0, 0, 100, 122);
                sauls[i].framesize = new Point(100, 122);
                sauls[i].currentframe = new Point(0, 0);
                sauls[i].sheetsize = new Point(4, 1);
                sauls[i].visible = false;
                sauls[i].alive = true;
                sauls[i].spawnTime = i + 1;
            }
            gusNormal = Content.Load<Texture2D>(@"Players\gus");
            gusDying = Content.Load<Texture2D>(@"Players\gushurt");
            gus = new Player(gusNormal);
            gus.SourceRectangle = new Rectangle(0, 0, 100, 122);
            gus.position = new Vector2(402, 1000);
            gus.framesize = new Point(100, 122);
            gus.currentframe = new Point(0, 0);
            gus.sheetsize = new Point(1, 1);
            gus.visible = false;
            gus.center = new Vector2(100/2, 122/2);
            crystals = new FallingStuff[20];
            for (int i = 0; i < crystals.Length; i++)
            {
                xPos = rnd.Next(50, 800);
                crystals[i] = new FallingStuff(Content.Load<Texture2D>(@"Drops\meth"), graphics);
                crystals[i].position = new Vector2(xPos, -10);
                crystals[i].rotation = rnd.Next(0, 100);
                crystals[i].speed = 5;
                crystals[i].Scale = .2f;
                crystals[i].spawnTime = rnd.Next(2, 25);
                crystals[i].alive = true;
                crystals[i].visible = false;
            }
            chicken = new FallingStuff[10];
            for (int i = 0; i < chicken.Length; i++)
            {
                xPos = rnd.Next(50, 700);
                chicken[i] = new FallingStuff(Content.Load<Texture2D>(@"Drops\chicken"), graphics);
                chicken[i].position = new Vector2(xPos, -10);
                chicken[i].rotation = rnd.Next(0, 100);
                chicken[i].speed = rnd.Next(2, 7);
                chicken[i].alive = true;
                chicken[i].spawnTime = rnd.Next(2, 25);
                chicken[i].Scale = .2f;
                chicken[i].visible = false;
            }

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                life1 = 100;
                life2 = 100;
            }

            // TODO: Add your update logic here

            switch (currentGameState)
            {
                case GameState.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.D1))
                    {
                        info = p1info;
                        infoblink = p1infoblink;
                        players = 1;
                        currentGameState = GameState.Info;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    {
                        info = p2info;
                        infoblink = p2infoblink;
                        players = 2;
                        currentGameState = GameState.Info;
                    }
                    break;
                case GameState.Info:
                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    timer = (int)time;

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        time = 0;
                        currentGameState = GameState.Level1;
                    }
                    break;
                case GameState.Level1:
                    playerControls(gameTime);
                    walter.visible = true;
                    if (players == 2)
                        jesse.visible = true;
                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    timer = (int)time;
                    van.position.X++;
                    if (van.position.X >= 750)
                        van.position.X = 0;
                    foreach (FallingStuff gameObject in crystals)
                    {
                        if (timer >= gameObject.spawnTime)
                        {
                            if (gameObject.alive)
                            {
                                gameObject.visible = true;
                                gameObject.update(gameTime);
                                if (gameObject.position.Y > 600)
                                {
                                    gameObject.position.Y = 0;
                                    gameObject.position.X = rnd.Next(25, 800);
                                }
                            }
                        }
                    }
                    foreach (FallingStuff gameObject in chicken)
                    {
                        if (timer >= gameObject.spawnTime)
                        {
                            if (gameObject.alive)
                            {
                                gameObject.visible = true;
                                gameObject.update(gameTime);
                                if (gameObject.position.Y > 600)
                                {
                                    gameObject.position.Y = 0;
                                    gameObject.position.X = rnd.Next(25, 800);
                                }
                            }
                        }
                    }
                    foreach (FallingStuff gameObject in crystals)
                    {
                        if (walter.visible && gameObject.alive)
                        {
                            if (walter.checkCollideWith(gameObject))
                            {
                                if (gameObject.position.Y >= 400)
                                {
                                    gameObject.alive = false;
                                    gameObject.visible = false;
                                    gameObject.speed = 0;
                                    score1 += 20;
                                }
                            }
                        }
                    }
                    foreach (FallingStuff gameObject in crystals)
                    {
                        if (jesse.visible && gameObject.alive)
                        {
                            if (jesse.checkCollideWith(gameObject))
                            {
                                if (gameObject.position.Y >= 400)
                                {
                                    gameObject.alive = false;
                                    gameObject.visible = false;
                                    score2 += 20;
                                }
                            }
                        }
                    }
                    foreach (FallingStuff gameObject in chicken)
                    {
                        if (walter.visible && gameObject.alive)
                        {
                            if (walter.checkCollideWith(gameObject))
                            {
                                if (gameObject.position.Y >= 400)
                                {
                                    gameObject.alive = false;
                                    gameObject.visible = false;
                                    life1--;
                                    score1 -= 10;
                                }
                            }
                        }
                    }
                    foreach (FallingStuff gameObject in chicken)
                    {
                        if (jesse.visible && gameObject.alive)
                        {
                            if (jesse.checkCollideWith(gameObject))
                            {
                                if (gameObject.position.Y >= 400)
                                {
                                    gameObject.alive = false;
                                    gameObject.visible = false;
                                    life2--;
                                    score2 -= 10;
                                }
                            }
                        }
                    }
                    if (life1 <= 0)
                        walter.visible = false;
                    if (life2 <= 0)
                        jesse.visible = false;
                    if (timer >= 35)
                    {
                        time = 0;
                        timer = 0;
                        currentGameState = GameState.level2screen;
                       
                    }
                    if (!walter.visible && !jesse.visible)
                        currentGameState = GameState.GameOver;
                    break;
                case GameState.level2screen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.Level2;
                    }
                    break;
                case GameState.Level2:
                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    timer = (int)time;
                    backgroundMotion(gameTime);
                    playerControls2(gameTime);
                    cactus.position.X--;
                    if (cactus.position.X <= 0)
                        cactus.position.X = 800;
                    if (life1 > 0)
                        walt2.visible = true;
                    if ((players == 2) && (life2 > 0))
                        jesse2.visible = true;
                    saul.visible = true;
                    if (saul.position.X >= 700)
                        saul.position.X = 0;
                    if (saul.checkCollideWith(walt2) && walt2.visible)
                    {
                        saul.backup = true;
                        score1 -= 5;
                        life1--;
                    }
                    if (saul.checkCollideWith(jesse2) && jesse2.visible)
                    {
                        saul.backup = true;
                        score2 -= 5;
                        life2--;
                    }
                    if (!saul.backup)
                    {
                        ++saul.currentframe.X;
                        if (saul.currentframe.X >= saul.sheetsize.X)
                            saul.currentframe.X = 0;
                        saul.position.X += saul.speed;
                    }
                    if (saul.backup)
                    {
                        saul.backup = true;
                        ++saul.currentframe.X;
                        if (saul.currentframe.X >= saul.sheetsize.X)
                            saul.currentframe.X = 0;
                        saul.position.X -= saul.speed;
                    }


                    if ((saul.position.X <= 150) && saul.backup)
                        saul.backup = false;
                    if (wmoney.visible)
                    {
                        wmoney.alive = false;
                        wmoney.position.X -= wmoney.speed;

                    }
                    if (wmoney.checkCollideWith(saul) && wmoney.visible)
                    {
                        saul.backup = true;
                        wmoney.visible = false;
                        wmoney.alive = true;
                        score1 += 10;
                        money.Play();
                    }
                    if (jmoney.visible)
                    {
                        jmoney.alive = false;
                        jmoney.position.X -= jmoney.speed;

                    }
                    if (jmoney.checkCollideWith(saul) && jmoney.visible)
                    {
                        saul.backup = true;
                        jmoney.visible = false;
                        jmoney.alive = true;
                        score2 += 10;
                        money.Play();
                    }
                    if (walt2.jumping)
                        walt2.position.Y -= walt2.velocity.Y;
                    if (walt2.jumping && !walt2.falling)
                    {
                        walt2.falling = true;
                    }
                    if (walt2.falling)
                    {
                        walt2.jumping = false;
                        walt2.position.Y += walt2.gravity.Y;
                    }
                    if (walt2.position.Y >= walt2.gposition.Y)
                        walt2.falling = false;
                    else if(walt2.falling && walt2.checkCollideWith(cactus))
                        walt2.falling = false;
                    if (walt2.checkCollideWith(cactus) && walt2.position.Y < walt2.gposition.Y)
                        walt2.position.X = cactus.position.X;
                    if (jesse2.jumping)
                        jesse2.position.Y -= jesse2.velocity.Y;
                    if (jesse2.jumping && !jesse2.falling)
                    {
                        jesse2.falling = true;
                    }
                    if (jesse2.falling)
                    {
                        jesse2.jumping = false;
                        jesse2.position.Y += jesse2.gravity.Y;
                    }
                    if (jesse2.position.Y >= jesse2.gposition.Y)
                        jesse2.falling = false;
                    else if (jesse2.falling && jesse2.checkCollideWith(cactus))
                        jesse2.falling = false;
                    if (jesse2.checkCollideWith(cactus) && jesse2.position.Y < jesse2.gposition.Y)
                        jesse2.position.X = cactus.position.X;
                    if (life1 <= 0)
                        walter.visible = false;
                    if (life2 <= 0)
                        jesse.visible = false;
                    if (life1 <= 0 && life2 <= 0)
                        currentGameState = GameState.GameOver;
                    if (timer >= 30)
                    {
                        time = 0;
                        timer = 0;
                        walt2.position = new Vector2(250, 350);
                        jesse2.position = new Vector2(500, 350);
                        currentGameState = GameState.level3screen;

                    }
                    break;
                case GameState.level3screen:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.Level3;
                    }
                    break;
                case GameState.Level3:
                    bossHealth = (int)MathHelper.Clamp(bossHealth, 0, 100);
                    playerControls3(gameTime);
                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    timer = (int)time;
                    if (bossHealth <= 50)
                    {
                        gus.player = gusDying;
                    }
                    if(wshot.visible && gus.alive)
                    {
                        if(wshot.checkCollideWith(gus))
                        {
                            wshot.visible = false;
                            wshot.alive = true;
                            bossHealth -= 2;
                            bossGrunt.Play();
                            score1 += 20;
                        }
                    }
                    if (jshot.visible && gus.alive)
                    {
                        if (jshot.checkCollideWith(gus))
                        {
                            jshot.visible = false;
                            jshot.alive = true;
                            bossHealth -= 2;
                            bossGrunt.Play();
                            score2 += 20;
                        }
                    }
                    if(walt2.checkCollideWith(gus))
                    {
                        score1 -= 10;
                        life1--;
                    }
                    if (jesse2.checkCollideWith(gus))
                    {
                        score2 -= 10;
                        life2--;
                    }
                    foreach (Player gameObject in sauls)
                    {
                        if (timer >= gameObject.spawnTime)
                        {
                            if (gameObject.alive)
                            {
                                gameObject.visible = true;
                                if(gameObject.direct == 1)
                                {
                                    ++gameObject.currentframe.X;
                                    if (gameObject.currentframe.X >= gameObject.sheetsize.X)
                                        gameObject.currentframe.X = 0;
                                    gameObject.position.X += gameObject.speed;
                                }
                                else
                                {
                                    ++gameObject.currentframe.X;
                                    if (gameObject.currentframe.X >= gameObject.sheetsize.X)
                                        gameObject.currentframe.X = 0;
                                    gameObject.position.X -= gameObject.speed;
                                }
                            }
                        }
                    }
                    if (wshot.visible)
                    {
                        wshot.alive = false;
                        if (wshot.direction == 1)
                        {
                            wshot.spriteEffects = SpriteEffects.FlipHorizontally;
                            wshot.position.X -= wshot.speed;
                        }
                        else
                        {
                            wshot.spriteEffects = SpriteEffects.None;
                            wshot.position.X += wshot.speed;
                        }
                    }
                    if (wshot.position.X <= 0 || wshot.position.X >= 650)
                    {
                        wshot.visible = false;
                        wshot.alive = true;
                    }
                    if (jshot.position.X <= 0 || jshot.position.X >= 650)
                    {
                        jshot.visible = false;
                        jshot.alive = true;
                    }
                    foreach(Player gameObject in sauls)
                    {
                        if(gameObject.alive && gameObject.visible)
                        {
                            if(wshot.visible)
                            {
                                if(wshot.checkCollideWith(gameObject))
                                {
                                    gameObject.visible = false;
                                    gameObject.alive = false;
                                    wshot.visible = false;
                                    wshot.alive = true;
                                    grunt.Play();
                                    score1 += 20;
                                }
                            }
                        }
                    }
                    if (jshot.visible)
                    {
                        jshot.alive = false;
                        if (jshot.direction == 1)
                        {
                            jshot.spriteEffects = SpriteEffects.FlipHorizontally;
                            jshot.position.X -= jshot.speed;
                        }
                        else
                        {
                            jshot.spriteEffects = SpriteEffects.None;
                            jshot.position.X += jshot.speed;
                        }
                    }
                    foreach(Player gameObject in sauls)
                    {
                        if(gameObject.alive && gameObject.visible)
                        {
                            if (jshot.visible)
                            {
                                if (jshot.checkCollideWith(gameObject))
                                {
                                    gameObject.visible = false;
                                    gameObject.alive = false;
                                    jshot.visible = false;
                                    jshot.alive = true;
                                    grunt.Play();
                                    score2 += 20;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < sauls.Length; i++)
                    {
                        if(!sauls[i].alive)
                            killCount++;
                    }
                    if(killCount == sauls.Length)
                    {
                        gus.visible = true;
                        
                    }
                    if(gus.visible)
                    {
                        if(gus.position.Y >= 385)
                            gus.position.Y -= gus.speed;
                    }
                    if(bossHealth <= 70)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.L) && vanish == 0)
                        {
                            bomb.Play();
                            score1 += 50;
                            bossHealth -= 30;
                            gus.player = gusDying;
                            explosion.visible = true;
                        }
                        else if (Keyboard.GetState().IsKeyDown(Keys.D1) && vanish == 0)
                        {
                            bomb.Play();
                            score2 += 50;
                            bossHealth -= 30;
                            gus.player = gusDying;
                            explosion.visible = true;

                        }
                        
                    }
                    if (bossHealth <= 50)
                        gus.player = gusDying;
                    if(explosion.visible)
                    {
                        vanish++;
                        
                    }
                    if (vanish >= 20)
                        explosion.visible = false;
                    if(bossHealth <= 0)
                    {
                        if (score1 > score2)
                            currentGameState = GameState.wWin;
                        else
                            currentGameState = GameState.jWin;
                    }
                    break;
                case GameState.Win:
                    break;
                case GameState.GameOver:
                    break;

            }

            base.Update(gameTime);
        }

        public void playSound()
        {
            hector.Play();
        }
        public void playerControls(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            {
                ++walter.currentframe.X;
                if (walter.currentframe.X >= walter.sheetsize.X)
                    walter.currentframe.X = 0;
                walter.position.X -= walter.speed;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
            {
                ++walter.currentframe.X;
                if (walter.currentframe.X >= walter.sheetsize.X)
                    walter.currentframe.X = 0;
                walter.position.X += walter.speed;
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0)
            {
                ++jesse.currentframe.X;
                if (jesse.currentframe.X >= jesse.sheetsize.X)
                    jesse.currentframe.X = 0;
                jesse.position.X -= jesse.speed;
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0)
            {
                ++jesse.currentframe.X;
                if (jesse.currentframe.X >= jesse.sheetsize.X)
                    jesse.currentframe.X = 0;
                jesse.position.X += jesse.speed;
            }


#if!XBOX
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ++walter.currentframe.X;
                if (walter.currentframe.X >= walter.sheetsize.X)
                    walter.currentframe.X = 0;
                walter.position.X -= walter.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                ++walter.currentframe.X;
                if (walter.currentframe.X >= walter.sheetsize.X)
                    walter.currentframe.X = 0;
                walter.position.X += walter.speed;

            }

            //updateBullets();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ++jesse.currentframe.X;
                if (jesse.currentframe.X >= jesse.sheetsize.X)
                    jesse.currentframe.X = 0;
                jesse.position.X -= jesse.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ++jesse.currentframe.X;
                if (jesse.currentframe.X >= jesse.sheetsize.X)
                    jesse.currentframe.X = 0;
                jesse.position.X += jesse.speed;
            }
            //if (Keyboard.GetState().IsKeyDown(Keys.Q))
            //{
            //    if (canFire)
            //    {
            //        ShotUpdate = TimeSpan.Zero;
            //        fire2();
            //        canFire = false;
            //    }
            //}
            //updateBullets2();
            //walter.position.X = MathHelper.Clamp(walter.position.X, 0 + (walter.SourceRectangle.Width / 2)
            //            , 800 - (walter.SourceRectangle.Width / 2));
            //walter.position.Y = MathHelper.Clamp(walter.position.Y, 0 + (walter.SourceRectangle.Height / 2)
            //    , 480 - (walter.SourceRectangle.Height / 2));
            //jesse.position.X = MathHelper.Clamp(jesse.position.X, 0 + (jesse.SourceRectangle.Width / 2)
            //            , 800 - (jesse.SourceRectangle.Width / 2));
            //jesse.position.Y = MathHelper.Clamp(jesse.position.Y, 0 + (jesse.SourceRectangle.Height / 2)
            //    , 480 - (jesse.SourceRectangle.Height / 2));
#endif
        }
        public void playerControls2(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X -= walt2.speed;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X += walt2.speed;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.jumping = true;

            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                if (wmoney.alive)
                {
                    wmoney.position = walt2.position;
                    wmoney.position.Y += 40;
                    wmoney.visible = true;
                }
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0)
            {
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X -= jesse2.speed;
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0)
            {
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X += jesse2.speed;
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y < 0)
            {
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.jumping = true;

            }
            if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed)
            {
                if (jmoney.alive)
                {
                    jmoney.position = jesse2.position;
                    jmoney.position.Y += 40;
                    jmoney.visible = true;
                }
            }

#if!XBOX
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X -= walt2.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X += walt2.speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                if (!walt2.jumping && !walt2.falling)
                {
                    walt2.jumping = true;
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightAlt))
            {

                if (wmoney.alive)
                {
                    wmoney.position = walt2.position;
                    wmoney.position.Y += 40;
                    wmoney.visible = true;
                }
            }
            //ShotUpdate += gameTime.ElapsedGameTime;
            //if (ShotUpdate > TimeSpan.FromMilliseconds(ShotFreq))
            //{
            //    fire();
            //    canFire = true;
            //    ShotUpdate = TimeSpan.Zero;
            //}
            //updateBullets();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X -= jesse2.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X += jesse2.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                if (!jesse2.jumping && !jesse2.falling)
                {
                    jesse2.jumping = true;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (jmoney.alive)
                {
                    jmoney.position = jesse2.position;
                    jmoney.position.Y += 40;
                    jmoney.visible = true;
                }
            }
            //updateBullets2();
            //walter.position.X = MathHelper.Clamp(walter.position.X, 0 + (walter.SourceRectangle.Width / 2)
            //            , 800 - (walter.SourceRectangle.Width / 2));
            //walter.position.Y = MathHelper.Clamp(walter.position.Y, 0 + (walter.SourceRectangle.Height / 2)
            //    , 480 - (walter.SourceRectangle.Height / 2));
            //jesse.position.X = MathHelper.Clamp(jesse.position.X, 0 + (jesse.SourceRectangle.Width / 2)
            //            , 800 - (jesse.SourceRectangle.Width / 2));
            //jesse.position.Y = MathHelper.Clamp(jesse.position.Y, 0 + (jesse.SourceRectangle.Height / 2)
            //    , 480 - (jesse.SourceRectangle.Height / 2));
#endif
        }
        public void playerControls3(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
            {
                walt2.spriteEffects = SpriteEffects.FlipHorizontally;
                walt2.direction = 1;
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X -= walt2.speed;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
            {
                walt2.spriteEffects = SpriteEffects.None;
                walt2.direction = 2;
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X += walt2.speed;
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
            {
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.jumping = true;

            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                walt2.currentframe.X = 4;
                if (wshot.alive)
                {
                    wshot.position = walt2.position;
                    wshot.position.Y += 40;
                    wshot.visible = true;
                }
                wshot.direction = walt2.direction;
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0)
            {
                jesse2.spriteEffects = SpriteEffects.FlipHorizontally;
                jesse2.direction = 1;
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X -= jesse2.speed;
            }
            if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0)
            {
                jesse2.spriteEffects = SpriteEffects.None;
                jesse2.direction = 2;
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X += jesse2.speed;
            }
            if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed)
            {
                jesse2.currentframe.X = 4;
                if (jshot.alive)
                {
                    jshot.position = walt2.position;
                    jshot.position.Y += 40;
                    jshot.visible = true;
                }
                jshot.direction = jesse2.direction;
            }

#if!XBOX
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                walt2.spriteEffects = SpriteEffects.FlipHorizontally;
                walt2.direction = 1;
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X -= walt2.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                walt2.spriteEffects = SpriteEffects.None;
                walt2.direction = 2;
                ++walt2.currentframe.X;
                if (walt2.currentframe.X >= 4)
                    walt2.currentframe.X = 0;
                walt2.position.X += walt2.speed;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.RightAlt))
            {
                
                walt2.currentframe.X = 4;
                if (wshot.alive && !wshot.visible)
                {
                    wshot.position = walt2.position;
                    wshot.position.Y += 40;
                    wshot.visible = true;
                }
                wshot.direction = walt2.direction;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                jesse2.spriteEffects = SpriteEffects.FlipHorizontally;
                jesse2.direction = 1;
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X -= jesse2.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                jesse2.spriteEffects = SpriteEffects.None;
                jesse2.direction = 2;
                ++jesse2.currentframe.X;
                if (jesse2.currentframe.X >= 4)
                    jesse2.currentframe.X = 0;
                jesse2.position.X += jesse2.speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                jesse2.currentframe.X = 4;
                if (jshot.alive)
                {
                    jshot.position = jesse2.position;
                    jshot.position.Y += 40;
                    jshot.visible = true;
                }
                jshot.direction = jesse2.direction;
            }
            //updateBullets2();
            //walter.position.X = MathHelper.Clamp(walter.position.X, 0 + (walter.SourceRectangle.Width / 2)
            //            , 800 - (walter.SourceRectangle.Width / 2));
            //walter.position.Y = MathHelper.Clamp(walter.position.Y, 0 + (walter.SourceRectangle.Height / 2)
            //    , 480 - (walter.SourceRectangle.Height / 2));
            //jesse.position.X = MathHelper.Clamp(jesse.position.X, 0 + (jesse.SourceRectangle.Width / 2)
            //            , 800 - (jesse.SourceRectangle.Width / 2));
            //jesse.position.Y = MathHelper.Clamp(jesse.position.Y, 0 + (jesse.SourceRectangle.Height / 2)
            //    , 480 - (jesse.SourceRectangle.Height / 2));
#endif
        }
        public void backgroundMotion(GameTime gameTime)
        {
            if (lvl2p1.Position.X < -lvl2p1.Size.Width)
            {
                lvl2p1.Position.X = lvl2p3.Position.X + lvl2p3.Size.Width;
            }

            if (lvl2p2.Position.X < -lvl2p2.Size.Width)
            {
                lvl2p2.Position.X = lvl2p1.Position.X + lvl2p1.Size.Width;
            }
            if (lvl2p3.Position.X < -lvl2p3.Size.Width)
            {
                lvl2p3.Position.X = lvl2p2.Position.X + lvl2p2.Size.Width;
            }

            Vector2 aDirection = new Vector2(-1, 0);
            Vector2 aSpeed = new Vector2(100, 0);
            lvl2p1.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            lvl2p2.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            lvl2p3.Position += aDirection * aSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        }
        /// <summary>
        /// This is called when the 'game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.Menu:
                    spriteBatch.Draw(menu, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Info:
                    if (timer % 2 == 0)
                    {
                        spriteBatch.Draw(info, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                            null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(infoblink, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                            null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                    break;
                case GameState.Level1:
                    spriteBatch.Draw(level1, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    van.draw(spriteBatch);
                    walter.draw(spriteBatch);
                    if (players == 2)
                        jesse.draw(spriteBatch);
                    spriteBatch.DrawString(basic, "Time: " + timer, new Vector2(700, 10), Color.Blue);
                    spriteBatch.DrawString(basic, "W Score: " + score1, new Vector2(10, 10), Color.White);
                    if (players == 2)
                        spriteBatch.DrawString(basic, "J Score: " + score2, new Vector2(170, 10), Color.White);
                    spriteBatch.DrawString(basic, "W Lives: " + life1, new Vector2(10, 450), Color.Blue);
                    if (players == 2)
                        spriteBatch.DrawString(basic, "J Lives: " + life2, new Vector2(600, 450), Color.Blue);
                    foreach (FallingStuff gameObject in crystals)
                    {
                        gameObject.draw(spriteBatch);
                    }
                    foreach (FallingStuff gameObject in chicken)
                    {
                        gameObject.draw(spriteBatch);
                    }
                    break;
                case GameState.level2screen:
                    spriteBatch.Draw(level2screen, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Level2:
                    lvl2p1.Draw(this.spriteBatch);
                    lvl2p2.Draw(this.spriteBatch);
                    lvl2p3.Draw(this.spriteBatch);
                    cactus.draw(spriteBatch);
                    walt2.draw(spriteBatch);
                    saul.draw(spriteBatch);
                    if (players == 2)
                        jesse2.draw(spriteBatch);
                    spriteBatch.DrawString(basic, "Time: " + timer, new Vector2(700, 10), Color.Blue);
                    spriteBatch.DrawString(basic, "W Score: " + score1, new Vector2(10, 10), Color.White);
                    if (players == 2)
                        spriteBatch.DrawString(basic, "J Score: " + score2, new Vector2(170, 10), Color.White);
                    spriteBatch.DrawString(basic, "W Lives: " + life1, new Vector2(10, 450), Color.Blue);
                    if (players == 2)
                        spriteBatch.DrawString(basic, "J Lives: " + life2, new Vector2(600, 450), Color.Blue);
                    wmoney.draw(spriteBatch);
                    jmoney.draw(spriteBatch);
                    break;
                case GameState.level3screen:
                    spriteBatch.Draw(level3screen, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Level3:
                    spriteBatch.Draw(level3, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                            null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.DrawString(basic, "Time: " + vanish, new Vector2(700, 10), Color.Blue);
                    spriteBatch.DrawString(basic, "W Score: " + score1, new Vector2(10, 10), Color.White);
                    if (players == 2)
                        spriteBatch.DrawString(basic, "J Score: " + score2, new Vector2(170, 10), Color.White);
                    spriteBatch.DrawString(basic, "W Lives: " + life1, new Vector2(10, 450), Color.Blue);
                    if (players == 2)
                        spriteBatch.DrawString(basic, "J Lives: " + life2, new Vector2(600, 450), Color.Blue);
                    spriteBatch.Draw(healthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - healthBar.Width / 2,
                        30, healthBar.Width, 44), new Rectangle(0, 45, healthBar.Width, 44), Color.Gray);
                    spriteBatch.Draw(healthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - healthBar.Width / 2,
                        30, (int)(healthBar.Width * ((double)bossHealth / 100)), 44),
                        new Rectangle(0, 45, healthBar.Width, 44), Color.Red);
                    spriteBatch.Draw(healthBar, new Rectangle(this.Window.ClientBounds.Width / 2 - healthBar.Width / 2,
                        30, healthBar.Width, 44), new Rectangle(0, 0, healthBar.Width, 44), Color.White);
                    walt2.draw(spriteBatch);
                    if (players == 2)
                        jesse2.draw(spriteBatch);
                    foreach (Player gameObject in sauls)
                        gameObject.draw(spriteBatch);
                    if (bossHealth <= 70)
                    {
                        spriteBatch.DrawString(basic, "  SPECIAL MOVE: \nPLAYER 1 PRESS L \nPlayer 2 PRESS 1",
                            new Vector2(550, 130), Color.Red);
                    }
                    wshot.draw(spriteBatch);
                    jshot.draw(spriteBatch);
                    gus.draw(spriteBatch);
                    explosion.draw(spriteBatch);
                    break;
                case GameState.wWin:
                    spriteBatch.Draw(wWin, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.jWin:
                    spriteBatch.Draw(jWin, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;
                case GameState.Win:
                    break;
                case GameState.GameOver:
                    spriteBatch.Draw(gameOver, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    break;

            }
            // TODO: Add your drawing code here



            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
