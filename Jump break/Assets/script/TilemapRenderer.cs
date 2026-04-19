using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRenderer : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }
}
