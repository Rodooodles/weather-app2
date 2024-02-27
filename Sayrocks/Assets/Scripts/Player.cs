using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;

    public Stone currStone;

    public bool isSwapping = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectStone(Stone stone)
    {   
        if (isSwapping)
        {
            SwapStones(currStone, stone);
            return;
        }
        ClearStone();
        this.currStone = stone;

        this.currStone.isSelected = true;

    }

    public void SwapStones(Stone stoneA, Stone stoneB)
    {
        if (!stoneA.isOnField || !stoneB.isOnField)
        {
            Debug.LogWarning("failed to swap stones: " + stoneA.id + "or" + stoneB.id + " is not on the field");
            return;
        }
        Vector3 stoneAPos = stoneA.transform.position;
        stoneA.transform.position = stoneB.transform.position;
        stoneB.transform.position = stoneAPos;
        ClearStone();
        isSwapping = false;
    }

    public void ClearStone()
    {
        if (this.currStone != null)
        {
            this.currStone.isSelected = false;
        }
        
        this.currStone = null;
    }
}
