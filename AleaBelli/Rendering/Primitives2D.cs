using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace C3.XNA
{
    /// <summary>
    /// </summary>
    public static class Primitives2Dx
    {


        #region Private Members

        private static readonly Dictionary<String, List<Vector2>> circleCache = new Dictionary<string, List<Vector2>>();
        //private static readonly Dictionary<String, List<Vector2>> arcCache = new Dictionary<string, List<Vector2>>();
        private static Texture2D pixel;

        #endregion


        #region Private Methods

        private static void CreateThePixel(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }


        /// <summary>
        /// Draws a list of connecting points
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// /// <param name="position">Where to position the points</param>
        /// <param name="points">The points to connect with lines</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the lines</param>
        private static void DrawPoints(SpriteBatch spriteBatch, Vector2 position, List<Vector2> points, Color color, float thickness)
        {
            if (points.Count < 2)
                return;

            for (int i = 1; i < points.Count; i++)
            {
                DrawLine(spriteBatch, points[i - 1] + position, points[i] + position, color, thickness);
            }
        }


        /// <summary>
        /// Creates a list of vectors that represents a circle
        /// </summary>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <returns>A list of vectors that, if connected, will create a circle</returns>
        private static List<Vector2> CreateCircle(double radius, int sides)
        {
            // Look for a cached version of this circle
            String circleKey = radius + "x" + sides;
            if (circleCache.ContainsKey(circleKey))
            {
                return circleCache[circleKey];
            }

            List<Vector2> vectors = new List<Vector2>();

            const double max = 2.0 * Math.PI;
            double step = max / sides;

            for (double theta = 0.0; theta < max; theta += step)
            {
                vectors.Add(new Vector2((float)(radius * Math.Cos(theta)), (float)(radius * Math.Sin(theta))));
            }

            // then add the first vector again so it's a complete loop
            vectors.Add(new Vector2((float)(radius * Math.Cos(0)), (float)(radius * Math.Sin(0))));

            // Cache this circle so that it can be quickly drawn next time
            circleCache.Add(circleKey, vectors);

            return vectors;
        }


        /// <summary>
        /// Creates a list of vectors that represents an arc
        /// </summary>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate in the circle that this will cut out from</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The radians to draw, clockwise from the starting angle</param>
        /// <returns>A list of vectors that, if connected, will create an arc</returns>
        private static List<Vector2> CreateArc(float radius, int sides, float startingAngle, float radians)
        {
            List<Vector2> points = new List<Vector2>();
            points.AddRange(CreateCircle(radius, sides));
            points.RemoveAt(points.Count - 1); // remove the last point because it's a duplicate of the first

            // The circle starts at (radius, 0)
            double curAngle = 0.0;
            double anglePerSide = MathHelper.TwoPi / sides;

            // "Rotate" to the starting point
            while ((curAngle + (anglePerSide / 2.0)) < startingAngle)
            {
                curAngle += anglePerSide;

                // move the first point to the end
                points.Add(points[0]);
                points.RemoveAt(0);
            }

            // Add the first point, just in case we make a full circle
            points.Add(points[0]);

            // Now remove the points at the end of the circle to create the arc
            int sidesInArc = (int)((radians / anglePerSide) + 0.5);
            points.RemoveRange(sidesInArc + 1, points.Count - sidesInArc - 1);

            return points;
        }

        #endregion


        #region FillRectangle

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            // Simply use the function already there
            spriteBatch.Draw(pixel, rect, color);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            spriteBatch.Draw(pixel, rect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
        {
            FillRectangle(spriteBatch, location, size, color, 0.0f);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            // stretch the pixel between the two vectors
            spriteBatch.Draw(pixel,
                             location,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             size,
                             SpriteEffects.None,
                             0);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, 0.0f);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle of the rectangle in radians</param>
        public static void FillRectangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle)
        {
            FillRectangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle);
        }

        #endregion


        #region DrawRectangle

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            DrawRectangle(spriteBatch, rect, color, 1.0f);
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness)
        {

            // TODO: Handle rotations
            // TODO: Figure out the pattern for the offsets required and then handle it in the line instead of here

            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // top
            DrawLine(spriteBatch, new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + thickness), color, thickness); // right
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1.0f);
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawRectangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float thickness)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness);
        }

        #endregion


        #region DrawLine

        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x1">The X coord of the first point</param>
        /// <param name="y1">The Y coord of the first point</param>
        /// <param name="x2">The X coord of the second point</param>
        /// <param name="y2">The Y coord of the second point</param>
        /// <param name="color">The color to use</param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }


        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x1">The X coord of the first point</param>
        /// <param name="y1">The Y coord of the first point</param>
        /// <param name="x2">The X coord of the second point</param>
        /// <param name="y2">The Y coord of the second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLine(this SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }


        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            DrawLine(spriteBatch, point1, point2, color, 1.0f);
        }


        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point1">The first point</param>
        /// <param name="point2">The second point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            // calculate the distance between the two vectors
            float distance = Vector2.Distance(point1, point2);

            // calculate the angle between the two vectors
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }


        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point in radians</param>
        /// <param name="color">The color to use</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color)
        {
            DrawLine(spriteBatch, point, length, angle, color, 1.0f);
        }


        /// <summary>
        /// Draws a line from point1 to point2 with an offset
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="point">The starting point</param>
        /// <param name="length">The length of the line</param>
        /// <param name="angle">The angle of this line from the starting point</param>
        /// <param name="color">The color to use</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            // stretch the pixel between the two vectors
            spriteBatch.Draw(pixel,
                             point,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             new Vector2(length, thickness),
                             SpriteEffects.None,
                             0);
        }

        #endregion


        #region PutPixel

        public static void PutPixel(this SpriteBatch spriteBatch, float x, float y, Color color)
        {
            PutPixel(spriteBatch, new Vector2(x, y), color);
        }


        public static void PutPixel(this SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            spriteBatch.Draw(pixel, position, color);
        }

        #endregion


        #region DrawCircle

        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="center">The center of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color)
        {
            DrawPoints(spriteBatch, center, CreateCircle(radius, sides), color, 1.0f);
        }


        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="center">The center of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        /// <param name="thickness">The thickness of the lines used</param>
        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, Color color, float thickness)
        {
            DrawPoints(spriteBatch, center, CreateCircle(radius, sides), color, thickness);
        }


        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The center X of the circle</param>
        /// <param name="y">The center Y of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color)
        {
            DrawPoints(spriteBatch, new Vector2(x, y), CreateCircle(radius, sides), color, 1.0f);
        }


        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The center X of the circle</param>
        /// <param name="y">The center Y of the circle</param>
        /// <param name="radius">The radius of the circle</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="color">The color of the circle</param>
        /// <param name="thickness">The thickness of the lines used</param>
        public static void DrawCircle(this SpriteBatch spriteBatch, float x, float y, float radius, int sides, Color color, float thickness)
        {
            DrawPoints(spriteBatch, new Vector2(x, y), CreateCircle(radius, sides), color, thickness);
        }

        #endregion


        #region DrawArc

        /// <summary>
        /// Draw a arc
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="center">The center of the arc</param>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The number of radians to draw, clockwise from the starting angle</param>
        /// <param name="color">The color of the arc</param>
        public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color)
        {
            DrawArc(spriteBatch, center, radius, sides, startingAngle, radians, color, 1.0f);
        }


        /// <summary>
        /// Draw a arc
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="center">The center of the arc</param>
        /// <param name="radius">The radius of the arc</param>
        /// <param name="sides">The number of sides to generate</param>
        /// <param name="startingAngle">The starting angle of arc, 0 being to the east, increasing as you go clockwise</param>
        /// <param name="radians">The number of radians to draw, clockwise from the starting angle</param>
        /// <param name="color">The color of the arc</param>
        /// <param name="thickness">The thickness of the arc</param>
        public static void DrawArc(this SpriteBatch spriteBatch, Vector2 center, float radius, int sides, float startingAngle, float radians, Color color, float thickness)
        {
            List<Vector2> arc = CreateArc(radius, sides, startingAngle, radians);
            //List<Vector2> arc = CreateArc2(radius, sides, startingAngle, degrees);
            DrawPoints(spriteBatch, center, arc, color, thickness);
        }

        #endregion


        #region DrawTriangle

        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillTriangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            // Simply use the function already there
            spriteBatch.Draw(pixel, rect, color);
        }

        /// <summary>
        ///  horizline draws horizontal segment with coordinates(S.x, Y), (E.x, Y)
        /// S.x, E.x are left and right x-coordinates of the segment we have to draw
        /// S = A means that S.x = A.x; S.y = A.y;
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="angle"></param>
        private static void Horizline(this SpriteBatch spriteBatch, float x1, float x2, float y, Color color, float angle, float offset)
        {
            //DrawLine(spriteBatch, x1, y, x2, y, color, 1.0f);
            Vector2 point = new Vector2(x1, y);
            float length = (x2 - x1);

            // stretch the pixel between the two vectors
            spriteBatch.Draw(pixel,
                             point,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             new Vector2(length, 1.0f),
                             SpriteEffects.None,
                             0);

        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        public static void FillTriangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float angle)
        {
            /*
                the coordinates of vertices are(A.x, A.y), (B.x, B.y), (C.x, C.y); we assume that A.y <= B.y <= C.y(you should sort them first)
    dx1,dx2,dx3 are deltas used in interpolation
    horizline draws horizontal segment with coordinates(S.x, Y), (E.x, Y)
    S.x, E.x are left and right x-coordinates of the segment we have to draw
    S = A means that S.x = A.x; S.y = A.y;
                ***begin triangle filler ***
                */
            Vector2 A = new Vector2(rect.X, rect.Y);
            Vector2 B = new Vector2(rect.X, rect.Bottom);
            Vector2 C = new Vector2(rect.Right, rect.Bottom);
            float dx1 = 0;
            float dx2 = 0;
            float dx3 = 0;

            if (B.Y - A.Y > 0) { dx1 = (B.X - A.X) / (B.Y - A.Y); } else dx1 = 0;
            if (C.Y - A.Y > 0) { dx2 = (C.X - A.X) / (C.Y - A.Y); } else dx2 = 0;
            if (C.Y - B.Y > 0) { dx3 = (C.X - B.X) / (C.Y - B.Y); } else dx3 = 0;

            Vector2 S = new Vector2(rect.X, rect.Y);
            Vector2 E = new Vector2(rect.X, rect.Y);
            Vector2 location = new Vector2(rect.X, rect.Y);
            Vector2 size = new Vector2(rect.Width, 2);
            if (dx1 > dx2)
            {
                for (; S.Y <= B.Y; S.Y++, E.Y++, S.X += dx2, E.X += dx1)
                {
                    Horizline(spriteBatch, S.X, E.X, S.Y, color, angle, rect.Y - S.Y);
                }
                E = B;
                for (; S.Y <= C.Y; S.Y++, E.Y++, S.X += dx2, E.X += dx3)
                    Horizline(spriteBatch, S.X, E.X, S.Y, color, angle, rect.Y - S.Y);
            }
            else
            {
                for (; S.Y <= B.Y; S.Y++, E.Y++, S.X += dx1, E.X += dx2)
                    Horizline(spriteBatch, S.X, E.X, S.Y, color, angle, rect.Y - S.Y);
                S = B;
                for (; S.Y <= C.Y; S.Y++, E.Y++, S.X += dx3, E.X += dx2)
                    Horizline(spriteBatch, S.X, E.X, S.Y, color, angle, rect.Y - S.Y);
            }


            /*
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            int x1 = rect.X;
            int y1 = rect.Y;
            int x2 = rect.Right;
            int y2 = rect.Bottom;

            for (int i = 0; i < x2; i++)
            {
                DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color); // top
                x1 += 1;
                y1 += 1;
                y2 += 1;
            }
            */

            /*
                                   Vector2 location = new Vector2(rect.X, rect.Y);
                        Vector2 size = new Vector2(rect.Width,2);
                        int height = rect.Height;

                        for (int i = 0; i < height; i++)
                        {
                            location = new Vector2(rect.X, rect.Y + height);

                            // stretch the pixel between the two vectors
                            spriteBatch.Draw(pixel,
                                         location,
                                         null,
                                         color,
                                         angle,
                                         Vector2.Zero,
                                         size,
                                         SpriteEffects.None,
                                         0);
                        }

                                // spriteBatch.Draw(pixel, rect, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
                                */
        }



        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillTriangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
        {
            FillTriangle(spriteBatch, location, size, color, 0.0f);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="angle">The angle in radians to draw the rectangle at</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillTriangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float angle)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }

            // stretch the pixel between the two vectors
            spriteBatch.Draw(pixel,
                             location,
                             null,
                             color,
                             angle,
                             Vector2.Zero,
                             size,
                             SpriteEffects.None,
                             0);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void FillTriangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color)
        {
            FillTriangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, 0.0f);
        }


        /// <summary>
        /// Draws a filled rectangle
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="x">The X coord of the left side</param>
        /// <param name="y">The Y coord of the upper side</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="angle">The angle of the rectangle in radians</param>
        public static void FillTriangle(this SpriteBatch spriteBatch, float x, float y, float w, float h, Color color, float angle)
        {
            FillTriangle(spriteBatch, new Vector2(x, y), new Vector2(w, h), color, angle);
        }

        #endregion


        #region DrawTriangle

        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void DrawTriangle(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            DrawTriangle(spriteBatch, rect, color, 1.0f);
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="rect">The rectangle to draw</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the lines</param>
        public static void DrawTriangle(this SpriteBatch spriteBatch, Rectangle rect, Color color, float thickness)
        {

            DrawLine(spriteBatch, new Vector2(rect.X, rect.Y), new Vector2(rect.Right, rect.Y), color, thickness); // top
            DrawLine(spriteBatch, new Vector2(rect.X + 1f, rect.Y), new Vector2(rect.X + 1f, rect.Bottom + thickness), color, thickness); // left
            DrawLine(spriteBatch, new Vector2(rect.X, rect.Bottom), new Vector2(rect.Right, rect.Bottom), color, thickness); // bottom
            DrawLine(spriteBatch, new Vector2(rect.Right + 1f, rect.Y), new Vector2(rect.Right + 1f, rect.Bottom + thickness), color, thickness); // right
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        public static void DrawTriangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color)
        {
            DrawTriangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, 1.0f);
        }


        /// <summary>
        /// Draws a rectangle with the thickness provided
        /// </summary>
        /// <param name="spriteBatch">The destination drawing surface</param>
        /// <param name="location">Where to draw</param>
        /// <param name="size">The size of the rectangle</param>
        /// <param name="color">The color to draw the rectangle in</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawTriangle(this SpriteBatch spriteBatch, Vector2 location, Vector2 size, Color color, float thickness)
        {
            DrawTriangle(spriteBatch, new Rectangle((int)location.X, (int)location.Y, (int)size.X, (int)size.Y), color, thickness);
        }



        #endregion
        /*
                rotected void paintComponent(Graphics g)
                {
                    super.paintComponent(g);

                    int x1 = 0;
                    int y1 = 0;
                    int x2 = 100;
                    int y2 = 0;

                    for (int i = 0; i < x2; i++)
                    {
                        g.drawLine(x1, y1, x2, y2);
                        x1 += 1;
                        y1 += 1;
                        y2 += 1;
                    }
                }

                public Dimension getPreferredSize()
                {
                    return new Dimension(100, 100);
                }

                public static void main(String[] args)
                {
                    JFrame frame = new JFrame();
                    frame.add(new Test());
                    frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
                    frame.pack();
                    frame.setVisible(true);
                }
        */

    }
}
