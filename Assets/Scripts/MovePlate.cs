using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<RookMoves>().GetXBoard(),
            reference.GetComponent<RookMoves>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<RookMoves>().SetXBoard(matrixX);
        reference.GetComponent<RookMoves>().SetYBoard(matrixY);
        reference.GetComponent<RookMoves>().SetCoords();

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        //Switch Current Player
       controller.GetComponent<Game>().NextPlayerTurn();

        //Destroy the move plates including self
        reference.GetComponent<RookMoves>().DestroyMovePlates();
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}