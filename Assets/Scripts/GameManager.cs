using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public static int cost;

    //PLayer Information
    public string playerName;
    public string playerSurname;
    public string playerSchoolName;

    public int simulationDurationTime;
    public bool endGame = false;


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

    public void IncrementCost(int areaCost)
    {
        cost += areaCost;
    }

    public void EndGame()
    {
        instance.endGame = true;
    }

    #region Level Management

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //TODO : when final number of scenes will be added, the parameter must change to something more clean and dependent free.
    public void ReplayGame()
    {
        SceneManager.LoadScene(1);
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

}
