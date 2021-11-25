using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
 * Dernière modification: 23/04/2021
 */
public class ChangementCamera : MonoBehaviour
{

    public List<GameObject> cams;
    public GameObject cam;
    public bool selectOurs;
    public bool selectCerf;
    public bool selectLapin;
    public bool selectLoup;
    // Start is called before the first frame update
    void Start()
    {
        cams = new List<GameObject>();
        cams.Add(cam);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            TrouverVu();
            ChangerVu();
        }

    }

    /**Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de savoir quels agents sont sélectionnés par l'utilisateur pendant une simulation et d'activer la caméra des agents sélectionnés 
     * 
     * derniere modification: 22/04/2021
     */
    public void TrouverVu()
    {
        GameObject[] ours = GameObject.FindGameObjectsWithTag("Ours");
        GameObject[] lapin = GameObject.FindGameObjectsWithTag("lapin");
        GameObject[] loup = GameObject.FindGameObjectsWithTag("loup");
        GameObject[] cerf = GameObject.FindGameObjectsWithTag("Proie");
        int n = 1;

        //boucle permettant de supprimer la caméra d'un agent de la liste qui stocke toutes les caméras actives et d'activer la caméra principale par défault
        for (int i = 0; i < cams.Count; i++)
        {
            if (cams[i] == null)
            {
                cams.Remove(cams[i]);
                cams[0].SetActive(true);
            }
        }
        //condition qui permet de rentrer dans la boucle uniquement si un ours est déjà sélectionné afin de gagner en vitesse de calcul 
        if (selectOurs)
        {
            //boucle qui permet de parcourir tous les ours et de voir quel ours a la variable "selection" à vraie
            for (int i = 0; i < ours.Length; i++)
            {
                if (ours[i].GetComponent<Agent>().selection && !ours[i].GetComponent<Agent>().choisi)
                {
                    cams.Add(ours[i].GetComponent<Agent>().cam);
                    ours[i].GetComponent<Agent>().choisi = true;
                    n++;
                    break;
                }
            }
        }
        //condition qui permet de rentrer dans la boucle uniquement si un lapin est déjà sélectionné afin de gagner en vitesse de calcul 
        if (selectLapin)
        {
            //boucle qui permet de parcourir tous les lapins et de voir quel lapin a la variable "selection" à vraie
            for (int i = 0; i < lapin.Length; i++)
            {
                if (lapin[i].GetComponent<Agent>().selection && !lapin[i].GetComponent<Agent>().choisi)
                {
                    cams.Add(lapin[i].GetComponent<Agent>().cam);
                    lapin[i].GetComponent<Agent>().choisi = true;
                    n++;
                    break;
                }
            }
        }
        //condition qui permet de rentrer dans la boucle uniquement si un loup est déjà sélectionné afin de gagner en vitesse de calcul
        if (selectLoup)
        {
            //boucle qui permet de parcourir tous les loups et de voir quel loup a la variable "selection" à vraie
            for (int i = 0; i < loup.Length; i++)
            {
                if (loup[i].GetComponent<Agent>().selection && !loup[i].GetComponent<Agent>().choisi)
                {
                    cams.Add(loup[i].GetComponent<Agent>().cam);
                    loup[i].GetComponent<Agent>().choisi = true;
                    n++;
                    break;
                }
            }
        }
        //condition qui permet de rentrer dans la boucle uniquement si un cerf est déjà sélectionné afin de gagner en vitesse de calcul
        if (selectCerf)
        {
            //boucle qui permet de parcourir tous les cerfs et de voir quel cerf a la variable "selection" à vraie
            for (int i = 0; i < cerf.Length; i++)
            {
                if (cerf[i].GetComponent<Agent>().selection && !cerf[i].GetComponent<Agent>().choisi)
                {
                    cams.Add(cerf[i].GetComponent<Agent>().cam);
                    cerf[i].GetComponent<Agent>().choisi = true;
                    n++;
                    break;
                }
            }
        }
        //boucle permettant de supprimer une caméra de la liste qui stocke toutes les caméras actives
        for (int i = 0; i < cams.Count; i++)
        {
            if (i != 0 && !cams[i].transform.GetComponentInParent<Agent>().selection)
            {
                cams[i].transform.GetComponentInParent<Agent>().choisi = false;
                cams.Remove(cams[i]);

            }
        }
    }


    /**Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de changer de caméra entre les caméras activent des différents agents en appuyant la touche "C"
     * 
     * Derniere modification: 23/04/2021
     */
    public void ChangerVu()
    {


        //boucle de parcourir les caméras actives
        for (int i = 0; i < cams.Count; i++)
        {
            if (cams[i].activeSelf)
            {

                cams[i].SetActive(false);
                if (i + 1 == cams.Count)
                {

                    cams[0].SetActive(true);
                }
                else
                {
                    cams[i + 1].SetActive(true);
                }

                return;
            }
        }

        //si la simulation est en pause alors on retourne sur la caméra principale
        if (MenuPause.pause)
        {
            for (int i = 0; i < cams.Count; i++)
            {
                if (cams[i].activeSelf)
                {
                    cams[i].SetActive(false);
                    cams[0].SetActive(true);
                }
            }
        }

    }
}
