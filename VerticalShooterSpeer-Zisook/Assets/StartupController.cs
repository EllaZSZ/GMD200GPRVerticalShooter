using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    [SerializeField] private GameObject Game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Destroy(gameObject);
            Instantiate(Game);
            ScoreController.score = 0;
        }
    }
}
