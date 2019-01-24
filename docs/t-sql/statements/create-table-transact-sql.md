---
title: "CREATE TABLE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "FILESTREAM_TSQL"
  - "TABLE"
  - "CREATE_TABLE_TSQL"
  - "CREATE TABLE"
  - "FILESTREAM"
  - "TABLE_TSQL"
  - "FILESTREAM_ON"
  - "FILESTREAM_ON_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CHECK constraints"
  - "global temporary tables [SQL Server]"
  - "local temporary tables [SQL Server]"
  - "size [SQL Server], tables"
  - "UNIQUE constraints [SQL Server], creating"
  - "constraints [SQL Server], columns"
  - "maximum number of indexes per table"
  - "number of tables per database"
  - "number of indexes per table"
  - "table creation [SQL Server], CREATE TABLE"
  - "temporary tables [SQL Server], creating"
  - "table size [SQL Server]"
  - "nullability [SQL Server]"
  - "partitioned tables [SQL Server], creating"
  - "DEFAULT definition"
  - "null values [SQL Server], columns"
  - "number of rows per table"
  - "maximum number of columns per table"
  - "FOREIGN KEY constraints, creating"
  - "PRIMARY KEY constraints"
  - "number of bytes per row"
  - "CREATE TABLE statement"
  - "number of columns per table"
  - "maximum number of bytes per row"
ms.assetid: 1e068443-b9ea-486a-804f-ce7b6e048e8b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE TABLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

> [!div class="nextstepaction"]
> [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

Creates a new table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
> [!NOTE]   
>  For [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] syntax, see [CREATE TABLE (Azure SQL Data Warehouse)](../../t-sql/statements/create-table-azure-sql-data-warehouse.md).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Simple Syntax  
  
```  
--Simple CREATE TABLE Syntax (common if not using options)  
CREATE TABLE   
    [ database_name . [ schema_name ] . | schema_name . ] table_name   
    ( { <column_definition> } [ ,...n ] )   
[ ; ]  
```  
  
## Full Syntax  
  
```  
--Disk-Based CREATE TABLE Syntax  
CREATE TABLE   
    [ database_name . [ schema_name ] . | schema_name . ] table_name   
    [ AS FileTable ]  
    ( {   <column_definition>   
        | <computed_column_definition>    
        | <column_set_definition>   
        | [ <table_constraint> ] [ ,... n ] 
        | [ <table_index> ] }  
          [ ,...n ]    
          [ PERIOD FOR SYSTEM_TIME ( system_start_time_column_name   
             , system_end_time_column_name ) ]  
      )  
    [ ON { partition_scheme_name ( partition_column_name )   
           | filegroup   
           | "default" } ]   
    [ TEXTIMAGE_ON { filegroup | "default" } ]   
    [ FILESTREAM_ON { partition_scheme_name   
           | filegroup   
           | "default" } ]  
    [ WITH ( <table_option> [ ,...n ] ) ]  
[ ; ]  
  
<column_definition> ::=  
column_name <data_type>  
    [ FILESTREAM ]  
    [ COLLATE collation_name ]   
    [ SPARSE ]  
    [ MASKED WITH ( FUNCTION = ' mask_function ') ]  
    [ CONSTRAINT constraint_name [ DEFAULT constant_expression ] ]   
    [ IDENTITY [ ( seed,increment ) ]  
    [ NOT FOR REPLICATION ]   
    [ GENERATED ALWAYS AS ROW { START | END } [ HIDDEN ] ]   
    [ NULL | NOT NULL ]  
    [ ROWGUIDCOL ]  
    [ ENCRYPTED WITH   
        ( COLUMN_ENCRYPTION_KEY = key_name ,  
          ENCRYPTION_TYPE = { DETERMINISTIC | RANDOMIZED } ,   
          ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'  
        ) ]  
    [ <column_constraint> [, ...n ] ]   
    [ <column_index> ]  
  
<data type> ::=   
[ type_schema_name . ] type_name   
    [ ( precision [ , scale ] | max |   
        [ { CONTENT | DOCUMENT } ] xml_schema_collection ) ]   
  
<column_constraint> ::=   
[ CONSTRAINT constraint_name ]   
{     { PRIMARY KEY | UNIQUE }   
        [ CLUSTERED | NONCLUSTERED ]   
        [   
            WITH FILLFACTOR = fillfactor    
          | WITH ( < index_option > [ , ...n ] )   
        ]   
        [ ON { partition_scheme_name ( partition_column_name )   
            | filegroup | "default" } ]  
  
  | [ FOREIGN KEY ]   
        REFERENCES [ schema_name . ] referenced_table_name [ ( ref_column ) ]   
        [ ON DELETE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ ON UPDATE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ NOT FOR REPLICATION ]   
  
  | CHECK [ NOT FOR REPLICATION ] ( logical_expression )   
}   
  
<column_index> ::=   
 INDEX index_name [ CLUSTERED | NONCLUSTERED ]  
    [ WITH ( <index_option> [ ,... n ] ) ]  
    [ ON { partition_scheme_name (column_name )   
         | filegroup_name  
         | default   
         }  
    ]   
    [ FILESTREAM_ON { filestream_filegroup_name | partition_scheme_name | "NULL" } ]  
  
<computed_column_definition> ::=  
column_name AS computed_column_expression   
[ PERSISTED [ NOT NULL ] ]  
[   
    [ CONSTRAINT constraint_name ]  
    { PRIMARY KEY | UNIQUE }  
        [ CLUSTERED | NONCLUSTERED ]  
        [   
            WITH FILLFACTOR = fillfactor   
          | WITH ( <index_option> [ , ...n ] )  
        ]  
        [ ON { partition_scheme_name ( partition_column_name )   
        | filegroup | "default" } ]  
  
    | [ FOREIGN KEY ]   
        REFERENCES referenced_table_name [ ( ref_column ) ]   
        [ ON DELETE { NO ACTION | CASCADE } ]   
        [ ON UPDATE { NO ACTION } ]   
        [ NOT FOR REPLICATION ]   
  
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )   
]   
  
<column_set_definition> ::=  
column_set_name XML COLUMN_SET FOR ALL_SPARSE_COLUMNS  
  
< table_constraint > ::=  
[ CONSTRAINT constraint_name ]   
{   
    { PRIMARY KEY | UNIQUE }   
        [ CLUSTERED | NONCLUSTERED ]   
        (column [ ASC | DESC ] [ ,...n ] )   
        [   
            WITH FILLFACTOR = fillfactor   
           |WITH ( <index_option> [ , ...n ] )   
        ]  
        [ ON { partition_scheme_name (partition_column_name)  
            | filegroup | "default" } ]   
    | FOREIGN KEY   
        ( column [ ,...n ] )   
        REFERENCES referenced_table_name [ ( ref_column [ ,...n ] ) ]   
        [ ON DELETE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ ON UPDATE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]   
        [ NOT FOR REPLICATION ]   
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )  
 
< table_index > ::=   
{  
    {  
      INDEX index_name [ CLUSTERED | NONCLUSTERED ]   
         (column_name [ ASC | DESC ] [ ,... n ] )   
    | INDEX index_name CLUSTERED COLUMNSTORE  
    | INDEX index_name [ NONCLUSTERED ] COLUMNSTORE (column_name [ ,... n ] )  
    }  
    [ WITH ( <index_option> [ ,... n ] ) ]   
    [ ON { partition_scheme_name (column_name )   
         | filegroup_name  
         | default   
         }  
    ]   
    [ FILESTREAM_ON { filestream_filegroup_name | partition_scheme_name | "NULL" } ]  
  
}   

<table_option> ::=  
{  
    [DATA_COMPRESSION = { NONE | ROW | PAGE }  
      [ ON PARTITIONS ( { <partition_number_expression> | <range> }   
      [ , ...n ] ) ]]  
    [ FILETABLE_DIRECTORY = <directory_name> ]   
    [ FILETABLE_COLLATE_FILENAME = { <collation_name> | database_default } ]  
    [ FILETABLE_PRIMARY_KEY_CONSTRAINT_NAME = <constraint_name> ]  
    [ FILETABLE_STREAMID_UNIQUE_CONSTRAINT_NAME = <constraint_name> ]  
    [ FILETABLE_FULLPATH_UNIQUE_CONSTRAINT_NAME = <constraint_name> ]  
    [ SYSTEM_VERSIONING = ON [ ( HISTORY_TABLE = schema_name . history_table_name  
        [, DATA_CONSISTENCY_CHECK = { ON | OFF } ] ) ] ]  
    [ REMOTE_DATA_ARCHIVE =   
      {   
          ON [ ( <table_stretch_options> [,...n] ) ]  
        | OFF ( MIGRATION_STATE = PAUSED )   
      }   
    ]  
}  
  
<table_stretch_options> ::=  
{  
     [ FILTER_PREDICATE = { null | table_predicate_function } , ]  
       MIGRATION_STATE = { OUTBOUND | INBOUND | PAUSED }  
 }  
  
<index_option> ::=  
{   
    PAD_INDEX = { ON | OFF }   
  | FILLFACTOR = fillfactor   
  | IGNORE_DUP_KEY = { ON | OFF }   
  | STATISTICS_NORECOMPUTE = { ON | OFF }   
  | STATISTICS_INCREMENTAL = { ON | OFF }  
  | ALLOW_ROW_LOCKS = { ON | OFF}   
  | ALLOW_PAGE_LOCKS ={ ON | OFF}   
  | COMPRESSION_DELAY= {0 | delay [Minutes]}  
  | DATA_COMPRESSION = { NONE | ROW | PAGE | COLUMNSTORE | COLUMNSTORE_ARCHIVE }  
       [ ON PARTITIONS ( { <partition_number_expression> | <range> }   
       [ , ...n ] ) ]  
}  
<range> ::=   
<partition_number_expression> TO <partition_number_expression>  
```  
  
```  
--Memory optimized 
LE Syntax  
CREATE TABLE  
    [database_name . [schema_name ] . | schema_name . ] table_name  
    ( { <column_definition>  
    | [ <table_constraint> ] [ ,... n ]  
    | [ <table_index> ]  
      [ ,... n ] }   
      [ PERIOD FOR SYSTEM_TIME ( system_start_time_column_name   
        , system_end_time_column_name ) ]  
)  
    [ WITH ( <table_option> [ ,... n ] ) ]  
 [ ; ]  
  
<column_definition> ::=  
column_name <data_type>  
    [ COLLATE collation_name ]  
    [ GENERATED ALWAYS AS ROW { START | END } [ HIDDEN ] ]   
    [ NULL | NOT NULL ]  
[  
    [ CONSTRAINT constraint_name ] DEFAULT memory_optimized_constant_expression ]  
    | [ IDENTITY [ ( 1, 1 ) ]  
]  
    [ <column_constraint> ]  
    [ <column_index> ]  
  
<data type> ::=  
 [type_schema_name . ] type_name [ (precision [ , scale ]) ]  
  
<column_constraint> ::=  
 [ CONSTRAINT constraint_name ]  
{   
  { PRIMARY KEY | UNIQUE }    
      {   NONCLUSTERED   
        | NONCLUSTERED HASH WITH (BUCKET_COUNT = bucket_count)   
      }   
  | [ FOREIGN KEY ]   
        REFERENCES [ schema_name . ] referenced_table_name [ ( ref_column ) ]   
  | CHECK ( logical_expression )   
}  
  
< table_constraint > ::=  
 [ CONSTRAINT constraint_name ]  
{    
   { PRIMARY KEY | UNIQUE }  
     {   
       NONCLUSTERED (column [ ASC | DESC ] [ ,... n ])  
       | NONCLUSTERED HASH (column [ ,... n ] ) WITH ( BUCKET_COUNT = bucket_count )   
                    }   
    | FOREIGN KEY   
        ( column [ ,...n ] )   
        REFERENCES referenced_table_name [ ( ref_column [ ,...n ] ) ]   
    | CHECK ( logical_expression )   
}  
  
<column_index> ::=  
  INDEX index_name  
{ [ NONCLUSTERED ] | [ NONCLUSTERED ] HASH WITH (BUCKET_COUNT = bucket_count)  }  
  
<table_index> ::=  
  INDEX index_name  
{   [ NONCLUSTERED ] HASH (column [ ,... n ] ) WITH (BUCKET_COUNT = bucket_count)   
  | [ NONCLUSTERED ] (column [ ASC | DESC ] [ ,... n ] )   
      [ ON filegroup_name | default ]  
  | CLUSTERED COLUMNSTORE [WITH ( COMPRESSION_DELAY = {0 | delay [Minutes]})]  
      [ ON filegroup_name | default ]  
  
}  
  
<table_option> ::=  
{  
    MEMORY_OPTIMIZED = ON   
  | DURABILITY = {SCHEMA_ONLY | SCHEMA_AND_DATA}  
  | SYSTEM_VERSIONING = ON [ ( HISTORY_TABLE = schema_name . history_table_name  
        [, DATA_CONSISTENCY_CHECK = { ON | OFF } ] ) ]   
  
}  
```  
  
## Arguments  
 *database_name*  
 Is the name of the database in which the table is created. *database_name* must specify the name of an existing database. If not specified, *database_name* defaults to the current database. The login for the current connection must be associated with an existing user ID in the database specified by *database_name*, and that user ID must have CREATE TABLE permissions.  
  
 *schema_name*  
 Is the name of the schema to which the new table belongs.  
  
 *table_name*  
 Is the name of the new table. Table names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). *table_name* can be a maximum of 128 characters, except for local temporary table names (names prefixed with a single number sign (#)) that cannot exceed 116 characters.  
  
 AS FileTable 
 
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
 
 Creates the new table as a FileTable. You do not specify columns because a FileTable has a fixed schema. For more information about FileTables, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).  
  
 *column_name*  
 *computed_column_expression*  
 Is an expression that defines the value of a computed column. A computed column is a virtual column that is not physically stored in the table, unless the column is marked PERSISTED. The column is computed from an expression that uses other columns in the same table. For example, a computed column can have the definition: **cost** AS **price** \* **qty**. The expression can be a noncomputed column name, constant, function, variable, and any combination of these connected by one or more operators. The expression cannot be a subquery or contain alias data types.  
  
 Computed columns can be used in select lists, WHERE clauses, ORDER BY clauses, or any other locations in which regular expressions can be used, with the following exceptions:  
  
-   Computed columns must be marked PERSISTED to participate in a FOREIGN KEY or CHECK constraint.  
  
-   A computed column can be used as a key column in an index or as part of any PRIMARY KEY or UNIQUE constraint, if the computed column value is defined by a deterministic expression and the data type of the result is allowed in index columns.  
  
     For example, if the table has integer columns **a** and **b**, the computed column **a+b** may be indexed, but computed column **a+DATEPART(dd, GETDATE())** cannot be indexed because the value may change in subsequent invocations.  
  
-   A computed column cannot be the target of an INSERT or UPDATE statement.  
  
> [!NOTE]  
> Each row in a table can have different values for columns that are involved in a computed column; therefore, the computed column may not have the same value for each row.  
  
 Based on the expressions that are used, the nullability of computed columns is determined automatically by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. The result of most expressions is considered nullable even if only nonnullable columns are present, because possible underflows or overflows also produce NULL results. Use the COLUMNPROPERTY function with the **AllowsNull** property to investigate the nullability of any computed column in a table. An expression that is nullable can be turned into a nonnullable one by specifying ISNULL with the *check_expression* constant, where the constant is a nonnull value substituted for any NULL result. REFERENCES permission on the type is required for computed columns based on common language runtime (CLR) user-defined type expressions.  
  
 PERSISTED  
 Specifies that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] will physically store the computed values in the table, and update the values when any other columns on which the computed column depends are updated. Marking a computed column as PERSISTED lets you create an index on a computed column that is deterministic, but not precise. For more information, see [Indexes on Computed Columns](../../relational-databases/indexes/indexes-on-computed-columns.md). Any computed columns that are used as partitioning columns of a partitioned table must be explicitly marked PERSISTED. *computed_column_expression* must be deterministic when PERSISTED is specified.  
  
 ON { *partition_scheme* | *filegroup* | **"**default**"** }  

 Specifies the partition scheme or filegroup on which the table is stored. If *partition_scheme* is specified, the table is to be a partitioned table whose partitions are stored on a set of one or more filegroups specified in *partition_scheme*. If *filegroup* is specified, the table is stored in the named filegroup. The filegroup must exist within the database. If **"**default**"** is specified, or if ON is not specified at all, the table is stored on the default filegroup. The storage mechanism of a table as specified in CREATE TABLE cannot be subsequently altered.  
  
 ON {*partition_scheme* | *filegroup* | **"**default**"**} can also be specified in a PRIMARY KEY or UNIQUE constraint. These constraints create indexes. If *filegroup* is specified, the index is stored in the named filegroup. If **"**default**"** is specified, or if ON is not specified at all, the index is stored in the same filegroup as the table. If the PRIMARY KEY or UNIQUE constraint creates a clustered index, the data pages for the table are stored in the same filegroup as the index. If CLUSTERED is specified or the constraint otherwise creates a clustered index, and a *partition_scheme* is specified that differs from the *partition_scheme* or *filegroup* of the table definition, or vice-versa, only the constraint definition will be honored, and the other will be ignored.  
  
