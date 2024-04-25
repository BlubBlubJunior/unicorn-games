using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gridbehavior : MonoBehaviour
{
    public int rows = 10;
    public int cloums = 10;
    public int scale = 1;
    public GameObject gridprefab;
    public Vector3 leftbottomLocation = new Vector3(0, 0, 0);

    public GameObject[,] gridarray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;

    public List<GameObject> path = new List<GameObject>();

    public bool finddistance = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (finddistance)
        {
            setDistance();
            setpath();
            finddistance = false;
        }
    }

    void generategrid()
    {
        for (int i = 0; i < cloums; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridprefab, new Vector3(leftbottomLocation.x + scale * i, leftbottomLocation.y, leftbottomLocation.z + scale * j), quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;
                gridarray[i, j] = obj;
            }
        }
    }

    void initialSetup()
    {
        foreach (GameObject obj in gridarray)
        {
            obj.GetComponent<GridStat>().visited = -1;
        }

        gridarray[startX, startY].GetComponent<GridStat>().visited = 0;
    }

    bool testDirection(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if (y + 1 < rows && gridarray[x, y + 1] && gridarray[x, y + 1].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;
            case 2:
                if (x + 1 < cloums && gridarray[x  + 1, y] && gridarray[x + 1, y].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;
            case 3:
                if (y - 1 > -1 && gridarray[x, y - 1] && gridarray[x, y - 1].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 > -1 && gridarray[x - 1, y] && gridarray[x - 1, y].GetComponent<GridStat>().visited == step)
                    return true;
                else
                    return false;
        }

        return false;
    }

    void setDistance()
    {
        initialSetup();
        int x = startX;
        int y = startY;
        int[] testarray = new int[rows * cloums];

        for (int step = 1; step < rows * cloums; step++)
        {
            foreach (GameObject obj in gridarray)
            {
                if (obj && obj.GetComponent<GridStat>().visited == step - 1)
                {
                    testFourDirections(obj.GetComponent<GridStat>().x, obj.GetComponent<GridStat>().y, step);
                }
            }
        }
    }

    void setpath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> templist = new List<GameObject>();
        path.Clear();
        if (gridarray[endX,endY] && gridarray[endY,endX].GetComponent<GridStat>().visited > 0)
        {
            path.Add(gridarray[x,y]);
            step = gridarray[x, y].GetComponent<GridStat>().visited - 1;
        }
        else
        {
            print(" can't reach the desired location");
            return;
        }

        for (int i = step; step > -1; step--)
        {
            if (testDirection(x,y,step,1))
                templist.Add(gridarray[x,y + 1]);
            
            if (testDirection(x,y,step,2))
                templist.Add(gridarray[x + 1,y]);
            
            if (testDirection(x,y,step,3))
                templist.Add(gridarray[x,y - 1]);
            
            if (testDirection(x,y,step,4))
                templist.Add(gridarray[x - 1,y]);
            
        }

        GameObject tempobj = findClosest(gridarray[endX, endY].transform, templist);
        path.Add(tempobj);
        x = tempobj.GetComponent<GridStat>().x;
        y = tempobj.GetComponent<GridStat>().y;
        templist.Clear();
    }
    void testFourDirections(int x, int y, int step)
    {
        if (testDirection(x,y,-1,1))
            setVisited(x,y+1,step);
        
        if (testDirection(x,y,-1,2))
            setVisited(x + 1,y,step);
        
        if (testDirection(x,y,-1,3))
            setVisited(x,y-1,step);
        
        if (testDirection(x,y,-1,4))
            setVisited(x-1,y,step);
        
    }
    void setVisited(int x, int y, int step)
    {
        if (gridarray[x,y])
        {
            gridarray[x, y].GetComponent<GridStat>().visited = step;
        }
    }

    GameObject findClosest(Transform targetlocation, List<GameObject> list)
    {
        float currentdistance = scale * rows * cloums;
        int indexnumber = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetlocation.position, list[i].transform.position) < currentdistance)
            {
                currentdistance = Vector3.Distance(targetlocation.position, list[i].transform.position);
                indexnumber = i;
            }
        }

        return list[indexnumber];
    }
    private void Awake()
    {
        gridarray = new GameObject[cloums, rows];
        if (gridprefab)
        {
            generategrid();
        }
        else
        {
            print(" missing gridprefab");
        }
    }
}
