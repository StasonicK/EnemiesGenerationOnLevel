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

    private List<SpawnPoint> _spawnsList = new List<SpawnPoint>();
    private float _xStart;
    private float _xEnd;
    private float _zStart;
    private float _zEnd;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _enemyMeshRenderer = _enemy.GetComponent<MeshRenderer>();
        _enemyYPosition = _enemyMeshRenderer.bounds.size.y / 2;
        float x = _meshRenderer.bounds.size.x;
        float z = _meshRenderer.bounds.size.z;
        _xStart = _meshRenderer.bounds.center.x - x / 2;
        _xEnd = _meshRenderer.bounds.center.x + x / 2;
        _zStart = _meshRenderer.bounds.center.z - z / 2;
        _zEnd = _meshRenderer.bounds.center.z + z / 2;

        Debug.Log($"_enemyYPosition _enemyYPosition {_enemyYPosition}");
        Debug.Log($"Start x {x}");
        Debug.Log($"Start z {z}");
        Debug.Log($"Start _xStart {_xStart}");
        Debug.Log($"Start _xEnd {_xEnd}");
        Debug.Log($"Start _zStart {_zStart}");
        Debug.Log($"Start _zEnd {_zEnd}");

        GenerateSpawnPoints();
        StartCoroutine(GenerateEnemies());
    }

    private IEnumerator GenerateEnemies()
    {
        var waitForOneSeconds = new WaitForSeconds(_spawnSpan);

        foreach (SpawnPoint spawnPoint in _spawnsList)
        {
            Instantiate(_enemy, new Vector3((float)spawnPoint.X, _enemyYPosition, (float)spawnPoint.Z),
                Quaternion.identity);

            yield return waitForOneSeconds;
        }
    }

    private void GenerateSpawnPoints()
    {
        Debug.Log("GenerateSpawnPoints");
        bool spawnPointEmpty = false;

        // Create spawn points
        for (int i = 0; i < _count; i++)
        {
            GenerateSpawnPoint();
        }
    }

    // Check for equals coordinate in existing spawn points and add new spawn point
    private void GenerateSpawnPoint()
    {
        double x = 0.0f;
        double z = 0.0f;
        bool spawnPointCreated = false;

        while (spawnPointCreated == false)
        {
            x = Random.Range(_xStart, _xEnd);
            z = Random.Range(_zStart, _zEnd);
            x =Math.Round(x, 1);
            z =Math.Round(z, 1);
            Debug.Log($"GenerateSpawnPoint x {x}");
            Debug.Log($"GenerateSpawnPoint z {z}");

            // if _spawnsList contains objects check for equals new coordinates with existing ones
            if (_spawnsList.Count != 0)
            {
                foreach (SpawnPoint spawnPoint in _spawnsList)
                {
                    if (spawnPoint.X == x && spawnPoint.Z == z)
                    {
                        spawnPointCreated = false;
                        Debug.Log($"GenerateSpawnPoint spawnPointEmpty {spawnPointCreated}");
                        break;
                    }
                    else
                    {
                        spawnPointCreated = true;
                        Debug.Log($"GenerateSpawnPoint spawnPointEmpty {spawnPointCreated}");
                    }
                }
            }
            else
            {
                spawnPointCreated = true;
                Debug.Log($"GenerateSpawnPoint spawnPointEmpty {spawnPointCreated}");
            }
        }

        Debug.Log($"GenerateSpawnPoint added SpawnPoint: x = {x},z = {z}");
        _spawnsList.Add(new SpawnPoint(x, z));
    }
}