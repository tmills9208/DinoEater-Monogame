/* GameState.cs
 * Superclass to handle game states
 * 
 * Revision History
 *      Tyler Mills, 2019.12.08: Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DinoEater
{
    public class GameState : DrawableGameComponent
    {
        protected Game1 parent;
        public List<GameComponent> Components { get; set; }
        public GameState(Game game) : base(game)
        {
            parent = (Game1)game;
            Components = new List<GameComponent>();
            Hide();
        }
        /// <summary>
        /// Sets the state and enables / makes visible of the components of the state
        /// </summary>
        /// <param name="state"></param>
        public virtual void SetState(bool state)
        {
            this.Enabled = state;
            this.Visible = state;
            foreach (GameComponent item in Components)
            {
                item.Enabled = state;
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    comp.Visible = state;
                }
            }
        }
        /// <summary>
        /// Changes the state to true
        /// </summary>
        public virtual void Show()
        {
            SetState(true);
        }
        /// <summary>
        /// Changes the state to false
        /// </summary>
        public virtual void Hide()
        {
            SetState(false);
        }
        /// <summary>
        /// Updates components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// Draws components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
