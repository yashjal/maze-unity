using UnityEngine;

public class MazeCell : MonoBehaviour {
    public int num;
    public IntVector2 coordinates;
    public MazeWall[] walls = new MazeWall[4]; 
}
