using UnityEngine;

[System.Serializable]
public class SpeciesSet
{
    public string speciesName       = "Unnamed";
    public GameObject speciesPrefab = null;
    public Vector2 speciesMinMax    = Vector2.zero;
    public Vector2 spawnBurst       = Vector2.zero;
    [Range(0.0f, 1.0f)] public float childChange = 0.25f;

    [HideInInspector] public int amountAlive = 0;
}
