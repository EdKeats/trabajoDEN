using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dato;


namespace BibliotecaDeClases
{
    public class Producto
    {
        #region Propiedades

        #region IdProducto
        private String strIdProducto;

        public String IdProducto
        {
            get { return strIdProducto; }
            set { strIdProducto = value; }
        }
        #endregion
        #region Nombre
        private String strNombre;

        public String Nombre
        {
            get { return strNombre; }
            set { strNombre = value; }
        }
        #endregion
        #region Marca
        private String strMarca;

        public String Marca
        {
            get { return strMarca; }
            set { strMarca = value; }
        }
        #endregion
        #region volumen
        private Double dblVolumenLitros;

        public Double VolumenLitros
        {
            get { return dblVolumenLitros; }
            set {
                if (dblVolumenLitros < 0)
                    throw new Exception("El volumen del producto NO puede ser menor a 0");
                dblVolumenLitros = value; }
        }
        #endregion volumen
        #region cantidad
        private int intCantidad;

        public int Cantidad
        {
            get { return intCantidad; }
            set {
                if (intCantidad < 0)
                    throw new Exception("La cantidad NO puede ser menor a 0");
                intCantidad = value; }
        }
        #endregion cantidad
        #region precio
        private Double dblPrecio;

        public Double Precio
        {
            get { return dblPrecio; }
            set {
                if(dblPrecio<0)
                    throw new Exception("El precio NO puede ser menor a 0");
                dblPrecio = value; }
        }
        #endregion precio
        #region tipo
        private String strTipoProducto;

        public String TipoProducto
        {
            get { return strTipoProducto; }
            set { strTipoProducto = value; }
        }
        #endregion
        #region comentario
        private String srtComentario;

        public String Comentario
        {
            get { return srtComentario; }
            set { srtComentario = value; }
        }
        #endregion
        #endregion

        #region Métodos

        //Suma o resta una cantidad ingresada por parametro al producto actual, luego actualiza la BD
        public void ActualizarStock(int actualizacion)
        {
            this.Cantidad +=actualizacion;
            MotorXML xml = MotorXML.getIntancia();
            xml.ModificarObjeto(this, "ID");
            xml.GuardarXMLArchivo();
        }

        public Boolean EliminarProducto(String idProducto)
        {
            MotorXML xml = MotorXML.getIntancia();
            xml.EliminarObjeto(this, "IdProducto");
            xml.GuardarXMLArchivo();
            return true;
        }

        //Agrega un nuevo producto, asignándole una ID única
        public Boolean ingresarProducto(Producto nuevoProducto)
        {            
            MotorXML xml = MotorXML.getIntancia();
            IdProducto = xml.ObtenerIdentificadorNuevo("IdProducto", "IdProducto").ToString();
            xml.AgregarObjeto(this, "ID", "ID");
            xml.GuardarXMLArchivo();
            return true;
        }
        
        //Busca la existencia de un producto con "idProductoValidando".
        //Si existe, carga sus parametros en una instancia de Producto y devuelve "true"
        //De lo contrario, solo devuelve "false" sin cargar datos a la instancia
        public Boolean RecuperarEstadoPorId(String idProductoValidando)
        {
            if (MotorXML.getIntancia().Buscar(IdProducto.ToString(), "ID", this) != null)
                return true;
            else
                return false;
        }

        #endregion



    }

	


}
