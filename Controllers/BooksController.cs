using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

public class BooksController : ODataController
{
    private BookStoreContext _db;
    public BooksController(BookStoreContext context)
    {
        _db = context;
        if (context.Books.Count() == 0)
        {
            foreach (var b in DataSource.GetBooks())
            {
                context.Books.Add(b);
                context.Press.Add(b.Press);
            }
            context.SaveChanges();
        }
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_db.Books);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
        return Ok(_db.Books.FirstOrDefault(c => c.Id == key));
    }

    [EnableQuery]
    public IActionResult Get(string ISBN)
    {
        return Ok(_db.Books.FirstOrDefault(x => x.ISBN == ISBN));
    }

    [EnableQuery]
    public IActionResult Post([FromBody]Book book)
    {
        _db.Books.Add(book);
        _db.SaveChanges();
        return Created(book);
    }


}