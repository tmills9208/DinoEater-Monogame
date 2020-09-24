/* Game1.cs
 * The game runs on here
 * 
 * Revision History
 *      Tyler Mills, 2019.12.08: Created
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DinoEater
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public const string BACKGROUND = "Images/DinoGameBackground";
        public const string FONT = "Fonts/Arial";
        public const string LARGE_FONT = "Fonts/ArialLarge";
        public const string MUSIC = "Sounds/Old RuneScape Soundtrack Jungle Troubles";
        public const string YOSHI_LICK = "Sounds/YoshiLick";

        GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics { get => graphics; }
        SpriteBatch spriteBatch;
        public SpriteBatch Sprite { get => spriteBatch; }

        private GameState currentState;
        private List<string> menuItems = new List<string>
        {
            "Play",
            "Help",
            "About",
            "Quit"
        };
        private ActionState actionState;
        private MenuState mainMenuState;
        private HelpState helpState;
        private AboutState aboutState;

        Texture2D background;
        public SpriteFont font;
        public SpriteFont largeFont;
        SoundEffect music;
        public SoundEffect yoshiLick;

        public int hiscore;

        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            oldState = Keyboard.GetState();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = this.Content.Load<Texture2D>(BACKGROUND);
            music = this.Content.Load<SoundEffect>(MUSIC);
            yoshiLick = this.Content.Load<SoundEffect>(YOSHI_LICK);
            font = this.Content.Load<SpriteFont>(FONT);
            largeFont = this.Content.Load<SpriteFont>(LARGE_FONT);

            actionState = new ActionState(this);
            mainMenuState = new MenuState(this, menuItems);
            helpState = new HelpState(this);
            aboutState = new AboutState(this);
            this.Components.Add(actionState);
            this.Components.Add(mainMenuState);
            this.Components.Add(helpState);
            this.Components.Add(aboutState);

            music.Play();

            currentState = mainMenuState;
            currentState.Show();
        }
        public void Notify(GameState sender, string action)
        {
            currentState.Hide();

            if (sender is MenuState)
            {
                switch (action)
                {
                    case "Play":
                        currentState = actionState;
                        break;
                    case "Help":
                        currentState = helpState;
                        break;
                    case "About":
                        currentState = aboutState;
                        break;
                    case "Quit":
                        Exit();
                        break;
                }
            }
            else if (sender is ActionState)
            {
                currentState = mainMenuState;
                actionState.Show();
                actionState.Enabled = false;
            }
            else if (sender is HelpState)
            {
                currentState = mainMenuState;
            }
            else if (sender is AboutState)
            {
                currentState = mainMenuState;
            }

            currentState.Show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Sprite.Begin();
            Sprite.Draw(background, Vector2.Zero, Color.White);
            Sprite.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// Updates the high score
        /// </summary>
        /// <param name="score"></param>
        public void UpdateHiScore(int score)
        {
            if (score > hiscore)
            {
                hiscore = score;
            }
        }
    }
}
