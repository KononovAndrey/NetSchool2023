namespace NetSchool.Test.Api;

public static class BooksTestData
{
    private static TestDataUtilities testDataUtilities = new();

    public static string[] ValidBookNames => new string[]
    {
        "B",
        "Book1",
        "Book 1",
        testDataUtilities.GetRandomString(100)
    };

    public static string[] InvalidBookNames => new string[]
    {
        string.Empty,
        testDataUtilities.GetRandomString(101)
    };
}