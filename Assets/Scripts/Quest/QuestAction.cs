using System;

public class QuestAction
{
    public string Message;
    public Action Handler;

    public int Time = 6;


    
    public QuestAction(string message, Action handler)
    {
        Message = message;
        Handler = handler;
    }
}