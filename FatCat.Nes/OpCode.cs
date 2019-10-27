using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FatCat.Nes
{
	public class JsonConverterStringToInt : JsonConverter<int>
	{
		public override int Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				// try to parse number directly from bytes
				var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

				if (Utf8Parser.TryParse(span, out int number, out var bytesConsumed) && span.Length == bytesConsumed) return number;

				// try to parse from a string if the above failed, this covers cases with other escaped/UTF characters
				if (int.TryParse(reader.GetString(), out number)) return number;
			}

			// fallback to default handling
			return reader.GetInt32();
		}

		public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
	}

	public class OpCode
	{
		[JsonPropertyName("bytes")]
		[JsonConverter(typeof(JsonConverterStringToInt))]
		public int Bytes { get; set; }

		[JsonPropertyName("cycles")]
		public string Cycles { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("mode")]
		public string Mode { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("opcode")]
		public string Value { get; set; }
	}
}