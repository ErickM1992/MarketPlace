using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z_Market.Models;
using Z_Market.ViewModels;


namespace Z_Market.Controllers
{
    public class OrdersController : Controller
    {

        private Z_MarketContext db = new Z_MarketContext();

        // GET: Orders
        public ActionResult NewOrder()
        {

            var OrderView = new OrderView();
            OrderView.Customer = new Customer();
            OrderView.Products = new List<ProductOrder>();
            Session["OrderView"] = OrderView;//Variable de session.


            var List = db.Customers.ToList();
            List.Add(new Customer { CustomerID = 0, FirtsName = "[Seleccione un cliente]" });
            List = List.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(List, "CustomerID", "FullName");
            return View(OrderView);
        }


        [HttpPost]
        public ActionResult NewOrder(OrderView OrderView)
        {
            OrderView = Session["OrderView"] as OrderView;

            var customerID = int.Parse(Request["CustomerID"]);




            //Validando que se seleecione un producto.
            if (customerID == 0)
            {

                var List = db.Customers.ToList();
                List.Add(new Customer { CustomerID = 0, FirtsName = "[Seleccione un cliente]" });
                List = List.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(List, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un Cliente";
                return View(OrderView);

            }


            var customer = db.Customers.Find(customerID);//Buscando el ID del producto que fue solicitado.

            //Validando que se seleecione un producto.
            if (customer == null)
            {

                var List = db.Customers.ToList();
                List.Add(new Customer { CustomerID = 0, FirtsName = "[Seleccione un cliente]" });
                List = List.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(List, "CustomerID", "FullName");
                ViewBag.Error = "Cliente no existe";
                return View(OrderView);

            }


            if (OrderView.Products.Count == 0)
            {

                var List = db.Customers.ToList();
                List.Add(new Customer { CustomerID = 0, FirtsName = "[Seleccione un cliente]" });
                List = List.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(List, "CustomerID", "FullName");
                ViewBag.Error = "Debe ingresar detalle";
                return View(OrderView);

            }



            //Orden transaccional.

            var orderID = 0;


            using (var trasaction = db.Database.BeginTransaction())
            {

                try
                {

                    //Llegado a este apunto todo esta correcto para crear el producto e introducirlo a la base de datos.
                    var order = new Order
                    {
                        CustomerID = customerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.Created
                    };

                    //Agregando y guardando el producto a la base de datos.
                    db.Orders.Add(order);
                    db.SaveChanges();


                    //Seleccionando el ultimo id que fue creado.
                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max();

                    //For each que recorre las cordenes de los productos.
                    foreach (var item in OrderView.Products)
                    {

                        var orderDetail = new OrderDetail
                        {
                            ID = item.ID,
                            Description = item.Description,
                            price = item.price,
                            Quantity = (decimal)item.Quantity,
                            OrderID = orderID

                        };
                        db.OrderDetail.Add(orderDetail);
                        db.SaveChanges();
                    }
                    //Si no hay error confirmamos la trasaccion.
                    trasaction.Commit();
                }
                catch (Exception ex)
                {
                    trasaction.Rollback();
                    ViewBag.Error = "Error: " + ex.Message;
                    return View(OrderView);
                }

               
            }

            ViewBag.Message = String.Format("La orden {0} ha sido grabada con exito.", orderID);

            var ListC = db.Customers.ToList();
            ListC.Add(new Customer { CustomerID = 0, FirtsName = "[Seleccione un cliente]" });
            ListC = ListC.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(ListC, "CustomerID", "FullName");


            OrderView = new OrderView();
            OrderView.Customer = new Customer();
            OrderView.Products = new List<ProductOrder>();
            Session["OrderView"] = OrderView;//Variable de session.

            return View(OrderView) ;
        }



        public ActionResult AddProduct()
        {
            var List = db.Products.ToList();
            List.Add(new Product{ ID = 0, Description = "[Seleccione un producto]" });
            List = List.OrderBy(p => p.Description).ToList();
            ViewBag.ID = new SelectList(List, "ID", "Description");

            return View();
        }



        [HttpPost]
        public ActionResult AddProduct(ProductOrder ProductOrder)
        {
            var OrderView = Session["OrderView"] as OrderView; //Recuperando el objeto de Orderview desde la variable de session.

            var productID = int.Parse(Request["ID"]);//Obteniendo la respues del formulario.

            //Validando que se seleecione un producto.
            if (productID == 0)
            {

                var List = db.Products.ToList();
                List.Add(new Product { ID = 0, Description = "[Seleccione un producto]" });
                List = List.OrderBy(p => p.Description).ToList();
                ViewBag.ID = new SelectList(List, "ID", "Description");
                ViewBag.Error = "Debe seleccionar un producto";
                return View(ProductOrder);

            }


            var product = db.Products.Find(productID);//Buscando el ID del producto que fue solicitado.

            //Validando que se seleecione un producto.
            if (product == null)
            {

                var List = db.Products.ToList();
                List.Add(new Product { ID = 0, Description = "[Seleccione un producto]" });
                List = List.OrderBy(p => p.Description).ToList();
                ViewBag.ID = new SelectList(List, "ID", "Description");
                ViewBag.Error = "El producto seleccionado NO existe";
                return View(ProductOrder);

            }




            ProductOrder = OrderView.Products.Find(p => p.ID == productID);

            if (ProductOrder == null)
            {

                //Creando y llenando objetos de producto.
                ProductOrder = new ProductOrder
                {
                    Description = product.Description,
                    price = product.price,
                    ID = product.ID,
                    Quantity = float.Parse(Request["Quantity"])
                };

                OrderView.Products.Add(ProductOrder);//Agregando un producto existente.

            }
            else
            {
                ProductOrder.Quantity += float.Parse(Request["Quantity"]);
            }

           

            var ListC = db.Customers.ToList();
            ListC.Add(new Customer { CustomerID = 0, FirtsName = "[Seleccione un cliente]" });
            ListC = ListC.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(ListC, "CustomerID", "FullName");

            return View("NewOrder", OrderView);
        }



























        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}