using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FlightState
{
    BoardingState,
    TakeOffState,
    InFlightState,
    LandingState,
}
public enum Diffuclty
{
    Easy,
    Medium,
    Hard
}
public class GameManagerScript : MonoBehaviour
{
    [Header("States")]
    public FlightState currentState;

    [Header("Diffculty")]
    public Diffuclty LevelDiff;

    [Header("Boarding State Positions")]
    [SerializeField] private Transform player_Spawn_Pos;
    [SerializeField] private Transform flight_attendent_Spawn_Pos;

    [Header("Instance")]
    public static GameManagerScript instance;

    #region Unity Methods

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentState = FlightState.BoardingState;
        LevelDiff = Diffuclty.Easy;
        ChanageCurrentState(currentState);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region StateManagement

    public void ChanageCurrentState(FlightState newState)
    {
        switch (newState)
        {
            case FlightState.BoardingState:
                BoardingThePlane_State();
                break;
            case FlightState.TakeOffState:
                TakeOff_State();
                break;
        }
    }

    #endregion

    #region States
    public void BoardingThePlane_State()
    {
        if (flight_attendent_Spawn_Pos != null && player_Spawn_Pos != null)
            SpawnObjects.instance.BoardingPlaneSpawning(flight_attendent_Spawn_Pos, player_Spawn_Pos);

        if (SoundManagerScript.instance != null)
        {
            SoundManagerScript.instance.PlayJetSound();
        }
    }

    public void TakeOff_State()
    {

    }

    #endregion
}
