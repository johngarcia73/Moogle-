namespace MoogleEngine;

public static class Moogle
{
    public static SearchResult Query(string query)
    {   

        Dictionary<string,double>[] matrix=Vocabulary.documentsMatrix;  
        string[] wordsQuery=Vocabulary.ConvertToWords(query);
        Dictionary<string, double> vectorIDF=new Dictionary<string, double>();
        Dictionary<string, double> vectorQuery=VectorQuery(matrix);
        Dictionary<int,double> importancy=DocumentsImportancy(vectorQuery);  //Almacena el indice del documento y su relevancia correspondiente

        SearchItem[] items=Item();
        string suggestion=string.Join(" ",Distance.WordsDistance(wordsQuery, matrix));
        SearchResult result=new SearchResult(items, suggestion);

        ///////// CALCULA EL TF-IDF DE CADA PALABRA DE LA QUERY Y ASIGNA A UN VECTOR QUERY
        Dictionary<string,double> VectorQuery(Dictionary<string,double>[] docMatrix)
        {
            Dictionary<string, double> vectorTf=new Dictionary<string, double>();
            Dictionary<string, double> vectorIdf=new Dictionary<string, double>();
            Dictionary<string, double> vectorTfIdf=new Dictionary<string, double>();

            for (int i = 0; i < wordsQuery.Length; i++)
            {
                double numOfDocsContWord=0;
                string word=wordsQuery[i];
                if(!vectorTf.ContainsKey(word))
                {
                    vectorTf.Add(word,(double)1/(double)wordsQuery.Length);
                }else
                {
                    vectorTf[word]+=(1/(double)wordsQuery.Length);
                }

                for (int j = 0; j < docMatrix.Length; j++)
                {
                    if(docMatrix[j].ContainsKey(word))
                    {
                        numOfDocsContWord++;
                    }
                }
                
                if(!vectorIdf.ContainsKey(word))
                {
                    vectorIdf.Add(word,(Math.Log(docMatrix.Length/numOfDocsContWord)));
                    vectorTfIdf.Add(word, vectorIdf[word]*vectorTf[word]);
                }
                
            }
            vectorIDF=vectorIdf;
            return vectorTfIdf;
        }
        
        //Genera un diccionario con la relevancia de cada documento 
        Dictionary<int, double> DocumentsImportancy(Dictionary<string, double> vector)
        {
            Dictionary<int,double> importancy=new Dictionary<int, double>();
            for (int i = 0; i < matrix.Length; i++)
            {
                double sumImportance=0;
                for (int j = 0; j < wordsQuery.Length; j++)
                {
                    string word=wordsQuery[j];
                    if(matrix[i].ContainsKey(word))
                    {
                        
                        double idfWordVocabulary=vectorIDF[word];
                        matrix[i][word]=Math.Round(matrix[i][word]*idfWordVocabulary,6);
                        sumImportance = sumImportance + (matrix[i][word]*vector[word]);
                    }
                }
                importancy.Add(i,sumImportance);
            }
            return importancy;
        }       

        double Norma(Dictionary<string,double> document)
        {
            double sum=0;
            foreach(var element in document)
            {
                sum+=Math.Pow(element.Value,2);
            }

            double norma = Math.Sqrt(sum);
            return norma;
        } 


        ///ORDENAR LOS SEARCHITEM
        static void Change(SearchItem[] a, int i, int j)
        {
            SearchItem aux=a[j];
            a[j]=a[i];
            a[i]=aux;
        }

        static int Position(SearchItem[] a, int s)
        {
            int p=s;
            for (int i = s+1; i < a.Length; i++)
            {
                if(a[p].Score < a[i].Score)
                {
                    p=i;
                }
            }
            return p;
        }

        static void Sort(SearchItem[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Change(a, i, Position(a,i));
            }
        }

        /// Convirtiendo a SearchItem
        SearchItem[] Item()
        {
            List<SearchItem> items2=new List<SearchItem>();
            for (int i = 0; i < matrix.Length; i++)
            {
                string title=Path.GetFileName(Vocabulary.documentsArray[i]);
                float score=Convert.ToSingle(importancy[i]/(Norma(matrix[i])*Norma(vectorQuery)));
                string snippet="";
                if(wordsQuery.Length==0)
                {
                    score=0;
                }
                if(score!=0 && Norma(matrix[i])!=0)
                {
                    snippet=Snippet.GetSnippet(i,wordsQuery);
                }else
                {
                    continue;
                }

                SearchItem item=new SearchItem(title, snippet, score);
                items2.Add(item);
            }
            SearchItem[] items1=items2.ToArray();
            Sort(items1);
            
            return items1;
        }

        return result;//Devuelve los resultados de la busqueda del usuario, que vienen en query
    }

}