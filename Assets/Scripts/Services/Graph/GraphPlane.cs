using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TriangleNet.Geometry;
using TriangleNet;

public class GraphPlane : MonoBehaviour
{
    [SerializeField] private Transform _graphObject;

    [SerializeField] private Material _planeMaterial;
    [SerializeField] private int _planeSize = 20;

    [SerializeField] private int _maxColumnHeight = 20;

    [SerializeField] private GameObject _labelSpherePrefab;

    [SerializeField] private AxisGenerator _axisGenerator;

    // Поля для хранения данных
    private List<Vector3> _vertices = new List<Vector3>();
    private List<Vector2> _points2D = new List<Vector2>();

    public void Generate(List<GraphData> data)
    {
        Clear();

        if (data == null || data.Count == 0)
        {
            Debug.LogError("Data list cannot be null or empty");
            return;
        }

        float maxAverageCountOfParts = data.Max(d => d.averageCountOfParts);
        float minAverageCountOfParts = data.Min(d => d.averageCountOfParts);
        var graphMinHeight = (minAverageCountOfParts / maxAverageCountOfParts) * _maxColumnHeight;

        double maxFValue = data.Max(d => d.fValue);
        double maxVValue = data.Max(d => d.vValue);

        foreach (var graphData in data)
        {
            float normalizedX = (float)(graphData.fValue / maxFValue);
            float normalizedZ = (float)(graphData.vValue / maxVValue);

            float xCoord = normalizedX * _planeSize;
            float zCoord = normalizedZ * _planeSize;

            float height = (graphData.averageCountOfParts / maxAverageCountOfParts) * _maxColumnHeight;

            _vertices.Add(new Vector3(xCoord, height, zCoord));
        }

        _vertices = _vertices.OrderBy(v => v.z).ThenBy(v => v.x).ToList();

        CreatePlaneFromVertices(_vertices, graphMinHeight);

        foreach (var pos in _vertices)
        {
            Instantiate(_labelSpherePrefab, pos, Quaternion.identity, _graphObject);
        }

        var maxValue = new Vector3((float)maxFValue, maxAverageCountOfParts, (float)maxVValue);
        var axisSize = new Vector3(_planeSize, _maxColumnHeight, _planeSize);

        _axisGenerator.DrawAxes(axisSize, _graphObject, maxValue);

        _graphObject.localPosition = Vector3.zero - new Vector3(_planeSize / 2, 0, _planeSize / 2);
    }

    private void CreatePlaneFromVertices(List<Vector3> vertices, float graphMinHeight)
    {
        if (vertices.Count < 3)
        {
            Debug.LogError("Недостаточно точек для создания полигона!");
            return;
        }
               
        _points2D.Capacity = vertices.Count;

        // Преобразуем List<Vector3> в List<Vector2> для Delaunay Triangulation
        foreach (var v in vertices)
        {
            _points2D.Add(new Vector2(v.x, v.z));
        }

        Polygon poly = new Polygon();
        foreach (var point in _points2D)
        {
            poly.Add(point);
        }

        var triangleNetMesh = (TriangleNetMesh)poly.Triangulate();

        // Применяем меш к MeshFilter и MeshRenderer
        GameObject planeObject = new GameObject("GeneratedPlane");
        MeshFilter meshFilter = planeObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = planeObject.AddComponent<MeshRenderer>();

        var mesh = triangleNetMesh.GenerateUnityMesh();

        var temporaryVertices = mesh.vertices;

        for (int i = 0; i < temporaryVertices.Length; i++)
        {
            temporaryVertices[i] = vertices[i];
        }
        mesh.vertices = temporaryVertices;

        meshFilter.mesh = mesh;
        meshRenderer.material = _planeMaterial;

        var propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetFloat("_Height", graphMinHeight / 2);
        meshRenderer.SetPropertyBlock(propertyBlock);

        planeObject.transform.SetParent(_graphObject, false);

        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();
    }

    public void Clear()
    {
        foreach (Transform child in _graphObject)
        {
            Destroy(child.gameObject);
        }
        _vertices.Clear();
        _points2D.Clear();
        _graphObject.position = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}

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
