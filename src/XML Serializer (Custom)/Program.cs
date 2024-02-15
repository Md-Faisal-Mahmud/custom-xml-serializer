using JSON_Serializer__Custom_;

class Program
{
    public static void Main(string[] args)
    {

        //SampleInput o = new SampleInput();
        //string xml = xmlFormatter.Convert(o);
        //Console.WriteLine(xml);

        Course o = new Course();
        string xml = xmlFormatter.Convert(o);
        Console.WriteLine(xml);
    }
}