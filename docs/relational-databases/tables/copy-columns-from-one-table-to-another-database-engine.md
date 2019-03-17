---
title: "Copy Columns from One Table to Another (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "09/01/2016"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "copying columns"
  - "columns [SQL Server], copying"
ms.assetid: 5f5e70dc-69f9-44b8-bc48-b5d51ac20d77
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Copy Columns from One Table to Another (Database Engine)
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

  This topic describes how to copy columns from one table to another, copying either just the column definition, or the definition and data in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To copy columns, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 When you copy a column that has an alias data type from one database to another, the alias data type may not be available in the destination database. In such a case, the column will be assigned the nearest matching base data type available in that database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To copy column definitions from one table to another  
  
1.  Open the table with columns you want to copy and the one you want to copy into by right-clicking the tables, and then clicking **Design**.  
  
2.  Click the tab for the table with the columns you want to copy and select those columns.  
  
3.  From the **Edit** menu, click **Copy**.  
  
4.  Click the tab for the table into which you want to copy the columns.  
  
5.  Select the column you want to follow the inserted columns and, from the **Edit** menu, click **Paste**.  
  
#### To copy data from one table to another  
  
1.  Follow the directions for copying column definitions above.  
  
    > [!NOTE]  
    >  Before you begin to copy data from one table to another, make sure that the data types in the destination columns are compatible with the data types of the source columns  
  
2.  Open a new Query Editor window. 

3.  Right-click the Query Editor, and then click **Design Query in Editor**. 

4.  In the **Add Table** dialog box, select the source and destination table, click **Add**, and then close the **Add Table** dialog box. 

5.  Right-click an open area of the Query Editor, point to **Change Type**, and then click **Insert Results**.  

6.  In the **Choose Target Table for Insert Results** dialog box, select the destination table. 

7.  In the upper portion of the Query Designer, click the source column in the source table.

8. The Query Designer has now created an INSERT query. Click OK to place the query into the original Query Editor window.  

9.  Execute the query to insert the data from the source table to the destination table.

  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To copy column definitions from one table to another  
  
1.  You cannot copy individual columns from one table to another existing table by using Transact-SQL statements. However, you can create a new table in the default filegroup and inserts the resulting rows from the query into it by using SELECT INTO. For more information, see [INTO Clause &#40;Transact-SQL&#41;](../../t-sql/queries/select-into-clause-transact-sql.md).  
  
#### To copy data from one table to another  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    CREATE TABLE dbo.EmployeeSales  
    ( BusinessEntityID   varchar(11) NOT NULL,  
      SalesYTD money NOT NULL  
    );  
    GO  
    INSERT INTO dbo.EmployeeSales  
        SELECT BusinessEntityID, SalesYTD   
        FROM Sales.SalesPerson;  
    GO  
    ```  
  
  
