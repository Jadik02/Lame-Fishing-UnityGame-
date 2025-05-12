using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMG : MonoBehaviour
{
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;
    [SerializeField] Transform fish;

    float fishPos;
    float fishGoal;

    float fishSpeed;
    [SerializeField] float MoveTime = 1f;
    float catchTime;
    [SerializeField] float timer = 1f;

    [SerializeField] Transform hook;
    float hookPos;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 0.5f;
    float hookProgress;
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.009f;
    [SerializeField] float ProgressLossMultiplier = 0.1f;
    [SerializeField] SpriteRenderer hookSprite;

    [SerializeField] Transform ProgressBarScale;
    [SerializeField] float failTime = 10f;

    public bool startMG = true;
    
    public bool won = false;
    public bool lose = false;

    [SerializeField] PlayerController PlayerContr;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hookPullVelocity = 0f;
        hookPos = UnityEngine.Random.Range(0f, 1f);
        fishPos = UnityEngine.Random.Range(0f, 1f);
        Resize();
    }
    private void FixedUpdate()
    {
        if (startMG)
        {
            
            Pull();
            Fish();
            ProgressUpdate();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(won)
        {
            
            hookPullVelocity = 0f;
            hookPos = UnityEngine.Random.Range(0f, 1f);
            fishPos = UnityEngine.Random.Range(0f, 1f);
            hookProgress = 0f;
            failTime = 10f;
            
            startMG = false;
            won = false;
            
            PlayerContr.fishGameWon();
        }

        if (lose)
        {
            
            hookPullVelocity = 0f;
            hookPos = UnityEngine.Random.Range(0f, 1f);
            fishPos = UnityEngine.Random.Range(0f, 1f);
            hookProgress = 0f;
            failTime = 10f;

            startMG = false;
            lose = false;
            

            PlayerContr.fishGameLose();
        }
        

       /*if (startMG)
        {
            Fish();
            
            ProgressUpdate();
        }
        */
        
    }

    
    void Fish()
    {
        catchTime -= Time.deltaTime;
        if (catchTime < 0f)
        {
            catchTime = UnityEngine.Random.value * timer;
            fishGoal = UnityEngine.Random.value;
        }
        fishPos = Mathf.SmoothDamp(fishPos, fishGoal, ref fishSpeed, MoveTime);
        fish.position = Vector3.Lerp(bottom.position, top.position, fishPos);
    }
    void Pull()
    {
        
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime; /// 0.016f)
        }
        
        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        hookPos += hookPullVelocity;
        hookPos = Mathf.Clamp(hookPos, hookSize / 2, 1 - hookSize / 2); 
        hook.position = Vector3.Lerp(bottom.position, top.position, hookPos);

        if(hookPos - hookSize / 2 <= 0.01f && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }
        if (hookPos + hookSize / 2 >= 0.99f && hookPullVelocity > 0f)
        {
            hookPullVelocity = 0f;
        }
    }
    void Resize()
    {
        Bounds b = hookSprite.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(top.position, bottom.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = ls;
    }
    void ProgressUpdate()
    {
        

        Vector3 ls = ProgressBarScale.localScale;
        ls.y = hookProgress;
        ProgressBarScale.localScale = ls;

        float min = hookPos - hookSize / 2;
        float max = hookPos + hookSize / 2;

        if (min < fishPos && fishPos < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= ProgressLossMultiplier * Time.deltaTime;
            failTime -= Time.deltaTime;
            if(failTime < 0f)
            {
                Debug.Log("lose");
                lose = true;
                startMG = false;
                
            }
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);

        if(hookProgress >= 1f)
        {
            Debug.Log("Won");
            won = true;
        }
    }
}
