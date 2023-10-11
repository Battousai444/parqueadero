using System;
using libComunes.CapaDatos; 
using libComunes.CapaObjetos; 
using System.Web.UI.WebControls; 

namespace clases.BaseDatos
{
   public class clsIndex
    {
        #region Constructor
        public clsIndex()
        {

        }
        #endregion
        #region Constructor
        public GridView grdCeldasIndex { get; set; }
        private string SQL;
        public string Error { get; private set; }
        #endregion

        public bool LlenarGrid()
        {
            //Se crea la instrucción sql
            SQL = "SELECT         Celda, placa, tipo "+
            "FROM dbo.tblCeldas "+
            "ORDER BY Celda;";

            //Se crea el objeto grid
            clsGrid oGrid = new clsGrid();
            //Paso el sql y el grid vacío
            oGrid.SQL = SQL;
            oGrid.grdGenerico = grdCeldasIndex;
            //Invocar el llenado del grid
            if (oGrid.LlenarGridWeb())
            {
                //lee el grid lleno
                grdCeldasIndex = oGrid.grdGenerico;
                return true;
            }
            else
            {
                Error = oGrid.Error;
                return false;
            }
        }                                                   
    }
}
