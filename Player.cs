using UnityEngine;

public class Player : MonoBehaviour {

    private MazeCell currentCell;

    public void SetLocation(MazeCell cell) {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

    private void Move(int direction) {
        MazeWall edge = currentCell.walls[direction];
        if (!edge.isWall) {
            if (currentCell.num == edge.cell1.num) {
                SetLocation(edge.cell2);
            } else {
                SetLocation(edge.cell1);
            }
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Move(3);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(0);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Move(1);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(2);
        }
    }
}
