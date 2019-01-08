using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;

namespace LoadCalc._GLOBAL
{
    class ClsGlobal
    {
        internal static UcMain _ucMain;


        internal static string LoadCalcPath = Environment.GetEnvironmentVariable("AppData") + @"\GPTOOLBOX-14\\NET4_0\\LoadCalc Data";
        internal static string DataFilePath = LoadCalcPath + "\\csLoadCalc_Data.xlsx";

        internal static Dictionary<string, string[]> Dict_ConvLoads = new Dictionary<string, string[]>();
        internal static Dictionary<string, string[]> Dict_Members = new Dictionary<string, string[]>();
        internal static Dictionary<string, String[]> Dict_Material = new Dictionary<string, string[]>();

        internal static ObjectId SelectedObjectId = ObjectId.Null;
        internal static double Length_D = 0;
        internal static double Multiplier = 0;

        internal static List<ObjectId> CalculatedMembers = new List<ObjectId>();

        internal static List<string> LockedLayers = new List<string>();
        internal static List<string> FrozenLayer = new List<string>();
    }
}
