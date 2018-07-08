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

    [EnableQuery] // ใส่ property PageSize ได้
    public IActionResult Get()
    {
        // สามารถใส่ linq ได้
        return Ok(_db.Books);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    { 
        //Example Books(1)
        return Ok(_db.Books.FirstOrDefault(c => c.Id == key));
    }

    [EnableQuery]
    public IActionResult Post([FromBody]Book book) // อันนี้ก็เหมือน WebAPI ทั่วไป
    {
        _db.Books.Add(book);
        _db.SaveChanges();
        return Created(book);
    }


}