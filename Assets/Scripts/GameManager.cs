using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //statics
    public static GameManager instance = null;
    public static int cost = 0;
    public static int replayIterations = 0;
    public enum MainArea { mountains, fields, seashore }

    //PLayer Information
    public string playerName;
    public string playerSurname;
    public string playerSchoolName;

    //simulation information
    public int simulationDurationTime;
    public bool endGame = false;

    //Area information
    public MainArea Areachoice;
    public int SubAreachoice;

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
        print(cost);
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

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {

        if (replayIterations <= 3)
        {
            SceneManager.LoadScene("Stage2");
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

    void OnGUI()
    {
        if (printMsg)
        {
            GUI.Label(new Rect(Screen.width / 2 - 500, Screen.height - 100, 1500, 100), "<color=white><size=50>Maximum replay iterations has been reached</size></color>");
        }
    }

}
