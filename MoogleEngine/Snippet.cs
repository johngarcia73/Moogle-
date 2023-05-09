namespace MoogleEngine;

public static class Snippet
{
    public static string GetSnippet(int numDoc, string[] words)
    {
        int mostValued=0;
        double globalValue=0;
        string snippet="...";

        //Determina cual es la palabra mas importante de la query en este documento
        for (int i = 0; i < words.Length; i++)
        {
            if(!Vocabulary.documentsMatrix[numDoc].ContainsKey(words[i]))
            {
                continue;
            }
            double wordValue=Vocabulary.documentsMatrix[numDoc][words[i]];

            if(globalValue<wordValue)
            {
                globalValue=wordValue;
                mostValued=i;
            }

        }

        //Devolver el primer trozo donde aparece esa palabra
        string document=Vocabulary.ConvertToString(Vocabulary.documentsArray[numDoc]);
        int index=0;
        try
        {
            index=document.ToLower().IndexOf(words[mostValued]);
        }catch(Exception)
        {
            return "";
        }

        if(index<=0)
        {
            return "";
        }
        if(document.Length-index<50)
        {
            snippet=snippet+document.Substring(index, document.Length-index)+" ";

        }else
        {
            snippet=snippet+document.Substring(index, 50)+" ";
        }
        return snippet;
    }
}