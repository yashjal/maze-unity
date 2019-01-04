using UnityEngine;

public static class RandomPermutation {

    public static IntVector3[] Generate(int n) {
        IntVector3[] edges = new IntVector3[2*n*n];
        int count = -1;
        for (int i = 0; i < n*n; i++) {
            if (FirstRow(i,n)) {
                if (FirstColumn(i,n)) {
                    edges[(++count)] = new IntVector3(i, i + 1, 0); //right
                    edges[(++count)] = new IntVector3(i, i + n, 1); //bottom
                    edges[(++count)] = new IntVector3(i, i + (n - 1), 2); //left
                    edges[(++count)] = new IntVector3(i, i + n * n - n, 3); //top
                } else if (LastColumn(i,n)) {
                    edges[(++count)] = new IntVector3(i, i + n * n - n, 3); //top
                    edges[(++count)] = new IntVector3(i, i + n, 1); //bottom
                } else {
                    edges[(++count)] = new IntVector3(i, i + 1, 0); //right
                    edges[(++count)] = new IntVector3(i, i + n * n - n, 3); //top
                    edges[(++count)] = new IntVector3(i, i + n, 1); //bottom
                }
            } else if (FirstColumn(i,n)) {
                if (LastRow(i,n)) {
                    edges[(++count)] = new IntVector3(i, i + 1, 0); //right
                    edges[(++count)] = new IntVector3(i, i + (n - 1), 2); //left
                } else {
                    edges[(++count)] = new IntVector3(i, i + n, 1); //bottom
                    edges[(++count)] = new IntVector3(i, i + 1, 0); //right
                    edges[(++count)] = new IntVector3(i, i + (n - 1), 2); //left
                }
            } else if (LastColumn(i,n)) {
                if (!LastRow(i,n)) {
                    edges[(++count)] = new IntVector3(i, i + n, 1); //bottom
                }
            } else if (LastRow(i,n)) {
                edges[(++count)] = new IntVector3(i, i + 1, 0); //right
            } else {
                edges[(++count)] = new IntVector3(i, i + 1, 0); //right
                edges[(++count)] = new IntVector3(i, i + n, 1); //bottom
            }

        }
        return Permute(edges);
    }

    public static bool FirstRow(int i,int n) {
        return (i < n);
    }

    public static bool LastRow(int i, int n) {
        return (i >= (n*n-n));
    }

    public static bool FirstColumn(int i, int n) {
        return (i % n == 0);
    }

    public static bool LastColumn(int i, int n) {
        return (i % n == (n-1));
    }

    public static IntVector3[] Permute(IntVector3[] edges) {
        int n = edges.Length;
        for (int i = 0; i < n; i++) {
            int j = Random.Range(0,n);
            IntVector3 temp = edges[i];
			edges[i] = edges[j];
			edges[j] = temp;
		}
		return edges;
	}

    public static IntVector2 CellToCoordinate(int i, int n) {
        int col = i % n;
        int row = i / n;
        return new IntVector2(row,col);
    }
}
