﻿namespace Models
{
    using System;
    using System.Collections.Generic;

    // TODO Board Builder maken
    public class BoardParser
    {
        private const char _delimeter = ':';
        private const char _endOfExp = ';';
        private const char _comment = '#';
        private const char _variableDelimeter = '_';
        private const char _addition = ',';

        public BoardBuilder BoardBuilder = new BoardBuilder();

        private readonly char[] _trimMap = {'\t', ' ', _endOfExp};

        private bool _startProbLinking;

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

//            Console.WriteLine($"{varName} {assignValue}");

            var component = ParseComponent(assignValue);
            component.Name = varName;

            BoardBuilder.addNode(varName, component);
        }

        private Component ParseComponent(string line)
        {
            var val = line.Split(_variableDelimeter);
            var compName = val[0];
            var component = BoardBuilder.Create(compName);


            // Input definition is optional input
            if (val.Length >= 2)
            {
                var input = val[1];

//                Console.WriteLine($"{compName} {input ?? "LOW"}");
                component.Output = (Bit) Enum.Parse(typeof(Bit), input, true);
            }

            return component;
        }

        private void ParseLinkLine(string line)
        {
			// Split assignment
			var val = line.Split(_delimeter);
            var assignTo = val[0];

            // Split different components and assign it as the next
            foreach (var componentName in val[1].Split(_addition))
            {
                BoardBuilder.Link(componentName, assignTo);
            }
        }
    }
}