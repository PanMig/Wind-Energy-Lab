using UnityEngine;
using UnityEngine.UI;


public class InputFieldHandler : MonoBehaviour {

    public Text msgText;

    private void Start()
    {
        msgText.enabled = false;
    }

    public void InputFieldsFilled() {
		if(GameManager.instance.playerName != null && GameManager.instance.playerSurname != null && GameManager.instance.playerSchoolName != null){
            GameManager.instance.LoadNextLevel();
		}
		else
        {
			msgText.enabled = true;
		}
	}
}
