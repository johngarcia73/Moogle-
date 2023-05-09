namespace MoogleEngine;

public static class Corpus
{
    public static List<string> wordsBag=new List<string>();
    public static string path=@"D:\Projects\Moogle!\Content\WordsBag.txt";
    public static string documentsPath=@"D:\Projects\Moogle!\Content\Documents";

    
    
    // Metodo para insertar palabras en el vocabulario.
    
    public static void Exists()
    {
        if(!File.Exists(path))
        {
            File.CreateText(path);
        }
    }
    public static void Insert(string word, List<string> wordsBag)
    {


        if(!wordsBag.Contains(word))
        {
            wordsBag.Add(word);
        }

        TextWriter tw=new StreamWriter(path);
        for(int i=0;i<wordsBag.Count;i++)
        {
            tw.Write(wordsBag[i]+" ");
        }
        tw.Close();
    }

    public static string[] GetDocuments()
    {
        string[] documents=Directory.GetFiles(documentsPath);

        return documents;
    }

    public static List<string> ConvertToWords(string document)
    {   
        string text=File.ReadAllText(documentsPath + document);
        List<string> words=new List<string>();
        bool finish=false;
        for(int i=0;i>=0;i++)
        {
            for(int j=0;j>=0;j++)
            {   
                if(j==text.Length-1)
                {
                    words.Add(text.Substring(i,j));
                    finish=true;
                    break;
                }

                if(text[i]!=' ' && text[j]==' ')
                {
                    string word=text.Substring(i,j);
                    if(word[j-1]=='e' && word[j]=='s')
                    {
                        word=word.Substring(i, j-1);
                    }
                    if(word[j]=='s')
                    {
                        word=word.Substring(i,j=1);
                    }
                    words.Add(text.Substring(i,j));
                    i=j;
                    continue;
                }
            }
            if (finish) break;

        }
        return words;
    }

}