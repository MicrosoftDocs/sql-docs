---
title: "Modify Unique Constraints"
description: "Learn how to modify Unique Constraints in SQL Server Management Studio (SSMS) or T-SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/07/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "modifying constraints"
  - "UNIQUE constraints [SQL Server], modifying"
  - "constraints [SQL Server], modifying"
  - "constraints [SQL Server], unique"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Modify unique constraints

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

You can modify a unique constraint in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

<a id="Security"></a>

#### Permissions

Requires `ALTER` permission on the table.  

<a id="SSMSProcedure"></a>

<a id="using-sql-server-management-studio"></a>

## Use SQL Server Management Studio

<a id="to-modify-a-unique-constraint"></a>

#### Modify a unique constraint

1. In the **Object Explorer**, right-click the table containing the unique constraint and select **Design**.  

1. On the **Table Designer** menu, select **Indexes/Keys...**.  

1. In the **Indexes/Keys** dialog box, under **Selected Primary/Unique Key or Index**, select the constraint you wish to edit.  

1. Complete an action from the following table:  

    | To |Follow these steps|  
    | :-------- |:------------------------|  
    | Change the columns that the constraint is associated with |1) In the grid under **(General)**, select **Columns** and then select the ellipses **(...)** to the right of the property.<br /><br /> 2) In the **Index Columns** dialog box, specify the new column or sort order or both for the index.|  
    | Rename the constraint |In the grid under **Identity**, type a new name in the **Name** box. Make sure that your new name does not duplicate a name in the **Selected Primary/Unique Key or Index** list.|  
    | Set the clustered option |In the grid under **Table Designer**, select **Create As Clustered** and from the dropdown list choose Yes to create a clustered index and No to create a nonclustered one. Only one clustered index can exist per table. If a clustered index already exists in this table, you must clear this setting on the original index.|  
    | Define a fill factor |In the grid under **Table Designer**, expand the **Fill Specification** category and type an integer from 0 to 100 in the **Fill Factor** box.|  

1. On the **File** menu, select **Save _table name_**.  

<a id="TsqlProcedure"></a>

<a id="to-modify-a-unique-constraint"></a>

## Modify a unique constraint

To modify a `UNIQUE` constraint using Transact-SQL, you must first delete the existing `UNIQUE` constraint and then re-create it with the new definition. 

## Related content

- [Delete Unique Constraints](delete-unique-constraints.md)
- [Create unique constraints](create-unique-constraints.md)
