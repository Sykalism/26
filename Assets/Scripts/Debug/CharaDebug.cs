using UnityEngine;

public class CharaDebug : MonoBehaviour
{
    public class Stats
    {
        public Endurance endurance;
        public Stats()
        {
            endurance = new Endurance(100);
        }

    }
    private Stats stats;

    void Start()
    {
        stats = new Stats();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            stats.endurance.Remove(50);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            stats.endurance.Add(50);
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            Debug.Log(stats.endurance.max);
        }
        else if (Input.GetKey(KeyCode.LeftControl)) Debug.Log(stats.endurance.current);

    }
}


