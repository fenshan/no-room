using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Right, Left };
public enum Canvas {Fijo, Movible, Lateral, Settings, Desktop, Whatsapp, Facebook };
public enum State { PuttingL, L, PuttingF, F}

public class GameManager : MonoBehaviour
{

    //Static instance of GameManager which allows it to be accessed by any other script.  
    public static GameManager instance = null;
    public DesplazarCanvas dc;
    public StoryManager sm;
    public GameObject CanvasFijo;
    public GameObject CanvasMovible;
    public GameObject Lateral;
    public GameObject Settings;
    public GameObject Desktop;
    public GameObject Whatsapp;
    public GameObject Facebook;

    public CanvasMovibleManager cmm;
    public CanvasFijoManager cfm;
    public ContactsManager cm;

    public State state = State.F;

    public bool pantallaLateral; //true if active

    //Singlenton
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public void Start()
    {
        pantallaLateral = false;
        state = State.F;
        cfm.ActiveDesktop();

    }

    public void Quit()
    {
        Application.Quit();
    }



    public void SwipeListener(Direction dir)
    {
        if (dir == Direction.Right)
        {
            pantallaLateral = true;

        }

        else if (dir == Direction.Left)
        {
            pantallaLateral = false;
        }

    }

    //Para los dos bloques (Fijo y Movible) cambiar entre interactuable o no  (siempre estarán activos ambos)
   //Para los paneles dentro de los canvas cambiar entre activo o no
    public void ActivarPanel(Canvas name, bool activo)
    {
        switch (name)
        {
            //TODO 
            //El marcar los canvas como no interactuables, no hace que los elementos de sus hijos dejen de serlo 
            //(ejemplo: el boton hacia atrás de la bandeja de whatsapp)
            //Lo he resuelto de forma guarra, solo desactivando el panel de whatsapp y face cuando se activa el canvas lateral, 
            //pero hay que arreglarlo bien (el cambio que he hecho es en DesplazarCanvas.cs)
            case Canvas.Fijo: CanvasFijo.GetComponent<CanvasGroup>().interactable = activo; break;
            case Canvas.Movible: CanvasMovible.GetComponent<CanvasGroup>().interactable = activo; break;
            case Canvas.Lateral: Lateral.SetActive(activo); break;
            case Canvas.Settings: Settings.SetActive(activo); break;
            case Canvas.Desktop: Desktop.SetActive(activo); break;
            case Canvas.Whatsapp: Whatsapp.SetActive(activo); break;
            case Canvas.Facebook: Facebook.SetActive(activo); break;



        }
    }

}