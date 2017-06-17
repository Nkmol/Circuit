using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Helpers.DGML
{
    public class DgmlWriter
    {
        public DgmlWriter()
        {
            Nodes = new List<Node>();
            Links = new List<Link>();
        }

        public List<Node> Nodes { get; set; }
        public List<Link> Links { get; set; }
        public static string Extension { get; } = ".dgml";

        public void AddNode(Node n)
        {
            Nodes.Add(n);
        }

        public void AddLink(Link l)
        {
            Links.Add(l);
        }

        public void Serialize(string xmlpath)
        {
            var g = new Graph();
            g.Nodes = Nodes.ToArray();
            g.Links = Links.ToArray();

            var root = new XmlRootAttribute("DirectedGraph");
            root.Namespace = "http://schemas.microsoft.com/vs/2009/dgml";
            var serializer = new XmlSerializer(typeof(Graph), root);

            var settings = new XmlWriterSettings();
            settings.Indent = true;

            var xmlWriter = XmlWriter.Create(xmlpath, settings);
            serializer.Serialize(xmlWriter, g);

            xmlWriter.Close();
        }

        public struct Graph
        {
            public Node[] Nodes;
            public Link[] Links;
        }

        public struct Node
        {
            [XmlAttribute] public string Id;
            [XmlAttribute] public string Label;

            public Node(string id, string label)
            {
                Id = id;
                Label = label;
            }
        }

        public struct Link
        {
            [XmlAttribute] public string Source;
            [XmlAttribute] public string Target;
            [XmlAttribute] public string Label;

            public Link(string source, string target, string label)
            {
                Source = source;
                Target = target;
                Label = label;
            }
        }
    }
}