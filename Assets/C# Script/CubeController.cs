using UnityEngine;

public class BaseCube : MonoBehaviour
{
    [SerializeField]
    protected float splitChance = 1.0f;

    [SerializeField]
    protected float explosionForce = 5.0f;

    [SerializeField]
    protected float explosionRadius = 5.0f;

    public float SplitChance
    {
        get { return splitChance; }
        set { splitChance = value; }
    }

    public float ExplosionForce
    {
        get { return explosionForce; }
        set { explosionForce = value; }
    }

    public float ExplosionRadius
    {
        get { return explosionRadius; }
        set { explosionRadius = value; }
    }

    public virtual void OnMouseDown()
    {
        ChangeColor(Color.red);
        LogCubeClicked();
    }

    protected void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

    protected void LogCubeClicked()
    {
        Debug.Log(" уб был нажат!");
    }
}

public class CubeController : BaseCube
{
    public override void OnMouseDown()
    {
        base.OnMouseDown();

        if (ShouldSplit())
        {
            SplitCube();
            AdjustSplitChance();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool ShouldSplit()
    {
        return Random.value <= splitChance;
    }

    private void SplitCube()
    {
        int numberOfCubes = Random.Range(2, 7);
        Vector3 position = transform.position;

        for (int i = 0; i < numberOfCubes; i++)
        {
            CreateNewCube(position);
        }

        Destroy(gameObject);
    }

    private void CreateNewCube(Vector3 position)
    {
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newCube.transform.localScale = transform.localScale / 2;
        newCube.transform.position = position;
        newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();

        Rigidbody rb = newCube.AddComponent<Rigidbody>();
        Vector3 explosionDirection = Random.onUnitSphere;
        rb.AddExplosionForce(explosionForce, position, explosionRadius);
    }

    private void AdjustSplitChance()
    {
        splitChance /= 2;
    }
}
