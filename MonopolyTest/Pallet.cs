using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTest
{
    public class Pallet
    {
        public Pallet(double weight, double height, double depth, double width, List<Box> boxes) 
        {
            Id = new Guid();
            Weight = weight;
            Height = height;
            Depth = depth;
            Width = width;

            foreach (Box box in boxes)
            {
                if (box.Width > Width || box.Depth > Depth)
                    continue;
                Boxes.Add(box);
            }

            Volume = Height * Width * Depth + Boxes.Sum(x => x.Volume);

            BestBeforeDate = Boxes.Min(x => x.BestBeforeDate);
        }

        public Guid Id { get; }
        public double Weight { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Width { get; }
        public double Volume { get; }
        public DateTime BestBeforeDate { get; }
        public List<Box> Boxes { get; } = new List<Box>();
    }
}