using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParseadorNiveles : MonoBehaviour
{
    [Header("Configuración")]
    public NivelData datosNivel;
    public GameObject luzPrefab;
    
    [Header("Conexiones")]
    public ManejadorLuces manejador;

    void Start()
    {
        GenerarMatriz();
    }

    public void GenerarMatriz()
    {
        foreach (Transform hijo in transform)
        {
            Destroy(hijo.gameObject);
        }
        
        if (manejador != null)
        {
            manejador.luces.Clear();
        }

        if (datosNivel == null)
        {
            Debug.LogError("No asignaste el NivelData al Parseador.");
            return;
        }

        if (datosNivel.archivoMatriz == null)
        {
            Debug.LogError($"El NivelData '{datosNivel.name}' no tiene asignado un archivoMatriz (.txt).");
            return;
        }

        if (manejador == null)
        {
            Debug.LogError("No asignaste el ManejadorLuces al Parseador.");
            return;
        }

        string textoBruto = datosNivel.archivoMatriz.text.Trim();
        string[] filas = textoBruto.Split(new char[] { ';', '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (filas.Length > 0)
        {
            manejador.columnas = filas[0].Split(',').Length;
        }

        for (int y = 0; y < filas.Length; y++)
        {
            string[] columnas = filas[y].Split(',');

            for (int x = 0; x < columnas.Length; x++)
            {
                GameObject nuevaLuzObj = Instantiate(luzPrefab, transform);
                nuevaLuzObj.name = $"Luz_{x}_{y}";

                Luz componenteLuz = nuevaLuzObj.GetComponent<Luz>();
                
                Button boton = nuevaLuzObj.GetComponent<Button>();
                if (boton != null)
                {
                    boton.onClick.AddListener(() => manejador.Conmutador(componenteLuz));
                }

                string valorCelda = columnas[x].Trim();
                if (valorCelda == "1")
                {
                    componenteLuz.Encender();
                }
                else if (valorCelda == "0")
                {
                    componenteLuz.Apagar();
                }

                manejador.luces.Add(componenteLuz);
            }
        }
    }
}