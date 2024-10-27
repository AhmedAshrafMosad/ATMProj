#ifndef SUBLINKEDLIST_H
#define SUBLINKEDLIST_H
#include <bits/stdc++.h>
using namespace std;
const int N=105, block_size=4;
template<typename T>
struct Node
{
    T data[block_size];
    Node* next;
    Node():next(nullptr) {}
    Node(T val)
    {
        data[0]=val;
        next=nullptr;
    }
};
template<typename T>
class SubLinkedList
{
private:
    Node<T>*head=nullptr;
    int full_blocks=-2;
    int start_idx=0;
    int end_idx=block_size;
public:
    int size()
    {
        return full_blocks*block_size+block_size-start_idx+end_idx;
    }
    bool empty()
    {
        if(size()==0)
            return true;
        return false;
    }
    T front()
    {
        if(empty())
            assert(!("Your data structure is empty"));
        return head->data[start_idx];
    }
    T back()
    {
        if(empty())
            assert(!("Your data structure is empty"));
        Node<T>*tail=head;
        while(tail->next!=nullptr)
            tail=tail->next;
        return tail->data[end_idx-1];
    }
    void pop_front()
    {
        if(empty())
            assert(false);
        if(start_idx+1==block_size)
        {
            Node<T>*nxt=head->next;
            start_idx=0;
            full_blocks--;
            delete head;
            head=nxt;
            return;
        }
        start_idx++;
    }
    void print()
    {
        Node<T>* cur = head;
        cout<<"The ids of the products are: "<<endl;
        while (cur != nullptr)
        {
            int left = 0, right = block_size;
            if (cur == head)
                left = start_idx;
            if (cur->next == nullptr)
                right = end_idx;
            for (int idx = left; idx < right; idx++)
                cout << cur->data[idx] << ' ';
            cur = cur->next;
        }
        cout << '\n';
    }

    void pop_back()
    {
        if(empty())
            assert(false);
        Node<T>*tail=head,*prev=nullptr;
        while(tail->next!=nullptr)
            prev=tail,tail=tail->next;
        --end_idx;
        if(end_idx==0)
        {
            full_blocks--;
            end_idx=block_size;
            delete tail;
            tail=prev;
        }
    }
    void push(T val)
    {
        if(empty())
        {
            end_idx=1;
            full_blocks++;
            head=new Node<T>(val);
            return;
        }
        Node<T>*tail=head;
        while(tail->next!=nullptr)
            tail=tail->next;
        if(end_idx==block_size)
        {
            full_blocks++;
            end_idx=1;
            tail->next=new Node<T>(val);
            tail=tail->next;
            return;
        }
        tail->data[end_idx++]=val;
    }

    };

#endif // SUBLINKEDLIST_H