> [!NOTE]
>  In this context, default is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON **"**default**"** or ON **[**default**]**. If **"**default**"** is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
> 
> [!NOTE]
>  After you create a partitioned table, consider setting the LOCK_ESCALATION option for the table to AUTO. This can improve concurrency by enabling locks to escalate to partition (HoBT) level instead of the table. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
 TEXTIMAGE_ON { *filegroup*| **"**default**"** }  
 Indicates that the **text**, **ntext**, **image**, **xml**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and CLR user-defined type columns (including geometry and geography) are stored on the specified filegroup.  
  
 TEXTIMAGE_ON is not allowed if there are no large value columns in the table. TEXTIMAGE_ON cannot be specified if *partition_scheme* is specified. If **"**default**"** is specified, or if TEXTIMAGE_ON is not specified at all, the large value columns are stored in the default filegroup. The storage of any large value column data specified in CREATE TABLE cannot be subsequently altered.  

> [!NOTE]
> Varchar(max), nvarchar(max), varbinary(max), xml and large UDT values are stored directly in the data row, up to a limit of 8000 bytes and as long as the value can fit the record. If the value does not fit in the record, a pointer is sorted in-row and the rest is stored out of row in the LOB storage space. 0 is the default value.
>  TEXTIMAGE_ON only changes the location of the "LOB storage space", it does not affect when data is stored in-row. Use large value types out of row option of sp_tableoption to store the entire LOB value out of the row. 
> 
> 
> [!NOTE]
>  In this context, default is not a keyword. It is an identifier for the default filegroup and must be delimited, as in TEXTIMAGE_ON **"**default**"** or TEXTIMAGE_ON **[**default**]**. If **"**default**"** is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 FILESTREAM_ON { *partition_scheme_name* | filegroup | **"**default**"** } 
 
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Azure SQL Database does not support `FILESTREAM`.
 
 Specifies the filegroup for FILESTREAM data.  
  
 If the table contains FILESTREAM data and the table is partitioned, the FILESTREAM_ON clause must be included and must specify a partition scheme of FILESTREAM filegroups. This partition scheme must use the same partition function and partition columns as the partition scheme for the table; otherwise, an error is raised.  
  
 If the table is not partitioned, the FILESTREAM column cannot be partitioned. FILESTREAM data for the table must be stored in a single filegroup. This filegroup is specified in the FILESTREAM_ON clause.  
  
 If the table is not partitioned and the FILESTREAM_ON clause is not specified, the FILESTREAM filegroup that has the DEFAULT property set is used. If there is no FILESTREAM filegroup, an error is raised.  
  
-   As with ON and TEXTIMAGE_ON, the value set by using CREATE TABLE for FILESTREAM_ON cannot be changed, except in the following cases:  
  
-   A [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) statement converts a heap into a clustered index. In this case, a different FILESTREAM filegroup, partition scheme, or NULL can be specified.  
  
