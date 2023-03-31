using UnityEngine;
public enum Character
{
    Thoughts,
    Vakako,
    Liana,
    Jonny,
    Pepe,
    Courier,
    Smowsher,
    Victor,
    Meredit
}

[CreateAssetMenu(menuName = "PageData ")]

public class PageData : ScriptableObject
{
    public string textboxText;
    public string titleText;
    public AudioClip voice;
    public Sprite backgroundImage;
    public int[] nextPage;
    public string[] buttonText;
    public Character character;
    public bool isCheckPoint;
}
