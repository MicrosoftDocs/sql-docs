---
title: Create Statistics
description: Learn how to create query optimization statistics on columns of a table or indexed view in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: katsmith, randolphwest
ms.date: 07/22/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
ms.custom: event-tier1-build-2022
f1_keywords:
  - "sql13.swb.stat.properties.f1"
  - "sql13.swb.statistics.filter.f1"
  - "sql13.swb.stat.columns.f1"
  - "sql13.swb.statistics.properties.f1"
helpviewer_keywords:
  - "creating statistics"
  - "statistics [SQL Server], creating"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# Create statistics

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

You can create query optimization statistics on one or more columns of a table or indexed view in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. For most queries, the query optimizer already generates the necessary statistics for a high-quality query plan; in a few cases, you need to create additional statistics.

## <a id="Restrictions"></a> Limitations and restrictions

Before creating statistics with the CREATE STATISTICS statement, verify that the AUTO_CREATE_STATISTICS option is set at the database level. This will ensure that the query optimizer continues to routinely create single-column statistics for query predicate columns.

You can list up to 32 columns per statistics object.

You can't drop, rename, or alter the definition of a table column that is defined in a filtered statistic predicate.

## Permissions

Requires that the user be the table or indexed view owner, or a member of one of the following roles: **sysadmin** fixed server role, **db_owner** fixed database role, or the **db_ddladmin** fixed database role.

### Use SQL Server Management Studio

1. In **Object Explorer**, select the plus sign to expand the database in which you want to create a new statistic.

1. Select the plus sign to expand the **Tables** folder.

1. Select the plus sign to expand the table in which you want to create a new statistic.

1. Right-click the **Statistics** folder and select **New Statistics...**.

   The following properties show on the **General** page in the **New Statistics on Table** *table_name* dialog box.

   |Property|Description|
   | --- | --- |
   |**Table Name**|Displays the name of the table described by the statistics.|
   |**Statistics Name**|Displays the name of the database object where the statistics are stored.|
   |**Statistics Columns**|This grid shows the columns described by this set of statistics. All values in the grid are read-only.|
   |**Name**|Displays the name of the column described by the statistics. This can be a single column or a combination of columns in a single table.|
   |**Data Type**|Indicates the data type of the columns described by the statistics.|
   |**Size**|Displays the size of the data type for each column.|
   |**Identity**|Indicates an identity column when it is checked.|
   |**Allow NULLs**|Indicates whether the column accepts NULL values.|
   |**Add**|Add more columns from the table to the statistics grid.|
   |**Remove**|Remove the selected column from the statistics grid.|
   |**Move Up**|Move the selected column to an earlier location in the statistics grid. The location in the grid can substantially affect the usefulness of the statistics.|
   |**Move Down**|Move the selected column to a later location in the statistics grid.|
   |**Statistics for these columns were last updated**|Indicates how old the statistics are. Statistics are more valuable when they are current. Update statistics after large changes to the data or after adding atypical data. Statistics for tables that have a consistent distribution of data need to be updated less frequently.|
   |**Update statistics for these columns**|Check to update the statistics when the dialog box is closed.|

   The following property shows on the **Filter** page in the **New Statistics on Table** *table_name* dialog box.

   |Property|Description|
   | --- | --- |
   |**Filter Expression**|Defines which data rows to include in the filtered statistics. For example, `Production.ProductSubcategoryID IN ( 1, 2, 3 )`|

1. In the **New Statistics on Table** *table_name* dialog box, on the **General** page, select **Add**.

   The following properties show in the **Select Columns** dialog box. This information is read-only.

   |Property|Description|
   | --- | --- |
   |**Name**|Displays the name of the column described by the statistics. This can be a single column or a combination of columns in a single table.|
   |**Data Type**|Indicates the data type of the columns described by the statistics.|
   |**Size**|Displays the size of the data type for each column.|
   |**Identity**|Indicates an identity column when checked.|
   |**Allow NULLs**|Indicates whether the column accepts NULL values.|

1. In the **Select Columns** dialog box, select the check box or check boxes of each column for which you want to create a statistic and then select **OK**.

1. In the **New Statistics on Table** *table_name* dialog box, select **OK**.

## Use Transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**.

   ```sql
   USE AdventureWorks2012;
   GO
   -- Create new statistic object called ContactMail1
   -- on the BusinessEntityID and EmailPromotion columns in the Person.Person table.

   CREATE STATISTICS ContactMail1
       ON Person.Person (BusinessEntityID, EmailPromotion);
   GO
   ```

1. The statistic created above potentially improves the results for the following query.

   ```sql
   USE AdventureWorks2012;
   GO
   SELECT LastName, FirstName
   FROM Person.Person
   WHERE EmailPromotion = 2
   ORDER BY LastName, FirstName;
   GO
   ```

## Next steps

- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
