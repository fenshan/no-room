using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct Contact
{
    public int reference;
    public string name;
    public string whatsapp;
    public string facebook;
}

public class ContactsManager : MonoBehaviour
{
    public TextAsset t;
    public List<Contact> contacts = new List<Contact>();
    //string path = "Contacts.txt";

    public void Start()
    {
        //Inicializar lista

        string s = t.text;
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        StreamReader reader = new StreamReader(stream);

        //StreamReader reader = new StreamReader(path);
        //StreamReader reader = File.OpenText(path);
        //t = Resources.Load("contacts") as TextAsset;
        //StreamReader reader = new StreamReader(t.text);
        string n, w, f;
        for (int i = 0; (n = reader.ReadLine()) != null; ++i)
        {
            w = reader.ReadLine();
            f = reader.ReadLine();

            contacts.Add(new Contact
            {
                reference = i,
                name = n,
                whatsapp = w,
                facebook = f
            });

            reader.ReadLine();
            //Debug.Log(i + "\n" + n + "\n" + w + "\n" + f + "\n");

        }
    }



    public void changeWhatsapp(int r, string w)
    {
        contacts[r] = new Contact
        {
            reference = contacts[r].reference,
            name = contacts[r].name,
            whatsapp = w,
            facebook = contacts[r].facebook
        };

    }






}
