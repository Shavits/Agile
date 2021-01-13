using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambulance : MonoBehaviour
{

    private static Ambulance instance;
    private Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    public static Ambulance GetInstance()
    {
        return instance;
    }

    public void PersonEnter()
    {
        anim.SetTrigger("PersonEnter");
    }
}
