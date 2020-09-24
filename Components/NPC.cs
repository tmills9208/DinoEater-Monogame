/* NPC.cs
 * Non-Playable Dinos that automaticaly move around the game
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
    class NPC : Dino
    {
        Game1 parent;
        Random rand;
        public NPC(Game game, string textureLocation, int size) : base(game, textureLocation)
        {
            parent = (Game1)game;
            rand = new Random();
            int chosenColor = rand.Next(0, 3);

            if (chosenColor == 0)
                textureLocation = RED_DINO;
            else if (chosenColor == 1)
                textureLocation = BLUE_DINO;
            else if (chosenColor == 2)
                textureLocation = YELLOW_DINO;

            dinoTexture = parent.Content.Load<Texture2D>(textureLocation);

            speed = rand.Next(1, 3);
            Size = rand.Next(size - 25, size + 25);

            // Positioning and direction
            int direction = rand.Next(0, 2);
            int yPosition = rand.Next(0, parent.Graphics.PreferredBackBufferHeight - this.Size);

            this.Position = new Vector2(
                (parent.Graphics.PreferredBackBufferWidth) * direction, 
                yPosition
            );
            if (direction == 1)
                this.speed *= -1;
        }
        /// <summary>
        /// Updates npc position
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            this.Position = new Vector2(this.Position.X + this.speed, this.Position.Y);

            base.Update(gameTime);
        }
    }
}
