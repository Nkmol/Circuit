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
        private const char _comma = ',';

        private LinkedList<LinkedListNode<Component>> _linked = new LinkedList<LinkedListNode<Component>>();

        private Dictionary<string, Component> variables = new Dictionary<string, Component>();
        private bool _startProbLinking = false;

        private readonly Dictionary<string, Func<Component>> _componentMapping = 
            new Dictionary<string, Func<Component>>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"Input", () => new Input()},
            {"Probe", () => new Probe() },
            {"OR", () => new OR()},
            {"NOT", () => new NOT()},
            {"AND", () => new AND() },
            {"NOR", () => new NOR() },
            {"NAND", () => new NAND() },
            {"XOR", () => new XOR() }
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
            variables.Add(varName, ParseComponent(assignValue));
        }

        private Component ParseComponent(string component)
        {
            var val = component.Split(_variableDelimeter);
            var compName = val[0];

            var test = _componentMapping[compName]();

            // Input definition is optional input
            if (val.Length >= 2)
            {
                var input = val[1];

                Console.WriteLine($"{compName} {input ?? "LOW"}");
                test.output = (Bit)Enum.Parse(typeof(Bit), input, true);
            }

            return test;
        }

        private void ParseLinkLine(string line)
        {
            Console.WriteLine("ParseLinkLine");
            //Console.WriteLine(line);

            var route = line.Split(_delimeter);
			
            var origin = route[0];
            var stops = route[1].Split(_comma);
            var component = variables[origin];

            LinkedListNode<Component> node = new LinkedListNode<Component>(component);

            if(_linked.Count() <= 0){
                _linked.AddFirst(node);
            } else {

                List<Component> components = new List<Component>();

                foreach (var textNode in stops)
                {
                    var cmp = variables[textNode];
                    components.Add(variables[textNode]);
                }

            }



            Console.WriteLine(origin);
            Console.WriteLine(variables[origin]);
        }
    }
}
