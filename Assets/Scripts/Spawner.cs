using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private Enemy _enemy;

    private const int DigitsAfterDot = 1;
    private const int TwoDelimeter = 2;

    private float _spawnSpan = 2.0f;
    private MeshRenderer _meshRenderer;
    private MeshRenderer _enemyMeshRenderer;
    private float _enemyYPosition;
    private Vector2 _topLeftPoint;
    private Vector2 _downRightPoint;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _enemyMeshRenderer = _enemy.GetComponent<MeshRenderer>();
        _enemyYPosition = _enemyMeshRenderer.bounds.size.y / TwoDelimeter;
        float x = _meshRenderer.bounds.size.x;
        float z = _meshRenderer.bounds.size.z;
        float minX = _meshRenderer.bounds.center.x - x / TwoDelimeter;
        float maxX = _meshRenderer.bounds.center.x + x / TwoDelimeter;
        float minZ = _meshRenderer.bounds.center.z - z / TwoDelimeter;
        float maxZ = _meshRenderer.bounds.center.z + z / TwoDelimeter;
        _topLeftPoint = new Vector2(minX, minZ);
        _downRightPoint = new Vector2(maxX, maxZ);

        StartCoroutine(GenerateEnemies());
    }

    private IEnumerator GenerateEnemies()
    {
        var waitForOneSeconds = new WaitForSeconds(_spawnSpan);

        for (int i = 0; i < _count; i++)
        {
            double x = Random.Range(_topLeftPoint.x, _downRightPoint.x);
            double z = Random.Range(_topLeftPoint.y, _downRightPoint.y);
            x = Math.Round(x, DigitsAfterDot);
            z = Math.Round(z, DigitsAfterDot);

            Instantiate(_enemy, new Vector3((float)x, _enemyYPosition, (float)z),
                Quaternion.identity);

            yield return waitForOneSeconds;
        }
    }
}