using UnityEngine;

public static class PreferenceManager
{
    private static string LEVELINDES = "LEVELINDES";
    public static int LevelIndex
    {
        get
        {
            return PlayerPrefs.GetInt(LEVELINDES,0);
        }
        set
        {
            PlayerPrefs.SetInt(LEVELINDES, value);
        }
    }

    private static string UNLOCKLEVEL = "UNLOCKLEVEL";
    public static int UnlockLevel
    {
        get
        {
            return PlayerPrefs.GetInt(UNLOCKLEVEL,1);
        }
        set
        {
            PlayerPrefs.SetInt(UNLOCKLEVEL, value);
        }
    }
}
