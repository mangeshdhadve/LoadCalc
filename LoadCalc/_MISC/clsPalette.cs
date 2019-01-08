using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;


namespace LoadCalc._MISC
{
    class ClsPalette
    {
        internal static PaletteSet _PS;
        public void MakePalette()
        {

            if (_PS == null)
            {
                clsReg clsReg = new clsReg();
                String strGuid = clsReg.getMyGuid();
                _PS = new PaletteSet("LoadCalc", new Guid(strGuid))
                {
                    Style = Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowPropertiesMenu
                    | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowAutoHideButton
                    | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowCloseButton
                    | Autodesk.AutoCAD.Windows.PaletteSetStyles.Snappable,

                    MinimumSize = new System.Drawing.Size(320, 370),
                    DockEnabled = DockSides.Left | DockSides.Right | DockSides.Bottom
                };
            //_PS = new PaletteSet("LoadCalc", new Guid(strGuid));

            //    _PS.Style = Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowPropertiesMenu
            //        | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowAutoHideButton
            //        | Autodesk.AutoCAD.Windows.PaletteSetStyles.ShowCloseButton
            //        | Autodesk.AutoCAD.Windows.PaletteSetStyles.Snappable;


            //    _PS.MinimumSize = new System.Drawing.Size(320, 370);
            //    _PS.DockEnabled = DockSides.Left | DockSides.Right | DockSides.Bottom;


                UcMain ucMain = new UcMain();
                _PS.Add("LoadCalc", ucMain);

            }
            _PS.Visible = true;
            _PS[0].PaletteSet.Activate(0);
        }
    }
}