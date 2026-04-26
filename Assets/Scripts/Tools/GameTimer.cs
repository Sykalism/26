public class GameTimer
{
    private float duration;
    private float timeRemaining;

    public bool IsRunning { get; private set; }
    public bool IsFinished => IsRunning && timeRemaining <= 0f;

    public GameTimer(float duration)
    {
        this.duration = duration;
    }

    public void Start()
    {
        timeRemaining = duration;
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Tick(float deltaTime)
    {
        if (!IsRunning) return;

        timeRemaining -= deltaTime;

        if (timeRemaining <= 0f)
        {
            IsRunning = false;
        }
    }
}
