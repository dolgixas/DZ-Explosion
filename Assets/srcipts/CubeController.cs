using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private float minSize = 0.25f;
    [SerializeField] private float initialSplitChance = 1f;
    [SerializeField] private float splitChanceDecreaseFactor = 0.5f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private float maxOffsetDistance = 1.5f; // ������������ ���������� ��� ������ ����

    private float _splitChance;
    private List<Color> _colors;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _splitChance = initialSplitChance;
        _colors = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };
        SetRandomColor();
    }

    private void OnMouseDown()
    {
        if (CanSplit())
        {
            SplitCube();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool CanSplit()
    {
        return Random.value <= _splitChance;
    }

    private void SplitCube()
    {
        int numCubes = Random.Range(2, 7);
        List<Rigidbody> newCubes = new List<Rigidbody>(); // ������, ����� ������� ����� ����

        for (int i = 0; i < numCubes; i++)
        {
            newCubes.Add(CreateNewCube());
        }

        // ��������� �������� ���� ������ � ����� �����
        foreach (var cube in newCubes)
        {
            ApplyExplosionForceToCube(cube);
        }

        UpdateSplitChance();
        Destroy(gameObject);
    }

    private Rigidbody CreateNewCube()
    {
        Vector3 positionOffset = GenerateRandomOffset();
        GameObject newCubeObj = Instantiate(cubePrefab, transform.position + positionOffset, Quaternion.identity);
        CubeController newCubeController = newCubeObj.GetComponent<CubeController>();

        // ��������� ������ ������ ����
        newCubeController.transform.localScale = transform.localScale / 2;
        newCubeController.SetColor(GetRandomColor());

        // ���������� Rigidbody ������ ����
        return newCubeObj.GetComponent<Rigidbody>();
    }

    private Vector3 GenerateRandomOffset()
    {
        float xOffset = Random.Range(-maxOffsetDistance, maxOffsetDistance);
        float yOffset = Random.Range(-maxOffsetDistance, maxOffsetDistance);
        float zOffset = Random.Range(-maxOffsetDistance, maxOffsetDistance);
        return new Vector3(xOffset, yOffset, zOffset);
    }

    private Color GetRandomColor()
    {
        int randomIndex = Random.Range(0, _colors.Count);
        return _colors[randomIndex];
    }

    private void ApplyExplosionForceToCube(Rigidbody cube)
    {
        if (cube != null)
        {
            // ��������� �������� ���� � ���������� ����
            cube.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }

    private void UpdateSplitChance()
    {
        _splitChance *= splitChanceDecreaseFactor;
    }

    public void CheckSizeAndDestroy()
    {
        if (transform.localScale.x <= minSize)
        {
            Destroy(gameObject);
        }
    }

    private void SetRandomColor()
    {
        SetColor(GetRandomColor());
    }

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}