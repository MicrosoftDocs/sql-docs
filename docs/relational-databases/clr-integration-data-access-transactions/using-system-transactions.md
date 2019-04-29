---
title: "Using System.Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "TransactionScope class"
  - "Dispose method"
  - "System.Transactions namespace"
ms.assetid: 79656ce5-ce46-4c5e-9540-cf9869bd774b
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Using System.Transactions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **System.Transactions** namespace provides a transaction framework that is fully integrated with ADO.NET and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] common language runtime (CLR) integration. The **System.Transactions.TransactionScope** class makes a code block transactional by implicitly enlisting connections in a distributed transaction. You must call the **Complete** method at the end of the code block marked by the **TransactionScope**. The **Dispose** method is invoked when program execution leaves a code block, causing the transaction to be discontinued if the **Complete** method is not called. If an exception has been thrown that causes the code to leave scope, the transaction is considered to be discontinued.  
  
 We recommend that you employ a **using** block to ensure that the **Dispose** method is called on the **TransactionScope** object when the **using** block is exited. Failure to commit or roll back pending transactions can seriously degrade performance because the default time-out for the **TransactionScope** is one minute. If you do not use a **using** statement, you must perform all work in a **Try** block and explicitly call the **Dispose** method in the **Finally** block.  
  
 If an exception occurs within the **TransactionScope**, the transaction is marked as inconsistent and is abandoned. It is rolled back when the **TransactionScope** is disposed. If no exception occurs, participating transactions commit.  
  
 **TransactionScope** should be used only when local and remote data sources or external resource managers are being accessed. This is because **TransactionScope** always causes transactions to promote, even if it is being used only within a context connection.  
  
> [!NOTE]  
>  The **TransactionScope** class creates a transaction with a **System.Transactions.Transaction.IsolationLevel** of **Serializable** by default. Depending on your application, you might want to consider lowering the isolation level to avoid high contention in your application.  
  
> [!NOTE]  
>  We recommend that you perform only updates, inserts, and deletes within distributed transactions against remote servers because they consume significant database resources. If the operation is going to be performed on the local server, a distributed transaction is not necessary and a local transaction will suffice. SELECT statements may lock database resources unnecessarily, and in some scenarios it may be necessary to use transactions for selects. Any non-database work should be done outside of the scope of the transaction, unless it involves other transacted resource managers. Although an exception within the scope of the transaction prevents the transaction from committing, the **TransactionScope** class has no provision for rolling back any changes your code has made outside of the scope of the transaction itself. If you need to take some action when the transaction is rolled back, you must write your own implementation of the **System.Transactions.IEnlistmentNotification** interface, and explicitly enlist in the transaction.  
  
## Example  
 To work with **System.Transactions**, you must have a reference to the System.Transactions.dll file.  
  
 The following code demonstrates how to create a transaction that can be promoted against two different instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These instances are represented by two different **System.Data.SqlClient.SqlConnection** objects, which are wrapped in a **TransactionScope** block. The code creates the **TransactionScope** block with a **using** statement, and opens the first connection, which automatically enlists it in the **TransactionScope**. The transaction is initially enlisted as a lightweight transaction, not a full distributed transaction. The code assumes the existence of conditional logic (which has been omitted for brevity). It opens the second connection only if it is needed, enlisting it in the **TransactionScope**. When the connection is opened, the transaction is automatically promoted to a full distributed transaction. The code then invokes **TransactionScope.Complete**, which commits the transaction. The code disposes of the two connections when exiting the **using** statements for the connections. The **TransactionScope.Dispose** method for the **TransactionScope** is automatically called at the termination of the **using** block for the **TransactionScope**. If an exception has been thrown at any point in the **TransactionScope** block, **Complete** does not get called, and the distributed transaction will roll back when the **TransactionScope** is disposed.  
  
 Visual Basic  
  
```  
Using transScope As New TransactionScope()  
    Using connection1 As New SqlConnection(connectString1)  
        ' Opening connection1 automatically enlists it in the   
        ' TransactionScope as a lightweight transaction.  
        connection1.Open()  
  
        ' Do work in the first connection.  
  
        ' Assumes conditional logic in place where the second  
        ' connection will only be opened as needed.  
        Using connection2 As New SqlConnection(connectString2)  
            ' Open the second connection, which enlists the   
            ' second connection and promotes the transaction to  
            ' a full distributed transaction.  
            connection2.Open()  
  
            ' Do work in the second connection.  
  
        End Using  
    End Using  
  
    ' Commit the transaction.  
    transScope.Complete()  
End Using  
```  
  
 C#  
  
```  
using (TransactionScope transScope = new TransactionScope())  
{  
    using (SqlConnection connection1 = new   
       SqlConnection(connectString1))  
    {  
        // Opening connection1 automatically enlists it in the   
        // TransactionScope as a lightweight transaction.  
        connection1.Open();  
  
        // Do work in the first connection.  
  
        // Assumes conditional logic in place where the second  
        // connection will only be opened as needed.  
        using (SqlConnection connection2 = new   
            SqlConnection(connectString2))  
        {  
            // Open the second connection, which enlists the   
            // second connection and promotes the transaction to  
            // a full distributed transaction.   
            connection2.Open();  
  
            // Do work in the second connection.  
        }  
    }  
    //  The Complete method commits the transaction.  
    transScope.Complete();  
}  
```  
  
## See Also  
 [CLR Integration and Transactions](../../relational-databases/clr-integration-data-access-transactions/clr-integration-and-transactions.md)  
  
  
