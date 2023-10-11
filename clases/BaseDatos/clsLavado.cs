using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;


namespace clases.BaseDatos
{
    public class clsLavado
    { 
         #region Constructor
        public clsLavado()
        {
    

        }
        #endregion
        #region atributos
        public Int32 Celda { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Lavado { get; set; }      
        private string SQL;
        public string Error { get; private set; }
        public GridView grdLavado { get; set; }
        #endregion
        public bool LlenarGrid()
        {
            //Se crea la instrucción sql
            SQL = "SELECT        Celda, placa, tipo, lavado "+
                  "FROM           dbo.tblCeldas " +
                  "WHERE            lavado = 'SI' " +
                  "ORDER BY         Celda; ";

            //Se crea el objeto grid
            clsGrid oGrid = new clsGrid();
            //Paso el sql y el grid vacío
            oGrid.SQL = SQL;
            oGrid.grdGenerico = grdLavado;
            //Invocar el llenado del grid
            if (oGrid.LlenarGridWeb())
            {
                //lee el grid lleno
                grdLavado = oGrid.grdGenerico;
                return true;
            }
            else
            {
                Error = oGrid.Error;
                return false;
            }
        }
        public bool Actualizar()
        {
            SQL = "UPDATE       tblCeldas " +
                  "SET          placa = @prPlaca, " +
                               "tipo = @prTipo, " +
                               "lavado = @prLavado " +
                  "WHERE        Celda = @prCelda";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCelda", Celda);
            oConexion.AgregarParametro("@prPlaca", Placa);
            oConexion.AgregarParametro("@prTipo", Tipo);
            oConexion.AgregarParametro("@prLavado", Lavado);          

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
    }
}
