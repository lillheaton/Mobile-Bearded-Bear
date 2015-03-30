using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Mobile_Bearded_Bear
{
    public class SnakeGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D texture2D;
        private Snake snake;

        public SnakeGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.IsFullScreen = true;

            TouchPanel.EnabledGestures = GestureType.HorizontalDrag | GestureType.VerticalDrag;

            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            snake = new Snake(this.texture2D);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.texture2D = new Texture2D(this.GraphicsDevice, 1, 1);
            this.texture2D.SetData(new[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            snake.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            snake.Draw(spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
