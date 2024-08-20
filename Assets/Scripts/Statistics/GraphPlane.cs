using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TriangleNet.Geometry;
using TriangleNet;

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

public class GraphPlane : MonoBehaviour
{
    [SerializeField] private Material planeMaterial;
    [SerializeField] private float _planeSize = 20f; // ������ ���������   

    [SerializeField] private float maxColumnHeight = 100f; // ������������ ������ �������

    [SerializeField] private GameObject _cylinderPrefab;
    [SerializeField] private float _axisScaleFactor = 0.1f;

    [SerializeField] private Material xAxisMaterial;
    [SerializeField] private Material yAxisMaterial;
    [SerializeField] private Material zAxisMaterial;

    [SerializeField] private float tickDiameter = 0.2f;
    [SerializeField] private GameObject _labelPrefab;
    [SerializeField] private GameObject _labelSpherePrefab;

    public void Generate(List<GraphData> data)
    {
        if (data == null || data.Count == 0)
        {
            Debug.LogError("Data list cannot be null or empty");
            return;
        }       

        // ����� ������������ �������� averageCountOfParts ��� ��������������� �����
        float maxAverageCountOfParts = data.Max(d => d.averageCountOfParts);

        // ����� ������������ �������� fValue � vValue ��� ������������
        double maxFValue = data.Max(d => d.fValue);
        double maxVValue = data.Max(d => d.vValue);

        List<Vector3> vertices = new List<Vector3>();

        // ����������� ������ ����� ������
        foreach (var graphData in data)
        {
            // ����������� ���������� fValue � vValue � �������� [0, 1]
            float normalizedX = (float)(graphData.fValue / maxFValue);
            float normalizedZ = (float)(graphData.vValue / maxVValue);

            // ����������� ��������������� ���������� � ���������� Terrain
            float xCoord = normalizedX * _planeSize;
            float zCoord = normalizedZ * _planeSize;

            // ���������� ������ ����� ��� ������ ����� ��� ������� �� ������������ ������
            float height = (graphData.averageCountOfParts / maxAverageCountOfParts) * maxColumnHeight;

            vertices.Add(new Vector3(xCoord, height, zCoord));
        }
        vertices = vertices.OrderBy(v => v.z).ThenBy(v => v.x).ToList();

        var planeMeshSize = CreatePlaneFromVertices(vertices).bounds.size;

        foreach (var pos in vertices)
        {
            Instantiate(_labelSpherePrefab, pos, Quaternion.identity, this.transform);
        }
        // ������ ���
        DrawAxes(new Vector3(planeMeshSize.x, maxColumnHeight, planeMeshSize.z));
    }
    private Mesh CreatePlaneFromVertices(List<Vector3> vertices)
    {
        if (vertices.Count < 3)
        {
            Debug.LogError("������������ ����� ��� �������� ��������!");
            return null;
        }        

        // ����������� List<Vector3> � List<Vector2> ��� Delaunay Triangulation
        List<Vector2> points2D = vertices.Select(v => new Vector2(v.x, v.z)).ToList();

        Polygon poly = new Polygon();
        foreach (var point in points2D)
        {
            poly.Add(point);
        }

        var triangleNetMesh = (TriangleNetMesh)poly.Triangulate();

        // ��������� ��� � MeshFilter � MeshRenderer
        GameObject planeObject = new GameObject("GeneratedPlane");       
        MeshFilter meshFilter = planeObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = planeObject.AddComponent<MeshRenderer>();
        
        var mesh = triangleNetMesh.GenerateUnityMesh();

        var temporaryVertices = mesh.vertices;

        for (int i = 0; i < temporaryVertices.Length; i++)
        {
            Debug.Log(mesh.vertices[i]);
            temporaryVertices[i] = vertices[i];
            Debug.Log(mesh.vertices[i]);
        }
        mesh.vertices = temporaryVertices;

        meshFilter.mesh = mesh;
        meshRenderer.material = planeMaterial;
            
        planeObject.transform.SetParent(this.transform, false);

        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();

        return mesh;
    }

