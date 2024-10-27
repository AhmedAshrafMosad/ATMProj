#ifndef SMARTFACTORYQUEUE_H
#define SMARTFACTORYQUEUE_H
#include <Queue.h>
#include <ProductStack.h>
template<typename T>
class SmartFactoryQueue
{
private:
    Queue<T>smart_factory;
public:
    void add_product(T product)
    {
        smart_factory.push(product);
    }
    T print_front()
    {
        return smart_factory.front();
    }
    void pop_product()
    {
        smart_factory.pop();
    }
    int size()
    {
        return smart_factory.size();
    };
    void print_all()
    {
        smart_factory.print();
    }
    bool empty()
    {
        return smart_factory.empty();
    }
};

#endif // SMARTFACTORYQUEUE_H
