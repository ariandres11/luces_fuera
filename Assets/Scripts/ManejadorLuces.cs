using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorLuces : MonoBehaviour
{
    [Header("Referencias")]
    public List<Luz> luces;
    public GameObject contenedorGanaste;
    
    [Header("Configuración Automática")]
    public int columnas;


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
        if (indice == -1) return; 

        luz.Conmutar();

        if (indice % columnas != 0)
        {
            luces[indice - 1].Conmutar();
        }

        if ((indice + 1) % columnas != 0 && (indice + 1) < luces.Count)
        {
            luces[indice + 1].Conmutar();
        }

        if (indice - columnas >= 0)
        {
            luces[indice - columnas].Conmutar();
        }

        if (indice + columnas < luces.Count)
        {
            luces[indice + columnas].Conmutar();
        }
        if (TodasApagadas())
        {
            contenedorGanaste.SetActive(true);
        }
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

    public bool TodasApagadas()
    {
        foreach (Luz luz in luces)
        {
            if (luz.encendida)
            {
                return false; 
            }
        }
        return true; 
    }
}