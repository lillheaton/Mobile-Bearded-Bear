using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Linq;

namespace Mobile_Bearded_Bear
{
    public class SnakeGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D texture2D;        
        private Snake snake;
        private SnakeFood snakeFood;

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
            snakeFood = new SnakeFood(texture2D, GraphicsDevice.Viewport);
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
            snakeFood.Update(gameTime);
            this.CheckCollisions();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

            snake.Draw(spriteBatch);
            snakeFood.Draw(spriteBatch);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        private void CheckCollisions()
        {
            if (snake.Dead) return;

            // Get head
            var futureSnakeHead = Vector2.Add(snake.BodyParts[0], snake.NextDirection.GetVector());

            // check self collision
            if (snake.BodyParts.GetRange(1, snake.BodyParts.Count - 1).Any(part => part == futureSnakeHead))
            {
                snake.SetDead();
                snakeFood.Food.Clear();
            }

            // viewport collision
            var headViewPosition = Vector2.Multiply(futureSnakeHead, Snake.SnakeBodySize);

            if (headViewPosition.X > GraphicsDevice.Viewport.Width || headViewPosition.X < 0 || 
                headViewPosition.Y > GraphicsDevice.Viewport.Height || headViewPosition.Y < 0)
            {
                snake.SetDead();
                snakeFood.Food.Clear();
            }

            for (int i = 0; i < snakeFood.Food.Count; i++)
            {
                if (snakeFood.Food[i] == futureSnakeHead)
                {
                    snakeFood.Food.Remove(snakeFood.Food[i]);
                    snake.AddPart();
                }
            }
        }
    }
}
