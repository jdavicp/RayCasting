using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace RayCasting;

class World
{
    private readonly byte[] m_Map;
    private readonly int TILE_SIZE = 64;

    public World() 
    { 
        m_Map = new byte[64] {
            1, 1, 1, 1, 1, 1, 1, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 0, 0, 0, 0, 0, 0, 1,
            1, 1, 1, 1, 1, 1, 1, 1,
        }; 
    }
    /* TODO(true)
        Remove those dynamic allocations to outside the loop
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

    public bool HitWall(Vector2 postion)
    {
        //todo
        return true;
    }
}

public class Program
{
    public static int Main()
    {
        const int screenWidth = 1024;
        const int screenHeight = 512;

        InitWindow(screenWidth, screenHeight, "RayCastingEngine");
        World world = new();
        while(!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.Gray);

            world.Draw();
            EndDrawing();
        }
        CloseWindow();
        return 0;
    }
}