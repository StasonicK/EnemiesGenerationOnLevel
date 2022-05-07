using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EmeniesGenearator : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private GameObject _enemy;

    private float _spawnSpan = 2.0f;
    private MeshRenderer _meshRenderer;
    private MeshRenderer _enemyMeshRenderer;

    private float _enemyYPosition;
    private Vector2 _start;
    private Vector2 _end;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _enemyMeshRenderer = _enemy.GetComponent<MeshRenderer>();
        _enemyYPosition = _enemyMeshRenderer.bounds.size.y / 2;
        float x = _meshRenderer.bounds.size.x;
        float z = _meshRenderer.bounds.size.z;
        _start = new Vector2(_meshRenderer.bounds.center.x - x / 2, _meshRenderer.bounds.center.z - z / 2);
        _end = new Vector2(_meshRenderer.bounds.center.x + x / 2, _meshRenderer.bounds.center.z + z / 2);

        // GenerateSpawnPoints();
        StartCoroutine(GenerateEnemies());
    }

    private IEnumerator GenerateEnemies()
    {
        var waitForOneSeconds = new WaitForSeconds(_spawnSpan);

        for (int i = 0; i < _count; i++)
        {
            double x = Random.Range(_start.x, _end.x);
            double z = Random.Range(_start.y, _end.y);
            x = Math.Round(x, 1);
            z = Math.Round(z, 1);

            Instantiate(_enemy, new Vector3((float)x, _enemyYPosition, (float)z),
                Quaternion.identity);

            yield return waitForOneSeconds;
        }
    }
}