using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;


namespace clases.BaseDatos
{
    public class clsRecepcion
    {
        #region Constructor
        public clsRecepcion()
        {
            

        }
        #endregion
        #region atributos
        public Int32 Celda { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Cedula { get; set; }
        public string Lavado { get; set; }
        public string Loker { get; set; }
        public Int32 Tiempo { get; set; }
        private string SQL;
        public string Error { get; private set; }
        public GridView grdRecepcion { get; set; } 
        #endregion
        #region  metodos
        public bool LlenarGrid()
        {
            //Se crea la instrucción sql
            SQL = "SELECT        Celda, placa, tipo, cedula, lavado, loker, tiempo " +
                   "FROM         dbo.tblCeldas " +
                    "ORDER BY    Celda; ";

            //Se crea el objeto grid
            clsGrid oGrid = new clsGrid();
            //Paso el sql y el grid vacío
            oGrid.SQL = SQL;
            oGrid.grdGenerico = grdRecepcion;
            //Invocar el llenado del grid
            if (oGrid.LlenarGridWeb())
            {
                //lee el grid lleno
                grdRecepcion = oGrid.grdGenerico;
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
            SQL = "UPDATE       tblCeldas " +
                  "SET          placa = @prPlaca, " +
                               "tipo = @prTipo, " +
                               "cedula = @prCedula, " +
                               "lavado = @prLavado, " +
                               "loker = @prLoker, " +
                               "tiempo = @prTiempo " +
                  "WHERE        Celda = @prCelda";

            clsConexion oConexion = new clsConexion();
            
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCelda", Celda);
            oConexion.AgregarParametro("@prPlaca", Placa);
            oConexion.AgregarParametro("@prTipo", Tipo);
            oConexion.AgregarParametro("@prCedula", Cedula);
            oConexion.AgregarParametro("@prLavado", Lavado);
            oConexion.AgregarParametro("@prLoker", Loker);
            oConexion.AgregarParametro("@prTiempo", Tiempo);

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
            SQL = "UPDATE       tblCeldas " +
                  "SET          placa = @prPlaca, " +
                               "tipo = @prTipo, " +
                               "cedula = @prCedula, " +
                               "lavado = @prLavado, " +
                               "loker = @prLoker, " +
                               "tiempo = @prTiempo " +
                  "WHERE        Celda = @prCelda";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCelda", Celda);
            oConexion.AgregarParametro("@prPlaca", Placa);
            oConexion.AgregarParametro("@prTipo", Tipo);
            oConexion.AgregarParametro("@prCedula", Cedula);
            oConexion.AgregarParametro("@prLavado", Lavado);
            oConexion.AgregarParametro("@prLoker", Loker);
            oConexion.AgregarParametro("@prTiempo", Tiempo);

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
            SQL = "UPDATE       tblCeldas " +
                  "SET          placa = @prPlaca, " +
                               "tipo = @prTipo, " +
                               "cedula = @prCedula, " +
                               "lavado = @prLavado, " +
                               "loker = @prLoker, " +
                               "tiempo = @prTiempo " +
                  "WHERE        placa = @prPlaca";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCelda", Celda);
            oConexion.AgregarParametro("@prPlaca", Placa);
            oConexion.AgregarParametro("@prTipo", Tipo);
            oConexion.AgregarParametro("@prCedula", Cedula);
            oConexion.AgregarParametro("@prLavado", Lavado);
            oConexion.AgregarParametro("@prLoker", Loker);
            oConexion.AgregarParametro("@prTiempo", Tiempo);

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