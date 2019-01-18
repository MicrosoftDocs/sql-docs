---
title: "Indexes - Keys Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:65539"
  - "vdt.ppg.indexeskeys"
ms.assetid: 9e4060ba-80c3-468f-bccb-e12e99f672c2
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Indexes - Keys Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Use this dialog box to create or modify indexes, primary keys, and unique keys. To access this dialog box, open the table definition for the table with the index or key, right-click the table definition grid, and then click **Indexes/Keys**.  
  
> [!NOTE]  
> If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## Options  
**Selected Primary/Unique Key or Index**  
Lists existing primary or unique keys and indexes. Select one to show its properties in the grid to the right. If the list is empty, none have been defined for the table.  
  
**Add**  
Create a new primary or unique key or index.  
  
**Delete**  
Delete the key or index selected in the **Selected Primary/Unique Key or Index** list.  
  
**General Category**  
When expanded, shows the properties **Columns**, **Is Unique**, and **Type**.  
  
**Columns**  
Lists chosen sort orders for the columns in the key or index, and provides access to a dialog box where the sort orders can be defined. To display the dialog box, click **Columns** and then click the ellipsis button (...) that appears to the right of the property field.  
  
**Is Unique**  
Indicates whether data entered into this index or key must be unique. This is unavailable for XML Indexes.  
  
**Type**  
Specify whether the item selected in the **Selected Primary/Unique Key or Index** list is a unique key, a primary key, or an index. For primary keys this field is read-only.  
  
**Identity Category**  
When expanded, it shows the property fields for **Name** and **Description**.  
  
**Name**  
Shows the name of the key or index. When a new one is created, it is given a default name based on the table in the active window in Table Designer. You can change the name at any time.  
  
**Description**  
Provides a place to describe the key or index. To write a more detailed description, click **Description** and then click the ellipsis button (**...**) that appears to the right of the property field. This provides a larger area in which to write text.  
  
**Table Designer Category**  
When expanded, shows information for **Create as Clustered**.  
  
**Create as Clustered**  
Make the key or index clustered. Only one clustered index is allowed on a table. Data in the table is stored in the order of the clustered index. For more information, see [Create Clustered Indexes](../../relational-databases/indexes/create-clustered-indexes.md) and [Create Nonclustered Indexes](../../relational-databases/indexes/create-nonclustered-indexes.md).  
  
**Data Space Specification**  
When expanded, shows information for **(Data Space Type)**, **Filegroup or Partition Scheme Name**, and **Partition Column List**.  
  
**(Data Space Type)**  
Indicates whether this index or key belongs to a file group or partition scheme.  
  
**Filegroup or Partition Scheme Name**  
Shows the name of the file group or partition scheme on which it is stored.  
  
**Partition Column List**  
Displays a comma-separated list of columns that participate in the partition column function. Unavailable if Filegroup is selected in the **(Data Space Type)** field.  
  
**Fill Specification**  
When expanded, shows information for **Fill Factor** and **Pad Index**.  
  
**Fill Factor**  
Specifies what percentage of the index's leaf-level pages the system can fill. Once a page is full, the system must split the pages to add new data, impairing performance.  
  
-   A value of 100 means the pages will be full. This will require the least amount of storage space. This setting should be used only when there will be no changes to the data, for example, on a read-only table.  
  
-   A lower value leaves more empty space on the data pages. This reduces the need to split data pages as indexes grow but requires more storage space.  
  
**Pad Index**  
Indicate whether intermediate pages in this index are provided the same percentage of empty space (padding) specified in **Fill Factor** when they grow.  
  
**Ignore Duplicate Keys**  
Specify what happens when a row is inserted during a bulk insert operation whose key value equals an existing key value. If you choose:  
  
-   **Yes** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] issues a warning, ignores the offending incoming row, and tries to insert the remaining rows.  
  
-   **No** [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] issues an error message and rolls back the entire bulk insert operation.  
  
**Included Columns**  
Displays a comma-separated list of the names of all the columns that constitute the index key. Subkey columns can only be specified for nonclustered indexes. This property is hidden for XML indexes.  
  
**Is Disabled**  
Indicates whether this index is disabled. This is a read-only property. This property is only set to **Yes** if the index has been disabled outside of the Visual Database tools.  
  
**Is Full-Text Key**  
Specify whether this index is a full-text key. For more information on full-text keys, see SQL Server Books Online. This property is hidden for XML indexes.  
  
**Page Locks Allowed**  
Specify whether page-level locking is allowed on this index. Allowing or disallowing page-level locking affects database performance. The recommended setting is **Yes**.  
  
**Re-compute Statistics**  
Specify whether the underlying [!INCLUDE[ssDE](../../includes/ssde_md.md)] computes new statistics when the index is created. Re-computing statistics slows the building of indexes but will very likely improve query performance.  
  
**Row Locks Allowed**  
Specify whether row-level locking is allowed on this index. Allowing or disallowing row-level locking affects database performance. The recommended setting is **Yes**.  
  
## See Also  
[Working with Constraints (Visual Database Tools)](https://msdn.microsoft.com/637098af-2567-48f8-90f4-b41df059833e)  
[Working with Keys (Visual Database Tools)](https://msdn.microsoft.com/31fbcc9f-2dc5-4bf9-aa50-ed70ec7b5bcd)  
  
