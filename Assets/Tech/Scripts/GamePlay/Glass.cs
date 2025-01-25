using UnityEngine;

public class Glass : MonoBehaviour
{
    MeshRenderer[] renderers;

    public float FillAmount;

    public bool IsSatisfied => FillAmount > 1;

    Color startColor = Color.white;
    [SerializeField] LiquidUpdated myLiquidUpdated;
    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        startColor = renderers[0].material.color;
    }
    private void Update()
    {
        float fill = Mathf.Lerp(1, 0, FillAmount);
        myLiquidUpdated.SetFill(fill);
    }
    private void OnParticleCollision(GameObject other)
    {
        FillAmount += Time.deltaTime * 0.35f;
    }
}
