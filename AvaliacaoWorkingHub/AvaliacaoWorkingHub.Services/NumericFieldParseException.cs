using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaliacaoWorkingHub.Services
{
    public class NumericFieldParseException : FormatException
    {
        public NumericFieldParseException() { }

        public NumericFieldParseException(string message) : base(message) { }

        public NumericFieldParseException(string message, Exception inner) : base(message, inner) { }
    }
}
