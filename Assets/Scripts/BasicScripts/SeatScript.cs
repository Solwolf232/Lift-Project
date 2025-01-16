using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class SeatScript : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform chairTransform;
    private Transform playerTransform;
    

    [Header("Variables")]
    private bool IsSitting = false;
    private bool IsAvabile = true;
    private bool SeatBeltOn = false;
    Vector3 originalPlayerSize;


    [Header("Npc Related")]
    [SerializeField] GameObject customer_npc_model;


    [Header("Vr Tutorials")]
    [SerializeField] GameObject SeatBelt_Tutorial;
    private GameObject new_SeatBelt_Tutorial;


    private void Start()
    {
        Invoke(nameof(SetPlayerTransform), 1.5f);

        if (customer_npc_model != null)
            RandomizeNpcSpawn();
    }
    public void SetPlayerTransform()
    {
        PlayerCamPlay playerCameraPlayScript = FindObjectOfType<PlayerCamPlay>();
        if (playerCameraPlayScript != null)
        {
            playerTransform = playerCameraPlayScript.gameObject.transform;
            originalPlayerSize = playerTransform.localScale;
        }
        else
        {
            Debug.LogError("Player Is Not Found");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Chair Clicekd");
        if (IsAvabile)
        {
            IsSitting = true;
            SitOnChair();
        }   
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (IsAvabile && !IsSitting)
        {
            IsSitting = true;
            SitOnChair();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsSitting = true;
            Debug.Log("Collided!");
            SitOnChair();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && IsSitting && !SeatBeltOn)
        {
            IsSitting = false;
            SitOnChair();
           
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(new_SeatBelt_Tutorial != null)
            {
                DestoryTutorial(new_SeatBelt_Tutorial);
                SeatBeltOn = true;
                new_SeatBelt_Tutorial = null;
            }
           
        }

    }


    private void SitOnChair()
    {

      //  PlayerController playerMovement = playerTransform.gameObject.GetComponent<PlayerController>();
         PlayerMovement_Vr playerMovement_Vr = playerTransform.gameObject.GetComponent<PlayerMovement_Vr>();
        if (IsSitting)
        {

            //   playerMovement.canMove = false;
            playerMovement_Vr.CanMove = false;
            playerTransform.position = chairTransform.position;
            playerTransform.rotation = chairTransform.rotation;
            playerTransform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            SeatBelt_Tutorial_Method(IsSitting);

        }
        else
        {

            playerTransform.localScale = originalPlayerSize;
            //    playerMovement.canMove = true;
            playerMovement_Vr.CanMove = true;

            Vector3 moveOut = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
            playerTransform.position = moveOut;

            SeatBelt_Tutorial_Method(IsSitting);

        }
    }

    private void RandomizeNpcSpawn()
    {
        int spawnNum;
        Diffuclty currentDiffuclty = GameManagerScript.instance.LevelDiff;
        switch (currentDiffuclty)
        {
            case Diffuclty.Easy:
                spawnNum = Random.Range(0, 6); // Randomize Between More Numbers So Less People Will Spawn 
                if (spawnNum > 0 && spawnNum < 5)
                {
                    SpawnNpc(customer_npc_model);
                }
                break;

            case Diffuclty.Medium:
                spawnNum = Random.Range(0, 10);  // Randomize Between Less Numbers So More People Will Spawn 
                if (spawnNum > 0 && spawnNum < 5)
                {
                    SpawnNpc(customer_npc_model);
                }
                break;

            case Diffuclty.Hard:
                spawnNum = Random.Range(0, 7);  // Randomize Between Less Numbers So More People Will Spawn 
                if (spawnNum > 0 && spawnNum < 5)
                {
                    SpawnNpc(customer_npc_model);
                }
                break;
        }

    }

    private void SeatBelt_Tutorial_Method(bool Sitting)
    {
        if (Sitting)
        {
            Debug.Log("Tutorial Spawned!");
            new_SeatBelt_Tutorial = SpawnTutorial();

        }
        else if(!IsSitting && new_SeatBelt_Tutorial != null)
        {
            DestoryTutorial(new_SeatBelt_Tutorial);
        }
        
    }

    private GameObject SpawnTutorial()
    {
        Vector3 tutorial_spawn_point = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z - 1f);
        GameObject new_SeatBelt_Tutorial = Instantiate(SeatBelt_Tutorial, tutorial_spawn_point, Quaternion.identity);

        return new_SeatBelt_Tutorial;
    }
    private void DestoryTutorial(GameObject Tutorial)
    {
        Destroy(Tutorial);
        Debug.Log("Destroyed Tutorial!");
    }

    private void SpawnNpc(GameObject NpcModel)
    {
      GameObject new_customer_Npc = Instantiate(NpcModel,chairTransform.position,Quaternion.identity);
        IsAvabile = false;
        new_customer_Npc.transform.SetParent(this.transform);
        Debug.Log("Customer Spawned!");
    }
}
