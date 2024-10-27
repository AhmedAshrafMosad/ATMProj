#ifndef PRODUCTSTACK_H
#define PRODUCTSTACK_H
#include <Stack.h>
template<typename T>
class ProductStack
{
private:
    int product_id;
    Stack<T>product_orders;
public:
    ProductStack(int product_id=rand()%N+1):product_id(product_id) {}
    void add_operation(T operation)
    {
        product_orders.push(operation);
    }
    void pop_operation(T operation)
    {
        product_orders.pop();
    }
    int number_of_operations()
    {
        return product_orders.size();
    }
    bool empty()
    {
        return product_orders.empty();
    }
    friend ostream & operator<<(ostream &output, ProductStack<T> product)
    {
        output<<product.product_id;
        return output;
    }
    void read()
    {
        cout << "Enter the id of this product"<<endl;
        cin >> product_id;

        cout << "Enter the number of operations of this product"<<endl;
        int operations;
        cin >> operations;

        cout << "Enter the ids of the operations:"<<endl;
        while (operations--)
        {
            T order;
            cin >> order;
            product_orders.push(order);
        }
    }
};

#endif // PRODUCTSTACK_H
