using UnityEngine;

[System.Serializable]
public class AxisGenerator
{
    [SerializeField] private Material _xAxisMaterial;
    [SerializeField] private Material _yAxisMaterial;
    [SerializeField] private Material _zAxisMaterial;

    [SerializeField] private GameObject _cylinderPrefab;
    [SerializeField] private float _axisDiameter;
    [SerializeField] private float _tickDiameter = 0.08f;
    [SerializeField] private int _tickSteps = 1;
    [SerializeField] private Font _lableFont;
    [SerializeField] private float _lableOffset;

    private Transform _parentTransform;
    private Vector3 _maxValue;

    public void DrawAxes(Vector3 size, Transform parentTransform, Vector3 maxValue)
    {
        _parentTransform = parentTransform;
        _maxValue = maxValue;

        if (!GameObject.Find("XAxis"))
        {
            CreateAxis(new Vector3(0, 0, 0), new Vector3(size.x, 0, 0), _xAxisMaterial, "XAxis");
        }

        if (!GameObject.Find("ZAxisLeft"))
        {
            CreateAxis(new Vector3(0, 0, 0), new Vector3(0, 0, size.z), _zAxisMaterial, "ZAxisLeft");
        }

        if (!GameObject.Find("YAxisLeft"))
        {
            CreateAxis(new Vector3(0, 0, 0), new Vector3(0, size.y, 0), _yAxisMaterial, "YAxisLeft");
        }

        AddAxisLabel(new Vector3(size.x, 0, 0), "X(F)", _xAxisMaterial, new Vector3(_lableOffset, 0, 0));
        AddAxisLabel(new Vector3(0, size.y, 0), "Y(Count)", _yAxisMaterial, new Vector3(0, _lableOffset, 0));
        AddAxisLabel(new Vector3(0, 0, size.z), "Z(V)", _zAxisMaterial, new Vector3(0, 0, _lableOffset));

        AddAxisTicks(new Vector3(size.x, 0, 0), _xAxisMaterial, 'X');
        AddAxisTicks(new Vector3(0, size.y, 0), _yAxisMaterial, 'Y');
        AddAxisTicks(new Vector3(0, 0, size.z), _zAxisMaterial, 'Z');
    }

    private void CreateAxis(Vector3 start, Vector3 end, Material material, string name)
    {
        Vector3 direction = end - start;
        float length = Vector3.Distance(start, end);

        GameObject axisCylinder = GameObject.Instantiate(_cylinderPrefab);
        axisCylinder.name = name;
        axisCylinder.transform.SetParent(_parentTransform, true);
        axisCylinder.transform.position = start;
        axisCylinder.transform.localScale = new Vector3(_axisDiameter, length / 2, _axisDiameter);
        axisCylinder.transform.up = direction.normalized;

        MeshRenderer renderer = axisCylinder.GetComponentInChildren<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    private void AddAxisTicks(Vector3 axisEnd, Material material, char axis)
    {
        float axisLength = axisEnd.magnitude;
        int numberOfTicks = Mathf.CeilToInt(axisLength / _tickSteps);
        float maxValue = 0;

        switch (axis)
        {
            case 'X':
                maxValue = _maxValue.x;
                break;
            case 'Y':
                maxValue = _maxValue.y;
                break;
            case 'Z':
                maxValue = _maxValue.z;
                break;
        }

        for (int i = 0; i <= numberOfTicks; i++)
        {
            float position = i * _tickSteps;
            float tickValue = Mathf.Lerp(0, maxValue, (float)i / numberOfTicks);

            Vector3 tickPosition = Vector3.zero;
            Vector3 tickRotation = Vector3.zero;
            Vector3 textOffset = Vector3.zero;

            bool itsY = false;
            switch (axis)
            {
                case 'X':
                    tickRotation = new Vector3(0, 0, 90);
                    tickPosition = new Vector3(position, 0, 0);
                    textOffset = new Vector3(0, _lableOffset, 0);
                    break;
                case 'Y':
                    tickRotation = new Vector3(0, 90, 0);
                    tickPosition = new Vector3(0, position, 0);
                    textOffset = new Vector3(_lableOffset, 0, 0);
                    itsY = true;
                    break;
                case 'Z':
                    tickRotation = new Vector3(90, 0, 0);
                    tickPosition = new Vector3(0, 0, position);
                    textOffset = new Vector3(0, _lableOffset, 0);                    
                    break;
            }

            GameObject tick = Object.Instantiate(_cylinderPrefab, tickPosition, Quaternion.identity, _parentTransform);
            tick.transform.localScale = new Vector3(_tickDiameter + _axisDiameter, 0.02f, _tickDiameter + _axisDiameter);
            tick.transform.localEulerAngles = tickRotation;
            MeshRenderer tickRenderer = tick.GetComponentInChildren<MeshRenderer>();
            tickRenderer.material = material;

            Vector3 labelPosition = tickPosition + new Vector3(0, 0.1f, 0);
            if (itsY == true)
                AddAxisLabel(labelPosition, System.Math.Round(tickValue, 0).ToString(), material, textOffset);
            else
                AddAxisLabel(labelPosition, tickValue.ToString("0.0"), material, textOffset);
        }
    }

    private void AddAxisLabel(Vector3 position, string text, Material material, Vector3 offset)
    {
        GameObject labelObject = new GameObject("AxisLabel");

        labelObject.transform.position = position + offset;
        labelObject.transform.SetParent(_parentTransform, false);

        TextMesh textMesh = labelObject.AddComponent<TextMesh>();
        textMesh.font = _lableFont;
        textMesh.text = text;
        textMesh.color = Color.black;
        textMesh.fontSize = 54;
        textMesh.characterSize = 0.1f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;

        MeshRenderer meshRenderer = labelObject.GetComponent<MeshRenderer>();
        meshRenderer.material = _lableFont.material;

        labelObject.AddComponent<Billboard>();
    }
}
