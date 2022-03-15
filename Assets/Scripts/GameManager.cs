using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Protocol
    /*
     * This is a proposal only
     Message = playerID [space] positionID

     int that represents position of the player's step.
     [0][1][2]
     [3][4][5]
     [6][7][8]
     */
    #endregion

    private void Start()
    {
        networkManager.StartUDP();
    }

    public NetworkManager networkManager; //don't forget to drag in inspector

    public void GotNetworkMessage(string message)
    {
        Debug.Log("got network message: " + message);
        switch (message)
        {
            //do something with the message
            //case 5:
            //Do something
        }
    }

    public void PositionClicked(int position)
    {
        //draw the shape on the UI

        //update the other player about the shape
        networkManager.SendMessage("");// your job to finish it
    }

    //for debug purpouses only
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            networkManager.SendMessage("A was sent");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            networkManager.SendMessage("B was sent");
        }
    }
}