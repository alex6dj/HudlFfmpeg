﻿using Hudl.FFmpeg.Settings.Attributes;

namespace Hudl.FFmpeg.Settings.Serialization
{
    internal class SettingSerializerDataParameter
    {
        public string Value { get; set; }

        public SettingParameterAttribute Parameter { get; set; }
    }
}
