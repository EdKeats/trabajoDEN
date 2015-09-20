using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliotecaDeClases
{
    public class Venta
    {
        #region Propiedades Venta
        private String strIdVenta;

        public String IdVenta
        {
            get { return strIdVenta; }
            set { strIdVenta = value; }
        }

        private List<Producto> lstProductos;

        public List<Producto> Productos
        {
            get { return lstProductos; }
            set { lstProductos = value; }
        }

        public DateTime Fecha { get; set; } 
        #endregion


        public Boolean GenerarVenta()
        {
            //modificar
            //Este metodo tomara los datos que el usuario agrega al carro e imprimira una boleta con los datos, precio, iva, etc.
            return true;
        }
    }
}
