/* ActionState.cs
 * The actual game logic and runtime is done here
 * 
 * Revision History
 *      Tyler Mills, 2019.12.08: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoEater
{
    class ActionState : GameState
    {
        Game1 parent;

        Player player;
        List<NPC> enemies;

        int score;
        bool isPlaying;

        KeyboardState oldState;
        public ActionState(Game game) : base(game)
        {
            parent = (Game1)game;
            Initialize();
        }
        /// <summary>
        /// Resets the game
        /// </summary>
        public override void Initialize()
        {
            if (player != null)
            {
                this.Components.Remove(player);
                player = null;
            }
            if (enemies != null)
            {
                foreach (var npc in enemies)
                {
                    this.Components.Remove(npc);
                }
                enemies = null;
            }
            score = 0;

            enemies = new List<NPC>();

            player = new Player(parent, Dino.GREEN_DINO);
            this.Components.Add(player);

            isPlaying = true;

            base.Initialize();
        }
        /// <summary>
        /// Gets user input and makes sure there are enough enemy dinos on screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape))
            {
                parent.Notify(this, "");
            }

            if (enemies.Count < 30)
            {
                NPC npc = new NPC(parent, Dino.GREEN_DINO, player.Size);
                enemies.Add(npc);
                this.Components.Add(npc);
            }
            List<NPC> tempList = new List<NPC>();
            foreach (var npc in enemies)
            {
                if (npc.OutOfBounds())
                {
                    tempList.Add(npc);
                }

                if (npc.GetCollisionBounds().Intersects(player.GetCollisionBounds()))
                {
                    if (npc.Size > player.Size)
                    {
                        parent.UpdateHiScore(score);
                        Initialize();
                        parent.Notify(this, "");
                    }
                    else
                    {
                        player.Size += 1;
                        score += npc.Size;
                        tempList.Add(npc);
                        parent.yoshiLick.Play();
                    }
                }
            }
            if (tempList.Count > 0)
            {
                foreach (var npc in tempList)
                {
                    this.Components.Remove(npc);
                    enemies.Remove(npc);
                }
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// Draws the text of the current score of the running game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            parent.Sprite.Begin();
            parent.Sprite.DrawString(
                parent.font,
                "Score: " + score,
                new Vector2(
                    400,
                    5),
                Color.White
            );
            parent.Sprite.End();
        }
    }
}
