---
title: "Modify Primary Keys | Microsoft Docs"
ms.custom: ""
ms.date: "07/25/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying primary keys"
  - "primary keys [SQL Server], modifying"
ms.assetid: 8e2a15ba-1cd1-4408-b860-16c3ee37d635
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify Primary Keys
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  You can modify a primary key in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You can modify the primary key of a table by changing the column order, index name, clustered option, or fill factor.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To modify a primary key, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify a primary key  
  
1.  Open the Table Designer for the table whose primary key you want to modify, right-click in the Table Designer, and choose **Indexes/Keys** from the shortcut menu.  
  
2.  In the **Indexes/Keys** dialog box, select the primary key index from the **Selected Primary/Unique Key or Index** list.  
  
3.  Complete an action from the following table:  
  
    |To|Follow these steps|  
    |--------|------------------------|  
    |Rename the primary key|Type a new name in the **Name** box. Make sure that your new name does not duplicate a name in the **Selected Primary/Unique Key or Index** list.|  
    |Set the clustered option|To create a clustered index for the primary key, select **Create as CLUSTERED**, and select the option from the drop-down list box. Only one clustered index can exist per table. If this option is not available for your index, you must first clear this setting on the existing clustered index.<br /><br /> If this option is not selected, a unique nonclustered index is created.|  
    |Define a fill factor|Expand the **Fill Specification** category and type an integer from 0 to 100 in the **Fill factor** box. For more information about fill factors and their uses, see [Specify Fill Factor for an Index](../../relational-databases/indexes/specify-fill-factor-for-an-index.md).|  
    |Change the column order|Select **Columns**, and then click the ellipses **(...)** to the right of the property. In the  **Index Columns** dialog box, remove the columns from the primary key. Then add the columns back in the order you want. To remove a column from the key, simply remove the column name from the **Column** name list.|  
  
4.  On the **File** menu, click **Save**_table name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To modify a primary key**  
  
 To modify a PRIMARY KEY constraint using Transact-SQL, you must first delete the existing PRIMARY KEY constraint and then re-create it with the new definition. For more information, see [Delete Primary Keys](../../relational-databases/tables/delete-primary-keys.md) and [Create Primary Keys](../../relational-databases/tables/create-primary-keys.md).  
  
###  <a name="TsqlExample"></a>  
