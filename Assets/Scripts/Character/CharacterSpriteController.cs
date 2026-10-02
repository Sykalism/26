using UnityEngine;
using UnityEngine.U2D.Animation;

public class CharacterSpriteController : MonoBehaviour
{
    [System.Serializable]
    public class SpritePart
    {
        public SpriteResolver resolver;
        public string catagory;
    }

    [SerializeField] private SpritePart[] parts;

    public void SetSprite(string label)
    {
        foreach(SpritePart part in parts)
        {
            if (part.resolver == null) continue;

            part.resolver.SetCategoryAndLabel(part.catagory, label);
        }
    }
}
