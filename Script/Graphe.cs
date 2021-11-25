using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Fait par Chamsi Ben kaabar et Chiheb Ben Jamaa
 * dernière modification: 22/04/2021
 */
public class Graphe : MonoBehaviour
{

    public GameObject g;
    public GameObject ordonnee;
    public GameObject abcisse;
    public GameObject textes;
    public int a = 1;
    public GameObject pointOrigine;
    public bool nvCercle;
    public float x1;
    public float y1;
    public List<double[]> points;
    List<GameObject> texts;
    public float uniteAbs;
    public float uniteOrd;
    public int nb;

    public GameObject lig1;

    public GameObject lig2;

    public int tailleA;

    public float t;

    public float chg;

    public bool r = false;

    public Terrain ter;

    public GameObject cc;

    public int nb1;

    void Start()
    {
        nb = 0;
        nb1 = 0;
        if (!nvCercle)
        {
            //g.transform.position = new Vector2(pointOrigine.transform.position.x, pointOrigine.transform.position.y);
            //CreerCercle(15, 50, a);
        }
        texts = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {

        t += Time.deltaTime;
        chg += Time.deltaTime;

        if (!r)
        {
            TextOrd(ter.GetComponent<Statistiques>().maxNbAgent);
            TextAbs(ApparitionAgents.secondes);
            CreerLigne(lig1, 0, ter.GetComponent<Statistiques>().nbProieDebut * uniteOrd, true);
            ConvertirCoord(lig1, true);
            CreerLigne(lig2, 0, ter.GetComponent<Statistiques>().nbPredateurDebut * uniteOrd, false);
            ConvertirCoord(lig2, false);
            r = true;
        }



        //CreerCercle(x1, y1, a);
    }

    /*Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui prend en paramètre un entier, permettant créer la ligne en ordonné du repère du graphe.
     * taille (int): entier qui permet de donner le repère en ordonné, qui correspond au nombre maximal atteint d'agent au cour d'une simulation.
  
     * dernière modification: 18/04/2021
     * */
    void TextOrd(int taille)
    {
        uniteOrd = int.Parse(ordonnee.GetComponent<LineRenderer>().GetPosition(1).y.ToString()) / taille;//reel qui permet de tracer les unités du repère en ordonne
        int c = 0;
        //boucle permettant de créer tous les points du repere en ordonné toutes les deux unités et en augmentant de hauteur pour utiliser toute la surface de la ligne 
        for (int i = 0; i <= taille; i++)
        {
            if (i % 2 == 0)
            {
                GameObject t = Instantiate(textes, new Vector3(pointOrigine.transform.position.x, pointOrigine.transform.position.y + c, 0), Quaternion.identity);
                t.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                t.transform.GetChild(0).GetComponent<Text>().transform.position = new Vector3(pointOrigine.transform.position.x - 30, pointOrigine.transform.position.y + c, 0);
                t.transform.SetParent(cc.GetComponent<RectTransform>(), true);
                texts.Add(t);
            }
            c += int.Parse(ordonnee.GetComponent<LineRenderer>().GetPosition(1).y.ToString()) / taille;
        }

    }


    /*Fait par Chamsi Ben kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui prend en paramètres un GameObject, deux réels et un booleen, permettant de créer une ligne grace aux points données en parametres 
     * ligne1 (GameObject): GameObject qui stocke la variable Linerenderer qui permet de tracer la ligne 
     * x1 (float): réel qui correspond à l'axe des abscisses du point
     * y1 (float):  réel qui correspond à l'axe des ordonnés du point
     * proie (bool): booléen qui permet de savoir si l'agent est une proie ou non
     * 
     * Dernière modification: 17/04/2021
     */
    void CreerLigne(GameObject ligne1, float x1, float y1, bool proie)
    {
        if (proie)//permet de savoir si c'est une proie 
        {
            if (nb >= ligne1.GetComponent<LineRenderer>().positionCount)//permet de augmenter la taille de LineRenderer pour y ajouter de nouveaux points
            {
                ligne1.GetComponent<LineRenderer>().positionCount++;
            }


            ligne1.GetComponent<RectTransform>().position = new Vector3(pointOrigine.transform.position.x, pointOrigine.transform.position.y, 0);
            //lig1.transform.position = new Vector3(pointOrigine.transform.position.x, pointOrigine.transform.position.y, 0);
            ligne1.GetComponent<LineRenderer>().SetPosition(nb, new Vector3(pointOrigine.transform.position.x + x1, pointOrigine.transform.position.y + y1, 0));
            nb++;
        }
        else//si c'est un predateur 
        {
            if (nb1 >= ligne1.GetComponent<LineRenderer>().positionCount)
            {
                ligne1.GetComponent<LineRenderer>().positionCount++;
            }


            ligne1.GetComponent<RectTransform>().position = new Vector3(pointOrigine.transform.position.x, pointOrigine.transform.position.y, 0);
            //lig1.transform.position = new Vector3(pointOrigine.transform.position.x, pointOrigine.transform.position.y, 0);
            ligne1.GetComponent<LineRenderer>().SetPosition(nb1, new Vector3(pointOrigine.transform.position.x + x1, pointOrigine.transform.position.y + y1, 0));
            nb1++;
        }


    }

    /**Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui prend en paramètre deux paramètres, GameObject et un booléen, qui permet de créer une ligne à partir d'un tableau qui stocke l'ensemble des points
     * lig1 (GameObject): GameObject qui stocke un LineRenderer
     * proie (bool): booléen qui permet de savoir c'est une proie ou non
     * 
     * Dernière modification: 17/04/2021
     */
    public void ConvertirCoord(GameObject lig1, bool proie)
    {

        double x = 0;
        double y = 0;


        if (proie)
        {
            List<double> proies = ter.GetComponent<Statistiques>().proies;
            //boucle qui permet de parcourir la liste entière qui stocke le temps où il y a eu un changement de nombre d'agent  afin de créer la ligne du graphe concernant les proies
            for (int i = 1; i < ter.GetComponent<Statistiques>().tempsProies.Count; i++)
            {

                x = ter.GetComponent<Statistiques>().tempsProies[i];
                y = proies[i];
                CreerLigne(lig1, (float)x * uniteAbs, (float)y * uniteOrd, proie);
            }
        }
        else
        {
            List<double> predateur = ter.GetComponent<Statistiques>().predateurs;
            //boucle qui permet de parcourir la liste entière qui stocke le temps où il y a eu un changement de nombre d'agent afin de créer la ligne du graphe concernant les prédateurs
            for (int i = 1; i < ter.GetComponent<Statistiques>().tempsPredateurs.Count; i++)
            {
                x = ter.GetComponent<Statistiques>().tempsPredateurs[i];
                y = predateur[i];
                CreerLigne(lig1, (float)x * uniteAbs, (float)y * uniteOrd, proie);
            }
        }



    }


    /*Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui prend en paramètre un reel, permettant créer la ligne en abscisse du repere du graphe.
     * temps (float): reel qui permet de donner le repère en abscisse, qui correspond au temps passé en secondes, au cour d'une simulation.
  
     * dernière modification: 18/04/2021
     * */
    void TextAbs(float temps)
    {
        int c = 0;
        float a = temps / 10;
        uniteAbs = (int.Parse(abcisse.GetComponent<LineRenderer>().GetPosition(1).y.ToString()) / 10) / a;//reel qui permet de tracer les unites du repère en abscisse
        print(abcisse.GetComponent<LineRenderer>().GetPosition(1).y.ToString());
        //boucle qui permet de couper en 10 l'axe des abscisses en utilisant toute la longueur de la ligne 
        for (int i = 0; i < 10; i++)
        {
            c += int.Parse(abcisse.GetComponent<LineRenderer>().GetPosition(1).y.ToString()) / 10;
            GameObject t = Instantiate(textes, new Vector3(pointOrigine.transform.position.x + c, pointOrigine.transform.position.y, 0), Quaternion.identity);
            texts.Add(t);
            t.transform.GetChild(0).GetComponent<Text>().text = a.ToString();
            t.transform.GetChild(0).GetComponent<Text>().transform.position = new Vector3(pointOrigine.transform.position.x + c, pointOrigine.transform.position.y - 20, 0);
            t.transform.SetParent(cc.GetComponent<RectTransform>(), true);
            a += temps / 10;

        }
        textes.SetActive(false);
    }



    /* Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de re-initialiser les variables apres chaque passage de l'utilisateur dans le menu Statistiques

     * Dernière modification: 20/04/2021
     */
    public void RFalse()
    {
        lig1.GetComponent<LineRenderer>().positionCount = 0;
        lig2.GetComponent<LineRenderer>().positionCount = 0;
        nb = 0;
        nb1 = 0;
        r = false;
        textes.SetActive(true);
        for (int i = 0; i < texts.Count; i++)
        {
            Destroy(texts[i]);
        }

    }

}
