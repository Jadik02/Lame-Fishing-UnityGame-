using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;


public class FishDrop : MonoBehaviour
{
    public GameObject droppedFishPrefab;
    public List<Fish> FishList = new List<Fish> ();

    Fish GetDroppedFish()
    {
        int randomNumber = Random.Range(1, 101);
        List<Fish> possibleFish  = new List<Fish> ();
        foreach (Fish fish in FishList)
        {
            if(randomNumber <= fish.dropChance)
                possibleFish.Add (fish);
        }
        if(possibleFish.Count > 0)
        {
            Fish droppedfish = possibleFish[Random.Range(0, possibleFish.Count)];
            return droppedfish;
        }
        Debug.Log("No Fish Dropped");
        return null;
    }

    

    public void instantiateFish(Vector3 spawnPosition)
    {
        Fish droppedfish = GetDroppedFish();
        if(droppedfish != null )
        {
            
            GameObject FishGameObject = Instantiate(droppedFishPrefab, spawnPosition, Quaternion.identity);
            FishGameObject.GetComponent<SpriteRenderer>().sprite = droppedfish.FishSprite;
            FishScript fishScript = Object.FindFirstObjectByType<FishScript>();
            fishScript.fishData = droppedfish;
        }
    }
    public void GetFish()
    {
        
        GetComponent<FishDrop>().instantiateFish(transform.position);
        
    }

    public void Update()
    {
        
    }
}

