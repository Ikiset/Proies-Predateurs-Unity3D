using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// La classe a été fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
//Derniere modification le 15/04/2021

public class ApparitionAgents : MonoBehaviour
{
    public Terrain ter;

    public GameObject terrain;

    public List<GameObject> oursS = new List<GameObject>();

    public List<GameObject> cerfs = new List<GameObject>();

    public List<GameObject> herbes = new List<GameObject>();

    public List<GameObject> lapins = new List<GameObject>();

    public List<GameObject> loups = new List<GameObject>();

    public List<GameObject> plantes1 = new List<GameObject>();

    public List<GameObject> plantes2 = new List<GameObject>();

    public List<string> tagAgents = new List<string>();

    [SerializeField]
    public static int nbCerf;
    [SerializeField]
    public static int nbOurs;
    [SerializeField]
    public static int nbHerbes;

    public static int nbLoup;

    public static int nbLapin;

    public static int nbPlante1;

    public static int nbPlante2;

    public int nbProies;

    public int nbPredateurs;

    public int nbTotal;

    [SerializeField]
    public static GameObject ours;
    [SerializeField]
    public static GameObject cerf;
    [SerializeField]
    public static GameObject herbe;

    public static GameObject loup;

    public static GameObject lapin;

    public static GameObject plante1;

    public static GameObject plante2;

    public GameObject our;
    public GameObject cer;
    public GameObject lou;
    public GameObject lap;
    public GameObject pl1;
    public GameObject pl2;

    public bool apparition;

    public float tempsIntervalleApparationAutotrophe;
    public float temps;
    public static float secondes;

    // Start is called before the first frame update
    void Start()
    {
        Initialisation();
    }

