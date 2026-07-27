using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorLuces : MonoBehaviour
{
    public List<Luz> luces;
    // Start is called before the first frame update
    void Start()
    {
        EncenderTodas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApagarTodas()
    {
        foreach (Luz luz in luces)
        {
            luz.Apagar();
        }
    }
    public void EncenderTodas()
    {
        foreach (Luz luz in luces)
        {
            luz.Encender();
        }
    }

    public void ApagarLuz(Luz luz)
    {
        int indice = luces.IndexOf(luz);
        
        if (indice == 0)
        {
            luz.Apagar();
            luces[indice + 1].Encender();
            return;
        }

        if (indice == luces.Count - 1)
        {
            luces[indice - 1].Encender();
            luz.Apagar();
            return;
        }

        luces[indice - 1].Encender();
        luz.Apagar();
        luces[indice + 1].Encender();

    }

    public void Interruptor()
    {
        foreach (Luz luz in luces)
        {
            if (luz.encendida)
            {
                luz.Apagar();
            }
            else
            {
                luz.Encender();
            }
        }
    }
}
