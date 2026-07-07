---
title: table_constraint (Transact-SQL)
description: Syntax for the table_constraint argument of the ALTER TABLE T-SQL command.
author: VanMSFT
ms.author: vanto
ms.date: 04/12/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CONSTRAINT_TSQL"
helpviewer_keywords:
  - "table_constraint"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# ALTER TABLE table_constraint (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricdw-fabricsqldb.md)]

Use `ALTER TABLE` to specify the properties of a `PRIMARY KEY`, `UNIQUE`, `FOREIGN KEY`, `CHECK` constraint, or `DEFAULT` definition that you add to a table by using [ALTER TABLE (Transact-SQL)](alter-table-transact-sql.md).    

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
[ CONSTRAINT constraint_name ]   
{   
    { PRIMARY KEY | UNIQUE }   
        [ CLUSTERED | NONCLUSTERED ]   
        (column [ ASC | DESC ] [ ,...n ] )  
        [ WITH FILLFACTOR = fillfactor ]
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
        [ ON DELETE { NO ACTION | CASCADE } ]
    | DEFAULT constant_expression FOR column [ WITH VALUES ]   
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )  
}  
```  

## Arguments

#### CONSTRAINT

 Specifies the start of a definition for a `PRIMARY KEY`, `UNIQUE`, `FOREIGN KEY`, or `CHECK` constraint, or a `DEFAULT`.  

#### constraint_name

The name of the constraint. Constraint names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md), except that the name can't start with a number sign (`#`). If you don't supply `constraint_name`, the system assigns a generated name to the constraint.

#### PRIMARY KEY

A constraint that enforces entity integrity for a specified column or columns by using a unique index. You can create only one `PRIMARY KEY` constraint for each table.

#### UNIQUE

A constraint that provides entity integrity for a specified column or columns by using a unique index.

#### CLUSTERED | NONCLUSTERED

 Specifies that a clustered or nonclustered index is created for the `PRIMARY KEY` or `UNIQUE` constraint. `PRIMARY KEY` constraints default to `CLUSTERED`. `UNIQUE` constraints default to `NONCLUSTERED`.  

 If a clustered constraint or index already exists on a table, you can't specify `CLUSTERED`. If a clustered constraint or index already exists on a table, `PRIMARY KEY` constraints default to `NONCLUSTERED`.  

 You can't specify columns that are of the **ntext**, **text**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or **image** data types as columns for an index.  

#### column

A column or list of columns specified in parentheses that you use in a new constraint.

#### [ ASC | DESC ]

 Specifies the order in which the column or columns participating in table constraints are sorted. The default is ascending sort order (`ASC`).

#### WITH FILLFACTOR = _fillfactor_

 Specifies how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make each index page used to store the index data. User-specified *fillfactor* values can be from 1 through 100. If you don't specify a value, the default is `0`.

 For backward compatibility, this documentation includes `WITH FILLFACTOR = <fillfactor>` as the only index option that applies to `PRIMARY KEY` or `UNIQUE` constraints. This syntax won't be documented in future releases. You can specify other index options in the [index_option](alter-table-index-option-transact-sql.md) clause of `ALTER TABLE`.

#### ON { _partition\_scheme\_name_(_partition\_column\_name_) | _filegroup_| "default" }

 **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.  

 Specifies the storage location of the index created for the constraint. If you specify *partition_scheme_name*, the index is partitioned and the partitions are mapped to the filegroups that *partition_scheme_name* specifies. If you specify *filegroup*, the index is created in the named filegroup. If you specify **"**default**"** or if you don't specify `ON` at all, the index is created in the same filegroup as the table. If you specify `ON` when you add a clustered index for a `PRIMARY KEY` or `UNIQUE` constraint, the whole table is moved to the specified filegroup when the clustered index is created.  

 In this context, default isn't a keyword; it's an identifier for the default filegroup and must be delimited, as in `ON` **"**default**"** or `ON` **[**default**]**. If you specify **"**default**"**, the `QUOTED_IDENTIFIER` option must be `ON` for the current session. This is the default setting.  

