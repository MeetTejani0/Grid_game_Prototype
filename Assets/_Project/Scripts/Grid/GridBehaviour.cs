using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    [Header("Starter")]
    [SerializeField] bool findDistance = false;

    [Header("Grid Setup")]
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int scale = 1;
    [SerializeField] private GameObject gridPrefab;
    private Vector3 leftBottomLocation = Vector3.zero;

    // define array
    [Header("Others")]
    public GameObject[,] gridArray;

    
    private int startX = 0;
    private int startY = 0;
    private int endX = 2;
    private int endY = 2;

    // Path
    [Header("Final Path")]
    public List<GameObject> path = new List<GameObject>();

    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;



    private GameObject player;
    private bool canMove;


    private void Awake()
    {
        leftBottomLocation = transform.position;

        gridArray = new GameObject[columns, rows];

        if (gridPrefab)
        {
            GenerateGrid();
        }
        else
        {
            Debug.Log("Prefab not available");
        }
        canMove = true;
        
    }
    private void Start()
    {
        player = Instantiate(playerPrefab,transform.position,Quaternion.identity);
    }

    private void Update()
    {
        if(findDistance && canMove)
        {
            SetDistance();
            SetPath();
            StartCoroutine(MovePlayer());
            findDistance = false;
            canMove = false;
        }
    }
    public void SetEndPosition(int x, int y)
    {
        if (canMove && endX != x && endY != y)
        {
            endX = x; endY = y;
            findDistance = true;
        }
        
    }
    private IEnumerator MovePlayer()
    {
        while(path.Count>0)
        {
            yield return new WaitUntil(() => player.transform.DOMove(path[path.Count<2? path.Count-1: path.Count-2].transform.position, 1f).IsPlaying());
            //yield return new WaitWhile(() => player.transform.DOMove(path[path.Count<2? path.Count-1: path.Count-2].transform.position, 0.2f).IsPlaying());
            yield return new WaitForSecondsRealtime(0.5f);
            path.Remove(path[path.Count-1]);
        }
        startX = endX;
        startY = endY;
        canMove = true;
    }
    private void GenerateGrid()
    {
        for(int i = 0; i < columns; i++) // generate block row and colomn wise
        {
            for(int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab,new Vector3(leftBottomLocation.x + scale*i,leftBottomLocation.y,leftBottomLocation.z+scale*j),Quaternion.identity);
                obj.transform.parent = transform;

                // fill the info in block
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;

                gridArray[i,j] = obj;
            }
        }
    }

    private void SetDistance()
    {
        Initialsetup();
        // set visit variable to ensure movement
        for(int steps = 1; steps < rows * columns; steps++)
        {
            foreach(GameObject obj in gridArray)
            {
                if(obj && obj.GetComponent<GridStat>().visited == steps - 1)
                {
                    TestFourDirection(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, steps);// check in all 4 direction
                }
            }
        }
    }
    private void SetPath()
    {
        int steps;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if (gridArray[endX,endY] && gridArray[endX,endY].GetComponent<GridStat>().visited > 0)
        {
            path.Add(gridArray[x,y]);
            steps = gridArray[x,y].GetComponent<GridStat>().visited -1;
        }
        else
        {
            Debug.Log("Can't Reach desire location");
            return;
        }

        for(int i = steps; steps > -1; steps--)
        {
            if(TestDirection(x, y, steps,1))
                tempList.Add(gridArray[x,y+1]);
            if (TestDirection(x, y, steps, 2))
                tempList.Add(gridArray[x+1, y]);
            if (TestDirection(x, y, steps, 3))
                tempList.Add(gridArray[x, y - 1]);
            if (TestDirection(x, y, steps, 4))
                tempList.Add(gridArray[x-1, y]);

            // Add Closest path

            GameObject tmpObj = ClosestTarget(gridArray[endX, endY].transform, tempList);
            path.Add(tmpObj);

            x = tmpObj.GetComponent<GridStat>().x;
            y = tmpObj.GetComponent<GridStat>().y;

            tempList.Clear();
        }

    }

    private GameObject ClosestTarget(Transform targetLocation,List<GameObject> list)
    {
        float currentDistance = rows * columns * scale; // go through all block to reach target and according to distance it will choose closest path
        int indexNumber = 0;
        for(int i=0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetLocation.position, list[i].transform.position) < currentDistance)
            {
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }
        }
        return list[indexNumber];
    }

    private void Initialsetup() // set all visited to -1 and current tile to 0
    {
        foreach(GameObject obj in gridArray)
        {
            obj.GetComponent<GridStat>().visited = -1;
        }
        gridArray[startX,startY].GetComponent<GridStat>().visited = 0;

    }

    private bool TestDirection(int x, int y,int step,int direction)
    {
        // direction 1 = up , 2 = right , 3 = down , 4 = Left
        
        switch(direction)
        {
            case 1:
                if(y +1 < rows && gridArray[x,y+1] && gridArray[x,y+1].GetComponent<GridStat>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (x + 1 < columns && gridArray[x +1, y] && gridArray[x+1, y].GetComponent<GridStat>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<GridStat>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 4:
                if (x - 1 > -1 && gridArray[x-1, y] && gridArray[x-1, y].GetComponent<GridStat>().visited == step)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default: 
                return false;
        }

    }

    private void SetVisited(int x,int y, int steps)
    {
        if (gridArray[x, y])
        {
            gridArray[x, y].GetComponent<GridStat>().visited = steps; // mark visit
        }
    }

    private void TestFourDirection(int x,int y,int steps) // test all 4 direction for -1 visited variable
    {
        if (TestDirection(x, y, -1, 1))
        {
            SetVisited(x, y +1, steps);
        }
        if(TestDirection(x, y, -1, 2))
        {
            SetVisited(x+ 1, y, steps);
        }
        if (TestDirection(x, y, -1, 3))
        {
            SetVisited(x,y-1, steps);
        }
        if(TestDirection(x, y, -1, 4))
        {
            SetVisited(x-1,y, steps);
        }
    }
}
