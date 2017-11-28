using UnityEngine;
using UnityEngine.SceneManagement;
using goedle_sdk;


public class GameManager : MonoBehaviour
{

    //statics
    public static GameManager instance = null;
    public static int cost = 0;
    public static int replayIterations = 0;

    //PLayer Information
    public string playerName;
    public string playerSurname;
    public string playerSchoolName;

    //simulation information
    public int simulationDurationTime;
    public int maxNumberOfTurbines = 0;
    public bool endSimulation = false;

    //usage statistics
    public int underPowerSec;
    public int correctPowerSec;
    public int overPowerSec;
    public float profit = 0;
    public float score = 0;
    public int areaInstallationCost = 0; // defines the cost sum of area and subarea.

    //Area information
    public enum MainArea { mountains, fields, seashore }
    public MainArea Areachoice;

    public enum SubArea { archaiological, HVLines, other }
    public SubArea SubAreachoice;

    //Turbine Type Information
    [SerializeField] private TurbineSelector.TurbineType type;
    public TurbineSelector.TurbineType Type { get { return type; } set { type = value; } }

    [SerializeField] private int windclass;
    public int Windclass { get { return windclass; } set { windclass = value ; } }



    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerName = null;
        playerSurname = null;
        playerSchoolName = null;
        GoedleAnalytics.track("launch");
    }

    public void EndSimulation()
    {
        instance.endSimulation = true;
    }

    #region AreaManagement

    public void SetArea(int choice)
    {
        if (choice == 1)
        {
            instance.Areachoice = MainArea.mountains;
        }
        else if (choice == 2)
        {
            instance.Areachoice = MainArea.fields;
        }
        else
        {
            instance.Areachoice = MainArea.seashore;
        }
    }

    public void SetSubArea(int choice)
    {
        if (choice == 1)
        {
            instance.SubAreachoice = SubArea.archaiological;
        }
        else if (choice == 2)
        {
            instance.SubAreachoice = SubArea.HVLines;
        }
        else
        {
            instance.SubAreachoice = SubArea.other;
        }
    }

    #endregion

    #region Cost Management

    public void IncrementCost(int areaCost)
    {
        cost += areaCost;
    }

    #endregion

    #region Level Management

    public void LoadSimulationLevel()
    {
        if(instance.Areachoice == MainArea.mountains)
        {
            SceneManager.LoadScene("Stage3(Mountains)");
        }
        else if(instance.Areachoice == MainArea.fields)
        {
            SceneManager.LoadScene("Stage3(Plains)");
        }
        else if (instance.Areachoice == MainArea.seashore)
        {
            SceneManager.LoadScene("Stage3(Seashore)");
        }
    }

    public void LoadSubAreaLevel()
    {
        if (instance.Areachoice == MainArea.mountains)
        {
            SceneManager.LoadScene("Stage2(Mountains)");
        }
        else if (instance.Areachoice == MainArea.fields)
        {
            SceneManager.LoadScene("Stage2(Plains)");
        }
        else if (instance.Areachoice == MainArea.seashore)
        {
            SceneManager.LoadScene("Stage2(Seashore)");
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {

        if (replayIterations < 3)
        {
            instance.LoadSubAreaLevel();
            replayIterations++;
            cost = 0;

        }
    }

    #endregion

    #region Player Information

    public void SetName(string text)
    {
        instance.playerName = text;
        GoedleAnalytics.trackTraits("first_name",instance.playerName);
    }
    public string GetName()
    {
        return instance.playerName;
    }
    public void SetSurname(string text)
    {
        instance.playerSurname = text;
    }
    public string GetSurname()
    {
        return instance.playerSurname;
    }
    public void SetSchoolName(string text)
    {
        instance.playerSchoolName = text;
        GoedleAnalytics.track("group", "school", instance.playerSchoolName);
    }
    public string ReturnSchoolName()
    {
        return instance.playerSchoolName;
    }

    #endregion

}
