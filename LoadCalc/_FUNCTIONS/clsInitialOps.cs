using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.IO;
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
using OpenMode = Autodesk.AutoCAD.DatabaseServices.OpenMode;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace LoadCalc._FUNCTIONS
{
    class ClsInitialOps
    {
        internal void M1Q()
        {
            _GLOBAL.ClsGlobal clsGlobal = new _GLOBAL.ClsGlobal();
            Document acDoc = AcadApp.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;
            Editor acEd = acDoc.Editor;
            if (acDoc.Name != "Drawing1.dwg")
            {
                using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
                {
                    using (DocumentLock acLock = acDoc.LockDocument())
                    {
                        BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead) as BlockTable;
                        BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite) as BlockTableRecord;
                        LayerTable acLyrTbl = acTrans.GetObject(acDb.LayerTableId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead) as LayerTable;
                        Point3d Max = acDb.Extmax;
                        Point3d Min = acDb.Extmin;
                        if (Min.X < 0 || Min.Y < 0)
                        {
                            double Width = Max.X - Min.X;
                            double Height = Max.Y - Min.Y;
                            double OldCenterX = (Max.X + Min.X) / 2;
                            double OldCenterY = (Max.Y + Min.Y) / 2;
                            Point2d OldCenterXY = new Point2d(OldCenterX, OldCenterY);
                            string strOldCenterXY = OldCenterX + "," + OldCenterY;
                            double CenterX = Width / 2;
                            double CenterY = Height / 2;
                            Point2d NewCenterXY = new Point2d(CenterX, CenterY);
                            string strNewCenterXY = CenterX + "," + CenterY;
                            if (OldCenterXY != NewCenterXY)
                            {
                                foreach (ObjectId ObjId in acLyrTbl)
                                {
                                    LayerTableRecord acLyrTblRec = acTrans.GetObject(ObjId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite) as LayerTableRecord;
                                    if (acLyrTblRec.IsLocked == true)
                                    {
                                        acLyrTblRec.IsLocked = false;
                                        _GLOBAL.ClsGlobal.LockedLayers.Add(acLyrTblRec.Name);
                                    }
                                    if (acLyrTblRec.IsFrozen == true)
                                    {
                                        acLyrTblRec.IsFrozen = false;
                                        _GLOBAL.ClsGlobal.FrozenLayer.Add(acLyrTblRec.Name);
                                    }
                                }

                                acDoc.SendStringToExecute("._move _all  " + strOldCenterXY + " " + strNewCenterXY + " ", true, false, false);
                                acDoc.SendStringToExecute("._Zoom _all ", true, false, false);
                            }
                            else if (OldCenterXY == NewCenterXY)
                            {
                                acEd.WriteMessage("\nDrwaing is in first quadrant");
                                //_GLOBAL.clsGlobal._ucMain.panel2.Enabled = true;
                                //SetDefaultValues();
                            }
                        }
                        else
                        {
                            acEd.WriteMessage("\nDrwaing is in first quadrant");
                            //_GLOBAL.clsGlobal._ucMain.panel2.Enabled = true;
                            //SetDefaultValues();
                        }

                    }
                    //_GLOBAL.clsGlobal._ucMain.M1Q.Enabled = false;
                    acEd.WriteMessage("\nDrawing moved to first quadrant");
                    acTrans.Commit();
                }
            }
            else
            {
                AcadApp.ShowAlertDialog("No drawing found");
            }
        }

        internal void LockLayers()
        {
            Document acDoc = AcadApp.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;
            Editor acEd = acDoc.Editor;
            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock acLock = acDoc.LockDocument())
                {
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead) as BlockTable;
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite) as BlockTableRecord;
                    LayerTable acLyrTbl = acTrans.GetObject(acDb.LayerTableId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead) as LayerTable;
                    int I = 0;
                    foreach (ObjectId ObjId in acLyrTbl)
                    {
                        LayerTableRecord acLyrTblRec = acTrans.GetObject(ObjId, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForWrite) as LayerTableRecord;
                        I++;
                        if (_GLOBAL.ClsGlobal.FrozenLayer.Contains(acLyrTblRec.Name))
                            acLyrTblRec.IsFrozen = true;
                        if (_GLOBAL.ClsGlobal.LockedLayers.Contains(acLyrTblRec.Name))
                            acLyrTblRec.IsLocked = true;
                    }
                    acEd.Regen();
                    acEd.UpdateScreen();

                }
                _GLOBAL.ClsGlobal._ucMain.panel2.Enabled = true;
                acTrans.Commit();
            }
        }

        internal void Initialize()
        {
            ReadData();
        }

        private void ReadData()
        {
            _GLOBAL.ClsGlobal.Dict_ConvLoads = ReadExcelFile("CONV_LOADS", 3);
            _GLOBAL.ClsGlobal.Dict_Members = ReadExcelFile("MEMBERS", 6);
            _GLOBAL.ClsGlobal.Dict_Material = ReadExcelFile("MATERIAL", 5);
        }

        private void SetBuildDate()
        {
            string FilePath = Assembly.GetExecutingAssembly().Location;
            FileInfo FileIn = new FileInfo(FilePath);
            DateTime Date = FileIn.LastWriteTime;
            _GLOBAL.ClsGlobal._ucMain.BuildDate.Text = Date.ToShortDateString();
        }

       
        internal Dictionary<string, string[]> ReadExcelFile(string SheetName, int ColumnCount)
        {
            ClsFunctions clsFunctions = new ClsFunctions();
            Dictionary<string, String[]> rtnValue = new Dictionary<string, string[]>();
            try
            {
                ExcelPackage ExcelPackage = new ExcelPackage();
                using (FileStream fileStream = new FileStream(_GLOBAL.ClsGlobal.DataFilePath, FileMode.Open, FileAccess.Read))
                {
                    ExcelPackage.Load(fileStream);
                    fileStream.Dispose();
                    fileStream.Close();
                }
                ExcelWorkbook excelWorkbook = ExcelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkbook.Worksheets[SheetName];
                rtnValue = clsFunctions.GetDictionaryFromExcel(excelWorksheet, ColumnCount);
                ExcelPackage.Dispose();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Error in Reading DataFile "+SheetName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return rtnValue;
        }
    }
}
