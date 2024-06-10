---
title: "OPENROWSET (Transact-SQL)"
description: "OPENROWSET includes all connection information that is required to access remote data from an OLE DB data source."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest, hudequei, wiassaf, nzagorac
ms.date: 06/06/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OPENROWSET_TSQL"
  - "OPENROWSET"
helpviewer_keywords:
  - "data sources [SQL Server]"
  - "OPENROWSET function"
  - "remote data access [SQL Server], OPENROWSET"
  - "ad hoc distributed queries"
  - "OPENROWSET function, Transact-SQL"
  - "OPENROWSET statement"
  - "OLE DB data sources [SQL Server]"
  - "ad hoc connection information"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# OPENROWSET (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Includes all connection information that is required to access remote data from an OLE DB data source. This method is an alternative to accessing tables in a linked server and is a one-time, ad hoc method of connecting and accessing remote data by using OLE DB. For more frequent references to OLE DB data sources, use linked servers instead. For more information, see [Linked Servers (Database Engine)](../../relational-databases/linked-servers/linked-servers-database-engine.md). The `OPENROWSET` function can be referenced in the `FROM` clause of a query as if it were a table name. The `OPENROWSET` function can also be referenced as the target table of an `INSERT`, `UPDATE`, or `DELETE` statement, subject to the capabilities of the OLE DB provider. Although the query might return multiple result sets, `OPENROWSET` returns only the first one.

`OPENROWSET` also supports bulk operations through a built-in `BULK` provider that enables data from a file to be read and returned as a rowset.

Many examples in this article only apply to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only. Details and links to similar examples on other platforms:

