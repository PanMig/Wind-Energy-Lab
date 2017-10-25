using UnityEngine;
using UnityEngine.UI;

public class TurbineSelector : MonoBehaviour
{

    public static int numberOfTurbines = 0;
    public static float turbineDefaultPower = 0.0f;
    public float turbinePower = 0.0f;
    private int availiableSpace;
    private int rotorDiameter;
    public enum TurbineType { A, B, C, D, E, F, G, H, I }
    public Text text;


    private void Start()
    {
        availiableSpace = 2000;
        GameManager.instance.Windclass = 1;
        rotorDiameter = 128;
        text.enabled = false;
    }

    public void SetWindClass(int index)
    {
        if (index == 0)
        {
            availiableSpace = 2000;
            GameManager.instance.Windclass = 1;
        }
        else if (index == 1)
        {
            availiableSpace = 2000;
            GameManager.instance.Windclass = 2;
        }
        else
        {
            availiableSpace = 3000;
            GameManager.instance.Windclass = 3;

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
        else if (index == 4)
        {
            rotorDiameter = 90;
            GameManager.cost += 3;
            GameManager.instance.Type = TurbineType.D;
        }
        else if (index == 5)
        {
            rotorDiameter = 90;
            GameManager.cost += 2;
            GameManager.instance.Type = TurbineType.E;
        }
        else if (index == 6)
        {
            rotorDiameter = 60;
            GameManager.cost += 1;
            GameManager.instance.Type = TurbineType.F;
        }
    }

    public void CalculateMaxNumberOfTurbines()
    {
        double number = availiableSpace / (3 * rotorDiameter);
        numberOfTurbines = (int)number;
    }

    public void CheckPlayersSubmission()
    {
        if (GameManager.instance.Windclass == 1)
        {
            if (GameManager.instance.Type == TurbineType.A || GameManager.instance.Type == TurbineType.B || GameManager.instance.Type == TurbineType.C)
            {
                GameManager.instance.LoadSimulationLevel();
            }
            else
            {
                text.enabled = true;
            }
        }
        else if (GameManager.instance.Windclass == 2)
        {
            if (GameManager.instance.Type == TurbineType.D || GameManager.instance.Type == TurbineType.E || GameManager.instance.Type == TurbineType.F)
            {
                GameManager.instance.LoadSimulationLevel();
            }
            else
            {
                text.enabled = true;
            }
        }
    }

}
