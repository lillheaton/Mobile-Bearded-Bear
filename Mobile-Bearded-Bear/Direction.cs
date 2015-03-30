using Microsoft.Xna.Framework;

namespace Mobile_Bearded_Bear
{
    public class Direction
    {
        private static Vector2 WestVector = new Vector2(-1, 0);
        private static Vector2 EastVector = new Vector2(1, 0);
        private static Vector2 NorthVector = new Vector2(0, -1);
        private static Vector2 SouthVector = new Vector2(0, 1);

        public static Direction West = new Direction(WestVector);
        public static Direction East = new Direction(EastVector);
        public static Direction North = new Direction(NorthVector);
        public static Direction South = new Direction(SouthVector);

        private readonly Vector2 direction;

        public Direction(Vector2 directionVector)
        {
            this.direction = directionVector;
        }

        public Vector2 GetVector()
        {
            return direction;
        }

        public bool IsOppositeOf(Direction dir)
        {
            return (Vector2.Add(dir.direction, this.direction) == new Vector2(0, 0));
        }

        public static bool operator ==(Direction value1, Direction value2)
        {
            if (value1 != null && value2 != null)
            {
                return value1.GetVector() == value2.GetVector();    
            }
            return false;
        }

        public static bool operator !=(Direction value1, Direction value2)
        {
            return !(value1 == value2);
        }
    }
}