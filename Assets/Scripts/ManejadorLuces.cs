using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorLuces : MonoBehaviour
{
    [Header("Referencias")]
    public List<Luz> luces;
    
    [Header("Configuración Automática")]
    public int columnas; // El Parseador llenará este dato automáticamente

    // Ya no llamamos a EncenderTodas en el Start() porque el Parseador 
    // se encarga de encender o apagar las luces según el archivo de texto.

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
        if (indice == -1) return; // Por seguridad, si la luz no está en la lista

        // 1. Conmutar la luz central (la que el jugador tocó)
        luz.Conmutar();

        // 2. Conmutar Izquierda 
        // (Solo si NO estamos en el borde izquierdo)
        if (indice % columnas != 0)
        {
            luces[indice - 1].Conmutar();
        }

        // 3. Conmutar Derecha 
        // (Solo si NO estamos en el borde derecho y no nos salimos de la lista)
        if ((indice + 1) % columnas != 0 && (indice + 1) < luces.Count)
        {
            luces[indice + 1].Conmutar();
        }

        // 4. Conmutar Arriba 
        // (Restamos las columnas para ir a la fila de arriba)
        if (indice - columnas >= 0)
        {
            luces[indice - columnas].Conmutar();
        }

        // 5. Conmutar Abajo 
        // (Sumamos las columnas para ir a la fila de abajo)
        if (indice + columnas < luces.Count)
        {
            luces[indice + columnas].Conmutar();
        }
        if (TodasApagadas())
        {
            Debug.Log("¡Felicidades! Has apagado todas las luces.");
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
                return false; // Si encontramos una luz encendida, no todas están apagadas
            }
        }
        return true; // Todas las luces están apagadas
    }
}