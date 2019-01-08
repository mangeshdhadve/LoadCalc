using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;


namespace LoadCalc._MISC
{
    class clsReg
    {


        static internal string strBase = "SOFTWARE";
        static internal string strGPKey = "GlidePathGroup";
        static internal string strGPProgramName = "CheckPart";

        internal string getMyGuid()
        {
            return getGuid("LoadCalculation");
        }


        #region "Basic Code"



        private bool getGPkey(ref RegistryKey gpKey)
        {
            bool rtnValue = false;
            RegistryKey regBaseKey = null;
            try
            {
                // get the base software key
                regBaseKey = Registry.CurrentUser.OpenSubKey(strBase, true);
                if (getSubKey(ref regBaseKey, strGPKey, ref gpKey))
                {
                    rtnValue = true;
                }
                else
                {
                    rtnValue = false;
                }
            }
            catch (Exception)
            {
                rtnValue = false;
            }
            finally
            {
                regBaseKey.Close();
            }

            return rtnValue;
        }


        // Get GUID of palette or create a new one
        internal string DeleteGuid(string strKey)
        {
            string strGuid = "";
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    RegistryKey guidKey = null;
                    // Get sub key
                    if (getSubKey(ref gpKey, strKey, ref guidKey))
                    {
                        // Get Guid
                        if (hasValue(ref guidKey, "GUID"))
                        {
                            strGuid = guidKey.GetValue("GUID", "").ToString();
                            guidKey.DeleteValue("GUID");
                        }

                        guidKey.Close();
                    }
                }
                gpKey.Close();
                return strGuid;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Saving GUID" + "\n" + ex.ToString());
            }
            return "";
        }


        // Get GUID of palette or create a new one
        private string getGuid(string strKey)
        {
            string strGuid = "";
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    RegistryKey guidKey = null;
                    // Get sub key
                    if (getSubKey(ref gpKey, strKey, ref guidKey))
                    {
                        // Get Guid
                        if (hasValue(ref guidKey, "GUID"))
                        {
                            strGuid = guidKey.GetValue("GUID", "").ToString();
                        }
                        else
                        {
                            // Create new Guid
                            strGuid = "{" + System.Guid.NewGuid().ToString() + "}";
                            guidKey.SetValue("GUID", strGuid);
                        }
                        guidKey.Close();
                    }
                }
                gpKey.Close();
                return strGuid;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Saving GUID" + "\n" + ex.ToString());
            }
            return "";
        }

        // Check if the Registry Key contains a subkey
        private bool getSubKeyList(ref RegistryKey subkey, ref List<string> lstKeys)
        {
            foreach (string item in subkey.GetSubKeyNames())
            {
                lstKeys.Add(item);
            }

            return true;
        }

        // Check if the Registry Key contains a subkey
        private bool getSubKeyValueList(ref RegistryKey subkey, ref List<string> lstKeys)
        {
            foreach (string item in subkey.GetValueNames())
            {
                if (!string.IsNullOrEmpty(item))
                {
                    lstKeys.Add(item);
                }
            }
            return true;
        }

        // Check if the Registry Key contains a subkey
        private bool hasValue(ref RegistryKey subkey, string value)
        {
            foreach (string item in subkey.GetValueNames())
            {
                if (item.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }

        // Delete Key
        private bool DeleteSubKey(string strKey)
        {
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    if (hasSubKey(ref gpKey, strKey))
                    {
                        //RegistryKey subkey = null;
                        // Delete Sub Key
                        gpKey.DeleteSubKey(strKey, true);
                    }
                    gpKey.Close();
                }

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        // Delete the name in the SubKey Given
        private bool DeleteSubKeyName(string strKey, string name)
        {
            bool rtnValue = false;
            try
            {
                RegistryKey gpKey = null;
                // get Default key
                if (getGPkey(ref gpKey))
                {
                    if (hasSubKey(ref gpKey, strKey))
                    {
                        RegistryKey subkey = null;
                        // get subkey
                        if (getSubKey(ref gpKey, strKey, ref subkey))
                        {
                            // Delete the name of the Key
                            foreach (string item in subkey.GetSubKeyNames())
                            {
                                if (item == name)
                                {
                                    subkey.DeleteValue(name);
                                    rtnValue = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            subkey.DeleteValue(name);
                            subkey.Close();
                        }
                    }
                    gpKey.Close();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return rtnValue;
        }

        // Check if the Registry Key contains a subkey
        private bool hasSubKey(ref RegistryKey subkey, string value)
        {
            foreach (string item in subkey.GetSubKeyNames())
            {
                if (item.ToString() == value)
                {
                    return true;
                }
            }
            return false;
        }

        // get SubKey and or create a new one and return the new key
        private bool getSubKey(ref RegistryKey key, string strkey, ref RegistryKey rtnKey)
        {
            try
            {
                if (!hasSubKey(ref key, strkey))
                {
                    key.CreateSubKey(strkey);
                    rtnKey = key.OpenSubKey(strkey, true);
                    return true;
                }
                else
                {
                    rtnKey = key.OpenSubKey(strkey, true);
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }



        internal void SaveData(string strName, string Value)
        {
            SaveValue(strGPProgramName, strName, Value);
        }

        internal string GetData(string strName)
        {
            string strValue = GetValue(strGPProgramName, strName);
            return strValue;
        }

        internal void SaveValue(string strKey, string myName, string myData)
        {
            RegistryKey gpKey = null;
            if (getGPkey(ref gpKey))
            {
                RegistryKey subkey = null;
                if (getSubKey(ref gpKey, strKey, ref subkey))
                {
                    subkey.SetValue(myName, myData);
                }
            }
        }

        internal string GetValue(string strKey, string myName)
        {
            RegistryKey gpKey = null;
            if (getGPkey(ref gpKey))
            {
                RegistryKey subkey = null;
                if (getSubKey(ref gpKey, strKey, ref subkey))
                {
                    return (string)subkey.GetValue(myName, "");
                }
            }
            return "";
        }
        internal void SaveProjectDefaults(List<string> lstProjects, List<string> lstDefault)
        {
            string strCombined = "";
            for (int i = 0; i <= lstProjects.Count - 1; i++)
            {
                string strValues = "";
                for (int k = 0; k <= lstDefault.Count - 1; k++)
                {
                    if (lstDefault.Count - 1 == k)
                    {
                        strValues += lstDefault[k];
                    }
                    else
                    {
                        strValues += lstDefault[k] + "|";
                    }
                }
                strCombined += lstProjects[i] + "|" + strValues + "$";
            }
        }

        #endregion "Basic Code"
    }
}
