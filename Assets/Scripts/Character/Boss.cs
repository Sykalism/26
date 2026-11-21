using UnityEngine;

public class Boss : MonoBehaviour, ISaveable
{
    private CharacterBehaviour character;
    private SaveID saveID;

    private void Awake()
    {
        character = GetComponent<CharacterBehaviour>();
        saveID = GetComponent<SaveID>();
    }
    public void Save(SaveData data)
    {
        BossData bossData = new BossData();

        //set ID
        bossData.ID = saveID.ID;

        //set active
        bossData.isActive = gameObject != null? true : false;

        //set position
        bossData.position = new float[2];
        bossData.position[0] = transform.position.x;
        bossData.position[1] = transform.position.y;

        //set current health
        bossData.currentHealth = character.stats.health.current;

        //apply data
        data.bosses.Add(bossData);
    }
    public void Load(SaveData data)
    {
        BossData bossData = data.bosses.Find(x => x.ID == saveID.ID);

        if(bossData == null) return;

        gameObject.SetActive(bossData.isActive);

        transform.position = new Vector3(
            bossData.position[0],
            bossData.position[1],
            0f
        );

        character.stats.health.SetCurrent(bossData.currentHealth);
    }
}
