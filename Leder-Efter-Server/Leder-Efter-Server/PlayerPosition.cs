using Leder_Efter_Server;
using System.Numerics;

namespace ClientData
{
    public class Position
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        private float speed = 5f / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        public Position(int _id, string uname, Vector3 pos)
        {
            id = _id;
            username = uname;
            position = pos;
            rotation = Quaternion.Identity;

            inputs = new bool[4];
        }

        public void Update()
        {
            Vector2 direction = Vector2.Zero;
            if (inputs[0])
            {
                direction.Y += 1;
            }
            if (inputs[1])
            {
                direction.Y -= 1;
            }
            if (inputs[2])
            {
                direction.X -= 1;
            }
            if (inputs[3])
            {
                direction.X += 1;
            }

            Move(direction);
        }

        private void Move(Vector2 direction)
        {
            Vector3 forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);
            //Vector3 forward = Vector3.Normalize(Vector3.Cross(new Vector3(1, 0, 0), new Vector3(0, 0, 0)));
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0, 1, 0)));

            Vector3 moveDir = right * direction.X + forward * direction.Y;
            position += moveDir * speed;

            ServerSend.PlayerPos(this);
            ServerSend.PlayerRot(this);
        }

        public void SetInput(bool[] _inputs, Quaternion rot)
        {
            inputs = _inputs;
            rotation = rot;
        }
    }
}