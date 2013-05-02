namespace monoDoodler
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using monoDoodler.Entity;
    using monoDoodler.Manager;

    public class Game1 : Game
    {
        private const int DefaultDoodleSpeedX = 10;

        private readonly GraphicsDeviceManager graphics;

        private KeyboardState currentKeyboardState;

        private SpriteBatch spriteBatch;

        private Doodle myDoodle;

        private PlatformManager platformManager;

        private BonusManager bonusManager;

        private BulletManager bulletManager;

        private EnemyManager enemyManager;

        private int mouseX;

        private int score;

        private bool pressedGoToRight;

        private bool pressedGoToLeft;

        private bool pressedFire;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.graphics.IsFullScreen = true;
            this.score = 0;
            this.myDoodle = new Doodle();
            this.myDoodle = new Doodle();
            this.platformManager = new PlatformManager();
            this.bonusManager = new BonusManager();
            this.bulletManager = new BulletManager();
            this.enemyManager = new EnemyManager();
            Properties.Settings.MonitorHeight = this.graphics.GraphicsDevice.DisplayMode.Height;
            Properties.Settings.MonitorWigth = this.graphics.GraphicsDevice.DisplayMode.Width;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.myDoodle.Initialize(Content);
            this.platformManager.Initialize(Content);
            this.bonusManager.Initialize(Content);
            this.bulletManager.Initialize(Content);
            this.enemyManager.Initialize(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            RefreshKeyBoard();

            if (this.pressedFire)
            {
                this.bulletManager.Fire(this.myDoodle, this.bonusManager.BonusIsActive(BonusType.MultFire), Content);
            }

            if (this.enemyManager.KillEnemy(this.bulletManager.List))
            {
                this.score += 50;
            }

            if (enemyManager.KillDoodle(myDoodle))
            {
                this.GameOver();
            }

            this.bonusManager.IsTakenBonus(this.myDoodle);

            this.bonusManager.TimeRefresh();

            float strenge;
            if (this.platformManager.StendToPlatfotm(this.myDoodle, out strenge))
            {
                if (this.bonusManager.BonusIsActive(BonusType.DoobleJump))
                {
                    strenge *= 1.5F;
                }

                this.myDoodle.Jamp(strenge);
            }

            if (this.pressedGoToLeft)
            {
                this.myDoodle.MooveX(-DefaultDoodleSpeedX);
            }
            else if (this.pressedGoToRight)
            {
                this.myDoodle.MooveX(DefaultDoodleSpeedX);
            }

            this.bulletManager.Moove();
            this.enemyManager.Moove();
            this.platformManager.Moove();
            this.enemyManager.ReInitIfOutMonitor(Content);
            this.platformManager.ReInitIfOutMonitor(Content);

            this.myDoodle.AccelerationY--;
            if (this.myDoodle.Position.Y < Properties.Settings.MonitorHeight / 2F && this.myDoodle.AccelerationY > 0)
            {
                this.platformManager.WindowMooveY(this.myDoodle.AccelerationY, Content);
                this.enemyManager.WindowMooveY(this.myDoodle.AccelerationY, Content);
                this.bonusManager.WindowMooveY(this.myDoodle.AccelerationY, Content);
                this.score++;
            }
            else
            {
                this.myDoodle.MooveY();
            }

            if (this.myDoodle.Position.Y > Properties.Settings.MonitorHeight)
            {
                GameOver();
            }

            if (Math.Abs(gameTime.TotalGameTime.Milliseconds % 10000 - 0) < 20)
            {
                this.enemyManager.AddItem(Content);
            }

            if (Math.Abs(gameTime.TotalGameTime.Milliseconds % 20000 - 0) < 20)
            {
                this.bonusManager.AddItem(Content);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            this.platformManager.Draw(this.spriteBatch);
            this.bonusManager.Draw(this.spriteBatch);
            this.bulletManager.Draw(this.spriteBatch);
            this.enemyManager.Draw(this.spriteBatch);
            this.myDoodle.Draw(this.spriteBatch);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void GameOver()
        { 
            platformManager.List = new List<Platform>();
            enemyManager.List = new List<Enemy>();
            this.myDoodle.Initialize(Content);
            enemyManager.Initialize(Content);
            platformManager.Initialize(Content);
           
        }

        private void RefreshKeyBoard()
        {
            currentKeyboardState = Keyboard.GetState();
            this.pressedGoToLeft = false;
            this.pressedGoToRight = false;
            this.pressedFire = false;

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                this.pressedGoToLeft = true;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                this.pressedGoToRight = true;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Space) || currentKeyboardState.IsKeyDown(Keys.Up)
               || currentKeyboardState.IsKeyDown(Keys.W))
            {
                this.pressedFire = true;
            }
        }
    }
}