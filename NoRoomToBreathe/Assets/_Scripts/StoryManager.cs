using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;


//GUarrada de principio a fin
public class StoryManager : MonoBehaviour
{
    public GameManager gm;

    public GameObject selfM;
    public GameObject otherM;

    public GameObject container;

    public TextAsset a1, a2, a3, a4, a5, a6;
    Story s1, s2, s3, s4, s5, s6;
    Story current;
    int currentIndex;
    //int num;

    //option selected
    //int optionSelected = -1;

    private void Start()
    {
        s1 = new Story(a1.text);
        s2 = new Story(a2.text);
        s3 = new Story(a3.text);
        s4 = new Story(a4.text);
        s5 = new Story(a5.text);
        s6 = new Story(a6.text);

        //num = 0;
        current = s1;
        currentIndex = 1;
        ExecuteStory();
        //ExecuteStory(s2);
        //ExecuteStory(s3);
        //ExecuteStory(s4);
        //ExecuteStory(s5);
        //ExecuteStory(s6);

    }

    //private void Update()
    //{
    //    //Ejecutar linealmente

    //    if (num == 0)
    //    {
    //        ++num;
    //        StartCoroutine(ExecuteStory(s1));
    //    }
    //    else if (num == 2)
    //    {
    //        ++num;
    //        StartCoroutine(ExecuteStory(s2));
    //    }
    //    else if (num == 4)
    //    {
    //        ++num;
    //        StartCoroutine(ExecuteStory(s3));
    //    }
    //    else if (num == 6)
    //    {
    //        ++num;
    //        StartCoroutine(ExecuteStory(s4));
    //    }
    //    else if (num == 8)
    //    {
    //        ++num;
    //        StartCoroutine(ExecuteStory(s5));

    //    }
    //    else if (num == 10)
    //    {
    //        ++num;
    //        StartCoroutine(ExecuteStory(s6));

    //    }
    //}


    public void ChangeCurrentStory()
    {
        switch (currentIndex)
        {
            case 1: current = s2; currentIndex = 2; break;
            case 2: current = s3; currentIndex = 3; break;
            case 3: current = s4; currentIndex = 4; break;
            case 4: current = s5; currentIndex = 5; break;
            case 5: current = s6; currentIndex = 6; break;
            case 6: currentIndex = -1; break;
        }
        if (currentIndex != -1) ExecuteStory();
    }

    public void OptionSelected(int n)
    {
        current.ChooseChoiceIndex(n);
        //desactivar botones
        for (int i = 0; i < 3; ++i)
        {
            Debug.Log(i);
            GameObject b = container.transform.Find(i.ToString()).gameObject;
            b.SetActive(false);
        }
        Next();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }


    void Next()
    {

        Debug.Log("enter");
        while (current.canContinue)
        {
            //Debug.Log(s.Continue());
            //yield return new WaitForSeconds(3.0f);


            //s.Continue() ponerlo en el prefab y lugar según las tags
            //Debug.Log(s.Continue());
            string sentence = current.Continue();
            if (sentence != "\n" && sentence != "")
            {
                GameObject prefab = selfM;
                //if (current.currentTags.Count > 0 && current.currentTags[0] == "tu") prefab = selfM;
                //else prefab = otherM;

                if (current.currentTags.Count > 0 && current.currentTags[0] != "tu") prefab = otherM;


                GameObject m = Instantiate(prefab) as GameObject;
                sentence = sentence.Remove(sentence.Length - 1);
                m.GetComponentInChildren<Text>().text = sentence;
                m.transform.SetParent(container.transform.Find("ScrollPanel").Find("ContentScrollPanel").transform/*por que este ultimo transform, no es transform ya?*/, false);
                m.transform.Find("Image").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(m.GetComponentInChildren<Text>().preferredWidth + 50, m.GetComponentInChildren<Text>().preferredHeight + 50);
                //m.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(m.GetComponentInChildren<Text>().preferredWidth + 50, m.GetComponentInChildren<Text>().preferredHeight + 50);
                m.GetComponent<RectTransform>().sizeDelta = new Vector2(m.GetComponentInChildren<Text>().preferredWidth + 50, m.GetComponentInChildren<Text>().preferredHeight + 50);
                Debug.Log(m.GetComponentInChildren<Text>().preferredWidth);
                Debug.Log(m.GetComponentInChildren<Text>().preferredHeight);
            }

        }

        //yield return new WaitForSecondsRealtime(5);
        if (current.currentChoices.Count > 0)
        {
            Debug.Log(current.currentChoices.Count);
            for (int i = 0; i < current.currentChoices.Count; ++i)
            {
                //Activar boton
                GameObject b = container.transform.Find(i.ToString()).gameObject;
                b.GetComponentInChildren<Text>().text = current.currentChoices[i].text;
                b.SetActive(true);
            }
            //Listener (sería mejor)
            //Desactivar botones
            //while (optionSelected == -1)
            //{
            //    StartCoroutine(Wait());
            //}

        }
        else ChangeCurrentStory();
    }

    void SetContainer()
    {
        int r = 0;
        switch (current.globalTags[1])
        {
            case "novio": r = 0; break;
            case "madre": r = 7; break;
            case "ex": r = 4; break;
            case "compa": r = 6; break;
        }
        if (current.globalTags[0] == "facebook")
        {
            gm.cfm.fm.ActualizeFacebookContacts();
            foreach (GameObject conver in gm.cfm.fm.conversaciones)
            {

                if (conver.name == r.ToString())
                {
                    container = conver;
                }
            }
        }
        else
        {
            gm.cfm.wm.ActualizeWhatsappContacts();
            foreach (GameObject conver in gm.cfm.wm.conversaciones)
            {
                if (conver.name == r.ToString())
                {
                    container = conver;
                }
            }

        }
    }

    //IEnumerator para hacer las paraditas se queda pa otro día
    void ExecuteStory()
    {

        SetContainer();

        //foreach (string tag in s.globalTags)
        // {
        //     Debug.Log(tag);
        // }

        Next();

    }


}
