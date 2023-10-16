using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test01
{
    abstract class Collider
    {

        public Vector Position { get; }
        public Collider(Vector position)
        {

            this.Position = position;
        }
       public abstract float LeftEdge{get;set;}
       public abstract float RightEdge{get;set;}
       public abstract float TopEdge{get;set;}
       public abstract float BottomEdge{get;set;}
       

        abstract bool CollideWith(RectCollider collider)
        abstract bool CollideWith(CircleCollider collider)
    }
    class RectCollider : Collider { 

        public RectCollider(float width, float height)
        {
            this._width = width;
            this._height = height;


         }

          public  float LeftEdge {
            get { return Position.X; }
            set { Position.X = value; }
        }

        public  float RightEdge {
            get { return Position.X + _width; }
            set { Position.X = value - _width; }
        }
          public  float BottomEdge {
            get { return Position.Y + _height; }
            set { Position.Y = value -_height ; }
        }

        public  float TopEdge {
            get { return Position.Y; }
            set { Position.Y = value ; }
        }   
        bool CollideWith(Collider collider)
        {
            collider.CollideWith(this)
        }
        bool CollideWith(RectCollider collider)
        {
           CollisionDetection.CheckCollision(this,collider)
        }
         bool CollideWith(CircleCollider collider)
        {
           CollisionDetection.CheckCollision(this,collider)
        }
  }   
     class CircleCollider : Collider
     {
     public CircleCollider(float radius)
        {
            this._radius = radius;
            
         }
     }

 class CollisionDetection
    {
        bool CheckCollision (RectCollider rect1, RectCollider rect2)
        {
        return (rect1.RightEdge>rect2.LeftEdge)
            &&(rect2.RightEdge>rect1.LeftEdge)
            &&(rect1.BottomEdge>rect2.TopEdge)
            &&(rect2.BottomEdge>rect1.TopEdge);
        }
        bool CheckCollision (CircleCollider circle1, RectCollider circle2)
        {
        Vector center1=new Vector(circle1.Position.X+circle1.Radius,
        circle1.Position.Y+circle1.Radius);
        Vector center2=new Vector(circle2.Position.X + circle2.Radius,
        circle2.Position.Y+circle2.Radius);
        float dist=(center1-center2).SquareSize;
        float radius=circle1.Radius+circle2.Radius
        ;return dist < radius * radius;

        }
        bool CheckCollision (RectCollider rect, CircleCollider circle)
        {
        float X=circle.Position.X+circle.Radius;
        float Y=circle.Position.Y+circle.Radius;

        float closeX=X;
        float closeY=Y;

        if (closeX<rect.LeftEdge) closeX=rect.LeftEdge;
        else if(closeX>rect.RightEdge)closeX=rect.RightEdge;

        if(closeY<rect.TopEdge)closeY=rect.TopEdge;
        else if(closeY>rect.BottomEdge)closeY=rect.BottomEdge;

        X-=closeX;
        Y-=closeY;

        return X*X+Y*Y<circle.Radius*circle.Radius;
    }
    }
     class Program
    {   
        static void Main(string[] args)
        {
            Collider circle1=new CircleCollider(new Vector(0,0),50);
            Collider circle2=new CircleCollider(new Vector(50,50),70);
            Collider rect1=new RectCollider(new Vector(110,110),50,100);
            Collider rect2=new RectCollider(new Vector(150,150),100,50);

            Console.WriteLine(circle1.CollideWith(circle2));
            Console.WriteLine(rect1.CollideWith(rect2));
            Console.WriteLine(circle2.CollideWith(rect1));
            Console.WriteLine(rect1.CollideWith(circle1));
            
            Console.ReadKey()
        }
    }
}
