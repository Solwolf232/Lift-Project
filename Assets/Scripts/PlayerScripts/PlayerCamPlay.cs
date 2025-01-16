using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamPlay : MonoBehaviour
{
    [Header("Player Componenets")]
    private PlayerController playerController;
    public float playerlookSpeed;

    [Header("Models To Look At")]

    [HideInInspector] public Transform flight_Attendent_pos;


    [Header("OffSets")]
    public Vector3 camerOffset;
    public bool IsLookingAtNpc;

    #region Unity Methods
    private void Start()
    {
        playerController = GetComponent<PlayerController>();


    }
    private void Update()
    {
        PlayerLookAtNpc();
    }

    public void setNpc_Pos(Transform pos)
    {
        flight_Attendent_pos = pos;
    }

    private void LateUpdate()
    {

        if (playerController.LooksAtAttendent)
        {
            PlayerLooking(flight_Attendent_pos);
        }



    }

    #endregion

    #region Act Methods
    public void PlayerLookAtNpc()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerController.LooksAtAttendent = true;
            IsLookingAtNpc = playerController.LooksAtAttendent;
            Debug.Log("Looking");
        }

    }

    public void PlayerLooking(Transform target)
    {
        Vector3 direction = target.position - transform.position;

        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerlookSpeed);

    }

    #endregion
    #region GetComponenets



    #endregion
}
