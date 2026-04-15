using System.Collections.Generic;

public interface IOrderService
{
    Order AddOrder(RecipeData recipe);
    void CompleteOrder(Order order);
    List<Order> GetActiveOrders();
    List<RecipeData> GetActiveRecipes();
}
