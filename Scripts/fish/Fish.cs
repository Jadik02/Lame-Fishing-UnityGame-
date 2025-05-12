using UnityEngine;

[CreateAssetMenu]

public class Fish : ScriptableObject
{
    public Sprite FishSprite;
    public string FishName;
    public int dropChance;
    public int FishPrice;
    public Fish(string name, int dropChance, int fishPrice)
    {
        this.FishName = name;
        this.dropChance = dropChance;
        this.FishPrice = fishPrice;
    }
}
