using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_SeatManager : MonoBehaviour
{
    [Header("Seat Booleans")]
    public bool Is_Sitted = false;
    public bool Is_Seat_Belt_On = false;

    private event Action OnSeatBeltStatusChanged;
}
