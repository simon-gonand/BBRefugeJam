using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocSelector : MonoBehaviour
{
    public static BlocSelector Instance;


    //Blocs
    public GameObject currentBloc;

    public enum Blocs
    {
        test,
        total
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        #region Make Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void chooseCurrentBloc(Blocs b)
    {
        switch (b)
        {
            case Blocs.test,
                currentBloc = 
                break;
            default:

                break;
        }
    }*/
}
