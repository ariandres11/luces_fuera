using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseadorNiveles : MonoBehaviour
{
    [Header("Configuración")]
    public NivelData datosNivel;
    public GameObject luzPrefab;    // El prefab del botón con el script Luz.cs

    public List<Luz> lucesGeneradas = new List<Luz>();

    void Start()
    {
        GenerarMatriz();
    }

    public void GenerarMatriz()
    {
        // 1. Limpieza de seguridad
        foreach (Transform hijo in transform)
        {
            Destroy(hijo.gameObject);
        }
        lucesGeneradas.Clear();

        // 2. Validaciones
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

        // 3. Extraemos el texto directamente desde el ScriptableObject
        string textoBruto = datosNivel.archivoMatriz.text.Trim();

        // 4. Separamos por filas (;)
        string[] filas = textoBruto.Split(new char[] { ';', '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        for (int y = 0; y < filas.Length; y++)
        {
            // Separamos la fila en columnas (,)
            string[] columnas = filas[y].Split(',');

            for (int x = 0; x < columnas.Length; x++)
            {
                // Instanciamos
                GameObject nuevaLuzObj = Instantiate(luzPrefab, transform);
                nuevaLuzObj.name = $"Luz_{x}_{y}";

                Luz componenteLuz = nuevaLuzObj.GetComponent<Luz>();
                string valorCelda = columnas[x].Trim();

                // Encendemos o apagamos
                if (valorCelda == "1")
                {
                    componenteLuz.Encender();
                }
                else if (valorCelda == "0")
                {
                    componenteLuz.Apagar();
                }

                lucesGeneradas.Add(componenteLuz);
            }
        }
    }
}