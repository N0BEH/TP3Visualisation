using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class streamline : MonoBehaviour
{

    public int taille = 10;
    public float step = 1;

    private List<Vector3> _valeurs;

    private float min;
    private float max;

    private Transform cube;


    void Start()
    {

        _valeurs = new List<Vector3>();
        if (step <= 0)
        {
            step = 1;
        }

        cube = gameObject.transform.Find("volume");

        float val = 0;

        if (taille % 2 == 0)
        {
            Debug.Log("y");
            val = -0.5f;
        }

        
        
        cube.position = new Vector3((taille / 2) + val, (taille / 2) + val, (taille / 2) + val);
        cube.localScale = new Vector3(taille, taille, taille);
        
        
        
        for (float y = 0; y < taille; y += step)
        {
            for (float z = 0; z < taille; z += step)
            {
                for (float x = 0; x < taille; x += step)
                {

                    Vector3 currentPosition = new Vector3(x, y, z);

                    Vector3 calculatedPoint = new Vector3(vectorX(currentPosition), vectorY(currentPosition), vectorZ(currentPosition));


                    _valeurs.Add(calculatedPoint);

                }
            }
        }

        for (int i = 0; i < _valeurs.Count; i++)
        {

            float tmp = 0;
            tmp += _valeurs[i].x;
            tmp += _valeurs[i].y;
            tmp += _valeurs[i].y;

            if (tmp > max)
            {
                max = tmp;
            }

            if (tmp < min)
            {
                min = tmp;
            }

            

        }
        
        Debug.Log(min + " --- " + max);
        
        for (float y = 0; y < taille; y += step)
        {
            for (float z = 0; z < taille; z += step)
            {
                for (float x = 0; x < taille; x += step)
                {
                    Vector3 cp = _valeurs.First();
                    _valeurs.RemoveAt(0);
                    
                    float tmp = 0;
                    tmp += cp.x;
                    tmp += cp.y;
                    tmp += cp.z;
                    
                    float value = (tmp-min)/(max - min);
                    
                    Vector3 currentPosition = new Vector3(x, y, z);

                    GameObject arrow = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    var mr = arrow.GetComponentInChildren<MeshRenderer>();
                    //mr.material.color = new Color(255,0,0);
                    mr.material.color = Color.Lerp(Color.green, Color.red, value);
                    arrow.transform.position = new Vector3(x, y, z);
                    
                    arrow.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                    
                    //Debug.Log(Quaternion.FromToRotation(currentPosition, cp));
                    arrow.transform.rotation = Quaternion.FromToRotation(currentPosition, cp);
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
