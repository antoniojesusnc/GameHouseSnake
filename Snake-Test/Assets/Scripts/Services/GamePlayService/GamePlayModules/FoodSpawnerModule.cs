namespace GameHouse.Snake.GamePlay
{
    public class FoodSpawnerModule
    {
        public void BeginGame()
        {
            SpawnFood();
        }
        
        private void SpawnFood() {
            do {
                foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

            foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
            foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        }
    }
}