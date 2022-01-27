using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowState : MonoBehaviour
{
    private TMP_Text txt;
    public Player_Movement pMovement;

    void Start() {
        txt = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "DECREE 0.0.1 \nstate: " + pMovement.currentState;
    }
}
