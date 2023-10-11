using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;

namespace clases.BaseDatos
{
    public class clsCliente
    {
        public clsCliente()
        {

        }
        #region atributos
        public Int32 Codigo { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        private string SQL;
        public string Error { get; private set; }
        public GridView grdCliente { get; set; }
        #endregion
        #region
        public bool LlenarGrid()
        {
            //Se crea la instrucción sql
            SQL = "SELECT         Codigo, Nombre, Contraseña, Correo " +
                   "FROM          dbo.tblClientep " +
                   "ORDER BY      Codigo; ";

            //Se crea el objeto grid
            clsGrid oGrid = new clsGrid();
            //Paso el sql y el grid vacío
            oGrid.SQL = SQL;
            oGrid.grdGenerico = grdCliente;
            //Invocar el llenado del grid
            if (oGrid.LlenarGridWeb())
            {
                //lee el grid lleno
                grdCliente = oGrid.grdGenerico;
                return true;
            }
            else
            {
                Error = oGrid.Error;
                return false;
            }
        }
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
        public bool Actualizar()
        {
            SQL = "UPDATE       tblClientep " +
                  "SET          Nombre = @prNombre, " +
                               "Contraseña = @prContraseña, " +
                               "Correo = @prCorreo " +
                  "WHERE        Codigo = @prCodigo ";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCodigo", Codigo);
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
        public bool Eliminar()
        {
            SQL = "DELETE FROM tblClientep " +
                  "WHERE        Codigo = @prCodigo";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCodigo", Codigo);

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