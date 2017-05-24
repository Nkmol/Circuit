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
            Console.WriteLine(line);

            // Separate starting point from the points it will connect with
            var route = line.Split(_delimeter);
			
            // Save node into origin variable
            var origin = route[0];

            // Seperate and store the points it connect with
            var stops = route[1].Split(_comma);

            // Store origin and points into a dictionary
            Dictionary<string, Component> seperatedRoute = new Dictionary<string, Component>();
            var component = variables[origin];
            component.name = origin;
            seperatedRoute.Add(origin, component);

            foreach (var stop in stops)
            {
                variables[stop].name = stop;
                seperatedRoute.Add(stop, variables[stop]);
            }

            //Cin: NODE3,NODE7,NODE10

            Console.WriteLine(seperatedRoute);


            LinkedListNode<Component> startnode = new LinkedListNode<Component>(component);

            if(_linked.Count() <= 0){
                _linked.AddFirst(startnode);

                // First node should also be last node 
            } 

            List<Component> components = new List<Component>();

            foreach (var textNode in stops)
            {
                var cmp = variables[textNode];
                components.Add(variables[textNode]);
            }


            int count = 0;
            foreach (var com in components)
            {
				foreach (var node in _linked)
				{
                    var current = node;
                    Component nextCom = null;

                    if(components[count + 1] != null){
                        nextCom = components[count + 1];
					}

                    if(current.Next == null && nextCom != null) {

                        LinkedListNode<Component> nextNode = new LinkedListNode<Component>(nextCom);
                        //_linked.AddAfter(current, nextNode);
                        //_linked.AddAfter(current, nextNode);
                        //current.Next.Value = nextCom;
                    }
				}

                count++;
            }

            var result = _linked;

                // loop through the linkedlist till you hit last

           



            //Console.WriteLine(origin);
            //Console.WriteLine(variables[origin]);
        }
    }
}
