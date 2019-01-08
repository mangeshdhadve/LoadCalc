using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OfficeOpenXml;
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Colors;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Microsoft.VisualBasic;

namespace LoadCalc
{
    public partial class UcMain : UserControl
    {
        private _FUNCTIONS.ClsInitialOps clsInitialOps = new _FUNCTIONS.ClsInitialOps();

        public UcMain()
        {
            InitializeComponent();
            _GLOBAL.ClsGlobal._ucMain = this;
            Initialize();
        }

        private void Initialize()
        {
            clsInitialOps.Initialize();
        }       

        private void M1Q_Click(object sender, EventArgs e)
        {
            clsInitialOps.M1Q(); //moves entire drawing in first quadrant (+X, +Y)
            M1Q.Enabled = false;
            AcadApp.DocumentManager.MdiActiveDocument.SendStringToExecute("LL"+" ", true, false, false);
        }
        [CommandMethod("LL", CommandFlags.Session & CommandFlags.NoMultiple)]
        public void LockLayers()
        {
            clsInitialOps.LockLayers();
            panel2.Enabled = true;
            panel2.Invalidate();
            panel2.Update();
            panel2.Refresh();
            System.Windows.Forms.Application.DoEvents();
            _GLOBAL.ClsGlobal.LockedLayers.Clear();
            _GLOBAL.ClsGlobal.FrozenLayer.Clear();
        }
    }
}
