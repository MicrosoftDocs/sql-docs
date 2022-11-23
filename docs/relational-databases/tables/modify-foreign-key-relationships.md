---
description: "Modify Foreign Key Relationships"
title: "Modify Foreign Key Relationships | Microsoft Docs"
ms.custom: ""
ms.date: "07/25/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:65538"
  - "vdt.ppg.relationships"
helpviewer_keywords: 
  - "foreign keys [SQL Server], modifying"
  - "modifying foreign keys"
ms.assetid: 0c9ca80d-d79b-44c4-a21e-0fce39c398ec
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify Foreign Key Relationships

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

  You can modify the foreign key side of a relationship in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Modifying a table's foreign key changes which columns are related to columns in the primary key table.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To modify a foreign key using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 The new foreign key column must match the data type and size of the primary key column to which it relates, with these exceptions:  
  
-   A **char** column or **sysname** column can relate to a **varchar** column.  
  
-   A **binary** column can relate to a **varbinary** column.  
  
-   An alias data type can relate to its base type.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To modify a foreign key  
  
1.  In **Object Explorer**, expand the table with the foreign key and then expand **Keys**.  
  
2.  Right-click the foreign key to be modified and select **Modify**.  
  
3.  In the **Foreign Key Relationships** dialog box, you can make the following modifications.  
  
     **Selected Relationship**  
     Lists existing relationships. Select a relationship to show its properties in the grid to the right. If the list is empty, no relationships have been defined for the table.  
  
     **Add**  
     Create a new relationship. The **Tables and Columns Specifications** must be set before the relationship will be valid.  
  
     **Delete**  
     Delete the relationship selected in the **Selected Relationships** list. To cancel the addition of a relationship, use this button to remove the relationship.  
  
     **General Category**  
     Expand to show **Check Existing Data on Creation or RE-Enabling** and **Tables and Columns Specifications**.  
  
     **Check Existing Data on Creation or Re-Enabling**  
     Verify all existing data in the table before the constraint was created or re-enabled, against the constraint.  
  
     **Tables and Columns Specifications Category**  
     Expand to show which columns from which tables act as the foreign key and primary (or unique) key in the relationship. To edit or define these values, click the ellipsis button (**...**) to the right of the property field.  
  
     **Foreign Key Base Table**  
     Shows which table contains the column acting as a foreign key in the selected relationship.  
  
     **Foreign Key Columns**  
     Shows which column acts as a foreign key in the selected relationship.  
  
     **Primary/Unique Key Base Table**  
     Shows which table contains the column acting as a primary (or unique) key in the selected relationship.  
  
     **Primary/Unique Key Columns**  
     Shows which column acts as a primary (or unique) key in the selected relationship.  
  
     **Identity Category**  
     Expand to show the property fields for **Name** and **Description**.  
  
     **Name**  
     Shows the name of the relationship. When a new relationship is created, it is given a default name based on the table in the active window in **Table Designer**. You can change the name at any time.  
  
     **Description**  
     Describe the relationship. To write a more detailed description, click **Description** and then click the ellipsis **(...)** that appears to the right of the property field. This provides a larger area in which to write text.  
  
     **Table Designer Category**  
     Expand to show information for **Check Existing Data on Creation or Re-Enabling** and **Enforce for Replication**.  
  
     **Enforce For Replication**  
     Indicates whether to enforce the constraint when a replication agent performs an insert, update, or delete on this table.  
  
     **Enforce Foreign Key Constraint**  
     Specify whether changes are allowed to the data of the columns in the relationship if those changes would invalidate the integrity of the foreign key relationship. Choose **Yes** if you do not want to allow such changes, and choose **No** if you do want to allow them.  
  
     **INSERT and UPDATE Specification Category**  
     Expand to show information for the **Delete Rule** and the **Update Rule** for the relationship.  
  
     **Delete Rule**  
     Specify what happens if a user tries to delete a row with data that is involved in a foreign key relationship:  
  
    -   **No Action** An error message tells the user that the deletion is not allowed and the DELETE is rolled back.  
  
    -   **Cascade** Deletes all rows containing data involved in the foreign key relationship. Do not specify CASCADE if the table will be included in a merge publication that uses logical records.  
  
    -   **Set Null** Sets the value to null if all foreign key columns for the table can accept null values.  
  
    -   **Set Default** Sets the value to the default value defined for the column if all foreign key columns for the table have defaults defined for them.  
  
     **Update Rule**  
     Specify what occurs if a user tries to update a row with data that is involved in a foreign key relationship:  
  
    -   **No Action** An error message tells the user that the update is not allowed and the UPDATE is rolled back.  
  
    -   **Cascade** Updates all rows that contain data involved in the foreign key relationship. Do not specify CASCADE if the table will be included in a merge publication that uses logical records.  
  
    -   **Set Null** Sets the value to null if all foreign key columns for the table can accept null values.  
  
    -   **Set Default** Sets the value to the default value that is defined for the column if all foreign key columns for the table have defaults defined for them.  
  
4.  On the **File** menu, click **Save**_table name_.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To modify a foreign key**  
  
 To modify a FOREIGN KEY constraint by using Transact-SQL, you must first delete the existing FOREIGN KEY constraint and then re-create it with the new definition. For more information, see [Delete Foreign Key Relationships](../../relational-databases/tables/delete-foreign-key-relationships.md) and [Create Foreign Key Relationships](../../relational-databases/tables/create-foreign-key-relationships.md).  
  
###  <a name="TsqlExample"></a>  
