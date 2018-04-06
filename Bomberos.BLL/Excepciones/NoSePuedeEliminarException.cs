using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL.Excepciones {

    public sealed class NoSePuedeEliminarException : ApplicationException {

        public NoSePuedeEliminarException (String message) : base ("No se puede eliminar " + message) {
        }

        public NoSePuedeEliminarException (String message, Exception innerException) : base ("No se puede eliminar " + message, innerException) {
        }
    }
}