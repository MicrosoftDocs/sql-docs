---
title: "Create unique constraints"
description: "This article shows you how to create unique constraints using SQL Server Management Studio and Transact-SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/20/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
f1_keywords:
  - "UNIQUE_TSQL"
helpviewer_keywords:
  - "UNIQUE constraints [SQL Server], creating"
  - "constraints [SQL Server], creating"
  - "constraints [SQL Server], unique"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create unique constraints

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can create a unique constraint in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)] to ensure no duplicate values are entered in specific columns that don't participate in a primary key. Creating a unique constraint automatically creates a corresponding unique index.  
  
> [!NOTE]
> For information on unique constraints in Azure Synapse Analytics, see [Primary key, foreign key, and unique key in Azure Synapse Analytics](/azure/sql-data-warehouse/sql-data-warehouse-table-constraints).
  
## <a id="Security"></a> <a name="Permissions"></a> Permissions

Requires ALTER permission on the table.  

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio (SSMS)
  
### <a id="to-create-a-unique-constraint-using-ssms"></a> Create a unique constraint using SSMS
  
1. In **Object Explorer**, right-click the table to which you want to add a unique constraint, and select **Design**.  
  
1. On the **Table Designer** menu, select **Indexes/Keys**.  
  
1. In the **Indexes/Keys** dialog box, select **Add**.  
  
1. In the grid under **General**, select **Type** and choose **Unique Key** from the dropdown list box to the right of the property, and then select **Close**.  
  
1. On the **File** menu, select **Save _table name_**.  

## <a id="TsqlExample"></a> <a name="TsqlProcedure"></a> Use Transact-SQL

### <a id="to-create-a-unique-constraint-using-transact-sql"></a> Create a unique constraint using Transact-SQL
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the **Standard** bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**. The example creates the table `TransactionHistoryArchive4` and creates a unique constraint on the column `TransactionID`.  
  
    ```sql  
    USE AdventureWorks2022;  
    GO  
    CREATE TABLE Production.TransactionHistoryArchive4  
     (  
       TransactionID int NOT NULL,   
       CONSTRAINT AK_TransactionID UNIQUE(TransactionID)   
    );   
    GO  
    ```  
  
### <a id="to-create-a-unique-constraint-on-an-existing-table"></a> Create a unique constraint on an existing table
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the **Standard** bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**. The example creates a unique constraint on the columns `PasswordHash` and `PasswordSalt` in the table `Person.Password`.  
  
    ```sql  
    USE AdventureWorks2022;   
    GO  
    ALTER TABLE Person.Password   
    ADD CONSTRAINT AK_Password UNIQUE (PasswordHash, PasswordSalt);   
    GO  
  
    ```  
  
### <a id="to-create-a-unique-constraint-on-a-new-table"></a> Create a unique constraint on a new table
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].  
  
1. On the **Standard** bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**. The example creates a table and defines a unique constraint on the column `TransactionID`.  
  
    ```sql  
    USE AdventureWorks2022;  
    GO  
    CREATE TABLE Production.TransactionHistoryArchive2  
    (  
       TransactionID int NOT NULL,  
       CONSTRAINT AK_TransactionID UNIQUE(TransactionID)  
    );  
    GO  
    ```

### Create a unique constraint on a nullable column

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].  

1. On the **Standard** bar, select **New Query**.  

1. Copy and paste the following example into the query window and select **Execute**. The example creates a [filtered](../indexes/create-filtered-indexes.md) unique constraint using the `CREATE UNIQUE INDEX` syntax, only enforcing uniqueness on non-`NULL` values.

    ```sql  
    USE AdventureWorks2022;  
    GO
    CREATE UNIQUE INDEX UQ_AdventureWorksDWBuildVersion
    ON dbo.AdventureWorksDWBuildVersion (DBVersion)
    WHERE (DBVersion IS NOT NULL);
    GO  
    ```  

## Related content

- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [table_constraint (Transact-SQL)](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)