- Azure SQL Database only supports reading from Azure Blob Storage.
- For examples on [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Query data sources using OPENROWSET](/azure/azure-sql/managed-instance/data-virtualization-overview#query-data-sources-using-openrowset).
- For information and examples with serverless SQL pools in Azure Synapse, see [How to use OPENROWSET using serverless SQL pool in Azure Synapse Analytics](/azure/synapse-analytics/sql/develop-openrowset).
- Dedicated SQL pools in Azure Synapse don't support the `OPENROWSET` function.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
OPENROWSET
( { 'provider_name'
    , { 'datasource' ; 'user_id' ; 'password' | 'provider_string' }
    , {   <table_or_view> | 'query' }
   | BULK 'data_file' ,
       { FORMATFILE = 'format_file_path' [ <bulk_options> ]
       | SINGLE_BLOB | SINGLE_CLOB | SINGLE_NCLOB }
} )

<table_or_view> ::= [ catalog. ] [ schema. ] object

<bulk_options> ::=
   [ , DATASOURCE = 'data_source_name' ]
   [ , ERRORFILE = 'file_name' ]
   [ , ERRORFILE_DATA_SOURCE = 'data_source_name' ]
   [ , MAXERRORS = maximum_errors ]
   [ , FIRSTROW = first_row ]
   [ , LASTROW = last_row ]
   [ , ROWS_PER_BATCH = rows_per_batch ]
   [ , ORDER ( { column [ ASC | DESC ] } [ , ...n ] ) [ UNIQUE ] ]

   -- bulk_options related to input file format
   [ , CODEPAGE = { 'ACP' | 'OEM' | 'RAW' | 'code_page' } ]
   [ , FORMAT = { 'CSV' | 'PARQUET' | 'DELTA' } ]
   [ , FIELDQUOTE = 'quote_characters' ]
   [ , FORMATFILE = 'format_file_path' ]
   [ , FORMATFILE_DATA_SOURCE = 'data_source_name' ]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

### Common arguments

#### '*provider_name*'

A character string that represents the friendly name (or `PROGID`) of the OLE DB provider as specified in the registry. *provider_name* has no default value. Provider name examples are `Microsoft.Jet.OLEDB.4.0`, `SQLNCLI`, or `MSDASQL`.

#### '*datasource*'

A string constant that corresponds to a particular OLE DB data source. *datasource* is the `DBPROP_INIT_DATASOURCE` property to be passed to the `IDBProperties` interface of the provider to initialize the provider. Typically, this string includes the name of the database file, the name of a database server, or a name that the provider understands for locating the database or databases.

Data source can be file path `C:\SAMPLES\Northwind.mdb'` for `Microsoft.Jet.OLEDB.4.0` provider, or connection string `Server=Seattle1;Trusted_Connection=yes;` for `SQLNCLI` provider.

#### '*user_id*'

A string constant that is the user name passed to the specified OLE DB provider. *user_id* specifies the security context for the connection and is passed in as the `DBPROP_AUTH_USERID` property to initialize the provider. *user_id* can't be a Microsoft Windows login name.

#### '*password*'

A string constant that is the user password to be passed to the OLE DB provider. *password* is passed in as the `DBPROP_AUTH_PASSWORD` property when initializing the provider. *password* can't be a Microsoft Windows password.

```sql
SELECT a.* FROM OPENROWSET(
    'Microsoft.Jet.OLEDB.4.0',
    'C:\SAMPLES\Northwind.mdb';
    'admin';
    'password',
    Customers
) AS a;
```

#### '*provider_string*'

A provider-specific connection string that is passed in as the `DBPROP_INIT_PROVIDERSTRING` property to initialize the OLE DB provider. *provider_string* typically encapsulates all the connection information required to initialize the provider. For a list of keywords that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider recognizes, see [Initialization and Authorization Properties (Native Client OLE DB Provider)](../../relational-databases/native-client-ole-db-data-source-objects/initialization-and-authorization-properties.md).

```sql
SELECT d.* FROM OPENROWSET(
    'SQLNCLI',
    'Server=Seattle1;Trusted_Connection=yes;',
    Department
) AS d;
```

#### <table_or_view>

Remote table or view containing the data that `OPENROWSET` should read. It can be three-part-name object with the following components:

- *catalog* (optional) - the name of the catalog or database in which the specified object resides.
- *schema* (optional) - the name of the schema or object owner for the specified object.
- *object* - the object name that uniquely identifies the object to work with.

```sql
SELECT d.* FROM OPENROWSET(
    'SQLNCLI',
    'Server=Seattle1;Trusted_Connection=yes;',
    AdventureWorks2022.HumanResources.Department
) AS d;
```

#### '*query*'

A string constant sent to and executed by the provider. The local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't process this query, but processes query results returned by the provider, a pass-through query. Pass-through queries are useful when used on providers that don't make available their tabular data through table names, but only through a command language. Pass-through queries are supported on the remote server, as long as the query provider supports the OLE DB Command object and its mandatory interfaces. For more information, see [SQL Server Native Client (OLE DB) Interfaces](../../relational-databases/native-client-ole-db-interfaces/sql-server-native-client-ole-db-interfaces.md).

```sql
SELECT a.*
FROM OPENROWSET(
    'SQLNCLI',
    'Server=Seattle1;Trusted_Connection=yes;',
    'SELECT TOP 10 GroupName, Name FROM AdventureWorks2022.HumanResources.Department'
) AS a;
```

### BULK arguments

Uses the `BULK` rowset provider for `OPENROWSET` to read data from a file. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], `OPENROWSET` can read from a data file without loading the data into a target table. This lets you use `OPENROWSET` with a basic `SELECT` statement.

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

The arguments of the `BULK` option allow for significant control over where to start and end reading data, how to deal with errors, and how data is interpreted. For example, you can specify that the data file is read as a single-row, single-column rowset of type **varbinary**, **varchar**, or **nvarchar**. The default behavior is described in the argument descriptions that follow.

