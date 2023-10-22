namespace Test01
{
    abstract class Collider
    {
        public Vector Position { get; }
        public Collider(Vector position)
        {
            this.Position = position;
        }
        public abstract float LeftEdge { get; set; }
        public abstract float RightEdge { get; set; }
        public abstract float TopEdge { get; set; }
        public abstract float BottomEdge { get; set; }
        public abstract bool CollideWith(Collider collider);
        public abstract bool CollideWith(RectCollider collider);
        public abstract bool CollideWith(CircleCollider collider);
    }
    class RectCollider : Collider
    {
        public float Width;
        public float Height;
        public RectCollider(Vector position, float width, float height) : base(position)
        {
            this.Width = width;
            this.Height = height;
        }

        public override float LeftEdge
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public override float RightEdge
        {
            get { return Position.X + Width; }
            set { Position.X = value - Width; }
        }
        public override float BottomEdge
        {
            get { return Position.Y + Height; }
            set { Position.Y = value - Height; }
        }

        public override float TopEdge
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }
        public override bool CollideWith(Collider collider)
        {
            return collider.CollideWith(this);
        }
        public override bool CollideWith(RectCollider collider)
        {
            return CollisionDetection.CheckCollision(this, collider);
        }
        public override bool CollideWith(CircleCollider collider)
        {
            return CollisionDetection.CheckCollision(this, collider);
        }
    }
    class CircleCollider : Collider
    {
        public float Radius;
        public CircleCollider(Vector position, float radius) : base(position)
        {
            this.Radius = radius;
        }
        public override float LeftEdge
        {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public override float RightEdge
        {
            get { return Position.X + Radius * 2; }
            set { Position.X = value - Radius * 2; }
        }
        public override float BottomEdge
        {
            get { return Position.Y + Radius * 2; }
            set { Position.Y = value - Radius * 2; }
        }

        public override float TopEdge
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public override bool CollideWith(Collider collider)
        {
            return collider.CollideWith(this);
        }
        public override bool CollideWith(RectCollider collider)
        {
            return CollisionDetection.CheckCollision(collider, this);
        }
        public override bool CollideWith(CircleCollider collider)
        {
            return CollisionDetection.CheckCollision(collider, this);
        }
    }

    class CollisionDetection
    {
        public static bool CheckCollision(RectCollider rect1, RectCollider rect2)
        {
            return (rect1.RightEdge > rect2.LeftEdge)
                && (rect2.RightEdge > rect1.LeftEdge)
                && (rect1.BottomEdge > rect2.TopEdge)
                && (rect2.BottomEdge > rect1.TopEdge);
        }
        public static bool CheckCollision(CircleCollider circle1, CircleCollider circle2)
        {
            Vector center1 = new Vector(circle1.Position.X + circle1.Radius,
            circle1.Position.Y + circle1.Radius);
            Vector center2 = new Vector(circle2.Position.X + circle2.Radius,
            circle2.Position.Y + circle2.Radius);
            float dist = (center1 - center2).SquareSize;
            float radius = circle1.Radius + circle2.Radius;
            return dist < radius * radius;

        }
        public static bool CheckCollision(RectCollider rect, CircleCollider circle)
        {
            float X = circle.Position.X + circle.Radius;
            float Y = circle.Position.Y + circle.Radius;

            float closeX = X;
            float closeY = Y;

            if (closeX < rect.LeftEdge) closeX = rect.LeftEdge;
            else if (closeX > rect.RightEdge) closeX = rect.RightEdge;

            if (closeY < rect.TopEdge) closeY = rect.TopEdge;
            else if (closeY > rect.BottomEdge) closeY = rect.BottomEdge;

            X -= closeX;
            Y -= closeY;

            return X * X + Y * Y < circle.Radius * circle.Radius;
        }
    }
}