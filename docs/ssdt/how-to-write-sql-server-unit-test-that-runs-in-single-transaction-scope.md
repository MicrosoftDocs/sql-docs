---
title: "How to: Write a SQL Server Unit Test that Runs within the Scope of a Single Transaction | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: cb241e94-d81c-40e9-a7ae-127762a6b855
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Write a SQL Server Unit Test that Runs within the Scope of a Single Transaction
You can modify unit tests to run within the scope of a single transaction. If you take this approach, you can roll back any changes that the test enacted after the test ends. The following procedures explain how to:  
  
-   Create a transaction in your Transact\-SQL test script that uses **BEGIN TRANSACTION** and **ROLLBACK TRANSACTION**.  
  
-   Create a transaction for a single test method in a test class.  
  
-   Create a transaction for all test methods in a given test class.  
  
**Prerequisites**  
  
For some procedures in this topic, the Distributed Transaction Coordinator service must be running on the computer on which you run unit tests. For more information, see the procedure at the end of this topic.  
  
## To create a transaction using Transact\-SQL  
  
#### To create a transaction using Transact\-SQL  
  
1.  Open a unit test in the SQL Server Unit Test Designer. (Double click the source code file for the unit test to display the designer.)  
  
2.  Specify the type of script for which you want to create the transaction. For example, you can specify pre-test, test, or post-test.  
  
3.  Enter a test script in the Transact\-SQL editor.  
  
