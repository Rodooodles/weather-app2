using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public string id;

    public bool isOnField=false;

    public bool isHidden=false;

    private GameManager gm;

    GameObject Highlight;

    public bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = Object.FindObjectOfType<GameManager>();
        
        TMP_Text label = gameObject.GetComponentInChildren<TMP_Text>();
        label.text = id;

        Highlight = transform.Find("Highlight").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Highlight.SetActive(isSelected);
    }
 
    void OnMouseDown() 
    {
        Debug.Log("Stone "+id+" has been clicked!");

        Player player = gm.GetCurrentPlayer();
        if (!player) {
            Debug.LogWarning("failed to select stone: no current player found");
            return;
        }

        player.SelectStone(this);
    }

    public void Hide()
    {
        transform.Rotate(0, 0, 180);
        isHidden = true;
    }
}
