using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePrefabScript : MonoBehaviour
{
    public static GamePrefabScript instence;

    public GamePrefabScript()
    {
        instence = this;
    }

    public void KillGamePrefab()
    {
        Destroy(gameObject);
    }
}
