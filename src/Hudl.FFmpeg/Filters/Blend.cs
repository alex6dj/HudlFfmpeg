﻿using System.Collections.Generic;
using System.Linq;
using Hudl.FFmpeg.Attributes;
using Hudl.FFmpeg.Enums;
using Hudl.FFmpeg.Filters.Attributes;
using Hudl.FFmpeg.Filters.Interfaces;
using Hudl.FFmpeg.Formatters;
using Hudl.FFmpeg.Metadata;
using Hudl.FFmpeg.Metadata.Interfaces;
using Hudl.FFmpeg.Resources.BaseTypes;

namespace Hudl.FFmpeg.Filters
{
    /// <summary>
    /// Blend Video filter combines two input resources into a single Video output.
    /// </summary>
    [ForStream(Type=typeof(VideoStream))]
    [Filter(Name = "blend", MinInputs = 2, MaxInputs = 2)]
    public class Blend : 
        IFilter, 
        IMetadataManipulation
    {
        public Blend() 
        {
            Mode = BlendVideoModeType.and;
            Option = BlendVideoOptionType.all_expr;
        }
        public Blend(string expression) 
            : this()
        {
            Expression = expression;
        }

        [FilterParameter]
        public BlendVideoOptionType? Option { get; set; }

        [FilterParameter(Default = BlendVideoModeType.and)]
        public BlendVideoModeType? Mode { get; set; }

        /// <summary>
        /// the blend expression details can be found at http://ffmpeg.org/ffmpeg-all.html#blend. 
        /// </summary>
        [FilterParameter(Formatter = typeof(SingleQuoteFormatter))]
        public string Expression { get; set; }

        public virtual MetadataInfoTreeContainer EditInfo(MetadataInfoTreeContainer infoToUpdate, List<MetadataInfoTreeContainer> suppliedInfo)
        {
            infoToUpdate.VideoStream.VideoMetadata.Duration = suppliedInfo.Min(r => r.VideoStream.VideoMetadata.Duration);

            return infoToUpdate;
        }
    }
}
