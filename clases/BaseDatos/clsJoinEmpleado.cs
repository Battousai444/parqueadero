using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;


namespace clases.BaseDatos
{
    public class clsJoinEmpleado
    {
        #region Constructor
        public clsJoinEmpleado()
        {

        }
        #endregion
        #region atributos
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Tipo { get; set; }
        private string SQL;
        public string Error { get; private set; }
        #endregion
        #region  metodos
        public bool Consultar()
        {
            SQL = "SELECT        Nombre, Tipo, Contraseña, Codigo " +
                    "FROM       dbo.tblEmpleadop " +
                     "WHERE     Nombre = @prNombre AND Contraseña = @prContraseña  AND Tipo = @prTipo";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prContraseña", Contraseña);
            oConexion.AgregarParametro("@prTipo", Tipo);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //leer
                    oConexion.Reader.Read();
                    Nombre = oConexion.Reader.GetString(0);
                    Contraseña = oConexion.Reader.GetString(1);
                    Tipo = oConexion.Reader.GetString(2);
                    return true;
                }
                else
                {
                    Error = "No hay Ciudades con el código: " + Nombre + " en la BD, por favor revise la inforamción";
                    return false;
                }
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }

            #endregion
        }
    }
}