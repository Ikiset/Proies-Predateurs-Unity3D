using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//version 2.0
public class Agent : MonoBehaviour
{
    public GameObject camPrincipale;
    public GameObject cam;
    public bool choisi;
    public bool bug;
    public double frequenceReproduction;
    public string[] regimeA = new string[5];
    public float poids = 10f;
    public bool arrive = false;
    public Terrain ter;
    public NavMeshAgent agent;
    [SerializeField]
    public float vitesseChasser;
    public int portee = 5;
    private Genes genes;
    public float besoinEnerg = 200f;
    public int besoinEnergMax = 200;
    public int besoinEnergMin = 200;
    public float apportEnerg = 30f;
    public float gainParGorgee = 12f;
    [SerializeField]
    private int age;
    public float x;
    public float y;
    public float z;
    [SerializeField]
    public float vitesse = 10f;
    public float vitesseMax;
    public float vitesseMin;
    public int esperanceVie;
    public float esperenceDeVieMin;
    public float esperenceDeVieMax;
    public float frequenceReproductionMin;
    public float frequenceReproductionMax;
    public int genre; // 1 signifie que l'agent est un Mâle et 0 une femelle
    public bool veutSeReproduire;
    public bool estEnceinte = false;
    private Genes genesPartenaire;
    public float t;//temps neccesaire jusqu a l'accouchement;
    public float jour;
    public int ageMaturation;
    public bool estAdulte;
    public int nbNouveauNees;
    public double ageMaturationMin;
    public double ageMaturationMax;
    public double faim;
    public bool nouveauNee;
    public float tempsRestantDigestion;
    [SerializeField]
    public bool enVie;
    public float tempsDigestion;
    public float tempsConsommationProie;
    public bool enFuite = false;
    public bool seReproduit;
    public Animator animation;
    public GameObject partenaireP = null;
    public int id;
    public bool estAutotrophe;
    public bool mort = false;
    public bool attaque;
    public int jetAttaque;
    public bool gagneCombat;
    public float tempsAvantProchaineChasse;
    public bool finCombat;
    public float sante;
    public int jetBlessure;
    public bool enChasse;
    public double soif;
    public bool finiDeBoire = false;
    public float besoinHydriqueMax;
    public float besoinHydriqueMin;
    public float besoinHydrique = 200f;
    public float hydratation;
    public float tempsRestantHydratation;
    public int chance;
    public bool selection;
    public float croissance;
    public bool boit;
    public bool mange;
    public GameObject selectione;
    public bool necrophage;
    public int force;
    public bool intraGuilde;
    public bool entrainDeDormir;
    public bool noctambule;
    public GameObject pointEauRecent;
    //formule :
    void Start()
    {

        Affectation();
        if (!estAutotrophe)
        {
            agent.speed = vitesse;//permet d'initialiser la vitesse de l'agent
            x = Random.Range(0, ter.terrainData.size.x);//on lui donne des coordonnées aléatoire situées dans le terrain
            z = Random.Range(0, ter.terrainData.size.z);
            y = ter.terrainData.GetHeight((int)x, (int)z);
            agent.SetDestination(new Vector3(x, y, z));//l'agent se déplace ainsi à la position donné par les coordonnées aléatoires
        }

    }


    /**Fait par Chamsi Ben kaarbar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de selectionner un agent lorsqu'on lui clique desssus pendant la simulation.
     * 
     * Derniere modification le 20/04/2021
     */
    private void OnMouseDown()
    {
        if (gameObject.tag == "auto" || !camPrincipale.activeSelf)
        {
            return;
        }
        GameObject[] agents = GameObject.FindGameObjectsWithTag(gameObject.tag);
        //si l'agent est selectionné alors on le désélectionne
        if (selection)
        {
            selection = false;
            selectione.SetActive(selection);

            if (gameObject.tag == "Proie")
            {
                ter.GetComponent<ChangementCamera>().selectCerf = false;
            }
            if (gameObject.tag == "loup")
            {
                ter.GetComponent<ChangementCamera>().selectLoup = false;
            }
            if (gameObject.tag == "lapin")
            {
                ter.GetComponent<ChangementCamera>().selectLapin = false;
            }
            if (gameObject.tag == "Ours")
            {
                ter.GetComponent<ChangementCamera>().selectOurs = false;
            }
            return;
        }

        //selectionne l'agent courant et désélectionne le dernier agent selectionné.
        for (int i = 0; i < agents.Length; i++)
        {
            if (agents[i].GetComponent<Agent>().selection && agents[i].GetComponent<Agent>().tag == gameObject.tag)
            {
                agents[i].GetComponent<Agent>().selection = false;
                agents[i].GetComponent<Agent>().selectione.SetActive(false);
            }
        }

        if (gameObject.tag == "Proie")
        {
            ter.GetComponent<ChangementCamera>().selectCerf = true;
        }
        if (gameObject.tag == "loup")
        {
            ter.GetComponent<ChangementCamera>().selectLoup = true;
        }
        if (gameObject.tag == "lapin")
        {
            ter.GetComponent<ChangementCamera>().selectLapin = true;
        }
        if (gameObject.tag == "Ours")
        {
            ter.GetComponent<ChangementCamera>().selectOurs = true;
        }

        selection = true;
        selectione.SetActive(selection);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.tag == "auto")
        {
            Mort();
            MiseAJour();
        }

        Mort();

        if (!enVie)
        {
            apportEnerg -= Time.deltaTime;
        }

