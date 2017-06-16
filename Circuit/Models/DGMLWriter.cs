﻿namespace Models
{
    using System.Collections.Generic;
    using System.IO;
    using System.Security.AccessControl;
    using System.Xml;
    using System.Xml.Serialization;

    public class DGMLWriter
    {
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

        public List<Node> Nodes { get; protected set; }
        public List<Link> Links { get; protected set; }

        public DGMLWriter()
        {
            Nodes = new List<Node>();
            Links = new List<Link>();
        }

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

            var xmlWriter = XmlWriter.Create($"{xmlpath}\\file.dgml", settings);
            serializer.Serialize(xmlWriter, g);

            xmlWriter.Close();
        }
    }
}