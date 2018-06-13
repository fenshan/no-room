using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
using UnityEngine.UI;

enum PanelesFacebook
{
    Bandeja, Conversacion
};

//Copiado tal cual de WhatsappManager
public class FacebookManager : MonoBehaviour
{

    public GameObject contactButtonPrefab;
    public GameObject converPrefab;
    public GameObject contentScrollPanel;

    public GameManager gm;
    public GameObject Bandeja;
    public List<GameObject> conversaciones = new List<GameObject>();
    List<GameObject> contactButtons = new List<GameObject>();
    public GameObject panelActive;
    //List<WhatsappContact> wc;
    //List of buttons?

    PanelesFacebook panelActivo = PanelesFacebook.Bandeja;


    //Actualizar los nombres de whatsapp de los buttons y paneles
    //Añadir buttons y paneles si faltaban
    public void ActualizeFacebookContacts()
    {
        foreach (Contact contacto in gm.cm.contacts)
        {
            if (contacto.facebook != "")
            {
                bool esta = false;
                for (int i = 0; i < contactButtons.Count; ++i)
                {
                    GameObject cb = contactButtons[i];
                    //Si ya está creado, actualizar el nombre
                    if (cb.name == contacto.reference.ToString())
                    {
                        esta = true;
                        //button
                        cb.GetComponentInChildren<Text>().text = contacto.facebook;
                        //panel
                        conversaciones[i].GetComponentInChildren<Text>().text = contacto.facebook;
                        break;
                    }
                }

                //Si no está en los botones tampoco está en los paneles
                if (!esta)
                {
                    //Create panel
                    GameObject cv = Instantiate(converPrefab) as GameObject;
                    cv.name = contacto.reference.ToString();
                    cv.GetComponentInChildren<Text>().text = contacto.facebook;
                    cv.transform.SetParent(this.transform, false);
                    cv.GetComponentInChildren<Button>().onClick.AddListener(ActiveBandeja);
                    conversaciones.Add(cv);


                    //Create button
                    GameObject cb = Instantiate(contactButtonPrefab) as GameObject;
                    cb.name = contacto.reference.ToString();
                    cb.GetComponentInChildren<Text>().text = contacto.facebook;
                    cb.transform.SetParent(contentScrollPanel.transform, false);
                    cb.GetComponent<Button>().onClick.AddListener(delegate { ActiveConversacion(contacto.reference); });
                    contactButtons.Add(cb);

                }

                //Ponerlo en la posicion adecuada


                //Crear el panel en el caso de que no exista y meterlo en la list


            }
        }
        //Sort by order
        //wc.OrderBy(c => c.order);    
    }


    public void ActiveBandeja()
    {
        ActualizeFacebookContacts();
        Bandeja.SetActive(true);
        panelActivo = PanelesFacebook.Bandeja;
        panelActive = Bandeja;
        foreach (GameObject conver in conversaciones) conver.SetActive(false);

    }

    public void ActiveConversacion(int n)
    {
        foreach (GameObject conver in conversaciones)
        {
            if (conver.name == n.ToString())
            {
                conver.SetActive(true);
                panelActive = conver;
            }
        }

        //Debug.Log(n);
        panelActivo = PanelesFacebook.Conversacion;
        Bandeja.SetActive(false);
        //El resto de convers no hace falta ponerlas a false, porque siempre se vendrá desde la bandeja

    }


    void OnEnable()
    {
        ActualizeFacebookContacts();



    }

    private void Awake()
    {
        ActiveBandeja();
    }

    private void Update()
    {
        if (gm.state == State.F)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (panelActivo != PanelesFacebook.Bandeja)
                {
                    ActiveBandeja();
                }

                else if (panelActivo == PanelesFacebook.Bandeja)
                {
                    gm.cfm.ActiveDesktop();
                }
            }
        }







    }


}