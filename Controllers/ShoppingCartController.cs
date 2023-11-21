using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Highland.Models;

namespace Highland.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBQLHiglandEntities database = new DBQLHiglandEntities();
        // GET: ShoppingCart
        public ActionResult ShowCart()
        {

            if (Session["Cart"] == null)
                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }

        //Tạo mới giỏ hàng
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        //Thêm product mới vào giỏ hàng
        public ActionResult AddToCart(int id)
        {
            var _pro = database.Products.SingleOrDefault(s => s.ID == id); //lấy pro theo ID 
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        //Cập nhận lại số lượng san phẩm và tính lại tiền
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        //Xóa dòng sản phẩm trong giỏ hàng
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        //Tổng số lượng sản phẩm có trong giỏ hàng
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }

        //Xử lý số lượng tồn trong bảng Product
        //public ActionResult CheckOut(FormCollection form)
        //{
        //    try
        //    {
        //        Cart cart = Session["Cart"] as Cart;
        //        OrderPro _order = new OrderPro(); //Bảng hóa đơn sản phẩm
        //        _order.DateOrder = DateTime.Now;
        //        _order.AddressDelivery = form["AddressDelivery"];
        //        _order.IDCus = int.Parse(form["CodeCustomer"]);
        //        database.OrderProes.Add(_order);
        //        foreach (var item in cart.Items)
        //        {
        //            OrderDetail _order_detail = new OrderDetail(); //Lưu dòng sản phẩm vào bảng chi tiết hóa đơn
        //            _order_detail.IDOrder = _order.IDOrder;
        //            _order_detail.IDProduct = item._product.IDProduct;
        //            _order_detail.UnitPrice = (double)item._product.PriceProduct;
        //            _order_detail.Quantity = item._quantity;
        //            database.OrderDetails.Add(_order_detail);

        //            //xử lý cập nhập lại số lượng tồn trong bảng Product
        //            foreach (var p in database.Products.Where(s => s.IDProduct == _order_detail.IDProduct)) //Lấy ID Product đang có trong giỏ hàng
        //            {
        //                var update_quan_pro = p.QuantityProduct - item._quantity; //số lượng tồn mới=số lượng tồn-số lượng đã mua
        //                p.QuantityProduct = update_quan_pro; //thực hiện cập nhập lại số lượng tồn cho cột Quantity của bảng Product
        //            }
        //        }
        //        database.SaveChanges();
        //        cart.ClearCart();
        //        return RedirectToAction("CheckOut_Success", "ShoppingCart");
        //    }
        //    catch
        //    {
        //        return Content("Error checkout. Please check infomation of Customer...Thanks");
        //    }
        //}
        public ActionResult CheckOutForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOutForm(Customer cus)
        {
            try
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                database.Customers.Add(cus);
                database.SaveChanges();
                int cusID = cus.ID;
                Session["IDCus"] = cusID;
                return RedirectToAction("CheckOut", "ShoppingCart", new { cusID = cusID });
            }
            catch
            {
                return Content("Error with saving data.");
            }
        }


        public ActionResult CheckOut(int cusID)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                var cus = database.Customers.Where(s => s.ID == cusID).FirstOrDefault();
                OrderPro _order = new OrderPro();
                _order.DateOrder = DateTime.Now;
                _order.AddressCus = cus.AddressCus;
                _order.IDCus = cus.ID;
                _order.DeliveryInfor = cus.PhoneCus;
                
                database.OrderProes.Add(_order);

                foreach(var item in cart.Items)
                {
                    OrderDetail _order_Detail = new OrderDetail();
                    _order_Detail.IDOrder = _order.ID; //lấy id trong order lưu vào order Detail
                    _order_Detail.IDProduct = item._product.ID;
                    _order_Detail.Quantity = item._quantity;
                    database.OrderDetails.Add(_order_Detail);
                }

                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Error CheckOut");
            }
        }
        
        public ActionResult CheckOut_Success()
        {
            return View();
        }
    }
}