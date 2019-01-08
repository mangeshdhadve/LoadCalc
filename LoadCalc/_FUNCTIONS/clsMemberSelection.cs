using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System.Collections.Generic;
using System.Windows.Forms;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace LoadCalc._FUNCTIONS
{
    class ClsMemberSelection
    {
        internal bool BeamSelection(string LayerName, ref ObjectId ObjId)
        {
            Document acDoc = AcadApp.DocumentManager.MdiActiveDocument;
            Database acDb = acDoc.Database;
            Editor acDocEd = acDoc.Editor;

            TypedValue[] acTypValAr = new TypedValue[1];
            acTypValAr.SetValue(new TypedValue((int)DxfCode.LayerName, LayerName), 0);
            SelectionFilter acSelFtr = new SelectionFilter(acTypValAr);


            using (Transaction acTrans = acDb.TransactionManager.StartTransaction())
            {
                using (DocumentLock acLock = acDoc.LockDocument())
                {
                    BlockTable acBlkTbl = acTrans.GetObject(acDb.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord acBlkTblRec = acTrans.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                    LayerTable acLyrTbl = acTrans.GetObject(acDb.LayerTableId, OpenMode.ForRead) as LayerTable;
                    PromptSelectionOptions acOption = new PromptSelectionOptions
                    {
                        SingleOnly = true,
                        SinglePickInSpace = true
                    };
                    acOption.SingleOnly = true;
                    acOption.SinglePickInSpace = true;
                    PromptSelectionResult acSelect = acDocEd.GetSelection(acOption, acSelFtr);
                    if (acSelect.Status == PromptStatus.OK)
                    {
                        SelectionSet acSelectionSet = acSelect.Value;
                        string strBlockName = "";
                        ObjectId objectId = ObjectId.Null;
                        if (!GetName_ID(acTrans, acSelectionSet, ref strBlockName, ref objectId))
                        {
                            return false;
                        }

                        if (_GLOBAL.ClsGlobal.CalculatedMembers.Contains(ObjId))
                        {
                            DialogResult dialogResult = MessageBox.Show("This member has been solved previously, do you want to continue?", "Member Already Solved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            switch (dialogResult)
                            {
                                case DialogResult.No:
                                    return false;
                            }

                        }
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error in selection, check if the object is in the correct layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                acTrans.Dispose();
            }
            return false;
        }

        internal bool GetName_ID(Transaction acTrans, SelectionSet acSelectionSet, ref string strBlockName, ref ObjectId objectId)
        {
            if (acSelectionSet != null)
            {
                foreach (SelectedObject acSelectedObj in acSelectionSet)
                {
                    if (acSelectedObj != null)
                    {
                        Entity acEnt = acTrans.GetObject(acSelectedObj.ObjectId, OpenMode.ForRead) as Entity;
                        if (acEnt is BlockReference)
                        {
                            BlockReference acBlkRef = acTrans.GetObject(acSelectedObj.ObjectId, OpenMode.ForRead) as BlockReference;
                            objectId = acSelectedObj.ObjectId;
                            if (acBlkRef.IsDynamicBlock)
                            {
                                strBlockName = acBlkRef.GetEffectiveName(acTrans);
                            }
                            else
                            {
                                strBlockName = acBlkRef.Name;
                            }

                            if (objectId == ObjectId.Null || strBlockName == "")
                            {
                                return false;
                            }

                            return true;
                        }
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        internal bool GetName_ID(Transaction acTrans, SelectionSet acSelectionSet, ref List<string> lstBlockNames, ref List<ObjectId> lstObjectIds)
        {
            string strBlockName = "";
            ObjectId objectId = ObjectId.Null;
            if (acSelectionSet != null)
            {
                foreach (SelectedObject acSelectedObj in acSelectionSet)
                {
                    if (acSelectedObj != null)
                    {
                        Entity acEnt = acTrans.GetObject(acSelectedObj.ObjectId, OpenMode.ForRead) as Entity;
                        if (acEnt is BlockReference)
                        {
                            BlockReference acBlkRef = acTrans.GetObject(acSelectedObj.ObjectId, OpenMode.ForRead) as BlockReference;
                            objectId = acSelectedObj.ObjectId;
                            if (acBlkRef.IsDynamicBlock)
                            {
                                strBlockName = acBlkRef.GetEffectiveName(acTrans);
                            }
                            else
                            {
                                strBlockName = acBlkRef.Name;
                            }

                            lstBlockNames.Add(strBlockName);
                            lstObjectIds.Add(objectId);
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }


    }
}