#### FOREIGN KEY REFERENCES

A constraint that provides referential integrity for the data in the column. `FOREIGN KEY` constraints require that each value in the column exist in the specified column in the referenced table.

#### referenced_table_name

The table referenced by the `FOREIGN KEY` constraint.

#### ref_column

A column or list of columns in parentheses referenced by the new `FOREIGN KEY` constraint.

#### ON DELETE { NO ACTION \| CASCADE \| SET NULL \| SET DEFAULT }

 Specifies what action happens to rows in the table that you alter, if those rows have a referential relationship and you delete the referenced row from the parent table. The default is `NO ACTION`.  

#### NO ACTION

 The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] raises an error and rolls back the delete action on the row in the parent table.  

#### CASCADE

 Deletes corresponding rows from the referencing table if you delete that row from the parent table.

#### SET NULL

 Sets all the values that make up the foreign key to `NULL` when you delete the corresponding row in the parent table. For this constraint to execute, the foreign key columns must be nullable.

#### SET DEFAULT

 Sets all the values that comprise the foreign key to their default values when you delete the corresponding row in the parent table. For this constraint to execute, all foreign key columns must have default definitions. If a column is nullable and there's no explicit default value set, `NULL` becomes the implicit default value of the column.

 Don't specify `CASCADE` if the table is included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).

 You can't define `ON DELETE CASCADE` if an `INSTEAD OF` trigger `ON DELETE` already exists on the table that you're altering.  

 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the `ProductVendor` table has a referential relationship with the `Vendor` table. The `ProductVendor.VendorID` foreign key references the `Vendor.VendorID` primary key.  

  If you execute a `DELETE` statement on a row in the `Vendor` table and specify an `ON DELETE CASCADE` action for `ProductVendor.VendorID`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the `ProductVendor` table. If any exist, the dependent rows in the `ProductVendor` table are deleted, in addition to the row referenced in the `Vendor` table.    

  Conversely, if you specify `NO ACTION`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the delete action on the `Vendor` row when there's at least one row in the `ProductVendor` table that references it.    

#### ON UPDATE { NO ACTION \| CASCADE \| SET NULL \| SET DEFAULT }

  Specifies what action happens to rows in the table you alter when those rows have a referential relationship and you update the referenced row in the parent table. The default is `NO ACTION`.    

#### NO ACTION

 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error, and the update action on the row in the parent table is rolled back.  

#### CASCADE

 Corresponding rows are updated in the referencing table when that row is updated in the parent table.  

#### SET NULL

  Sets all the values that make up the foreign key to `NULL` when you update the corresponding row in the parent table. For this constraint to execute, the foreign key columns must be nullable.    

#### SET DEFAULT

 All the values that make up the foreign key are set to their default values when the corresponding row in the parent table is updated. For this constraint to execute, all foreign key columns must have default definitions. If a column is nullable, and there is no explicit default value set, `NULL` becomes the implicit default value of the column.  

 Don't specify `CASCADE` if the table is included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  

 `ON UPDATE CASCADE`, `SET NULL`, or `SET DEFAULT` cannot be defined if an `INSTEAD OF` trigger `ON UPDATE` already exists on the table that is being altered.  

 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the `ProductVendor` table has a referential relationship with the `Vendor` table. The `ProductVendor.VendorID` foreign key references the `Vendor.VendorID` primary key.  

 If you execute an `UPDATE` statement on a row in the `Vendor` table and specify an `ON UPDATE CASCADE` action for `ProductVendor.VendorID`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the `ProductVendor` table. If any exist, the dependent row in the `ProductVendor` table is updated, as well as the row referenced in the `Vendor` table.  

 Conversely, if you specify `NO ACTION`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the update action on the `Vendor` row when there's at least one row in the `ProductVendor` table that references it.  

