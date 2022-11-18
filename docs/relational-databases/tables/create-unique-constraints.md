---
description: "Create Unique Constraints"
title: "Create Unique Constraints"
ms.custom: FY22Q2Fresh
ms.date: "10/21/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "UNIQUE_TSQL"
helpviewer_keywords: 
  - "UNIQUE constraints [SQL Server], creating"
  - "constraints [SQL Server], creating"
  - "constraints [SQL Server], unique"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Unique Constraints

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  You can create a unique constraint in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)] to ensure no duplicate values are entered in specific columns that do not participate in a primary key. Creating a unique constraint automatically creates a corresponding unique index.  
  
> [!NOTE]    
> See [Primary key, foreign key, and unique key in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-table-constraints) for information on unique constraints in Azure Synapse Analytics.
  
##  <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  

##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To create a unique constraint  
  
1.  In **Object Explorer**, right-click the table to which you want to add a unique constraint, and select **Design**.  
  
2.  On the **Table Designer** menu, select **Indexes/Keys**.  
  
3.  In the **Indexes/Keys** dialog box, select **Add**.  
  
4.  In the grid under **General**, select **Type** and choose **Unique Key** from the drop-down list box to the right of the property, and then select **Close**.  
  
5.  On the **File** menu, select **Save** _table name_.  

##  <a name="TsqlExample"></a><a name="TsqlProcedure"></a> Use Transact-SQL  


#### To create a unique constraint  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. The example creates the table `TransactionHistoryArchive4` and creates a unique constraint on the column `TransactionID`.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    CREATE TABLE Production.TransactionHistoryArchive4  
     (  
       TransactionID int NOT NULL,   
       CONSTRAINT AK_TransactionID UNIQUE(TransactionID)   
    );   
    GO  
  
    ```  
  
#### To create a unique constraint on an existing table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. The example creates a unique constraint on the columns `PasswordHash` and `PasswordSalt` in the table `Person.Password`.  
  
    ```sql  
    USE AdventureWorks2012;   
    GO  
    ALTER TABLE Person.Password   
    ADD CONSTRAINT AK_Password UNIQUE (PasswordHash, PasswordSalt);   
    GO  
  
    ```  
  
#### To create a unique constraint on a new table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. The example creates a table and defines a unique constraint on the column `TransactionID`.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    CREATE TABLE Production.TransactionHistoryArchive2  
    (  
       TransactionID int NOT NULL,  
       CONSTRAINT AK_TransactionID UNIQUE(TransactionID)  
    );  
    GO  
    ```  

## Next steps
  
 - [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
 - [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)
 - [table_constraint &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
  