-   A [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md) statement converts a clustered index into a heap. In this case, a different FILESTREAM filegroup, partition scheme, or **"**default**"** can be specified.  
  
 The filegroup in the `FILESTREAM_ON <filegroup>` clause, or each FILESTREAM filegroup that is named in the partition scheme, must have one file defined for the filegroup. This file must be defined by using a [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?&tabs=sqlserver) or [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement; otherwise, an error is raised.  
  
 For related FILESTREAM topics, see [Binary Large Object &#40;Blob&#41; Data &#40;SQL Server&#41;](../../relational-databases/blob/binary-large-object-blob-data-sql-server.md).  
  
 [ _type\_schema\_name_**.** ] *type_name*  
 Specifies the data type of the column, and the schema to which it belongs. For disk-based tables, the data type can be one of the following:  
  
-   A system data type.  
  
-   An alias type based on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type. Alias data types are created with the CREATE TYPE statement before they can be used in a table definition. The NULL or NOT NULL assignment for an alias data type can be overridden during the CREATE TABLE statement. However, the length specification cannot be changed; the length for an alias data type cannot be specified in a CREATE TABLE statement.  
  
-   A CLR user-defined type. CLR user-defined types are created with the CREATE TYPE statement before they can be used in a table definition. To create a column on CLR user-defined type, REFERENCES permission is required on the type.  
  
 If *type_schema_name* is not specified, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] references *type_name* in the following order:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type.  
  
-   The default schema of the current user in the current database.  
  
-   The **dbo** schema in the current database.  
  
 For memory-optimized tables, see [Supported Data Types for In-Memory OLTP](../../relational-databases/in-memory-oltp/supported-data-types-for-in-memory-oltp.md) for a list of supported system types.  
  
 *precision*  
 Is the precision for the specified data type. For more information about valid precision values, see [Precision, Scale, and Length](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
 *scale*  
 Is the scale for the specified data type. For more information about valid scale values, see [Precision, Scale, and Length](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
 **max**  
 Applies only to the **varchar**, **nvarchar**, and **varbinary** data types for storing 2^31 bytes of character and binary data, and 2^30 bytes of Unicode data.  
  
 CONTENT  
 Specifies that each instance of the **xml** data type in *column_name* can contain multiple top-level elements. CONTENT applies only to the **xml** data type and can be specified only if *xml_schema_collection* is also specified. If not specified, CONTENT is the default behavior.  
  
 DOCUMENT  
 Specifies that each instance of the **xml** data type in *column_name* can contain only one top-level element. DOCUMENT applies only to the **xml** data type and can be specified only if *xml_schema_collection* is also specified.  
  
 *xml_schema_collection*  
 Applies only to the **xml** data type for associating an XML schema collection with the type. Before typing an **xml** column to a schema, the schema must first be created in the database by using [CREATE XML SCHEMA COLLECTION](../../t-sql/statements/create-xml-schema-collection-transact-sql.md).  
  
 DEFAULT  
 Specifies the value provided for the column when a value is not explicitly supplied during an insert. DEFAULT definitions can be applied to any columns except those defined as **timestamp**, or those with the IDENTITY property. If a default value is specified for a user-defined type column, the type should support an implicit conversion from *constant_expression* to the user-defined type. DEFAULT definitions are removed when the table is dropped. Only a constant value, such as a character string; a scalar function (either a system, user-defined, or CLR function); or NULL can be used as a default. To maintain compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a constraint name can be assigned to a DEFAULT.  
  
 *constant_expression*  
 Is a constant, NULL, or a system function that is used as the default value for the column.  
  
 *memory_optimized_constant_expression*  
 Is a constant, NULL, or a system function that is supported in used as the default value for the column. Must be supported in natively compiled stored procedures. For more information about built-in functions in natively compiled stored procedures, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).  
  
 IDENTITY  
 Indicates that the new column is an identity column. When a new row is added to the table, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] provides a unique, incremental value for the column. Identity columns are typically used with PRIMARY KEY constraints to serve as the unique row identifier for the table. The IDENTITY property can be assigned to **tinyint**, **smallint**, **int**, **bigint**, **decimal(p,0)**, or **numeric(p,0)** columns. Only one identity column can be created per table. Bound defaults and DEFAULT constraints cannot be used with an identity column. Both the seed and increment or neither must be specified. If neither is specified, the default is (1,1).  
  
 *seed*  
 Is the value used for the very first row loaded into the table.  
  
 *increment*  
 Is the incremental value added to the identity value of the previous row loaded.  
  
 NOT FOR REPLICATION  
 In the CREATE TABLE statement, the NOT FOR REPLICATION clause can be specified for the IDENTITY property, FOREIGN KEY constraints, and CHECK constraints. If this clause is specified for the IDENTITY property, values are not incremented in identity columns when replication agents perform inserts. If this clause is specified for a constraint, the constraint is not enforced when replication agents perform insert, update, or delete operations.  
  
 GENERATED ALWAYS AS ROW { START | END } [ HIDDEN ] [ NOT NULL ]  
 
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Specifies that a specified datetime2 column will be used by the system to record either the start time for which a record is valid or the end time for which a record is valid. The column must be defined as NOT NULL. If you attempt to specify them as NULL, the system will throw an error. If you do not explicitly specify NOT NULL for a period column, the system will define the column as NOT NULL by default. Use this argument in conjunction with the PERIOD FOR SYSTEM_TIME and WITH SYSTEM_VERSIONING = ON arguments to enable system versioning on a table. For more information, see [Temporal Tables](../../relational-databases/tables/temporal-tables.md).  
  
 You can mark one or both period columns with **HIDDEN** flag to implicitly hide these columns such that **SELECT \* FROM**_`<table>`_ does not return a value for those columns. By default, period columns are not hidden. In order to be used, hidden columns must be explicitly included in all queries that directly reference the temporal table. To change the **HIDDEN** attribute for an existing period column, **PERIOD** must be dropped and recreated with a different hidden flag.  
  
 `INDEX *index_name* [ CLUSTERED | NONCLUSTERED ] (*column_name* [ ASC | DESC ] [ ,... *n* ] )`  
     
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

Specifies to create an index on the table. This can be a clustered index, or a nonclustered index. The index will contain the columns listed, and will sort the data in either ascending or descending order.  
  
 INDEX *index_name* CLUSTERED COLUMNSTORE  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 Specifies to store the entire table in columnar format with a clustered columnstore index. This always includes all columns in the table. The data is not sorted in alphabetical or numeric order since the rows are organized to gain columnstore compression benefits.  
  
 INDEX *index_name* [ NONCLUSTERED ] COLUMNSTORE (*column_name* [ ,... *n* ] )  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 Specifies to create a nonclustered columnstore index on the table. The underlying table can be a rowstore heap or clustered index, or it can be a clustered columnstore index. In all cases, creating a nonclustered columnstore index on a table stores a second copy of the data for the columns in the index.  
  
 The nonclustered columnstore index is stored and managed as a clustered columnstore index. It is called a nonclustered columnstore index to because the columns can be limited and it exists as a secondary index on a table.  
  
 ON _partition\_scheme\_name_**(**_column\_name_**)**  
 Specifies the partition scheme that defines the filegroups onto which the partitions of a partitioned index will be mapped. The partition scheme must exist within the database by executing either [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md) or [ALTER PARTITION SCHEME](../../t-sql/statements/alter-partition-scheme-transact-sql.md). *column_name* specifies the column against which a partitioned index will be partitioned. This column must match the data type, length, and precision of the argument of the partition function that *partition_scheme_name* is using. *column_name* is not restricted to the columns in the index definition. Any column in the base table can be specified, except when partitioning a UNIQUE index, *column_name* must be chosen from among those used as the unique key. This restriction allows the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to verify uniqueness of key values within a single partition only.  
  
> [!NOTE]  
> When you partition a non-unique, clustered index, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by default adds the partitioning column to the list of clustered index keys, if it is not already specified. When partitioning a non-unique, nonclustered index, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] adds the partitioning column as a non-key (included) column of the index, if it is not already specified.  
  
 If *partition_scheme_name* or *filegroup* is not specified and the table is partitioned, the index is placed in the same partition scheme, using the same partitioning column, as the underlying table.  
  