        if (enVie && gameObject.tag != "auto")
        {
            MiseAJour();




            if (!estAutotrophe)
            {
                Fuite();
                Dormir();
            }

            if (!enFuite && !estAutotrophe)
            {
                if (besoinEnerg >= faim)
                {
                    Deplacement();
                    PredationIntraGuilde();
                }
                else
                {
                    Chercher();
                    Chasser();


                }
            }


            if (besoinHydrique <= soif && !enChasse)
            {
                finiDeBoire = false;

                if (tempsRestantHydratation <= 0)
                    StartCoroutine(Boire());
            }

            if (t <= 0 && estEnceinte && !estAutotrophe)
            {
                NaissanceReproduction();
                estEnceinte = false;
                animation.SetBool("Idle", false);
                animation.SetBool("Walk", true);
                agent.isStopped = false;
                frequenceReproduction = genes.genes[2];
            }
            if (veutSeReproduire && !estEnceinte && !seReproduit && !estAutotrophe)
            {
                StartCoroutine(Reproduction());
            }

        }



    }
    /**
     * 
     * Méthode qui permet de mettre à jour les attributs des agents en fonction du temps.
     * 
     * Derniere modification le 24/04/2021
     */
    void MiseAJour()
    {
        jour += Time.deltaTime;
        if (jour > 100)
        {
            if (!estAdulte)
            {
                gameObject.transform.localScale += new Vector3(croissance, croissance, croissance);
            }
            age++;
            jour = 0;
        }
        if (tempsRestantDigestion > 0)
            tempsRestantDigestion -= Time.deltaTime;

        if (age >= ageMaturation)
        {
            estAdulte = true;
            if (frequenceReproduction > 0)
                frequenceReproduction -= Time.deltaTime;
            if (t > 0)
                t -= Time.deltaTime;
        }
        if (t < 3 && estEnceinte)//permet que un animal enceinte ne bouge pas lorsqu'il va faire naître
        {
            animation.SetBool("Walk", false);
            animation.SetBool("Idle", true);
            agent.isStopped = true;
        }
        if (frequenceReproduction <= 0)
        {
            veutSeReproduire = true;
        }

        if (sante > 0 && sante < 200)
        {
            if (sante + Time.deltaTime * 1.5 < 200)
            {
                sante += Time.deltaTime * 1.5f;
            }

            else
            {
                sante = 200;
            }
        }


        if (tempsAvantProchaineChasse > 0)
            tempsAvantProchaineChasse -= Time.deltaTime;

        if (tempsAvantProchaineChasse <= 0)
            finCombat = false;

        if (tempsRestantHydratation > 0)
        {
            tempsRestantHydratation -= Time.deltaTime;
        }

        if (tempsRestantHydratation < 0)
            tempsRestantHydratation = 0;

        if (besoinHydrique > 0)
        {
            if (!entrainDeDormir)
            {
                if (enChasse || enFuite)
                {
                    besoinHydrique -= Time.deltaTime * 0.4f;
                    print("le besoin hydrique baisse plus vite");

                }

                else
                {
                    besoinHydrique -= Time.deltaTime * 0.2f;
                    print("le besoin hydrique baisse normalement");
                }
            }
            else
            {
                besoinHydrique -= Time.deltaTime * 0.1f;
                print("le besoin hydrique baisse moins vite");
            }
        }

        if (tempsRestantDigestion > 0)
            tempsRestantDigestion -= Time.deltaTime;

        if (besoinEnerg > 0)
        {

            if (!entrainDeDormir)
            {
                if (enChasse || enFuite)
                {
                    besoinEnerg -= Time.deltaTime * 0.4f;
                    print("le besoin energétique baisse plus vite");

                }

                else
                {
                    besoinEnerg -= Time.deltaTime * 0.2f;
                    print("le besoin energétique baisse normalement");
                }
            }
            else
            {
                besoinEnerg -= Time.deltaTime * 0.1f;
                print("le besoin energétique baisse moins vite");
            }
        }



    }

    /**
     * 
     * Méthode qui permet l'initialisation des attributs de la classe Agent
     * 
     */
    void Affectation()
    {
        bug = false;
        animation.SetBool("Walk", true);
        if (!nouveauNee)
        {
            genes = new Genes(vitesseMin, vitesseMax, frequenceReproductionMin, frequenceReproductionMax, (float)ageMaturationMin, (float)ageMaturationMax, (float)besoinEnergMin, (float)besoinEnergMax, esperenceDeVieMin, esperenceDeVieMax, besoinHydriqueMin, besoinHydriqueMax);
        }

        partenaireP = null;
        age = 0;
        vitesse = genes.genes[0];
        genre = (int)genes.genes[1];
        vitesseChasser = vitesse + 2;
        frequenceReproduction = genes.genes[2];
        ageMaturation = (int)genes.genes[3];
        besoinEnerg = genes.genes[4];
        esperanceVie = (int)genes.genes[5];
        besoinHydrique = genes.genes[6];

        veutSeReproduire = false;
        seReproduit = false;
        jour = 0;
        enVie = true;
        enFuite = false;
        if (age >= ageMaturation)
        {
            estAdulte = true;
        }
        else
        {
            estAdulte = false;
        }
        id = Random.Range(0, 50000000);
        attaque = false;
        sante = 200f;
        tempsAvantProchaineChasse = 0f;
        finCombat = false;
        gagneCombat = false;
        hydratation = 15f;
        faim = besoinEnerg * 0.7;
        soif = besoinHydrique * 0.7;

        selection = false;
        necrophage = false;
        noctambule = false;
        entrainDeDormir = false;
		pointEauRecent = null;
    }

    /// Auteur : Julien Dai
    /// <summary>
    /// Déplacement aléatoire autour de l'agent avec une grande chance d'aller enface
    /// </summary>
    void Deplacement()
    {
        agent.speed = vitesse;

        if (arrive)
        {
            float directionAleatoire = -90;
            // un nombre tel que 360 est divisible par ce nombre
            int nombreTirage = 5; //Plus ce nombre est haut plus l'agent va se diriger vers l'avant
            for (int i = 0; i < nombreTirage; i++)
            {
                directionAleatoire += Random.Range(0, (360 / nombreTirage) + 1);
            }
            directionAleatoire *= Mathf.PI / 180;
            //print("déplacement : " + directionAleatoire / Mathf.PI);
            float directionActuel = (360 - transform.localEulerAngles.y % 360) * Mathf.PI / 180;
            Vector3 nouvelleDirection = new Vector3(Mathf.Cos(directionActuel + directionAleatoire), 0, Mathf.Sin(directionActuel + directionAleatoire));
            int distance = 10 + Random.Range(0, portee * 2);
            if (agent.SetDestination(transform.position + nouvelleDirection * distance))
            {
                arrive = false;
                if ((agent.transform.position - agent.destination).magnitude < distance * 0.99)
                {
                    if (Random.Range(0, 3) > 1)
                    {
                        agent.SetDestination(transform.position + (nouvelleDirection + (EviteObstcle() - transform.forward)) * distance);
                    }
                    else
                    {
                        agent.SetDestination(transform.position + (nouvelleDirection + EviteObstcle()) * distance);
                    }
                }
            }
        }
        if (new Vector2(
            agent.destination.x - agent.transform.position.x,
            agent.destination.z - agent.transform.position.z
            ).magnitude < 2)
        {
            arrive = true;
        }
    }

    /// Auteur : Julien Dai
    /// <summary>
    /// Déplacement aléatoire autour de l'agent avec une vitesse plus élevé et une chance plus grande de se diriger vers l'avant
    /// </summary>
    void Chercher()
    {
        agent.speed = vitesse * 1.5f;//lorque l'agent a faim, sa vitesse augmente un peu
        if (arrive)//si l'agent l'agent est arrivé à sa destination
        {
            float directionAleatoire = -90;
            // un nombre tel que 360 est divisible par ce nombre
            int nombreTirage = 12; //Plus ce nombre est haut plus l'agent va se diriger droit devant
            for (int i = 0; i < nombreTirage; i++)
            {
                directionAleatoire += Random.Range(0, (360 / nombreTirage) + 1);
            }
            directionAleatoire *= Mathf.PI / 180;
            //print("chercher : "+directionAleatoire / Mathf.PI);
            float directionActuel = (360 - transform.localEulerAngles.y % 360) * Mathf.PI / 180;
            Vector3 nouvelleDirection = new Vector3(Mathf.Cos(directionActuel + directionAleatoire), 0, Mathf.Sin(directionActuel + directionAleatoire));
            int distance = 10 + Random.Range(0, portee * 2);
            if (agent.SetDestination(transform.position + nouvelleDirection * distance))
            {
                arrive = false;
                if ((agent.transform.position - agent.destination).magnitude < distance * 0.99)
                {
                    //print("chercher evite obstacle");
                    if (Random.Range(0, 3) > 1) {
                        agent.SetDestination(transform.position + (nouvelleDirection + (EviteObstcle() - transform.forward)) * distance);
                    }
                    else
                    {
                        agent.SetDestination(transform.position + EviteObstcle() * distance);
                    }
                }
            }
        }
        if (new Vector2(
            agent.destination.x - agent.transform.position.x,
            agent.destination.z - agent.transform.position.z
            ).magnitude < 2)
        {
            arrive = true;
        }
    }



    /**Fait par Chamsi Ben kaarbar et Chiheb Ben jamaa
     * 
     *Méthode qui permet à un agent de chasser. 
     *
     *Modif: 20/04/2021 gestion de la nécrophagie, Karam Raxi
     *
     * Derniere modification le 22/04/2021
     */
    void Chasser()
    {
        double dist;
        double distanceMin;//variable permettant de stocker la plus petite distance de la cible la plus proche
        List<Agent> proies = new List<Agent>();//Liste qui stock les proies de l'agent
        RepererAgents(proies, true);//permet de repérer les agents
        Agent proieP = null;//initialisation de la proies la plus proche
        distanceMin = double.PositiveInfinity;//initialisation de la 1er proie comme la distance la plus proche de la proie la plus proche

        for (int i = 0; i < proies.Count; i++)//double boucle permettant de parcourir les espèces proies de cet agent 
        {
            if (!necrophage)
            {
                if (proies[i].apportEnerg > 0 && proies[i].enVie)
                {
                    dist = Vector3.Distance(agent.transform.position, proies[i].transform.position);//calcul la distance entre l'agent et une proie
                    if (dist < distanceMin)//permet de savoir quelle est la proie la plus proche de l'agent 
                    {
                        proieP = proies[i];//stocke la proie courante la plus proche
                        distanceMin = dist;//et sa distance aussi
                    }
                }
            }
            else
            {
                if (proies[i].apportEnerg > 0)
                {
                    dist = Vector3.Distance(agent.transform.position, proies[i].transform.position);//calcul la distance entre l'agent et une proie
                    if (dist < distanceMin)//permet de savoir quelle est la proie la plus proche de l'agent 
                    {
                        proieP = proies[i];//stocke la proie courante la plus proche
                        distanceMin = dist;//et sa distance aussi
                    }
                }
            }
        }
        if (proieP != null && distanceMin < portee && tempsRestantDigestion <= 0 && finCombat == false)
        {//si la distance entre la proie la plus proche et l'agent est inferieur à la portee de l'agent alors l'agent se met à la poursuite de la proie
            enChasse = true;
            agent.speed = vitesseChasser;//ainsi la vitesse de l'agent augmente
            animation.SetBool("Walk", false);
            if (!animation.GetBool("Eat"))
            {
                animation.SetBool("Run", true);
            }
            agent.SetDestination(proieP.transform.position);//l'agent poursuit la proie
        }
        else
        {
            animation.SetBool("Run", false);
            animation.SetBool("Walk", true);
        }
        if (proieP != null && Vector3.Distance(agent.transform.position, proieP.transform.position) < 1.5f && !proieP.boit && !proieP.seReproduit && !proieP.mange)//si l'agent est à une distance très proche pour manger la proie
        {
            if (proieP.GetComponent<Agent>().enVie && tempsRestantDigestion <= 0)
            {

                StartCoroutine(Attaque(proieP));//attaque la proie

                proieP.GetComponent<Agent>().attaque = false;
                attaque = false;
                if (gagneCombat)//si le predateur gagne le combat alors il mange la proie
                {
                    StartCoroutine(Manger(proieP));//mange la proie
                }

            }
            else if (tempsRestantDigestion <= 0)
            {
                StartCoroutine(Manger(proieP));//mange la proie
            }

        }

    }

    /**Fait par Karam Raxi et Julien Dai
     * Méthode qui permet à l'agent prédateur d'attaquer sa proie 
     */
    IEnumerator Attaque(Agent proie)
    {
        if (!proie.estAutotrophe) //si la proie n'est pas un agent autotrophe alors elle s'arrête.
        {
            proie.agent.isStopped = true;
            proie.animation.SetBool("Walk", false);
            proie.animation.SetBool("Run", false);
        }
        agent.isStopped = true; //le prédateur s'arrête
        animation.SetBool("Run", false);
        animation.SetBool("Attack", true);
        proie.attaque = true;
        attaque = true;
        Defense(proie); //la proie essaye de se défendre contre le prédateur.
        yield return new WaitForSeconds(1);
        //proie.GetComponent<Agent>().animation.SetBool("Die", true);
        //proie.GetComponent<Agent>().enVie = false;
        animation.SetBool("Attack", false);

        if (!gagneCombat) //si la proie a réussi à se défendre alors les deux agents ne sont plus à l'arrêt
        {
            animation.SetBool("Walk", true);
            proie.agent.isStopped = false;
            agent.isStopped = false;
        }

        //proie.enVie = false;

    }

    /**Fait par Karam Raxi et Julien Dai
    * Méthode qui permet à un agent de manger sa proie. 
    * L'agent gagne de l'énergie après un temps de digestion
    */
    IEnumerator Manger(Agent proie)
    {
        float pApport = proie.apportEnerg;
        agent.isStopped = true;  //l'agent s'arrête pour manger.
        animation.SetBool("Walk", false);
        animation.SetBool("Run", false);
        animation.SetBool("Eat", true);
        float gorgee = gainParGorgee < pApport ? gainParGorgee : pApport; //la proie pert son apport énergétique en fonction de la quantité de nouriture ingurgitée par le prédateur.
        proie.apportEnerg -= gorgee;
        tempsRestantDigestion = tempsDigestion + tempsConsommationProie; //le prédateur est en digestion.
        mange = true;
        yield return new WaitForSeconds(tempsConsommationProie); //le prédateur consomme sa proie pendant un certain temps.

        animation.SetBool("Eat", false);
        besoinEnerg += gorgee; //le prédateur gagne de l'énergie en fonction de la quantité de nouriture ingurgitée.
        animation.SetBool("Walk", true);
        agent.isStopped = false; // le prédateur n'est plus à l'arrêt.
        mange = false;
    }
    // Modif : 18/04/2021 Changement en OverlapSphere, Julien Dai
    // Ajout le 18/04 : ajout dans la base de connaisance les point d'eau aperçu
    //Méthode qui permet de repérer les proies ou prédateurs qui sont dans la porté de l'agent
    //Méthode qui prend en argument :
    //    agents une liste de GameObject qui permet de récuperer les proies ou prédateurs présent dans la porté de l'agent
    //    chercheProie true si on cherche les proies, false si on cherche les prédateurs
    void RepererAgents(List<Agent> agents, bool chercheProie)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, portee);
        foreach (Collider collider in colliders)
        {
            Agent agent = collider.GetComponent<Agent>();
            if (agent != null)
            {
                if (chercheProie)
                {
                    foreach (string regime in regimeA)
                    {
                        if (collider.tag == regime)
                        {
                            agents.Add(agent);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (string regime in agent.regimeA)
                    {
                        if (tag == regime)
                        {
                            if (!agent.gameObject.Equals(gameObject))
                            {
                                agents.Add(agent);
                            }
                            break;
                        }
                    }
                }
            }
            /*else if (collider.CompareTag("PointEau"))
            {
                pointEau.Add(collider.gameObject);
                print("ajout point d'eau " + collider + " " + collider.gameObject);
            }*/
        }
    }
    // Modif : 17/04/2021 traitement bordure et direction fuite, Dai Julien
    //Méthode qui permet à un agent proie de fuir un prédateur.
    // Fait par Julien Dai et Karam Raxi
    void Fuite()
    {
        List<Agent> predateurP = new List<Agent>();//variable permettant de stocker le prédateur dans sa porté

        RepererAgents(predateurP, false);
        if (predateurP.Count > 0)
        {
            //print("Fuite detecter");
            enFuite = true;
            animation.SetBool("Walk", false);
            animation.SetBool("Run", true);
            agent.speed = vitesseChasser;//Mettre une autre vitesse pour fuir
            Vector3 directionFuite = Vector3.zero;
            for (int i = 0; i < predateurP.Count; i++)
            {
                directionFuite += agent.transform.position - predateurP[i].transform.position;
            }
            float modulo = directionFuite.magnitude;
            directionFuite = directionFuite / modulo * portee;
            directionFuite.y = 0;
            Vector3 obstacle = EviteObstcle();
            for (int i = 0; i < 5; i++)
            {
                if (agent.SetDestination(agent.transform.position + directionFuite + obstacle))
                {
                    break;
                }
                directionFuite /= 2;
            }
        }
        else if (transform.position.x == agent.destination.x && transform.position.z == agent.destination.z)
        {
            enFuite = false;
            animation.SetBool("Run", false);
            animation.SetBool("Walk", true);
        }

    }

    // Auteur : Julien Dai
    // Action d'esquiver les murs, ou obstacles, s'il les voit dans sa portée de vu (actuellement 45° gauche et droite de sa vue)
    // Méthode utilisé dans Fuite()
    // Retourne un Vector3 qui indique si l'agent doit tourner à gauche ou à droite
    Vector3 EviteObstcle()
    {
        Vector3 direction = Vector3.zero;
        RaycastHit hit;
        Vector3 centre = gameObject.GetComponent<Collider>().bounds.center;
        // Liste qui recupère les laser touchant un obstable
        List<RaycastHit> vuGauche = new List<RaycastHit>(),
                    vuDroite = new List<RaycastHit>();
        int nombreLaser = 1; // Nombre de laser de coté (mieu de mettre en tant qu'attribue)
        float angle = 90; // L'angle de vue de l'agent (mieu de mettre en tant qu'attribue)
        for (int i = 1; i <= nombreLaser; i++)
        {
            float angleI = angle / 2 / nombreLaser * i; // Recherche l'angle pour les differents laser
            angleI = angleI * Mathf.PI / 180;   // Conversion en radiant
            float localAngle = (360 - transform.localEulerAngles.y % 360) * Mathf.PI / 180; // L'angle actuel en radiant (ainsi que le traiter dans le bon sens)
            // Vue à gauche
            Vector3 vecteurG = new Vector3(Mathf.Cos(localAngle + Mathf.PI / 2 + angleI), 0, Mathf.Sin(localAngle + Mathf.PI / 2 + angleI));
            if (Physics.SphereCast(centre, 1, vecteurG, out hit, portee))
            {
                if (hit.collider.tag == "Bord" || hit.collider.tag == "Obstacle")
                {
                    vuGauche.Add(hit);
                }
            }
            // Vue à droite
            Vector3 vecteurD = new Vector3(Mathf.Cos(localAngle + Mathf.PI / 2 - angleI), 0, Mathf.Sin(localAngle + Mathf.PI / 2 - angleI));
            if (Physics.SphereCast(centre, 1, vecteurD, out hit, portee))
            {
                if (hit.collider.tag == "Bord" || hit.collider.tag == "Obstacle")
                {
                    vuDroite.Add(hit);
                }
            }
        }
        if (vuGauche.Count > vuDroite.Count)
        {
            direction += transform.right;
        }
        else if (vuGauche.Count > vuDroite.Count)
        {
            direction -= transform.right;
        }
        else if (vuGauche.Count > 0 || vuDroite.Count > 0)
        {
            float distanceMinLeft = float.MaxValue;
            float distanceMinRight = float.MaxValue;
            foreach (RaycastHit h in vuGauche)
            {
                distanceMinLeft = distanceMinLeft > h.distance ? h.distance : distanceMinLeft;
            }
            foreach (RaycastHit h in vuDroite)
            {
                distanceMinRight = distanceMinRight > h.distance ? h.distance : distanceMinRight;
            }
            direction += distanceMinLeft < distanceMinRight ? transform.right : -transform.right;
        }
        return direction;
    }




    /**Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de créer un nouveau agent issus de la reproduction
     * 
     * derniere modification 13/04/2021
     */
    void NaissanceReproduction()
    {
        //si il y a au moins 100 agents sur le terrain alors on annule la naissance.
        if (ter.GetComponent<Statistiques>().nbEnvie >= 100)
        {
            return;
        }

        GameObject enfant;
        //On crée un nouveau agent en faisant une copie de la mère, puis on l'initialise en tant que nouveau née, on lui donne une petite taille, de nouveau gene ect.
        for (int i = 0; i < nbNouveauNees; i++)
        {
            enfant = Instantiate(gameObject, new Vector3(agent.transform.position.x, 0, agent.transform.position.z), Quaternion.identity);
            enfant.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            enfant.GetComponent<Agent>().estEnceinte = false;
            List<float> t = genes.CroissementGenes(genesPartenaire);//on fait un croisement de gene entre les genes de la mère et du père
            enfant.GetComponent<Agent>().nouveauNee = true;
            enfant.GetComponent<Agent>().genes = new Genes();
            enfant.GetComponent<Agent>().genes.genes = t;
            enfant.GetComponent<Agent>().veutSeReproduire = false;
            enfant.GetComponent<Agent>().jour = 0;
            seReproduit = false;
            enfant.GetComponent<Agent>().age = 0;
            enfant.GetComponent<Agent>().selection = false;
            enfant.GetComponent<Agent>().selectione.SetActive(false);
            enfant.GetComponent<Agent>().croissance = 1 / enfant.GetComponent<Agent>().genes.genes[3];
        }
        miseAJourStatistique(true);//on met à jour les statistiques
        ter.GetComponent<Statistiques>().nbEnvie += nbNouveauNees;
        ter.GetComponent<Statistiques>().nbReproduction++;

    }


    /**Fait par Chamsi Ben kaarbar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de faire la reproduction entre deux agents de genres différents.
     * 
     * Derniere modification le 17/04/2021
     */
    IEnumerator Reproduction()
    {


        //s'il a envie de se reproduire alors il chercher un(e) partenaire
        if (frequenceReproduction <= 0)
        {
            veutSeReproduire = true;
            double dist;
            double distanceMin = 10000;//variable permettant de stocker la plus petite distance de la cible la plus proche
            //partenaireP = null;
            GameObject[] partenaire = GameObject.FindGameObjectsWithTag(gameObject.tag);
            if (!bug)
            {
                //Cherche le partennaire le plus proche de lui et disponible.
                for (int i = 0; i < partenaire.Length; i++)
                {
                    if (partenaire[i].GetComponent<Agent>().genre != genre && partenaire[i].GetComponent<Agent>().veutSeReproduire && !partenaire[i].GetComponent<Agent>().agent.isStopped && !partenaire[i].GetComponent<Agent>().seReproduit)
                    {
                        dist = Vector3.Distance(agent.transform.position, partenaire[i].transform.position);
                        if (dist < distanceMin)
                        {
                            partenaireP = partenaire[i];
                            distanceMin = dist;
                        }
                    }
                }
            }

            //permet de gerer si deux agents veulent se reproduire avec le meme agent du sexe opposé 
            for (int i = 0; i < partenaire.Length; i++)
            {
                if (partenaireP != null && partenaireP == partenaire[i].GetComponent<Agent>().partenaireP && gameObject.name != partenaire[i].name && Vector3.Distance(partenaireP.transform.position, partenaire[i].transform.position) < portee * 1.5)
                {
                    if (id > partenaire[i].GetComponent<Agent>().id)//On regarde quel agent à le plus grand id (la valeur est géneré aléatoirement lors de sa création), l'agent qui a la plus grande valeur gagne et se reproduit avec le/la partenaire.
                    {
                        partenaire[i].GetComponent<Agent>().partenaireP.GetComponent<Agent>().partenaireP = gameObject;
                        partenaire[i].GetComponent<Agent>().partenaireP = null;
                    }
                    else
                    {
                        partenaireP.GetComponent<Agent>().partenaireP = partenaire[i];
                        partenaireP = null;
                    }
                }
            }

            //si les deux partenaires sont assez proches, alors ils se rapprochent
            if (partenaireP != null && Vector3.Distance(agent.transform.position, partenaireP.transform.position) < portee * 1.5)
            {
                bug = true;
                seReproduit = true;
                agent.SetDestination(partenaireP.transform.position - new Vector3(1, 0, 1));
                partenaireP.GetComponent<Agent>().partenaireP = gameObject;

                if (partenaireP != null && Vector3.Distance(agent.transform.position, partenaireP.transform.position) < 3.5f)//si les deux partenaires sont très proches, alors ils se reproduisent
                {
                    animation.SetBool("Walk", false);
                    animation.SetBool("Idle", true);
                    agent.isStopped = true;
                    yield return new WaitForSeconds(5);
                    if (agent.GetComponent<Agent>().genre == 0)//pour la femmelle on lui initialise ses variables concernant la naissance
                    {
                        t = 30;
                        estEnceinte = true;
                    }
                    //on récupère les genes des deux parents pour la partie génétique 
                    if (partenaireP != null && agent.GetComponent<Agent>().genre == 0)
                    {
                        genesPartenaire = partenaireP.GetComponent<Agent>().genes;
                    }
                    else if (partenaireP != null)
                    {
                        genesPartenaire = agent.GetComponent<Agent>().genes;
                    }
                    frequenceReproduction = genes.genes[2];
                    veutSeReproduire = false;
                    bug = false;
                }
                //partenaireP.GetComponent<Agent>().seReproduit = false;
                animation.SetBool("Idle", false);
                animation.SetBool("Walk", true);
                agent.isStopped = false;
                seReproduit = false;
                partenaireP = null;

            }
        }
    }

    /** Fait par Karam Raxi et Julien Dai.
     * Méthode qui permet de faire mourir un agent.
    */
    void Mort()
    {

        if (besoinEnerg < 0) //si l'agent ne s'est pas assez nourrit, il meurt.
        {
            enVie = false;
        }
        if (esperanceVie < age)  //si l'agent est trop vieux, il meurt.
        {
            enVie = false;
        }

        if (sante <= 0) //si l'agent est trop blessé, il meurt.
        {
            enVie = false;
        }

        if (besoinHydrique < 0) //si l'agent ne s'est pas assez abreuver en eau, il meurt.
        {
            enVie = false;
        }

        if (!enVie)
        {

            //agent.GetComponent<BoxCollider>().isTrigger = true;

            if (!estAutotrophe)
            {
                agent.isStopped = true;
                animation.SetBool("Idle", false);
                animation.SetBool("Run", false);
                animation.SetBool("Walk", false);
                animation.SetBool("Die", true);
            }

            //if (apportEnerg <= 0)
            //{
            //    Destroy(this.gameObject);
            //}
            /*if (gameObject.tag == "auto")
            {
                Destroy(this.gameObject, tempsConsommationProie);

            }*/

            else if (!mort)
            {
                // miseAJourStatistique(false);
                // Destroy(this.gameObject, 4);
                //ter.GetComponent<Statistiques>().nbMortsTotal++;
                // ter.GetComponent<Statistiques>().nbEnvie--;
                mort = true;
            }

            if (apportEnerg <= 0)
                Destroy(this.gameObject); //l'agent disparait du terrain.
        }
    }


    /** Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de mettre à jour les statistiques.
     * 
     * derniere modification 13/04/2021
     */
    public void miseAJourStatistique(bool incr)
    {
        if (!incr && (gameObject.tag == "Ours" || gameObject.tag == "loup"))
        {
            ter.GetComponent<Statistiques>().nbPredateurs--;
            ter.GetComponent<Statistiques>().predateurs.Add(ter.GetComponent<Statistiques>().nbPredateurs);
            ter.GetComponent<Statistiques>().tempsPredateurs.Add(ApparitionAgents.secondes);
        }
        if (incr && (gameObject.tag == "Ours" || gameObject.tag == "loup"))
        {
            ter.GetComponent<Statistiques>().nbPredateurs += nbNouveauNees;
            ter.GetComponent<Statistiques>().predateurs.Add(ter.GetComponent<Statistiques>().nbPredateurs);
            ter.GetComponent<Statistiques>().tempsPredateurs.Add(ApparitionAgents.secondes);
        }
        if (!incr && !(gameObject.tag == "Ours" || gameObject.tag == "loup"))
        {
            ter.GetComponent<Statistiques>().nbProies--;
            ter.GetComponent<Statistiques>().proies.Add(ter.GetComponent<Statistiques>().nbProies);
            ter.GetComponent<Statistiques>().tempsProies.Add(ApparitionAgents.secondes);
        }
        if (incr && !(gameObject.tag == "Ours" || gameObject.tag == "loup"))
        {
            ter.GetComponent<Statistiques>().nbProies += nbNouveauNees;
            ter.GetComponent<Statistiques>().proies.Add(ter.GetComponent<Statistiques>().nbProies);
            ter.GetComponent<Statistiques>().tempsProies.Add(ApparitionAgents.secondes);
        }
    }

    /**Fait par Karam Raxi
      * Méthode qui permet à un agent de boire.
      */
    IEnumerator Boire()
    {
        List<GameObject> pointEau = new List<GameObject>(); //Liste de GameObject permettant de stocker tous les points d'eau du terrain.
        GameObject eauP = null; //Variable permettant de représenter le point d'eau le plus proche.
        double distance; //variable permettant de stocker la distance entre l'agent et un point d'eau.
        double distanceMin = double.PositiveInfinity; ; //variable permettant de stocker la plus petite distance entre l'agent et le point d'eau le plus proche.

        GameObject[] eaux = GameObject.FindGameObjectsWithTag("pointEau"); // On stocke tous les points d'eau du terrain dans un tableau.

        for (int i = 0; i < eaux.Length; i++) //On ajoute les points d'eau à la liste.
        {
            pointEau.Add(eaux[i]);
        }

        for (int i = 0; i < pointEau.Count; i++) //On recherche le point d'eau le plus proche.
        {
            distance = Vector3.Distance(agent.transform.position, pointEau[i].transform.position);

            if (distance < distanceMin)
            {
                eauP = pointEau[i];
                distanceMin = distance;
            }
        }

        if (eauP != null && distanceMin < portee) //L'agent se déplace vers le point d'eau le plus proche.
        {
            agent.SetDestination(eauP.transform.position);
			pointEauRecent = eauP;
        }
        else if(pointEauRecent != null)
        {
            agent.SetDestination(pointEauRecent.transform.position);
        }
        if (eauP != null && Vector3.Distance(agent.transform.position, eauP.transform.position) < 1f) //Si l'agent est assez proche du point d'eau...
        {
            agent.isStopped = true; //Il s'arrête
            animation.SetBool("Walk", false);
            animation.SetBool("Eat", true);
            boit = true;
            yield return new WaitForSeconds(tempsConsommationProie); //Il boit pendant un certain temps.
            print("l'agent boit");

            if (tempsRestantHydratation <= 0)
            {
                tempsRestantHydratation = hydratation + tempsConsommationProie; //Le temps nécessaire avant qu'il s'abreuve à nouveau est réinitialisé.
            }

            animation.SetBool("Eat", false);

            if (besoinHydrique + gainParGorgee < besoinEnergMax)
            {
                besoinHydrique += gainParGorgee; //L'agent s'hydrate en fonction de la quantité d'eau qu'il avale.
            }

            else if (!finiDeBoire)
            {
                besoinHydrique += besoinHydriqueMax - besoinHydrique; // L'agent s'hydrate en fonction de la quantité d'eau qu'il avale.
                finiDeBoire = true;
            }

            animation.SetBool("Walk", true);
            agent.isStopped = false;
            boit = false;
        }
    }

    /**Fait par Karam Raxi
     * Méthode qui permet de simuler une confrontation entre une proie et un prédateur.
     */
    void Defense(Agent proie)
    {
        if (proie.attaque == true && attaque == true)
        {
            proie.jetAttaque = Random.Range(0, proie.GetComponent<Agent>().chance); //La proie obient un nombre aléatoire.
            jetAttaque = Random.Range(0, chance); //Le prédateur obtient un nombre aléatoire.

            proie.jetBlessure = Random.Range(0, 9); //La blessure de la proie est plus ou moins grave aléatoirement.

            //print("Jet blessure de la proie de base est de " + proie.GetComponent<Agent>().jetBlessure);

            jetBlessure = Random.Range(0, 9); //La blessure du prédateur est plus ou moins grave aléatoirement.

            //print("Jet blessure du prédateur de base est de " + jetBlessure);

            if (besoinEnerg >= proie.besoinEnerg) //Si le prédateur a plus d'énergie que sa proie alors il a plus de chance de gagner la confrontation.
            {
                jetAttaque += 250;
            }
            else
            {
                proie.jetAttaque += 250; //Si la proie a plus d'énergie que son prédateur alors elle a plus de chance de remporter la confrontation.
            }
            if (sante >= proie.sante) //Si le prédateur est moins blessé que sa proie alors il a plus de chance de remporter la confrontation.
            {
                jetAttaque += 250;
            }
            else
            {
                proie.jetAttaque += 250; //Si la proie est moins blessée que son prédateur alors elle a plus de chance de remporter la confrontation.
            }

            if ((jetAttaque >= proie.jetAttaque) && !finCombat) //Si le prédateur obtient un nombre aléatoire supérieur ou égal à sa proie...
            {
                print(agent.tag + " a obtenu " + jetAttaque);
                print(proie.tag + " a obtenu " + proie.GetComponent<Agent>().jetAttaque);
                print(agent.tag + " a gagné le combat");
                gagneCombat = true; //Il remporte le combat.
                finCombat = true; //Le combat s'arrête.
                proie.enVie = false; //La proie meurt.
                jetBlessure += 3; //Le prédateur a moins de chance d'être blessé.
                print("La blessure de " + agent.tag + " est " + jetBlessure);

                if (jetBlessure < 5) //Si le prédateur obtient un mauvais nombre aléatoire...
                {
                    print(agent.tag + " est blessé");
                    sante -= (10 - jetBlessure) * 10; //Il est blessé plus ou moins gravement proportionnelement à ce nombre.
                    print(agent.tag + " a " + sante + " de santé");
                }
            }

            if ((jetAttaque < proie.jetAttaque) && !finCombat) //Si la proie obient un nombre aléatoire supérieur à son prédateur.
            {
                print(agent.tag + " a obtenu " + jetAttaque);
                print(proie.tag + " a obtenu " + proie.GetComponent<Agent>().jetAttaque);
                print(proie.tag + " a gagné le combat");
                gagneCombat = false; //Le prédateur pert le combat.
                finCombat = true; //Le combat s'arrête.
                tempsAvantProchaineChasse = 10f; //Le temps nécessaire avant la prochaine confrontation du prédateur est réinitialisé.

                print("La blessure de " + proie.tag + " est " + proie.GetComponent<Agent>().jetBlessure);

                if (proie.jetBlessure < 5) //Si la proie obtient un mauvais nombre aléatoire...
                {
                    print(proie.tag + " est blessé ");
                    proie.sante -= (10 - proie.jetBlessure) * 10; //Elle est blessé plus ou moins gravement proportionnelement à ce nombre.
                    print(proie.tag + " a " + proie.GetComponent<Agent>().sante + " de santé");
                }

                print("La blessure de " + agent.tag + " est " + jetBlessure);


                if (jetBlessure < 5) //Si le prédateur obtient un mauvais nombre aléatoire...
                {
                    print(agent.tag + " est blessé ");
                    sante -= (10 - jetBlessure) * 10; //Il est blessé plus ou moins gravement proportionnelement à ce nombre.
                    print(agent.tag + " a " + sante + " de santé");
                }

                if (proie.sante <= 0) //Si la proie meurt de ses blessures pendant la confrontation.
                    gagneCombat = true; //Le prédateur remporte le combat.

                tempsRestantDigestion = tempsDigestion;
                print("agent is stopped : " + agent.isStopped);
            }
        }
    }

    /**Fait par Karam Raxi
     * 
     * Méthode permettant d'introduire le phénomène de prédation intraguilde
     */
    void PredationIntraGuilde()
    {
        double dist;
        double distanceMin = double.PositiveInfinity;
        List<Agent> proies = new List<Agent>();
        Agent proieP = null;

        print("test appel");

        Collider[] colliders = Physics.OverlapSphere(transform.position, portee);

        foreach (Collider collider in colliders) //On recherche les agents dans le champ de vision ayant au moins une ressource similaire dans le régime alimentaire
        {
            Agent agent = collider.GetComponent<Agent>();
            print(agent);
            if (agent != null)
            {
                for (int j = 0; j < agent.regimeA.Length; j++)
                {
                    for (int k = 0; k < regimeA.Length; k++)
                    {
                        if (agent.regimeA[j].Equals(regimeA[k]))
                        {
                            if (!proies.Contains(agent) && agent.tag != gameObject.tag && agent.force < force)
                            {
                                proies.Add(agent);
                                print("Agent = " + agent);
                            }
                        }
                    }
                }
            }
        }

        if (proies.Count == 0)
        {
            return;
        }

        for (int i = 0; i < proies.Count; i++)
        {

            dist = Vector3.Distance(agent.transform.position, proies[i].transform.position);//calcul la distance entre l'agent et une proie

            if (dist < distanceMin && proies[i].enVie)//permet de savoir quelle est la proie la plus proche de l'agent 
            {

                proieP = proies[i];//stocke la proie courante la plus proche
                distanceMin = dist;//et sa distance aussi
            }


        }
        if (proieP != null && distanceMin < portee && finCombat == false && tempsRestantDigestion <= 0)
        {//si la distance entre la proie la plus proche et l'agent est inferieur à la portee de l'agent alors l'agent se met à la poursuite de la proie
            enChasse = true;
            agent.speed = vitesseChasser;//ainsi la vitesse de l'agent augmente
            animation.SetBool("Walk", false);
            if (!animation.GetBool("Eat"))
            {
                animation.SetBool("Run", true);
            }
            agent.SetDestination(proieP.transform.position);//l'agent poursuit la proie
        }
        else
        {
            animation.SetBool("Run", false);
            animation.SetBool("Walk", true);
        }
        if (proieP != null && Vector3.Distance(agent.transform.position, proieP.transform.position) < 1.5f)//si l'agent est à distance très proche pour manger la proie
        {
            if (proieP.GetComponent<Agent>().enVie)
            {

                if (tempsRestantDigestion <= 0)
                {
                    StartCoroutine(Attaque(proieP));
                    proieP.GetComponent<Agent>().attaque = false;
                    attaque = false;
                    agent.isStopped = false;

                }

            }

        }
        print(agent.isStopped);
    }

    /**Fait par Karam Raxi
     * 
     *Méthode permettant aux agents de dormir 
     */
    void Dormir()
    {
        if (!noctambule)
        {
            if (GameObject.Find("Directional Light").GetComponent<DayNightCycle>().isNightTime)
            {
                print("test 1");
                agent.isStopped = true;
                entrainDeDormir = true;
                //yield return new WaitForSeconds(GameObject.Find("Directional Light").GetComponent<DayNightCycle>().NightTimeLength);
            }

            else
            {
                agent.isStopped = false;
                entrainDeDormir = false;
            }
        }

        else
        {
            print("test 2");
            if (GameObject.Find("Directional Light").GetComponent<DayNightCycle>().isDayTime)
            {
                agent.isStopped = true;
                entrainDeDormir = true;
                //yield return new WaitForSeconds(GameObject.Find("Directional Light").GetComponent<DayNightCycle>().DayTimeLength);
            }

            else
            {
                agent.isStopped = false;
                entrainDeDormir = false;
            }
        }
    }

}
