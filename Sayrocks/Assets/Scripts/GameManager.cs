using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player currPlayer;

    public StonePosition[] stonePositions;
    public int centerStoneIndex;
    public int leftEdge;
    public int rightEdge;
    public TextMeshProUGUI debugLeftIndex;
    public TextMeshProUGUI debugRightIndex;


    // Start is called before the first frame update
    void Start()
    {
        leftEdge = centerStoneIndex;
        rightEdge = centerStoneIndex;
    }

    // Update is called once per frame
    void Update()
    {
        debugLeftIndex.text = leftEdge.ToString();
        debugRightIndex.text = rightEdge.ToString();
    }

    public Player GetCurrentPlayer() {
        return currPlayer;
    }

    private void shiftRight() {
        for (int i = stonePositions.Length; i >= 0; i--) {
            int prev = i-1;
            if (prev >= 0) {
                Stone prevStone = stonePositions[prev].stone;
                if (!prevStone) {
                    continue;
                }
                stonePositions[i].SetStone(prevStone);
                stonePositions[prev].ClearStone();
            }
        }
        leftEdge = 0;
        rightEdge++;
    }

    private void shiftLeft() {
        for (int i = 0; i < stonePositions.Length; i++) {
            int next = i+1;
            if (next < stonePositions.Length) {
                Stone nextStone = stonePositions[next].stone;
                if (!nextStone) {
                    continue;
                }
                stonePositions[i].SetStone(nextStone);
                stonePositions[next].ClearStone();
            }
        }
        rightEdge = stonePositions.Length-1;
        leftEdge--;
    }


    public void PlaceLeft() {
        if (!currPlayer) {
            Debug.LogError("failed to place stone: no current player designated");
            return;
        }

        Stone stone = currPlayer.currStone;
        if (!stone) {
            Debug.LogError("failed to place stone: no current stone selected");
            return;
        }

        if (stone.isOnField)
        {
            Debug.LogWarning("failed to place stone: " + stone.id + " is already on the field");
            return;
        }

        Debug.Log("Player " + currPlayer.id + " is placing stone " + stone.id + " left");

        // move selected stone to target stone position
        // TODO animate this movement
        StonePosition targetPosition = stonePositions[leftEdge];
        if (targetPosition.stone) {
            leftEdge--;
            if (leftEdge < 0) {
                shiftRight();
            }

            targetPosition = stonePositions[leftEdge];
        }
        targetPosition.SetStone(stone);
        currPlayer.ClearStone();
    }

    public void PlaceRight() {
        if (!currPlayer) {
            Debug.LogError("failed to place stone: no current player designated");
            return;
        }

        Stone stone = currPlayer.currStone;
        if (!stone) {
            Debug.LogError("failed to place stone: no current stone selected");
            return;
        }

        if (stone.isOnField)
        {
            Debug.LogWarning("failed to place stone: " + stone.id + " is already on the field");
            return;
        }

        Debug.Log("Player " + currPlayer.id + " is placing stone " + stone.id + " right");

        // move selected stone to target stone position
        // TODO animate this movement
        StonePosition targetPosition = stonePositions[rightEdge];
        if (targetPosition.stone) {
            rightEdge++;
            if (rightEdge >= stonePositions.Length) {
                shiftLeft();
            }

            targetPosition = stonePositions[rightEdge];
        }
        targetPosition.SetStone(stone);
        currPlayer.ClearStone();
    }


    public void HideStone() {
        if (!currPlayer) {
            Debug.LogError("failed to hide stone: no current player designated");
            return;
        }

        Stone stone = currPlayer.currStone;
        if (!stone) {
            Debug.LogError("failed to hide stone: no current stone selected");
            return;
        }

        if (!stone.isOnField)
        {
            Debug.LogWarning("failed to hide stone: " + stone.id + " is not on the field");
            return;
        }

        if (stone.isHidden)
        {
            Debug.LogWarning("failed to hide stone: " + stone.id + " is already hidden");
            return;
        }

        Debug.Log("Player " + currPlayer.id + " is hiding stone " + stone.id);

        stone.Hide();
        currPlayer.ClearStone();
    }

    public void StartSwapStones()
    {
         if (!currPlayer) {
            Debug.LogError("failed to swap stone: no current player designated");
            return;
        }
        Stone stone = currPlayer.currStone;
        if (!stone) {
            Debug.LogError("failed to swap stone: no current stone selected");
            return;
        }

        if (!stone.isOnField)
        {
            Debug.LogWarning("failed to swap stone: " + stone.id + " is not on the field");
            return;
        }
        currPlayer.isSwapping = true;
        // TODO put game manager in swapping stones state, wait for another on-field stone to be
        // selected, then have it report to game manager (don't replace current stone) and then
        // if we're in swapping state, swap them.
    }
}
