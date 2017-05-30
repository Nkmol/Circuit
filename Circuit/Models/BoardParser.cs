using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    using System.Security.Policy;
    using System.Xml;

    public class BoardParser
    {
        private const char _delimeter = ':';
        private const char _endOfExp = ';';
        private const char _comment = '#';
        private const char _variableDelimeter = '_';
        private const char _addition = ',';

        // Create new graph
        DirectGraph _linked = new DirectGraph();

        private Dictionary<string, Component> variables = new Dictionary<string, Component>();
        private bool _startProbLinking = false;

        private readonly Dictionary<string, Func<Component>> _componentMapping =
            new Dictionary<string, Func<Component>>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"Input", () => new Input()},
                {"Probe", () => new Probe()},
                {"OR", () => new OR()},
                {"NOT", () => new NOT()},
                {"AND", () => new AND()},
                {"NOR", () => new NOR()},
                {"NAND", () => new NAND()},
                {"XOR", () => new XOR()}
            };

        private readonly char[] _trimMap = new[] {'\t', ' ', _endOfExp};

        public void Parse(string val)
        {
            #region clean value

            foreach (var c in _trimMap)
            {
                val = val.Replace(c.ToString(), string.Empty);
            }

            #endregion

            // Don't parse comment lines
            if (val.StartsWith(_comment.ToString()))
            {
                return;
            }

            // Indicator that prob linking has completed
            if (val == string.Empty)
            {
                _startProbLinking = true;
                return;
            }

            if (_startProbLinking)
            {
                ParseLinkLine(val);
            }
            else
            {
                ParseVariableLine(val);
            }
        }

        private void ParseVariableLine(string line)
        {
            line = line.Trim();

            var val = line.Split(_delimeter);
            var varName = val[0];
            var assignValue = val[1];

            Console.WriteLine($"{varName} {assignValue}");

            var component = ParseComponent(assignValue);
            component.name = varName;

            variables.Add(varName, component);
        }

        private Component ParseComponent(string line)
        {
            var val = line.Split(_variableDelimeter);
            var compName = val[0];

            var component = _componentMapping[compName]();

            // Input definition is optional input
            if (val.Length >= 2)
            {
                var input = val[1];

                Console.WriteLine($"{compName} {input ?? "LOW"}");
                component.output = (Bit) Enum.Parse(typeof(Bit), input, true);
            }

            return component;
        }

        public Dictionary<string, GraphNode> nodes = null;
        private void ParseLinkLine(string line)
        {
            // Make every node into a 
            // TODO only happen once
            if(nodes == null) nodes = variables.ToDictionary(x => x.Key, x => new GraphNode(x.Value));

            // Split assignment
            var val = line.Split(_delimeter);
            var AssignTo = val[0];

            // Split different components and assign it as the next
            foreach (var componentName in val[1].Split(_addition))
            {
                // TODO make DirectGraph function for this
                var component = nodes[componentName];
                var componentAssign = nodes[AssignTo];

                componentAssign.Next.Add(component);
                component.Previous.Add(componentAssign);
            }
        }
    }
}
