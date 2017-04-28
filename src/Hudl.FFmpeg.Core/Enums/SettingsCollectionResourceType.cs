﻿namespace Hudl.FFmpeg.Enums
{
    /// <summary>
    /// an enumeration describing how the setting applies to the command
    /// </summary>
    public enum SettingsCollectionResourceType
    {
        /// <summary>
        /// indicates that the setting can be applied to input and output type
        /// </summary>
        Any = 0,
        /// <summary>
        /// indicates that the setting can only be applied to an input stream
        /// </summary>
        Input = 1,
        /// <summary>
        /// indicates that the settig can only be applied to the output stream
        /// </summary>
        Output = 2
    }
}
