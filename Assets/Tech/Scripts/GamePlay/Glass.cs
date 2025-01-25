using UnityEngine;

public class Glass : MonoBehaviour
{
    MeshRenderer[] renderers;

    public float FillAmount;

    public bool IsSatisfied => FillAmount > 1;

    Color startColor = Color.white;
    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        startColor = renderers[0].material.color;
    }
    private void Update()
    {
        foreach(MeshRenderer r in renderers)
        {
            r.material.color = Color.Lerp(startColor, Color.yellow, FillAmount);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        FillAmount += Time.deltaTime * 0.5f;
    }
}
