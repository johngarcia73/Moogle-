namespace MoogleEngine;

public static class Distance
{
    public static double LevenshteinDistance(string s, string t)
    {
        double percent=0;

        int costo=0;
        int m=s.Length;
        int n=t.Length;
        int[,] d=new int[m+1, n+1];

        //Verifica que exista algo que comparar
        if(n==0) return m;
        if(m==0) return n;

        //LLena la columna 1 y fila 1
        for(int i=0; i<=m; d[i,0]=i++);
        for(int j=0; j<=n; d[0,j]=j++);

        //Recorre la matriz llenando cada uno de los pesos, si no el peso suma a uno

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                costo=(s[i-1] == t[j-1]) ? 0:1;
                d[i,j]=Math.Min(Math.Min(d[i-1,j]+1, d[i,j-1]+1), d[i-1, j-1]+costo);
            }
        }

        //Se calcula el porcentaje

        if(s.Length>t.Length)
        {
            percent=((double)d[m,n] / (double)s.Length);
        }
        else
        {
            percent=((double)d[m,n] / (double)t.Length);
        }
            
        return percent;
        
    }

    //Retorna Las palabras con mayor parecido a las buscadas
    public static string[] WordsDistance(string[] query, Dictionary<string,double>[] matrix)
    {
        List<string> suggestion=new List<string>();

        for (int i = 0; i < query.Length; i++)
        {
            double levenshtein=1;
            string word="";
            for (int j = 0; j < matrix.Length; j++)
            {
                foreach(var element in matrix[j])
                {
                    double percent=LevenshteinDistance(query[i], element.Key);
                    if(levenshtein>percent)
                    {
                        levenshtein=percent;
                        word=element.Key;
                    }
                }
                
            }
            suggestion.Add(word);
        }
        return suggestion.ToArray();        
        
    }
}