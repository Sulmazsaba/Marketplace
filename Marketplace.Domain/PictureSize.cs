using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class PictureSize : Value<PictureSize>
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }

        public PictureSize(int width, int height)
        {
            if (Width <= 0)
                throw new ArgumentOutOfRangeException(nameof(Width), $"Picture Width Must be a positive number");
            if (Height <= 0)
                throw new ArgumentOutOfRangeException(nameof(Height), "Picture Height must be a positive number");
            Width = width;
            Height = height;

        }

        internal PictureSize()
        {
            
        }
    }
}
