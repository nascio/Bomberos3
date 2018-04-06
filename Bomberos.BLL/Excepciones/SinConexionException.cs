using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bomberos.BLL.Excepciones {

    public sealed class SinConexionException : ApplicationException {

        public SinConexionException ( ) : base ("No se puede conectar al servidor.") {
        }

        public SinConexionException (Exception innerException) : base ("String message", innerException) {
        }
    }
}