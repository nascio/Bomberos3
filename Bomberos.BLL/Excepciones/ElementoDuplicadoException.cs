using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL.Excepciones {

    public sealed class ElementoDuplicadoException : ApplicationException {

        public ElementoDuplicadoException (String message) : base (message) {
        }

        public ElementoDuplicadoException (Exception innerException) : base ("El elemento esta duplicado.", innerException) {
        }
    }
}