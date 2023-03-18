namespace MinecraftClone;

static class BlockData
{
    public static float[] OffsetFace(float[] data, Vector3Int offset)
    {
        data[0] += offset.X;
        data[1] += offset.Y;
        data[2] += offset.Z;

        data[8] += offset.X;
        data[9] += offset.Y;
        data[10] += offset.Z;

        data[16] += offset.X;
        data[17] += offset.Y;
        data[18] += offset.Z;

        data[24] += offset.X;
        data[25] += offset.Y;
        data[26] += offset.Z;

        return data;
    }

    public static float[] NegZFaceVertex => new[]
            {
            -0.5f, 0.5f, -0.5f,//Position
            0, 0, -1,//Normal
            0, 1,//UV

            0.5f, 0.5f, -0.5f,//Position
            0, 0, -1,//Normal
            1, 1,//UV

            0.5f, -0.5f, -0.5f,//Position
            0, 0, -1,//Normal
            1, 0,//UV

            -0.5f, -0.5f, -0.5f,//Position
            0, 0, -1,//Normal
            0, 0,//UV
        };

    public static uint[] NegZFaceIndice => new[]
    {
        0U, 1U, 2U,
        0U, 2U, 3U
        };

    public static float[] ZFaceVertex => new[]
    {
            -0.5f, 0.5f, 0.5f,//Position
            0, 0, 1,//Normal
            0, 1,//UV

            0.5f, 0.5f, 0.5f,//Position
            0, 0, 1,//Normal
            1, 1,//UV

            0.5f, -0.5f, 0.5f,//Position
            0, 0, 1,//Normal
            1, 0,//UV

            -0.5f, -0.5f, 0.5f,//Position
            0, 0, 1,//Normal
            0, 0,//UV
        };

    public static uint[] ZFaceIndice => new[]
    {
        1U, 0U, 3U,
        1U, 3U, 2U
        };

    public static float[] XFaceVertex => new[]
    {
            0.5f, 0.5f, -0.5f,//Position
            1, 0, 0,//Normal
            0, 1,//UV

            0.5f, 0.5f, 0.5f,//Position
            1, 0, 0,//Normal
            1, 1,//UV

            0.5f, -0.5f, 0.5f,//Position
            1, 0, 0,//Normal
            1, 0,//UV

            0.5f, -0.5f, -0.5f,//Position
            1, 0, 0,//Normal
            0, 0,//UV
        };

    public static uint[] XFaceIndice => new[]
    {
        0U, 1U, 2U,
        0U, 2U, 3U
        };

    public static float[] NegXFaceVertex => new[]
    {
            -0.5f, 0.5f, -0.5f,//Position
            -1, 0, 0,//Normal
            0, 1,//UV

            -0.5f, 0.5f, 0.5f,//Position
            -1, 0, 0,//Normal
            1, 1,//UV

            -0.5f, -0.5f, 0.5f,//Position
            -1, 0, 0,//Normal
            1, 0,//UV

            -0.5f, -0.5f, -0.5f,//Position
            -1, 0, 0,//Normal
            0, 0,//UV
        };

    public static uint[] NegXFaceIndice => new[]
    {
        1U, 0U, 3U,
        1U, 3U, 2U
        };

    public static float[] YFaceVertex => new[]
    {
            -0.5f, 0.5f, 0.5f,//Position
            0, 1, 0,//Normal
            0, 1,//UV

            0.5f, 0.5f, 0.5f,//Position
            0, 1, 0,//Normal
            1, 1,//UV

            0.5f, 0.5f, -0.5f,//Position
            0, 1, 0,//Normal
            1, 0,//UV

            -0.5f, 0.5f, -0.5f,//Position
            0, 1, 0,//Normal
            0, 0,//UV
        };

    public static uint[] YFaceIndice => new[]
    {
        0U, 1U, 2U,
        0U, 2U, 3U
        };

    public static float[] NegYFaceVertex => new[]
    {
            -0.5f, -0.5f, 0.5f,//Position
            0, -1, 0,//Normal
            0, 1,//UV

            0.5f,- 0.5f, 0.5f,//Position
            0, -1, 0,//Normal
            1, 1,//UV

            0.5f, -0.5f, -0.5f,//Position
            0, -1, 0,//Normal
            1, 0,//UV

            -0.5f, -0.5f, -0.5f,//Position
            0, -1, 0,//Normal
            0, 0,//UV
        };

    public static uint[] NegYFaceIndice => new[]
    {
        1U, 0U, 3U,
        1U, 3U, 2U
        };
}