    private void CorrectNormals(Mesh mesh)
    {
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.up; // ������������� ������� � ����������� �����
        }
        mesh.normals = normals;
    }


    private void DrawAxes(Vector3 size)
    {
        // ��������� ������������� ���� ����� �� ���������
        if (!GameObject.Find("XAxis"))
        {
            CreateAxis(
                new Vector3(0, 0, 0),
                new Vector3(size.x, 0, 0),
                xAxisMaterial,
                "XAxis"
            );
        }

        if (!GameObject.Find("ZAxisLeft"))
        {
            CreateAxis(
                new Vector3(0, 0, 0),
                new Vector3(0, 0, size.z),
                zAxisMaterial,
                "ZAxisLeft"
            );
        }

        if (!GameObject.Find("YAxisLeft"))
        {
            CreateAxis(
                new Vector3(0, 0, 0),
                new Vector3(0, size.y, 0),
                yAxisMaterial,
                "YAxisLeft"
            );
        }

        // ��������� ������� �� ���
        AddAxisLabel(new Vector3(size.x, 0, 0), "X", xAxisMaterial.color);
        AddAxisLabel(new Vector3(0, size.y, 0), "Y", yAxisMaterial.color);
        AddAxisLabel(new Vector3(0, 0, size.z), "Z", zAxisMaterial.color);

        // ��������� ����������� �� ���
        AddAxisTicks(new Vector3(size.x, 0, 0), xAxisMaterial.color, 'X');
        AddAxisTicks(new Vector3(0, size.y, 0), yAxisMaterial.color, 'Y');
        AddAxisTicks(new Vector3(0, 0, size.z), zAxisMaterial.color, 'Z');
    }

    private void AddAxisTicks(Vector3 axisEnd, Color color, char axis)
    {
        // ���������� ��� ����������� � ����������� �� ����� ���
        float axisLength = axisEnd.magnitude;
        float step = axisLength > 10 ? 5f : axisLength > 1 ? 1f : 0.05f;
        int numberOfTicks = Mathf.CeilToInt(axisLength / step);

        for (int i = 0; i <= numberOfTicks; i++)
        {
            float position = i * step;
            Vector3 tickPosition = Vector3.zero;
            Vector3 tickRotation= Vector3.zero;

            if (axis == 'X')
            {
                tickRotation = new Vector3(0,0,90);
                tickPosition = new Vector3(position, 0, 0);
            }
            else if (axis == 'Y')
            {
                tickRotation = new Vector3(0, 90, 0);
                tickPosition = new Vector3(0, position, 0);
            }
            else if (axis == 'Z')
            {
                tickRotation = new Vector3(90,0, 0);
                tickPosition = new Vector3(0, 0, position);
            }

            // �������� �����������
            GameObject tick = Instantiate(_cylinderPrefab, tickPosition, Quaternion.identity, this.transform);
            tick.transform.localScale = new Vector3(tickDiameter + _axisScaleFactor, 0.02f, tickDiameter + _axisScaleFactor); // ������ ����������� ������ �������� ���
            tick.transform.localEulerAngles = tickRotation;
            MeshRenderer tickRenderer = tick.GetComponentInChildren<MeshRenderer>();
            if (tickRenderer != null)
            {
                tickRenderer.material.color = color;
            }

            // �������� ��������� �����
            Vector3 labelPosition = tickPosition + new Vector3(0, 0.1f, 0); // ������� ����� ���� �����������
            AddAxisLabel(labelPosition, position.ToString("0.0"), color);
        }
    }

    private void AddAxisLabel(Vector3 position, string text, Color color)
    {
        GameObject labelObject = Instantiate(_labelPrefab, position, Quaternion.identity, this.transform);
        TextMesh textMesh = labelObject.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = text;
            textMesh.color = color;
            textMesh.alignment = TextAlignment.Center;
            textMesh.anchor = TextAnchor.MiddleCenter;
        }
    }
    private void CreateAxis(Vector3 start, Vector3 end, Material material, string name)
    {
        Vector3 direction = end - start;
        float length = Vector3.Distance(start, end);

        GameObject axisCylinder = Instantiate(_cylinderPrefab);
        axisCylinder.name = name;
        axisCylinder.transform.SetParent(this.transform, true);
        axisCylinder.transform.position = start;
        axisCylinder.transform.localScale = new Vector3(_axisScaleFactor, length/2, _axisScaleFactor); // ������ ��� � ������ ���
        axisCylinder.transform.up = direction.normalized;

        // ��������� �������� � ��������
        MeshRenderer renderer = axisCylinder.GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }
}