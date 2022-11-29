---
title: "Modify Columns (Database Engine)"
description: "Modify Columns (Database Engine)"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying data types"
  - "column data types [SQL Server]"
  - "data types [SQL Server], columns"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: FY22Q2Fresh
ms.reviewer: ""
ms.date: "10/21/2021"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify Columns (Database Engine)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  You can modify the data type of a column in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
> [!WARNING]  
>  Modifying the data type of a column that already contains data can result in the permanent loss of data when the existing data is converted to the new type. In addition, code and applications that depend on the modified column may fail. These include queries, views, stored procedures, user-defined functions, and client applications. Note that these failures will cascade. For example, a stored procedure that calls a user-defined function that depends on the modified column may fail. Carefully consider any changes you want to make to a column before making it.  
  
## <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To modify the data type of a column  
  
1.  In **Object Explorer**, right-click the table with columns for which you want to change the scale and select **Design**.  
  
2.  Select the column for which you want to modify the data type.  
  
3.  In the **Column Properties** tab, select the grid cell for the **Data Type** property and choose a new data type from the drop-down list.  
  
4.  On the **File** menu, select **Save** _table name_.  
  
> [!NOTE]  
>  When you modify the data type of a column, Table Designer applies the default length of the data type you selected, even if you have already specified another. Always set the data type length for to the desired value after specifying the data type.  
  
> [!WARNING]  
>  If you attempt to modify the data type of a column that relates to other tables, Table Designer asks you to confirm that the change should be made to the columns in the other tables as well.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To modify the data type of a column  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**.  
  
    ```sql  
    CREATE TABLE dbo.doc_exy (column_a INT );  
    GO  
    INSERT INTO dbo.doc_exy (column_a) VALUES (10);  
    GO  
    ALTER TABLE dbo.doc_exy ALTER COLUMN column_a DECIMAL (5, 2);  
    GO  
  
    ```  

## Next steps
 - [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)
  
  
