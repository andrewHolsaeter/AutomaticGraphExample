﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using WebApplication1.Svg;
using Microsoft.AspNetCore.Html;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public HtmlString SvgDomElement { get; set; }

        public Graph drawingGraph { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            drawingGraph = new Graph();

            drawingGraph.AddNode(new ComponentNode("Foo"));
            drawingGraph.AddNode(new ComponentNode("Bar", "Bar Component", "[Azure Functions]", "This is the Bar component, really really important!"));
            drawingGraph.AddNode(new ComponentNode("Component01", "First Component", null, "Bizar"));
            drawingGraph.AddNode(new ComponentNode("Component02"));
            drawingGraph.AddNode(new LabeledNode("Component05", new System.Collections.Generic.List<string> { "Component Nr. 5" }));
            drawingGraph.AddNode(new ComponentNode("Component06"));
            drawingGraph.AddNode(new ComponentNode("Component08"));

            drawingGraph.AddNode(new ComponentNode("Component03"));
            drawingGraph.AddNode(new ComponentNode("Component04"));
            drawingGraph.AddNode(new ComponentNode("Component07"));

            var subGraph = new Subgraph("Section01");
            subGraph.AddNode(drawingGraph.FindNode("Component02"));
            subGraph.AddNode(drawingGraph.FindNode("Component07"));
            subGraph.AddNode(drawingGraph.FindNode("Bar"));
            subGraph.AddNode(drawingGraph.FindNode("Component01"));
            drawingGraph.RootSubgraph.AddSubgraph(subGraph);

            var labels = new List<Svg.Label> {
                new Svg.Label("Component Nr. 9") {
                    Font = new System.Drawing.Font("Consolas", 20f, System.Drawing.FontStyle.Bold),
                    Color = Color.Azure
                },
                new Svg.Label("This is the description. It can become quite large and if all goes to plan; it will wrap itself.") {
                    Color = Color.MediumOrchid
                }
            };
            var labeledNode = new LabeledNode("Component09", labels);
            labeledNode.SvgElement.BackgroundColor = Color.Maroon;
            drawingGraph.AddNode(labeledNode);

            drawingGraph.AddEdge("Foo", "Something", "Bar");
            drawingGraph.AddEdge("Bar", "Bar_Component01", "Component01");
            drawingGraph.AddEdge("Bar", "Bar_Component02", "Component02");
            drawingGraph.AddEdge("Component01", "Bar_Component01", "Component03");
            drawingGraph.AddEdge("Component01", "Bar_Component01", "Component02");
            drawingGraph.AddEdge("Component02", "Bar_Component01", "Component04");
            drawingGraph.AddEdge("Component02", "Bar_Component01", "Component05");
            drawingGraph.AddEdge("Component02", "Bar_Component01", "Component06");
            drawingGraph.AddEdge("Component02", "Bar_Component01", "Component07");
            drawingGraph.AddEdge("Component02", "Bar_Component01", "Component08");
            drawingGraph.AddEdge("Component03", "Bar_Component01", "Component09");


            var doc = new Diagram(drawingGraph);
            doc.Run();
            //System.Console.WriteLine(doc.ToString());
            //TextCopy.ClipboardService.SetText(doc.ToString());

            SvgDomElement = new HtmlString(doc.ToString());
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public ActionResult OnPostSend(string subject_id)
        {
            var time = DateTime.Now.ToString();
            drawingGraph.AddNode(new ComponentNode(time));
            
            if (subject_id != null)
            {
                // Add edge/connection if subject id is passed in
                drawingGraph.AddEdge(subject_id, "New Node", time);
            }
            
            var doc = new Diagram(drawingGraph);
            doc.Run();
            SvgDomElement = new HtmlString(doc.ToString());

            return Content(SvgDomElement.Value);
        }
    }
}