#### NOT FOR REPLICATION

 **Applies to**: [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.

 If you specify this clause for a constraint, replication agents don't enforce the constraint when they perform write operations. You can specify this clause for `FOREIGN KEY` constraints and `CHECK` constraints.    

#### CONNECTION

 Specifies the pair of node tables that the given edge constraint is allowed to connect. `ON DELETE` specifies what happens to the rows in the edge table when the nodes that the edge connects are deleted. 

#### DEFAULT

 Specifies the default value for the column. Use `DEFAULT` definitions to provide values for a new column in the existing rows of data. You can't add `DEFAULT` definitions to columns that have a **timestamp** data type, an `IDENTITY` property, an existing `DEFAULT` definition, or a bound default. If the column has an existing default, you must drop the default before you can add a new default. If you specify a default value for a user-defined type column, the type should support an implicit conversion from *constant_expression* to the user-defined type. To maintain compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can assign a constraint name to a `DEFAULT`.  

#### constant_expression

A literal value, a `NULL`, or a system function that you use as the default column value. If you use *constant_expression* in conjunction with a column defined to be of a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] user-defined type, the implementation of the type must support an implicit conversion from the *constant_expression* to the user-defined type.

#### FOR column

Specifies the column associated with a table-level `DEFAULT` definition.  

#### WITH VALUES

- When you add a column and a `DEFAULT` constraint, if the column allows nulls, using `WITH VALUES` sets the new column's value for existing rows to the value given in `DEFAULT` *constant_expression*.

- If the column you're adding doesn't allow nulls, the column's value for existing rows is always set to the value given in the `DEFAULT` *constant expression*.

In SQL Server 2012 and later versions, this operation can be a metadata operation [adding-not-null-columns-as-an-online-operation](alter-table-transact-sql.md#adding-not-null-columns-as-an-online-operation).

If you use `WITH VALUES` when the related column isn't also being added, it has no effect. 

#### CHECK

A constraint that enforces domain integrity by limiting the possible values that can be entered into a column or columns.

#### logical_expression

A logical expression used in a `CHECK` constraint that returns `TRUE` or `FALSE`. *logical_expression* used with `CHECK` constraints can't reference another table but can reference other columns in the same table for the same row. The expression can't reference an alias data type.

## Remarks

 When you add `FOREIGN KEY` or `CHECK` constraints, the system checks all existing data for constraint violations unless you specify the `WITH NOCHECK` option. If any violations occur, `ALTER TABLE` fails and returns an error. When you add a new `PRIMARY KEY` or `UNIQUE` constraint to an existing column, the data in the column or columns must be unique. If duplicate values are found, `ALTER TABLE` fails. The `WITH NOCHECK` option has no effect when you add `PRIMARY KEY` or `UNIQUE` constraints.  

  Each `PRIMARY KEY` and `UNIQUE` constraint generates an index. The number of `UNIQUE` and `PRIMARY KEY` constraints can't cause the number of indexes on the table to exceed 999 nonclustered indexes and 1 clustered index. Foreign key constraints don't automatically generate an index. However, you frequently use foreign key columns in join criteria in queries by matching the column or columns in the foreign key constraint of one table with the primary or unique key column or columns in the other table. An index on the foreign key columns enables the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to quickly find related data in the foreign key table.    

 In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, resumable operations support adding table constraints for primary key and unique key constraints. For more information on enabling and using resumable `ALTER TABLE ADD CONSTRAINT` operations, see [Resumable add table constraints](../../relational-databases/security/resumable-add-table-constraints.md).

  [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] supports `ADD` or `DROP` `PRIMARY KEY`, `UNIQUE`, and `FOREIGN_KEY` column constraints, but only if you specify the `NOT ENFORCED` option. [!INCLUDE [fabric-dw](../../includes/fabric-dw.md)] blocks all other `ALTER TABLE` operations.  

## Examples

 For examples, see [ALTER TABLE (Transact-SQL)](alter-table-transact-sql.md#examples).

## Related content

- [ALTER TABLE (Transact-SQL)](alter-table-transact-sql.md)
- [ALTER TABLE column_constraint (Transact-SQL)](alter-table-column-constraint-transact-sql.md)
