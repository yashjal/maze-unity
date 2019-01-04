using UnityEngine;

public class MazeWall : MonoBehaviour {

    public bool isWall = false;
    public MazeCell cell1, cell2;

    public MazeWall(bool isWall, MazeCell cell1, MazeCell cell2) {
        this.isWall = isWall;
        this.cell1 = cell1;
        this.cell2 = cell2;
    }
}
