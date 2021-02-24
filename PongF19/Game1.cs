using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions;

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

        private CollisionComponent _collisionComponent;
        private GameBoard _gameBoard;
        private Player _player1;
        private Player _player2;
        private Ball _ball;
        private Wall _Nwall;
        private Wall _Swall;
        private Wall _Wwall;
        private Wall _Ewall;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _collisionComponent = new CollisionComponent(new MonoGame.Extended.RectangleF(0, 0, 400, 300));

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
            _ball = new Ball(GraphicsDevice, mainSpritesTexture, new Rectangle(8, 0, 8, 8));
            _Nwall = new Wall(new Rectangle(4, 56, 392, 4));
            _Swall = new Wall(new Rectangle(4, 296, 392, 4));
            _Wwall = new Wall(new Rectangle(0, 60, 4, 236));
            _Ewall = new Wall(new Rectangle(396, 60, 4, 236));

            _collisionComponent.Insert(_player1);
            _collisionComponent.Insert(_player2);
            _collisionComponent.Insert(_ball);
            _collisionComponent.Insert(_Nwall);
            _collisionComponent.Insert(_Swall);
            _collisionComponent.Insert(_Wwall);
            _collisionComponent.Insert(_Ewall);
            
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
            _ball.Update(deltaTime);
            _gameBoard.update();
            _collisionComponent.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _gameBoard.Draw(_spriteBatch);
            _ball.Draw(deltaTime, _spriteBatch);
            _player1.Draw(deltaTime, _spriteBatch);
            _player2.Draw(deltaTime, _spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, 800, 600), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
