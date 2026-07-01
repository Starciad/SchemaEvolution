using MessagePack;
using MessagePack.Resolvers;

using SchemaEvolution.Core;
using SchemaEvolution.Core.Data;

using System;
using System.IO;

namespace SchemaEvolution.Sample
{
    internal sealed class SampleSerializer : SchemaSerializer
    {
        private readonly MessagePackSerializerOptions options = MessagePackSerializerOptions.Standard
            .WithResolver(CompositeResolver.Create(StandardResolver.Instance))
            .WithSecurity(MessagePackSecurity.UntrustedData)
            .WithCompression(MessagePackCompression.Lz4BlockArray);

        protected override IData OnDeserialize(Stream stream, Type versionType)
        {
            return (IData)MessagePackSerializer.Deserialize(versionType, stream, this.options);
        }

        protected override void OnSerialize(Stream stream, IData data)
        {
            MessagePackSerializer.Serialize(data.GetType(), stream, data, this.options);
        }
    }
}
