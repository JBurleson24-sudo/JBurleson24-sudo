using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BurlesonJLogonView
{
    internal static class clsHTML
    {
        public static void CreateSalesReport(
            DateTime startDate,
            DateTime endDate,
            string reportTitle)
        {
            DataTable dt =
              clsDatabase.GetSalesByDateRange(
                  startDate,
                  endDate);

            string folder =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.MyDocuments),
                    "VideoGameStore");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string safeTitle =
    reportTitle.Replace(" ", "");

            string fileName =
                Path.Combine(
                    folder,
                    safeTitle + "_" +
                    startDate.ToString("yyyyMMdd") +
                    ".html");

            StringBuilder html =
                new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<title>" + reportTitle + "</title>");

            html.AppendLine("<style>");
            html.AppendLine("body{font-family:Arial;margin:40px;background:#f8f8f8;}");
            html.AppendLine(".report{background:white;padding:30px;border:1px solid #ccc;max-width:1000px;margin:auto;}");
            html.AppendLine("h1,h2{text-align:center;}");
            html.AppendLine("table{border-collapse:collapse;width:100%;margin-top:20px;}");
            html.AppendLine("th,td{border:1px solid #999;padding:8px;}");
            html.AppendLine("th{background:#e6e6e6;}");
            html.AppendLine(".money{text-align:right;}");
            html.AppendLine("</style>");

            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class='report'>");

            html.AppendLine("<h1>Video Game Store</h1>");

            html.AppendLine(
                "<h2>" +
                reportTitle +
                "</h2>");

            if (startDate.Date == endDate.Date)
            {
                html.AppendLine(
                    "<p><strong>Date:</strong> " +
                    startDate.ToString("MM/dd/yyyy") +
                    "</p>");
            }
            else
            {
                html.AppendLine(
                    "<p><strong>Date Range:</strong> " +
                    startDate.ToString("MM/dd/yyyy") +
                    " - " +
                    endDate.ToString("MM/dd/yyyy") +
                    "</p>");
            }

            decimal reportSubtotal = 0m;
            decimal reportDiscount = 0m;
            decimal reportTax = 0m;
            decimal reportTotal = 0m;

            var orderGroups =
                dt.AsEnumerable()
                .GroupBy(r =>
                    Convert.ToInt32(r["OrderID"]));

            foreach (var orderGroup in orderGroups)
            {
                int orderID =
                    orderGroup.Key;

                decimal subtotal = 0m;

                foreach (DataRow row in orderGroup)
                {
                    decimal price =
                        Convert.ToDecimal(
                            row["RetailPrice"]);

                    int quantity =
                        Convert.ToInt32(
                            row["Quantity"]);

                    subtotal +=
                        price * quantity;
                }

                decimal discountAmount = 0m;

                DataRow firstRow =
                    orderGroup.First();

                if (firstRow["OrderDiscountID"] != DBNull.Value)
                {
                    int discountID =
                        Convert.ToInt32(
                            firstRow["OrderDiscountID"]);

                    DataRow discount =
                        clsDatabase.GetDiscountByIDForReport(
                            discountID);

                    if (discount != null)
                    {
                        int type =
                            Convert.ToInt32(
                                discount["DiscountType"]);

                        if (type == 0 &&
                            discount["DiscountPercentage"] != DBNull.Value)
                        {
                            discountAmount =
                                subtotal *
                                Convert.ToDecimal(
                                    discount["DiscountPercentage"]);
                        }
                        else if (
                            type == 1 &&
                            discount["DiscountDollarAmount"] != DBNull.Value)
                        {
                            discountAmount =
                                Convert.ToDecimal(
                                    discount["DiscountDollarAmount"]);
                        }
                    }
                }
                else
                {
                    foreach (DataRow row in orderGroup)
                    {
                        if (row["ItemDiscountID"] == DBNull.Value)
                            continue;

                        int discountID =
                            Convert.ToInt32(
                                row["ItemDiscountID"]);

                        DataRow discount =
                            clsDatabase.GetDiscountByIDForReport(
                                discountID);

                        if (discount == null)
                            continue;

                        decimal lineTotal =
                            Convert.ToDecimal(
                                row["RetailPrice"]) *
                            Convert.ToInt32(
                                row["Quantity"]);

                        int type =
                            Convert.ToInt32(
                                discount["DiscountType"]);

                        if (type == 0 &&
                            discount["DiscountPercentage"] != DBNull.Value)
                        {
                            discountAmount +=
                                lineTotal *
                                Convert.ToDecimal(
                                    discount["DiscountPercentage"]);
                        }
                        else if (
                            type == 1 &&
                            discount["DiscountDollarAmount"] != DBNull.Value)
                        {
                            decimal amount =
                                Convert.ToDecimal(
                                    discount["DiscountDollarAmount"]);

                            if (amount > lineTotal)
                                amount = lineTotal;

                            discountAmount += amount;
                        }
                    }
                }

                if (discountAmount > subtotal)
                {
                    discountAmount = subtotal;
                }

                decimal discountedSubtotal =
                    subtotal - discountAmount;

                decimal tax =
                    discountedSubtotal * 0.0825m;

                decimal total =
                    discountedSubtotal + tax;

                reportSubtotal += subtotal;
                reportDiscount += discountAmount;
                reportTax += tax;
                reportTotal += total;

                html.AppendLine("<table>");

                html.AppendLine(
                    "<tr><th colspan='4'>Order #" +
                    orderID +
                    "</th></tr>");

                html.AppendLine(
                    "<tr>" +
                    "<th>Item</th>" +
                    "<th>Price</th>" +
                    "<th>Qty</th>" +
                    "<th>Total</th>" +
                    "</tr>");

                foreach (DataRow row in orderGroup)
                {
                    decimal price =
                        Convert.ToDecimal(
                            row["RetailPrice"]);

                    int quantity =
                        Convert.ToInt32(
                            row["Quantity"]);

                    decimal lineTotal =
                        price * quantity;

                    html.AppendLine("<tr>");

                    html.AppendLine(
                        "<td>" +
                        row["ItemName"] +
                        "</td>");

                    html.AppendLine(
                        "<td class='money'>" +
                        price.ToString("C") +
                        "</td>");

                    html.AppendLine(
                        "<td>" +
                        quantity +
                        "</td>");

                    html.AppendLine(
                        "<td class='money'>" +
                        lineTotal.ToString("C") +
                        "</td>");

                    html.AppendLine("</tr>");
                }

                html.AppendLine(
                    "<tr><td colspan='3'><strong>Subtotal</strong></td>" +
                    "<td class='money'>" +
                    subtotal.ToString("C") +
                    "</td></tr>");

                html.AppendLine(
                    "<tr><td colspan='3'><strong>Discount</strong></td>" +
                    "<td class='money'>-" +
                    discountAmount.ToString("C") +
                    "</td></tr>");

                html.AppendLine(
                    "<tr><td colspan='3'><strong>Tax</strong></td>" +
                    "<td class='money'>" +
                    tax.ToString("C") +
                    "</td></tr>");

                html.AppendLine(
                    "<tr><td colspan='3'><strong>Total</strong></td>" +
                    "<td class='money'><strong>" +
                    total.ToString("C") +
                    "</strong></td></tr>");

                html.AppendLine("</table>");
            }

            html.AppendLine("<hr>");

            html.AppendLine(
                "<h3>Sales Subtotal: " +
                reportSubtotal.ToString("C") +
                "</h3>");

            html.AppendLine(
                "<h3>Total Discounts: -" +
                reportDiscount.ToString("C") +
                "</h3>");

            html.AppendLine(
                "<h3>Total Tax: " +
                reportTax.ToString("C") +
                "</h3>");

            html.AppendLine(
                "<h2>Total Sales: " +
                reportTotal.ToString("C") +
                "</h2>");

            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(
                fileName,
                html.ToString());

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true
                });
        }
        public static void CreateReceipt(clsCart cart,int orderID,int personID)
        {
            string folder =
                 Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                   "VideoGameStore");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string fileName =
            Path.Combine(folder,
            "Receipt_" + orderID + ".html");
            StringBuilder html = new StringBuilder();
            DataRow customer = clsDatabase.GetCustomerInfo(personID);

            string customerName = "";
            string phone = "";

            if (customer != null)
            {
                customerName =
                    customer["NameFirst"] + " " +
                    customer["NameLast"];

                phone =
                    customer["PhonePrimary"].ToString();
            }

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<title>Receipt</title>");

            html.AppendLine("<style>");
            html.AppendLine("body{font-family:Arial;margin:40px;background:#f8f8f8;}");
            html.AppendLine(".receipt{background:white;padding:30px;border:1px solid #ccc;max-width:800px;margin:auto;}");
            html.AppendLine("h1,h2{text-align:center;}");
            html.AppendLine("table{border-collapse:collapse;width:100%;margin-top:20px;}");
            html.AppendLine("th,td{border:1px solid #999;padding:8px;}");
            html.AppendLine("th{background:#e6e6e6;}");
            html.AppendLine(".money{text-align:right;}");
            html.AppendLine("</style>");

            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class='receipt'>");

            html.AppendLine("<h1>Video Game Store</h1>");
            html.AppendLine("<h2>Sales Receipt</h2>");
            html.AppendLine("<p><strong>Customer:</strong> " + customerName + "</p>");
            html.AppendLine("<p><strong>Phone:</strong> " + phone + "</p>");

            html.AppendLine("<p><strong>Order #:</strong> " + orderID + "</p>");
            html.AppendLine("<p><strong>Date:</strong> " + DateTime.Now.ToString() + "</p>");

            html.AppendLine("<table>");

            html.AppendLine("<tr>");
            html.AppendLine("<th>Game</th>");
            html.AppendLine("<th>Price</th>");
            html.AppendLine("<th>Qty</th>");
            html.AppendLine("<th>Total</th>");
            html.AppendLine("</tr>");

            foreach (clsCartItem item in cart.Items)
            {
                html.AppendLine("<tr>");

                html.AppendLine("<td>" + item.ItemName + "</td>");

                html.AppendLine("<td>" +
                    item.Price.ToString("C") +
                    "</td>");

                html.AppendLine("<td>" +
                    item.Quantity +
                    "</td>");

                html.AppendLine("<td>" +
                    item.Total.ToString("C") +
                    "</td>");

                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");

            html.AppendLine("<br>");

            html.AppendLine("<h3>Subtotal: " +
                cart.Subtotal.ToString("C") +
                "</h3>");

            if (cart.AppliedDiscount != null)
            {
                string discountLabel;

                if (cart.AppliedDiscount.DiscountType == 0)
                {
                    discountLabel =
                        "Discount (" +
                        (cart.AppliedDiscount.DiscountPercentage.Value * 100)
                            .ToString("0") +
                        "%)";
                }
                else
                {
                    discountLabel = "Discount";
                }

                html.AppendLine(
                    "<h3>" +
                    discountLabel +
                    ": -" +
                    cart.DiscountAmount.ToString("C") +
                    "</h3>");

                html.AppendLine(
                    "<h3>Discounted Subtotal: " +
                    cart.DiscountedSubtotal.ToString("C") +
                    "</h3>");
            }

            html.AppendLine("<h3>Tax: " +
                cart.Tax.ToString("C") +
                "</h3>");

            html.AppendLine("<h2>Total: " +
                cart.GrandTotal.ToString("C") +
                "</h2>");

            html.AppendLine("<p>Thank you for your business!</p>");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(fileName, html.ToString());

            Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true
            });
        }
        public static void CreateInventoryReport(
    int reportType,
    string reportTitle)
        {
            DataTable dt =
                clsDatabase.GetInventoryReport(
                    reportType);

            string folder =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.MyDocuments),
                    "VideoGameStore");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string safeTitle =
                reportTitle.Replace(" ", "");

            string fileName =
                Path.Combine(
                    folder,
                    safeTitle + ".html");

            StringBuilder html =
                new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");

            html.AppendLine(
                "<title>" +
                reportTitle +
                "</title>");

            html.AppendLine("<style>");
            html.AppendLine(
                "body{font-family:Arial;margin:40px;background:#f8f8f8;}");

            html.AppendLine(
                ".report{background:white;padding:30px;border:1px solid #ccc;max-width:1100px;margin:auto;}");

            html.AppendLine(
                "h1,h2{text-align:center;}");

            html.AppendLine(
                "table{border-collapse:collapse;width:100%;margin-top:20px;}");

            html.AppendLine(
                "th,td{border:1px solid #999;padding:8px;}");

            html.AppendLine(
                "th{background:#e6e6e6;}");

            html.AppendLine(
                ".money{text-align:right;}");

            html.AppendLine("</style>");

            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class='report'>");

            html.AppendLine(
                "<h1>Video Game Store</h1>");

            html.AppendLine(
                "<h2>" +
                reportTitle +
                "</h2>");

            html.AppendLine(
                "<p><strong>Generated:</strong> " +
                DateTime.Now.ToString() +
                "</p>");

            html.AppendLine("<table>");

            html.AppendLine(
                "<tr>" +
                "<th>ID</th>" +
                "<th>Name</th>" +
                "<th>Cost</th>" +
                "<th>Price</th>" +
                "<th>Quantity On Hand</th>" +
                "<th>Restock Threshold</th>" +
                "<th>Available</th>" +
                "</tr>");

            foreach (DataRow row in dt.Rows)
            {
                html.AppendLine("<tr>");

                html.AppendLine(
                    "<td>" +
                    row["InventoryID"] +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    row["ItemName"] +
                    "</td>");

                html.AppendLine(
                    "<td class='money'>" +
                    Convert.ToDecimal(
                        row["Cost"]).ToString("C") +
                    "</td>");

                html.AppendLine(
                    "<td class='money'>" +
                    Convert.ToDecimal(
                        row["RetailPrice"]).ToString("C") +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    row["Quantity"] +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    row["RestockThreshold"] +
                    "</td>");

                html.AppendLine(
                    "<td>" +
                    row["Available"] +
                    "</td>");

                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");

            html.AppendLine(
                "<p><strong>Total Items:</strong> " +
                dt.Rows.Count +
                "</p>");

            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(
                fileName,
                html.ToString());

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true
                });
        }
        public static void CreatePOSReceipt(
    clsCart cart,
    int orderID,
    int customerPersonID,
    int managerPersonID)
        {
            string folder =
                Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.MyDocuments),
                    "VideoGameStore");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName =
                Path.Combine(
                    folder,
                    "POSReceipt_" + orderID + ".html");

            StringBuilder html = new StringBuilder();

            DataRow customer =
                clsDatabase.GetCustomerInfo(customerPersonID);

            DataRow manager =
                clsDatabase.GetCustomerInfo(managerPersonID);

            string customerName = "";
            string phone = "";
            string managerName = "";

            if (customer != null)
            {
                customerName =
                    customer["NameFirst"] + " " +
                    customer["NameLast"];

                phone =
                    customer["PhonePrimary"].ToString();
            }

            if (manager != null)
            {
                managerName =
                    manager["NameFirst"] + " " +
                    manager["NameLast"];
            }

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<title>POS Receipt</title>");

            html.AppendLine("<style>");
            html.AppendLine(
                "body{font-family:Arial;margin:40px;background:#f8f8f8;}");
            html.AppendLine(
                ".receipt{background:white;padding:30px;border:1px solid #ccc;max-width:800px;margin:auto;}");
            html.AppendLine("h1,h2{text-align:center;}");
            html.AppendLine(
                "table{border-collapse:collapse;width:100%;margin-top:20px;}");
            html.AppendLine(
                "th,td{border:1px solid #999;padding:8px;}");
            html.AppendLine("th{background:#e6e6e6;}");
            html.AppendLine(".money{text-align:right;}");
            html.AppendLine("</style>");

            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class='receipt'>");

            html.AppendLine("<h1>Video Game Store</h1>");
            html.AppendLine("<h2>Sales Receipt</h2>");

            html.AppendLine(
                "<p><strong>Customer:</strong> " +
                customerName + "</p>");

            html.AppendLine(
                "<p><strong>Phone:</strong> " +
                phone + "</p>");

            html.AppendLine(
                "<p><strong>Employee:</strong> " +
                managerName + "</p>");

            html.AppendLine(
                "<p><strong>Sales Receipt #:</strong> " +
                orderID + "</p>");

            html.AppendLine(
                "<p><strong>Date:</strong> " +
                DateTime.Now.ToString() + "</p>");

            html.AppendLine("<table>");

            html.AppendLine("<tr>");
            html.AppendLine("<th>Game</th>");
            html.AppendLine("<th>Price</th>");
            html.AppendLine("<th>Qty</th>");
            html.AppendLine("<th>Total</th>");
            html.AppendLine("</tr>");

            foreach (clsCartItem item in cart.Items)
            {
                html.AppendLine("<tr>");

                html.AppendLine(
                    "<td>" + item.ItemName + "</td>");

                html.AppendLine(
                    "<td>" + item.Price.ToString("C") + "</td>");

                html.AppendLine(
                    "<td>" + item.Quantity + "</td>");

                html.AppendLine(
                    "<td>" + item.Total.ToString("C") + "</td>");

                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");
            html.AppendLine("<br>");

            html.AppendLine(
                "<h3>Subtotal: " +
                cart.Subtotal.ToString("C") +
                "</h3>");

            if (cart.AppliedDiscount != null)
            {
                string discountLabel;

                if (cart.AppliedDiscount.DiscountType == 0)
                {
                    discountLabel =
                        "Discount (" +
                        (cart.AppliedDiscount.DiscountPercentage.Value * 100)
                            .ToString("0") +
                        "%)";
                }
                else
                {
                    discountLabel = "Discount";
                }

                html.AppendLine(
                    "<h3>" +
                    discountLabel +
                    ": -" +
                    cart.DiscountAmount.ToString("C") +
                    "</h3>");

                html.AppendLine(
                    "<h3>Discounted Subtotal: " +
                    cart.DiscountedSubtotal.ToString("C") +
                    "</h3>");
            }

            html.AppendLine(
                "<h3>Tax (8.25%): " +
                cart.Tax.ToString("C") +
                "</h3>");

            html.AppendLine(
                "<h2>Total: " +
                cart.GrandTotal.ToString("C") +
                "</h2>");

            html.AppendLine(
                "<p>Thank you for your business!</p>");

            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(
                fileName,
                html.ToString());

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = true
                });
        }
    }
}
