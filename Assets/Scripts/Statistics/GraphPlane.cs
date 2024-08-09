using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GraphData
{
    public GraphData(double fValue, double vValue, float averageCountOfParts)
    {
        this.fValue = fValue;
        this.vValue = vValue;
        this.averageCountOfParts = averageCountOfParts;
    }

    public double fValue;
    public double vValue;
    public float averageCountOfParts;
}

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GraphPlane : MonoBehaviour
{
    [SerializeField] private GameObject _plane; // Already existing plane
    [SerializeField] private float _planeSize; // Desired size of the plane
    [SerializeField] private GameObject _cylinderPrefab; // Prefab of the cylinder
    [SerializeField] private float maxColumnHeight; // Maximum possible height of the columns
    [SerializeField] private Material xAxisMaterial; // Material for the X axis
    [SerializeField] private Material yAxisMaterial; // Material for the Y axis
    [SerializeField] private Material zAxisMaterial; // Material for the Z axis

    private Vector3 realPlaneSize;

    private void Start()
    {
        // Get the real size of the plane
        realPlaneSize = GetRealPlaneSize();

        // Scale the plane to the desired size
        ScalePlane();
    }

    public void Generate(List<GraphData> data)
    {
        if (data == null || data.Count == 0)
        {
            Debug.LogError("Data list cannot be null or empty");
            return;
        }

        // Find the maximum fValue and vValue to determine the scaling factor
        double maxFValue = data.Max(d => d.fValue);
        double maxVValue = data.Max(d => d.vValue);

        // Calculate scaling factors
        float scaleFactorX = realPlaneSize.x / (float)maxFValue;
        float scaleFactorZ = realPlaneSize.z / (float)maxVValue;

        // Calculate the offset to center the grid on the plane
        float offsetX = realPlaneSize.x / 2;
        float offsetZ = realPlaneSize.z / 2;

        // Find the maximum averageCountOfParts to scale the column heights
        float maxAverageCountOfParts = data.Max(d => d.averageCountOfParts);

        foreach (var graphData in data)
        {
            // Calculate the position on the plane
            float x = (float)(graphData.fValue * scaleFactorX) - offsetX;
            float z = (float)(graphData.vValue * scaleFactorZ) - offsetZ;

            // Calculate the height of the cylinder based on the percentage of maxAverageCountOfParts
            float height = (graphData.averageCountOfParts / maxAverageCountOfParts) * maxColumnHeight;

            // Create the cylinder
            GameObject cylinder = Instantiate(_cylinderPrefab);
            cylinder.transform.position = new Vector3(x, 0, z);
            cylinder.transform.localScale = new Vector3(1, height, 1);
            cylinder.transform.SetParent(_plane.transform, true);
        }

        var ZeroPoint = new Vector3(-offsetX, 0, -offsetZ);
        // Draw the coordinate axes with their respective materials
        CreateAxis(ZeroPoint, new Vector3(offsetX, 0, -offsetZ), xAxisMaterial, "X Axis");
        CreateAxis(ZeroPoint, new Vector3(-offsetX, maxColumnHeight * 2f, -offsetZ), yAxisMaterial, "Y Axis");
        CreateAxis(ZeroPoint, new Vector3(-offsetX, 0, offsetZ), zAxisMaterial, "Z Axis");
    }

    private void CreateAxis(Vector3 start, Vector3 end, Material material, string name)
    {
        Vector3 direction = end - start;
        float length = direction.magnitude;

        GameObject axisCylinder = Instantiate(_cylinderPrefab);
        axisCylinder.name = name;
        axisCylinder.transform.SetParent(_plane.transform, true);
        axisCylinder.transform.position = start;
        axisCylinder.transform.localScale = new Vector3(0.1f, length / 2, 0.1f);
        axisCylinder.transform.up = direction.normalized;

        // Apply the material to the cylinder
        MeshRenderer renderer = axisCylinder.GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    private Vector3 GetRealPlaneSize()
    {
        MeshFilter meshFilter = _plane.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.sharedMesh;
            Vector3 size = mesh.bounds.size;
            Vector3 scale = _plane.transform.localScale;
            return new Vector3(size.x * scale.x, size.y * scale.y, size.z * scale.z);
        }
        return Vector3.zero;
    }

    private void ScalePlane()
    {
        if (_plane == null)
        {
            Debug.LogError("Plane object is not assigned");
            return;
        }

        // Assuming the plane is a unit plane (1x1 size), scale it to the desired size
        _plane.transform.localScale = new Vector3(_planeSize, 1, _planeSize);
    }
}