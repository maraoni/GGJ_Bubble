using UnityEngine;

public class Glass : MonoBehaviour
{
    MeshRenderer[] renderers;

    public float FillAmount;

    public bool IsSatisfied => FillAmount > 1;

    Color startColor = Color.white;
    [SerializeField] LiquidUpdated myLiquidUpdated;
    [SerializeField] AudioSource myPourLiquid;

    float KeepLiquidSound = 0;
    const float nextLiquid = 0.15f;
    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        startColor = renderers[0].material.color;
    }

    public void Initialize()
    {
        FillAmount = 0;
    }

    private void Update()
    {
        float fill = Mathf.Lerp(1, 0, FillAmount);
        myLiquidUpdated.SetFill(fill);

        if (KeepLiquidSound > 0)
        {
            KeepLiquidSound -= Time.deltaTime;
        }
        else
        {
            myPourLiquid.Stop();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (!myPourLiquid.isPlaying)
        {
            myPourLiquid.Play();
        }
        KeepLiquidSound = nextLiquid;
        FillAmount += Time.deltaTime * 0.35f;
    }
}
