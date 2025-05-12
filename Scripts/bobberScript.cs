using UnityEngine;

public class bobberScript : MonoBehaviour
{
    public bool gameIsOver = false;
    public Animator bobberAnim;
    public float bobberTime;
    public bool inWater;
    PlayerController PC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
        {
            PC = obj.GetComponent<PlayerController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        bobberTime += Time.deltaTime;
        if(bobberTime >= 3)
        {
            bobberAnim.Play("BobberIdle");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag ==("FishingSpot"))
        {
            
            inWater = true;
            
            Debug.Log("EnterSpot");
            BobberCheck();
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FishingSpot"))
        {
            
            inWater = false;
            Debug.Log("ExitSpot");
            BobberCheck();
        }
    }
    public void BobberCheck()
    {
        PC.InWater = inWater;
    }
    
}
   

