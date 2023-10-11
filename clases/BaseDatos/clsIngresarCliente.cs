using System;
using libComunes.CapaDatos; 
using libComunes.CapaObjetos; 
using System.Web.UI.WebControls; 
namespace clases.BaseDatos
{
    public class clsIngresarCliente
    {
        #region Constructor
        public clsIngresarCliente()
        {

        }
        #endregion
        #region atributos
        public string   Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        private string SQL;
        public string Error { get; private set; }

        #endregion
        #region metodos
        public bool Insertar()
        {
            SQL = "INSERT INTO tblClientep (Nombre, Contraseña, Correo) " +
                  "VALUES (@prNombre, @prContraseña, @prCorreo)";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prContraseña", Contraseña);
            oConexion.AgregarParametro("@prCorreo", Correo);
            
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
        #endregion
    }
}
