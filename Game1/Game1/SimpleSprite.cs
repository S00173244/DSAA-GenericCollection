using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Engine.Engines;

namespace Sprites
{
    public class SimpleSprite
    {
        public Texture2D Image;
        public Vector2 Position;
        public Rectangle BoundingRect;
        static Random rnd = new Random();
        public Vector2 Target;
        public bool IsClicked = false;
        // Constructor expects to see a loaded Texture
        // and a start position
        public SimpleSprite( Texture2D spriteImage,
                            Vector2 startPosition)
        {
            //
            // Take a copy of the texture passed down
            Image = spriteImage;
            // Take a copy of the start position
            Position = startPosition;
            // Calculate the bounding rectangle
            BoundingRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, Image.Width, Image.Height);

        }

        public void Update(GameTime gameTime, Viewport v)
        {
            //         if(gameTime.TotalGameTime.Seconds % 1 == 0)
            //         {

            //             Vector2 newPosition = Vector2.Clamp(Position + new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)), new Vector2(v.X-Image.Width, v.Y-Image.Height));
            ////             Vector2 newPosition = Vector2.SmoothStep(Position, new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10)), 0.1f);


            //             //Vector2 newPosition = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));
            //             Position += newPosition;
            //             BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            //         }
            if (InputEngine.IsMouseLeftClick() && BoundingRect.Contains(InputEngine.MousePosition)) IsClicked = true;
            //if (gameTime.TotalGameTime.Seconds % 3 == 0)
            //{
                Position = Vector2.Lerp(Position, Target, 0.1f);
                if (Vector2.DistanceSquared(Position, Target) <= 0.1f)
                {
                    Position = Target;
                    Target = new Vector2(rnd.Next(0, v.Width - Image.Width), rnd.Next(0, v.Height - Image.Height));
                    Console.WriteLine("Position : " + Position);
                    Console.WriteLine("Target : " + Target);
                }
                BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            //}

            
        }

        public void draw(SpriteBatch sp)
        {
            
            if (Image != null)
            {
                sp.Begin();
                sp.Draw(Image, BoundingRect, Color.White);
                sp.End();
            }
        }

        public void Move(Vector2 delta)
        {
            Position += delta;
            BoundingRect = new Rectangle((int)Position.X, (int)Position.Y, Image.Width, Image.Height);
            BoundingRect.X = (int)Position.X;
            BoundingRect.Y = (int)Position.Y;
        }

    }
}
