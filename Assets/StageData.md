```json
{
  "stages": [
    {
      "stageNumber": 1,
      "waves": [
        {
          "enemyType": "Normal",
          "enemyCount": 5,
          "spawnInterval": 1,
          "spawnX": 10
        },
        {
          "enemyType": "Fast",
          "enemyCount": 3,
          "spawnInterval": 0.7,
          "spawnX": -20
        },
        {
          "enemyType": "Fast",
          "enemyCount": 3,
          "spawnInterval": 1,
          "spawnX": 20
        }
      ],
      "boss": "Boss1"
    },
    {
      "stageNumber": 2,
      "waves": [
        {
          "enemyType": "Normal",
          "enemyCount": 10,
          "spawnInterval": 1,
          "spawnX": 20
        },
        {
          "enemyType": "Normal",
          "enemyCount": 10,
          "spawnInterval": 1,
          "spawnX": 10
        },
        {
          "enemyType": "Normal",
          "enemyCount": 10,
          "spawnInterval": 1,
          "spawnX": -10
        },
        {
          "enemyType": "Normal",
          "enemyCount": 10,
          "spawnInterval": 1,
          "spawnX": -20
        },
        {
          "enemyType": "Fast",
          "enemyCount": 5,
          "spawnInterval": 0.5,
          "spawnX": 0
        },
        {
          "enemyType": "Strong",
          "enemyCount": 3,
          "spawnInterval": 3,
          "spawnX": 0
        }
      ],
      "boss": "Boss2"
    },
    {
      "stageNumber": 3,
      "waves": [
        {
          "enemyType": "Normal",
          "enemyCount": 10,
          "spawnInterval": 1,
          "spawnX": 20
        },
        {
          "enemyType": "Strong",
          "enemyCount": 10,
          "spawnInterval": 2,
          "spawnX": -5
        },
        {
          "enemyType": "Normal",
          "enemyCount": 10,
          "spawnInterval": 1,
          "spawnX": -20
        },
        {
          "enemyType": "Fast",
          "enemyCount": 5,
          "spawnInterval": 0.4,
          "spawnX": 0
        },
        {
          "enemyType": "Strong",
          "enemyCount": 3,
          "spawnInterval": 2,
          "spawnX": 0
        }
      ],
      "boss": "Boss3"
    }
  ],
  "enemies": {
    "Normal": {
      "health": 40,
      "speed": 2
    },
    "Fast": {
      "health": 20,
      "speed": 4
    },
    "Strong": {
      "health": 200,
      "speed": 0.5
    }
  },
  "bosses": {
    "Boss1": {
      "health": 1200,
      "speed": 20,
      "attackPattern": "Pattern1"
    },
    "Boss2": {
      "health": 1000,
      "speed": 20,
      "attackPattern": "Pattern2"
    },
    "Boss3": {
      "health": 1500,
      "speed": 20,
      "attackPattern": "Pattern3"
    }
  }
}
```