using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace type_converter.src
{
    public static class FFmpeg
    {
        public static bool Convert(string _inputPath, string _outputPath)
        {
            string ffmpegExecutablePath = Path.GetFullPath(@"..\..\..\src\ffmpeg.exe");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegExecutablePath,
                    Arguments = $"-i \"{_inputPath}\" \"{_outputPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0 )
            {
                var _error = process.StandardError.ReadToEnd();
                Debug.WriteLine(_error); // Debug
                return false;
            }

            return true;
        }
    }
}
