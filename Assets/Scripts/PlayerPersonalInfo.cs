using UnityEngine;
using UnityEngine.UI;


// A script that is only used in the inputScene to retrieve the personal information of the user.
public class InputHandler : MonoBehaviour {

    public Text msgText;

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
