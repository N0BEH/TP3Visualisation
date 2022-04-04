using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class fleche : MonoBehaviour
{

    //Taille du cube et du nombre de flèche.
    public int taille = 10;
    //Pas entre chaque flèche.
    //1 unité = 1 mètre.
    public float step = 1;

    public GameObject pt;

    //On stock les valeurs pour calculer le min et le max.
    private List<Vector3> _valeurs;

    private float min;
    private float max;

    private Transform cube;


    //Fonction propre à unity qui est enclanché au demarrage du script, dans notre cas également au démérrage du """"jeu"""".
    void Start()
    {
        //Limitation du framerate à 60, histoire que le PC n'explose pas au demarrage.
        Application.targetFrameRate = 60;
        InitiateArrows();
    }

    //Fonction popre à unity qui est enclanché à chaque framerate (60 fois par seconde dans ce cas ci.)
    private void Update()
    {
        //Dès que l'on utilise la touche Echap.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Appel à la fonction.
            QuitGame();
        }
    }

    
    public void QuitGame()
    {
        //Ferme l'app.
        Application.Quit();
    }

    //Corps de l'app.
    //Permet de créer les flèches en fonction de la position du point et de la formule.
    void InitiateArrows()
    {
        _valeurs = new List<Vector3>();
        //Si jamais l'utilisateur à la bonne idée de mettre un pas de 0. ( boucle infinie si 0 )
        if (step <= 0)
        {
            step = 1;
        }

        //Notre volume transparant.
        cube = gameObject.transform.Find("volume");

        float val = 0;

        //Un petit tweak pour la position du volume autour des nos flèches.
        if (taille % 2 == 0)
        {
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

                    //Calcules du vector en fonction de la position actuelle.
                    Vector3 calculatedPoint = new Vector3(vectorX(currentPosition), vectorY(currentPosition), vectorZ(currentPosition));


                    _valeurs.Add(calculatedPoint);

                }
            }
        }

        for (int i = 0; i < _valeurs.Count; i++)
        {

            //On effectue une moyenne par rapport aux trois points du vecteur ( j'ai pas eu de meilleur idée )
            float tmp = 0;
            tmp += _valeurs[i].x;
            tmp += _valeurs[i].y;
            tmp += _valeurs[i].z;

            if (tmp > max)
            {
                max = tmp;
            }

            if (tmp < min)
            {
                min = tmp;
            }

            

        }

        for (float y = 0; y < taille; y += step)
        {
            for (float z = 0; z < taille; z += step)
            {
                for (float x = 0; x < taille; x += step)
                {
                    //Prend la première valeur de la liste.
                    Vector3 cp = _valeurs.First();
                    //Et la supprime.
                    _valeurs.RemoveAt(0);
                    
                    float tmp = 0;
                    tmp += cp.x;
                    tmp += cp.y;
                    tmp += cp.z;
                    
                    //Normalisation des valeurs pour faire le gradiant de couleur sur les flèches 3D.
                    float value = (tmp-min)/(max - min);
                    
                    Vector3 currentPosition = new Vector3(x, y, z);

                    //Création de l'objet flèche.
                    GameObject arrow = Instantiate(pt);
                    var mr = arrow.GetComponentInChildren<MeshRenderer>();
                    //Application de la couleur sur l'objet.
                    mr.material.color = Color.Lerp(Color.green, Color.red, value);
                    arrow.transform.position = new Vector3(x, y, z);
                    //Changement de sa taille selon le step (Sinon ça fait du grand n'importe quoi).
                    arrow.transform.localScale = new Vector3(step, step, step);
                    
                    arrow.transform.rotation = Quaternion.FromToRotation(currentPosition, cp);
                }
            }
        }
    }

    //Forumule de l'énoncé pour calculer la valeur de X en fonction de la position.
    float vectorX(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return (-4) * x * y + 2 * x - 4 * z - 3;
    }
    
    //Forumule de l'énoncé pour calculer la valeur de Y en fonction de la position.
    float vectorY(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;
        
        return (-4) * Mathf.Pow(x, 2) + 4 * Mathf.Pow(z, 2) + 12 * x - 12 * z;
    }
    
    //Forumule de l'énoncé pour calculer la valeur de Z en fonction de la position.
    float vectorZ(Vector3 position)
    {
        float x = position.x;
        float y = position.y;
        float z = position.z;

        return (-4) * x * y + 4 * y * z - 2 * z + 3;
    }


}
