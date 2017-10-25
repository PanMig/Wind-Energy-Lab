using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool endGame = false;

    //Area information
    public enum MainArea { mountains, fields, seashore }
    public MainArea Areachoice;
    public int SubAreachoice;

    //TurbineTypeInformation
    [SerializeField] private TurbineSelector.TurbineType type;
    public TurbineSelector.TurbineType Type { get { return type; } set { type = value; } }

    [SerializeField] private int windclass;
    public int Windclass { get { return windclass; } set { windclass = value ; } }

    private bool printMsg = false;


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
    }

    public void EndGame()
    {
        instance.endGame = true;
    }

    #region AreaManagement

    public void SetArea(int choice)
    {
        if (choice == 1)
        {
            cost = 3;
            instance.Areachoice = MainArea.mountains;
        }
        else if (choice == 2)
        {
            cost = 2;
            instance.Areachoice = MainArea.fields;
        }
        else
        {
            cost = 1;
            instance.Areachoice = MainArea.seashore;
        }
    }

    public void SetSubArea(int choice)
    {
        SubAreachoice = choice;
    }

    #endregion

    #region Cost Management

    public void IncrementCost(int areaCost)
    {
        cost += areaCost;
    }


    public void ReArrangeCost(MainArea choice)
    {
        if (choice == MainArea.mountains)
        {
            cost = 3;
        }
        else if (choice == MainArea.fields)
        {
            cost = 2;
        }
        else
        {
            cost = 1;
        }
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

        if (replayIterations <= 3)
        {
            instance.LoadSubAreaLevel();
            replayIterations++;
            ReArrangeCost(Areachoice);

        }
        else
        {
            instance.printMsg = true;
        }
    }

    #endregion

    #region Player Information

    public void SetName(string text)
    {
        instance.playerName = text;
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
    }
    public string ReturnSchoolName()
    {
        return instance.playerSchoolName;
    }

    #endregion

    //used only for displaying the replay iterations message.
    void OnGUI()
    {
        if (printMsg)
        {
            GUI.Label(new Rect(Screen.width / 2 - 500, Screen.height - 100, 1500, 100), "<color=white><size=50>Maximum replay iterations has been reached</size></color>");
        }
    }

}
