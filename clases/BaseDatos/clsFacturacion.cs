using System;
using libComunes.CapaDatos;
using libComunes.CapaObjetos;
using System.Web.UI.WebControls;


namespace clases.BaseDatos
{
    public class clsFacturacion
    {
        public clsFacturacion()
        {

        }
        #region atributos
        public Int32 Celda { get; set; }
        public string Placa { get; set; }
        public string Tipo { get; set; }
        public string Cedula { get; set; }
        public string Lavado { get; set; }
        public string Loker { get; set; }
        public Int32 Tiempo { get; set; }
        public Int32 Pagado { get; set; }

        private string SQL;
        public string Error { get; private set; }
        public GridView grdInformes { get; set; }
        #endregion
        #region  metodos
        public bool LlenarGrid()
        {
            //Se crea la instrucción sql
            SQL = "SELECT        codigo, Celda, Cedula, Placa, Tipo, Tiempo, Lavado, Pagado "+
                  "FROM          dbo.tblInformes "+
                  "ORDER BY      codigo; ";

            //Se crea el objeto grid
            clsGrid oGrid = new clsGrid();
            //Paso el sql y el grid vacío
            oGrid.SQL = SQL;
            oGrid.grdGenerico = grdInformes;
            //Invocar el llenado del grid
            if (oGrid.LlenarGridWeb())
            {
                //lee el grid lleno
                grdInformes = oGrid.grdGenerico;
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
            SQL = "INSERT INTO tblInformes (Celda, Cedula, Placa, Tipo, Tiempo, Lavado, Pagado) " +
                  "VALUES (@prCelda, @prCedula, @prPlaca, @prTipo, @prTiempo, @prLavado, @prPagado)";

            clsConexion oConexion = new clsConexion();

            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prCelda", Celda);
            oConexion.AgregarParametro("@prCedula", Cedula);
            oConexion.AgregarParametro("@prPlaca", Placa);
            oConexion.AgregarParametro("@prTipo", Tipo);
            oConexion.AgregarParametro("@prTiempo", Tiempo);
            oConexion.AgregarParametro("@prLavado", Lavado);
            oConexion.AgregarParametro("@prPagado", Pagado);

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
