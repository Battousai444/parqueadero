using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;


namespace clases.BaseDatos
{
    public class clsEmpleado
    {
        public clsEmpleado() 
        {
        
        }
        #region atributos
        public Int32 Codigo { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Tipo { get; set; }
        private string SQL;
        public string Error { get; private set; }
        public GridView grdEmpleado { get; set; }
        #endregion
        #region
        public bool LlenarGrid()
        {
            //Se crea la instrucción sql
            SQL = "SELECT         Codigo, Nombre, Contraseña, Tipo "+
                   "FROM          dbo.tblEmpleadop "+
                   "ORDER BY      Codigo; ";

            //Se crea el objeto grid
            clsGrid oGrid = new clsGrid();
            //Paso el sql y el grid vacío
            oGrid.SQL = SQL;
            oGrid.grdGenerico = grdEmpleado;
            //Invocar el llenado del grid
            if (oGrid.LlenarGridWeb())
            {
                //lee el grid lleno
                grdEmpleado = oGrid.grdGenerico;
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
            SQL = "INSERT INTO tblEmpleadop (Nombre, Contraseña, Tipo) " +
                  "VALUES (@prNombre, @prContraseña, @prTipo)";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prContraseña", Contraseña);
            oConexion.AgregarParametro("@prTipo", Tipo);

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
            SQL = "UPDATE       tblEmpleadop " +
                  "SET          Nombre = @prNombre, " +
                               "Contraseña = @prContraseña, " +
                               "Tipo = @prTipo " +
                  "WHERE        Codigo = @prCodigo ";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCodigo", Codigo);
            oConexion.AgregarParametro("@prNombre", Nombre);
            oConexion.AgregarParametro("@prContraseña", Contraseña);
            oConexion.AgregarParametro("@prTipo", Tipo);
            

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
            SQL = "DELETE FROM tblEmpleadop " +
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