4.  Insert `BEGIN TRANSACTION` and `ROLLBACK TRANSACTION` statements, as shown in this simple example. The example uses a database table that is named OrderDetails and that contains 50 rows of data:  
  
    ```  
    BEGIN TRANSACTION TestTransaction  
    UPDATE "OrderDetails" set Quantity = Quantity + 10  
    IF @@ROWCOUNT!=50  
    RAISERROR('Row count does not equal 50',16,1)  
    ROLLBACK TRANSACTION TestTransaction  
    ```  
  
    > [!NOTE]  
    > You cannot roll back a transaction after a COMMIT TRANSACTION statement is executed.  
  
    For more information about how ROLLBACK TRANSACTION works with stored procedures and triggers, see this page on the Microsoft Web site: [ROLLBACK TRANSACTION (Transact-SQL)](https://go.microsoft.com/fwlink/?LinkID=115927).  
  
## To create a transaction for a single test method  
In this example, you are using an ambient transaction when you use the [System.Transactions.TransactionScope](https://docs.microsoft.com/dotnet/api/system.transactions.transactionscope) type. By default, the Execution and Privileged connections will not use the ambient transaction, because the connections were created before the method is executed. The SqlConnection has an [System.Data.SqlClient.SqlConnection.EnlistTransaction](https://docs.microsoft.com/dotnet/api/system.data.sqlclient.sqlconnection.enlisttransaction) method, which associates an active connection with a transaction. When an ambient transaction is created, it registers itself as the current transaction, and you can access it through the [System.Transactions.Transaction.Current](https://docs.microsoft.com/dotnet/api/system.transactions.transaction.current) property. In this example, the transaction is rolled back when the ambient transaction is disposed. If you want to commit any changes that were made when you ran the unit test, you must call the [System.Transactions.TransactionScope.Complete](https://docs.microsoft.com/dotnet/api/system.transactions.transactionscope.complete) method.  
  
#### To create a transaction for a single test method  
  
1.  In **Solution Explorer**, right-click the **References** node in your test project and click **Add Reference**.  
  
    The **Add Reference** dialog box appears.  
  
2.  Click the **.NET** tab.  
  
3.  In the list of assemblies, click **System.Transactions** and then click **OK**.  
  
4.  Open the Visual Basic or C# file for your unit test.  
  
5.  Wrap the pre-test, test and post-test actions as shown in the following Visual Basic code example:  
  
    ```  
    <TestMethod()> _  
    Public Sub dbo_InsertTable1Test()  
  
        Using ts as New System.Transactions.TransactionScope( System.Transactions.TransactionScopeOption.Required)  
            ExecutionContext.Connection.EnlistTransaction(Transaction.Current)  
            PrivilegedContext.Connection.EnlistTransaction(Transaction.Current)  
  
            Dim testActions As DatabaseTestActions = Me.dbo_InsertTable1TestData  
            'Execute the pre-test script  
            '  
            System.Diagnostics.Trace.WriteLineIf((Not (testActions.PretestAction) Is Nothing), "Executing pre-test script...")  
            Dim pretestResults() As ExecutionResult = TestService.Execute(Me.PrivilegedContext, Me.PrivilegedContext, testActions.PretestAction)  
            'Execute the test script  
  
            System.Diagnostics.Trace.WriteLineIf((Not (testActions.TestAction) Is Nothing), "Executing test script...")  
            Dim testResults() As ExecutionResult = TestService.Execute(ExecutionContext, Me.PrivilegedContext, testActions.TestAction)  
  
            'Execute the post-test script  
            '  
            System.Diagnostics.Trace.WriteLineIf((Not (testActions.PosttestAction) Is Nothing), "Executing post-test script...")  
            Dim posttestResults() As ExecutionResult = TestService.Execute(Me.PrivilegedContext, Me.PrivilegedContext, testActions.PosttestAction)  
  
            'Because the transaction is not explicitly committed, it  
            'is rolled back when the ambient transaction is   
            'disposed.  
            'To commit the transaction, remove the comment delimiter  
            'from the following statement:  
            'ts.Complete()  
  
    End Sub  
    Private dbo_InsertTable1TestData As DatabaseTestActions  
    ```  
  
    > [!NOTE]  
    > If you are using Visual Basic, you must add `Imports System.Transactions` (in addition to `Imports Microsoft.VisualStudio.TestTools.UnitTesting`, `Imports Microsoft.VisualStudio.TeamSystem.Data.UnitTesting`, and `Imports Microsoft.VisualStudio.TeamSystem.Data.UnitTest.Conditions`) If you are using Visual C#, you must add `using System.Transactions` (in addition to the `using` statements for Microsoft.VisualStudio.TestTools, Microsoft.VisualStudio.TeamSystem.Data.UnitTesting, and Microsoft.VisualStudio.TeamSystem.Data.UnitTesting.Conditions). You must also add a reference to your project to those assemblies.  
  
## To create a transaction for all test methods in a test class  
  
#### To create a transaction for all test methods in a test class  
  
1.  Open the Visual Basic or C# file for your unit test.  
  
2.  Create the transaction in TestInitialize, and dispose of it in TestCleanup, as shown in the following Visual C# code example:  
  
    ```  
    TransactionScope _trans;  
  
            [TestInitialize()]  
            public void Init()  
            {  
                _trans = new TransactionScope();  
                base.InitializeTest();  
            }  
  
            [TestCleanup()]  
            public void Cleanup()  
            {  
                base.CleanupTest();  
                _trans.Dispose();  
            }  
  
            [TestMethod()]  
            public void TransactedTest()  
            {  
                DatabaseTestActions testActions = this.DatabaseTestMethod1Data;  
                // Execute the pre-test script  
                //   
                System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");  
                ExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);  
                // Execute the test script  
                //   
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");  
                ExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);  
                // Execute the post-test script  
                //   
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");  
                ExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);  
  
            }  
    ```  
  
## To start the Distributed Transaction Coordinator service  
Some procedures in this topic use types in the System.Transactions assembly. Before you follow these procedures, you must make sure that the Distributed Transaction Coordinator service is running on the computer where you run the unit tests. Otherwise, the tests fail, and the following error message appears: "Test method *ProjectName*.*TestName*.*MethodName* threw exception: System.Data.SqlClient.SqlException: MSDTC on server '*ComputerName*' is unavailable".  
  
#### To start the Distributed Transaction Coordinator service  
  
1.  Open **Control Panel**.  
  
2.  In **Control Panel**, open **Administrative Tools**.  
  
3.  In **Administrative Tools**, open **Services**.  
  
4.  In the **Services** pane, right-click the **Distributed Transaction Controller** service, and click **Start**.  
  
    The status of the service should update to **Started**. You should now be able to run unit tests that use System.Transactions.  
  
> [!IMPORTANT]  
> The following error might appear, even if you have started the Distributed Transaction Controller service: `System.Transactions.TransactionManagerCommunicationException: Network access for Distributed Transaction Manager (MSDTC) has been disabled. Please enable DTC for network access in the security configuration for MSDTC using the Component Services Administrative tool. ---> System.Runtime.InteropServices.COMException: The transaction manager has disabled its support for remote/network transactions. (Exception from HRESULT: 0x8004D024)`. If this error appears, you must configure the Distributed Transaction Controller for network access. For more information, see [Enable Network DTC Access](https://go.microsoft.com/fwlink/?LinkId=193916).  
  
## See Also  
[Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
  
