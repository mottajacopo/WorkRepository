using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Models;
using Labyrinth.Sprites;
using Labyrinth.Manager;

namespace Labyrinth.Sprites
{
    public class Bullets : Microsoft.Xna.Framework.DrawableGameComponent //non viene gestito da game, ma si gestisce automaticamente come se fosse un altro game
    {
        protected Texture2D texture;
        protected Rectangle spriteRectangle;
        protected Point position;
        protected int YSpeed;
        protected int XSpeed;
        protected Random random;
        protected SpriteBatch sBatch;

        public Bullets(Game game, ref Texture2D theTexture) : base(game)
        {
            this.texture = theTexture;
            this.position = new Point();

            sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            spriteRectangle = new Rectangle(0, 0, C.BULLETWIDTH, C.BULLETHEIGHT);

            random = new Random(this.GetHashCode());

            PutInStartPosition();
        }
        public void PutInStartPosition()
        {
            position.X = random.Next(C.MAINWINDOW.X - C.BULLETWIDTH - C.PIXELSXPOINT.X);
            position.Y = random.Next(C.MAINWINDOW.Y - C.BULLETWIDTH - C.PIXELSXPOINT.Y);

            XSpeed = -1 - random.Next(9);
            YSpeed = random.Next(3) - 1;
        }
        public override void Draw(GameTime gameTime)
        {
            sBatch.Draw(texture, new Rectangle(position, new Point(C.BULLETWIDTH, C.BULLETHEIGHT)), Color.White);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (V.labyrinthPixels.Intersects(new Rectangle(position.X, position.Y, C.BULLETWIDTH, C.BULLETHEIGHT)))
            {
                for (int i = 0; i < V.labyrinthMatrixRows; i++)
                {
                    for (int j = 0; j < V.labyrinthMatrixColumns; j++)
                    {
                        if (V.labyrinthMatrix[i, j] == '1' || V.labyrinthMatrix[i, j] == 'I')
                        {
                            if (new Rectangle(new Point(i, j), C.PIXELSXPOINT).Intersects(new Rectangle(position.X, position.Y, C.BULLETWIDTH, C.BULLETHEIGHT)))
                            {
                                PutInStartPosition();
                            }
                        }
                    }
                }
            }
            position.X += XSpeed;
            position.Y += YSpeed;
            base.Update(gameTime);
        }
    }
}

