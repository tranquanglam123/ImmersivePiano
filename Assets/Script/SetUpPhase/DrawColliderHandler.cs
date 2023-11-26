using System.Collections;
using UnityEngine;

public class DrawColliderHandler : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float width = 0.003f;
    private int positionCount = 2;
    private int count;
    public Material material;
    public GetPointerHandler getPointerHandler;
    private bool isDrawing = false;

    [SerializeField] GameObject colliderPrefab;
    private Vector3 initPoint;
    private Vector3 endPoint;
    private bool colliderSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        getPointerHandler = GetComponent<GetPointerHandler>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.positionCount = positionCount;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            var skeleton = getPointerHandler.rightHand.GetComponent<OVRSkeleton>();
            foreach (var b in skeleton.Bones)
            {
                if (b.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {
                    endPoint = b.Transform.position;
                    lineRenderer.SetPosition(1, endPoint);
                }
            }
        }
        else
        {
            colliderPrefab.SetActive(false); // Disable the collider until it's needed again
        }

        if (count >= 2 && !colliderSpawned)
        {
            InitCollider(initPoint, endPoint);
            colliderSpawned = true; // Set the flag to true to prevent further spawning
        }
    }

    void DrawLine()
    {
        if (count < 1)
        {
            initPoint = getPointerHandler.GetPointer(0);
            lineRenderer.SetPosition(0, initPoint);
            count++;
        }
        else
        {
            isDrawing = false;
            count++;
        }
    }

    public void StartDrawing()
    {
        isDrawing = true;
        DrawLine();
        colliderPrefab.SetActive(true); // Activate the collider when drawing starts
    }

    public void InitCollider(Vector3 init, Vector3 end)
    {
        Vector3 center = (init + end) / 2;
        center.z += 0.25f;

        // Calculate the direction vector of the line
        Vector3 direction = end - init;

        // Set the rotation of the collider to align with the line
        Quaternion rotation = Quaternion.LookRotation(direction)*Quaternion.Euler(0,90,0);

        // Spawn the collider at the center of the line, with the rotation aligned to the line
        GameObject collider = Instantiate(colliderPrefab, center, rotation);
        collider.SetActive(true); // Activate the collider after instantiation
        colliderPrefab.SetActive(false); // Disable the prefab until drawing starts again
    }
}
