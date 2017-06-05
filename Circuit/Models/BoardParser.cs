﻿﻿namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

            #region Early exit
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
            #endregion

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

            var componentProps = ParseComponent(assignValue);

            BoardBuilder.AddComponent(varName, componentProps[0], componentProps.ElementAtOrDefault(1));
        }

        private string[] ParseComponent(string line)
        {
            // [0] is component name, [1] is optional input value indication
            return line.Split(_variableDelimeter);
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