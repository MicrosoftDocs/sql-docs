---
title: "table_constraint (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/11/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CONSTRAINT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "table_constraint"
ms.assetid: ac2a11e0-cc77-4e27-b107-4fe5bc6f5195
author: VanMSFT
ms.author: vanto
manager: craigg
---
# ALTER TABLE table_constraint (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Specifies the properties of a PRIMARY KEY, UNIQUE, FOREIGN KEY, a CHECK constraint, or a DEFAULT definition added to a table by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
[ CONSTRAINT constraint_name ]   
{   
    { PRIMARY KEY | UNIQUE }   
        [ CLUSTERED | NONCLUSTERED ]   
        (column [ ASC | DESC ] [ ,...n ] )  
        [ WITH FILLFACTOR = fillfactor   
        [ WITH ( <index_option>[ , ...n ] ) ]  
        [ ON { partition_scheme_name ( partition_column_name ... )  
          | filegroup | "default" } ]   
    | FOREIGN KEY   
        ( column [ ,...n ] )  
        REFERENCES referenced_table_name [ ( ref_column [ ,...n ] ) ]   
        [ ON DELETE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ ON UPDATE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ NOT FOR REPLICATION ]   
    | CONNECTION
        ( { node_table TO node_table } 
          [ , {node_table TO node_table }]
          [ , ...n ]
        )
        [ ON DELETE NO ACTION]
    | DEFAULT constant_expression FOR column [ WITH VALUES ]   
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )  
}  
```  
  
## Arguments  
 CONSTRAINT  
 Specifies the start of a definition for a PRIMARY KEY, UNIQUE, FOREIGN KEY, or CHECK constraint, or a DEFAULT.  
  
 *constraint_name*  
 Is the name of the constraint. Constraint names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md), except that the name cannot start with a number sign (#). If constraint_name is not supplied, a system-generated name is assigned to the constraint.  
  
 PRIMARY KEY  
 Is a constraint that enforces entity integrity for a specified column or columns by using a unique index. Only one PRIMARY KEY constraint can be created for each table.  
  
 UNIQUE  
 Is a constraint that provides entity integrity for a specified column or columns by using a unique index.  
  
 CLUSTERED | NONCLUSTERED  
 Specifies that a clustered or nonclustered index is created for the PRIMARY KEY or UNIQUE constraint. PRIMARY KEY constraints default to CLUSTERED. UNIQUE constraints default to NONCLUSTERED.  
  
 If a clustered constraint or index already exists on a table, CLUSTERED cannot be specified. If a clustered constraint or index already exists on a table, PRIMARY KEY constraints default to NONCLUSTERED.  
  
 Columns that are of the **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or **image** data types cannot be specified as columns for an index.  
  
 *column*  
 Is a column or list of columns specified in parentheses that are used in a new constraint.  
  
 [ **ASC** | DESC ]  
 Specifies the order in which the column or columns participating in table constraints are sorted. The default is ASC.  
  
 WITH FILLFACTOR **=**_fillfactor_  
 Specifies how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make each index page used to store the index data. User-specified *fillfactor* values can be from 1 through 100. If a value is not specified, the default is 0.  
  
> [!IMPORTANT]  
>  Documenting WITH FILLFACTOR = *fillfactor* as the only index option that applies to PRIMARY KEY or UNIQUE constraints is maintained for backward compatibility, but will not be documented in this manner in future releases. Other index options can be specified in the [index_option](../../t-sql/statements/alter-table-index-option-transact-sql.md) clause of ALTER TABLE.  
  
 ON { _partition\_scheme\_name_**(**_partition\_column\_name_**)** | _filegroup_| **"**default**"** }  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the storage location of the index created for the constraint. If *partition_scheme_name* is specified, the index is partitioned and the partitions are mapped to the filegroups that are specified by *partition_scheme_name*. If *filegroup* is specified, the index is created in the named filegroup. If **"**default**"** is specified or if ON is not specified at all, the index is created in the same filegroup as the table. If ON is specified when a clustered index is added for a PRIMARY KEY or UNIQUE constraint, the whole table is moved to the specified filegroup when the clustered index is created.  
  
 In this context, default is not a keyword; it is an identifier for the default filegroup and must be delimited, as in ON **"**default**"** or ON **[**default**]**. If **"**default**"** is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting.  
  
 FOREIGN KEY REFERENCES  
 Is a constraint that provides referential integrity for the data in the column. FOREIGN KEY constraints require that each value in the column exist in the specified column in the referenced table.  
  
 *referenced_table_name*  
 Is the table referenced by the FOREIGN KEY constraint.  
  
 *ref_column*  
 Is a column or list of columns in parentheses referenced by the new FOREIGN KEY constraint.  
  
 ON DELETE { **NO ACTION** | CASCADE | SET NULL | SET DEFAULT }  
 Specifies what action happens to rows in the table that is altered, if those rows have a referential relationship and the referenced row is deleted from the parent table. The default is NO ACTION.  
  
 NO ACTION  
 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] raises an error and the delete action on the row in the parent table is rolled back.  
  
 CASCADE  
 Corresponding rows are deleted from the referencing table if that row is deleted from the parent table.  
  
 SET NULL  
 All the values that make up the foreign key are set to NULL when the corresponding row in the parent table is deleted. For this constraint to execute, the foreign key columns must be nullable.  
  
 SET DEFAULT  
 All the values that comprise the foreign key are set to their default values when the corresponding row in the parent table is deleted. For this constraint to execute, all foreign key columns must have default definitions. If a column is nullable and there is no explicit default value set, NULL becomes the implicit default value of the column.  
  
 Do not specify CASCADE if the table will be included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 ON DELETE CASCADE cannot be defined if an INSTEAD OF trigger ON DELETE already exists on the table that is being altered.  
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the **ProductVendor** table has a referential relationship with the **Vendor** table. The **ProductVendor.VendorID** foreign key references the **Vendor.VendorID** primary key.  
  
 If a DELETE statement is executed on a row in the **Vendor** table and an ON DELETE CASCADE action is specified for **ProductVendor.VendorID**, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the **ProductVendor** table. If any exist, the dependent rows in the **ProductVendor** table will be deleted, in addition to the row referenced in the **Vendor** table.  
  
 Conversely, if NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the delete action on the **Vendor** row when there is at least one row in the **ProductVendor** table that references it.  
  
 ON UPDATE { **NO ACTION** | CASCADE | SET NULL | SET DEFAULT }  
 Specifies what action happens to rows in the table altered when those rows have a referential relationship and the referenced row is updated in the parent table. The default is NO ACTION.  
  
 NO ACTION  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error, and the update action on the row in the parent table is rolled back.  
  
 CASCADE  
 Corresponding rows are updated in the referencing table when that row is updated in the parent table.  
  
 SET NULL  
 All the values that make up the foreign key are set to NULL when the corresponding row in the parent table is updated. For this constraint to execute, the foreign key columns must be nullable.  
  
 SET DEFAULT  
 All the values that make up the foreign key are set to their default values when the corresponding row in the parent table is updated. For this constraint to execute, all foreign key columns must have default definitions. If a column is nullable, and there is no explicit default value set, NULL becomes the implicit default value of the column.  
  
 Do not specify CASCADE if the table will be included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 ON UPDATE CASCADE, SET NULL, or SET DEFAULT cannot be defined if an INSTEAD OF trigger ON UPDATE already exists on the table that is being altered.  
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the **ProductVendor** table has a referential relationship with the **Vendor** table. The **ProductVendor.VendorID** foreign key references the **Vendor.VendorID** primary key.  
  
 If an UPDATE statement is executed on a row in the **Vendor** table and an ON UPDATE CASCADE action is specified for **ProductVendor.VendorID**, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the **ProductVendor** table. If any exist, the dependent row in the **ProductVendor** table will be updated, as well as the row referenced in the **Vendor** table.  
  
 Conversely, if NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the update action on the **Vendor** row when there is at least one row in the **ProductVendor** table that references it.  
  
 NOT FOR REPLICATION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Can be specified for FOREIGN KEY constraints and CHECK constraints. If this clause is specified for a constraint, the constraint is not enforced when replication agents perform insert, update, or delete operations.  

 CONNECTION
 Specifies the pair of node tables that the given edge constraint is allowed to connect.  
 
 DEFAULT  
 Specifies the default value for the column. DEFAULT definitions can be used to provide values for a new column in the existing rows of data. DEFAULT definitions cannot be added to columns that have a **timestamp** data type, an IDENTITY property, an existing DEFAULT definition, or a bound default. If the column has an existing default, the default must be dropped before the new default can be added. If a default value is specified for a user-defined type column, the type should support an implicit conversion from *constant_expression* to the user-defined type. To maintain compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a constraint name can be assigned to a DEFAULT.  
  
 *constant_expression*  
 Is a literal value, a NULL, or a system function that is used as the default column value. If *constant_expression* is used in conjunction with a column defined to be of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] user-defined type, the implementation of the type must support an implicit conversion from the *constant_expression* to the user-defined type.  
  
 FOR *column*  
 Specifies the column associated with a table-level DEFAULT definition.  
  
 WITH VALUES  
 When adding a column AND a DEFAULT constraint, if the column allows NULLS using WITH VALUES will, for existing rows, set the new column's value to the value given in DEFAULT *constant_expression*. 
 If the column being added does not allow NULLS, for existing rows, the column's value will always be set to the value given in the DEFAULT *constant expression*. 
 Starting in SQL Server 2012 this may be a meta data operation [adding-not-null-columns-as-an-online-operation](alter-table-transact-sql.md?view=sql-server-2017#adding-not-null-columns-as-an-online-operation).
 If this is used when the related column isn't also being added then it has no effect. 
  
 CHECK  
 Is a constraint that enforces domain integrity by limiting the possible values that can be entered into a column or columns.  
  
 *logical_expression*  
 Is a logical expression used in a CHECK constraint and returns TRUE or FALSE. *logical_expression* used with CHECK constraints cannot reference another table but can reference other columns in the same table for the same row. The expression cannot reference an alias data type.  
  
## Remarks  
 When FOREIGN KEY or CHECK constraints are added, all existing data is verified for constraint violations unless the WITH NOCHECK option is specified. If any violations occur, ALTER TABLE fails and an error is returned. When a new PRIMARY KEY or UNIQUE constraint is added to an existing column, the data in the column or columns must be unique. If duplicate values are found, ALTER TABLE fails. The WITH NOCHECK option has no effect when PRIMARY KEY or UNIQUE constraints are added.  
  
 Each PRIMARY KEY and UNIQUE constraint generates an index. The number of UNIQUE and PRIMARY KEY constraints cannot cause the number of indexes on the table to exceed 999 nonclustered indexes and 1 clustered index. Foreign key constraints do not automatically generate an index. However, foreign key columns are frequently used in join criteria in queries by matching the column or columns in the foreign key constraint of one table with the primary or unique key column or columns in the other table. An index on the foreign key columns enables the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to quickly find related data in the foreign key table.  
  
## Examples  
 For examples, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
  
