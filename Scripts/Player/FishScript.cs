using Unity.VisualScripting;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public static event FishDropping OnFishDropping;
    public delegate void FishDropping(Fish itemData);
    public Fish fishData;
    public static event System.Action<FishScript> OnFishScriptSpawned;

    private void Awake()
    {
        OnFishScriptSpawned?.Invoke(this);
    }
    public void Collect()
    {
        OnFishDropping?.Invoke(fishData);
        Destroy(gameObject);
    }
    private void Update()
    {
        
    }

}

