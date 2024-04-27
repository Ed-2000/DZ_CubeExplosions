using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeRenderer : MonoBehaviour
{
    public void SetRandomColor()
    {
        GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        float red = UnityEngine.Random.value;
        float green = UnityEngine.Random.value;
        float blue = UnityEngine.Random.value;

        Color color = new Color(red, green, blue);

        return color;
    }
}