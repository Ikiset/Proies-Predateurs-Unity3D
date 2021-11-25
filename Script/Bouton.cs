using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





// La classe a été fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
//Derniere modification le 28/03/2021
public class Bouton : MonoBehaviour
{
    public int min;
    public int max;
    public Canvas c;
    public string nom;
    public List<string> parametres;
    public int i;
    public Text vitesseMin;
    public Text vitesseMax;
    public Text nbReproduction;
    public Text frequenceMin;
    public Text frequenceMax;
    public Text besoinMax;
    public Text besoinMin;
    public Text apport;
    public Text nbAgent;
    public Text ageMaturationMin;
    public Text ageMaturationMax;
    public Text tempsDigestion;
    public Text esperanceVieMin;
    public Text esperanceVieMax;
    public Text besoinHydriqueMax;
    public Text besoinHydriqueMin;
    public Terrain terrainActuel;
    public InputField tempsAutotrophe;
    public bool finSimulation;
    public GameObject[] terrain = new GameObject[3];
    public Camera camera;
    public Toggle[] selectTerrain = new Toggle[3];
    public Toggle[] vitesseCamera = new Toggle[3];
    public Toggle[] vitesseSimulation = new Toggle[4];

    public GameObject[] gameObjects = new GameObject[3];


    private void Update()
    {

    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'activer un objet
     * scene (GameObject) : l'object a rendre visible
     * 
     * Derniere modification le 20/03/2021
     */
    public void ChargerPartie(GameObject scene)
    {
        scene.SetActive(true);
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de désactiver un objet
     * scene (GameObject) : l'object a rendre visible
     * 
     * Derniere modification le 20/03/2021
     */
    public void DechargerPartie(GameObject scene)
    {
        scene.SetActive(false);
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de quitter l'application.
     * 
     * Derniere modification le 20/03/2021
     */

    public void QuitterApplication()
    {
        Application.Quit();
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'incrémenter une variable de type Text.
     * n (Text) : variable à un incrémenter
     * 
     * Derniere modification le 20/03/2021
     */
    public void Incrementation(Text n)
    {
        int i = int.Parse(n.text);
        i++;
        if (i > max)
        {
            i = max;
        }

        n.text = i.ToString();

    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de décrémenter une variable de type Text.
     * n (Text) : variable à décrémenter
     * 
     * Derniere modification le 20/03/2021
     */
    public void Decrementation(Text n)
    {
        int i = int.Parse(n.text);
        i--;
        if (i < min)
        {
            i = min;
        }
        n.text = i.ToString();
    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de modifier les paramètres de plusieurs GameObject.
     * 
     *Derniere modification le 28/03/2021
     */
    public void Parametre()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Agent>().vitesseMax = int.Parse(vitesseMax.text);
            gameObjects[i].GetComponent<Agent>().vitesseMin = int.Parse(vitesseMin.text);
            gameObjects[i].GetComponent<Agent>().nbNouveauNees = int.Parse(nbReproduction.text);
            gameObjects[i].GetComponent<Agent>().frequenceReproductionMin = int.Parse(frequenceMin.text);
            gameObjects[i].GetComponent<Agent>().apportEnerg = int.Parse(apport.text);
            gameObjects[i].GetComponent<Agent>().besoinEnergMin = int.Parse(besoinMin.text);
            gameObjects[i].GetComponent<Agent>().besoinEnergMax = int.Parse(besoinMax.text);
            gameObjects[i].GetComponent<Agent>().tempsDigestion = int.Parse(tempsDigestion.text);
            gameObjects[i].GetComponent<Agent>().esperenceDeVieMin = int.Parse(esperanceVieMin.text);
            gameObjects[i].GetComponent<Agent>().esperenceDeVieMax = int.Parse(esperanceVieMax.text);
            gameObjects[i].GetComponent<Agent>().ageMaturationMin = int.Parse(ageMaturationMin.text);
            gameObjects[i].GetComponent<Agent>().ageMaturationMax = int.Parse(ageMaturationMax.text);
            gameObjects[i].GetComponent<Agent>().besoinHydriqueMin = int.Parse(besoinHydriqueMin.text);
            gameObjects[i].GetComponent<Agent>().besoinHydriqueMax = int.Parse(besoinHydriqueMax.text);
        }

    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
    * 
    * Méthode qui permet de modifier les paramètres de plusieurs GameObject en pleine simulation.
    * 
    *Derniere modification le 28/03/2021
    */
    public void ParametreEnGame(GameObject gameObject)
    {
        gameObject.GetComponent<Agent>().vitesse = int.Parse(vitesseMax.text);
        gameObject.GetComponent<Agent>().nbNouveauNees = int.Parse(nbReproduction.text);
        gameObject.GetComponent<Agent>().frequenceReproduction = int.Parse(frequenceMax.text);
        gameObject.GetComponent<Agent>().apportEnerg = int.Parse(apport.text);
        gameObject.GetComponent<Agent>().besoinEnerg = int.Parse(besoinMax.text);
        gameObject.GetComponent<Agent>().tempsDigestion = int.Parse(tempsDigestion.text);
        gameObject.GetComponent<Agent>().esperanceVie = int.Parse(esperanceVieMax.text);
        gameObject.GetComponent<Agent>().ageMaturation = int.Parse(ageMaturationMax.text);
        gameObject.GetComponent<Agent>().besoinHydrique = int.Parse(besoinHydriqueMax.text);
        gameObject.GetComponent<Agent>().vitesseChasser = gameObject.GetComponent<Agent>().vitesse + 2;
    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de revenir dans la simulation.
     * 
     * Derniere modification le 15/04/2021
     */
    public void RetourEnGame()
    {
        if (MenuPause.pause)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (selectTerrain[i].isOn)
                {
                    gameObjects[i].SetActive(true);
                }
            }

            gameObject.SetActive(false);
        }

    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de revenir aux menus choisis.
     * menu (GameObject) : le menu dans lequel on veut revenir
     * 
     * Derniere modification le 15/04/2021
     */
    public void RetourMenu(GameObject menu)
    {
        if (!MenuPause.pause)
        {
            menu.SetActive(true);
            gameObject.SetActive(false);
        }

    }
    //

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'initialiser le menu des agents en pleine simulation.
     * gameObject (GameObject) : variable à initialiser
     * 
     * Derniere modification le 20/04/2021
     */
    public void InitialisationEnGame(GameObject gameObject)
    {
        if (MenuPause.pause)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(gameObject.tag);

            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].GetComponent<Agent>().selection)
                {
                    gameObjects[i].GetComponent<Agent>().vitesse = (int)gameObjects[i].GetComponent<Agent>().vitesse;
                    vitesseMax.text = gameObjects[i].GetComponent<Agent>().vitesse.ToString();
                    gameObjects[i].GetComponent<Agent>().nbNouveauNees.ToString();
                    nbReproduction.text = gameObjects[i].GetComponent<Agent>().nbNouveauNees.ToString();
                    apport.text = gameObjects[i].GetComponent<Agent>().apportEnerg.ToString();
                    gameObjects[i].GetComponent<Agent>().besoinEnerg = (int)gameObjects[i].GetComponent<Agent>().besoinEnerg;
                    besoinMax.text = gameObjects[i].GetComponent<Agent>().besoinEnerg.ToString();
                    tempsDigestion.text = gameObjects[i].GetComponent<Agent>().tempsDigestion.ToString();
                    esperanceVieMax.text = gameObjects[i].GetComponent<Agent>().esperanceVie.ToString();
                    ageMaturationMax.text = gameObjects[i].GetComponent<Agent>().ageMaturation.ToString();
                    gameObjects[i].GetComponent<Agent>().besoinHydriqueMax = (int)gameObjects[i].GetComponent<Agent>().besoinHydriqueMax;
                    besoinHydriqueMax.text = gameObjects[i].GetComponent<Agent>().besoinHydriqueMax.ToString();

                    return;
                }



            }
        }
    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de changer les paramètres des agents sélectionnés.
     * gameObject (GameObject) : variable à qui on change les les paramètres
     * 
     * Derniere modification le 19/04/2021
     */

    public void ChangementParametre(GameObject gameObject)
    {
        if (MenuPause.pause)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(gameObject.tag);

            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].GetComponent<Agent>().selection || gameObjects[i].GetComponent<Agent>().tag == "auto")
                {
                    ParametreEnGame(gameObjects[i]);
                }

            }
        }

    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'actualiser les paramètres l'utilisateur. 
     * 
     * Derniere modification le 19/04/2021
     */

    public void ActualiseUtilisteur()
    {
        AccelereTemps();
        VitesseCamera();
        NbAppartionAutotrophe();
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'accélérer le temps
     * 
     * Derniere modification le 19/04/2021
     */
    public void AccelereTemps()
    {
        for (int i = 0; i < vitesseSimulation.Length; i++)
        {
            if (vitesseSimulation[i].isOn)
            {
                Time.timeScale = int.Parse(vitesseSimulation[i].GetComponent<Text>().text);
            }
        }
    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'accélérer la vitesse de la caméra.
     * 
     * Derniere modification le 19/04/2021
     */
    public void VitesseCamera()
    {
        for (int i = 0; i < vitesseCamera.Length; i++)
        {
            if (vitesseCamera[i].isOn)
            {
                camera.GetComponent<VuMonde>().vitesse = int.Parse(vitesseCamera[i].GetComponent<Text>().text);
            }
        }
    }
    //Fait par Chamsi Ben kaarbar et Chiheb Ben jamaa
    //Derniere modification le 19/04/2021

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'actualiser l'intervalle de temps d'apparition des agents autotrophes 
     * 
     * Derniere modification le 19/04/2021
     */
    public void NbAppartionAutotrophe()
    {
        terrainActuel = Terrain.activeTerrain;
        if (tempsAutotrophe.text.Length == 0)
        {
            return;
        }
        if (int.Parse(tempsAutotrophe.text) < 30)
        {
            tempsAutotrophe.text = "30";
        }
        terrainActuel.GetComponent<ApparitionAgents>().tempsIntervalleApparationAutotrophe = int.Parse(tempsAutotrophe.text);
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'actualiser ce que l'utilisateur a sélectionné
     * 
     * Derniere modification le 19/04/2021
     */
    public void SelectTerrain()
    {
        for (int i = 0; i < terrain.Length; i++)
        {
            terrain[i].SetActive(selectTerrain[i].isOn);
        }

        terrainActuel = Terrain.activeTerrain;
        //NavMeshBuilder.BuildNavMesh();
    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de quitter la simulation
     * 
     * Derniere modification le 19/04/2021
     */
    public void QuitterSimulation()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Projet_L3AJ2");


    }



}
