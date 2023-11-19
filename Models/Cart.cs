using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Highland.Models
{
    public class CartItem
    {
        public Product _product { get; set; }
        public int _quantity { get; set; }
    }
    //Khai báo thuộc tính dòng sản phẩm items trong lớp Cart
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        //Viết hàm lấy sản phẩm bỏ vào giỏ hàng
        public void Add_Product_Cart(Product _pro, int _quan = 1)
        {
            var item = items.FirstOrDefault(s => s._product.ID == _pro.ID);
            if (item == null) //nếu giỏ hàng rỗng thì thêm giỏ hàng mới vào giỏ
                items.Add(new CartItem
                {
                    _product = _pro,
                    _quantity = _quan
                });
            else
                item._quantity += _quan; //tổng số lượng trong giỏ hàng được cộng dồn
        }

        //Viết hàm tính tổng số lượng trong giỏ hàng
        public int Total_quantity()
        {
            return Items.Sum(s => s._quantity);
        }

        //Viết hàm tính thành tiền cho mỗi dòng sản phẩm trong giỏ hàng
        public decimal Total_money()
        {
            var total = items.Sum(s => s._quantity * s._product.PriceProduct);
            return (decimal)total;
        }

        //Viết hàm cập nhập lại số lượng sản phẩm ở mỗi dòng sản phẩm khi khách hàng muốn đặt mua thêm
        public void Update_quantity(int id, int _new_quan)
        {
            var item = items.Find(s => s._product.ID == id);
            if (item != null)
            {
                if (items.Find(s => s._product.QuantityProduct > _new_quan) != null) //nếu số lượng mua nhỏ hơn số lượng tồn
                    item._quantity = _new_quan; //thì chấp nhận số lượng mua
                else item._quantity = 1; //ngược lại, thì số lượng mua trả về 1  
            }


        }

        //Viết hàm xóa sản phẩm trong giỏ hàng
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._product.ID == id);
        }

        //Viết hàm xóa giỏ hàng sau khi Khách hàng thực hiện thanh toán
        public void ClearCart()
        {
            items.Clear();
        }
    }
}