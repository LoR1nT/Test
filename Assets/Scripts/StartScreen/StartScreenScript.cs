using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour
{
    public static StartScreenScript instence;

    public StartScreenScript()
    {
        instence = this;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
