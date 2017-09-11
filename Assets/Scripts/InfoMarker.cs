using UnityEngine;
using UnityEngine.UI;

public class InfoMarker : MonoBehaviour {

    [SerializeField] private Image infoImage;
    public bool isEnabled = false;

    void Start()
    {
        infoImage.enabled = false;
    }

    public void EnableInfoImage()
    {
        infoImage.enabled = true;
    }

    public void DisableInfoImage()
    {
        infoImage.enabled = false;
    }
}
