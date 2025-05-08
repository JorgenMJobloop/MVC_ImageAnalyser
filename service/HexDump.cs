using System.Diagnostics;

public static class HexDump
{
    private static Process? process;

    public static void DumpHexData(string? filePath)
    {
        if (process == null)
        {
            throw new InvalidProgramException("Your system does not have a hexdump tool available in it's Environment");
        }

        process = new Process
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "xxd",
                Arguments = $"\"{filePath}\"",
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };

        process.Start();
    }
}