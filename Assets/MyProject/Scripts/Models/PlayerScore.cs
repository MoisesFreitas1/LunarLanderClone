public class PlayerScore
{
    public string playerID;
    public float timeFase;

    public string GetPlayerID()
    {
        return playerID;
    }

    public void SetPlayerID(string playerID)
    {
        this.playerID = playerID;
    }

    public float GetTimeFase()
    {
        return timeFase;
    }

    public void SetTimeFase(float timeFase)
    {
        this.timeFase = timeFase;
    }
}
