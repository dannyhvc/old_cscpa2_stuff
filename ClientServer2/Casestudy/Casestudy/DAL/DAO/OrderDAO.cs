using Casestudy.DAL.DomainClasses;
using Casestudy.Helpers;

namespace Casestudy.DAL.DAO
{
    public class OrderDAO
    {
        private readonly AppDbContext _db;
        public OrderDAO(AppDbContext ctx)
        {
            _db = ctx;
        }

        public async Task<int> AddOrder(int customerId, OrderItemHelper[] items)
        {
            int orderId = -1;
            // we need a transaction as multiple entities involved
            using (var _trans = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    Order order = new();
                    order.CustomerId = customerId;
                    order.OrderDate = DateTime.Now;
                    order.OrderAmount = 0;

                    // calculate the totals and then add the tray row to the table
                    ProductDAO pdao = new(_db);
                    foreach (OrderItemHelper item in items)
                    {
                        Product product = await pdao.GetById(item.Id!);
                        OrderLineItem orderLineItem = new OrderLineItem();

                        if (item.Qty <= item.Item!.QtyOnHand)
                        {
                            /**
                             * 1. Enough stock (qty ordered < QtyOnHand)
                             *      a. Decrease the QtyOnHand in the products table by Qty
                             *      b. QtySold = Qty, QtyOrdered = Qty, QtyBackOrdered = 0 in the line itemstable
                             */
                            item.Item!.QtyOnHand -= item.Qty;
                            orderLineItem.QtySold = item.Qty;
                            orderLineItem.QtyOrdered = item.Qty;
                            orderLineItem.QtyBackOrdered = 0;
                        }
                        else
                        {
                            /**
                             * 2. Not enough stock (qty ordered > QtyOnHand)
                             *       a. Decrease the QtyOnHand to 0 in the products table
                             *       b. Increase the QtyOnBackOrdered by the difference between Qty and
                             *       QtyOnHand in the products table
                             *       c. QtySold = QtyOnHand, QtyOrdered = Qty, QtyBackOrdered = Qty -
                             *       QtyOnHand
                             */
                            item.Item!.QtyOnBackOrder = item.Qty - item.Item!.QtyOnHand;

                            orderLineItem.QtySold = item.Item!.QtyOnHand;
                            orderLineItem.QtyOrdered = item.Qty;
                            orderLineItem.QtyBackOrdered = item.Item!.QtyOnBackOrder;
                            item.Item!.QtyOnHand = 0;
                        }//if else
                        order.OrderAmount += item.Item!.CostPrice;
                        orderLineItem.OrderId = order.Id;
                        orderLineItem.SellingPrice = item.Item.CostPrice;

                        // db updating and addition
                        _db.Entry(product).CurrentValues.SetValues(item.Item!); // updating product quantities
                        //await _db.OrderLineItems!.AddAsync(orderLineItem); // adding order line transaction info NOT WORKING!
                        await _db.SaveChangesAsync(); // saving
                    }// foreach

                    await _db.Orders!.AddAsync(order);
                    await _db.SaveChangesAsync();

                    await _trans.CommitAsync();
                    orderId = order.Id;
                }//try
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await _trans.RollbackAsync();
                }
            }
            return orderId;
        }
    }
}
