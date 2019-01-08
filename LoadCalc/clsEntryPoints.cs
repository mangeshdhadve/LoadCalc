
using Autodesk.AutoCAD.Runtime;



namespace LoadCalc
{
    public class clsEntryPoints
    {
        [CommandMethod("LoadCalc", CommandFlags.Session)]

        public static void LoadPalette()
        {
            _MISC.ClsPalette clsPalette = new _MISC.ClsPalette();
            clsPalette.MakePalette();
        }
    }
}