For information about how to use the `BULK` option, see the [Remarks](#remarks) section later in this article. For information about the permissions that the `BULK` option requires, see the [Permissions](#permissions) section, later in this article.

> [!NOTE]  
> When used to import data with the full recovery model, `OPENROWSET (BULK ...)` doesn't optimize logging.

For information on preparing data for bulk import, see [Prepare data for bulk export or import](../../relational-databases/import-export/prepare-data-for-bulk-export-or-import-sql-server.md).

#### BULK '*data_file*'

The full path of the data file whose data is to be copied into the target table.

```sql
SELECT * FROM OPENROWSET(
   BULK 'C:\DATA\inv-2017-01-19.csv',
   SINGLE_CLOB
) AS DATA;
```

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the data_file can be in Azure Blob Storage. For examples, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

### BULK error handling options

#### ERRORFILE = '*file_name*'

Specifies the file used to collect rows that have formatting errors and can't be converted to an OLE DB rowset. These rows are copied into this error file from the data file "as is."

The error file is created at the start of the command execution. An error is raised if the file already exists. Additionally, a control file that has the extension .ERROR.txt is created. This file references each row in the error file and provides error diagnostics. After the errors are corrected, the data can be loaded.

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the `error_file_path` can be in Azure Blob Storage.

#### ERRORFILE_DATA_SOURCE_NAME

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], this argument is a named external data source pointing to the Azure Blob storage location of the error file that will contain errors found during the import. The external data source must be created using the `TYPE = BLOB_STORAGE`. For more information, see [CREATE EXTERNAL DATA SOURCE](../statements/create-external-data-source-transact-sql.md).

#### MAXERRORS = *maximum_errors*

Specifies the maximum number of syntax errors or nonconforming rows, as defined in the format file, which can occur before `OPENROWSET` throws an exception. Until `MAXERRORS` is reached, `OPENROWSET` ignores each bad row, not loading it, and counts the bad row as one error.

The default for *maximum_errors* is 10.

> [!NOTE]  
> `MAX_ERRORS` doesn't apply to `CHECK` constraints, or to converting **money** and **bigint** data types.

### BULK data processing options

#### FIRSTROW = *first_row*

Specifies the number of the first row to load. The default is 1. This indicates the first row in the specified data file. The row numbers are determined by counting the row terminators. `FIRSTROW` is 1-based.

#### LASTROW = *last_row*

Specifies the number of the last row to load. The default is 0. This indicates the last row in the specified data file.

#### ROWS_PER_BATCH = *rows_per_batch*

Specifies the approximate number of rows of data in the data file. This value should be of the same order as the actual number of rows.

`OPENROWSET` always imports a data file as a single batch. However, if you specify *rows_per_batch* with a value > 0, the query processor uses the value of *rows_per_batch* as a hint for allocating resources in the query plan.

By default, `ROWS_PER_BATCH` is unknown. Specifying `ROWS_PER_BATCH = 0` is the same as omitting `ROWS_PER_BATCH`.

#### ORDER ( { *column* [ ASC | DESC ] } [ ,... *n* ] [ UNIQUE ] )

An optional hint that specifies how the data in the data file is sorted. By default, the bulk operation assumes the data file is unordered. Performance can improve if the query optimizer can exploit the order to generate a more efficient query plan. The following list provides examples for when specifying a sort can be beneficial:

- Inserting rows into a table that has a clustered index, where the rowset data is sorted on the clustered index key.
- Joining the rowset with another table, where the sort and join columns match.
- Aggregating the rowset data by the sort columns.
- Using the rowset as a source table in the `FROM` clause of a query, where the sort and join columns match.

#### UNIQUE

Specifies that the data file doesn't have duplicate entries.

If the actual rows in the data file aren't sorted according to the order that is specified, or if the `UNIQUE` hint is specified and duplicates keys are present, an error is returned.

Column aliases are required when `ORDER` is used. The column alias list must reference the derived table that is being accessed by the `BULK` clause. The column names that are specified in the `ORDER` clause refer to this column alias list. Large value types (**varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml**) and large object (LOB) types (**text**, **ntext**, and **image**) columns can't be specified.

#### SINGLE_BLOB

Returns the contents of *data_file* as a single-row, single-column rowset of type **varbinary(max)**.

