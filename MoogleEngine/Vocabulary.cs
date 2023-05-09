namespace MoogleEngine;

public class Vocabulary
{
    public static string documentsPath=@"..\\Content";
    public static string[] documentsArray=Directory.GetFiles(documentsPath, "*.txt");
    //public static List<string> wordsBag{get;set;}=new List<string>();
    public static Dictionary<string,double>[] documentsMatrix=GenerateMatrix(documentsArray);
    
    
    // Convierte el contenido de un documento en un string
    public static string ConvertToString(string document)
    {
        string text=File.ReadAllText(document);
        return text;
    }

    // Convierte un string en array de string
    public static string[] ConvertToWords(string document)
    {
        document=document.ToLower();
        char[] specialsChar={' ', ',', '.', ';', ':', '/', '?', '<', '>', '!', '|', '{', '}', '(', ')', '*','`', '~','[',']','"','^','&','#'};

        string[] words=document.Split(specialsChar, StringSplitOptions.RemoveEmptyEntries);

        return words;
    }

    //Genera la matriz de documentos
    public static Dictionary<string,double>[] GenerateMatrix(string[] docArray)
    {
        Dictionary<string,double>[] matrix=new Dictionary<string, double>[docArray.Length];

        for (int i = 0; i < matrix.Length; i++)
        {
            string[] words=ConvertToWords(ConvertToString(docArray[i]));
            double cantWords=words.Length;
            Dictionary<string,double> aux=new Dictionary<string, double>();

            
            for (int j = 0; j < words.Length; j++)
            {   

                string word=words[j];
                if(aux.ContainsKey(word))
                {
                    aux[word]+=1/cantWords;

                    continue;

                }else
                {
                    aux.Add(word,1/cantWords);
                    
                }


            }
            matrix[i]=aux;
        }
        

        return matrix;
    }

}                    