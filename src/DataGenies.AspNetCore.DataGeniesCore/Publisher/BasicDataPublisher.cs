﻿using System.Collections.Generic;
using System.Linq;
using DataGenies.AspNetCore.DataGeniesCore.Abstraction;

namespace DataGenies.AspNetCore.DataGeniesCore.Publisher
{
    public class BasicDataPublisher : IPublisher
    {
        private readonly IPublisher publisher;
        private readonly IEnumerable<IConverter> converters;
         
        private IConverter[] BeforePublish() => this.converters.Where(w => w.Type == ConverterType.BeforePublish).ToArray();

        public BasicDataPublisher(IPublisher publisher, IEnumerable<IConverter> converters)
        {
            this.publisher = publisher;
            this.converters = converters;
        }

        public void Publish(byte[] data)
        {
            foreach (var converter in BeforePublish())
            {
                data = converter.Convert(data);
            }

            this.publisher.Publish(data);
        }

        public void PublishRange(IEnumerable<byte[]> dataRange)
        {
            var dataRangeToPublish = new List<byte[]>();

            foreach (var data in dataRange)
            {
                var dataToPublish = data;
                
                foreach (var converter in BeforePublish())
                {
                    dataToPublish = converter.Convert(data);
                }

                dataRangeToPublish.Add(dataToPublish);
            }
             
            this.publisher.PublishRange(dataRangeToPublish);
        }
    }
}