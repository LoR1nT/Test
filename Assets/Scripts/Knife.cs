using BzKovSoft.ObjectSlicer.Samples;
using UnityEngine;

public class Knife : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        GameController.instence.Cut(other.gameObject);
    }
}
