﻿﻿namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // TODO Board Builder maken
    public class BoardParser
    {
        private const char Delimeter = ':';
        private const char EndOfExp = ';';
        private const char Comment = '#';
        private const char VariableDelimeter = '_';
        private const char Addition = ',';

        private readonly char[] _trimMap = {'\t', ' ', EndOfExp};

        public bool StartProbLinking { get; private set; }

        private string Parse(string val)
        {
            #region clean value

            foreach (var c in _trimMap)
            {
                val = val.Replace(c.ToString(), string.Empty);
            }

            val = val.Trim();

            #endregion

            #region Early exit
            // Don't parse comment lines
            if (val.StartsWith(Comment.ToString()))
            {
                return string.Empty;
            }

            // Indicator that prob linking has completed
            if (val == string.Empty)
            {
                StartProbLinking = true;
                return string.Empty;
            }
            #endregion


            return val;
        }

        public VariableLine ParseVariableLine(string line)
        {
            line = Parse(line);
            if (line == string.Empty)
                return null;

            // Only split at first occurance
            var val = line.Split(new []{ Delimeter }, 2);
            var varName = val[0];
            var assignValue = val[1];

            // This function is only valuable in this function
            var (componentName, input) = ParseComponent(assignValue);
            (string componentName, string input) ParseComponent(string component)
            {
                var compProps = component.Split(VariableDelimeter);
                return (compProps[0], compProps.ElementAtOrDefault(1));
            }

            return new VariableLine(varName, componentName, input);
        }

        public LinkLine ParseLinkLine(string line)
        {
            line = Parse(line);
            if (line == string.Empty)
                return null;

            // Split assignment
            var val = line.Split(Delimeter);
            var assignTo = val[0];

            // Component linked to one ore more components
            return new LinkLine(assignTo, new List<string>(val[1].Split(Addition)));
        }

        #region Define DTOs (Data Transfer Objects)

        public class LinkLine
        {
            public string Varname;
            public IList<string> Values;

            public LinkLine(string varname, IList<string> values)
            {
                Varname = varname;
                Values = values;
            }
        }

        public class VariableLine
        {
            public string Varname;
            public string Compname;
            public string Input;

            public VariableLine(string varname, string compname, string input)
            {
                Varname = varname;
                Compname = compname;
                Input = input;
            }
        }

        #endregion
    }
}