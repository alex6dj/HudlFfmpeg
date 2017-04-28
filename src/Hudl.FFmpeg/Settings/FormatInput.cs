﻿using Hudl.FFmpeg.Attributes;
using Hudl.FFmpeg.Enums;
using Hudl.FFmpeg.Resources.BaseTypes;
using Hudl.FFmpeg.Settings.Attributes;
using Hudl.FFmpeg.Settings.BaseTypes;

namespace Hudl.FFmpeg.Settings
{
    /// <summary>
    /// Force input or output file format. The format is normally auto detected for input files and guessed from the file extension for output files, so this option is not needed in most cases.
    /// </summary>
    [ForStream(Type = typeof(VideoStream))]
    [ForStream(Type = typeof(AudioStream))]
    [Setting(Name = "f", ResourceType = SettingsCollectionResourceType.Input)]
    public class FormatInput : BaseFormat
    {
        public FormatInput(string format)
            : base(format)
        {
        }

        public FormatInput(FormatType format)
            : base(format)
        {
        }
    }
}