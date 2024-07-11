using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{
    public int visited = -1;
    public int x = 0;
    public int y = 0;
    public GridType type = GridType.Path;

    //[SerializeField] private GameObject indicator;

/*    public void StartIndicator()
    {
        indicator.SetActive(true);
    }
    public void EndIndicator()
    {
        indicator.SetActive(false);
    }*/

    public enum GridType
    {
        Path,
        Obstacle,
        Other,
        Enemy
    }

}
