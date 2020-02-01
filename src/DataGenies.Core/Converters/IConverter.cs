namespace DataGenies.Core.Converters
{
    public interface IConverter
    {
        ConverterType Type { get; set; }

        byte[] Convert(byte[] data);
    }
}