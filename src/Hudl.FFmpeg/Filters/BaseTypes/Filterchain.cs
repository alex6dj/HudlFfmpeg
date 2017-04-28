﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hudl.FFmpeg.Collections;
using Hudl.FFmpeg.Command;
using Hudl.FFmpeg.Filters.Interfaces;
using Hudl.FFmpeg.Resources.Interfaces;

namespace Hudl.FFmpeg.Filters.BaseTypes
{
    public class Filterchain : IFilterchain
    {
        private Filterchain(List<IStream> outputsToUse) 
        {
            if (outputsToUse == null || outputsToUse.Count == 0)
            {
                throw new ArgumentNullException("outputsToUse");
            }

            Id = Guid.NewGuid().ToString();
            ReceiptList = new List<StreamIdentifier>();
            OutputList = new List<FilterchainOutput>(); 
            Filters = new ForStreamCollection<IFilter>(outputsToUse.First().GetType());
            OutputList.AddRange(outputsToUse.Select(output => FilterchainOutput.Create(this, output)));
        }
        private Filterchain(List<IStream> outputsToUse, params IFilter[] filters)
            : this(outputsToUse)
        {
            if (filters.Length > 0)
            {
                Filters.AddRange(filters); 
            }
        }

        public ForStreamCollection<IFilter> Filters { get; protected set; }

        public ReadOnlyCollection<StreamIdentifier> Resources { get { return ReceiptList.AsReadOnly(); } }

        public void SetResources(params StreamIdentifier[] streamIds)
        {
            if (streamIds == null)
            {
                throw new ArgumentNullException("streamIds");
            }
            if (streamIds.Length == 0)
            {
                throw new ArgumentException("Filterchain must contain at least one resource.");
            }

            SetResources(new List<StreamIdentifier>(streamIds));
        }

        public void SetResources(List<StreamIdentifier> streamIds)
        {
            if (streamIds == null)
            {
                throw new ArgumentNullException("streamIds");
            }
            if (streamIds.Count == 0)
            {
                throw new ArgumentException("Filterchain must contain at least one resource.");
            }

            ReceiptList = streamIds;
        }

        public Filterchain Copy()
        {
            var clonedResources = OutputList.Select(output => output.Stream.Copy());

            return FilterTo(clonedResources.ToList(), Filters.ToArray());
        }

        public List<StreamIdentifier> GetStreamIdentifiers()
        {
            return OutputList.Select(output => 
                    StreamIdentifier.Create(Owner.Owner.Owner.Id, Owner.Owner.Id, output.Stream.Map))
                    .ToList();
        }

        public static Filterchain FilterTo<TStreamType>(params IFilter[] filters)
            where TStreamType : class, IStream, new()
        {
            return FilterTo<TStreamType>(1, filters);
        }

        public static Filterchain FilterTo<TStreamType>(int count, params IFilter[] filters)
            where TStreamType : class, IStream, new()
        {
            var outputList = new List<IStream>();

            for (var i = 0; i < count; i++)
            {
                outputList.Add(new TStreamType());
            }

            return FilterTo(outputList, filters);
        }

        private static Filterchain FilterTo(List<IStream> outputsToUse, params IFilter[] filters)
        {
            if (outputsToUse == null || outputsToUse.Count == 0)
            {
                throw new ArgumentException("Outputs specified cannot be null or empty.", "outputsToUse");
            }

            return new Filterchain(outputsToUse, filters);
        }

        #region Internals
        internal string Id { get; set; }
        internal Filtergraph Owner { get; set; }
        internal List<StreamIdentifier> ReceiptList { get; set; }
        internal List<FilterchainOutput> OutputList { get; set; }
        #endregion

        public int InputCount
        {
            get { return ReceiptList.Count; }
        }

        public int OutputCount
        {
            get { return OutputList.Count; }
        }

        public void CreateInput(IStream stream)
        {
            var streamId = StreamIdentifier.Create(Owner.Owner.Owner.Id, Owner.Owner.Id, stream.Map);

            ReceiptList.Add(streamId);
        }

        public void CreateOutput(IStream stream)
        {
            var filterchainOutput = FilterchainOutput.Create(this, stream);

            OutputList.Add(filterchainOutput);
        }
    }
}
