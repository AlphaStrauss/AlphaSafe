using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace AlphaSafe.UI.BasisFunctions
{
    public class ColorProvider
    {
        private static bool colorsCreated = false;

        // Colors
        private static Color white;
        private static Color lightBlue;
        private static Color blue;
        private static Color blueGrey;
        private static Color lightGrey;
        private static Color grey;
        private static Color darkGrey;
        private static Color backGrey;
        private static Color black;
        private static Color transparent;
        private static Color red;
        private static Color orange;

        private static void CreateColors()
        {
            if (colorsCreated)
                return;

            white = Color.White;
            lightBlue = Color.FromRgba(127, 227, 238, 255);
            blue = Color.FromRgba(3, 200, 221, 255);
            blueGrey = Color.FromRgba(211, 226, 226, 255);
            lightGrey = Color.FromRgba(225, 234, 234, 255);
            grey = Color.FromRgba(116, 122, 122, 255);
            darkGrey = Color.FromRgba(87, 91, 94, 255);
            backGrey = Color.FromRgba(48, 50, 52, 255);
            black = Color.FromRgba(47, 49, 51, 255);
            transparent = Color.FromRgba(0, 0, 0, 0);
            red = Color.Red;
            orange = Color.FromRgba(255, 128, 0, 255);

            colorsCreated = true;
        }

        public static Color White { get { CreateColors(); return white; } }
        public static Color LightBlue { get { CreateColors(); return lightBlue; } }
        public static Color Blue { get { CreateColors(); return blue; } }
        public static Color BlueGrey { get { CreateColors(); return blueGrey; } }
        public static Color LightGrey { get { CreateColors(); return lightGrey; } }
        public static Color Grey { get { CreateColors(); return grey; } }
        public static Color DarkGrey { get { CreateColors(); return darkGrey; } }
        public static Color BackGrey { get { CreateColors(); return backGrey; } }
        public static Color Black { get { CreateColors(); return black; } }
        public static Color Transparent { get { CreateColors(); return transparent; } }
        public static Color Red { get { CreateColors(); return red; } }
        public static Color Orange { get { CreateColors(); return orange; } }
    }
}
