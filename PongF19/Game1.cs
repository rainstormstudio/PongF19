using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongF19
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private RenderTarget2D _renderTarget;
        private SpriteBatch _spriteBatch;

        private Texture2D mainSpritesTexture;
        private Texture2D gameBoardTexture;
        private Texture2D numbersTexture;

        private GameBoard _gameBoard;
        private Player _player1;
        private Player _player2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _renderTarget = new RenderTarget2D(GraphicsDevice, 400, 300);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            mainSpritesTexture = Content.Load<Texture2D>("mainsprites");
            gameBoardTexture = Content.Load<Texture2D>("gameboard");
            numbersTexture = Content.Load<Texture2D>("numbers");

            _player1 = new Player(mainSpritesTexture, new Rectangle(0, 0, 8, 32), new Vector2(34, 60));
            _player2 = new Player(mainSpritesTexture, new Rectangle(0, 0, 8, 32), new Vector2(358, 60));
            _gameBoard = new GameBoard(gameBoardTexture, _player1, _player2);
            _gameBoard.setScore1(new Score(numbersTexture, new Vector2(100, 20)));
            _gameBoard.setScore2(new Score(numbersTexture, new Vector2(300, 20)));
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player1.updateControl(deltaTime, Keys.W, Keys.S);
            _player2.updateControl(deltaTime, Keys.Up, Keys.Down);
            _gameBoard.update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _gameBoard.Draw(_spriteBatch);
            _player1.Draw(_spriteBatch);
            _player2.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, 800, 600), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
