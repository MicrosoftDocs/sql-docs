---
title: "OPENROWSET (Transact-SQL)"
description: "OPENROWSET includes all connection information that is required to access remote data from an OLE DB data source."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest, hudequei, wiassaf, nzagorac, jovanpop
ms.date: 06/25/2025
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
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# OPENROWSET (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver2016.md)]

`OPENROWSET` is an alternative to accessing tables in a linked server and is a one-time, ad hoc method of connecting and accessing remote data. An `OPENROWSET` T-SQL command includes all connection information that is required to access remote data from an external data source.

The `OPENROWSET` function can be referenced in the `FROM` clause of a query as if it were a table name. The `OPENROWSET` function can also be referenced as the target table of an `INSERT`, `UPDATE`, or `DELETE` statement, subject to the capabilities of the data provider. Although the query might return multiple result sets, `OPENROWSET` returns only the first one.

> [!TIP]
> For more frequent references to external data sources, use linked servers instead. For more information, see [Linked Servers (Database Engine)](../../relational-databases/linked-servers/linked-servers-database-engine.md).

`OPENROWSET` without the `BULK` operator is available on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only. Details and links to similar examples on other platforms:

 - `OPENROWSET` supports bulk operations through a built-in `BULK` provider on many [!INCLUDE [ssde-md](../../includes/ssde-md.md)] platforms, including SQL Server, Azure SQL Database, Azure SQL Managed Instance, and Microsoft Fabric Data Warehouse. For more information, see [OPENROWSET BULK (Transact-SQL)](openrowset-bulk-transact-sql.md).
   - For examples on [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], see [Data virtualization with Azure SQL Database](/azure/azure-sql/database/data-virtualization-overview?view=azuresql-db&preserve-view=true).
   - For examples on [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], see [Data virtualization with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/data-virtualization-overview?view=azuresql-mi&preserve-view=true#query-data-sources-using-openrowset).
- For information and examples with serverless SQL pools in Azure Synapse, see [How to use OPENROWSET using serverless SQL pool in Azure Synapse Analytics](/azure/synapse-analytics/sql/develop-openrowset). Dedicated SQL pools in Azure Synapse don't support the `OPENROWSET` function.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

`OPENROWSET` syntax is used to query external data sources:

```syntaxsql
OPENROWSET
(  'provider_name'
    , { 'datasource' ; 'user_id' ; 'password' | 'provider_string' }
    , {  [ catalog. ] [ schema. ] object | 'query' }
)
```

## Arguments

#### '*provider_name*'

A character string that represents the friendly name (or `PROGID`) of the data provider as specified in the registry. *provider_name* has no default value. Provider name examples are `MSOLEDBSQL`, `Microsoft.Jet.OLEDB.4.0`, or `MSDASQL`.

#### '*datasource*'

A string constant that corresponds to a particular data source. *datasource* is the `DBPROP_INIT_DATASOURCE` property to be passed to the `IDBProperties` interface of the provider to initialize the provider. Typically, this string includes the name of the database file, the name of a database server, or a name that the provider understands for locating the database or databases.

Data source can be file path `C:\SAMPLES\Northwind.mdb'` for `Microsoft.Jet.OLEDB.4.0` provider, or connection string `Server=Seattle1;Trusted_Connection=yes;` for `MSOLEDBSQL` provider.

#### '*user_id*'

A string constant that is the user name passed to the specified data provider. *user_id* specifies the security context for the connection and is passed in as the `DBPROP_AUTH_USERID` property to initialize the provider. *user_id* can't be a Microsoft Windows login name.

#### '*password*'

A string constant that is the user password to be passed to the data provider. *password* is passed in as the `DBPROP_AUTH_PASSWORD` property when initializing the provider. *password* can't be a Microsoft Windows password. For example:

```sql
SELECT a.* FROM OPENROWSET(
    'Microsoft.Jet.OLEDB.4.0',
    'C:\SAMPLES\Northwind.mdb';
    '<user name>';
    '<password>',
    Customers
) AS a;
```

#### '*provider_string*'

A provider-specific connection string that is passed in as the `DBPROP_INIT_PROVIDERSTRING` property to initialize the OLE DB provider. *provider_string* typically encapsulates all the connection information required to initialize the provider. 

For a list of keywords that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider recognizes, see [Initialization and Authorization Properties (Native Client OLE DB Provider)](../../relational-databases/native-client-ole-db-data-source-objects/initialization-and-authorization-properties.md). [!INCLUDE [snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

```sql
SELECT d.* FROM OPENROWSET(
    'MSOLEDBSQL',
    'Server=Seattle1;Trusted_Connection=yes;',
    Department
) AS d;
```

<a id="table_or_view"></a>

#### [ catalog. ] [ schema. ] object

Remote table or view containing the data that `OPENROWSET` should read. It can be three-part-name object with the following components:

- *catalog* (optional) - the name of the catalog or database in which the specified object resides.
- *schema* (optional) - the name of the schema or object owner for the specified object.
- *object* - the object name that uniquely identifies the object to work with.

```sql
SELECT d.* FROM OPENROWSET(
    'MSOLEDBSQL',
    'Server=Seattle1;Trusted_Connection=yes;',
    AdventureWorks2022.HumanResources.Department
) AS d;
```

#### '*query*'

A string constant sent to and executed by the provider. The local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't process this query, but processes query results returned by the provider, a pass-through query. Pass-through queries are useful when used on providers that don't make available their tabular data through table names, but only through a command language. Pass-through queries are supported on the remote server, as long as the query provider supports the OLE DB Command object and its mandatory interfaces. 

For more information, see [SQL Server Native Client (OLE DB) Interfaces](../../relational-databases/native-client-ole-db-interfaces/sql-server-native-client-ole-db-interfaces.md).

```sql
SELECT a.*
FROM OPENROWSET(
    'MSOLEDBSQL',
    'Server=Seattle1;Trusted_Connection=yes;',
    'SELECT TOP 10 GroupName, Name FROM AdventureWorks2022.HumanResources.Department'
) AS a;
```

## Remarks

`OPENROWSET` can be used to access remote data from OLE DB data sources only when the **DisallowAdhocAccess** registry option is explicitly set to 0 for the specified provider, and the Ad Hoc Distributed Queries advanced configuration option is enabled. When these options aren't set, the default behavior doesn't allow for ad hoc access.

When you access remote OLE DB data sources, the login identity of trusted connections isn't automatically delegated from the server on which the client is connected to the server that is being queried. Authentication delegation must be configured.

Catalog and schema names are required if the data provider supports multiple catalogs and schemas in the specified data source. Values for `catalog` and `schema` can be omitted when the data provider doesn't support them. If the provider supports only schema names, a two-part name of the form `schema.object` must be specified. If the provider supports only catalog names, a three-part name of the form `catalog.schema.object` must be specified. For more information, see [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

Three-part names are required for pass-through queries that use the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider. 

`OPENROWSET` doesn't accept variables for its arguments.

Any call to `OPENDATASOURCE`, `OPENQUERY`, or `OPENROWSET` in the `FROM` clause is evaluated separately and independently from any call to these functions used as the target of the update, even if identical arguments are supplied to the two calls. In particular, filter or join conditions applied on the result of one of those calls has no effect on the results of the other.

## Permissions

`OPENROWSET` permissions are determined by the permissions of the user name that is being passed to the data provider.

## Limitations

Not supported with [Microsoft Access Database Engine driver](https://support.microsoft.com/office/download-and-install-microsoft-365-access-runtime-185c5a32-8ba9-491e-ac76-91cbe3ea09c9). 

## Examples

This section provides general examples to demonstrate how to use OPENROWSET.

> [!NOTE]
> For examples that show using `INSERT...SELECT * FROM OPENROWSET(BULK...)`, see [OPENROWSET BULK (Transact-SQL)](openrowset-bulk-transact-sql.md#examples).

[!INCLUDE [snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

### A. Use OPENROWSET with SELECT and the SQL Server Native Client OLE DB Provider

The following example uses the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider to access the `HumanResources.Department` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on the remote server `Seattle1`. (Use `MSOLEDBSQL` for the modern Microsoft SQL Server OLE DB Data Provider that replaced `SQLNCLI`.) A `SELECT` statement is used to define the row set returned. The provider string contains the `Server` and `Trusted_Connection` keywords. These keywords are recognized by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider.

```sql
SELECT a.*
FROM OPENROWSET(
    'MSOLEDBSQL', 'Server=Seattle1;Trusted_Connection=yes;',
    'SELECT GroupName, Name, DepartmentID
         FROM AdventureWorks2022.HumanResources.Department
         ORDER BY GroupName, Name'
) AS a;
```

### B. Use the Microsoft OLE DB Provider for Jet

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

### C. Use OPENROWSET and another table in an INNER JOIN

The following example selects all data from the `Customers` table from the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] `Northwind` database and from the `Orders` table from the Microsoft Access `Northwind` database stored on the same computer.

> [!NOTE]  
> This example assumes that Microsoft Access is installed. To run this example, you must install the `Northwind` database.

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

## Related content

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [OPENDATASOURCE (Transact-SQL)](opendatasource-transact-sql.md)
- [OPENQUERY (Transact-SQL)](openquery-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sp_serveroption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)
- [UPDATE (Transact-SQL)](../queries/update-transact-sql.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
