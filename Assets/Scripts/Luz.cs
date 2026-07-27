using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Luz : MonoBehaviour
{
    public bool encendida;
    private Image imagen;
    public Sprite luzApagada;
    public Sprite luzEncendida;


    void Awake()
    {
        imagen = GetComponent<Image>();

    }
    
    public void Apagar()
    {
        encendida = false;
        imagen.sprite = luzApagada;
    }

    public void Encender()
    {
        encendida = true;
        imagen.sprite = luzEncendida;
    }
}
