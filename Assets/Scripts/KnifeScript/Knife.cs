using BzKovSoft.ObjectSlicer.Samples;
using UnityEngine;

public class Knife : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GamePlayController.instence.Cut(other.gameObject);
    }
}
