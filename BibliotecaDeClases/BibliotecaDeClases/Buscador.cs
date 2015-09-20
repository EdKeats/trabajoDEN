using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Dato;

namespace BibliotecaDeClases
{
    class Buscador
    {       

        public ArrayList ConsultarParteNombre(String nombreProductoBuscado)
        {
            ArrayList arrSalida = new ArrayList();
            ArrayList arrTodos = MotorXML.getIntancia().BuscarTodos(new Producto());
            foreach (var item in arrTodos)
            {
                Producto productoActual = (Producto)item;
                if (productoActual.Nombre.ToUpper().Contains(nombreProductoBuscado.ToUpper()))
                    arrSalida.Add(productoActual);
            }
            return arrSalida;
        }

        public ArrayList ConsultarStockDeMarca(String marcaProductoBuscado)
        {
            ArrayList arrSalida = new ArrayList();
            ArrayList arrTodos = MotorXML.getIntancia().BuscarTodos(new Producto());
            foreach (var item in arrTodos)
            {
                Producto productoActual = (Producto)item;
                if (productoActual.Marca.ToUpper().Contains(marcaProductoBuscado.ToUpper()))
                    arrSalida.Add(productoActual);
            }
            return arrSalida;
        }

        public ArrayList ConsultarPorPrecio(int precioBuscado)
        {
            ArrayList arrSalida = new ArrayList();
            ArrayList arrTodos = MotorXML.getIntancia().BuscarTodos(new Producto());
            foreach (var item in arrTodos)
            {
                Producto productoActual = (Producto)item;
                if (productoActual.Precio==precioBuscado)
                    arrSalida.Add(productoActual);
            }
            return arrSalida;
        }
    }
}
