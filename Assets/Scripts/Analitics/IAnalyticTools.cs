public interface IAnalyticTools
{
    void SendMessage(string nameEvent);
    void SendMessage(string nameEvent, (string, object) data);
}
