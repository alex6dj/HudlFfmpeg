﻿using System;

namespace Hudl.FFmpeg.Exceptions
{
    /// <summary>
    /// exception that is thrown when FFmpeg returns an exit code other than 0.
    /// </summary>
    public class FFmpegProcessingException: Exception
    {
        public FFmpegProcessingException(int exitCode, string errorOutput)
            : base(string.Format("FFmpeg failed processing with an exit code of {0}",
                   exitCode))
        {
            base.Data["ExitCode"] = exitCode;
            base.Data["ErrorOutput"] = errorOutput;

            ExitCode = exitCode;
            ErrorOutput = errorOutput; 
        }

        public int ExitCode { get; private set; }

        public string ErrorOutput { get; private set; }
    }
}
