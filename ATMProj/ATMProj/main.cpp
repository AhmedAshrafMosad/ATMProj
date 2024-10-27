#include <iostream>

using namespace std;

double balance = 1000;
double deposite = 0;
double withdrow = 0;
int password = 0;
int choice;

void show()
{
    cout<<"******Main******"<<endl;
    cout<<"1-Balance:- \n";
    cout<<"2-Withdrow:- \n";
    cout<<"3-Deposite:- \n";
    cout<<"4-Exit:- \n";
    cout<<"****************"<<endl;
}
void process()
{
    cout<<"Enter you password: ";
    cin>>password;
    do
    {
        if(password==0000)
        {

            cout<<"Enter your choice ([1][2][3][4]): ";
            cin>>choice;
            switch(choice)
            {
            case 1:
                cout<<"Your Balance is: "<<balance<<endl;

                break;
            case 2:
                cout<<"Enter your amount: ";
                cin>>withdrow;
                if(withdrow>balance)
                {
                    cout<<"Sorry your Balance is not Allowed! try again:-\n";

                }
                else
                {
                    balance-=withdrow;
                    cout<<"your Balance Now is: "<<balance<<endl;

                }
                break;
            case 3:
                cout<<"Enter your Amount: ";
                cin>>deposite;
                balance+=deposite;
                cout<<"your Balance Now is: "<<balance<<endl;
                break;
            case 4:
                cout<<"Thanks(-o-)"<<endl;
                break;
            default:
                cout<<"Please Enter right choice....\n";
                break;
            }

        }

        else
        {
            char c;
            cout<<"Wrong Password are you want to try again [y][n]:-";
            cin>>c;
            if(c=='y'||c=='Y')
            {
                cout<<"Enter you password: ";
                cin>>password;
            }
            else if(c=='n'||c=='N')
                choice=4;
        }

    }
    while(choice<4);
}


int main()
{
    show();
    process();

    return 0;
}
