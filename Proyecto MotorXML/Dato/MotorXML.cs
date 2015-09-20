using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Reflection;
using System.Runtime.Remoting;
using System.Collections;
using System.IO;

/// <summary>
/// Summary description for MotorXML
/// </summary>
/// 
namespace Dato
{
    public class MotorXML
    {
        private XmlDocument XDoc { get; set; }
        
        private static String strRutaActual;      

        public String RutaActual
        {
            get { return MotorXML.strRutaActual; }            
        }

        private static String strRutaFotos;

        public String RutaFotos
        {
            get { return MotorXML.strRutaFotos; }
        }
        
        private static MotorXML Instancia;


        #region Constructores - singleton
        /// <summary>
        /// Patrons singleton
        /// </summary>
        private MotorXML()
        {
            XDoc = new XmlDocument();
        }
        private MotorXML(string strRuta)
        {
            XDoc = new XmlDocument();
            CargarXMLArchivo(strRuta);
        }

      /// <summary>
        /// Devuelve la única instancia activa
        /// en el caso de no existir la crea
        /// Patrons singleton
        ///  
      /// </summary>
      /// <param name="strRuta">opcional, que permite cargar otro archivo distinto al generado por la clase</param>
      /// <returns></returns>
        public static MotorXML getIntancia(String strRuta="")
        {
            try
            {
                Init();
                if (Instancia == null)
                {
                    Instancia = new MotorXML(strRutaActual);
                    // MotorXML.strRutaActual = strRuta;
                }
                else
                {
                        if (strRuta != "" && !strRuta.Equals(MotorXML.strRutaActual))
                        {
                            Instancia = new MotorXML(strRuta);
                            // MotorXML.strRutaActual = strRuta;
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Instancia;
        }

        #endregion

        #region LoadSave
        /// <summary>
        /// Lee Archivo xml y lo carga en propiedad XDoc, la que mantiene el documento XML
        /// </summary>
        /// <param name="ruta">Ruta al archivo xml</param>
        private void CargarXMLArchivo(string ruta)
        {
            try
            {
                XDoc.Load(ruta);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        /// <summary>
        /// Almacena los cambios realizados en el documento en la ruta especificada
        /// </summary>
        /// <param name="ruta">Ruta al Archivo XML</param>
        private void GuardarXMLArchivo(string ruta)
        {
            try
            {
                XDoc.Save(ruta);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Almacena los cambios realizados en el documento en la ruta actual del documento.
        /// </summary>
        /// 
        public void GuardarXMLArchivo()
        {
            try
            {
                XDoc.Save(strRutaActual);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        private XmlNode ConstruirNodoXML(object o, XmlNode Abuelo)
        {

            try
            {
                Type tp = o.GetType();
                BindingFlags flags = BindingFlags.Instance
                   | BindingFlags.Public
                   | BindingFlags.DeclaredOnly
                   | BindingFlags.Static;
                PropertyInfo[] pr = tp.GetProperties(flags);
                XmlNode padre = XDoc.CreateElement(tp.Name);
                foreach (PropertyInfo m in pr)
                {
                    XmlElement xele = XDoc.CreateElement(m.Name);
                    xele.InnerText = m.GetValue(o, null).ToString();
                    padre.AppendChild(xele);
                    //(((PropertyInfo)pr[3]).GetValue(o,null).GetType().BaseType).FullName=="System.Array"
                }
                Abuelo.AppendChild(padre);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Abuelo;
        }       
        /// <summary>
       /// Agrega objeto al Archivo XML cargado anteriormente.
       /// </summary>
       /// <param name="o">objeto a almacenar</param>
       /// <param name="strPropiedadIdentidad">Cadena de texto con el nombre de la propiedad del objeto que es utilizado como identidad o codigo</param>
       /// <param name="strAtributoIdentidad">Cadena de texto con el nombre del Atributo del nodo Datos que posee y almacena la ultima identidad creada</param>
        public void AgregarObjeto(Object o,string strPropiedadIdentidad, string strAtributoIdentidad)
        {
            try
            {
                XmlNode padre = XDoc.SelectSingleNode("Datos");
                ConstruirNodoXML(o, padre);
                XDoc.SelectSingleNode("Datos").Attributes[strAtributoIdentidad].InnerText = padre.LastChild.SelectSingleNode(strPropiedadIdentidad).InnerText;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Busca un objeto en el archivo xml cargado previamente.
        /// </summary>
        /// <param name="strValorBuscado">Cadena de texto con el valor del campo buscado</param>
        /// <param name="strPropiedad">Cadena de texto con el nombre de la propiedad buscada</param>
        /// <param name="TipoDevuelto">Objeto con estado inicial, el cual será devuelto con datos</param>
        /// <returns>Devuelve objeto con datos desde xml, es del mismo tipo que en TipoDevuelto</returns>
        public Object Buscar(string strValorBuscado, string strPropiedad, Object TipoDevuelto)
        {

            try
            {
                Type tp = TipoDevuelto.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static;
                PropertyInfo[] pr = tp.GetProperties(flags);

                //see: http://geekswithblogs.net/shahed/archive/2006/12/06/100427.aspx
                //see: http://stackoverflow.com/questions/15855881/create-instance-of-unknown-enum-with-string-value-using-reflection-in-c-sharp
                //see: http://stackoverflow.com/questions/16100/how-do-i-convert-a-string-to-an-enum-in-c

                if (XDoc != null)
                {
                    foreach (XmlNode xnodo in XDoc.SelectNodes("Datos/" + tp.Name))
                    {
                        if (strValorBuscado == xnodo.SelectSingleNode(strPropiedad).InnerText)
                        {
                            foreach (PropertyInfo m in pr)
                            {
                                if (m.PropertyType.IsEnum)
                                {
                                    if (xnodo.SelectSingleNode(m.Name) != null)
                                        m.SetValue(TipoDevuelto, Enum.Parse(m.PropertyType, xnodo.SelectSingleNode(m.Name).InnerText), null);

                                }
                                else
                                {
                                    if (xnodo.SelectSingleNode(m.Name) != null)
                                        m.SetValue(TipoDevuelto, Convert.ChangeType(xnodo.SelectSingleNode(m.Name).InnerText, m.PropertyType), null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return TipoDevuelto;
        }      

        /// <summary>
        /// Construye un Array List con 
        /// </summary>
        /// <param name="TipoDevuelto"></param>
        /// <returns></returns>
        public ArrayList BuscarTodos(Object TipoDevuelto)
        {
            ArrayList aar = new ArrayList();
            try
            {
                Type tp = TipoDevuelto.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static;
                PropertyInfo[] pr = tp.GetProperties(flags);

                Object obj = new object();

                if (XDoc != null)
                {
                    foreach (XmlNode xnodo in XDoc.SelectNodes("Datos/" + tp.Name))
                    {
                        obj = Activator.CreateInstance(tp);
                        foreach (PropertyInfo m in pr)
                        {
                            if (m.PropertyType.IsEnum)
                            {
                                if (xnodo.SelectSingleNode(m.Name) != null)
                                    m.SetValue(obj, Enum.Parse(m.PropertyType, xnodo.SelectSingleNode(m.Name).InnerText), null);
                            }
                            else
                            {
                                if (xnodo.SelectSingleNode(m.Name) != null)
                                    m.SetValue(obj, Convert.ChangeType(xnodo.SelectSingleNode(m.Name).InnerText, m.PropertyType), null);
                            }
                        }
                        aar.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return aar;
        }        

        public object ModificarObjeto(Object o, string strPropiedadIdentidad)
        {
            try
            {
                Type tp = o.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static;
                PropertyInfo[] pr = tp.GetProperties(flags);
                if (XDoc != null)
                {
                    string strValorBuscado = null;

                    foreach (PropertyInfo m in pr)
                    {
                        if (m.Name == strPropiedadIdentidad)
                            strValorBuscado = m.GetValue(o, null).ToString();
                    }
                    if (strValorBuscado != null)
                    {
                        foreach (XmlNode xnodo in XDoc.SelectNodes("Datos/" + tp.Name + "[" + strPropiedadIdentidad + "=" + strValorBuscado + "]"))
                        {
                            foreach (PropertyInfo m in pr)
                            {
                                xnodo.SelectSingleNode(m.Name).InnerText = m.GetValue(o, null).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return o;
            }

        public void EliminarObjeto(Object o, string strPropiedadIdentidad)
        {
            try
            {
                Type tp = o.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static;
                PropertyInfo[] pr = tp.GetProperties(flags);

                if (XDoc != null)
                {
                    string strValorBuscado = null;

                    foreach (PropertyInfo m in pr)
                    {
                        if (m.Name == strPropiedadIdentidad)
                            strValorBuscado = m.GetValue(o, null).ToString();
                    }
                    if (strValorBuscado != null)
                    {

                        XmlNode xnodo = XDoc.SelectSingleNode("Datos/" + tp.Name + "[" + strPropiedadIdentidad + "=" + strValorBuscado + "]");

                        XDoc.SelectSingleNode("Datos").RemoveChild(xnodo);

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public int ObtenerIdentificadorNuevo(string strPropiedadIdentidad, string strAtributoIdentidad)
        {
            try
            {
                if (XDoc.SelectSingleNode("Datos").Attributes[strAtributoIdentidad] == null)
                    return 0;
                else
                    return XDoc.SelectSingleNode("Datos").Attributes[strAtributoIdentidad].Value != "" ? Convert.ToInt16(XDoc.SelectSingleNode("Datos").Attributes[strAtributoIdentidad].Value) + 1 : 0;

            }
            catch (Exception ex)
            {

                throw ex;
            }              
        }

        /// <summary>
        /// Se modificó el Framework del 3.5 al 4.0 para soportar la sobrecarga
        /// de mas de 2 Path en el metodo Combine
        /// </summary>
        /// 
        private static void Init()
        {
            try
            {
                String strPathRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Datos");
                String strPathFotos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Datos", "Fotos");
                String strPathBD = strPathRoot + "\\BD.xml";
                if (!Directory.Exists(strPathRoot))
                {
                    Directory.CreateDirectory(strPathRoot);
                    Directory.CreateDirectory(strPathFotos);
                    strRutaActual = strPathBD;
                    strRutaFotos = strPathFotos;
                }

                if (!File.Exists(strPathBD))
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPathBD))
                    {
                        file.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><Datos ID=\"\"></Datos>");
                    }
                }
                else
                {
                    if (strRutaActual == null || strRutaActual.Equals(String.Empty))
                    {
                        strRutaActual = strPathBD;
                        strRutaFotos = strPathFotos;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

       
        }
        
    }
}
