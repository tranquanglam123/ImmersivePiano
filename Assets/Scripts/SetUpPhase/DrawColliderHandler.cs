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

    [SerializeField] GameObject pianoPrefab;
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
            //pianoPrefab.SetActive(false); // Disable the collider until it's needed again
        }

        if (count >= 2 && !colliderSpawned)
        {
            InitCollider(initPoint, endPoint);
            colliderSpawned = true; // Set the flag to true to prevent further spawning
            //gameObject.SetActive(false); // Disable this script to prevent further drawing
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
        pianoPrefab.SetActive(true); // Activate the collider when drawing starts
    }

    public void InitCollider(Vector3 init, Vector3 end)
    {
        //Vector3 center = Vector3.zero;
        Vector3 center = (init + end) / 2;
        Debug.Log("Center: " + center);
        //center.x  = (init.x + end.x) / 2;
        //center.z = end.z +0.25f;
        //center.y = init.y;

        // Calculate the direction vector of the line
        Vector3 direction = end - init;

        // Set the rotation of the collider to align with the line
        Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);

        // Spawn the collider at the center of the line, with the rotation aligned to the line
        StartCoroutine(SpawnPiano(center, rotation));
    }
    IEnumerator SpawnPiano(Vector3 center, Quaternion rotation)
    {
        yield return new WaitForSeconds(1);
        pianoPrefab.transform.SetPositionAndRotation(center, rotation);
        lineRenderer.enabled = false;
    }
}
