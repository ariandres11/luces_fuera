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

    public void Conmutador(Luz luz)
    {
        int indice = luces.IndexOf(luz);
        
        if (indice == 0)
        {
            luz.Conmutar();
            luces[indice + 1].Conmutar();
            return;
        }

        if (indice == luces.Count - 1)
        {
            luces[indice - 1].Conmutar();
            luz.Conmutar();
            return;
        }

        luces[indice - 1].Conmutar();
        luz.Conmutar();
        luces[indice + 1].Conmutar();

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
