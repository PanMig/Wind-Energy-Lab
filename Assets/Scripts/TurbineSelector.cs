using UnityEngine;

public class TurbineSelector : MonoBehaviour
{

    public static int numberOfTurbines = 0;
    public static float turbineDefaultPower = 0.0f;
    public float turbinePower = 0.0f;
    private int availiableSpace;
    private int rotorDiameter;
    public enum TurbineType { A, B, C, D, E, F, G, H, I }

    private void Start()
    {
        availiableSpace = 2000;
        rotorDiameter = 128;
    }

    public void SetWindClass(int index)
    {
        if (index == 0 || index == 1)
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
        if (index == 0 || index == 1)
        {
            rotorDiameter = 128;
            GameManager.cost += 5;
            GameManager.instance.Type = TurbineType.A;
        }
        else if (index == 2)
        {
            rotorDiameter = 90;
            GameManager.cost += 3;
            GameManager.instance.Type = TurbineType.B;
        }
        else if (index == 3)
        {
            rotorDiameter = 52;
            GameManager.cost += 1;
            GameManager.instance.Type = TurbineType.C;
        }
    }

    public void CalculateMaxNumberOfTurbines()
    {
        double number = availiableSpace / (3 * rotorDiameter);
        numberOfTurbines = (int)number;
    }

}
