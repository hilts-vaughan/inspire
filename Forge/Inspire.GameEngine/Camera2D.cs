using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Inspire.GameEngine
{
    public class Camera2D
    {
           private const float zoomUpperLimit = 1.5f;
   private const float zoomLowerLimit = .5f;

   private float _zoom;
   private Matrix _transform;
   private Vector2 _pos;
   private float _rotation;
   private int _viewportWidth;
   private int _viewportHeight;
   private int _worldWidth;
   private int _worldHeight;

   public Camera2D(Vector2 viewport, int worldWidth, 
      int worldHeight, float initialZoom)
   {
      _zoom = initialZoom;
      _rotation = 0.0f;
      _pos = Vector2.Zero;
      _viewportWidth = (int) viewport.X;
      _viewportHeight = (int) viewport.Y;
      _worldWidth = worldWidth;
      _worldHeight = worldHeight;
   }

   #region Properties


        public Vector2 ViewportSize
        {
            set { _viewportWidth = (int) value.X;
                _viewportHeight = (int) value.Y;
            }
        }

   public float Zoom
   {
       get { return _zoom; }
       set
       {
           _zoom = value;
           if (_zoom < zoomLowerLimit)
              _zoom = zoomLowerLimit;
           if (_zoom > zoomUpperLimit)
              _zoom = zoomUpperLimit;
       }
   }

   public float Rotation
   {
       get { return _rotation; }
       set { _rotation = value; }
   }

   public void Move(Vector2 amount)
   {
       _pos += amount;
   }

   public Vector2 Pos
   {
       get { return _pos; }
       set
       {
           float leftBarrier = (float)_viewportWidth *
                  .5f / _zoom;
           float rightBarrier = _worldWidth -
                  (float)_viewportWidth * .5f / _zoom;
           float topBarrier = _worldHeight -
                  (float)_viewportHeight * .5f / _zoom;
           float bottomBarrier = (float)_viewportHeight *
                  .5f / _zoom;
           _pos = value;

        }
   }

   #endregion

   public Matrix GetTransformation()
   {
       var pos = new Vector2((float) Math.Floor(_pos.X), (float) Math.Floor(_pos.Y));

       _transform =
           Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0))*
           Matrix.CreateRotationZ(Rotation)*
           Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));

       return _transform;
   }
    }
}
