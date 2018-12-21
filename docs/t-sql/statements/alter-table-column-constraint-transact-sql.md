---
title: "column_constraint (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/05/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "column_constraint"
  - "column_constraint_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER TABLE statement"
  - "constraints [SQL Server], properties"
  - "constraints [SQL Server], definitions"
  - "column_constraint"
ms.assetid: 8119b7c7-e93b-4de5-8f71-c3b7c70b993c
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER TABLE column_constraint (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Specifies the properties of a PRIMARY KEY, FOREIGN KEY, UNIQUE, or CHECK constraint that is part of a new column definition added to a table by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
[ CONSTRAINT constraint_name ]   
{   
    [ NULL | NOT NULL ]   
    { PRIMARY KEY | UNIQUE }   
        [ CLUSTERED | NONCLUSTERED ]   
        [ WITH FILLFACTOR = fillfactor ]   
        [ WITH ( index_option [, ...n ] ) ]  
        [ ON { partition_scheme_name (partition_column_name)   
            | filegroup | "default" } ]   
    | [ FOREIGN KEY ]   
        REFERENCES [ schema_name . ] referenced_table_name   
            [ ( ref_column ) ]   
        [ ON DELETE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ ON UPDATE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ NOT FOR REPLICATION ]   
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )  
}  
```  
  
## Arguments  
 CONSTRAINT  
 Specifies the start of the definition for a PRIMARY KEY, UNIQUE, FOREIGN KEY, or CHECK constraint.  
  
 *constraint_name*  
 Is the name of the constraint. Constraint names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md), except that the name cannot start with a number sign (#). If *constraint_name* is not supplied, a system-generated name is assigned to the constraint.  
  
 NULL | NOT NULL  
 Specifies whether the column can accept null values. Columns that do not allow null values can be added only if they have a default specified. If the new column allows null values and no default is specified, the new column contains NULL for each row in the table. If the new column allows null values and a default definition is added with the new column, the WITH VALUES option can be used to store the default value in the new column for each existing row in the table.  
  
 If the new column does not allow null values, a DEFAULT definition must be added with the new column. The new column automatically loads with the default value in the new columns in each existing row.  
  
 When the addition of a column requires physical changes to the data rows of a table, such as adding DEFAULT values to each row, locks are held on the table while ALTER TABLE runs. This affects the ability to change the content of the table while the lock is in place. In contrast, adding a column that allows null values and does not specify a default value is a metadata operation only, and involves no locks.  
  
 When you use CREATE TABLE or ALTER TABLE, database and session settings influence and possibly override the nullability of the data type that is used in a column definition. We recommend that you always explicitly define noncomputed columns as NULL or NOT NULL or, if you use a user-defined data type, that you allow the column to use the default nullability of the data type. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).  
  
 PRIMARY KEY  
 Is a constraint that enforces entity integrity for a specified column or columns by using a unique index. Only one PRIMARY KEY constraint can be created for each table.  
  
 UNIQUE  
 Is a constraint that provides entity integrity for a specified column or columns by using a unique index.  
  
 CLUSTERED | NONCLUSTERED  
 Specifies that a clustered or nonclustered index is created for the PRIMARY KEY or UNIQUE constraint. PRIMARY KEY constraints default to CLUSTERED. UNIQUE constraints default to NONCLUSTERED.  
  
 If a clustered constraint or index already exists on a table, CLUSTERED cannot be specified. If a clustered constraint or index already exists on a table, PRIMARY KEY constraints default to NONCLUSTERED.  
  
 Columns that are of the **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or **image** data types cannot be specified as columns for an index.  
  
 WITH FILLFACTOR **=**_fillfactor_  
 Specifies how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make each index page used to store the index data. User-specified fill factor values can be from 1 through 100. If a value is not specified, the default is 0.  
  
> [!IMPORTANT]  
>  Documenting WITH FILLFACTOR = *fillfactor* as the only index option that applies to PRIMARY KEY or UNIQUE constraints is maintained for backward compatibility, but will not be documented in this manner in future releases. Other index options can be specified in the [index_option](../../t-sql/statements/alter-table-index-option-transact-sql.md) clause of ALTER TABLE.  
  
 ON { _partition_scheme_name_**(**_partition_column_name_**)** | *filegroup* | **"**default**"** } 
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the storage location of the index created for the constraint. If *partition_scheme_name* is specified, the index is partitioned and the partitions are mapped to the filegroups that are specified by *partition_scheme_name*. If *filegroup* is specified, the index is created in the named filegroup. If **"**default**"** is specified or if ON is not specified at all, the index is created in the same filegroup as the table. If ON is specified when a clustered index is added for a PRIMARY KEY or UNIQUE constraint, the whole table is moved to the specified filegroup when the clustered index is created.  
  
 In this context, default, is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON **"**default**"** or ON **[**default**]**. If **"**default**"** is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 FOREIGN KEY REFERENCES  
 Is a constraint that provides referential integrity for the data in the column. FOREIGN KEY constraints require that each value in the column exist in the specified column in the referenced table.  
  
 *schema_name*  
 Is the name of the schema to which the table referenced by the FOREIGN KEY constraint belongs.  
  
 *referenced_table_name*  
 Is the table referenced by the FOREIGN KEY constraint.  
  
 *ref_column*  
 Is a column in parentheses referenced by the new FOREIGN KEY constraint.  
  
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
  
 The ON DELETE CASCADE cannot be defined if an INSTEAD OF trigger ON DELETE already exists on the table that is being altered.  
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the **ProductVendor** table has a referential relationship with the **Vendor** table. The **ProductVendor.VendorID** foreign key references the **Vendor.VendorID** primary key.  
  
 If a DELETE statement is executed on a row in the **Vendor** table, and an ON DELETE CASCADE action is specified for **ProductVendor.VendorID**, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the **ProductVendor** table. If any exist, the dependent rows in the **ProductVendor** table will be deleted, in addition to the row referenced in the **Vendor** table.  
  
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
 All the values that make up the foreign key are set to their default values when the corresponding row in the parent table is updated. For this constraint to execute, all foreign key columns must have default definitions. If a column is nullable and there is no explicit default value set, NULL becomes the implicit default value of the column.  
  
 Do not specify CASCADE if the table will be included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 ON UPDATE CASCADE, SET NULL, or SET DEFAULT cannot be defined if an INSTEAD OF trigger ON UPDATE already exists on the table that is being altered.  
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the **ProductVendor** table has a referential relationship with the **Vendor** table. The **ProductVendor.VendorID** foreign key references the **Vendor.VendorID** primary key.  
  
 If an UPDATE statement is executed on a row in the **Vendor** table and an ON UPDATE CASCADE action is specified for **ProductVendor.VendorID**, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the **ProductVendor** table. If any exist, the dependent row in the **ProductVendor** table will be updated, in addition to the row referenced in the **Vendor** table.  
  
 Conversely, if NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the update action on the **Vendor** row when there is at least one row in the **ProductVendor** table that references it.  
  
 NOT FOR REPLICATION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Can be specified for FOREIGN KEY constraints and CHECK constraints. If this clause is specified for a constraint, the constraint is not enforced when replication agents perform insert, update, or delete operations.  
  
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
 [column_definition &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-column-definition-transact-sql.md)  
  
  
