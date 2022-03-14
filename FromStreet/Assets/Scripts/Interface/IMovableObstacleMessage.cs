using UnityEngine;

public interface IMovableObstacleMessage
{
    public void SetMovableObstacleInfomations(float moveSpeed, Vector3 spawnPosition, Transform transform);
}

