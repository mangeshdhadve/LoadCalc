using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;


namespace LoadCalc
{
    internal static class clsStatic
    {
        internal static string GetEffectiveName(this BlockReference acBlkRef, Transaction acTrans)
        {
            if (acBlkRef != null)
            {
                BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkRef.DynamicBlockTableRecord, OpenMode.ForRead) as BlockTableRecord;
                return acBlkTblRec.Name;
            }
            return "";
        }

    }
}
