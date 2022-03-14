using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private RandomTiles _randomTiles = null;

    private const int LAYER_TILE = 3;

    private void OnTriggerExit(Collider other)
    {
        if (LAYER_TILE == other.gameObject.layer)
        {
            ETileTypes type = ETileTypes.Pavement;

            switch (other.name)
            {
                case ConstantValue.PAVEMENT:
                    type = ETileTypes.Pavement;
                    break;
                case ConstantValue.ROAD:
                    type = ETileTypes.Road;
                    break;
                case ConstantValue.RAILWAY:
                    type = ETileTypes.RailWay;
                    break;
                case ConstantValue.RIVER:
                    type = ETileTypes.River;
                    break;
                default:
                    break;
            }

            _randomTiles.ReturnTile(type, other.gameObject);
        }
    }
}
