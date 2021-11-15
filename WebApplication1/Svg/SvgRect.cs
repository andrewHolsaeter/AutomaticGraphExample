using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using System.Xml;

namespace WebApplication1.Svg
{
    public class SvgRect : SvgElement
    {

        public override void WriteTo(XmlWriter writer)
        {
            writer.WriteStartElement("rect");
            writer.WriteAttribute("x", X);
            writer.WriteAttribute("y", Y);
            writer.WriteAttribute("width", Width);
            writer.WriteAttribute("height", Height);
            writer.WriteAttribute("stroke", BorderColor);
            writer.WriteAttribute("fill", BackgroundColor);

            if (this.StrokeDashArray != null)
            {
                writer.WriteAttribute("stroke-dasharray", StrokeDashArray);
            }

            writer.WriteEndElement();
        }

    }
}
