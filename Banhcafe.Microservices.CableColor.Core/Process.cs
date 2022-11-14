using Banhcafe.Microservices.CableColor.Infraestructure;
using System.Collections;
using System.Net;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using static Banhcafe.Microservices.CableColor.Infraestructure.Model.conectionModel;
namespace Banhcafe.Microservices.CableColor.Core
{
    public class Process
    {



        private string urlCableColor { get; set; }
        private string usuarioCableColor { get; set; }
        private string claveCableColor { get; set; }
        private string IdentificadorCableColor { get; set; }
        private string EstructuraXmlConsultaCliente { get; set; }
        private string EstructuraXmlConsultaIdentidad { get; set; }
        private string EstructuraXmlConsultaTelefono { get; set; }
        private string EstructuraXmlAplicarPago { get; set; }
        private string EstructuraXmlReversarPago { get; set; }


        public cableColor consultaXCliente(transaccion objTransaccion, string tipoSaldo)
        {

            string xmlDeEntrada = EstructuraXmlConsultaCliente.Replace("parametro1", objTransaccion.contrato);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro2", tipoSaldo);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro3", usuarioCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro4", claveCableColor);

            var response = InvokeService(xmlDeEntrada);

            cableColor respuesta = descomponeXmlConsultaCliente(response);
            respuesta.identificador_unico_consulta = objTransaccion.identificador_unico_transaccion;
            respuesta.mensajeConsulta = "Consulta Existosa";
            respuesta.tipoSaldo = tipoSaldo;

            objTransaccion.identificador_unico_consulta = objTransaccion.identificador_unico_transaccion;
            objTransaccion.monto = Double.Parse(respuesta.saldo, System.Globalization.CultureInfo.InvariantCulture);

            if (respuesta.codigoResultado == "00")
            {
                objTransaccion.respuesta = "Consulta exitosa";
              //  objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.CONSULTADA;

            }
            else
            {
                objTransaccion.respuesta = respuesta.mensajeResultado;
               // objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.NOPENDIENTES;
            }


           // guardar(respuesta);
            return respuesta;
        }


        public cableColor consultaXIdentidad(transaccion objTransaccion)
        {

            string xmlDeEntrada = EstructuraXmlConsultaIdentidad.Replace("parametro1", objTransaccion.contrato);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro2", usuarioCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro3", claveCableColor);

            var response = InvokeService(xmlDeEntrada);
            var respuesta = descomponeXmlConsultaIdentidad(response);
            if (respuesta.codigoResultado == "00")
            {
                objTransaccion.respuesta = "Consulta exitosa";
              //  objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.CONSULTADA;

            }
            else
            {
                objTransaccion.respuesta = respuesta.mensajeResultado;
              //  objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.NOPENDIENTES;
            }

            return respuesta;
        }

        public List<cableColor> consultaXTelefono(transaccion objTransaccion)
        {

            string xmlDeEntrada = EstructuraXmlConsultaTelefono.Replace("parametro1", objTransaccion.contrato);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro2", usuarioCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro3", claveCableColor);

