/* Dino.cs
 * Superclass to handle the dino sprites
 * 
 * Revision History
 *      Tyler Mills, 2019.12.08: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DinoEater
{
    class Dino : DrawableGameComponent
    {
        public const string GREEN_DINO = "Images/DinoSprites - vita";
        public const string RED_DINO = "Images/DinoSprites - mort";
        public const string BLUE_DINO = "Images/DinoSprites - doux";
        public const string YELLOW_DINO = "Images/DinoSprites - tard";
        public const int COLUMNS = 24;
        public const float PADDING_RATIO = 0.16333f;

        Game1 parent;
        protected Texture2D dinoTexture;
        int width;
        int currentX;
        public Vector2 Position { get; protected set; }
        private Vector2 oldPosition;
        protected int speed;
        private bool flipped;
        public int Size { get; set; }

        int timeSinceLastFrame;

        public Dino(Game game, string textureLocation) : base(game)
        {
            parent = (Game1)game;
            dinoTexture = parent.Content.Load<Texture2D>(textureLocation);
            width = dinoTexture.Width / COLUMNS;
            currentX = width * 4;
            speed = 3;
            Position = Vector2.Zero;
            oldPosition = Position;
            
            flipped = false;
            Size = 50;

            timeSinceLastFrame = 0;
        }
        /// <summary>
        /// CHanges which animation and which direction the dino faces, 
        /// along with updating position so it can move
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Animate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > 120)
            {
                currentX += width;
                // Idle animation
                if (oldPosition == Position)
                {
                    if (currentX < 0 || currentX >= width * 4)
                    {
                        currentX = 0;
                    }
                }
                // Running animation
                else if (oldPosition != Position){
                    if (currentX < width * 4 || currentX >= width * 10)
                    {
                        currentX = width * 4;
                    }
                }
                
                timeSinceLastFrame -= 120;
            }

            if (oldPosition.X > Position.X)
                flipped = true;
            else if (oldPosition.X < Position.X)
                flipped = false;

            oldPosition = Position;

            // Finalize
            Vector2 tempPosition = new Vector2(
                Position.X,
                MathHelper.Clamp(
                    Position.Y, 0, 
                    parent.Graphics.PreferredBackBufferHeight - Size)
            );
            Position = tempPosition;

            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the dino
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Rectangle sourceRectangle = new Rectangle(currentX, 0, width, dinoTexture.Height);
            SpriteEffects flipEffect = SpriteEffects.None;
            if (flipped)
                flipEffect = SpriteEffects.FlipHorizontally;

            parent.Sprite.Begin(SpriteSortMode.Immediate, 
                samplerState:SamplerState.PointClamp);

            parent.Sprite.Draw(
                dinoTexture,
                destinationRectangle: GetBounds(),
                sourceRectangle,
                origin: Vector2.Zero,
                rotation: 0,
                color:Color.White,
                effects:flipEffect,
                layerDepth: 0
            );
            parent.Sprite.Draw(dinoTexture,GetCollisionBounds(),Color.White);
            parent.Sprite.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Gets the rectangle bounds, based on position and size
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)Position.X, (int)Position.Y,
                Size, Size
            );
        }
        /// <summary>
        /// Checks if the dino is outside the game window
        /// </summary>
        /// <returns></returns>
        public bool OutOfBounds()
        {
            Rectangle bounds = GetBounds();
            if (bounds.X < -Size * 2 || bounds.X > parent.Graphics.PreferredBackBufferWidth)
                return true;
            else
                return false;
        }
        public Rectangle GetCollisionBounds()
        {
            Rectangle result = GetBounds();
            //result.X = (int)(result.X + result.X * (PADDING_RATIO / 2));
            //result.Y = (int)(result.Y + result.Y * (PADDING_RATIO / 2));
            result.Width = (int)(result.Width - result.Width * PADDING_RATIO);
            result.Height = (int)(result.Height - result.Height * PADDING_RATIO);

            return result;
        }
    }
}
