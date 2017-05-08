using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Mono.Cecil;
using UnityEngine;

[ExecuteInEditMode]
public class DynamicShapeRenderer : MonoBehaviour
{
    public ShapeController shape;

    public LineRenderer lineRenderer;

    public float radius = 1;
    
    private LinkedList<DynamicVertex> vertices;

    public bool close;

    public class DynamicVertex
    {
        public Vector3 position;
        public Vector3 target;
        public float speed;

        public DynamicVertex(Vector3 valuePosition, Vector3 valueTarget, float valueSpeed)
        {
            this.position = valuePosition;
            this.target = valueTarget;
            this.speed = valueSpeed;
        }
    }

	// Use this for initialization
	void Start () {
        vertices = new LinkedList<DynamicVertex>();
        vertices.AddLast(new DynamicVertex(Vector3.zero, Vector3.zero, 1));
        vertices.AddLast(new DynamicVertex(Vector3.zero, Vector3.zero, 1));
        vertices.AddLast(new DynamicVertex(Vector3.zero, Vector3.zero, 1));
    }
	
	// Update is called once per frame
    void Update()
    {
        UpdateVertexList();

        foreach (var vertex in vertices)
        {
            vertex.position = Vector3.Lerp(vertex.position, vertex.target, Time.deltaTime);
        }

        int size = close ? vertices.Count + 2: vertices.Count;
        var renderList = new Vector3[size];
        int n = 0;
        foreach (var vert in vertices)
        {
            renderList[n++] = vert.position;
        }
        if (close)
        {
            renderList[renderList.Length - 2] = renderList[0];
            renderList[renderList.Length - 1] = renderList[1];
        }

        lineRenderer.numPositions = renderList.Length;
	    lineRenderer.SetPositions(renderList);
	}

    private void UpdateVertexList()
    {
        int points = shape.sides;
        int vertexPoints = vertices.Count;
        int i = 0;
        foreach (var point in vertices)
        {
            float x = Mathf.Cos((i / (float) points) * 2 * Mathf.PI);
            float y = Mathf.Sin((i / (float) points) * 2 * Mathf.PI);
            //vertices.
            point.target = new Vector3(x, 0, y) * radius;
            i++;
        }
        for (i = vertexPoints; i < points; ++i)
        {
            float x = Mathf.Cos((i / (float) points) * 2 * Mathf.PI);
            float y = Mathf.Sin((i / (float) points) * 2 * Mathf.PI);
            var origin = close ? vertices.First : vertices.Last;
            vertices.AddLast(new DynamicVertex(origin.Value.position, new Vector3(x, 0, y) * radius,
                origin.Value.speed));
        }
        while (vertices.Count > points)
        {
            vertices.RemoveLast();
        }
    }
}
