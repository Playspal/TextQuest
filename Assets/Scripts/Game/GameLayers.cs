using UnityEngine;

public class GameLayers
{
    public static Transform GetRoot()
    {
        return GameObject.Find("GameContainer").transform;
    }

    public static Transform GetLayerCharacters()
    {
        return GameObject.Find("GameContainer/LayerCharacters").transform;
    }
}
