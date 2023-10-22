namespace Test01
{
    abstract class GameObject : IDrawable, IMovablecs
    {
        private Vector _postion;
        public Vector MinBounds;
        public Vector MaxBounds;
        public  Collider Collider;
        public Image Image;
        public Action OnDestroy;

        public Vector Position { get { return _postion; } set { _postion.X = value.X; _postion.Y = value.Y; } }
        public Vector Speed { get; set; }

        public void Draw(string[,] scene)
        {
            Image.Draw(scene);
        }

        virtual public void Move()
        {
            _postion.Add(Speed);
        }

        public GameObject(Vector postion)
        {
            _postion = postion;
            Speed = new Vector(0, 0);
            MinBounds = new Vector(int.MinValue, int.MinValue);
            MaxBounds = new Vector(int.MaxValue, int.MaxValue);
        }

        public bool CollideWith(GameObject other)
        {
            return Collider.CollideWith(other.Collider);
        }
    }
}