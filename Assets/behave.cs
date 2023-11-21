using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class behave : MonoBehaviour
{
    public bool findDistance = false;
    public int rows = 10;
    public int coloums = 10;
    public int scale = 1;
    public GameObject gridprefab;
    public Vector3 leftbottomlocation = new Vector3(0, 0, 0);
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 0;
    public int endY = 0;
    public List<GameObject> path = new List<GameObject>();

    void Awake()
    {
        gridArray = new GameObject[coloums, rows];
        if (gridprefab)
        {
            generategrid();
        }
        else
        {
            print("missing gridprefab, please assign.");
        }
    }

    void Update()
    {
        if (findDistance)
        {
            setdistance();
            setpath();
            findDistance = false;
        }
    }

    void generategrid()
    {
        print("Generating grid...");
        for (int i = 0; i < coloums; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridprefab,
                    new Vector3(leftbottomlocation.x + scale * i, leftbottomlocation.y, leftbottomlocation.z + scale * j), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<bty>().x = i;
                obj.GetComponent<bty>().y = j;
                gridArray[i, j] = obj;
            }
        }
    }

    void setdistance()
    {
        initialsetup();
        int x = startX;
        int y = startY;
        int[] testarray = new int[rows * coloums];
        for (int step =1; step < rows * coloums;step++)
        {
            foreach (GameObject obj in gridArray)
            {
                if (obj.GetComponent<bty>().visit == step-1)
                {
                    testfourdirections(obj.GetComponent<bty>().x, obj.GetComponent<bty>().y, step);
                }
            }
        }
    }
    void initialsetup()
    {
        foreach (var obj in gridArray)
        {
            obj.GetComponent<bty>().visit = -1;
        }

        gridArray[startX, startY].GetComponent<bty>().visit = 0;
    }

    bool testdirection(int x, int y, int step, int direction)
    {
        switch (direction)
        {
            case 1:
                if (y + 1 < rows && gridArray[x, y + 1] && gridArray[x, y + 1].GetComponent<bty>().visit == step)
                    return true;
                else
                    return false;
            case 2:
                if (x + 1 < coloums && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<bty>().visit == step)
                    return true;
                else
                    return false;
            case 3:
                if (y - 1 > -1 && gridArray[x, y - 1] && gridArray[x, y - 1].GetComponent<bty>().visit == step)
                    return true;
                else
                    return false;
            case 4:
                if (x - 1 > -1 && gridArray[x + 1, y] && gridArray[x + 1, y].GetComponent<bty>().visit == step)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }
    void testfourdirections(int x, int y, int step)
    {
        if (testdirection(x,y,-1,1))
            setvisit(x,y+1,step);
        if (testdirection(x,y,-1,2))
            setvisit(x,y-1,step);
        if (testdirection(x,y,-1,3))
            setvisit(x+1,y,step);
        if (testdirection(x,y,-1,4))
            setvisit(x-1,y,step);
    }
    void setvisit(int x, int y, int step)
    {
        if (gridArray[x,y])
        {
            gridArray[x, y].GetComponent<bty>().visit = step;
        }
    }

    void setpath()
    {
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> templist = new List<GameObject>();
        path.Clear();
        if (gridArray[endX, endY] && gridArray[endX, endY].GetComponent<bty>().visit > 0)
        {
            path.Add(gridArray[x, y]);
            step = gridArray[x, y].GetComponent<bty>().visit - 1;
        }
        else
        {
            print("can't reach");
            return;
        }

        for (int i = step; i >= 0; i--)
        {
            if (testdirection(x, y, i, 1))
                templist.Add(gridArray[x, y + 1]);
            if (testdirection(x, y, i, 2))
                templist.Add(gridArray[x + 1, y]);
            if (testdirection(x, y, i, 3))
                templist.Add(gridArray[x, y - 1]);
            if (testdirection(x, y, i, 4))
                templist.Add(gridArray[x - 1, y]);
        }

        GameObject tempobj = findcloset(gridArray[endX, endY].transform, templist);
        path.Add(tempobj);
        x = tempobj.GetComponent<bty>().x;
        y = tempobj.GetComponent<bty>().y;
        templist.Clear();
    }
    

    GameObject findcloset(Transform targetlocation, List<GameObject> list)
    {
        float currentdistance = scale * rows * coloums;
        int indexNumber = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(targetlocation.position, list[i].transform.position) < currentdistance)
            {
                currentdistance = Vector3.Distance(targetlocation.position, list[i].transform.position);
                indexNumber = i;
            }

        }

        return list[indexNumber];
    }
}
