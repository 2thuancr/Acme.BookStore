using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Categories;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore;

public class BookStoreDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Category, Guid> _categoryRepository;
    private readonly IRepository<Book, Guid> _bookRepository;

    public BookStoreDataSeederContributor(
        IRepository<Category, Guid> categoryRepository,
        IRepository<Book, Guid> bookRepository)
    {
        _categoryRepository = categoryRepository;
        _bookRepository = bookRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _categoryRepository.GetCountAsync() <= 0)
        {
            await _categoryRepository.InsertAsync(
                new Category
                {
                    Name = "Science Fiction",
                    Description = "Books that explore futuristic concepts and advanced technologies.",
                },
                autoSave: true
            );
            await _categoryRepository.InsertAsync(
                new Category
                {
                    Name = "Dystopian",
                    Description = "Books set in an imagined society that is far worse than our own.",
                },
                autoSave: true
            );
        }

        var scienceFictionCategory = await _categoryRepository.FindAsync(c => c.Name == "Science Fiction");
        var dystopianCategory = await _categoryRepository.FindAsync(c => c.Name == "Dystopian");
        
        if (scienceFictionCategory == null || dystopianCategory == null)
        {
            throw new Exception("Required categories not found.");
        }

        if (await _bookRepository.GetCountAsync() <= 0)
        {
            await _bookRepository.InsertAsync(
                new Book
                {
                    Name = "Dune",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(1965, 8, 1),
                    Price = 29.99f,
                    Status = BookStatus.Available,
                    CategoryId = scienceFictionCategory.Id // Set the foreign key
                },
                autoSave: true
            );
            await _bookRepository.InsertAsync(
                new Book
                {
                    Name = "1984",
                    Type = BookType.Dystopia,
                    PublishDate = new DateTime(1949, 6, 8),
                    Price = 19.99f,
                    Status = BookStatus.Available,
                    CategoryId = dystopianCategory.Id // Set the foreign key
                },
                autoSave: true
            );
        }
    }
}
