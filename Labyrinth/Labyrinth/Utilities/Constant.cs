using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Labyrinth
{
    public static class C // Constants
    {
        public static Point MAINWINDOW = new Point(960, 720);
        //public static Point WINDOWSOFFSET = new Point(50, 40);
        public static Point PIXELSXPOINT = new Point(40, 40); // quanti pixel voglio per ogni punto del labirinto

        public static string LabyrinthPathName = @"C:\Git\Computer-Graphics\Labyrinth\Labyrinth\Content\Labirinto.txt";

        public static Texture2D brickWall, brickGrass, brickStart, brickEnd, brickLava, brickDiamond, brickEnd2;
        public static Texture2D Grave;
        public static Texture2D cannon2_30, cannon3, cannon4_30 , cannon6 , cannon7_30, cannon9, cannon10_30, cannon12;
        public static Texture2D  bulletTexture;
        public static SpriteFont gameFont;              // prof
        public static SoundEffect explosion;            // prof
        public static SoundEffect newBullet;            // prof
        public static Song backMusic;                   // prof
                                                        // prof
        public const int STRTBULLETCOUNT = 1;          // prof
        public const int ADDBULLETTIME = 1;          // prof
                                                        // prof
        public static int BULLETWIDTH = 6;             // prof
        public static int BULLETHEIGHT = 6;            // prof
    }
}