> [!IMPORTANT]  
> We recommend that you import XML data only using the `SINGLE_BLOB` option, rather than `SINGLE_CLOB` and `SINGLE_NCLOB`, because only `SINGLE_BLOB` supports all Windows encoding conversions.

#### SINGLE_CLOB

By reading *data_file* as ASCII, returns the contents as a single-row, single-column rowset of type **varchar(max)**, using the collation of the current database.

#### SINGLE_NCLOB

By reading *data_file* as Unicode, returns the contents as a single-row, single-column rowset of type **nvarchar(max)**, using the collation of the current database.

```sql
SELECT * FROM OPENROWSET(
    BULK N'C:\Text1.txt',
    SINGLE_NCLOB
) AS Document;
```

### BULK input file format options

#### CODEPAGE = { 'ACP' | 'OEM' | 'RAW' | '*code_page*' }

Specifies the code page of the data in the data file. `CODEPAGE` is relevant only if the data contains **char**, **varchar**, or **text** columns with character values more than 127 or less than 32.

> [!IMPORTANT]  
> `CODEPAGE` isn't a supported option on Linux.

> [!NOTE]  
> We recommend that you specify a collation name for each column in a format file, except when you want the 65001 option to have priority over the collation/code page specification.

| CODEPAGE value | Description |
| --- | --- |
| `ACP` | Converts columns of **char**, **varchar**, or **text** data type from the ANSI/[!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows code page (ISO 1252) to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| `OEM` (default) | Converts columns of **char**, **varchar**, or **text** data type from the system OEM code page to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] code page. |
| `RAW` | No conversion occurs from one code page to another. This is the fastest option. |
| `code_page` | Indicates the source code page on which the character data in the data file is encoded; for example, 850.<br /><br />**Important** Versions before [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] don't support code page 65001 (UTF-8 encoding). |

#### FORMAT = { 'CSV' | 'PARQUET' | 'DELTA' }

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], this argument specifies a comma separated values file compliant to the [RFC 4180](https://datatracker.ietf.org/doc/html/rfc4180) standard.

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], both Parquet and Delta formats are supported.

```sql
SELECT *
FROM OPENROWSET(BULK N'D:\XChange\test-csv.csv',
    FORMATFILE = N'D:\XChange\test-csv.fmt',
    FIRSTROW=2,
    FORMAT='CSV') AS cars;
```

#### FORMATFILE = '*format_file_path*'

Specifies the full path of a format file. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports two types of format files: XML and non-XML.

A format file is required to define column types in the result set. The only exception is when `SINGLE_CLOB`, `SINGLE_BLOB`, or `SINGLE_NCLOB` is specified; in which case, the format file isn't required.

For information about format files, see [Use a format file to bulk import data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md).

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the format_file_path can be in Azure Blob Storage. For examples, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

