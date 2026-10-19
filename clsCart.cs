using System;
using System.ComponentModel;
using System.Linq;

namespace BurlesonJLogonView
{
    public class clsCart
    {
        public BindingList<clsCartItem> Items { get; } =
            new BindingList<clsCartItem>();

        public clsDiscount AppliedDiscount { get; set; }

        public void AddItem(clsCartItem item)
        {
            clsCartItem existing =
                Items.FirstOrDefault(i =>
                    i.InventoryID == item.InventoryID);

            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public decimal Subtotal
        {
            get
            {
                return Items.Sum(i => i.Total);
            }
        }

        public decimal DiscountAmount
        {
            get
            {
                if (AppliedDiscount == null)
                    return 0m;

                decimal discountAmount = 0m;

                // Cart-level discount
                if (AppliedDiscount.DiscountLevel == 0)
                {
                    // Percentage discount
                    if (AppliedDiscount.DiscountType == 0 &&
                        AppliedDiscount.DiscountPercentage.HasValue)
                    {
                        discountAmount =
                            Subtotal *
                            AppliedDiscount.DiscountPercentage.Value;
                    }

                    // Dollar discount
                    else if (AppliedDiscount.DiscountType == 1 &&
                             AppliedDiscount.DiscountDollarAmount.HasValue)
                    {
                        discountAmount =
                            AppliedDiscount.DiscountDollarAmount.Value;
                    }
                }

                // Item-level discount
                else if (AppliedDiscount.DiscountLevel == 1 &&
                         AppliedDiscount.InventoryID.HasValue)
                {
                    clsCartItem discountedItem =
                        Items.FirstOrDefault(i =>
                            i.InventoryID ==
                            AppliedDiscount.InventoryID.Value);

                    if (discountedItem == null)
                        return 0m;

                    // Item percentage discount
                    if (AppliedDiscount.DiscountType == 0 &&
                        AppliedDiscount.DiscountPercentage.HasValue)
                    {
                        discountAmount =
                            discountedItem.Total *
                            AppliedDiscount.DiscountPercentage.Value;
                    }

                    // Item dollar discount
                    else if (AppliedDiscount.DiscountType == 1 &&
                             AppliedDiscount.DiscountDollarAmount.HasValue)
                    {
                        discountAmount =
                            AppliedDiscount.DiscountDollarAmount.Value;

                        // Do not discount more than the item's line total
                        if (discountAmount > discountedItem.Total)
                        {
                            discountAmount = discountedItem.Total;
                        }
                    }
                }

                // Never allow the total discount to exceed the subtotal
                if (discountAmount > Subtotal)
                {
                    discountAmount = Subtotal;
                }

                return discountAmount;
            }
        }

        public decimal DiscountedSubtotal
        {
            get
            {
                return Subtotal - DiscountAmount;
            }
        }

        public decimal Tax
        {
            get
            {
                return DiscountedSubtotal * 0.0825m;
            }
        }

        public decimal GrandTotal
        {
            get
            {
                return DiscountedSubtotal + Tax;
            }
        }

        public void RemoveItem(int inventoryID)
        {
            clsCartItem item =
                Items.FirstOrDefault(i =>
                    i.InventoryID == inventoryID);

            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void UpdateQuantity(int inventoryID, int quantity)
        {
            clsCartItem item =
                Items.FirstOrDefault(i =>
                    i.InventoryID == inventoryID);

            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public int GetQuantityInCart(int inventoryID)
        {
            clsCartItem item =
                Items.FirstOrDefault(i =>
                    i.InventoryID == inventoryID);

            if (item == null)
                return 0;

            return item.Quantity;
        }
    }
}
