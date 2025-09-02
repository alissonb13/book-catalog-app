using BookCatalog.Domain.Entities;
using BookCatalog.Domain.Enums;

namespace BookCatalog.Infrastructure.Data.Configurations;

public static class BooksSeeding
{
    public static List<Book> GenerateBooks()
    {
        var catalog = new List<Book>();
        var random = new Random();

        var books = new (string Title, string Author)[]
        {
            ("Clean Code", "Robert C. Martin"),
            ("Domain-Driven Design", "Eric Evans"),
            ("The Pragmatic Programmer", "Andrew Hunt & David Thomas"),
            ("Refactoring", "Martin Fowler"),
            ("Patterns of Enterprise Application Architecture", "Martin Fowler"),
            ("Test-Driven Development: By Example", "Kent Beck"),
            ("Code Complete", "Steve McConnell"),
            ("Working Effectively with Legacy Code", "Michael Feathers"),
            ("Design Patterns", "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides"),
            ("Microservices Patterns", "Chris Richardson"),
            ("Building Microservices", "Sam Newman"),
            ("Software Architecture in Practice", "Len Bass, Paul Clements, Rick Kazman"),
            ("Continuous Delivery", "Jez Humble, David Farley"),
            ("Accelerate", "Nicole Forsgren, Jez Humble, Gene Kim"),
            ("Extreme Programming Explained", "Kent Beck"),
            ("Head First Design Patterns", "Eric Freeman, Elisabeth Robson"),
            ("Implementing Domain-Driven Design", "Vaughn Vernon"),
            ("Release It!", "Michael T. Nygard"),
            ("Site Reliability Engineering", "Betsy Beyer, Chris Jones, Jennifer Petoff, Niall Richard Murphy"),
            ("Production-Ready Microservices", "Susan J. Fowler"),
            ("Software Engineering at Google", "Titus Winters, Tom Manshreck, Hyrum Wright"),
            ("Cloud Native Patterns", "Cornelia Davis"),
            ("Building Evolutionary Architectures", "Neal Ford, Rebecca Parsons, Patrick Kua"),
            ("Enterprise Integration Patterns", "Gregor Hohpe, Bobby Woolf"),
            ("The Mythical Man-Month", "Fred Brooks"),
            ("Lean Software Development", "Mary Poppendieck, Tom Poppendieck"),
            ("Agile Estimating and Planning", "Mike Cohn"),
            ("Clean Architecture", "Robert C. Martin"),
            ("Building Event-Driven Microservices", "Adam Bellemare"),
            ("You Don’t Know JS", "Kyle Simpson"),
            ("Java Concurrency in Practice", "Brian Goetz"),
            ("Effective Java", "Joshua Bloch"),
            ("Programming Pearls", "Jon Bentley"),
            ("The Art of Computer Programming", "Donald Knuth"),
            ("Structure and Interpretation of Computer Programs", "Harold Abelson, Gerald Jay Sussman"),
            ("Introduction to Algorithms", "Thomas H. Cormen, Charles E. Leiserson, Ronald Rivest, Clifford Stein"),
            ("Grokking Algorithms", "Aditya Bhargava"),
            ("Cracking the Coding Interview", "Gayle Laakmann McDowell"),
            ("Algorithms to Live By", "Brian Christian, Tom Griffiths"),
            ("A Philosophy of Software Design", "John Ousterhout"),
            ("Practical Object-Oriented Design", "Sandi Metz"),
            ("Refactoring to Patterns", "Joshua Kerievsky"),
            ("Growing Object-Oriented Software, Guided by Tests", "Steve Freeman, Nat Pryce"),
            ("The Clean Coder", "Robert C. Martin"),
            ("97 Things Every Programmer Should Know", "Kevlin Henney (Editor)"),
            ("The DevOps Handbook", "Gene Kim, Jez Humble, Patrick Debois, John Willis"),
            ("Infrastructure as Code", "Kief Morris"),
            ("Kubernetes in Action", "Marko Lukša"),
            ("Learning Go", "Jon Bodner"),
            ("C# 13 and .NET 9 - Modern Cross-Platform Development Fundamentals", "Mark J. Price")
        };

        for (var i = 0; i < books.Length; i++)
        {
            catalog.Add(new Book(
                isbn: $"978-0-{random.Next(1000000, 9999999)}",
                title: books[i].Title,
                author: books[i].Author,
                ownership: Enum.GetValues<Ownership>()[i % Enum.GetValues<Ownership>().Length]
            ));
        }

        return catalog;
    }
}