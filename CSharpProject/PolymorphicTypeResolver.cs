using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace PyClickerRecorder
{
    /// <summary>
    /// Enables polymorphic deserialization for types annotated with JsonDerivedTypeAttribute.
    /// This is required for System.Text.Json to correctly handle the list of RecordedAction base types.
    /// </summary>
    public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

            Type basePointType = typeof(RecordedAction);
            if (jsonTypeInfo.Type == basePointType)
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = "$type",
                    IgnoreUnrecognizedTypeDiscriminators = true,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                };

                foreach (var derived in GetDerivedTypes(basePointType))
                {
                    var attr = (JsonDerivedTypeAttribute)Attribute.GetCustomAttribute(derived, typeof(JsonDerivedTypeAttribute));
                    if (attr != null)
                    {
                        jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(derived, attr.TypeDiscriminator.ToString()));
                    }
                }
            }

            return jsonTypeInfo;
        }

        private static IEnumerable<Type> GetDerivedTypes(Type baseType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (baseType.IsAssignableFrom(type) && type != baseType)
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}
