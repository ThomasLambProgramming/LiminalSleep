using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private Vector2 _grid;

    [SerializeField]
    private Vector2 _offset;


    private void Start()
    {
        Build();
    }

    private void Build()
    {
        int count = 0;

        for (int i = 0; i < _grid.x; i++)
        {
            for (int j = 0; j < _grid.y; j++)
            {
                GameObject instance = GameObject.Instantiate(_prefab, transform);
                instance.transform.localPosition = new Vector3(_offset.x * i, 0, _offset.y * j);
                instance.name = "" + count;
                count++;
            }

            // prepare all gameObject children to static batching
            StaticBatchingUtility.Combine(gameObject);
        }
    }
}