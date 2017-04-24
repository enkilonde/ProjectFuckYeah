using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSpotGizmo : MonoBehaviour
{
    public Mesh ship;
    private void OnDrawGizmos()
    {
        if (ship == null || Application.isPlaying) return;

        Gizmos.DrawMesh(ship, transform.position, transform.rotation, transform.localScale);

    }
}
