using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public float speed = 5f;
    public float gap = 3f;

    private float leftEdge;

    // Khởi tạo giá trị ban đầu cho các pipe
    public void Initialize()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        top.position += Vector3.up * gap / 2;
        bottom.position += Vector3.down * gap / 2;
    }

    // Di chuyển pipe qua thời gian
    public void MovePipe()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    // Gọi Initialize trong Start() để khởi tạo
    private void Start()
    {
        Initialize();
    }

    // Gọi MovePipe trong Update() để di chuyển
    private void Update()
    {
        MovePipe();
    }
}

public class PipesTest
{
    private GameObject pipeObject;
    private Pipes pipes;
    private Transform top;
    private Transform bottom;

    [SetUp]
    public void Setup()
    {
        // Khởi tạo đối tượng và thêm script Pipes
        pipeObject = new GameObject("Pipe");
        pipes = pipeObject.AddComponent<Pipes>();

        pipes.speed = 5f;
        pipes.gap = 3f;

        // Tạo các đối tượng cho top và bottom
        GameObject topObj = new GameObject("Top");
        GameObject bottomObj = new GameObject("Bottom");

        top = topObj.transform;
        bottom = bottomObj.transform;

        pipes.top = top;
        pipes.bottom = bottom;

        top.parent = pipeObject.transform;
        bottom.parent = pipeObject.transform;

        // Tạo camera giả nếu chưa có
        if (Camera.main == null)
        {
            var cam = new GameObject("MainCamera");
            cam.tag = "MainCamera";
            cam.AddComponent<Camera>();
        }

        // Gọi Initialize thay cho Start()
        pipes.Initialize();
    }

    [UnityTest]
    public IEnumerator PipeMovesLeftOverTime()
    {
        Vector3 initialPos = pipeObject.transform.position;

        // Đợi một chút rồi kiểm tra
        yield return new WaitForSeconds(0.5f);

        pipes.MovePipe(); // Thay cho Update()

        // Kiểm tra xem vị trí của pipeObject đã thay đổi chưa
        Assert.Less(pipeObject.transform.position.x, initialPos.x);
    }

    [UnityTest]
    public IEnumerator PipeDestroyedWhenOffScreen()
    {
        pipeObject.transform.position = new Vector3(-100f, 0f, 0f);

        // Cập nhật một lần và kiểm tra hủy
        yield return null;
        pipes.MovePipe();

        // Đợi một khung hình nữa để xem nếu đối tượng bị phá huỷ
        yield return null;

        // Kiểm tra đối tượng có bị huỷ
        Assert.IsTrue(pipeObject == null || !pipeObject.activeInHierarchy, "Pipe should be destroyed when off screen.");
    }
}
