using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banhcafe.Microservices.CableColor.Infraestructure.Model
{
    public class conectionModel
    {
        public class request
        {
            public string scheme { get; set; }
            public string dataBase { get; set; }
            public string storedProcedure { get; set; }
            public object parameters { get; set; }
        }

        public partial  class configuraciones
        {
            public string? descripcion { get; set; }
            public string? valor { get; set; }

        }

        public class estructuraXml{
            public string metodo { get; set; }
            public string estructuraEntrada { get; set; }

        }

        public partial class cableColor
        {
            public int id { get; set; }
            public string codCliente { get; set; }
            public string tipoSaldo { get; set; }
            public string codigoResultado { get; set; }
            public string mensajeResultado { get; set; }
            public string nombreCliente { get; set; }
            public string telefonos { get; set; }
            public string saldo { get; set; }
            public string cajero { get; set; }
            public string fecha { get; set; }
            public string hora { get; set; }
            public string codAgencia { get; set; }
            public string refBanco { get; set; }
            public string identificadorBanco { get; set; }
            public string numReferencia { get; set; }
            public string facts { get; set; }
            public string dets { get; set; }
            public string identificador_unico_consulta { get; set; }
            public string identificador_unico_pago { get; set; }
            public string identificador_unico_reversion { get; set; }
            public string mensajeConsulta { get; set; }
            public string mensajePago { get; set; }
            public string mensajeReversion { get; set; }
            public Nullable<System.DateTime> fechaTransaccion { get; set; }
        }
        public partial class transaccion
        {
            public int id { get; set; }
            public System.DateTime fecha { get; set; }
            public System.TimeSpan hora { get; set; }
            public Nullable<int> id_transaccion { get; set; }
            public Nullable<decimal> codigo_cliente { get; set; }
            public Nullable<decimal> codigo_cuenta { get; set; }
            public Nullable<decimal> id_moneda { get; set; }
            public string contrato { get; set; }
            public double monto { get; set; }
            public Nullable<int> id_movimiento { get; set; }
            public int id_estado { get; set; }
            public string identificador_unico_transaccion { get; set; }
            public Nullable<int> id_plataforma { get; set; }
            public string cajero { get; set; }
            public string respuesta { get; set; }
            public string identificador_unico_consulta { get; set; }
            public string identificador_unico_pago { get; set; }
            public string mensaje_error { get; set; }
            public Nullable<int> sucursal { get; set; }
            public string referencia_origen_transaccion { get; set; }
            public string contrato_b { get; set; }
            public Nullable<int> region { get; set; }
            public Nullable<int> banco { get; set; }
            public Nullable<System.DateTime> fecha_core { get; set; }
            public Nullable<int> agencia { get; set; }
        }
    }
}
