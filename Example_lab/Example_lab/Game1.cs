using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Example_lab.Models;
using Example_lab.Sprites;

namespace Example_lab
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public partial class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Sprite> _sprites;
     

        private Point _position = new Point(0, 0);

        private Point P = new Point(400 ,400);
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = C.MAINWINDOW.X;   /// definisco la larghezza della finestra esterna
            graphics.PreferredBackBufferHeight = C.MAINWINDOW.Y;

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
            //carichiamo i contenuti

            //C.brickBlack = H.CreateTexure(GraphicsDevice, 1, 1, pixel => Color.Black);

            

            C.brickWall = Content.Load<Texture2D>("mossy");
            C.brickGrass = Content.Load<Texture2D>("grass");
            C.brickLava = Content.Load<Texture2D>("lava");
            C.brickDiamond = Content.Load<Texture2D>("diamante2");
            C.brickStart = Content.Load<Texture2D>("grass");
            C.brickEnd = Content.Load<Texture2D>("door");
            C.brickEnd2 = Content.Load<Texture2D>("door2");


            ReadLabyrinthSpec(V.labyrinthMatrix ,C.LabyrinthPathName);

            V.hero = Content.Load<Texture2D>("hero");
            V.heroDamaged = Content.Load<Texture2D>("hero_damage");

            V.currentHeroPosition = V.labEnter[0];

      var animations = new Dictionary<string, Animation>()
      {
        { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 3) },
        { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 3) },
        { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 3) },
        { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 3) },
      };

      _sprites = new List<Sprite>()
      {
        new Sprite(new Dictionary<string, Animation>()
        {
          { "WalkUp", new Animation(Content.Load<Texture2D>("Player/WalkingUp"), 3) },
          { "WalkDown", new Animation(Content.Load<Texture2D>("Player/WalkingDown"), 3) },
          { "WalkLeft", new Animation(Content.Load<Texture2D>("Player/WalkingLeft"), 3) },
          { "WalkRight", new Animation(Content.Load<Texture2D>("Player/WalkingRight"), 3) },
        })
        {
          //Position = new Vector2(100, 100),
          Position = H.ToVector2(H.heroPosition()),
          Input = new Input()
          {
            Up = Keys.W,
            Down = Keys.S,
            Left = Keys.A,
            Right = Keys.D,
          }
        }
      };
            
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
            /*
            KeyboardState state = Keyboard.GetState();
            // If they hit esc, exit
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _position.Y -= 3 ;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _position.Y += 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _position.X -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _position.X += 3;
            }
            */
            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

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

            spriteBatch.Begin();
            ReadLabyrinthSpec(V.labyrinthMatrix, C.LabyrinthPathName);
            FillLabyrinth(spriteBatch , _sprites);

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            //spriteBatch.Draw(V.hero , new Rectangle(H.heroPosition() + _position, C.PIXELSXPOINT) , Color.White); 
            spriteBatch.End();

            base.Draw(gameTime);

            
        }
    }
}
