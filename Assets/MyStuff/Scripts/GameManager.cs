using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool CanPlay = true;
    public Transform TicTacToe;
    public Text ImPlayer;
    public GameObject YouWin;
    public GameObject YouLose;
    public GameObject ResetButton;
    public GameObject ChoosePlayer;
    List<Text> texts = new List<Text>();

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
        //networkManager.StartUDP();
        foreach (Transform t in TicTacToe)
        {
            texts.Add(t.GetChild(0).GetComponent<Text>());
        }
        foreach (Text t in texts)
        {
            t.text = string.Empty;
        }
        ImPlayer.text = "Im Player 1";
    }

    public NetworkManager networkManager; //don't forget to drag in inspector

    public void GotNetworkMessage(string message)
    {
        Debug.Log("got network message: " + message);
        /*
        switch (message)
        {
            //do something with the message
            //case 5:
            //Do something

            
        }*/
        int number;

        bool success = int.TryParse(message, out number);
        if (success)
        {
            if (number > -1 && number < 9)
            {
                EnemyPressedSpot(number);
            }
        }
        else if (message == "Reset")
        {
            Reseting();
        }
        else
        {
            Debug.Log("Wrong Msg");
        }
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

    public void PressSpot(int Num)
    {
        if (texts[Num].text == string.Empty && CanPlay)
        {
            networkManager.SendMessage(Num.ToString());
            texts[Num].text = "X";
            CanPlay = false;
            CheckIfWin("X");
        }
    } 

    public void EnemyPressedSpot(int Num)
    {
        if (texts[Num].text == string.Empty)
        {
            texts[Num].text = "O";
            CanPlay = true;
            CheckIfWin("O");
        }
    }

    public void CheckIfWin(string x)
    {
        if (texts[0].text==x && texts[1].text == x && texts[2].text == x ||
            texts[3].text == x && texts[4].text == x && texts[5].text == x ||
            texts[6].text == x && texts[7].text == x && texts[8].text == x ||
            texts[0].text == x && texts[3].text == x && texts[6].text == x ||
            texts[1].text == x && texts[4].text == x && texts[7].text == x ||
            texts[2].text == x && texts[5].text == x && texts[8].text == x ||
            texts[0].text == x && texts[4].text == x && texts[8].text == x ||
            texts[2].text == x && texts[4].text == x && texts[6].text == x 
            )
        {
            CanPlay = false;
            if (x == "X")
            {
                YouWin.SetActive(true);
            }
            else
            {
                YouLose.SetActive(true);
            }
        }
    }

    public void SwitchPlayer(int player)
    {
        if (player == 2)
        {
            ImPlayer.text = "Im Player 2";
            networkManager.ListeningPort = 40001;
            networkManager.SendingPort = 40000;
        }
        else
        {
            ImPlayer.text = "Im Player 1";
            networkManager.ListeningPort = 40000;
            networkManager.SendingPort = 40001;
        }
        TicTacToe.gameObject.SetActive(true);
        ResetButton.SetActive(true);
        ChoosePlayer.SetActive(false);
        networkManager.StartUDP();

    }

    public void Reset()
    {
        Reseting();
        networkManager.SendMessage("Reset");
    }

    void Reseting()
    {
        foreach (Text t in texts)
        {
            t.text = string.Empty;
        }
        CanPlay = true;
        YouWin.SetActive(false);
        YouLose.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}