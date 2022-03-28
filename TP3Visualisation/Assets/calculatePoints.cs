using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculatePoints : MonoBehaviour
{

    public int taille = 10;
    public float step = 1;

    public GameObject pt;
    
    void Start()
    {
        for (float y = 0; y < taille; y += step)
        {
            for (float z = 0; z < taille; z += step)
            {
                for (float x = 0; x < taille; x += step)
                {

                    Vector3 currentPosition = new Vector3(x, y, z);

                    Vector3 calculatedPoint = new Vector3(vectorX(currentPosition), vectorY(currentPosition), vectorZ(currentPosition));
                    Debug.Log(currentPosition + " -> " + calculatedPoint);

                    GameObject arrow = Instantiate(pt);
                    arrow.transform.position = new Vector3(x, y, z);
                    arrow.transform.rotation = Quaternion.FromToRotation(currentPosition, calculatedPoint);

                }
            }
        }
    }

    float vectorX(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return (-4) * x * y + 2 * x - 4 * z - 3;
    }
    
    float vectorY(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;
        
        return (-4) * Mathf.Pow(x, 2) + 4 * Mathf.Pow(z, 2) + 12 * x - 12 * z;
    }
    
    float vectorZ(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return (-4) * x * y + 4 * y * z - 2 * z + 3;
    }


}
