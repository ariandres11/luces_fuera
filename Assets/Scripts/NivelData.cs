using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Niveles/Nivel")]
public class NivelData : ScriptableObject
{
    
    [Tooltip("Arrastrá acá tu archivo .txt")]
    public TextAsset archivoMatriz; 
}