---
title: "Memory Optimization Advisor | Microsoft Docs"
ms.custom: ""
ms.date: "10/26/2015"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.memoryoptimizationwizard.f1"
  - "swb.memoryoptimizationwizard.f1"
ms.assetid: 181989c2-9636-415a-bd1d-d304fc920b8a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Memory Optimization Advisor
  Transaction performance reports tool (see [Determining if a Table or Stored Procedure Should Be Ported to In-Memory OLTP](determining-if-a-table-or-stored-procedure-should-be-ported-to-in-memory-oltp.md)) informs you about which tables in your database will benefit if ported to use In-Memory OLTP. After you identify a table that you would like to port to use In-Memory OLTP, you can use the memory optimization advisor to help you migrate the disk-based database table to In-Memory OLTP.  
  
 To begin, connect to the instance that contains the disk-based database table. You can connect to a [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instance. However, if you wish to perform a migration operation with the advisor, you must connect to a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instance on which In-Memory OLTP functionality is enabled. For more information about In-Memory OLTP requirements, see [Requirements for Using Memory-Optimized Tables](memory-optimized-tables.md).  
  
 For information about migration methodologies, see [In-Memory OLTP - Common Workload Patterns and Migration Considerations](https://msdn.microsoft.com/library/dn673538.aspx).  
  
## Walkthrough Using the Memory-Optimization Advisor  
 In **Object Explorer**, right click the table you want to convert, and select **Memory-Optimization Advisor**. This will display the welcome page for the **Table Memory Optimization Advisor**.  
  
### Memory Optimization Checklist  
 When you click **Next** in the welcome page for the **Table Memory Optimization Advisor**, you will see the memory optimization checklist. Memory-optimized tables do not support all the features in a disk-based table. The memory optimization checklist reports if the disk-based table uses any features that are incompatible with a memory-optimized table. The **Table Memory Optimization Advisor** does not modify the disk-based table so that it can be migrated to use In-Memory OLTP. You must make those changes before continuing migration. For each incompatibility found, the **Table Memory Optimization Advisor** displays a link to information that can help you modify your disk-based tables.  
  
 If you wish to keep a list of these incompatibilities, to plan your migration, click the **Generate Report** to generate a HTML list.  
  
 If your table has no incompatibilities and you are connected to a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] instance with In-Memory OLTP, click **Next**.  
  
### Memory Optimization Warnings  
 The next page, memory optimization warnings, contains a list of issues that do not prevent the table from being migrated to use In-Memory OLTP, but that may cause the behavior of other objects (such as stored procedures or CLR functions) to fail or result in unexpected behavior.  
  
 The first several warnings in the list are informational and may or may not apply to your table. Links in the right-hand column of the table will take you to more information.  
  
 The warning table will also display potential warning conditions that are not present in your table.  
  
 Actionable warnings will have a yellow triangle in the left-hand column. If there are actionable warnings, you should exit the migration, resolve the warnings, and then restart the process. If you do not resolve the warnings, your migrated table may cause a failure.  
  
 Click **Generate Report** to generate an HTML report of these warnings. Click **Next** to proceed.  
  
### Review Optimization Options  
 The next screen lets you modify options for the migration to In-Memory OLTP:  
  
 Memory-optimized filegroup  
 The name for your memory-optimized filegroup. A database must have a memory-optimized filegroup with at least one file before a memory-optimized table can be created.  
  
 If you do not have a memory-optimized filegroup, you can change the default name. Memory-optimized filegroups cannot be deleted. The existence of a memory-optimized filegroup may disable some database-level features such as AUTO CLOSE and database mirroring.  
  
 If a database already has a memory-optimized file group, this field will be pre-populated with its name and you will not be able to change the value of this field.  
  
 Logical file name and File path  
 The name of the file that will contain the memory-optimized table. A database must have a memory-optimized file group with at least one file before a memory-optimized table can be created.  
  
 If you do not have an existing memory-optimized file group, you can change the default name and path of the file to be created at the end of the migration process.  
  
 If you have an existing memory-optimized filegroup, these fields will be pre-populated and you will not be able to change the values.  
  
 Rename the original table as  
 At the end of the migration process, a new memory-optimized table will be created with the current name of the table. To avoid a name conflict, the current table must be renamed. You may change that name in this field.  
  
 Estimated current memory cost (MB)  
 The Memory-Optimization Advisor estimates the amount of memory the new memory-optimized table will consume based on metadata of the disk-based table. The calculation of the table size is explained in [Table and Row Size in Memory-Optimized Tables](table-and-row-size-in-memory-optimized-tables.md).  
  
 If sufficient memory is not allotted, the migration process may fail.  
  
 Also copy table data to the new memory optimized table  
 Select this option if you wish to also move the data in the current table to the new memory-optimized table. If this option is not selected, the new memory-optimized table will be created with no rows.  
  
 The table will be migrated as a durable table by default  
 In-Memory OLTP supports non-durable tables with superior performance compared to durable memory-optimized tables. However, data in a non-durable table will be lost upon server restart.  
  
 If this option is selected, the Memory-Optimization Advisor will create a non-durable table instead of a durable table.  
  
> [!WARNING]  
>  Select this option only if you understand the risk of data loss associated with non-durable tables.  
  
 Click **Next** to continue.  
  
### Review Primary Key Conversion  
 The next screen is **Review Primary Key Conversion**. The Memory-Optimization Advisor will detect if there are one or more primary keys in the table, and populates the list of columns based on the primary key metadata. Otherwise, if you wish to migrate to a durable memory-optimized table, you must create a primary key.  
  
 If a primary key doesn't exist and the table is being migrated to a non-durable table, this screen will not appear.  
  
 For textual columns (columns with types `char`, `nchar`, `varchar`, and `nvarchar`) you must select an appropriate collation. In-Memory OLTP only supports BIN2 collations for columns on a memory-optimized table and it does not support collations with supplementary characters. See [Collations and Code Pages](../../database-engine/collations-and-code-pages.md) for information on the collations supported and the potential impact of a change in collation.  
  
 You can configure the following parameters for the primary key:  
  
 Select a new name for this primary key  
 The primary key name for this table must be unique inside the database. You may change the name of the primary key here.  
  
 Select the type of this primary key  
 In-Memory OLTP supports two types of indexes on a memory-optimized table:  
  
-   A NONCLUSTERED HASH index. This index is best for indexes with many point lookups. You may configure the bucket count for this index in the **Bucket Count** field.  
  
-   A NONCLUSTERED index. This type of index is best for indexes with many range queries. You may configure the sort order for each column in the **Sort column and order** list.  
  
 To understand the type of index best for your primary key, see [Hash Indexes](../../database-engine/hash-indexes.md).  
  
 Click **Next** after you make your primary key choices.  
  
### Review Index Conversion  
 The next page is **Review Index Conversion**. The Memory-Optimization Advisor will detect if there are one or more indexes in the table, and populates the list of columns and data type. The parameters you can configure in the **Review Index Conversion** page are similar to the previous, **Review Primary Key Conversion** page.  
  
 If the table only has a primary key and it's being migrated to a durable table, this screen will not appear.  
  
 After you make a decision for every index in your table, click **Next**.  
  
### Verify Migration Actions  
 The next page is **Verify Migration Actions**. To script the migration operation, click **Script** to generate a [!INCLUDE[tsql](../../includes/tsql-md.md)] script. You may then modify and execute the script. Click **Migrate** to begin the table migration.  
  
 After the process is finished, refresh **Object Explorer** to see the new memory-optimized table and the old disk-based table. You can keep the old table or delete it at your convenience.  
  
## See Also  
 [Migrating to In-Memory OLTP](migrating-to-in-memory-oltp.md)  
  
  
