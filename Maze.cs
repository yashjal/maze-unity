using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public int size;
    public MazeCell cellPrefab;
    public MazeWall wallPrefab;
    private MazeCell[,] cells;

    public MazeCell GetCell(int x, int z) {
        return cells[x, z];
    }

    public void Generate() {

        // generate maze floor
        cells = new MazeCell[size, size];
        int num = -1;
        for (int x = 0; x < size; x++) {
            for (int z = 0; z < size; z++) {
                CreateCell(new IntVector2(x, z), (++num));
            }
        }

        // generate maze walls
        IntVector3[] edges = RandomPermutation.Generate(size); // all 2n^2 edges in random order
        UnionFind set = new UnionFind(size * size); // disjoint sets of cells
        IntVector3 edge;
        bool addToMaze;
        int direction;
        IntVector2 coord1;
        IntVector2 coord2;
        for (int i = 0; i < edges.Length; i++) {
            edge = edges[i];
            addToMaze = set.Union(edge.x, edge.z); // union-find
            direction = edge.d;
            coord1 = RandomPermutation.CellToCoordinate(edge.x, size);
            coord2 = RandomPermutation.CellToCoordinate(edge.z, size);
            if (addToMaze) {    // add edge/wall to maze
                FillCellWall(coord1, coord2, direction, true);
                CreateWall(coord1,coord2,direction);
            } else {
                FillCellWall(coord1, coord2, direction, false);
            }
        }

    }

    private void CreateCell(IntVector2 coordinates, int num) {

        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.num = num;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size * 0.5f + 0.5f, 0f, coordinates.z - size * 0.5f + 0.5f);
    }

    private void CreateWall(IntVector2 coord1, IntVector2 coord2, int direction) {

        MazeWall newWall = Instantiate(wallPrefab) as MazeWall;
        newWall.name = "Maze Wall " + coord1.x + ", " + coord1.z;
        newWall.transform.parent = transform;
        MazeWall newWall2;

        switch (direction) {
            case 0:
                newWall.transform.localPosition = new Vector3(coord1.x - size * 0.5f + 0.5f, 0.5f, coord1.z - size * 0.5f + 1f);
                break;
            case 1:
                newWall.transform.localPosition = new Vector3(coord1.x - size * 0.5f + 1f, 0.5f, coord1.z - size * 0.5f + 0.5f);
                newWall.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                newWall.transform.localPosition = new Vector3(coord1.x - size * 0.5f + 0.5f, 0.5f, coord1.z - size * 0.5f);
                newWall2 = Instantiate(wallPrefab) as MazeWall;
                newWall2.name = "Maze Wall " + coord2.x + ", " + coord2.z;
                newWall2.transform.parent = transform;
                newWall2.transform.localPosition = new Vector3(coord2.x - size * 0.5f + 0.5f, 0.5f, coord2.z - size * 0.5f + 1f);
                break;
            case 3:
                newWall.transform.localPosition = new Vector3(coord1.x - size * 0.5f, 0.5f, coord1.z - size * 0.5f + 0.5f);
                newWall.transform.rotation = Quaternion.Euler(0, 90, 0);
                newWall2 = Instantiate(wallPrefab) as MazeWall;
                newWall2.name = "Maze Wall " + coord2.x + ", " + coord2.z;
                newWall2.transform.parent = transform;
                newWall2.transform.localPosition = new Vector3(coord2.x - size * 0.5f + 1f, 0.5f, coord2.z - size * 0.5f + 0.5f);
                newWall2.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
        }
    }

    private void FillCellWall(IntVector2 coord1, IntVector2 coord2, int direction, bool isWall) {

        MazeCell cell1 = cells[coord1.x, coord1.z];
        MazeCell cell2 = cells[coord2.x, coord2.z];
        MazeWall wall = new MazeWall(isWall, cell1, cell2);
        cell1.walls[direction] = wall;
        cell2.walls[(direction+2)%4] = wall;
    }

}