> [!NOTE]  
> You cannot specify a partitioning scheme on an XML index. If the base table is partitioned, the XML index uses the same partition scheme as the table.  
  
 For more information about partitioning indexes, [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
 ON *filegroup_name*  
 Creates the specified index on the specified filegroup. If no location is specified and the table or view is not partitioned, the index uses the same filegroup as the underlying table or view. The filegroup must already exist.  
  
 ON **"**default**"**  
 Creates the specified index on the default filegroup.  
  
 The term default, in this context, is not a keyword. It is an identifier for the default filegroup and must be delimited, as in ON **"**default**"** or ON **[**default**]**. If "default" is specified, the QUOTED_IDENTIFIER option must be ON for the current session. This is the default setting. For more information, see [SET QUOTED_IDENTIFIER &#40;Transact-SQL&#41;](../../t-sql/statements/set-quoted-identifier-transact-sql.md).  
  
 [ FILESTREAM_ON { *filestream_filegroup_name* | *partition_scheme_name* | "NULL" } ]  
   
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

 Specifies the placement of FILESTREAM data for the table when a clustered index is created. The FILESTREAM_ON clause allows FILESTREAM data to be moved to a different FILESTREAM filegroup or partition scheme.  
  
 *filestream_filegroup_name* is the name of a FILESTREAM filegroup. The filegroup must have one file defined for the filegroup by using a [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md?&tabs=sqlserver) or [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement; otherwise, an error is raised.  
  
 If the table is partitioned, the FILESTREAM_ON clause must be included and must specify a partition scheme of FILESTREAM filegroups that uses the same partition function and partition columns as the partition scheme for the table. Otherwise, an error is raised.  
  
 If the table is not partitioned, the FILESTREAM column cannot be partitioned. FILESTREAM data for the table must be stored in a single filegroup that is specified in the FILESTREAM_ON clause.  
  
 FILESTREAM_ON NULL can be specified in a CREATE INDEX statement if a clustered index is being created and the table does not contain a FILESTREAM column.  
  
 For more information, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
 ROWGUIDCOL  
 Indicates that the new column is a row GUID column. Only one **uniqueidentifier** column per table can be designated as the ROWGUIDCOL column. Applying the ROWGUIDCOL property enables the column to be referenced using $ROWGUID. The ROWGUIDCOL property can be assigned only to a **uniqueidentifier** column. User-defined data type columns cannot be designated with ROWGUIDCOL.  
  
 The ROWGUIDCOL property does not enforce uniqueness of the values stored in the column. ROWGUIDCOL also does not automatically generate values for new rows inserted into the table. To generate unique values for each column, either use the [NEWID](../../t-sql/functions/newid-transact-sql.md) or [NEWSEQUENTIALID](../../t-sql/functions/newsequentialid-transact-sql.md) function on [INSERT](../../t-sql/statements/insert-transact-sql.md) statements or use these functions as the default for the column.  
  
 ENCRYPTED WITH  
 Specifies encrypting columns by using the [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature.  
  
 COLUMN_ENCRYPTION_KEY = *key_name*  
 Specifies the column encryption key. For more information, see [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md).  
  
 ENCRYPTION_TYPE = { DETERMINISTIC | RANDOMIZED }  
 **Deterministic encryption** uses a method which always generates the same encrypted value for any given plain text value. Using deterministic encryption allows searching using equality comparison, grouping, and joining tables using equality joins based on encrypted values, but can also allow unauthorized users to guess information about encrypted values by examining patterns in the encrypted column. Joining two tables on columns encrypted deterministically is only possible if both columns are encrypted using the same column encryption key. Deterministic encryption must use a column collation with a binary2 sort order for character columns.  
  
 **Randomized encryption** uses a method that encrypts data in a less predictable manner. Randomized encryption is more secure, but it prevents any computations and indexing on encrypted columns, unless your SQL Server instance supports Always Encrypted with secure enclaves. Please see [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) for details.
  
 If you are using Always Encrypted (without secure enclaves), use deterministic encryption for columns that will be searched with parameters or grouping parameters, for example a government ID number. Use randomized encryption, for data such as a credit card number, which is not grouped with other records or used to join tables, and which is not searched for because you use other columns (such as a transaction number) to find the row which contains the encrypted column of interest.

 If you are using Always Encrypted with secure enclaves, randomized encryption is a recommended encryption type.
  
 Columns must be of a qualifying data type.  
  
 ALGORITHM  
   
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
 
 Must be **'AEAD_AES_256_CBC_HMAC_SHA_256'**.  
  
 For more information including feature constraints, see [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md).  
 
 SPARSE  
 Indicates that the column is a sparse column. The storage of sparse columns is optimized for null values. Sparse columns cannot be designated as NOT NULL. For additional restrictions and more information about sparse columns, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).  
  
 MASKED WITH ( FUNCTION = ' *mask_function* ')  
 
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies a dynamic data mask. *mask_function* is the name of the masking function with the appropriate parameters. Three functions are available:  
  
-   default()  
  
-   email()  
  
-   partial()  
  
-   random()  
  
 For function parameters, see [Dynamic Data Masking](../../relational-databases/security/dynamic-data-masking.md).  
  
 FILESTREAM  
   
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

 Valid only for **varbinary(max)** columns. Specifies FILESTREAM storage for the **varbinary(max)** BLOB data.  
  
 The table must also have a column of the **uniqueidentifier** data type that has the ROWGUIDCOL attribute. This column must not allow null values and must have either a UNIQUE or PRIMARY KEY single-column constraint. The GUID value for the column must be supplied either by an application when inserting data, or by a DEFAULT constraint that uses the NEWID () function.  
  
 The ROWGUIDCOL column cannot be dropped and the related constraints cannot be changed while there is a FILESTREAM column defined for the table. The ROWGUIDCOL column can be dropped only after the last FILESTREAM column is dropped.  
  
 When the FILESTREAM storage attribute is specified for a column, all values for that column are stored in a FILESTREAM data container on the file system.  
  
 COLLATE *collation_name*  
 Specifies the collation for the column. Collation name can be either a Windows collation name or an SQL collation name. *collation_name* is applicable only for columns of the **char**, **varchar**, **text**, **nchar**, **nvarchar**, and **ntext** data types. If not specified, the column is assigned either the collation of the user-defined data type, if the column is of a user-defined data type, or the default collation of the database.  
  
 For more information about the Windows and SQL collation names, see [Windows Collation Name](../../t-sql/statements/windows-collation-name-transact-sql.md) and [SQL Collation Name](../../t-sql/statements/sql-server-collation-name-transact-sql.md).  
  
 For more information about the COLLATE clause, see [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md).  
  
 CONSTRAINT  
 Is an optional keyword that indicates the start of the definition of a PRIMARY KEY, NOT NULL, UNIQUE, FOREIGN KEY, or CHECK constraint.  
  
 *constraint_name*  
 Is the name of a constraint. Constraint names must be unique within the schema to which the table belongs.  
  
 NULL | NOT NULL  
 Determine whether null values are allowed in the column. NULL is not strictly a constraint but can be specified just like NOT NULL. NOT NULL can be specified for computed columns only if PERSISTED is also specified.  
  
 PRIMARY KEY  
 Is a constraint that enforces entity integrity for a specified column or columns through a unique index. Only one PRIMARY KEY constraint can be created per table.  
  
 UNIQUE  
 Is a constraint that provides entity integrity for a specified column or columns through a unique index. A table can have multiple UNIQUE constraints.  
  
 CLUSTERED | NONCLUSTERED  
 Indicate that a clustered or a nonclustered index is created for the PRIMARY KEY or UNIQUE constraint. PRIMARY KEY constraints default to CLUSTERED, and UNIQUE constraints default to NONCLUSTERED.  
  
 In a CREATE TABLE statement, CLUSTERED can be specified for only one constraint. If CLUSTERED is specified for a UNIQUE constraint and a PRIMARY KEY constraint is also specified, the PRIMARY KEY defaults to NONCLUSTERED.  
  
 The following shows how to use NONCLUSTERED in a disk-based table:  
  
```  
CREATE TABLE t1 ( c1 int, INDEX ix_1 NONCLUSTERED (c1))   
CREATE TABLE t2( c1 int INDEX ix_1 NONCLUSTERED (c1))   
CREATE TABLE t3( c1 int, c2 int INDEX ix_1 NONCLUSTERED)   
CREATE TABLE t4( c1 int, c2 int, INDEX ix_1 NONCLUSTERED (c1,c2))  
```  
  
 FOREIGN KEY REFERENCES  
 Is a constraint that provides referential integrity for the data in the column or columns. FOREIGN KEY constraints require that each value in the column exists in the corresponding referenced column or columns in the referenced table. FOREIGN KEY constraints can reference only columns that are PRIMARY KEY or UNIQUE constraints in the referenced table or columns referenced in a UNIQUE INDEX on the referenced table. Foreign keys on computed columns must also be marked PERSISTED.  
  
 [ _schema\_name_**.**] *referenced_table_name*]  
 Is the name of the table referenced by the FOREIGN KEY constraint, and the schema to which it belongs.  
  
 **(** *ref_column* [ **,**... *n* ] **)**  
 Is a column, or list of columns, from the table referenced by the FOREIGN KEY constraint.  
  
 ON DELETE { **NO ACTION** | CASCADE | SET NULL | SET DEFAULT }  
 Specifies what action happens to rows in the table created, if those rows have a referential relationship and the referenced row is deleted from the parent table. The default is NO ACTION.  
  
 NO ACTION  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and the delete action on the row in the parent table is rolled back.  
  
 CASCADE  
 Corresponding rows are deleted from the referencing table if that row is deleted from the parent table.  
  
 SET NULL  
 All the values that make up the foreign key are set to NULL if the corresponding row in the parent table is deleted. For this constraint to execute, the foreign key columns must be nullable.  
  
 SET DEFAULT  
 All the values that make up the foreign key are set to their default values if the corresponding row in the parent table is deleted. For this constraint to execute, all foreign key columns must have default definitions. If a column is nullable, and there is no explicit default value set, NULL becomes the implicit default value of the column.  
  
 Do not specify CASCADE if the table will be included in a merge publication that uses logical records. For more information about logical records, see [Group Changes to Related Rows with Logical Records](../../relational-databases/replication/merge/group-changes-to-related-rows-with-logical-records.md).  
  
 ON DELETE CASCADE cannot be defined if an INSTEAD OF trigger ON DELETE already exists on the table.  
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the **ProductVendor** table has a referential relationship with the **Vendor** table. The **ProductVendor.BusinessEntityID** foreign key references the **Vendor.BusinessEntityID** primary key.  
  
 If a DELETE statement is executed on a row in the **Vendor** table, and an ON DELETE CASCADE action is specified for **ProductVendor.BusinessEntityID**, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the **ProductVendor** table. If any exist, the dependent rows in the **ProductVendor** table are deleted, and also the row referenced in the **Vendor** table.  
  
 Conversely, if NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the delete action on the **Vendor** row if there is at least one row in the **ProductVendor** table that references it.  
  
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
  
 For example, in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the **ProductVendor** table has a referential relationship with the **Vendor** table: **ProductVendor.BusinessEntity** foreign key references the **Vendor.BusinessEntityID** primary key.  
  
 If an UPDATE statement is executed on a row in the **Vendor** table, and an ON UPDATE CASCADE action is specified for **ProductVendor.BusinessEntityID**, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for one or more dependent rows in the **ProductVendor** table. If any exist, the dependent rows in the **ProductVendor** table are updated, and also the row referenced in the **Vendor** table.  
  
 Conversely, if NO ACTION is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error and rolls back the update action on the **Vendor** row if there is at least one row in the **ProductVendor** table that references it.  
  
 CHECK  
 Is a constraint that enforces domain integrity by limiting the possible values that can be entered into a column or columns. CHECK constraints on computed columns must also be marked PERSISTED.  
  
 *logical_expression*  
 Is a logical expression that returns TRUE or FALSE. Alias data types cannot be part of the expression.  
  
 *column*  
 Is a column or list of columns, in parentheses, used in table constraints to indicate the columns used in the constraint definition.  
  
 [ **ASC** | DESC ]  
 Specifies the order in which the column or columns participating in table constraints are sorted. The default is ASC.  
  
 *partition_scheme_name*  
 Is the name of the partition scheme that defines the filegroups onto which the partitions of a partitioned table will be mapped. The partition scheme must exist within the database.  
  
 [ _partition\_column\_name_**.** ]  
 Specifies the column against which a partitioned table will be partitioned. The column must match that specified in the partition function that *partition_scheme_name* is using in terms of data type, length, and precision. A computed columns that participates in a partition function must be explicitly marked PERSISTED.  
  
> [!IMPORTANT]  
>  We recommend that you specify NOT NULL on the partitioning column of partitioned tables, and also nonpartitioned tables that are sources or targets of ALTER TABLE...SWITCH operations. Doing this makes sure that any CHECK constraints on partitioning columns do not have to check for null values.  
  
 WITH FILLFACTOR **=**_fillfactor_  
 Specifies how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make each index page that is used to store the index data. User-specified *fillfactor* values can be from 1 through 100. If a value is not specified, the default is 0. Fill factor values 0 and 100 are the same in all respects.  
  
> [!IMPORTANT]  
>  Documenting WITH FILLFACTOR = *fillfactor* as the only index option that applies to PRIMARY KEY or UNIQUE constraints is maintained for backward compatibility, but will not be documented in this manner in future releases.  
  
 *column_set_name* XML COLUMN_SET FOR ALL_SPARSE_COLUMNS  
 Is the name of the column set. A column set is an untyped XML representation that combines all of the sparse columns of a table into a structured output. For more information about column sets, see [Use Column Sets](../../relational-databases/tables/use-column-sets.md).  
  
 PERIOD FOR SYSTEM_TIME (*system_start_time_column_name* , *system_end_time_column_name* )  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 Specifies the names of the columns that the system will use to record the period for which a record is valid. Use this argument in conjunction with the GENERATED ALWAYS AS ROW { START | END } and WITH SYSTEM_VERSIONING = ON arguments to enable system versioning on a table. For more information, see [Temporal Tables](../../relational-databases/tables/temporal-tables.md).  
  
 COMPRESSION_DELAY  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 For a memory-optimized, delay specifies the minimum number of minutes a row must remain in the table, unchanged, before  it is eligible for compression into the columnstore index. SQL Server selects specific rows to compress according to their last update time. For example, if rows are changing frequently during a two-hour period of time, you could set COMPRESSION_DELAY = 120 Minutes to ensure updates are completed before SQL Server compresses the row.  
  
 For a disk-based table, delay specifies the minimum number of minutes a delta rowgroup in the CLOSED state must remain in the delta rowgroup before SQL Server can compress it into the compressed rowgroup. Since disk-based tables don't track insert and update times on individual rows, SQL Server applies the delay to delta rowgroups in the CLOSED state.  
  
 The default is 0 minutes.  
  
 For recommendations on when to use COMPRESSION_DELAY, please see [Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)  
  
 \< table_option> ::= 
 Specifies one or more table options.  
  
 DATA_COMPRESSION  
 Specifies the data compression option for the specified table, partition number, or range of partitions. The options are as follows:  
  
 NONE  
 Table or specified partitions are not compressed.  
  
 ROW  
 Table or specified partitions are compressed by using row compression.  
  
 PAGE  
 Table or specified partitions are compressed by using page compression.  
  
 COLUMNSTORE  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 Applies only to columnstore indexes, including both nonclustered columnstore and clustered columnstore indexes. COLUMNSTORE specifies to compress with the most performant columnstore compression. This is the typical choice.  
  
 COLUMNSTORE_ARCHIVE  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Applies only to columnstore indexes, including both nonclustered columnstore and clustered columnstore indexes. COLUMNSTORE_ARCHIVE will further compress the table or partition to a smaller size. This can be used for archival, or for other situations that require a smaller storage size and can afford more time for storage and retrieval.  
  
 For more information about compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
 ON PARTITIONS **(** { `<partition_number_expression>` | [ **,**...*n* ] **)**  
 Specifies the partitions to which the DATA_COMPRESSION setting applies. If the table is not partitioned, the ON PARTITIONS argument will generate an error. If the ON PARTITIONS clause is not provided, the DATA_COMPRESSION option will apply to all partitions of a partitioned table.  
  
 *partition_number_expression* can be specified in the following ways:  
  
-   Provide the partition number of a partition, for example: ON PARTITIONS (2).  
  
-   Provide the partition numbers for several individual partitions separated by commas, for example: ON PARTITIONS (1, 5).  
  
-   Provide both ranges and individual partitions, for example: ON PARTITIONS (2, 4, 6 TO 8)  
  
 `<range>` can be specified as partition numbers separated by the word TO, for example: ON PARTITIONS (6 TO 8).  
  
 To set different types of data compression for different partitions, specify the DATA_COMPRESSION option more than once, for example:  
  
```  
WITH   
(  
DATA_COMPRESSION = NONE ON PARTITIONS (1),   
DATA_COMPRESSION = ROW ON PARTITIONS (2, 4, 6 TO 8),   
DATA_COMPRESSION = PAGE ON PARTITIONS (3, 5)  
)  
```  
  
 \<index_option> ::=  
 Specifies one or more index options. For a complete description of these options, see [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md).  
  
 PAD_INDEX = { ON | **OFF** }  
 When ON, the percentage of free space specified by FILLFACTOR is applied to the intermediate level pages of the index. When OFF or a FILLFACTOR value it not specified, the intermediate level pages are filled to near capacity leaving enough space for at least one row of the maximum size the index can have, considering the set of keys on the intermediate pages. The default is OFF.  
  
 FILLFACTOR **=**_fillfactor_  
 Specifies a percentage that indicates how full the [!INCLUDE[ssDE](../../includes/ssde-md.md)] should make the leaf level of each index page during index creation or alteration. *fillfactor* must be an integer value from 1 to 100. The default is 0. Fill factor values 0 and 100 are the same in all respects.  
  
 IGNORE_DUP_KEY = { ON | **OFF** }  
 Specifies the error response when an insert operation attempts to insert duplicate key values into a unique index. The IGNORE_DUP_KEY option applies only to insert operations after the index is created or rebuilt. The option has no effect when executing [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md), [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md), or [UPDATE](../../t-sql/queries/update-transact-sql.md). The default is OFF.  
  
 ON  
 A warning message will occur when duplicate key values are inserted into a unique index. Only the rows violating the uniqueness constraint will fail.  
  
 OFF  
 An error message will occur when duplicate key values are inserted into a unique index. The entire INSERT operation will be rolled back.  
  
 IGNORE_DUP_KEY cannot be set to ON for indexes created on a view, non-unique indexes, XML indexes, spatial indexes, and filtered indexes.  
  
 To view IGNORE_DUP_KEY, use [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md).  
  
 In backward compatible syntax, WITH IGNORE_DUP_KEY is equivalent to WITH IGNORE_DUP_KEY = ON.  
  
 STATISTICS_NORECOMPUTE **=** { ON | **OFF** }  
 When ON, out-of-date index statistics are not automatically recomputed. When OFF, automatic statistics updating are enabled. The default is OFF.  
  
 ALLOW_ROW_LOCKS **=** { **ON** | OFF }  
 When ON, row locks are allowed when you access the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when row locks are used. When OFF, row locks are not used. The default is ON.  
  
 ALLOW_PAGE_LOCKS **=** { **ON** | OFF }  
 When ON, page locks are allowed when you access the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] determines when page locks are used. When OFF, page locks are not used. The default is ON.  
  
 FILETABLE_DIRECTORY = *directory_name*  
   
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 Specifies the windows-compatible FileTable directory name. This name should be unique among all the FileTable directory names in the database. Uniqueness comparison is case-insensitive, regardless of collation settings. If this value is not specified, the name of the filetable is used.  
  
 FILETABLE_COLLATE_FILENAME = { *collation_name* | database_default }  
   
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Azure SQL Database does not support `FILETABLE`. 
  
 Specifies the name of the collation to be applied to the **Name** column in the FileTable. The collation must be case-insensitive to comply with Windows file naming semantics. If this value is not specified, the database default collation is used. If the database default collation is case-sensitive, an error is raised and the CREATE TABLE operation fails.  
  
 *collation_name*  
 The name of a case-insensitive collation.  
  
 database_default  
 Specifies that the default collation for the database should be used. This collation must be case-insensitive.  
  
 FILETABLE_PRIMARY_KEY_CONSTRAINT_NAME = *constraint_name*  
   
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 Specifies the name to be used for the primary key constraint that is automatically created on the FileTable. If this value is not specified, the system generates a name for the constraint.  
  
 FILETABLE_STREAMID_UNIQUE_CONSTRAINT_NAME = *constraint_name*  
   
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 Specifies the name to be used for the unique constraint that is automatically created on the **stream_id** column in the FileTable. If this value is not specified, the system generates a name for the constraint.  
  
 FILETABLE_FULLPATH_UNIQUE_CONSTRAINT_NAME = *constraint_name*  
   
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. 
  
 Specifies the name to be used for the unique constraint that is automatically created on the **parent_path_locator** and **name** columns in the FileTable. If this value is not specified, the system generates a name for the constraint.  
  
 SYSTEM_VERSIONING **=** ON [ ( HISTORY_TABLE **=** *schema_name* .  *history_table_name* [, DATA_CONSISTENCY_CHECK **=** { **ON** | OFF } ] ) ]  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].   
  
 Enables system versioning of the table if the datatype, nullability constraint, and primary key constraint requirements are met. If the **HISTORY_TABLE** argument is not used, the system generates a new history table matching the schema of the current table in the same filegroup as the current table, creating a link between the two tables and enables the system to record the history of each record in the current table in the history table. The name of this history table will be `MSSQL_TemporalHistoryFor<primary_table_object_id>`. By default, the history table is **PAGE** compressed. If the HISTORY_TABLE argument is used to create a link to and use an existing history table, the link is created between the current table and the specified table. If current table is partitioned, the history table is created on default file group because partitioning configuration is not replicated automatically from the current table to the history table. If the name of a history table is specified during history table creation, you must specify the schema and table name. When creating a link to an existing history table, you can choose to perform a data consistency check. This data consistency check ensures that existing records do not overlap. Performing the data consistency check is the default. Use this argument in conjunction with the PERIOD FOR SYSTEM_TIME and GENERATED ALWAYS AS ROW { START | END } arguments to enable system versioning on a table. For more information, see [Temporal Tables](../../relational-databases/tables/temporal-tables.md).  
  
 REMOTE_DATA_ARCHIVE = { ON [ ( *table_stretch_options* [,...n] ) ] | OFF ( MIGRATION_STATE = PAUSED ) }  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Creates the new table with Stretch Database enabled or disabled. For more info, see [Stretch Database](../../sql-server/stretch-database/stretch-database.md).  
  
 **Enabling Stretch Database for a table**  
  
 When you enable Stretch for a table by specifying `ON`, you can optionally  specify `MIGRATION_STATE = OUTBOUND` to begin migrating data immediately, or `MIGRATION_STATE = PAUSED` to postpone data migration. The default value is `MIGRATION_STATE = OUTBOUND`. For more info about enabling Stretch for a table, see [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md).  
  
 **Prerequisites**. Before you enable Stretch for a table, you have to enable Stretch on the server and on the database. For more info, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md).  
  
 **Permissions**. Enabling Stretch for a database or a table requires db_owner permissions. Enabling Stretch for a table also requires ALTER permissions on the table.  
  
 [ FILTER_PREDICATE = { null | *predicate* } ]  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Optionally specifies a filter predicate to select rows to migrate from a table that contains both historical and current data. The predicate must call a deterministic inline table-valued function. For more info, see [Enable Stretch Database for a table](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md) and [Select rows to migrate by using a filter function](../../sql-server/stretch-database/select-rows-to-migrate-by-using-a-filter-function-stretch-database.md). 
   
