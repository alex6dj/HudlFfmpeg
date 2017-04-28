﻿using Hudl.FFmpeg.Common;
using Hudl.FFmpeg.Enums;
using Hudl.FFmpeg.Metadata.Models;
using Hudl.FFmpeg.Resources.Interfaces;

namespace Hudl.FFmpeg.Resources.BaseTypes
{
    public class VideoStream : IStream
    {
        public VideoStream()
        {
            Map = Helpers.NewMap();
            Info = MetadataInfo.Create();
        }

        private VideoStream(MetadataInfo info)
            : this()
        {
            Info = info;
        }

        public string ResourceIndicator { get { return "v"; } }

        public string Map { get; set; }

        public MetadataInfo Info { get; set; }

        public IStream Copy()
        {
            return new VideoStream
                {
                    Info = Info
                };
        }

        public static VideoStream Create(MetadataInfo info)
        {
            return new VideoStream(info);
        }
    }
}
