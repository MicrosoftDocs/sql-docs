---
title: "Create Primary Keys | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "primary keys [SQL Server], creating"
ms.assetid: 85c623ca-4656-4d70-a9db-ee4d897cd214
author: stevestein
ms.author: sstein
manager: craigg
---
# Create Primary Keys
  You can define a primary key in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Creating a primary key automatically creates a corresponding unique, clustered or nonclustered index.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a primary key, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   A table can contain only one PRIMARY KEY constraint.  
  
-   All columns defined within a PRIMARY KEY constraint must be defined as NOT NULL. If nullability is not specified, all columns participating in a PRIMARY KEY constraint have their nullability set to NOT NULL.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Creating a new table with a primary key requires CREATE TABLE permission in the database and ALTER permission on the schema in which the table is being created.  
  
 Creating a primary key in an existing table requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a primary key  
  
1.  In Object Explorer, right-click the table to which you want to add a unique constraint, and click **Design**.  
  
2.  In **Table Designer**, click the row selector for the database column you want to define as the primary key. If you want to select multiple columns, hold down the CTRL key while you click the row selectors for the other columns.  
  
3.  Right-click the row selector for the column and select **Set Primary Key**.  
  
> [!CAUTION]  
>  If you want to redefine the primary key, any relationships to the existing primary key must be deleted before the new primary key can be created. A message will warn you that existing relationships will be automatically deleted as part of this process.  
  
 A primary key column is identified by a primary key symbol in its row selector.  
  
 If a primary key consists of more than one column, duplicate values are allowed in one column, but each combination of values from all the columns in the primary key must be unique.  
  
 If you define a compound key, the order of columns in the primary key matches the order of columns as shown in the table. However, you can change the order of columns after the primary key is created. For more information, see [Modify Primary Keys](modify-primary-keys.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a primary key in an existing table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example creates a primary key on the column `TransactionID`.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    ALTER TABLE Production.TransactionHistoryArchive   
    ADD CONSTRAINT PK_TransactionHistoryArchive_TransactionID PRIMARY KEY CLUSTERED (TransactionID);  
    GO  
  
    ```  
  
#### To create a primary key in a new table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example creates a table and defines a primary key on the column `TransactionID`.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    CREATE TABLE Production.TransactionHistoryArchive1  
    (  
       TransactionID int NOT NULL,  
       CONSTRAINT PK_TransactionHistoryArchive_TransactionID PRIMARY KEY CLUSTERED (TransactionID)  
    );  
    GO  
  
    ```  
  
     For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql), [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql), and [table_constraint &#40;Transact-SQL&#41;](/sql/relational-databases/system-information-schema-views/table-constraints-transact-sql).  
  
###  <a name="TsqlExample"></a>  
