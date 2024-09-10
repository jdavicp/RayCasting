using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
namespace RayCasting;

class World
{
    private readonly byte[] m_Map;
    private readonly int TILE_SIZE = 64;

    public World() 
    { 
        m_Map = [
            1, 1, 1, 1, 1, 1, 1, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 1, 1, 1, 1, 1, 1, 1,
        ]; 
    }
    /* TODO(true)
        Remove those dynamic allocations to outside the main loop
    */
    public void Draw()
    {
        int xPos = 0;
        int yPos = 0;
        for(int i = 0; i < 8; ++i)
        {
            for(int j = 0; j < 8; ++j)
            {
                Rectangle wall = new(xPos, yPos, TILE_SIZE, TILE_SIZE);
                Color wallColor = m_Map[j + i*8] == 1 ? Color.Red : Color.Gray;
                DrawRectangleRec(wall, wallColor);
                DrawRectangleLinesEx(wall, 1.0f, Color.White);
                xPos += TILE_SIZE;
            }

            xPos = 0;
            yPos += TILE_SIZE;
        }
        
        Vector2 lineStart = new(1024/2.0f, 512.0f);
        Vector2 lineEnd = new(1024/2.0f, 0);
        
        DrawLineEx(lineStart, lineEnd, 4.0f, Color.Black );
    }

    public bool HitWall(Vector2 position)
    {
        int x = (int) (position.X / TILE_SIZE);
        int y = (int) (position.Y / TILE_SIZE);
        if(x > 8 || y > 8)
        {
            return true;
        }
        return m_Map[x + y*8] == 1;
    }
}

class Segment(float x, float y, float angle)
{
    Vector2 m_Position = new(x, y);
    float m_Angle = angle;
    float m_Size = 50f;
    Vector2 m_EndPoint = new(x, y);
    public void Draw()
    {
        Vector2 endPosition = new(m_Position.X + (float)Math.Cos(m_Angle)*m_Size, m_Position.Y + (float)Math.Sin(m_Angle)*m_Size);
        DrawLineEx(m_Position, endPosition, 2.0f, Color.Green);
    }

    public void Update()
    {
        if(IsKeyDown(KeyboardKey.Left)) {m_Angle -= 0.1f;}
        if(IsKeyDown(KeyboardKey.Right)) {m_Angle += 0.1f;}
    }

    public void Cast(World world)
    {
        
    }

}

class Point(float x, float y)
{
    private Vector2 m_Position = new(x, y);

    public void Draw()
    {
        DrawCircleV(m_Position, 7.0f, Color.Green);
    }
    public void Update()
    {
        if(IsKeyDown(KeyboardKey.Left))
        {
            m_Position.X -= 0.1f;
        }
        if(IsKeyDown(KeyboardKey.Right))
        {
            m_Position.X += 0.1f;
        }
        if(IsKeyDown(KeyboardKey.Up))
        {
            m_Position.Y -= 0.1f;
        }
        if(IsKeyDown(KeyboardKey.Down))
        {
            m_Position.Y += 0.1f;
        }
        
    }
    public void Hit(World world)
    {
        float x = m_Position.X;
        float y = m_Position.Y;
        Console.WriteLine($"Point({x},{y}) hit? {world.HitWall(m_Position)}");
    }
}

public class Program
{
    public static int Main()
    {
        const int screenWidth = 1024;
        const int screenHeight = 512;

        InitWindow(screenWidth, screenHeight, "RayCastingEngine");
        SetTargetFPS(60);
        World world = new();
        Segment ray = new(screenWidth/4.0f, screenHeight/2.0f, 3.1415f);
        while(!WindowShouldClose())
        {
            ray.Update();
            BeginDrawing();
            ClearBackground(Color.Gray);
            world.Draw();
            
            ray.Draw();
            EndDrawing();
        }
        CloseWindow();
        return 0;
    }
}