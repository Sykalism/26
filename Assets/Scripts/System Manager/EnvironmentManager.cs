using NUnit;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance {get; private set;}

    private EnvironmentObject[] envObjects;
    private int currentDepth;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GetAllEnvObjects();
    }

    private void GetAllEnvObjects()
    {
        envObjects = FindObjectsByType<EnvironmentObject>(FindObjectsSortMode.None);
    }
    private void ToggleDepth()
    {
        foreach(EnvironmentObject envObject in envObjects)
        {
            envObject.ToggleCollision(currentDepth);
        }
    }

    public void IncreaseDepth()
    {
        currentDepth += 1;
        ToggleDepth();
        Debug.Log(currentDepth);
    }
    public void DecreaseDepth()
    {
        currentDepth -= 1;
        ToggleDepth();
        Debug.Log(currentDepth);
    }

}
