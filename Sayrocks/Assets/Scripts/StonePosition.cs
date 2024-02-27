using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class StonePosition : MonoBehaviour
{
    public Stone stone;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStone(Stone stone) {
        this.stone = stone;

        stone.transform.position = new Vector3(this.transform.position.x, stone.transform.position.y, this.transform.position.z);
        stone.isOnField = true;
    }

    public void ClearStone() {
        stone = null;
    }
}
