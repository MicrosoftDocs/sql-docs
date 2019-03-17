---
title: "column_definition (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/24/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "column_definition"
  - "column_definition_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "column_definition"
  - "ALTER TABLE statement"
  - "column properties [SQL Server]"
  - "column definitions [SQL Server]"
ms.assetid: a1742649-ca29-4d9b-9975-661cdbf18f78
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER TABLE column_definition (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Specifies the properties of a column that are added to a table by using [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
column_name <data_type>  
[ FILESTREAM ]  
[ COLLATE collation_name ]   
[ NULL | NOT NULL ]  
[   
    [ CONSTRAINT constraint_name ] DEFAULT constant_expression [ WITH VALUES ]   
    | IDENTITY [ ( seed , increment ) ] [ NOT FOR REPLICATION ]   
]  
[ ROWGUIDCOL ]   
[ SPARSE ]   
[ ENCRYPTED WITH  
  ( COLUMN_ENCRYPTION_KEY = key_name ,  
      ENCRYPTION_TYPE = { DETERMINISTIC | RANDOMIZED } ,   
      ALGORITHM =  'AEAD_AES_256_CBC_HMAC_SHA_256'   
  ) ]  
[ MASKED WITH ( FUNCTION = ' mask_function ') ]  
[ <column_constraint> [ ...n ] ]  
  
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
```  
  
## Arguments  
 *column_name*  
 Is the name of the column to be altered, added, or dropped. *column_name* can consist of 1 through 128 characters. For new columns, created with a timestamp data type, *column_name* can be omitted. If no *column_name* is specified for a **timestamp** data type column, the name **timestamp** is used.  
  
 [ _type_schema_name_**.** ] *type_name*  
 Is the data type for the column that is added and the schema to which it belongs.  
  
 *type_name* can be:  
  
-   A [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type.  
  
-   An alias data type based on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type. Alias data types must be created by using CREATE TYPE before they can be used in a table definition.  
  
-   A [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] user-defined type and the schema to which it belongs. A [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] user-defined type must be created by using CREATE TYPE before it can be used in a table definition.  
  
 If *type_schema_name* is not specified, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] references *type_name* in the following order:  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type.  
  
-   The default schema of the current user in the current database.  
  
-   The **dbo** schema in the current database.  
  
*precision*  
 Is the precision for the specified data type. For more information about valid precision values, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
*scale*  
 Is the scale for the specified data type. For more information about valid scale values, see [Precision, Scale, and Length &#40;Transact-SQL&#41;](../../t-sql/data-types/precision-scale-and-length-transact-sql.md).  
  
**max**  
 Applies only to the **varchar**, **nvarchar**, and **varbinary** data types. These are used for storing 2^31 bytes of character and binary data, and 2^30 bytes of Unicode data.  
  
**CONTENT**  
 Specifies that each instance of the **xml** data type in *column_name* can comprise multiple top-level elements. CONTENT applies only to the **xml** data type and can be specified only if *xml_schema_collection* is also specified. If this is not specified, CONTENT is the default behavior.  
  
DOCUMENT  
 Specifies that each instance of the **xml** data type in *column_name* can comprise only one top-level element. DOCUMENT applies only to the **xml** data type and can be specified only if *xml_schema_collection* is also specified.  
  
 *xml_schema_collection*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Applies only to the **xml** data type for associating an XML schema collection with the type. Before typing an **xml** column to a schema, the schema must first be created in the database by using [CREATE XML SCHEMA COLLECTION](../../t-sql/statements/create-xml-schema-collection-transact-sql.md).  
  
FILESTREAM  
 Optionally specifies the FILESTREAM storage attribute for column that has a *type_name* of **varbinary(max)**.  
  
 When FILESTREAM is specified for a column, the table must also have a column of the **uniqueidentifier** data type that has the ROWGUIDCOL attribute. This column must not allow null values and must have either a UNIQUE or PRIMARY KEY single-column constraint. The GUID value for the column must be supplied either by an application when data is being inserted, or by a DEFAULT constraint that uses the NEWID () function.  
  
 The ROWGUIDCOL column cannot be dropped and the related constraints cannot be changed while there is a FILESTREAM column defined for the table. The ROWGUIDCOL column can be dropped only after the last FILESTREAM column is dropped.  
  
 When the FILESTREAM storage attribute is specified for a column, all values for that column are stored in a FILESTREAM data container on the file system.  
  
 For an example that shows how to use column definition, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
COLLATE *collation_name*  
 Specifies the collation of the column. If not specified, the column is assigned the default collation of the database. Collation name can be either a Windows collation name or an SQL collation name. For a list and more information, see [Windows Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/windows-collation-name-transact-sql.md) and [SQL Server Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/sql-server-collation-name-transact-sql.md).  
  
 The COLLATE clause can be used to specify the collations only of columns of the **char**, **varchar**, **nchar**, and **nvarchar** data types.  
  
 For more information about the COLLATE clause, see [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md).  
  
 NULL | NOT NULL  
 Determines whether null values are allowed in the column. NULL is not strictly a constraint but can be specified just like NOT NULL.  
  
[ CONSTRAINT *constraint_name* ]  
 Specifies the start of a DEFAULT value definition. To maintain compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a constraint name can be assigned to a DEFAULT. *constraint_name* must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md), except that the name cannot start with a number sign (#). If *constraint_name* is not specified, a system-generated name is assigned to the DEFAULT definition.  
  
DEFAULT  
 Is a keyword that specifies the default value for the column. DEFAULT definitions can be used to provide values for a new column in the existing rows of data. DEFAULT definitions cannot be applied to **timestamp** columns, or columns with an IDENTITY property. If a default value is specified for a user-defined type column, the type must support an implicit conversion from *constant_expression* to the user-defined type.  
  
*constant_expression*  
 Is a literal value, a NULL, or a system function used as the default column value. If used in conjunction with a column defined to be of a [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] user-defined type, the implementation of the type must support an implicit conversion from the *constant_expression* to the user-defined type.  
  
WITH VALUES   
 When adding a column AND a DEFAULT constraint, if the column allows NULLS using WITH VALUES will, for existing rows, set the new column's value to the value given in DEFAULT *constant_expression*. 
 If the column being added does not allow NULLS, for existing rows, the column's value will always be set to the value given in the DEFAULT *constant expression*. 
 Starting in SQL Server 2012 this may be a meta data operation [adding-not-null-columns-as-an-online-operation](alter-table-transact-sql.md?view=sql-server-2017#adding-not-null-columns-as-an-online-operation).
 If this is used when the related column isn't also being added then it has no effect.
 
 Specifies that the value given in DEFAULT *constant_expression* is stored in a new column that is added to existing rows. If the added column allows null values and WITH VALUES is specified, the default value is stored in the new column that is added to existing rows. If WITH VALUES is not specified for columns that allow nulls, the value NULL is stored in the new column, in existing rows. If the new column does not allow nulls, the default value is stored in new rows regardless of whether WITH VALUES is specified.  
  
IDENTITY  
 Specifies that the new column is an identity column. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] provides a unique, incremental value for the column. When you add identifier columns to existing tables, the identity numbers are added to the existing rows of the table with the seed and increment values. The order in which the rows are updated is not guaranteed. Identity numbers are also generated for any new rows that are added.  
  
 Identity columns are commonly used in conjunction with PRIMARY KEY constraints to serve as the unique row identifier for the table. The IDENTITY property can be assigned to a **tinyint**, **smallint**, **int**, **bigint**, **decimal(p,0)**, or **numeric(p,0)** column. Only one identity column can be created per table. The DEFAULT keyword and bound defaults cannot be used with an identity column. Either both the seed and increment must be specified, or neither. If neither are specified, the default is (1,1).  
  
> [!NOTE]  
>  You cannot modify an existing table column to add the IDENTITY property.  
  
 Adding an identity column to a published table is not supported because it can result in nonconvergence when the column is replicated to the Subscriber. The values in the identity column at the Publisher depend on the order in which the rows for the affected table are physically stored. The rows might be stored differently at the Subscriber; therefore, the value for the identity column can be different for the same rows..  
  
 To disable the IDENTITY property of a column by allowing values to be explicitly inserted, use [SET IDENTITY_INSERT](../../t-sql/statements/set-identity-insert-transact-sql.md).  
  
*seed*  
 Is the value used for the first row loaded into the table.  
  
*increment*  
 Is the incremental value added to the identity value of the previous row that is loaded.  
  
NOT FOR REPLICATION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Can be specified for the IDENTITY property. If this clause is specified for the IDENTITY property, values are not incremented in identity columns when replication agents perform insert operations.  
  
ROWGUIDCOL  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies that the column is a row globally unique identifier column. ROWGUIDCOL can only be assigned to a **uniqueidentifier** column, and only one **uniqueidentifier** column per table can be designated as the ROWGUIDCOL column. ROWGUIDCOL cannot be assigned to columns of user-defined data types.  
  
 ROWGUIDCOL does not enforce uniqueness of the values stored in the column. Also, ROWGUIDCOL does not automatically generate values for new rows that are inserted into the table. To generate unique values for each column, either use the NEWID function on INSERT statements or specify the NEWID function as the default for the column. For more information, see [NEWID &#40;Transact-SQL&#41;](../../t-sql/functions/newid-transact-sql.md)and [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md).  
  
SPARSE  
 Indicates that the column is a sparse column. The storage of sparse columns is optimized for null values. Sparse columns cannot be designated as NOT NULL. For additional restrictions and more information about sparse columns, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).  
  
\<column_constraint>  
 For the definitions of the column constraint arguments, see [column_constraint &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-column-constraint-transact-sql.md).  
  
 ENCRYPTED WITH  
 Specifies encrypting columns by using the [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature.  
  
 COLUMN_ENCRYPTION_KEY = *key_name*  
 Specifies the column encryption key. For more information, see [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md).  
  
ENCRYPTION_TYPE = { DETERMINISTIC | RANDOMIZED }  
 **Deterministic encryption** uses a method which always generates the same encrypted value for any given plain text value. Using deterministic encryption allows searching using equality comparison, grouping, and joining tables using equality joins based on encrypted values, but can also allow unauthorized users to guess information about encrypted values by examining patterns in the encrypted column. Joining two tables on columns encrypted deterministically is only possible if both columns are encrypted using the same column encryption key. Deterministic encryption must use a column collation with a binary2 sort order for character columns.  
  
 **Randomized encryption** uses a method that encrypts data in a less predictable manner. Randomized encryption is more secure, but it prevents any computations and indexing on encrypted columns, unless your SQL Server instance supports [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).
  
 If you are using Always Encrypted (without secure enclaves), use deterministic encryption for columns to be searched with parameters or grouping parameters, for example a government ID number. Use randomized encryption, for data such as a credit card number, which is not grouped with other records, or used to join tables, and which is not searched for because you use other columns (such as a transaction number) to find the row which contains the encrypted column of interest.  

 If you are using Always Encrypted with secure enclaves, randomized encryption is a recommended encryption type.  
  
 Columns must be of a qualifying data type.  
  
ALGORITHM  
**Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
Must be **'AEAD_AES_256_CBC_HMAC_SHA_256'**.  
  
 For more information including feature constraints, see [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md).  
  
   
ADD MASKED WITH ( FUNCTION = ' *mask_function* ')  
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies a dynamic data mask. *mask_function* is the name of the masking function with the appropriate parameters. The following functions are available:  
  
-   default()  
  
-   email()  
  
-   partial()  
  
-   random()  
  
 For function parameters, see [Dynamic Data Masking](../../relational-databases/security/dynamic-data-masking.md).  
  
## Remarks  
 If a column is added having a **uniqueidentifier** data type, it can be defined with a default that uses the NEWID() function to supply the unique identifier values in the new column for each existing row in the table.  
  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not enforce an order for specifying DEFAULT, IDENTITY, ROWGUIDCOL, or column constraints in a column definition.  
  
 ALTER TABLE statement will fail if adding the column will cause the data row size to exceed 8060 bytes.  
  
## Examples  
 For examples, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md).  
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)  
  