> [!IMPORTANT]  
> If you provide a filter predicate that performs poorly, data migration also performs poorly. Stretch Database applies the filter predicate to the table by using the CROSS APPLY operator.  
  
 If you don't specify a filter predicate, the entire table is migrated.  
  
 When you specify a filter predicate, you also have to specify *MIGRATION_STATE*.  
  
 MIGRATION_STATE = { OUTBOUND |  INBOUND | PAUSED }  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], and Azure SQL . 
  
-   Specify `OUTBOUND` to migrate data from SQL Server to Azure.  
  
-   Specify `INBOUND` to copy the remote data for the table from Azure back to SQL Server and to disable Stretch for the table. For more info, see [Disable Stretch Database and bring back remote data](../../sql-server/stretch-database/disable-stretch-database-and-bring-back-remote-data.md).  
  
     This operation incurs data transfer costs, and it can't be canceled.  
  
-   Specify `PAUSED` to pause or postpone data migration. For more info, see [Pause and resume data migration &#40;Stretch Database&#41;](../../sql-server/stretch-database/pause-and-resume-data-migration-stretch-database.md).  
  
 MEMORY_OPTIMIZED  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Azure SQL Database Managed Instance does not support memory optimized tables. 
  
 The value  ON indicates that the table is memory optimized. Memory-optimized tables are part of the In-Memory OLTP feature, which is used to optimized the performance of transaction processing. To get started with In-Memory OLTP see [Quick Start 1: In-Memory OLTP Technologies for Faster Transact-SQL Performance](../../relational-databases/in-memory-oltp/survey-of-initial-areas-in-in-memory-oltp.md). For more in-depth information about memory-optimized tables see [Memory-Optimized Tables](../../relational-databases/in-memory-oltp/memory-optimized-tables.md).  
  
 The default value OFF indicates that the table is disk-based.  
  
 DURABILITY  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].   
  
 The value of SCHEMA_AND_DATA indicates that the table is durable, meaning that changes are persisted on disk and survive restart or failover.  SCHEMA_AND_DATA is the default value.  
  
 The value of SCHEMA_ONLY indicates that the table is non-durable. The table schema is persisted but any data updates are not persisted upon a restart or failover of the database. DURABILITY=SCHEMA_ONLY is only allowed with MEMORY_OPTIMIZED=ON.  
  
> [!WARNING]  
> When a table is created with **DURABILITY = SCHEMA_ONLY**, and **READ_COMMITTED_SNAPSHOT** is subsequently changed using **ALTER DATABASE**, data in the table will be lost.  
  
 BUCKET_COUNT  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. 
  
 Indicates the number of buckets that should be created in the hash index. The maximum value for BUCKET_COUNT in hash indexes is 1,073,741,824. For more information about bucket counts, see [Indexes for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/indexes-for-memory-optimized-tables.md).  
  
 Bucket_count is a required argument.  
  
 INDEX  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. 
  
Column and table indexes can be specified as part of the CREATE TABLE statement. For details about adding and removing indexes on memory-optimized tables see: [Altering Memory-Optimized Tables](../../relational-databases/in-memory-oltp/altering-memory-optimized-tables.md)
  
 HASH  
   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. 
  
 Indicates that a HASH index is created.  
  
 Hash indexes are supported only on memory-optimized tables.  
  
## Remarks  
 For information about the number of allowed tables, columns, constraints and indexes, see [Maximum Capacity Specifications for SQL Server](../../sql-server/maximum-capacity-specifications-for-sql-server.md).  
  
 Space is generally allocated to tables and indexes in increments of one extent at a time. When the SET MIXED_PAGE_ALLOCATION  option of ALTER DATABASE is set to TRUE, or always prior to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], when a table or index is created, it is allocated pages from mixed extents until it has enough pages to fill a uniform extent. After it has enough pages to fill a uniform extent, another extent is allocated every time the currently allocated extents become full. For a report about the amount of space allocated and used by a table, execute **sp_spaceused**.  
  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not enforce an order in which DEFAULT, IDENTITY, ROWGUIDCOL, or column constraints are specified in a column definition.  
  
 When a table is created, the QUOTED IDENTIFIER option is always stored as ON in the metadata for the table, even if the option is set to OFF when the table is created.  
  
