using System;
using System.Linq;

namespace Models
{
    public class BoardParser
    {
        private const char Delimeter = ':';
        private const char EndOfExp = ';';
        private const char Comment = '#';
        private const char VariableDelimeter = '_';
        private const char Addition = ',';

        private readonly char[] _trimMap = {'\t', EndOfExp};

        public bool StartProbLinking { get; private set; }

        public string[] Parse(string val)
        {
            if (val == null)
                return null;

            #region clean value

            foreach (var c in _trimMap)
                val = val.Replace(c.ToString(), string.Empty);

            val = val.Trim(' ');
            #endregion

            #region Early exit

            // Don't parse comment lines
            if (val.StartsWith(Comment.ToString()))
                return null;

            #endregion

            // Indicator that prob linking has completed
            if (val == Environment.NewLine || val == string.Empty)
            {
                StartProbLinking = true;
                return null;
            }

            // Parse line based on flag
            string[] result;
            if (StartProbLinking)
                result = ParseLinkLine(val);
            else
                result = ParseVariableLine(val);

            return result.Select(x => x?.Trim()).ToArray();
        }

        private string[] ParseVariableLine(string line)
        {
            // Only split at first occurance
            var val = line.Split(new[] {Delimeter}, 2);
            var varName = val[0];
            var assignValue = val[1];

            // This function is only valuable in this function
            var (componentName, input) = ParseComponent(assignValue);

            (string componentName, string input) ParseComponent(string component)
            {
                var compProps = component.Split(VariableDelimeter);
                return (compProps[0], compProps.ElementAtOrDefault(1));
            }

            return new[] {varName, componentName, input};
        }

        private string[] ParseLinkLine(string line)
        {
            // Split assignment
            var val = line.Split(Delimeter);
            var assignTo = val[0];

            // Component linked to one ore more components
            return new[] {assignTo}.Concat(val[1].Split(Addition)).ToArray();
        }
    }
}