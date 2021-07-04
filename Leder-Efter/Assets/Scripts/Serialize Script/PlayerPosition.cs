namespace ClientData
{
    [System.Serializable]
    public class Position
    {
        public float posX;
        public float posY;

        public Position(float x, float y)
        {
            posX = x;
            posY = y;
        }
    }
}