using BookStore.Models.Cart;
using BookStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IBookService bookService;

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "PNBS";

        public HomeController(ICategoryService categoryService, IBookService bookService)
        {
            this.categoryService = categoryService;
            this.bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await categoryService.GetCategories());
        }


        /// Thêm sản phẩm vào cart
        [Route("/Home/AddToCart/{bookId:int}", Name = "Index")]
        public async Task<IActionResult> AddToCart(int bookId)
        {

            var book = await bookService.GetBookById(bookId);
            if (book == null)
                return NotFound("Không có sản phẩm");

            var bookItem = new BookItem()
            {
                BookId = book.BookId,
                Quantity = book.Quantity,
                Authors = book.Authors,
                BookName = book.BookName,
                CategoryId = book.CategoryId,
                Description = book.Description,
                Photo = book.Photo,
                Price = book.Price,
                PublishYear = book.PublishYear
            };
            // Xử lý đưa vào Cart ...

            var cart = GetCartItems();
            var cartitem = cart.Find(b => b.Book.BookId == bookId);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { Quantity = 1, Book = bookItem });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction("Cart");
        }

        // Hiện thị giỏ hàng
        [Route("Home/Cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("Home/UpdateCart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int bookId, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(b => b.Book.BookId == bookId);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        /// xóa item trong cart
        [Route("/Home/RemoveCart/{bookId:int}")]
        public IActionResult RemoveCart([FromRoute] int bookId)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Book.BookId == bookId);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
