using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesplazarCanvas : MonoBehaviour {

    public GameManager gm = GameManager.instance;

    public Vector3 der;
    public Vector3 izq;

    public float speed=1;
    public float delta = 0.5f;
   

    void Update()
    {

        if (gm.pantallaLateral)
        {
            if (transform.position.x > der.x - delta && gm.state==State.PuttingL)//Acabar, solo una vez
            {
                //Debug.Log("l acabado");
                gm.state = State.L;
                transform.position = der;
                gm.ActivarPanel(Canvas.Movible, true);


                //Guarrada para que no se joda la interaccion con el canvas lateral solo por los botones de whatsapp y face
                gm.Whatsapp.SetActive(false);
                gm.Facebook.SetActive(false);
            }

            else if (transform.position.x <= der.x - delta)
            {
                //Debug.Log("l haciendo");
                gm.state = State.PuttingL;
                gm.cmm.ActiveLateral();
                gm.ActivarPanel(Canvas.Fijo, false);
                //gm.cfm.wm.Bandeja.GetComponentInChildren<Button>().interactable = false;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

        }

        else if (!gm.pantallaLateral)
        {
            if (transform.position.x < izq.x + delta && gm.state ==State.PuttingF)//Acabar, solo una vez
            {
                //Debug.Log("f acabado");
                gm.state = State.F;
                transform.position = izq;
                gm.ActivarPanel(Canvas.Fijo, true);
                //gm.cfm.wm.Bandeja.GetComponentInChildren<Button>().interactable = true;
            }

            else if (transform.position.x >= izq.x + delta)
            {
                Debug.Log("f haciendo");

                //Guarrada para que no se joda la interaccion con el canvas lateral solo por los botones de whatsapp y face
                if (gm.cfm.panelActivoFijo == PanelesFijos.Whatsapp)
                {
                    gm.Whatsapp.SetActive(true);
                    if (gm.cfm.wm.panelActive != gm.cfm.wm.Bandeja) gm.cfm.wm.Bandeja.SetActive(false);
                    else gm.cfm.wm.Bandeja.SetActive(true);
                    foreach(GameObject c in gm.cfm.wm.conversaciones)
                    {
                        if (c != gm.cfm.wm.panelActive) c.SetActive(false);
                        else c.SetActive(true);

                    }

                }

                //Guarrada para que no se joda la interaccion con el canvas lateral solo por los botones de whatsapp y face
                if (gm.cfm.panelActivoFijo == PanelesFijos.Facebook)
                {
                    gm.Facebook.SetActive(true);
                    if (gm.cfm.fm.panelActive != gm.cfm.fm.Bandeja) gm.cfm.fm.Bandeja.SetActive(false);
                    else gm.cfm.fm.Bandeja.SetActive(true);
                    foreach (GameObject c in gm.cfm.fm.conversaciones)
                    {
                        if (c != gm.cfm.fm.panelActive) c.SetActive(false);
                        else c.SetActive(true);

                    }

                }



                gm.state = State.PuttingF;
                gm.ActivarPanel(Canvas.Movible, false);
                transform.Translate(Vector3.left * speed * Time.deltaTime);

            }

        }

    }

}