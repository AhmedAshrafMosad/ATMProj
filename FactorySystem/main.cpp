#include <bits/stdc++.h>
#include <SmartFactoryQueue.h>
#include <ProductStack.h>
using namespace std;
int main()
{
    SmartFactoryQueue<ProductStack<int>> smart_factory;
    int ch;
    do
    {
        cout<<"\t\t\t\tSmart Factory Management System"<<endl;
        cout<<"::::::::::::::: PROGRAM MENU :::::::::::::::"<<endl;
        cout<<"0. Exit\n";
        cout<<"1. Add a New Product"<<endl;
        cout<<"2. Display the front product"<<endl;
        cout<<"3. Delete the front product"<<endl;
        cout<<"4. Display All IDs of Saved Products"<<endl;
        cout<<"Enter Your Choice  "<<endl;
        cin>>ch;
        system("cls");
        cout<<"---------------------------------------"<<endl<<endl<<endl;
        switch(ch)
        {
        case 1:
        {
            ProductStack<int> product;
            product.read();
            smart_factory.add_product(product);
            break;
        }
        case 2:
        {
            cout << smart_factory.print_front() << '\n';
            break;
        }
        case 3:
        {
            smart_factory.pop_product();
            break;
        }
        case 4:
        {
            smart_factory.print_all();
            break;
        }
        }
    }
    while(ch);
    return 0 ;
}
