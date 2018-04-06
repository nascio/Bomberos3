using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL.Excepciones {
    public sealed class NoSePudoIngresarException : ApplicationException {
        public NoSePudoIngresarException (String message) : base ( ) {
        }
    }


    public sealed class AlgoMasException : ApplicationException {
        public AlgoMasException ( ) : base ( ) {
        }
    }
}
