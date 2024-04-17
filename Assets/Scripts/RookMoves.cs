using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RookMoves : MonoBehaviour
{
    
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject movePlate;

    int bHeight;
    int bWidth;

    private GameObject winningScreen;

    //Position for this Chesspiece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;

    int clicks = 0;

    //Variable for keeping track of the player it belongs to "black" or "white"

    public Sprite rook;

    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetBoardDimensions(controller.GetComponent<Game>().positions.GetLength(0), controller.GetComponent<Game>().positions.GetLength(1));

        //Take the instantiated location and adjust transform
        SetCoords();

        this.GetComponent<SpriteRenderer>().sprite = rook;
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.635f;
        y *= 0.635f;

        //Add constants (pos 0,0)
        x += -2.25f;
        y += -2.25f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        clicks++;

        if (clicks == 1)
        {
            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }

        clicks = 0;
    }

    public void InitiateMovePlates()
    {
        DestroyMovePlates();

        this.name = "black_rook";
        LineMovePlate(-1, 0);
        LineMovePlate(0, -1);
    }

    public void SetBoardDimensions(int width, int height)
    {
        bWidth = width;
        bHeight = height;
    }

    private bool CanMoveToPosition(int x, int y)
    {
        // Check if the position is within the bounds of the board
        if (x < 0 || x >= bWidth || y < 0 || y >= bHeight)
        {
            return false;
        }

        Game gm = controller.GetComponent<Game>();

        // Check if there's no piece at the position
        if (gm.GetPosition(x, y) != null)
        {
            return false;
        }


        return true;
    }

    void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
    }

    void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.635f;
        y *= 0.635f;

        //Add constants (pos 0,0)
        x += -2.25f;
        y += -2.25f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Debug.Log("WIN");
            gameObject.SetActive(false);

            winningScreen.SetActive(true);

            SceneManager.LoadScene("MainMenu");
            
        }
    }
}