## Temporary Tables  
 You can create local and global temporary tables. Local temporary tables are visible only in the current session, and global temporary tables are visible to all sessions. Temporary tables cannot be partitioned.  
  
 Prefix local temporary table names with single number sign (#*table_name*), and prefix global temporary table names with a double number sign (##*table_name*).  
  
 SQL statements reference the temporary table by using the value specified for *table_name* in the CREATE TABLE statement, for example:  
  
```sql  
CREATE TABLE #MyTempTable (cola INT PRIMARY KEY);  
  
INSERT INTO #MyTempTable VALUES (1);  
```  
  
 If more than one temporary table is created inside a single stored procedure or batch, they must have different names.  
 
 If you include a *schema_name* when you create or access a temporary table, it is ignored.  All temporary tables are created in the dbo schema.
  
 If a local temporary table is created in a stored procedure or application that can be executed at the same time by several users, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] must be able to distinguish the tables created by the different users. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does this by internally appending a numeric suffix to each local temporary table name. The full name of a temporary table as stored in the **sysobjects** table in **tempdb** is made up of the table name specified in the CREATE TABLE statement and the system-generated numeric suffix. To allow for the suffix, *table_name* specified for a local temporary name cannot exceed 116 characters.  
  
 Temporary tables are automatically dropped when they go out of scope, unless explicitly dropped by using DROP TABLE:  
  
-   A local temporary table created in a stored procedure is dropped automatically when the stored procedure is finished. The table can be referenced by any nested stored procedures executed by the stored procedure that created the table. The table cannot be referenced by the process that called the stored procedure that created the table.  
  
-   All other local temporary tables are dropped automatically at the end of the current session.  
  
