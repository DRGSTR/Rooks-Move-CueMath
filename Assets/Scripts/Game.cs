
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instance;
    //Reference from Unity IDE
    public GameObject piece;
    public Timer timer;
    private bool player1 = true;//current turn

    public GameObject panelPlayer1;
    public GameObject panelPlayer2;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] rook = new GameObject[1];

    //Game Ending
    //private bool gameOver = false;

    [SerializeField]
    private GameObject winningScreen;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
        winningScreen.SetActive(false);
        instance = this;
        rook = new GameObject[] { Create("black_rook", 7, 7) };

        //Set all piece positions on the positions board
        for (int i = 0; i < rook.Length; i++)
        {
            SetPosition(rook[i]);
        }
        panelPlayer1.SetActive(true);
        panelPlayer2.SetActive(false);
    }

    private void Update()
    {
        CheckRookPosition();
    }
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(piece, new Vector3(0, 0, -1), Quaternion.identity);
        RookMoves rookMoves = obj.GetComponent<RookMoves>(); //We have access to the GameObject, we need the script
        rookMoves.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        rookMoves.SetXBoard(x);
        rookMoves.SetYBoard(y);
        rookMoves.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        RookMoves rookMoves = obj.GetComponent<RookMoves>();

        //Overwrites either empty space or whatever was there
        positions[rookMoves.GetXBoard(), rookMoves.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public void CheckRookPosition()
    {
        if (GetPosition(0, 0) == rook[0])
        {
            timer.StopTimer();
            panelPlayer1.SetActive(false);
            panelPlayer2.SetActive(false);
            winningScreen.SetActive(true);
        }
    }

    public void NextPlayerTurn()
    {
        if (timer == null)
        {
            Debug.LogError("The timer variable is null!");
            return;
        }

        timer.ResetTimer();
        player1 = !player1;
        panelPlayer1.SetActive(player1);
        panelPlayer2.SetActive(!player1);
    }

    public bool Isplayer1()
    {
        return player1;
    }
}
