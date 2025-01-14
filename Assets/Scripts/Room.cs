using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject roomLight;
    [SerializeField] private GameObject ballSpawnPoint;
    [SerializeField] private GameObject pointOrb;

    [SerializeField] private Wall upWall;
    [SerializeField] private Wall downWall;
    [SerializeField] private Wall rightWall;
    [SerializeField] private Wall leftWall;

    private Vector2 coordinate = new Vector2();

    [SerializeField] private Room upNeighbor;
    [SerializeField] private Room rightNeighbor;
    [SerializeField] private Room leftNeighbor;
    [SerializeField] private Room downNeighbor;

    // Start is called before the first frame update
    void Start()
    {
        //roomLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getBallSpawnPoint()
    {
        return ballSpawnPoint.transform.position;
    }

    public Vector3 getNeighborSpawnPosition(string direction)
    {
        switch (direction)
        {
            case GraphGameEventNames.DIRECTION_UP:
                return upWall.getNextRoomPosition();
            case GraphGameEventNames.DIRECTION_DOWN:
                downWall.openWall();
                return downWall.getNextRoomPosition();
            case GraphGameEventNames.DIRECTION_LEFT:
                leftWall.openWall();
                return leftWall.getNextRoomPosition();
            case GraphGameEventNames.DIRECTION_RIGHT:
                rightWall.openWall();
                return rightWall.getNextRoomPosition();
            default: return Vector3.zero;
        }
    }

    public void lightOn()
    {
        this.roomLight.SetActive(true);
    }

    public void lightOff()
    {
        roomLight.SetActive(false);
    }

    public void activateOrb()
    {
        pointOrb.SetActive(true);
    }
    public void deactivateOrb()
    {
        pointOrb.SetActive(false);
    }

    public void setCoordinate(Vector2 newCoor)
    {
        coordinate = newCoor;
    }
    public Vector2 getCoordinate()
    {
        return coordinate;
    }

    public void setNeighborAndOpenWalls(Room neighbor, string localDirection)
    {
        switch (localDirection)
        {
            case GraphGameEventNames.DIRECTION_UP:
                upWall.openWall(); 
                upNeighbor = neighbor; break;
            case GraphGameEventNames.DIRECTION_DOWN:
                downWall.openWall(); 
                downNeighbor = neighbor; break;
            case GraphGameEventNames.DIRECTION_LEFT:
                leftWall.openWall(); 
                leftNeighbor = neighbor; break;
            case GraphGameEventNames.DIRECTION_RIGHT:
                rightWall.openWall(); 
                rightNeighbor = neighbor; break;
        }
    }
    public Room getNeighbor(string localDirection)
    {
        switch (localDirection)
        {
            case GraphGameEventNames.DIRECTION_UP: return upNeighbor; 
            case GraphGameEventNames.DIRECTION_DOWN: return downNeighbor; 
            case GraphGameEventNames.DIRECTION_LEFT: return leftNeighbor; 
            case GraphGameEventNames.DIRECTION_RIGHT: return rightNeighbor; 
            default: return null;
        }
    }

    public List<Room> getNeighbors()
    {
        List<Room> neighbors = new List<Room>();
        if (upNeighbor != null)
            neighbors.Add(upNeighbor);
        if (downNeighbor != null)
            neighbors.Add(downNeighbor);
        if (leftNeighbor != null)
            neighbors.Add(leftNeighbor);
        if (rightNeighbor != null)
            neighbors.Add(rightNeighbor);
        return neighbors;
    }
}
