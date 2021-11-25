using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuMonde : MonoBehaviour
{

    public Terrain terrain;

    public float coordX;

    public Camera cam;

    public int speed = 8;

    public int rot = 30;

    public bool peutBouger;

    private float x;
    private float y;
    public float yRotation = 6f;
    public float xRotation = 6f;
    private Vector3 rotateValue;

    // Start is called before the first frame update
    void Start()
    {
        peutBouger = true;
        speed = 8;
        print(Screen.width);
        print(Screen.height);
        cam.transform.position = new Vector3(terrain.transform.position.x + 3, terrain.transform.position.y + 25, terrain.transform.position.z + 28);
        cam.transform.rotation = Quaternion.Euler(new Vector3(40, 60, 0));
    }

    // Update is called once per frame
    void Update()
    {
        bloqueCam();
        if (peutBouger && !MenuPause.pause)
        {
            moveCam();
            resetRotation();
        }

    }


    /*Methode réalisée par Chamsi Ben Kaabar et Chiheb Ben jamaa
	Cette méthode permet à l'utilisateur de déplacer la camera afin d'explorer un terrain
	*/
    void moveCam()
    {
        float coordH = Input.GetAxis("Horizontal");
        float coordV = Input.GetAxis("Vertical");
        //Condition qui permet de savoir si la camera se situe sur le terrain et non en dehors du terrain
        if (cam.transform.position.y > terrain.GetComponent<Transform>().position.x + 5 && cam.transform.position.x < terrain.terrainData.size.x && cam.transform.position.x > 0 && cam.transform.position.z < terrain.terrainData.size.z && cam.transform.position.z > 0)
        {
            cam.transform.Translate(Vector3.forward * coordV * speed * Time.deltaTime);//permet de deplacer la camera vers l'avant ou vers l arriere 
            cam.transform.Translate(Vector3.right * coordH * speed * Time.deltaTime);//permet de deplacer la camera vers la gauche ou vers la droite
        }
        else
        {
            //si l'utilisateur deplace la camera en dehors du terrain alors la camera se re-positionne sur le terrain
            cam.transform.Translate(new Vector3(0, 0, 7) * -coordV * speed * Time.deltaTime);
            cam.transform.Translate(new Vector3(7, 0, 0) * -coordH * speed * Time.deltaTime);

        }



        //permet d'obtenir les valeurs de coefficient en ordonne et en abscisse par le biais de la souris
        xRotation += Input.GetAxis("Mouse Y");
        yRotation += Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(-1 * xRotation, yRotation, 0); // permet de changer la rotation de la camera afin d'avoir une maniabilite de camera plus optimale


        if (cam.transform.position.y < 60 && Input.GetKey(KeyCode.Space))//permet à l'utilisateur de faire monter la camera si il appuie sur la touche "espace" et qu'il n'a pas atteint deja une hauteur trop grande
        {
            cam.transform.position += new Vector3(0, 0.10f, 0);
        }

        //permet à l'utilisateur de faire descendre la camera si il n'est pas dans une hauteur trop basse 
        if (cam.transform.position.y > terrain.GetComponent<Transform>().position.x + 10 && Input.GetKey(KeyCode.LeftShift))
        {
            cam.transform.position += new Vector3(0, -0.10f, 0);
        }


    }


    //Methode permettant à l'utilisateur de bloquer la camera en apuiyant la touche "l" jusqu'à qu'il re-appuiye à cette meme touche
    void bloqueCam()
    {
        if (Input.GetKey(KeyCode.L))
        {
            peutBouger = !peutBouger;
        }
    }

    //Methode permettant de re-initialiser la camera au centre du terrain
    void resetRotation()
    {
        if (Input.GetKey(KeyCode.R))
        {
            cam.transform.position = new Vector3(terrain.terrainData.size.x / 2, terrain.transform.position.x + 20, terrain.terrainData.size.z / 2);
        }
    }

}
