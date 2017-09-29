using UnityEngine;

public class TurbineSelector : MonoBehaviour {

    public static int numberOfTurbines = 0;
    public static float turbineDefaultPower = 0.0f;
    public float turbinePower = 0.0f;
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
        if (index == 0)
        {
            rotorDiameter = 128;
            turbineDefaultPower = 6.0f;
            turbinePower = turbineDefaultPower;
        }
        else if (index == 1)
        {
            rotorDiameter = 128;
            turbineDefaultPower = 6.0f;
        }
        else if (index == 2)
        {
            rotorDiameter = 90;
            turbineDefaultPower = 3.0f;
        }
        else if (index == 3)
        {
            rotorDiameter = 52;
            turbineDefaultPower = 0.9f;
        }
    }

    public void CalculateMaxNumberOfTurbines()
    {
        double number = availiableSpace / (3 * rotorDiameter);
        numberOfTurbines = (int) number;
    }

}
