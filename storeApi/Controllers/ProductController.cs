using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using storeApi.Data;
using storeApi.Entities;

namespace storeApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly StoreContext ctx;
    public ProductController(StoreContext _ctx)
    {
      this.ctx = _ctx;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
      return await ctx.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      return await ctx.Products.FindAsync(id);
    }

  }
}