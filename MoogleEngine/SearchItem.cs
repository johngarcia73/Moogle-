namespace MoogleEngine;

public class SearchItem
{
    public SearchItem(string title, string snippet, float score){

        this.Title=title;
        this.Snippet=snippet;
        this.Score=score;

        //Cada uno de estos objetos representa a un documento que coincide con la consulta en query
    }

    public string Title{get; private set;}
    public string Snippet{get; private set;}
    public float Score{get; private set;}

    
}