-   Global temporary tables are automatically dropped when the session that created the table ends and all other tasks have stopped referencing them. The association between a task and a table is maintained only for the life of a single [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. This means that a global temporary table is dropped at the completion of the last [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that was actively referencing the table when the creating session ended.  
  
 A local temporary table created within a stored procedure or trigger can have the same name as a temporary table that was created before the stored procedure or trigger is called. However, if a query references a temporary table and two temporary tables with the same name exist at that time, it is not defined which table the query is resolved against. Nested stored procedures can also create temporary tables with the same name as a temporary table that was created by the stored procedure that called it. However, for modifications to resolve to the table that was created in the nested procedure, the table must have the same structure, with the same column names, as the table created in the calling procedure. This is shown in the following example.  
  
```sql  
CREATE PROCEDURE dbo.Test2  
AS  
n    CREATE TABLE #t(x INT PRIMARY KEY);  
    INSERT INTO #t VALUES (2);  
    SELECT Test2Col = x FROM #t;  
GO  
  
CREATE PROCEDURE dbo.Test1  
AS  
    CREATE TABLE #t(x INT PRIMARY KEY);  
    INSERT INTO #t VALUES (1);  
    SELECT Test1Col = x FROM #t;  
 EXEC Test2;  
GO  
  
CREATE TABLE #t(x INT PRIMARY KEY);  
INSERT INTO #t VALUES (99);  
GO  
  
EXEC Test1;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```
 (1 row(s) affected) 
 Test1Col 
 ----------- 
 1 

 (1 row(s) affected) 
 Test2Col 
 ----------- 
 2 
 ```
  
 When you create local or global temporary tables, the CREATE TABLE syntax supports constraint definitions except for FOREIGN KEY constraints. If a FOREIGN KEY constraint is specified in a temporary table, the statement returns a warning message that states the constraint was skipped. The table is still created without the FOREIGN KEY constraints. Temporary tables cannot be referenced in FOREIGN KEY constraints.  
  
 If a temporary table is created with a named constraint and the temporary table is created within the scope of a user-defined transaction, only one user at a time can execute the statement that creates the temp table. For example, if a stored procedure creates a temporary table with a named primary key constraint, the stored procedure cannot be executed simultaneously by multiple users.  

## Database scoped global temporary tables (Azure SQL Database)

Global temporary tables for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (initiated with ## table name) are stored in tempdb and shared among all users' sessions across the whole [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For information on SQL table types, see the above section on Create Tables.  

[!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] supports global temporary tables that are also stored in tempdb and scoped to the database level. This means that global temporary tables are shared for all users' sessions within the same [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. User sessions from other databases cannot access global temporary tables.

Global temporary tables for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] follow the same syntax and semantics that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses for temporary tables. Similarly, global temporary stored procedures are also scoped to the database level in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Local temporary tables (initiated with # table name) are also supported for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and follow the same syntax and semantics that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses.  See the above section on [Temporary Tables](#temporary-tables).  

> [!IMPORTANT]
> This feature is available for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

### Troubleshooting global temporary tables for Azure SQL Database 

For the troubleshooting the tempdb, see [Troubleshooting Insufficient Disk space in tempdb](https://docs.microsoft.com/previous-versions/sql/sql-server-2008-r2/ms176029(v=sql.105)). 

> [!NOTE]
> Only a server admin can access the troubleshooting DMVs in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
### Permissions  

 Any user can create global temporary objects. Users can only access their own objects, unless they receive additional permissions.  
  
### Examples 

- Session A creates a global temp table ##test in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] testdb1 and adds 1 row

```sql
CREATE TABLE ##test ( a int, b int);
INSERT INTO ##test values (1,1);

--Obtain object ID for temp table ##test 
SELECT OBJECT_ID('tempdb.dbo.##test') AS 'Object ID'; 

---Result
1253579504

---Obtain global temp table name for a given object ID 1253579504 in tempdb (2)
SELECT name FROM tempdb.sys.objects WHERE object_id = 1253579504

---Result
##test
```
- Session B connects to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] testdb1 and can access table ##test created by session A

```sql
SELECT * FROM ##test
---Results
1,1
```

- Session C connects to another database in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] testdb2 and wants to access ##test created in testdb1. This select fails due to the database scope for the global temp tables 

```sql
SELECT * FROM ##test
---Results
Msg 208, Level 16, State 0, Line 1
Invalid object name '##test'
```

- Addressing system object in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] tempdb from current user database testdb1

```sql
SELECT * FROM tempdb.sys.objects
SELECT * FROM tempdb.sys.columns
SELECT * FROM tempdb.sys.database_files
```

## Partitioned tables  
 Before creating a partitioned table by using CREATE TABLE, you must first create a partition function to specify how the table becomes partitioned. A partition function is created by using [CREATE PARTITION FUNCTION](../../t-sql/statements/create-partition-function-transact-sql.md). Second, you must create a partition scheme to specify the filegroups that will hold the partitions indicated by the partition function. A partition scheme is created by using [CREATE PARTITION SCHEME](../../t-sql/statements/create-partition-scheme-transact-sql.md). Placement of PRIMARY KEY or UNIQUE constraints to separate filegroups cannot be specified for partitioned tables. For more information, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
## PRIMARY KEY Constraints  
  
-   A table can contain only one PRIMARY KEY constraint.  
  
-   The index generated by a PRIMARY KEY constraint cannot cause the number of indexes on the table to exceed 999 nonclustered indexes and 1 clustered index.  
  
-   If CLUSTERED or NONCLUSTERED is not specified for a PRIMARY KEY constraint, CLUSTERED is used if there are no clustered indexes specified for UNIQUE constraints.  
  
-   All columns defined within a PRIMARY KEY constraint must be defined as NOT NULL. If nullability is not specified, all columns participating in a PRIMARY KEY constraint have their nullability set to NOT NULL.  
  
    > [!NOTE]  
    > For memory-optimized tables, the NULLable key column is allowed.  
  
-   If a primary key is defined on a CLR user-defined type column, the implementation of the type must support binary ordering. For more information, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).  
  
## UNIQUE Constraints  
  
-   If CLUSTERED or NONCLUSTERED is not specified for a UNIQUE constraint, NONCLUSTERED is used by default.  
  
-   Each UNIQUE constraint generates an index. The number of UNIQUE constraints cannot cause the number of indexes on the table to exceed 999 nonclustered indexes and 1 clustered index.  
  
-   If a unique constraint is defined on a CLR user-defined type column, the implementation of the type must support binary or operator-based ordering. For more information, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).  
  
## FOREIGN KEY Constraints  
  
-   When a value other than NULL is entered into the column of a FOREIGN KEY constraint, the value must exist in the referenced column; otherwise, a foreign key violation error message is returned.  
  
-   FOREIGN KEY constraints are applied to the preceding column, unless source columns are specified.  
  
-   FOREIGN KEY constraints can reference only tables within the same database on the same server. Cross-database referential integrity must be implemented through triggers. For more information, see [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md).  
  
-   FOREIGN KEY constraints can reference another column in the same table. This is referred to as a self-reference.  
  
-   The REFERENCES clause of a column-level FOREIGN KEY constraint can list only one reference column. This column must have the same data type as the column on which the constraint is defined.  
  
-   The REFERENCES clause of a table-level FOREIGN KEY constraint must have the same number of reference columns as the number of columns in the constraint column list. The data type of each reference column must also be the same as the corresponding column in the column list.  
  
-   CASCADE, SET NULL or SET DEFAULT cannot be specified if a column of type **timestamp** is part of either the foreign key or the referenced key.  
  
-   CASCADE, SET NULL, SET DEFAULT and NO ACTION can be combined on tables that have referential relationships with each other. If the [!INCLUDE[ssDE](../../includes/ssde-md.md)] encounters NO ACTION, it stops and rolls back related CASCADE, SET NULL and SET DEFAULT actions. When a DELETE statement causes a combination of CASCADE, SET NULL, SET DEFAULT and NO ACTION actions, all the CASCADE, SET NULL and SET DEFAULT actions are applied before the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks for any NO ACTION.  
  
-   The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not have a predefined limit on either the number of FOREIGN KEY constraints a table can contain that reference other tables, or the number of FOREIGN KEY constraints that are owned by other tables that reference a specific table.  
  
     Nevertheless, the actual number of FOREIGN KEY constraints that can be used is limited by the hardware configuration and by the design of the database and application. We recommend that a table contain no more than 253 FOREIGN KEY constraints, and that it be referenced by no more than 253 FOREIGN KEY constraints. The effective limit for you may be more or less depending on the application and hardware. Consider the cost of enforcing FOREIGN KEY constraints when you design your database and applications.  
  
-   FOREIGN KEY constraints are not enforced on temporary tables.  
  
-   FOREIGN KEY constraints can reference only columns in PRIMARY KEY or UNIQUE constraints in the referenced table or in a UNIQUE INDEX on the referenced table.  
  
-   If a foreign key is defined on a CLR user-defined type column, the implementation of the type must support binary ordering. For more information, see [CLR User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/clr-user-defined-types.md).  
  
-   Columns participating in a foreign key relationship must be defined with the same length and scale.  
  
## DEFAULT definitions  
  
-   A column can have only one DEFAULT definition.  
  
-   A DEFAULT definition can contain constant values, functions, SQL standard niladic functions, or NULL. The following table shows the niladic functions and the values they return for the default during an INSERT statement.  
  
    |SQL-92 niladic function|Value returned|  
    |------------------------------|--------------------|  
    |CURRENT_TIMESTAMP|Current date and time.|  
    |CURRENT_USER|Name of user performing an insert.|  
    |SESSION_USER|Name of user performing an insert.|  
    |SYSTEM_USER|Name of user performing an insert.|  
    |USER|Name of user performing an insert.|  
  
-   *constant_expression* in a DEFAULT definition cannot refer to another column in the table, or to other tables, views, or stored procedures.  
  
-   DEFAULT definitions cannot be created on columns with a **timestamp** data type or columns with an IDENTITY property.  
  
-   DEFAULT definitions cannot be created for columns with alias data types if the alias data type is bound to a default object.  
  
## CHECK Constraints  
  
-   A column can have any number of CHECK constraints, and the condition can include multiple logical expressions combined with AND and OR. Multiple CHECK constraints for a column are validated in the order they are created.  
  
-   The search condition must evaluate to a Boolean expression and cannot reference another table.  
  
-   A column-level CHECK constraint can reference only the constrained column, and a table-level CHECK constraint can reference only columns in the same table.  
  
     CHECK CONSTRAINTS and rules serve the same function of validating the data during INSERT and UPDATE statements.  
  
-   When a rule and one or more CHECK constraints exist for a column or columns, all restrictions are evaluated.  
  
-   CHECK constraints cannot be defined on **text**, **ntext**, or **image** columns.  
  
## Additional Constraint information  
  
-   An index created for a constraint cannot be dropped by using `DROP INDEX`; the constraint must be dropped by using ALTER TABLE. An index created for and used by a constraint can be rebuilt by using `ALTER INDEX ... REBUILD`. For more information, see [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md).  
  
-   Constraint names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md), except that the name cannot start with a number sign (#). If *constraint_name* is not supplied, a system-generated name is assigned to the constraint. The constraint name appears in any error message about constraint violations.  
  
-   When a constraint is violated in an INSERT, UPDATE, or DELETE statement, the statement is ended. However, when SET XACT_ABORT is set to OFF, the transaction, if the statement is part of an explicit transaction, continues to be processed. When SET XACT_ABORT is set to ON, the whole transaction is rolled back. You can also use the ROLLBACK TRANSACTION statement with the transaction definition by checking the @@ERROR system function.  
  
-   When `ALLOW_ROW_LOCKS = ON` and `ALLOW_PAGE_LOCK = ON`, row-, page-, and table-level locks are allowed when you access the index. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] chooses the appropriate lock and can escalate the lock from a row or page lock to a table lock. When `ALLOW_ROW_LOCKS = OFF` and `ALLOW_PAGE_LOCK = OFF`, only a table-level lock is allowed when you access the index.  
  
-   If a table has FOREIGN KEY or CHECK CONSTRAINTS and triggers, the constraint conditions are evaluated before the trigger is executed.  
  
 For a report on a table and its columns, use **sp_help** or **sp_helpconstraint**. To rename a table, use **sp_rename**. For a report on the views and stored procedures that depend on a table, use [sys.dm_sql_referenced_entities](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md) and [sys.dm_sql_referencing_entities](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md).  
  
## Nullability rules within a table definition  
 The nullability of a column determines whether that column can allow a null value (NULL) as the data in that column. NULL is not zero or blank: NULL means no entry was made or an explicit NULL was supplied, and it typically implies that the value is either unknown or not applicable.  
  
 When you use CREATE TABLE or ALTER TABLE to create or alter a table, database and session settings influence and possibly override the nullability of the data type that is used in a column definition. We recommend that you always explicitly define a column as NULL or NOT NULL for noncomputed columns or, if you use a user-defined data type, that you allow the column to use the default nullability of the data type. Sparse columns must always allow NULL.  
  
 When column nullability is not explicitly specified, column nullability follows the rules shown in the following table.  
  
|Column data type|Rule|  
|----------------------|----------|  
|Alias data type|The [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses the nullability that is specified when the data type was created. To determine the default nullability of the data type, use **sp_help**.|  
|CLR user-defined type|Nullability is determined according to the column definition.|  
|System-supplied data type|If the system-supplied data type has only one option, it takes precedence. **timestamp** data types must be NOT NULL. When any session settings are set ON by using SET:<br />**ANSI_NULL_DFLT_ON** = ON, NULL is assigned.  <br />**ANSI_NULL_DFLT_OFF** = ON, NOT NULL is assigned.<br /><br /> When any database settings are configured by using ALTER DATABASE:<br />**ANSI_NULL_DEFAULT_ON** = ON, NULL is assigned.  <br />**ANSI_NULL_DEFAULT_OFF** = ON, NOT NULL is assigned.<br /><br /> To view the database setting for ANSI_NULL_DEFAULT, use the **sys.databases** catalog view|  
  
 When neither of the ANSI_NULL_DFLT options is set for the session and the database is set to the default (ANSI_NULL_DEFAULT is OFF), the default of NOT NULL is assigned.  
  
 If the column is a computed column, its nullability is always automatically determined by the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. To find out the nullability of this type of column, use the COLUMNPROPERTY function with the **AllowsNull** property.  
  
> [!NOTE]  
>  The SQL Server ODBC driver and Microsoft OLE DB Provider for SQL Server both default to having ANSI_NULL_DFLT_ON set to ON. ODBC and OLE DB users can configure this in ODBC data sources, or with connection attributes or properties set by the application.  
  
## Data Compression  
 System tables cannot be enabled for compression. When you are creating a table, data compression is set to NONE, unless specified otherwise. If you specify a list of partitions or a partition that is out of range, an error will be generated. For a more information about data compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
 To evaluate how changing the compression state will affect a table, an index, or a partition, use the [sp_estimate_data_compression_savings](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md) stored procedure.  
  
## Permissions  
 Requires CREATE TABLE permission in the database and ALTER permission on the schema in which the table is being created.  
 
 If any columns in the CREATE TABLE statement are defined to be of a user-defined type, REFERENCES permission on the user-defined type is required. 
 
 If any columns in the CREATE TABLE statement are defined to be of a CLR user-defined type, either ownership of the type or REFERENCES permission on it is required.  
  
 If any columns in the CREATE TABLE statement have an XML schema collection associated with them, either ownership of the XML schema collection or REFERENCES permission on it is required.  
  
 Any user can create temporary tables in tempdb.  
  
## Examples  
  
### A. Create a PRIMARY KEY constraint on a column  
 The following example shows the column definition for a PRIMARY KEY constraint with a clustered index on the `EmployeeID` column of the `Employee` table. Because a constraint name is not specified, the system supplies the constraint name.  
  
```sql  
CREATE TABLE dbo.Employee (EmployeeID int  
PRIMARY KEY CLUSTERED);  
```  
  
### B. Using FOREIGN KEY constraints  
 A FOREIGN KEY constraint is used to reference another table. Foreign keys can be single-column keys or multicolumn keys. This following example shows a single-column FOREIGN KEY constraint on the `SalesOrderHeader` table that references the `SalesPerson` table. Only the REFERENCES clause is required for a single-column FOREIGN KEY constraint.  
  
```sql  
SalesPersonID int NULL  
REFERENCES SalesPerson(SalesPersonID)  
```  
  
 You can also explicitly use the FOREIGN KEY clause and restate the column attribute. Note that the column name does not have to be the same in both tables.  
  
```sql  
FOREIGN KEY (SalesPersonID) REFERENCES SalesPerson(SalesPersonID)  
```  
  
 Multicolumn key constraints are created as table constraints. In the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, the `SpecialOfferProduct` table includes a multicolumn PRIMARY KEY. The following example shows how to reference this key from another table; an explicit constraint name is optional.  
  
```sql  
CONSTRAINT FK_SpecialOfferProduct_SalesOrderDetail FOREIGN KEY  
 (ProductID, SpecialOfferID)  
REFERENCES SpecialOfferProduct (ProductID, SpecialOfferID)  
```  
  
### C. Using UNIQUE constraints  
 UNIQUE constraints are used to enforce uniqueness on nonprimary key columns. The following example enforces a restriction that the `Name` column of the `Product` table must be unique.  
  
```sql  
Name nvarchar(100) NOT NULL  
UNIQUE NONCLUSTERED  
```  
  
### D. Using DEFAULT definitions  
 Defaults supply a value (with the INSERT and UPDATE statements) when no value is supplied. For example, the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database could include a lookup table listing the different jobs employees can fill in the company. Under a column that describes each job, a character string default could supply a description when an actual description is not entered explicitly.  
  
```sql  
DEFAULT 'New Position - title not formalized yet'  
```  
  
 In addition to constants, DEFAULT definitions can include functions. Use the following example to get the current date for an entry.  
  
```sql  
DEFAULT (getdate())  
```  
  
 A niladic-function scan can also improve data integrity. To keep track of the user that inserted a row, use the niladic-function for USER. Do not enclose the niladic-functions with parentheses.  
  
```sql  
DEFAULT USER  
```  
  
### E. Using CHECK constraints  
 The following example shows a restriction made to values that are entered into the `CreditRating` column of the `Vendor` table. The constraint is unnamed.  
  
```sql  
CHECK (CreditRating >= 1 and CreditRating <= 5)  
```  
  
 This example shows a named constraint with a pattern restriction on the character data entered into a column of a table.  
  
```sql  
CONSTRAINT CK_emp_id CHECK (emp_id LIKE   
'[A-Z][A-Z][A-Z][1-9][0-9][0-9][0-9][0-9][FM]'   
OR emp_id LIKE '[A-Z]-[A-Z][1-9][0-9][0-9][0-9][0-9][FM]')  
```  
  
 This example specifies that the values must be within a specific list or follow a specified pattern.  
  
```sql  
CHECK (emp_id IN ('1389', '0736', '0877', '1622', '1756')  
OR emp_id LIKE '99[0-9][0-9]')  
```  
  
### F. Showing the complete table definition  
 The following example shows the complete table definitions with all constraint definitions for table `PurchaseOrderDetail` created in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Note that to run the sample, the table schema is changed to `dbo`.  
  
```sql  
CREATE TABLE dbo.PurchaseOrderDetail  
(  
    PurchaseOrderID int NOT NULL  
        REFERENCES Purchasing.PurchaseOrderHeader(PurchaseOrderID),  
    LineNumber smallint NOT NULL,  
    ProductID int NULL   
        REFERENCES Production.Product(ProductID),  
    UnitPrice money NULL,  
    OrderQty smallint NULL,  
    ReceivedQty float NULL,  
    RejectedQty float NULL,  
    DueDate datetime NULL,  
    rowguid uniqueidentifier ROWGUIDCOL  NOT NULL  
        CONSTRAINT DF_PurchaseOrderDetail_rowguid DEFAULT (newid()),  
    ModifiedDate datetime NOT NULL   
        CONSTRAINT DF_PurchaseOrderDetail_ModifiedDate DEFAULT (getdate()),  
    LineTotal  AS ((UnitPrice*OrderQty)),  
    StockedQty  AS ((ReceivedQty-RejectedQty)),  
    CONSTRAINT PK_PurchaseOrderDetail_PurchaseOrderID_LineNumber  
               PRIMARY KEY CLUSTERED (PurchaseOrderID, LineNumber)  
               WITH (IGNORE_DUP_KEY = OFF)  
)   
ON PRIMARY;  
```  
  
### G. Creating a table with an xml column typed to an XML schema collection  
 The following example creates a table with an `xml` column that is typed to XML schema collection `HRResumeSchemaCollection`. The `DOCUMENT` keyword specifies that each instance of the `xml` data type in *column_name* can contain only one top-level element.  
  
```sql  
CREATE TABLE HumanResources.EmployeeResumes   
   (LName nvarchar(25), FName nvarchar(25),   
    Resume xml( DOCUMENT HumanResources.HRResumeSchemaCollection) );  
```  
  
### H. Creating a partitioned table  
 The following example creates a partition function to partition a table or index into four partitions. Then, the example creates a partition scheme that specifies the filegroups in which to hold each of the four partitions. Finally, the example creates a table that uses the partition scheme. This example assumes the filegroups already exist in the database.  
  
```sql  
CREATE PARTITION FUNCTION myRangePF1 (int)  
    AS RANGE LEFT FOR VALUES (1, 100, 1000) ;  
GO  
  
CREATE PARTITION SCHEME myRangePS1  
    AS PARTITION myRangePF1  
    TO (test1fg, test2fg, test3fg, test4fg) ;  
GO  
  
CREATE TABLE PartitionTable (col1 int, col2 char(10))  
    ON myRangePS1 (col1) ;  
GO  
```  
  
 Based on the values of column `col1` of `PartitionTable`, the partitions are assigned in the following ways.  
  
|Filegroup|test1fg|test2fg|test3fg|test4fg|  
|---------------|-------------|-------------|-------------|-------------|  
|**Partition**|1|2|3|4|  
|**Values**|col 1 \<= 1|col1 > 1 AND col1 \<= 100|col1 > 100 AND col1 \<= 1,000|col1 > 1000|  
  
### I. Using the uniqueidentifier data type in a column  
 The following example creates a table with a `uniqueidentifier` column. The example uses a PRIMARY KEY constraint to protect the table against users inserting duplicated values, and it uses the `NEWSEQUENTIALID()` function in the `DEFAULT` constraint to provide values for new rows. The ROWGUIDCOL property is applied to the `uniqueidentifier` column so that it can be referenced using the $ROWGUID keyword.  
  
```sql  
CREATE TABLE dbo.Globally_Unique_Data  
    (guid uniqueidentifier   
        CONSTRAINT Guid_Default DEFAULT   
        NEWSEQUENTIALID() ROWGUIDCOL,  
    Employee_Name varchar(60)  
    CONSTRAINT Guid_PK PRIMARY KEY (guid) );  
```  
  
### J. Using an expression for a computed column  
 The following example shows the use of an expression (`(low + high)/2`) for calculating the `myavg` computed column.  
  
```sql  
CREATE TABLE dbo.mytable   
    ( low int, high int, myavg AS (low + high)/2 ) ;  
```  
  
### K. Creating a computed column based on a user-defined type column  
 The following example creates a table with one column defined as user-defined type `utf8string`, assuming that the type's assembly, and the type itself, have already been created in the current database. A second column is defined based on `utf8string`, and uses method `ToString()` of **type(class)**`utf8string` to compute a value for the column.  
  
```sql  
CREATE TABLE UDTypeTable   
    ( u utf8string, ustr AS u.ToString() PERSISTED ) ;  
```  
  
### L. Using the USER_NAME function for a computed column  
 The following example uses the `USER_NAME()` function in the `myuser_name` column.  
  
```sql  
CREATE TABLE dbo.mylogintable  
    ( date_in datetime, user_id int, myuser_name AS USER_NAME() ) ;  
```  
  
### M. Creating a table that has a FILESTREAM column  
 The following example creates a table that has a `FILESTREAM` column `Photo`. If a table has one or more `FILESTREAM` columns, the table must have one `ROWGUIDCOL` column.  
  
```sql  
CREATE TABLE dbo.EmployeePhoto  
    (  
     EmployeeId int NOT NULL PRIMARY KEY  
    ,Photo varbinary(max) FILESTREAM NULL  
    ,MyRowGuidColumn uniqueidentifier NOT NULL ROWGUIDCOL  
        UNIQUE DEFAULT NEWID()  
    );  
```  
  
### N. Creating a table that uses row compression  
 The following example creates a table that uses row compression.  
  
```sql  
CREATE TABLE dbo.T1   
(c1 int, c2 nvarchar(200) )  
WITH (DATA_COMPRESSION = ROW);  
```  
  
 For additional data compression examples, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
### O. Creating a table that has sparse columns and a column set  
 The following examples show to how to create a table that has a sparse column, and a table that has two sparse columns and a column set. The examples use the basic syntax. For more complex examples, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md) and [Use Column Sets](../../relational-databases/tables/use-column-sets.md).  
  
 This example creates a table that has a sparse column.  
  
```sql  
CREATE TABLE dbo.T1  
    (c1 int PRIMARY KEY,  
    c2 varchar(50) SPARSE NULL ) ;  
```  
  
 This example creates a table that has two sparse columns and a column set named `CSet`.  
  
```sql  
CREATE TABLE T1  
    (c1 int PRIMARY KEY,  
    c2 varchar(50) SPARSE NULL,  
    c3 int SPARSE NULL,  
    CSet XML COLUMN_SET FOR ALL_SPARSE_COLUMNS ) ;  
```  
  
### P. Creating a system-versioned disk-based temporal table  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 The following examples show how to create a temporal table linked to a new history table, and how to create a temporal table linked to an existing history table. Note that the temporal table must have a primary key defined to be enabled for the table to be enabled for system versioning. For examples showing how to add or remove system versioning on an existing table, see System Versioning in [Examples](../../t-sql/statements/alter-table-transact-sql.md#Example_Top). For use cases, see [Temporal Tables](../../relational-databases/tables/temporal-tables.md).  
  
 This example creates a new temporal table linked to a new history table.  
  
```sql  
CREATE TABLE Department   
(  
    DepartmentNumber char(10) NOT NULL PRIMARY KEY CLUSTERED,   
    DepartmentName varchar(50) NOT NULL,   
    ManagerID int  NULL,   
    ParentDepartmentNumber char(10) NULL,   
    SysStartTime datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,   
    SysEndTime datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,     
    PERIOD FOR SYSTEM_TIME (SysStartTime,SysEndTime)     
)  
WITH (SYSTEM_VERSIONING = ON);  
```  
  
 This example creates a new temporal table linked to an existing history table.  
  
```sql  
--Existing table   
CREATE TABLE Department_History   
(  
    DepartmentNumber char(10) NOT NULL,   
    DepartmentName varchar(50) NOT NULL,   
    ManagerID int  NULL,   
    ParentDepartmentNumber char(10) NULL,   
    SysStartTime datetime2 NOT NULL,   
    SysEndTime datetime2 NOT NULL   
);  
--Temporal table  
CREATE TABLE Department   
(  
    DepartmentNumber char(10) NOT NULL PRIMARY KEY CLUSTERED,   
    DepartmentName varchar(50) NOT NULL,   
    ManagerID INT  NULL,   
    ParentDepartmentNumber char(10) NULL,   
    SysStartTime datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,   
    SysEndTime datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,     
    PERIOD FOR SYSTEM_TIME (SysStartTime,SysEndTime)    
)  
WITH   
    (SYSTEM_VERSIONING = ON   
        (HISTORY_TABLE = dbo.Department_History, DATA_CONSISTENCY_CHECK = ON )  
    );  
```  
  
### Q. Creating a system-versioned memory-optimized temporal table  
   
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].
  
 The following example shows how to create a system-versioned memory-optimized temporal table linked to a new disk-based history table.  
  
 This example creates a new temporal table linked to a new history table.  
  
```sql  
CREATE SCHEMA History  
GO  
CREATE TABLE dbo.Department   
(  
    DepartmentNumber char(10) NOT NULL PRIMARY KEY NONCLUSTERED,   
    DepartmentName varchar(50) NOT NULL,   
    ManagerID int  NULL,   
    ParentDepartmentNumber char(10) NULL,   
    SysStartTime datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,   
    SysEndTime datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,     
    PERIOD FOR SYSTEM_TIME (SysStartTime,SysEndTime)     
)  
WITH   
    (  
        MEMORY_OPTIMIZED = ON, DURABILITY = SCHEMA_AND_DATA,  
            SYSTEM_VERSIONING = ON ( HISTORY_TABLE = History.DepartmentHistory )   
    );  
```  
  
 This example creates a new temporal table linked to an existing history table.  
  
```sql 
--Existing table   
CREATE TABLE Department_History   
(  
    DepartmentNumber char(10) NOT NULL,   
    DepartmentName varchar(50) NOT NULL,   
    ManagerID int  NULL,   
    ParentDepartmentNumber char(10) NULL,   
    SysStartTime datetime2 NOT NULL,   
    SysEndTime datetime2 NOT NULL   
);  
--Temporal table  
CREATE TABLE Department   
(  
    DepartmentNumber char(10) NOT NULL PRIMARY KEY CLUSTERED,   
    DepartmentName varchar(50) NOT NULL,   
    ManagerID INT  NULL,   
    ParentDepartmentNumber char(10) NULL,   
    SysStartTime datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,   
    SysEndTime datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,     
    PERIOD FOR SYSTEM_TIME (SysStartTime,SysEndTime)    
)  
WITH   
    (SYSTEM_VERSIONING = ON   
        (HISTORY_TABLE = dbo.Department_History, DATA_CONSISTENCY_CHECK = ON )  
    );  
```  
  
### R. Creating a table with encrypted columns  
 The following example creates a table with two encrypted columns. For more information, see [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md).  
  
```sql  
CREATE TABLE Customers (  
    CustName nvarchar(60)   
        ENCRYPTED WITH   
            (  
             COLUMN_ENCRYPTION_KEY = MyCEK,  
             ENCRYPTION_TYPE = RANDOMIZED,  
             ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'  
            ),   
    SSN varchar(11) COLLATE  Latin1_General_BIN2  
        ENCRYPTED WITH   
            (  
             COLUMN_ENCRYPTION_KEY = MyCEK,  
             ENCRYPTION_TYPE = DETERMINISTIC ,  
             ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'  
            ),   
    Age int NULL  
);  
```

### S. Create an inline filtered index 
Creates a table with an inline filtered index.
  
```sql
  CREATE TABLE t1 
  (
      c1 int,
      index IX1  (c1) WHERE c1 > 0   
 )
GO
```

### T. Create a temporary table with an anonymously named compound primary key
Creates a table with an anonymously named compound primary key. This is useful to avoid run-time conflicts where two session-scoped temp tables, each in a separate session, use the same name for a constraint. 
  
```
  CREATE TABLE #tmp 
 (
      c1 int,
      c2 int,
      PRIMARY KEY CLUSTERED ([c1], [c2])
 )
GO
```

If you explicitly name the constraint, the second session will generate an error such as:
 
```
Msg 2714, Level 16, State 5, Line 1
There is already an object named 'PK_#tmp' in the database.
Msg 1750, Level 16, State 1, Line 1
Could not create constraint or index. See previous errors.
```

The problem arises from the fact that while the temp table name is uniquified, the constraint names are not.
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md)   
 [CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)   
 [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)   
 [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)   
 [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)   
 [CREATE PARTITION FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-function-transact-sql.md)   
 [CREATE PARTITION SCHEME &#40;Transact-SQL&#41;](../../t-sql/statements/create-partition-scheme-transact-sql.md)   
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helpconstraint &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpconstraint-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)  
  
  


