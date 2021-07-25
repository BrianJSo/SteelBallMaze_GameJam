using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSpawner : MonoBehaviour
{
    /*
    [SerializeField] private List<Room> roomList;
    private Room curRoom;
    private Room nextRoom;

    // Start is called before the first frame update
    void Start()
    {
        curRoom = GameObject.Instantiate(this.roomCopy, this.transform);
        curRoom.transform.position = spawnOrigin.transform.position;
        roomList.Add(curRoom);

        for(int i=0; i<5; i++)
        {
            nextRoom = GameObject.Instantiate(this.roomCopy, this.transform);
            nextRoom.transform.position = curRoom.spawnAndGetPosition(GraphGameEventNames.LOCAL_NORTH);
            curRoom = nextRoom;
            roomList.Add(curRoom);
        }
    }
    */

    [SerializeField] private GameObject spawnOrigin;
    [SerializeField] private Room roomCopy;
    [SerializeField] private List<Room> roomList;

    int gridSizeX = 8, gridSizeY = 8;
    List<Vector2> takenPositions = new List<Vector2>();
    public int numberOfRooms = 10;

    Queue<Room> branches = new Queue<Room>();

    // Start is called before the first frame update
    void Start()
    {
        if (numberOfRooms >= (gridSizeX) * (gridSizeY))
        {
            numberOfRooms = Mathf.RoundToInt((gridSizeX) * (gridSizeY));
        }

        CreateRooms();
        foreach (var item in takenPositions)
        {
            Debug.Log(item);
        }
    }

    void CreateRooms()
    {
        // Create First room
        Vector2 midGraph = Vector2.zero;
        Room curRoom = GameObject.Instantiate(this.roomCopy, this.transform);
        curRoom.transform.position = spawnOrigin.transform.position;
        curRoom.setCoordinate(midGraph);
        roomList.Add(curRoom);

        //add coordinate to list
        takenPositions.Add(midGraph);

        Vector2 checkPos = Vector2.zero;
        string direction;
        int index;
        //float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            //float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            //randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            (checkPos, direction, index) = NewPosition();
            Debug.Log(direction);
            curRoom = roomList[index];

            Room nextRoom = GameObject.Instantiate(this.roomCopy, this.transform);
            nextRoom.transform.position = curRoom.openWallsAndGetPosition(direction);
            nextRoom.setCoordinate(checkPos);
            switch (direction)
            {
                case GraphGameEventNames.DIRECTION_UP:
                    nextRoom.openWallsAndGetPosition(GraphGameEventNames.DIRECTION_DOWN); break;
                case GraphGameEventNames.DIRECTION_DOWN:
                    nextRoom.openWallsAndGetPosition(GraphGameEventNames.DIRECTION_UP); break;
                case GraphGameEventNames.DIRECTION_LEFT:
                    nextRoom.openWallsAndGetPosition(GraphGameEventNames.DIRECTION_RIGHT); break;
                case GraphGameEventNames.DIRECTION_RIGHT:
                    nextRoom.openWallsAndGetPosition(GraphGameEventNames.DIRECTION_LEFT); break;
            }

            roomList.Add(nextRoom);
            takenPositions.Add(checkPos);
            Debug.Log(takenPositions.Count);
        }
    }

    (Vector2, string, int) NewPosition()
    {
        int finalIndex;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        string direction;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
            x = (int)takenPositions[index].x;//capture its x, y position
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
            bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
            if (UpDown)
            { //find the position bnased on the above bools
                if (positive)
                {
                    y += 1;
                    direction = GraphGameEventNames.DIRECTION_UP;
                }
                else
                {
                    y -= 1;
                    direction = GraphGameEventNames.DIRECTION_DOWN;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                    direction = GraphGameEventNames.DIRECTION_RIGHT;
                }
                else
                {
                    x -= 1;
                    direction = GraphGameEventNames.DIRECTION_LEFT;
                }
            }
            checkingPos = new Vector2(x, y);
            finalIndex = index;
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX/2 || x < -gridSizeX / 2 || y >= gridSizeY / 2 || y < -gridSizeY/2); //make sure the position is valid
        return (checkingPos, direction, finalIndex);
    }
}
