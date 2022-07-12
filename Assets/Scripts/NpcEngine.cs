using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcEngine : MonoBehaviour
{
    public Transform chatBackGround;
    public Transform npcCharacter;

    //private DialougeSystem dialougeSystem;
    public GameObject dialougeSystem;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(npcCharacter.position);
        Pos.y += 175;
        chatBackGround.position = Pos;

    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<NpcEngine>().enabled = true;
        //FindObjectOfType<DialougeSystem>().EnterRangeOfNpc();
        if((other.gameObject.tag == "Player"))
        {
            this.gameObject.GetComponent<NpcEngine>().enabled = true;
            dialougeSystem.SetActive(true);

        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        dialougeSystem.SetActive(false);
    }
}
