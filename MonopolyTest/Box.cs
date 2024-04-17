using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTest
{
    public class Box
    {
        public Box(DateTime bestBeforeDate, DateTime productionDate, double weight, double height, double depth, double width)
        {
            Id = new Guid();
            BestBeforeDate = bestBeforeDate;
            ProductionDate = productionDate;
            Weight = weight;
            Height = height;
            Depth = depth;
            Width = width;
            Volume = Height * Depth * Width;
        }

        public Box(DateTime productionDate, double weight, double height, double depth, double width)
        {
            Id = new Guid();
            ProductionDate = productionDate;
            BestBeforeDate = productionDate.AddDays(100);
            Weight = weight;
            Volume = Height * Depth * Width;
        }

        public Guid Id { get; set; }

        public double Weight { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Width { get; }
        public double Volume { get; }

        public DateTime BestBeforeDate { get; }

        public DateTime ProductionDate { get; }
    }
}