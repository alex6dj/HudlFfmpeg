﻿using Hudl.FFmpeg.Attributes;
using Hudl.FFmpeg.Resources.BaseTypes;
using Hudl.FFmpeg.Resources.Interfaces;

namespace Hudl.FFmpeg.Resources
{
    [ContainsStream(Type = typeof(AudioStream))]
    [ContainsStream(Type = typeof(VideoStream))]
    [ContainsStream(Type = typeof(DataStream))]
    public class Ismv : BaseContainer
    {
        private const string FileFormat = ".ismv";

        public Ismv()
            : base(FileFormat)
        {
        }

        protected override IContainer Clone()
        {
            return new Ismv();
        }
    }
}