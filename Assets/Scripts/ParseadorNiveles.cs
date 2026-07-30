using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importante para poder acceder al componente Button

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
        // 1. Limpieza de seguridad
        foreach (Transform hijo in transform)
        {
            Destroy(hijo.gameObject);
        }
        
        // Limpiamos la lista del manejador antes de empezar
        if (manejador != null)
        {
            manejador.luces.Clear();
        }

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

        if (manejador == null)
        {
            Debug.LogError("No asignaste el ManejadorLuces al Parseador.");
            return;
        }

        // 3. Extraemos el texto y separamos por filas
        string textoBruto = datosNivel.archivoMatriz.text.Trim();
        string[] filas = textoBruto.Split(new char[] { ';', '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);

        // 4. Le decimos al manejador cuántas columnas tiene este nivel
        if (filas.Length > 0)
        {
            manejador.columnas = filas[0].Split(',').Length;
        }

        // 5. Construimos la matriz
        for (int y = 0; y < filas.Length; y++)
        {
            string[] columnas = filas[y].Split(',');

            for (int x = 0; x < columnas.Length; x++)
            {
                // Instanciamos
                GameObject nuevaLuzObj = Instantiate(luzPrefab, transform);
                nuevaLuzObj.name = $"Luz_{x}_{y}";

                Luz componenteLuz = nuevaLuzObj.GetComponent<Luz>();
                
                // --- CONECTAMOS EL BOTÓN AL MANEJADOR ---
                Button boton = nuevaLuzObj.GetComponent<Button>();
                if (boton != null)
                {
                    boton.onClick.AddListener(() => manejador.Conmutador(componenteLuz));
                }

                // --- ESTABLECEMOS EL ESTADO INICIAL ---
                string valorCelda = columnas[x].Trim();
                if (valorCelda == "1")
                {
                    componenteLuz.Encender();
                }
                else if (valorCelda == "0")
                {
                    componenteLuz.Apagar();
                }

                // Añadimos la luz a la lista del manejador
                manejador.luces.Add(componenteLuz);
            }
        }
    }
}