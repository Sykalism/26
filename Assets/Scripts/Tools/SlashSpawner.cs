using UnityEngine;

public class SlashSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField]  Transform spawnPoint;
    [SerializeField] Transform parent;
    public int index;

    private int lastSpawnFrame = -1;
    public void Spawn(int i)
    {
        if (lastSpawnFrame == Time.frameCount) return;
        lastSpawnFrame = Time.frameCount;
        Instantiate(prefabs[i], spawnPoint.position, spawnPoint.rotation, parent);
    }
}