#### FIELDQUOTE = '*field_quote*'

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], this argument specifies a character that is used as the quote character in the CSV file. If not specified, the quote character (`"`) is used as the quote character as defined in the [RFC 4180](https://datatracker.ietf.org/doc/html/rfc4180) standard.

## Remarks

`OPENROWSET` can be used to access remote data from OLE DB data sources only when the **DisallowAdhocAccess** registry option is explicitly set to 0 for the specified provider, and the Ad Hoc Distributed Queries advanced configuration option is enabled. When these options aren't set, the default behavior doesn't allow for ad hoc access.

When you access remote OLE DB data sources, the login identity of trusted connections isn't automatically delegated from the server on which the client is connected to the server that is being queried. Authentication delegation must be configured.

Catalog and schema names are required if the OLE DB provider supports multiple catalogs and schemas in the specified data source. Values for *catalog* and *schema* can be omitted when the OLE DB provider doesn't support them. If the provider supports only schema names, a two-part name of the form *schema*.*object* must be specified. If the provider supports only catalog names, a three-part name of the form *catalog*.*schema*.*object* must be specified. Three-part names must be specified for pass-through queries that use the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider. For more information, see [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

`OPENROWSET` doesn't accept variables for its arguments.

Any call to `OPENDATASOURCE`, `OPENQUERY`, or `OPENROWSET` in the `FROM` clause is evaluated separately and independently from any call to these functions used as the target of the update, even if identical arguments are supplied to the two calls. In particular, filter or join conditions applied on the result of one of those calls has no effect on the results of the other.

## Use OPENROWSET with the BULK option

The following [!INCLUDE [tsql](../../includes/tsql-md.md)] enhancements support the `OPENROWSET(BULK...)` function:

- A `FROM` clause that is used with `SELECT` can call `OPENROWSET(BULK...)` instead of a table name, with full `SELECT` functionality.

   `OPENROWSET` with the `BULK` option requires a correlation name, also known as a range variable or alias, in the `FROM` clause. Column aliases can be specified. If a column alias list isn't specified, the format file must have column names. Specifying column aliases overrides the column names in the format file, such as:

  - `FROM OPENROWSET(BULK...) AS table_alias`
  - `FROM OPENROWSET(BULK...) AS table_alias(column_alias,...n)`

  > [!IMPORTANT]  
  > Failure to add the `AS <table_alias>` will result in the error:
  > Msg 491, Level 16, State 1, Line 20
  > A correlation name must be specified for the bulk rowset in the from clause.

- A `SELECT...FROM OPENROWSET(BULK...)` statement queries the data in a file directly, without importing the data into a table. `SELECT...FROM OPENROWSET(BULK...)` statements can also list bulk-column aliases by using a format file to specify column names, and also data types.
- Using `OPENROWSET(BULK...)` as a source table in an `INSERT` or `MERGE` statement bulk imports data from a data file into a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table. For more information, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).
- When the `OPENROWSET BULK` option is used with an `INSERT` statement, the `BULK` clause supports table hints. In addition to the regular table hints, such as `TABLOCK`, the `BULK` clause can accept the following specialized table hints: `IGNORE_CONSTRAINTS` (ignores only the `CHECK` and `FOREIGN KEY` constraints), `IGNORE_TRIGGERS`, `KEEPDEFAULTS`, and `KEEPIDENTITY`. For more information, see [Table Hints (Transact-SQL)](../queries/hints-transact-sql-table.md).

  For information about how to use `INSERT...SELECT * FROM OPENROWSET(BULK...)` statements, see [Bulk Import and Export of Data (SQL Server)](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md). For information about when row-insert operations that are performed by bulk import are logged in the transaction log, see [Prerequisites for minimal logging in bulk import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).

> [!NOTE]  
> When you use `OPENROWSET`, it's important to understand how [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] handles impersonation. For information about security considerations, see [Use BULK INSERT or OPENROWSET(BULK...) to import data to SQL Server](../../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md).

### Bulk importing SQLCHAR, SQLNCHAR, or SQLBINARY data

`OPENROWSET(BULK...)` assumes that, if not specified, the maximum length of `SQLCHAR`, `SQLNCHAR`, or `SQLBINARY` data doesn't exceed 8,000 bytes. If the data being imported is in a LOB data field that contains any **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** objects that exceed 8,000 bytes, you must use an XML format file that defines the maximum length for the data field. To specify the maximum length, edit the format file and declare the MAX_LENGTH attribute.

> [!NOTE]  
> An automatically generated format file doesn't specify the length or maximum length for a LOB field. However, you can edit a format file and specify the length or maximum length manually.

### Bulk exporting or importing SQLXML documents

To bulk export or import SQLXML data, use one of the following data types in your format file.

| Data type | Effect |
| --- | --- |
| `SQLCHAR` or `SQLVARYCHAR` | The data is sent in the client code page, or in the code page implied by the collation. |
| `SQLNCHAR` or `SQLNVARCHAR` | The data is sent as Unicode. |
| `SQLBINARY` or `SQLVARYBIN` | The data is sent without any conversion. |

