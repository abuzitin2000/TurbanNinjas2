using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        // Have only one object of this type
        DontDestroy[] donts = FindObjectsOfType<DontDestroy>();

        for (int i = 0; i < donts.Length; i++)
        {
            if (donts[i] == this)
            {
                continue;
            }

            if (donts[i].name == this.name)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
