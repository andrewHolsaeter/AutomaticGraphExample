using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using System.Xml;

namespace WebApplication1.Svg
{
    public interface ISvgElement
    {
        Color BackgroundColor { get; set; }
        Color BorderColor { get; set; }
        bool Dashed { get; set; }
        Color FontColor { get; set; }
        double Height { get; set; }
        double? StrokeDashArray { get; set; }
        double StrokeWidth { get; set; }
        double Width { get; set; }
        double X { get; set; }
        double Y { get; set; }

        void WriteTo(XmlWriter writer);
    }
}
