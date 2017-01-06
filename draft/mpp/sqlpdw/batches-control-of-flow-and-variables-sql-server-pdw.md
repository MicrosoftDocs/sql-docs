---
title: "Batches, Control-of-Flow, and Variables (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: eb1c9a84-1214-48f9-b69d-58d325870a77
caps.latest.revision: 16
author: BarbKess
---
# Batches, Control-of-Flow, and Variables (SQL Server PDW)
A batch is a group of one or more SQL statements sent as part of a single request from an application to SQL Server PDW for execution. Batches allow the use of conditional statements and variables in SQL Server PDW.  
  
## Contents  
  
-   [General Remarks](#GeneralRemarks)  
  
-   [Security Notices](#Security)  
  
-   [Examples](#Examples)  
  
## <a name="GeneralRemarks"></a>General Remarks  
The statements in a SQL Server PDW batch are compiled and run sequentially. Each statement in a batch goes through the normal execution path, and through the normal queues and concurrency locks. This means that other concurrent users can preempt the execution of the batch in between the batch statements.  
  
Transactions are supported within batches. This means that BEGIN TRANSACTION, SET AUTOCOMMIT, COMMIT and ROLLBACK can be used within a batch.  
  
Each SQL statement within a batch should be terminated with a semicolon. This requirement is not enforced, but the ability to end a statement without a semicolon is deprecated and may be removed in a future version of SQL Server PDW.  
  
A compilation or run-time error, such as a syntax error or arithmetic overflow error, prevents the batch from proceeding.  
  
A batch abort due to a runtime error during a transaction does not automatically rollback the transaction unless the specific statement level error that caused the batch to abort also aborts the transaction.  
  
Each batch has no ability to reference any variables declared in another batch.  
  
If comments are used, they must start and end within the same batch.  
  
CREATE VIEW statements cannot be combined with other statements in a batch.  
  
### Error Handling  
Statements useful for error handling include: [PRINT](../sqlpdw/print-sql-server-pdw.md), [RAISERROR](../sqlpdw/raiserror-sql-server-pdw.md), [THROW](../sqlpdw/throw-sql-server-pdw.md), and [TRY...CATCH](../sqlpdw/try-catch-sql-server-pdw.md).  
  
## <a name="Security"></a>Security  
Batch files might contain credentials stored in plain text. Credentials may be echoed to the screen of the user during batch execution.  
  
## <a name="Examples"></a>Examples  
  
### A. Batches within Transactions  
The following example shows several batches combined into one transaction. The BEGIN TRANSACTION and COMMIT statements delimit the transaction boundaries. The BEGIN TRANSACTION, USE, SELECT, and COMMIT statements are all in their own single-statement batches. All of the INSERT statements are included in one batch.  
  
This example INSERTs values into an existing `mycompanies` table in the `mydatabase` database, then SELECTs the values back for display to the user. The CREATE DATABASE and CREATE TABLE commands to prepare these objects are also shown below.  
  
```  
CREATE DATABASE mydatabase  
   WITH   
   (REPLICATED_SIZE = 1.5 GB,  
   DISTRIBUTED_SIZE = 5.25 GB,  
   LOG_SIZE = 10);  
  
CREATE TABLE mydatabase..mycompanies  
(  
 id_num int,  
 company_name nvarchar(100)  
);  
  
BEGIN TRANSACTION  
    BEGIN  
        USE mydatabase;  
    END  
    BEGIN  
        INSERT INTO mycompanies   
           VALUES (1, N'A Bike Store');  
        INSERT INTO mycompanies  
           VALUES (2, N'Progressive Sports');  
        INSERT INTO mycompanies  
           VALUES (3, N'Modular Cycle Systems');  
        INSERT INTO mycompanies  
           VALUES (4, N'Advanced Bike Components');  
        INSERT INTO mycompanies  
           VALUES (5, N'Metropolitan Sports Supply');  
        INSERT INTO mycompanies  
           VALUES (6, N'Aerobic Exercise Company');  
        INSERT INTO mycompanies  
           VALUES (7, N'Associated Bikes');  
        INSERT INTO mycompanies  
           VALUES (8, N'Exemplary Cycles');  
    END  
    BEGIN  
        SELECT id_num, company_name  
        FROM mycompanies  
        ORDER BY company_name ASC;  
    END  
COMMIT;  
```  
  
### B. Specifying Batches through Database APIs  
  
-   In ADO, a batch request can be specified by setting the **CommandText** property of a **Command** object to a string of multiple statements:  
  
    ```  
    Dim Cmd As New ADODB.Command  
    Set Cmd.ActiveConnection = Cn  
    Cmd.CommandText = "SELECT * FROM dbo.Vendor; SELECT * FROM dbo.Product"  
    Cmd.CommandType = adCmdText  
    Cmd.Execute  
    ```  
  
-   In OLE DB, a batch can be sent as a string of SQL statements enclosed in the string used to set the command text:  
  
    ```  
    WCHAR* wszSQLString =  
    L"SELECT * FROM dbo.Employee; SELECT * FROM dbo.Product";  
    hr = pICommandText->SetCommandText  
          (DBGUID_DBSQL, wszSQLString)  
    ```  
  
-   In ODBC, a batch can be sent as a string of Transact\-SQL statements enclosed on a **SQLPrepare** or **SQLExecDirect** call:  
  
    ```  
    SQLExecDirect(hstmt1,  
       "SELECT * FROM dbo.Employee; SELECT * FROM dbo.Product",  
    SQL_NTS):  
    ```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
