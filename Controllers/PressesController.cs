using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

public class PressesController : ODataController
{
    private BookStoreContext _db;
    public PressesController(BookStoreContext context)
    {
        _db = context;
        if (context.Press.Count() == 0)
        {
            _db.Press.Add(new Press() { Id = 1, Name = "Maibok1", Category = Category.Book });
            _db.Press.Add(new Press() { Id = 2, Name = "Maibok2", Category = Category.EBook });
            context.SaveChanges();
        }
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_db.Press);
    }

    [EnableQuery]
    public IActionResult Get(int key)
    {
        return Ok(_db.Press.FirstOrDefault(c => c.Id == key));
    }

    [EnableQuery]
    public IActionResult Post([FromBody]Press press)
    {
        _db.Press.Add(press);
        _db.SaveChanges();
        return Created(press);
    }


}