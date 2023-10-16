using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01
{
    class Scene
    {
        private string[,]_scene;
        private int _width;
        private int _height;
        public Scene(int height, int width)
        {

            this._height = height;
            this._width = width;
            _scene = new string[height, width];

        }

        private void Clear()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _scene[j, i] = "";
                }
            }
            Console.Clear();
        }
        public void Draw(params IDrawable[] drawables)
        {
            Clear();
            foreach (var drawable in drawables)
            {
                drawable.Draw(_scene);
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    stringBuilder.Append(_scene[i, j]);
                }
                stringBuilder.AppendLine();
            }
            Console.Write(stringBuilder.ToString());
        }
    }
}

