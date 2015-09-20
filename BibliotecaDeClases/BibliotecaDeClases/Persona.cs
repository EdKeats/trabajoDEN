using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dato;
using System.Collections;


namespace BibliotecaDeClases
{
    class Persona
    {
        private int intID;

	    public int ID	{		
            get { return intID;}
            set { intID = value;}
        }

        private String strNombreUsuario;

        public String NombreUsuario
        {
            get { return strNombreUsuario; }
            set 
            {
                if(!value.ToString().Equals(""))
                    strNombreUsuario = value; 
            }
        }

        private String strPassword;

        public String Password
        {
            get { return strPassword; }
            set 
            {
                if(!value.ToString().Equals(""))
                    strPassword = value; 
            }
        }

        private bool blnAdministrador;

        public bool Administrador
        {
            get { return blnAdministrador; }
            set { blnAdministrador = value; }
        }

        public static String BuscarUsername(string strUsername)
        {
            String strSalida = null;
            ArrayList arrTodos = MotorXML.getIntancia().BuscarTodos(new Persona());
            foreach (var item in arrTodos)
            {
                Persona perLista = (Persona)item;
                if (perLista.NombreUsuario.Equals(strUsername))
                {
                    strSalida = perLista.NombreUsuario;
                    return strSalida;
                }
            }
            return strSalida;
        }
       
        public void GuardarPersona()
        {            
            MotorXML xml = MotorXML.getIntancia();
            ID = xml.ObtenerIdentificadorNuevo("ID", "ID");
            xml.AgregarObjeto(this, "ID", "ID");
            xml.GuardarXMLArchivo();            
        }
    }
}