## Permissions

`OPENROWSET` permissions are determined by the permissions of the user name that is being passed to the OLE DB provider. To use the `BULK` option requires `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` permission.

## Examples

This section provides general examples to demonstrate how to use OPENROWSET.

### A. Use OPENROWSET with SELECT and the SQL Server Native Client OLE DB Provider

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

[!INCLUDE [snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

The following example uses the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider to access the `HumanResources.Department` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on the remote server `Seattle1`. (Use SQLNCLI and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] will redirect to the latest version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider.) A `SELECT` statement is used to define the row set returned. The provider string contains the `Server` and `Trusted_Connection` keywords. These keywords are recognized by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider.

```sql
SELECT a.*
FROM OPENROWSET(
    'SQLNCLI', 'Server=Seattle1;Trusted_Connection=yes;',
    'SELECT GroupName, Name, DepartmentID
         FROM AdventureWorks2022.HumanResources.Department
         ORDER BY GroupName, Name'
) AS a;
```

### B. Use the Microsoft OLE DB Provider for Jet

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example accesses the `Customers` table in the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Access `Northwind` database through the [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet.

> [!NOTE]  
> This example assumes that Microsoft Access is installed. To run this example, you must install the `Northwind` database.

```sql
SELECT CustomerID, CompanyName
FROM OPENROWSET(
    'Microsoft.Jet.OLEDB.4.0',
    'C:\Program Files\Microsoft Office\OFFICE11\SAMPLES\Northwind.mdb';
    'admin';'',
    Customers
);
```

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

### C. Use OPENROWSET and another table in an INNER JOIN

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example selects all data from the `Customers` table from the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] `Northwind` database and from the `Orders` table from the Access `Northwind` database stored on the same computer.

> [!NOTE]  
> This example assumes that Access is installed. To run this example, you must install the `Northwind` database.

```sql
USE Northwind;
GO

SELECT c.*, o.*
FROM Northwind.dbo.Customers AS c
INNER JOIN OPENROWSET(
        'Microsoft.Jet.OLEDB.4.0',
        'C:\Program Files\Microsoft Office\OFFICE11\SAMPLES\Northwind.mdb';'admin';'',
        Orders) AS o
    ON c.CustomerID = o.CustomerID;
```

> [!IMPORTANT]  
> [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] only supports reading from Azure Blob Storage.

### D. Use OPENROWSET to BULK INSERT file data into a varbinary(max) column

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example creates a small table for demonstration purposes, and inserts file data from a file named `Text1.txt` located in the `C:` root directory into a **varbinary(max)** column.

```sql
CREATE TABLE myTable (
    FileName NVARCHAR(60),
    FileType NVARCHAR(60),
    Document VARBINARY(MAX)
);
GO

INSERT INTO myTable (
    FileName,
    FileType,
    Document
)
SELECT 'Text1.txt' AS FileName,
    '.txt' AS FileType,
    *
FROM OPENROWSET(
    BULK N'C:\Text1.txt',
    SINGLE_BLOB
) AS Document;
GO
```

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

### E. Use the OPENROWSET BULK provider with a format file to retrieve rows from a text file

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example uses a format file to retrieve rows from a tab-delimited text file, `values.txt` that contains the following data:

```output
1     Data Item 1
2     Data Item 2
3     Data Item 3
```

The format file, `values.fmt`, describes the columns in `values.txt`:

```output
9.0
2
1  SQLCHAR  0  10 "\t"    1  ID           SQL_Latin1_General_Cp437_BIN
2  SQLCHAR  0  40 "\r\n"  2  Description  SQL_Latin1_General_Cp437_BIN
```

This query retrieves that data:

```sql
SELECT a.* FROM OPENROWSET(
    BULK 'C:\test\values.txt',
   FORMATFILE = 'C:\test\values.fmt'
) AS a;
```

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

### F. Specify a format file and code page

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

The following example shows how to use both the format file and code page options at the same time.

```sql
INSERT INTO MyTable
SELECT a.* FROM OPENROWSET (
    BULK N'D:\data.csv',
    FORMATFILE = 'D:\format_no_collation.txt',
    CODEPAGE = '65001'
) AS a;
```

### G. Access data from a CSV file with a format file

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions only.

```sql
SELECT * FROM OPENROWSET(
    BULK N'D:\XChange\test-csv.csv',
    FORMATFILE = N'D:\XChange\test-csv.fmt',
    FIRSTROW = 2,
    FORMAT = 'CSV'
) AS cars;
```

> [!IMPORTANT]  
> [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] only supports reading from Azure Blob Storage.

### H. Access data from a CSV file without a format file

**Applies to:** [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only.

```sql
SELECT * FROM OPENROWSET(
   BULK 'C:\Program Files\Microsoft SQL Server\MSSQL14.CTP1_1\MSSQL\DATA\inv-2017-01-19.csv',
   SINGLE_CLOB
) AS DATA;
```

```sql
SELECT *
FROM OPENROWSET('MSDASQL',
    'Driver={Microsoft Access Text Driver (*.txt, *.csv)}',
    'SELECT * FROM E:\Tlog\TerritoryData.csv'
);
```

> [!IMPORTANT]  
> The ODBC driver should be 64-bit. Open the **Drivers** tab of the [Connect to an ODBC Data Source (SQL Server Import and Export Wizard)](../../integration-services/import-export-data/connect-to-an-odbc-data-source-sql-server-import-and-export-wizard.md) application in Windows to verify this. There's 32-bit `Microsoft Text Driver (*.txt, *.csv)` that will not work with a 64-bit version of `sqlservr.exe`.

### I. Access data from a file stored on Azure Blob Storage

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions only.

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, the following example uses an external data source that points to a container in an Azure storage account and a database scoped credential created for a shared access signature.

```sql
SELECT * FROM OPENROWSET(
   BULK 'inv-2017-01-19.csv',
   DATA_SOURCE = 'MyAzureInvoices',
   SINGLE_CLOB
) AS DataFile;
```

For complete `OPENROWSET` examples including configuring the credential and external data source, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

### <a id="j-importing-into-a-table-from-a-file-stored-on-azure-blob-storage"></a> J. Import into a table from a file stored on Azure Blob Storage

The following example shows how to use the `OPENROWSET` command to load data from a csv file in an Azure Blob storage location on which you created the SAS key. The Azure Blob storage location is configured as an external data source. This requires a database scoped credential using a shared access signature that is encrypted using a master key in the user database.

```sql
-- Optional: a MASTER KEY is not required if a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'YourStrongPassword1';
GO

-- Optional: a DATABASE SCOPED CREDENTIAL is not required because the blob is configured for public (anonymous) access!
CREATE DATABASE SCOPED CREDENTIAL MyAzureBlobStorageCredential
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
    SECRET = '******srt=sco&sp=rwac&se=2017-02-01T00:55:34Z&st=2016-12-29T16:55:34Z***************';

-- Make sure that you don't have a leading ? in the SAS token, and that you
-- have at least read permission on the object that should be loaded srt=o&sp=r,
-- and that expiration period is valid (all dates are in UTC time)
CREATE EXTERNAL DATA SOURCE MyAzureBlobStorage
WITH (
    TYPE = BLOB_STORAGE,
    LOCATION = 'https://****************.blob.core.windows.net/curriculum',
    -- CREDENTIAL is not required if a blob is configured for public (anonymous) access!
    CREDENTIAL = MyAzureBlobStorageCredential
);

INSERT INTO achievements
WITH (TABLOCK) (
    id,
    description
)
SELECT * FROM OPENROWSET(
    BULK 'csv/achievements.csv',
    DATA_SOURCE = 'MyAzureBlobStorage',
    FORMAT = 'CSV',
    FORMATFILE = 'csv/achievements-c.xml',
    FORMATFILE_DATA_SOURCE = 'MyAzureBlobStorage'
) AS DataFile;
```

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

### K. Use a managed identity for an external source

The following example creates a credential by using a managed identity, creates an external source and then loads data from a CSV hosted on the external source.

First, create the credential and specify blob storage as the external source:

```sql
CREATE DATABASE SCOPED CREDENTIAL sampletestcred
WITH IDENTITY = 'MANAGED IDENTITY';

CREATE EXTERNAL DATA SOURCE SampleSource
WITH (
    TYPE = BLOB_STORAGE,
    LOCATION = 'https://****************.blob.core.windows.net/curriculum',
    CREDENTIAL = sampletestcred
);
```

Next, load data from the CSV file hosted on blob storage:

```sql
SELECT * FROM OPENROWSET(
    BULK 'Test - Copy.csv',
    DATA_SOURCE = 'SampleSource',
    SINGLE_CLOB
) as test;
```

> [!IMPORTANT]  
> Azure SQL Database only supports reading from Azure Blob Storage.

### L. Use OPENROWSET to access several Parquet files using S3-compatible object storage

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

The following example uses access several Parquet files from different location, all stored on S3-compatible object storage:

```sql
CREATE DATABASE SCOPED CREDENTIAL s3_dsc
WITH IDENTITY = 'S3 Access Key',
SECRET = 'contosoadmin:contosopwd';
GO

CREATE EXTERNAL DATA SOURCE s3_eds
WITH
(
    LOCATION = 's3://10.199.40.235:9000/movies',
    CREDENTIAL = s3_dsc
);
GO

SELECT * FROM OPENROWSET(
    BULK (
        '/decades/1950s/*.parquet',
        '/decades/1960s/*.parquet',
        '/decades/1970s/*.parquet'
    ),
    FORMAT = 'PARQUET',
    DATA_SOURCE = 's3_eds'
) AS data;
```

### M. Use OPENROWSET to access several Delta files from Azure Data Lake Gen2

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

In this example, the data table container is named `Contoso`, and is located on an Azure Data Lake Gen2 storage account.

```sql
CREATE DATABASE SCOPED CREDENTIAL delta_storage_dsc
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS Token>';

CREATE EXTERNAL DATA SOURCE Delta_ED
WITH (
    LOCATION = 'adls://<container>@<storage_account>.dfs.core.windows.net',
    CREDENTIAL = delta_storage_dsc
);

SELECT *
FROM OPENROWSET(
    BULK '/Contoso',
    FORMAT = 'DELTA',
    DATA_SOURCE = 'Delta_ED'
) AS result;
```

### More examples

For more examples that show using `INSERT...SELECT * FROM OPENROWSET(BULK...)`, see the following articles:

- [Examples of bulk import and export of XML documents (SQL Server)](../../relational-databases/import-export/examples-of-bulk-import-and-export-of-xml-documents-sql-server.md)
- [Keep identity values when bulk importing data (SQL Server)](../../relational-databases/import-export/keep-identity-values-when-bulk-importing-data-sql-server.md)
- [Keep nulls or default values during bulk import (SQL Server)](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)
- [Use a format file to bulk import data (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)
- [Use character format to import or export data (SQL Server)](../../relational-databases/import-export/use-character-format-to-import-or-export-data-sql-server.md)
- [Use a Format File to Skip a Table Column (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)
- [Use a format file to skip a data field (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)
- [Use a format file to map table columns to data-file fields (SQL Server)](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)
- [Query data sources using OPENROWSET in Azure SQL Managed Instances](/azure/azure-sql/managed-instance/data-virtualization-overview#query-data-sources-using-openrowset)

## Related content

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md)
- [Bulk Import and Export of Data (SQL Server)](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [OPENDATASOURCE (Transact-SQL)](opendatasource-transact-sql.md)
- [OPENQUERY (Transact-SQL)](openquery-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sp_serveroption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)
- [UPDATE (Transact-SQL)](../queries/update-transact-sql.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
