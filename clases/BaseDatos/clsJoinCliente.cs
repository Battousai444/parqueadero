using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;



namespace clases.BaseDatos
{
    public class clsJoinCliente
    {
        #region Constructor
        public clsJoinCliente()
        {

        }
        #endregion
        #region atributos
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        
        private string SQL;
        public string Error { get; private set; }
        #endregion
        #region  metodos
        public bool Consultar()
        {
            SQL = "SELECT        Nombre, Contraseña, Codigo, Correo " +
                    "FROM            dbo.tblClientep " +
                     "WHERE     Nombre = @prNombre AND Contraseña = @prContraseña ";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prContraseña", Contraseña);


            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //leer
                    oConexion.Reader.Read();
                    Nombre = oConexion.Reader.GetString(0);
                    Contraseña = oConexion.Reader.GetString(1);
                    
                    return true;
                }
                else
                {
                    
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