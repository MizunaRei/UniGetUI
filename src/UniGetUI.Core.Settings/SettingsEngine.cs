﻿using UniGetUI.Core.Data;
using UniGetUI.Core.Logging;

namespace UniGetUI.Core.SettingsEngine
{
    public static class Settings
    {
        public static bool Get(string setting, bool invert = false)
        {
            return File.Exists(Path.Join(CoreData.UniGetUIDataDirectory, setting)) ^ invert;
        }

        public static void Set(string setting, bool value)
        {
            try
            {
                if (value)
                {
                    if (!File.Exists(Path.Join(CoreData.UniGetUIDataDirectory, setting)))
                        File.WriteAllText(Path.Join(CoreData.UniGetUIDataDirectory, setting), "");
                }
                else
                {
                    if (File.Exists(Path.Join(CoreData.UniGetUIDataDirectory, setting)))
                        File.Delete(Path.Join(CoreData.UniGetUIDataDirectory, setting));
                }
            }
            catch (Exception e)
            {
                Logger.Log($"CRITICAL ERROR: CANNOT SET SETTING FOR setting={setting} enabled={value}: " + e.Message);
            }
        }

        public static string GetValue(string setting)
        {
            if (!File.Exists(Path.Join(CoreData.UniGetUIDataDirectory, setting)))
                return "";
            return File.ReadAllText(Path.Join(CoreData.UniGetUIDataDirectory, setting));
        }

        public static void SetValue(string setting, string value)
        {
            try
            {
                File.WriteAllText(Path.Join(CoreData.UniGetUIDataDirectory, setting), value);
            }
            catch (Exception e)
            {
                Logger.Log($"CRITICAL ERROR: CANNOT SET SETTING VALUE FOR setting={setting} value={value}: " + e.Message);
            }
        }
    }
}
