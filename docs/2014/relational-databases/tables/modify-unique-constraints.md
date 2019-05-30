---
title: "Modify Unique Constraints | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying constraints"
  - "UNIQUE constraints [SQL Server], modifying"
  - "constraints [SQL Server], modifying"
  - "constraints [SQL Server], unique"
ms.assetid: fddbdc9e-958b-4614-8e88-6ca205d64a4e
author: stevestein
ms.author: sstein
manager: craigg
---
# Modify Unique Constraints
  You can modify a unique constraint in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To modify a unique constraint using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify a unique constraint  
  
1.  In the **Object Explorer**, right-click the table containing the unique constraint and select **Design**.  
  
2.  On the **Table Designer** menu, click **Indexes/Keys...**.  
  
3.  In the **Indexes/Keys** dialog box, under **Selected Primary/Unique Key or Index**, select the constraint you wish to edit.  
  
4.  Complete an action from the following table:  
  
    |To|Follow these steps|  
    |--------|------------------------|  
    |Change the columns that the constraint is associated with|1) In the grid under **(General)**, click **Columns** and then click the ellipses **(...)** to the right of the property.<br /><br /> 2) In the **Index Columns** dialog box, specify the new column or sort order or both for the index.|  
    |Rename the constraint|In the grid under **Identity**, type a new name in the **Name** box. Make sure that your new name does not duplicate a name in the **Selected Primary/Unique Key or Index** list.|  
    |Set the clustered option|In the grid under **Table Designer**, select **Create As Clustered** and from the dropdown choose Yes to create a clustered index and No to create a non-clustered one. Only one clustered index can exist per table. If a clustered index already exists in this table, you must clear this setting on the original index.|  
    |Define a fill factor|In the grid under **Table Designer**, expand the **Fill Specification** category and type an integer from 0 to 100 in the **Fill Factor** box.|  
  
5.  On the **File** menu, click **Save**_table name_.  
  
##  <a name="TsqlProcedure"></a> **To modify a unique constraint**  
  
 To modify a UNIQUE constraint using Transact-SQL, you must first delete the existing UNIQUE constraint and then re-create it with the new definition. For more information, see [Delete Unique Constraints](delete-unique-constraints.md) and [Create Unique Constraints](create-unique-constraints.md).  
  
###  <a name="TsqlExample"></a>  
