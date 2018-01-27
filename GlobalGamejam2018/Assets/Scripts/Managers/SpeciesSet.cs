using UnityEngine;

[System.Serializable]
public class SpeciesSet
{
    public string speciesName       = "Unnamed";
    public GameObject speciesPrefab = null;
    public Vector2 speciesMinMax    = Vector2.zero;
    public Vector2 spawnBurst       = Vector2.zero;

    public int amountAlive = 0;
}
