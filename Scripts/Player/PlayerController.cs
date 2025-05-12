using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public bool isFishing;
    public bool isCollecting;
    public bool poleBack;
    public bool throwBobber;
    public bool InWater;
    private bool FishingSpot;
    public GameObject bobber;

    public float targetTime = 0f;
    public float savedtargetTime;
    public float extraBobberDistance;
    public float timeTillCatch;
    private float bobberSwingDistance = 1f;
    public int BaitCount = 5;
    public int maxBaitCount = 20;

    public GameObject fishGame;
    public FishingMG fishingMG;
    
    [SerializeField] GameObject FishSpotIndicator;

    [SerializeField] bobberScript BobberScript;
    [SerializeField] MoneyDisplay moneyDisplay;

    private bool triggerEnter;
    public float CatchTimer = 0.0f;
    public bool winAnim;

    public float moveSpeed;

    private Rigidbody2D rb;

    private float x;
    private float y;

    private Vector2 input;
    private bool moving;

 
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        
        fishscript = Object.FindFirstObjectByType<FishScript>();
        fishdrop = Object.FindFirstObjectByType<FishDrop>();
        rb = GetComponent<Rigidbody2D>();
        moneyDisplay.UpdateBait();
        isFishing = false;
        isCollecting = false;
        fishGame.SetActive(false);
        throwBobber = false;
        targetTime = 0.0f;
        savedtargetTime = 0.0f;
        extraBobberDistance = 0.0f;

    }
    private void Update()
    {

        FishingLogic();

        if (!isFishing && !isCollecting)
        {
            GetInput();
            
        }
        
        if (isCollecting)
        {
            anim.Play("PlayerIdle");
            
        }
        
        Animate();
        


        
        
    }

    void FishingLogic()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isFishing && !winAnim)
        {

            poleBack = true;
        }
        if (isFishing)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition; // freeze Player
            CatchTimer += Time.deltaTime;
            if (CatchTimer >= timeTillCatch)
            {
                fishGame.SetActive(true);
            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (Input.GetKeyUp(KeyCode.E) && !isFishing && !winAnim)
        {
            //timeTillCatch = UnityEngine.Random.Range(3f, 15f);
            //fishingMG.startMG = true;
            //poleBack = false;
            isFishing = true;


            throwBobber = true;
            if (targetTime >= 3)
            {
                extraBobberDistance = 6;
            }
            else
            {
                extraBobberDistance = targetTime * 2;
            }
        }

        Vector3 temp = new Vector3(extraBobberDistance, 0, 0);


        if (poleBack == true)
        {
            targetTime += Time.deltaTime;

            //anim.Rebind();
            savedtargetTime = targetTime;

            //targetTime += Time.deltaTime;


        }
        if (isFishing && throwBobber)
        {

            SpawnBobber();
            throwBobber = false;
            //targetTime = 5.0f;
            savedtargetTime = 0.0f;
            extraBobberDistance = 0.0f;

            anim.Play("playerFishing");
        }



        if (Input.GetKeyDown(KeyCode.P) && CatchTimer <= timeTillCatch
            && !isCollecting)
        {
            //anim.Play("PlayerIdle");
            Destroy(GameObject.Find("bobber(Clone)"));
            anim.Rebind();
            targetTime = 0.0f; //LastChangePoint
            fishingMG.startMG = false;
            poleBack = false;
            throwBobber = false;
            isFishing = false;
            CatchTimer = 0;

        }
    }
    void SpawnBobber()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 spawnDirection = (mousePos - transform.position).normalized;

        Vector3 spawnPosition = transform.position + spawnDirection * (bobberSwingDistance + extraBobberDistance);

        // Spawn bobber
        Instantiate(bobber, spawnPosition, Quaternion.identity);

        StartCoroutine(CheckBobber(BobberScript));


    }
    IEnumerator CheckBobber(bobberScript bobberS)
    {
        yield return new WaitForSeconds(0.5f); // delay

        if (bobberS != null && InWater && BaitCount > 0)
        {
            timeTillCatch = UnityEngine.Random.Range(3f, 15f);
            fishingMG.startMG = true;
            poleBack = false;
            isFishing = true;
            Debug.Log("Bobber in water");
        }
        if(bobberS != null && !InWater || BaitCount <= 0)
        {
            if (CatchTimer <= timeTillCatch
            && !isCollecting)
            {
                //anim.Play("PlayerIdle");
                Destroy(GameObject.Find("bobber(Clone)"));
                anim.Rebind();
                targetTime = 0.0f;
                fishingMG.startMG = false;
                poleBack = false;
                throwBobber = false;
                isFishing = false;
                CatchTimer = 0;

            }
            Debug.Log("Bobber missed water");
            
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("FishingSpot"))
        {
            FishingSpot = true;
        FishSpotIndicator.SetActive(true);
            triggerEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FishingSpot"))
        {
            FishingSpot = false;
            FishSpotIndicator.SetActive(false);
            triggerEnter = false;
        }
    }

    FishDrop fishdrop;
    
    private FishScript fishscript;
    private void OnEnable()
    {
        FishScript.OnFishScriptSpawned += RegisterFishScript;
    }

    private void OnDisable()
    {
        FishScript.OnFishScriptSpawned -= RegisterFishScript;
    }

    private void RegisterFishScript(FishScript newFishScript)
    {
        fishscript = newFishScript;
        Debug.Log("FishScript registered dynamically.");
    }
    public void fishGameWon()
    {
        BaitCount--;
        moneyDisplay.UpdateBait();
        targetTime = 0f;
        Destroy(GameObject.Find("bobber(Clone)"));
        StartCoroutine(WaitForButton());
        
        
        
    }
    public IEnumerator WaitForButton()
    {
        fishdrop.GetFish();

        winAnim = true;
        fishingMG.startMG = false;
        
        poleBack = false;
        throwBobber = false;
        
        fishGame.SetActive(false);
        CatchTimer = 0f;


        audioManager.PlaySFX(audioManager.WonFish);
        
        isCollecting = true;
        Debug.Log("Waiting for F button...");
        while (!Input.GetKeyDown(KeyCode.F))
        {
            isFishing = false;
            isCollecting = true;
            anim.Play("playerWonFish");
            
            yield return null; // Wait for the next frame
            

        }
        
        // continue execution
        Debug.Log("F button pressed...");

        anim.Rebind();//resets anim
        fishscript.Collect();

        isCollecting = false;
        isFishing = false;
        winAnim = false;



    }

    public void fishGameLose()
    {
        BaitCount--;
        moneyDisplay.UpdateBait() ;
        targetTime = 0f;
        Destroy(GameObject.Find("bobber(Clone)"));
        anim.Play("Idle");
        fishGame.SetActive(false);
        poleBack = false;
        throwBobber = false;
        isFishing = false;
        CatchTimer = 0;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x, y);
        input.Normalize();
    }
    private void Animate()
    {
        if(input.magnitude > 0.1f || input.magnitude <  -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if(moving)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        anim.SetBool("Moving", moving);
    }
}

