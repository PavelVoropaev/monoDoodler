using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using monoDoodler.Entity;
using monoDoodler.Manager;

namespace monoDoodler
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;

        KeyboardState currentKeyboardState;

        SpriteBatch _spriteBatch;

        private const int DefaultDoodleSpeedX = 10;

        private Doodle _myDoodle;

        private PlatformManager _platformManager;

        private BonusManager _bonusManager;

        private BulletManager _bulletManager;

        private EnemyManager _enemyManager;

        private int _mouseX;

        private int _score;

        private bool _pressedGoToRight;

        private bool _pressedGoToLeft;

        private bool _pressedFire;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _myDoodle = new Doodle();
            _score = 0;
            _myDoodle = new Doodle();
            _platformManager = new PlatformManager();
            _bonusManager = new BonusManager();
            _bulletManager = new BulletManager();
            _enemyManager = new EnemyManager();
            Properties.Settings.MonitorHeight = GraphicsDevice.Viewport.TitleSafeArea.Height;
            Properties.Settings.MonitorWigth =  GraphicsDevice.Viewport.TitleSafeArea.Width;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _myDoodle.Initialize(Content);
            _platformManager.Initialize(Content);
            _bonusManager.Initialize(Content);
            _bulletManager.Initialize(Content);
            _enemyManager.Initialize(Content);

        }
        private void RefreshKeyBoard()
        {
                _pressedGoToLeft = false;
                _pressedGoToRight = false;
                _pressedFire = false;

            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                _pressedGoToLeft = true;
            }
             if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                _pressedGoToRight = true;
            }
             if (currentKeyboardState.IsKeyDown(Keys.Space) || currentKeyboardState.IsKeyDown(Keys.Up)
                || currentKeyboardState.IsKeyDown(Keys.W))
            {
                _pressedFire = true;
            }

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            currentKeyboardState = Keyboard.GetState();
            RefreshKeyBoard();

            if (_pressedFire && gameTime.TotalGameTime.Milliseconds % 7 == 0)
            {
                _bulletManager.Fire(_myDoodle, _bonusManager.MultiFireIsActive(), Content);
            }

            if (_enemyManager.KillEnemy(_bulletManager.List))
            {
                _score += 50;
            }

          //  _enemyManager.KillDoodle(_myDoodle);

            _bonusManager.IsTakenBonus(_myDoodle);

            _bonusManager.TimeRefresh();

            float strenge;
            if (_platformManager.StendToPlatfotm(_myDoodle, out strenge))
            {
                if (_bonusManager.DoobleJumpIsActive())
                {
                    strenge *= 1.5F;
                }

                _myDoodle.Jamp(strenge);
            }

            if (_pressedGoToLeft)
            {
                _myDoodle.MooveX(-DefaultDoodleSpeedX);
            }
            else if (_pressedGoToRight)
            {
                _myDoodle.MooveX(DefaultDoodleSpeedX);
            }

            _bulletManager.Moove();
            _enemyManager.Moove();
            _platformManager.Moove();

            _myDoodle.AccelerationY--;
            if (_myDoodle.Position.Y < Properties.Settings.MonitorHeight / 2F && _myDoodle.AccelerationY > 0)
            {
                _platformManager.WindowMooveY(_myDoodle.AccelerationY, Content);
                _enemyManager.WindowMooveY(_myDoodle.AccelerationY, Content);
                _bonusManager.WindowMooveY(_myDoodle.AccelerationY, Content);
                _score++;
            }
            else
            {
                _myDoodle.MooveY();
            }

            if (_myDoodle.Position.Y > Properties.Settings.MonitorHeight)
            {
          //      GameOver();
            }

            if (gameTime.TotalGameTime.Seconds % 7 % 40 == 0)
            {
                _enemyManager.AddItem(Content);
            }

            if (gameTime.TotalGameTime.Seconds % 7 % 100 == 0)
            {
                _bonusManager.AddItem(Content);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _platformManager.Draw(_spriteBatch);
            _bonusManager.Draw(_spriteBatch);
            _bulletManager.Draw(_spriteBatch);
            _enemyManager.Draw(_spriteBatch);
            _myDoodle.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}