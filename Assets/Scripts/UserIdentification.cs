using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using goedle_sdk.detail;


public class UserIdentification : MonoBehaviour
{

    public InputField[] inputFields;
    public GameObject textLog;

    private void Start()
    {
        textLog.SetActive(false);
    }

    public bool IsInputEmpty()
    {
        foreach (InputField field in inputFields)
        {
            if (field.text.Length == 0)
            {
                return true;
            }
        }
        return false;
    }

    public void StartSimulation()
    {
        if (IsInputEmpty())
        {
            textLog.SetActive(true);
        }
        else
        {
            // Creating a hashed user id, md5 hash of a string and then using a guid
            string[] user_ids = inputFields.OfType<InputField>().Select(o => o.ToString()).ToArray();
            string hashed_user_id = GoedleUtils.userHash(user_ids.ToString());
            goedle_sdk.GoedleAnalytics.setUserId(hashed_user_id);
            StartCoroutine(waitOnStrategy());
        }
    }


    IEnumerator waitOnStrategy()
    {
        int c = 0;
        while (goedle_sdk.GoedleAnalytics.gio_interface.strategy == null || c < 150)
        {
            yield return null;
            c++;
        }
        // Apply the new configuration, the request 
        GameManager.instance.LoadLevel("Stage1");
    }

}
