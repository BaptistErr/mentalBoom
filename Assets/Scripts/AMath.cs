using System;
using System.Runtime.CompilerServices;

public static class AMath
{
    public const double DegToRad = Math.PI / 180D;
    public const double RadToDeg = 180D / Math.PI;
    
    public const float DegToRadF = (float)DegToRad;
    public const float RadToDegF = (float)RadToDeg;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int To1D(int x, int y, int z, int width, int height) => x + width*y + width*height*z;
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int To1D(int x, int y, int width) => x + width*y;
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void To2D(int index, int width, out int x, out int y)
    {
        x = index % width;
        y = index / width;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void To3D(int index, int width, int height, out int x, out int y, out int z)
    {
        x = index % width;
        y = (index / width) % height;
        z = index / (width * height);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Map(double value, double oldLow, double oldHigh, double newLow, double newHigh) 
        => newLow + (value - oldLow) * (newHigh - newLow) / (oldHigh - oldLow);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Map(float value, float oldLow, float oldHigh, float newLow, float newHigh) 
        => newLow + (value - oldLow) * (newHigh - newLow) / (oldHigh - oldLow);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CubicInterpolation(double a, double b, double c, double d, double t)
    {
        double x = d - c - (a - b);
        double y = a - b - x;
        double z = c - a;
        double w = b;
        return x*t*t*t + y*t*t + z*t + w;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double LinearInterpolation(double a, double b, double t) => (1.0D - t) * a + t * b;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SCurve3(double a) => a*a*(3.0 - 2.0*a);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double SCurve5(double a) => a*a*a*(a*(a*6.0 - 15.0) + 10.0);
}