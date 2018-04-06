using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL.Excepciones {

    public sealed class BloqueoException : ApplicationException {

        public BloqueoException ( ) : base ("Accion no valida, elemento bloqueado") {
        }

        public BloqueoException (Exception innerException) : base ("Accion no valida, elemento bloqueado", innerException) {
        }
    }
}