    // Update is called once per frame
    void Update()
    {
        secondes += Time.deltaTime;
        temps += Time.deltaTime;
        if (tempsIntervalleApparationAutotrophe <= 50)
        {
            tempsIntervalleApparationAutotrophe = 50;
        }
        if (temps > tempsIntervalleApparationAutotrophe)
        {
            Apparition(plante1, nbPlante1, plantes1);
            Apparition(plante2, nbPlante2, plantes2);
            temps = 0;
        }

        if (apparition)
        {
            Apparition(ours, nbOurs, oursS);
            Apparition(cerf, nbCerf, cerfs);
            Apparition(lapin, nbLapin, lapins);
            Apparition(loup, nbLoup, loups);

            apparition = false;
            if (ter.GetComponent<Statistiques>().nbEnvie < 100)
            {
                nbTotal += nbOurs + nbCerf + nbLapin + nbLoup;

                ter.GetComponent<Statistiques>().nbEnvie = nbTotal;

                ter.GetComponent<Statistiques>().nbPredateurs += nbOurs;
                ter.GetComponent<Statistiques>().predateurs.Add(ter.GetComponent<Statistiques>().nbPredateurs);
                ter.GetComponent<Statistiques>().tempsPredateurs.Add(secondes);

                ter.GetComponent<Statistiques>().nbPredateurs += nbLoup;
                ter.GetComponent<Statistiques>().predateurs.Add(ter.GetComponent<Statistiques>().nbPredateurs);
                ter.GetComponent<Statistiques>().tempsPredateurs.Add(secondes);

                ter.GetComponent<Statistiques>().nbProies += nbCerf;
                ter.GetComponent<Statistiques>().proies.Add(ter.GetComponent<Statistiques>().nbProies);
                ter.GetComponent<Statistiques>().tempsProies.Add(secondes);

                ter.GetComponent<Statistiques>().nbProies += nbLapin;
                ter.GetComponent<Statistiques>().proies.Add(ter.GetComponent<Statistiques>().nbProies);
                ter.GetComponent<Statistiques>().tempsProies.Add(secondes);

            }

            nbCerf = 0;
            nbLapin = 0;
            nbLoup = 0;
            nbOurs = 0;
        }
    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa.
     * 
     * Méthode qui permet de faire apparaître des agents dans un terrain.
     * agent (GameObject) : l'agent qu'on veut faire apparaître dans le terrain.
     * nb (int) : nombre d'agent à faire apparaitre 
     * 
     * Derniere modification le 15/04/2021
     */
    void Apparition(GameObject agent, int nb, List<GameObject> agents)
    {
        //S'il y a déjà au moins 100 agents sur le terrain alors on n'en fait pas apparaître d'autre
        if (ter.GetComponent<Statistiques>().nbEnvie >= 100)
        {
            return;
        }

        agent.SetActive(true);

        //Si le nombre d'agents est nul, alors on sort de la méthode.
        if (nb == 0)
        {
            agent.SetActive(false);
            return;
        }

        //on ajoute le tag de l'agent si c'est la première fois que l'agent apparaît dans le terrain.
        if (!tagAgents.Contains(agent.tag))
        {
            tagAgents.Add(agent.tag);
        }

        float x = 0;
        float z = 0;
        bool bon = false;
        bool bon2 = false;


        GameObject[] eau = GameObject.FindGameObjectsWithTag("eau");

        GameObject[] roches = GameObject.FindGameObjectsWithTag("roche");
        float distance = 0;
        float distance2 = 0;

        float distanceRoc = 0;
        float distanceRoche = 0;



        for (int i = 0; i < nb; i++)
        {
            //si le terrain ne dispose pas de point d'eau ou de rocher alors on prend des coordonnées aléatoires et on fait apparaitre l'agent.
            if (eau.Length == 0 && roches.Length == 0)
            {
                x = Random.Range(0, ter.terrainData.size.x);//on lui donne des coordonnées aléatoires situées dans le terrain
                z = Random.Range(0, ter.terrainData.size.z);
                bon = true;
            }

            //on cherche des coordonnées tant que les coordonnées ne sont pas valides c’est-à-dire, qu'il ne faut pas qu'ils apparaissent sous l'eau, ou dans les rochers
            while (!bon || !bon2)
            {
                x = Random.Range(0, ter.terrainData.size.x);//on lui donne des coordonnées aléatoires situées dans le terrain
                z = Random.Range(0, ter.terrainData.size.z);

                for (int j = 0; j < eau.Length; j++)
                {

                    distance = Vector3.Distance(new Vector3(x, 0, z), eau[j].transform.position); //on calcule la distance entre le point d'eau et les coordonnées choisis
                    distance2 = Vector3.Distance((eau[j].transform.GetChild(2).position), eau[j].transform.position);//on calcule le rayon de l'eau


                    if (distance2 >= distance)
                    {
                        bon = false;
                    }
                    else
                    {
                        bon = true;
                    }
                }


                for (int k = 0; k < roches.Length; k++)
                {
                    distanceRoche = Vector3.Distance(new Vector3(x, 0, z), roches[k].transform.position);//on calcule la distance entre le point d'eau et les coordonnées choisis
                    distanceRoc = Vector3.Distance(roches[k].transform.GetChild(0).position, roches[k].transform.position);//on calcule le rayon de la roche

                    if (distanceRoche > distanceRoc)
                    {
                        bon2 = true;
                    }
                    else
                    {
                        bon2 = false;
                    }
                }

            }

            Instantiate(agent, new Vector3(x, agent.transform.position.y, z), Quaternion.identity);


            agents.Add(agent);
            bon = false;
            bon2 = false;

        }
        agent.SetActive(false);
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui initialise les attributs
     * 
     * Derniere modification le 15/04/2021
     */

    public void Initialisation()
    {
        ours = our;
        cerf = cer;
        loup = lou;
        lapin = lap;
        plante1 = pl1;
        plante2 = pl2;
        Apparition(ours, nbOurs, oursS);
        Apparition(cerf, nbCerf, cerfs);
        Apparition(lapin, nbLapin, lapins);
        Apparition(loup, nbLoup, loups);
        Apparition(plante1, nbPlante1, plantes1);
        Apparition(plante2, nbPlante2, plantes2);
        nbTotal = nbCerf + nbLapin + nbLoup + nbOurs;
        nbPredateurs = nbLoup + nbOurs;
        nbProies = nbCerf + nbLapin;
        ter.GetComponent<Statistiques>().nbPredateurs = nbPredateurs;
        ter.GetComponent<Statistiques>().nbProies = nbProies;
        ter.GetComponent<Statistiques>().nbProieDebut = nbProies;
        ter.GetComponent<Statistiques>().nbPredateurDebut = nbPredateurs;
        ter.GetComponent<Statistiques>().nbEnvie = nbTotal;
        apparition = false;
        nbCerf = 0;
        nbLapin = 0;
        nbLoup = 0;
        nbOurs = 0;
    }

    //derniere modification le 22/04/2021
    public void setNbOurs(Text ours)
    {
        nbOurs = int.Parse(ours.text);
        ours.text = "0";
    }
    //derniere modification le 22/04/2021
    public void setNbCerf(Text cerf)
    {
        nbCerf = int.Parse(cerf.text);
        cerf.text = "0";
    }
    //derniere modification le 22/04/2021
    public void setNbLoup(Text loup)
    {
        nbLoup = int.Parse(loup.text);
        loup.text = "0";
    }
    //derniere modification le 22/04/2021
    public void setNbLapin(Text lapin)
    {
        nbLapin = int.Parse(lapin.text);
        lapin.text = "0";
    }
    //derniere modification le 22/04/2021
    public void setNbPlante1(Text plante1)
    {
        nbPlante1 = int.Parse(plante1.text);
        plante1.text = "0";
    }
    //derniere modification le 22/04/2021
    public void setNbPlante2(Text plante2)
    {
        nbPlante2 = int.Parse(plante2.text);
        plante2.text = "0";
    }

    //derniere modification le 22/04/2021
    public void setAppartionTrue()
    {
        apparition = true;
    }

    /** Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de remettre à 0 les variables apres une simulation. 
     * 
     * derniere modification le 22/04/2021
     */
    public void Recommencer()
    {
        nbCerf = 0;
        nbHerbes = 0;
        nbLapin = 0;
        nbLoup = 0;
        nbPlante1 = 0;
        nbPlante2 = 0;
        secondes = 0;
    }
}
