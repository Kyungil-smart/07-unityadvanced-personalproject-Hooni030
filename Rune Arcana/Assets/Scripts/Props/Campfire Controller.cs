using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CampfireController : MonoBehaviour
{
    public Light2D _light;
    [SerializeField] private float _radius;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        
    }

    private void FixedUpdate()
    {
        _light.pointLightInnerRadius = Random.Range(0f, 0.3f);
    }
}
