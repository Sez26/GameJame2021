using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMaker : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public float fov;
    public int rayCount;
    public float viewDistance;

    public MeshFilter meshFilter;

    public bool isPlayer = false;
    private PlayerController playerController;
    private GuardBehaviour guardBehaviour;

    // Start is called before the first frame update
    private void Start() {
        if (isPlayer) {
            playerController = transform.parent.GetComponent<PlayerController>();
        } else {
            guardBehaviour = transform.parent.GetComponent<GuardBehaviour>();
        }

        Mesh mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        float angleStep = -(fov * Mathf.PI/180f)/rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        var cosTheta = Mathf.Cos(fov*Mathf.PI/360f);
        var sinTheta = Mathf.Sin(fov*Mathf.PI/360f);

        var curVector = Vector3.zero;
        
        if (isPlayer) {
            curVector = playerController.LookVector;
        } else {
            curVector = new Vector3(guardBehaviour.lookVector.x, guardBehaviour.lookVector.y, 0);
        }
        
        Vector2 cur2d = new Vector2(curVector.x * cosTheta - curVector.y * sinTheta,
                                    curVector.x * sinTheta + curVector.y * cosTheta);
        curVector = new Vector3(cur2d.x, cur2d.y, 0);
        
        cosTheta = Mathf.Cos(angleStep);
        sinTheta = Mathf.Sin(angleStep);

        Vector3 origin = Vector3.zero;

        vertices[0] = origin;
        int vIndex = 1;
        int tIndex = 0;

        var rayOrigin = new Vector2(transform.position.x, transform.position.y);

        for (int i=0; i<=rayCount; i++) {
            Vector3 vertex;
            var rayHit = Physics2D.Raycast(rayOrigin, curVector, viewDistance, layerMask);

            if (rayHit.collider == null) {
                vertex = origin + curVector * viewDistance;
            } else {
                vertex = rayHit.point - rayOrigin;
            }

            vertices[vIndex] = vertex;

            if (i > 0) {
                triangles[tIndex] = 0;
                triangles[tIndex + 1] = vIndex - 1;
                triangles[tIndex + 2] = vIndex;

                tIndex += 3;
            }

            cur2d = new Vector2(curVector.x * cosTheta - curVector.y * sinTheta,
                                        curVector.x * sinTheta + curVector.y * cosTheta);
            curVector = new Vector3(cur2d.x, cur2d.y, 0);
            vIndex++;
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.uv = uv;
        meshFilter.mesh.triangles = triangles;
    }
}
