using UnityEngine;

public class GM_TurbineSelection : MonoBehaviour {

    public static int numberOfTurbines;
    private int availiableSpace;
    private int rotorDiameter;

    private void Start()
    {
        availiableSpace = 2000;
        rotorDiameter = 128;
    }


    public void SetWindClass(int index)
    {
        if(index == 0 || index == 1)
        {
            availiableSpace = 2000;
        }
        else
        {
            availiableSpace = 3000;
        }
    }

    public void SetRotorDiameter(int index)
    {
        if (index == 0) rotorDiameter = 128;
        else if (index == 1) rotorDiameter = 90;
        else if (index == 2) rotorDiameter = 52;
    }

    public void CalculateMaxNumberOfTurbines()
    {
        double number = availiableSpace / (3 * rotorDiameter);
        numberOfTurbines = (int) number;
    }
}
