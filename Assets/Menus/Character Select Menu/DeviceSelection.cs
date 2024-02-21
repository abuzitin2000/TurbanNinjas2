using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceSelection : MonoBehaviour
{
    public GameObject gamepad1;
    public GameObject gamepad2;
    public GameObject keyboard1;
    public GameObject keyboard2;

    private bool gamepad1Player1;
    private bool gamepad2Player1;
    private bool keyboard1Player1;
    private bool keyboard2Player1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Left(bool player1, bool active, bool gamepad)
    {

    }

    public void Right(bool player1, bool active, bool gamepad)
    {

    }

    public void Select()
    {
        PlayerInputPairing pairings = GameObject.FindWithTag("InputManager").GetComponent<PlayerInputPairing>();
        pairings.gamepad1.GetComponent<PlayerInputBattle>().player1 = gamepad1Player1;
        pairings.gamepad2.GetComponent<PlayerInputBattle>().player1 = gamepad2Player1;
        pairings.keyboard1.GetComponent<PlayerInputBattle>().player1 = keyboard1Player1;
        pairings.keyboard2.GetComponent<PlayerInputBattle>().player1 = keyboard2Player1;


    }
}