            var response = InvokeService(xmlDeEntrada);
            var respuesta = descomponeXmlConsultaTelefono(response);
            return respuesta;
        }

        public cableColor pagoCableColor(transaccion objTransaccion, cableColor cableColor)
        {
            cableColor.cajero = objTransaccion.cajero;
            cableColor.fecha = DateTime.Now.ToString("yyyyMMdd");
            cableColor.hora = DateTime.Now.ToString("HHmmss");
            cableColor.codAgencia = objTransaccion.sucursal.ToString();


            string xmlDeEntrada = EstructuraXmlAplicarPago.Replace("parametro1", cableColor.codCliente);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro2", cableColor.tipoSaldo);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro3", cableColor.saldo);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro4", cableColor.cajero);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro5", cableColor.fecha);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro6", cableColor.hora);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro7", cableColor.codAgencia);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro8", objTransaccion.identificador_unico_transaccion);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro9", IdentificadorCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametros10", usuarioCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametros11", claveCableColor);

            var response = InvokeService(xmlDeEntrada);
            var respuesta = descomponeXmlPago(response, cableColor);

            objTransaccion.identificador_unico_pago = objTransaccion.identificador_unico_transaccion;
           // objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.PAGADA;
            objTransaccion.respuesta = "Pagado con exito";

            cableColor.identificador_unico_pago = objTransaccion.identificador_unico_pago;
            cableColor.mensajePago = "Pago Exitoso";
            cableColor.refBanco = objTransaccion.identificador_unico_transaccion;
            cableColor.identificadorBanco = IdentificadorCableColor;

            cableColor.codigoResultado = respuesta.codigoResultado;
            cableColor.mensajeResultado = respuesta.mensajeResultado;
            cableColor.codCliente = respuesta.codCliente;
            cableColor.numReferencia = respuesta.numReferencia;
            cableColor.facts = respuesta.facts;
            cableColor.dets = respuesta.dets;

            if (cableColor.codigoResultado != "00")
            {
               // objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.ERROR;
                objTransaccion.respuesta = "Error en el pago";
            }

         //   ActualizarIdentificadorConsulta(cableColor);
            return cableColor;
        }
        public cableColor reversionCableColor(transaccion objTransaccion, cableColor cableColor)
        {

            string xmlDeEntrada = EstructuraXmlReversarPago.Replace("parametro1", objTransaccion.contrato);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro2", cableColor.numReferencia);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro3", cableColor.cajero);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro4", cableColor.fecha);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro5", cableColor.hora);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro6", cableColor.codAgencia);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro7", cableColor.refBanco);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro8", IdentificadorCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametro9", usuarioCableColor);
            xmlDeEntrada = xmlDeEntrada.Replace("parametros10", claveCableColor);

            var response = InvokeService(xmlDeEntrada);
            var respuesta = descomponeXmlReversion(response, cableColor);

        //    objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.REVERSADA;
            objTransaccion.respuesta = "Reversado con exito";

            cableColor.identificador_unico_reversion = objTransaccion.identificador_unico_transaccion;
            cableColor.mensajeReversion = "Reversion Exitosa";
            cableColor.codigoResultado = respuesta.codigoResultado;



            if (respuesta.codigoResultado != "00")
            {
            //    objTransaccion.id_estado = (int)BaseHelper.EstadoTransaccion.ERROR;
                objTransaccion.respuesta = respuesta.mensajeResultado;
            }

        //    ActualizarIdentificadorPago(cableColor);
            return respuesta;
        }

        public  void inicializarConfiguracion()
        {
            request request = new();

            request.scheme = "dbo";
            request.parameters = new Dictionary<string, object>()
            {
                { "id",12038 }
            };
            request.dataBase = "Integrador";
            request.storedProcedure = "SP_CONFIG_INTEGRADOR";

            var result = conexionBaseDatos.consumir(request);


            var config = JsonSerializer.Deserialize<List<configuraciones>>(result);


            request.scheme = "dbo";
            request.parameters = new Dictionary<string, object>(){};
            request.dataBase = "Integrador";
            request.storedProcedure = "SP_CABLECOLOR_XML_INTEGRADOR";

            result = conexionBaseDatos.consumir(request);

            var xml = JsonSerializer.Deserialize<List<estructuraXml>>(result);

            foreach (var item in config)
            {
                if (item.descripcion== "urlCableColor")
                {
                    urlCableColor = item.valor;
                }

                if (item.descripcion == "usuarioCableColor")
                {
                    usuarioCableColor = item.valor;
                }
                if (item.descripcion == "claveCableColor")
                {
                    claveCableColor = item.valor;
                }
                if (item.descripcion == "IdentificadorCableColor")
                {
                    IdentificadorCableColor = item.valor;
                }
            }

            foreach (var item in xml)
            {
                if (item.metodo == "consultaCliente")
                {
                    EstructuraXmlConsultaCliente = item.estructuraEntrada;
                }

                if (item.metodo == "consultaIdentidad")
                {
                    EstructuraXmlConsultaIdentidad = item.estructuraEntrada;
                }
                if (item.metodo == "consultaTelefono")
                {
                    EstructuraXmlConsultaTelefono = item.estructuraEntrada;
                }
                if (item.metodo == "aplicarPago")
                {
                    EstructuraXmlAplicarPago = item.estructuraEntrada;
                }
                if (item.metodo == "reversarPago")
                {
                    EstructuraXmlReversarPago = item.estructuraEntrada;
                }
            }


        }


        public  string InvokeService(string estructura)
        {
            string respuesta = "";
            try
            {
                //Calling CreateSOAPWebRequest method
                HttpWebRequest request = CreateSOAPWebRequest();

                XmlDocument SOAPReqBody = new XmlDocument();
                //SOAP Body Request

                var xml = estructura;


                SOAPReqBody.LoadXml(xml);

                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                //Geting response from request
                using (WebResponse Serviceres = request.GetResponse())
                {

                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        //reading stream
                        var ServiceResult = rd.ReadToEnd();
                        respuesta = ServiceResult;

                    }
                }

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(urlCableColor);
            //SOAPAction
            Req.Headers.Add(@"SOAPAction:http://tempuri.org/Addition");
            //Content_type
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method
            Req.Method = "POST";
            //return HttpWebRequest
            return Req;
        }


        public static cableColor descomponeXmlConsultaCliente(string xml)
        {
            XDocument xmlDocument = XDocument.Parse(xml);
            cableColor respuesta = new cableColor();

            respuesta = xmlDocument.Root
       .Descendants("consultaClienteResult")
       .Select(node => new cableColor
       {
           codigoResultado = node.Element("codigoResultado").Value,
           mensajeResultado = node.Element("mensajeResultado").Value,
           codCliente = node.Element("codCliente").Value,
           nombreCliente = node.Element("nombreCliente").Value,
           telefonos = node.Element("telefonos").Value,
           saldo = node.Element("saldo").Value
       })
       .SingleOrDefault();

            return respuesta;
        }


        public cableColor descomponeXmlConsultaIdentidad(string xml)
        {
            XDocument xmlDocument = XDocument.Parse(xml);
            cableColor respuesta = new cableColor();
            respuesta = xmlDocument.Root
       .Descendants("consultaIdentidadResult")
       .Select(node => new cableColor
       {
           codigoResultado = node.Element("codigoResultado").Value,
           mensajeResultado = node.Element("mensajeResultado").Value,
           codCliente = node.Element("codCliente").Value,
           nombreCliente = node.Element("nombreCliente").Value,
           saldo = node.Element("saldo").Value
       })
       .SingleOrDefault();

            return respuesta;
        }


        public List<cableColor> descomponeXmlConsultaTelefono(string xml)
        {
            XDocument xmlDocument = XDocument.Parse(xml);
            cableColor respuesta = new cableColor();

            respuesta = xmlDocument.Root
       .Descendants("consultaTelefonoResult")
       .Select(node => new cableColor
       {
           codigoResultado = node.Element("codigoResultado").Value,
           mensajeResultado = node.Element("mensajeResultado").Value,

       })
       .SingleOrDefault();


            var prueba = xmlDocument.Root
       .Descendants("item")
       .Select(node => new cableColor
       {
           codigoResultado = respuesta.codigoResultado,
           mensajeResultado = respuesta.mensajeResultado,
           codCliente = node.Element("codCliente").Value,
           nombreCliente = node.Element("nombreCliente").Value

       })
       .ToList();

            return prueba;
        }

        public cableColor descomponeXmlPago(string xml, cableColor cableColor)
        {
            XDocument xmlDocument = XDocument.Parse(xml);

            cableColor = xmlDocument.Root
       .Descendants("aplicarPagoResult")
       .Select(node => new cableColor
       {
           codigoResultado = node.Element("codigoResultado").Value,
           mensajeResultado = node.Element("mensajeResultado").Value,
           codCliente = node.Element("codCliente").Value,
           numReferencia = node.Element("numReferencia").Value,
           facts = node.Element("facts").Value,
           dets = node.Element("dets").Value,


       })
       .SingleOrDefault();

            return cableColor;
        }



        public cableColor descomponeXmlReversion(string xml, cableColor cableColor)
        {
            XDocument xmlDocument = XDocument.Parse(xml);

            cableColor = xmlDocument.Root
       .Descendants("reversarPagoResult")
       .Select(node => new cableColor
       {
           codigoResultado = node.Element("codigoResultado").Value,
           mensajeResultado = node.Element("mensajeResultado").Value,
           codCliente = node.Element("codCliente").Value

       })
       .SingleOrDefault();

            return cableColor;
        }

    }
}