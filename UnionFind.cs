public class UnionFind {

    private int[] I, S;                 // INTERNAL STORAGE FOR SUBSET INDICES AND SIZES.

    public UnionFind(int n) {           // INIT EACH SUBSET INDEX TO ITSELF.
        I = new int[n];                 // INIT EACH SUBSET SIZE TO ONE.
        S = new int[n];
        for (int i = 0; i < n; i++) { I[i] = i; S[i] = 1; }
    }

    public bool Union(int a, int b) {   // IF NOT ALREADY IN SAME SUBSET,
        int i = Find(a), j = Find(b);   // MERGE SMALL SUBSET INTO BIG ONE.
        if (i == j) { return true; }
        int u = S[i] < S[j] ? i : j, v = i + j - u;
        I[u] = v;
        S[v] += S[u];
        return false;
    }

    public int Find(int a) {            // FIND ROOT INDEX r OF SUBSET;
        int r = a;                      // SET ALL INDICES OF SUBSET TO r.
        while (I[r] != r) { r = I[r]; }
        while (I[a] != r) { int t = I[a]; I[a] = r; a = t; }
        return r;
    }

    public bool OnlyOneSubset() {
        int x = I[0];
        for (int i = 1; i < I.Length; i++) {
            if (I[i] != x) {
                return false;
            }
        }
        return true;
    }
}
