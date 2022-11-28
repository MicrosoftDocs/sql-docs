---
description: "Microsoft SQL Server Distributed Queries: OLE DB Connectivity"
title: "Create linked server provider"
ms.date: "07/01/2019"
ms.service: sql
ms.subservice: 
ms.reviewer: "MikeRayMSFT"
ms.topic: conceptual
author: rwestMSFT
ms.author: randolphwest
ms.custom: seo-dt-2019
---

# Microsoft SQL Server Distributed Queries: OLE DB Connectivity

This article describes how the Microsoft SQL Server query processor interacts with an OLE DB provider to enable distributed and heterogeneous queries. It is intended primarily for OLE DB provider developers, and assumes a solid understanding of the OLE DB specification. The emphasis is on the OLE DB interface between the SQL Server query processor and the OLE DB provider, and not on the distributed query functionality itself. For a full description of distributed querying functionality, see [Linked servers](../../relational-databases/linked-servers/linked-servers-database-engine.md).

## Overview and Terminology

 In Microsoft SQL Server, distributed queries enable SQL Server users to access data outside a SQL Server-based server, either within other servers running SQL Server or other data sources that expose an OLE DB interface. OLE DB provides a way to uniformly access tabular data from heterogeneous data sources.

A distributed query, for the purpose of this article, is any `SELECT`, `INSERT`, `UPDATE`, or `DELETE` statement that references tables and rowsets from one or more external OLE DB data sources.

A remote table is a table that is stored in an OLE DB data source and is external to the server running SQL Server executing the query. A distributed query accesses one or more remote tables.

### OLE DB Provider Categories

The following is a categorization of OLE DB providers based on their capabilities from a SQL Server distributed querying standpoint. As defined, these are not mutually exclusive; a given provider may belong to more than one of the following categories:

- SQL Command Providers

- Index Providers

- Simple Table Providers

- Non-SQL Command Providers

#### SQL Command Providers

Providers that support the `Command` object with an SQL standard dialect recognized by SQL Server belong to this category. The specific requirements for a given OLE DB provider to be treated as a SQL Command Provider by SQL Server are:

- The provider must support the `Command` object and all of its mandatory OLE DB interfaces: `ICommand`, `ICommandText`, `IColumnsInfo`, `ICommandProperties`, and `IAccessor`.

- The SQL dialect supported by the provider must be at least SQL Subminimum. The dialect must be reported by the provider through the `DBPROP_SQLSUPPORT` property.

Examples of SQL Command Providers are the Microsoft OLE DB Provider for SQL Server and the Microsoft OLE DB Provider for ODBC.

#### Index Providers

Index providers are those that support and expose indexes according to OLE DB and allow index-based lookup of base tables. The specific requirements for a given OLE DB provider to be treated as an Index provider by SQL Server are:

- The provider must support the `IDBSchemaRowset` interface with the TABLES, COLUMNS and INDEXES schema rowsets.

- The provider must support opening a rowset on an index through `IOpenRowset` by specifying the index name and the corresponding base table name.

- The `Index` object must support all its mandatory interfaces: `IRowset`, `IRowsetIndex`, `IAccessor`, `IColumnsInfo`, `IRowsetInfo`, and `IConvertTypes`.

- Rowsets opened against the indexed base table (through `IOpenRowset`) must support the `IRowsetLocate` interface for positioning on a row based off a bookmark.

If the OLE DB provider meets the above requirements, users can set the `Index As Access Path` provider option to enable SQL Server to use the provider's indexes to evaluate queries. By default, SQL Server does not attempt to use the provider's indexes unless this option is set.

>[!NOTE]
>SQL Server supports various options that influence how SQL Server accesses an OLE DB provider. The `Linked Server Properties` dialog box in SQL Server Enterprise Manager can be used to set these options.

#### Simple Table Providers

These are providers that expose the opening of a rowset against a base table through the `IOpenRowset` interface. Such providers are neither SQL Command Providers nor Index providers; rather, they are the simplest class of providers that SQL Server distributed queries can work with.

Against such providers, SQL Server can only perform table scans during distributed query evaluation.

#### Non-SQL Command Providers

Providers that support the `Command` object and its mandatory interfaces, but do not support an SQL standard dialect recognized by SQL Server, fall into this category.

Two examples of Non-SQL Command Providers are the Microsoft OLE DB Provider for Indexing Service and the [Microsoft OLE DB Provider for Microsoft Active Directory Service](../../ado/guide/appendixes/microsoft-ole-db-provider-for-microsoft-active-directory-service.md).

### Transact-SQL Subset

Each of the following classes of Transact-SQL statements is supported for distributed queries if the provider supports the required OLE DB interfaces.

- All `SELECT` statements are allowed except for `SELECT` INTO statements with a remote table as the destination table.

- `INSERT` statements are allowed against remote tables if the provider supports the required interfaces for insert. For more information about OLE DB requirements for INSERT, see \"INSERT Statement\" later in this article.

- `UPDATE` and DELETE statements are allowed against remote tables if the provider satisfies the OLE DB interface requirements on the specified table. For the OLE DB interface requirements and conditions under which a remote table can be updated or deleted, see \"UPDATE and DELETE Statements\" later in this article.

### Cursor Support

Both snapshot and keyset cursors are supported against distributed queries if the provider supports the necessary OLE DB functionality. Dynamic cursors are not supported against distributed queries. A user request for a dynamic cursor against a distributed query is downgraded to a keyset cursor.

Snapshot cursors are populated at cursor open time and the result set remains unchanged; updates, inserts, and deletes to the underlying tables are not reflected in the cursor.

Keyset cursors are populated at cursor open time and the result set remains unchanged throughout the lifetime of the cursor. However, updates and deletes to underlying tables are visible in the cursor as the rows are visited. Inserts to underlying tables that may affect cursor membership are not visible.

A remote table can be updated or deleted through a cursor that is defined on a distributed query and references the remote table if the provider meets the conditions for updates and deletes on the remote table, for example, table `UPDATE` \| DELETE `<remote-table>` `WHERE` CURRENT OF `<cursor-name>`. For more information, see \"UPDATE and DELETE Statements\" later in this article.

#### Keyset Cursor Support Requirements

A keyset cursor is supported on a distributed query if all the Transact-SQL syntax requirements are met and either of these exist:

- The OLE DB provider supports reusable bookmarks on all the remote tables in the query. Reusable bookmarks can be consumed from a rowset on a given table and used on a different rowset of the same table. The support for reusable bookmarks is indicated through the TABLES_INFO schema rowset of `IDBSchemaRowset` by setting the BOOKMARK_DURABILITY column to BMK_DURABILITY_INTRANSACTION or a higher durability.

- All the remote tables expose a unique key through the INDEXES rowset of `IDBSchemaRowset` interface. There should be an index entry with the UNIQUE column set to VARIANT_TRUE.

Keyset cursors are not supported against distributed queries that involve the *OpenQuery* function.

#### Updatable Keyset Cursor Requirements

A remote table can be updated or deleted through a keyset cursor that is defined on a distributed query, for example, `UPDATE` \| DELETE `<remote-table>` `WHERE` CURRENT OF `<cursor-name>`. The following are the conditions under which updatable cursors against distributed queries are allowed:

- Updatable cursors are allowed if the provider also meets the conditions for updates and deletes on the remote table. For more information, see \"UPDATE and DELETE Statements\" later in this article.

- All the updatable keyset cursor operations must be in a user-defined transaction with read-repeatable isolation level or a higher isolation level. Furthermore, the provider must support distributed transactions with the `ITransactionJoin` interface.

## OLE DB Provider Interaction Phases

 Six operations are common to all the distributed query execution scenarios:

- Connection establishment and property retrieval operations indicate how SQL Server connects to an OLE DB provider and what provider properties are used.

- Table name resolution and meta data retrieval operations indicate how SQL Server resolves the remote table name (which is specified in one of two ways: either a linked server based name or an ad hoc name) into the appropriate data object in the provider. This also includes the table meta data that SQL Server retrieves from the provider in order to compile and optimize a distributed query.

- Transaction management operations specify all transaction-related interaction with the OLE DB provider.

- Data type handling operations indicate how OLE DB data types are handled by SQL Server when it consumes data from or exports data to an OLE DB provider while processing a distributed query.

- Error handling operations indicate how SQL Server uses extended error information from the provider.

- Security operations specify how SQL Server security interacts with the provider's security.

### Connection Establishment and Property Retrieval

SQL Server supports two remote data object naming conventions: linked server-based four-part names and ad hoc names using the `OPENROWSET` function.

#### Linked server-based names

A linked server serves as an abstraction to an OLE DB data source. A linked server-based name is a four-part name of the form `<linked-server>.<catalog>`. `<schema>.<object>`, where `<linked-server>` is the name of the linked server. SQL Server interprets `<linked-server>` to derive the OLE DB provider and the connection attributes that identify the data source to the provider. The other three name parts are interpreted by the OLE DB data source to identify the specific remote table. :::

#### Ad hoc names

An ad hoc name is a name based on the `OPENROWSET` or `OPENDATASOURCE` function. It includes all the connection information (that is, the OLE DB provider to use, the attributes needed to identify the data source, the user ID and password) every time the remote table is referenced in a distributed query.

Using ad hoc names is not allowed by default except for members of the sysadmin role. In order to use ad hoc names against an OLE DB provider, the provider option `DisallowAdhocAccess` should be set to `0`.

If a linked server name is used, SQL Server extracts from the linked server definition the OLE DB provider name and the initialization properties for the provider. If an ad hoc name is used, SQL Server extracts the same information from the arguments of the `OPENROWSET` function.

For detailed instructions about setting up a linked server using a four-part name and ad hoc name-based syntax, see [Create Linked Servers](create-linked-servers-sql-server-database-engine.md).

### Connecting to an OLE DB Provider

These are the high-level steps that SQL Server performs when it connects to an OLE DB provider:

1. SQL Server creates a data source object.

   SQL Server uses the provider's `ProgID` to instantiate its data source object (DSO). The ProgID is specified as the `provider_name` parameter of a linked server configuration or as the first argument of the `OPENROWSET` function in the case of an ad hoc name.

   SQL Server instantiates the provider's DSO through the OLE DB service component interface `IDataInitialize`. This allows the Service Component Manager to aggregate its services, such as scrolling and update support, above the native functionality of the provider. Further, instantiating the provider through `IDataInitialize` allows the OLE DB service component to pool connections to the provider, thereby reducing some of the connection and initialization overhead.

   A given provider can be configured to be instantiated either in the same process as SQL Server or in its own process. Instantiating in a separate process protects the SQL Server process from failures in the provider. At the same time, there is a performance overhead associated with marshalling OLE DB calls out-of-process from SQL Server. A provider can be configured to be instantiated in-process or out-of-process by setting the `Allow In Process` provider option. For more information, see [setting provider options](../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md).

   To learn more about the OLE DB service components and session pooling, refer to the OLE DB documentation for provider requirements.

2. The data source is initialized.

   After the DSO has been created, the `IDBProperties` interface sets the DBPROP_INIT_TIMEOUT initialization property if the server configuration option `remote login timeout` is greater than 0; this is a required property.

   These properties are set if they are specified or implied in either the linked server definition or in the second argument of the `OPENROWSET` function:

   - `DBPROP_INIT_PROVIDERSTRING`

   - `DBPROP_INIT_DATASOURCE`

   - `DBPROP_INIT_LOCATION`

   - `DBPROP_INIT_CATALOG`

   - `DBPROP_AUTH_USERID`

   - `DBPROP_AUTH_PASSWORD`

   After these properties are set, `IDBInitialize::Initialize` is called to initialize the DSO with the specified properties.

3. SQL Server gathers provider-specific information.

   SQL Server gathers several provider properties to be used in distributed query evaluation; these properties are retrieved by calling `IDBProperties::GetProperties`. All these properties are optional; however, supporting all relevant properties enables SQL Server to take full advantage of the provider's capabilities. For instance, `DBPROP_SQLSUPPORT` is needed to determine whether SQL Server can send queries to the provider. If this property is not supported, SQL Server will not use the remote provider as a SQL Command Provider even if it is one. In the following table, the Default value column indicates what value SQL Server assumes if the provider does not support the property.

Property| Default value| Use |
|:----|:----|:----|
|`DBPROP_DBMSNAME`|None|Used for error messages.|
|`DBPROP_DBMSVER` |None|Used for error messages.|
|`DBPROP_PROVIDERNAME`|None|Used for error messages.|
|`DBPROP_PROVIDEROLEDBVER1`|1.5|Used to determine availability of 2.0 features.
|`DBPROP_CONCATNULLBEHAVIOR`|None|Used to determine whether the `NULL` concatenation behavior of the provider is the same as that of SQL Server.|
|`DBPROP_NULLCOLLATION`|None|Allows sorting/index-use only if `NULLCOLLATION` matches the SQL Server instance null collation behavior.|
|`DBPROP_OLEOBJECTS`|None|Determines whether the provider supports structured storage interfaces for large data object columns.|
|`DBPROP_STRUCTUREDSTORAGE`|None|Determines which of the structured storage interfaces are supported for large object types (among `ILockBytes`, `Istream`, and `ISequentialStream`).|
|`DBPROP_MULTIPLESTORAGEOBJECTS`|False|Determines whether more than one large object column can be open at the same time.|
|`DBPROP_SQLSUPPORT`|None|Determines whether SQL queries can be sent to the provider.|
|`DBPROP_CATALOGLOCATION`|`DBPROPVAL_CL_START`|Used to construct multipart table names.
|`SQLPROP_DYNAMICSQL`|False|SQL Server-specific property: if it returns `VARIANT_TRUE`, it indicates that `?` parameter markers are supported for parameterized query execution.
|`SQLPROP_NESTEDQUERIES`|False|SQL Server-specific property: if it returns `VARIANT_TRUE`, it indicates that the provider supports nested `SELECT` statements in the `FROM` clause.
|`SQLPROP_GROUPBY`|False|SQL Server-specific property: if it returns `VARIANT_TRUE`, it indicates that the provider supports GROUP BY clause in the `SELECT` statement as specified by the SQL-92 standard.
|`SQLPROP_DATELITERALS `|False|SQL Server-specific property: if it returns `VARIANT_TRUE`, it indicates that the provider supports datetime literals as per SQL Server Transact-SQL syntax.
|`SQLPROP_ANSILIKE `|False|SQL Server-specific property: This property is of interest to a provider that supports the SQL-Minimum level and it supports the `LIKE` operator as per SQL-92 entry level (\'%\' and \'_\' as wildcard characters).
|`SQLPROP_SUBQUERIES `|False|SQL Server property: This property is of interest in a provider that supports the SQL-Minimum level. This property indicates that the provider supports subqueries as specified by SQL-92 entry level. This includes subqueries in the `SELECT` list and in the `WHERE` clause with support for correlated subqueries, `IN`, `EXISTS`, `ALL` and `ANY` operators.
|`SQLPROP_INNERJOIN`|False|SQL Server-specific property: This property is of interest to providers that support the SQL-Minimum level. This property indicates support for joins using multiple tables in the `FROM` clause. ------ ---

The following three literals are retrieved from `IDBInfo::GetLiteralInfo`: `DBLITERAL_CATALOG_SEPARATOR`, `DBLITERAL_SCHEMA_SEPARATOR` (to construct a full object name given its catalog, schema, and object name parts), and `DBLITERAL_QUOTE` (to quote identifier names in an SQL query sent to the provider).

If the provider does not support the separator literals, SQL Server uses a period (.) as the default separator character. If the provider supports only the catalog separator character but not the schema separator character, SQL Server uses the catalog separator character as the schema separator character also. If the provider does not support `DBLITERAL_QUOTE`, SQL Server uses a single quotation mark (`'`) as the quoting character.

>[!NOTE]
>If the provider's name separator literals do not match these default values, the provider must expose them through `IDBInfo` for SQL Server to access its tables through four-part names. If these literals are not exposed, only pass-through queries can be used against such a provider.

For information about exposing the `SQLPROP_DYNAMICSQL` and `SQLPROP_NESTEDQUERIES` properties, see [SQL Server Specific Properties](#appendixc).

### Table Name Resolution and Meta Data Retrieval

SQL Server resolves a given remote table name in a distributed query to a specific table or view in an OLE DB data source. Both the linked server-based and ad hoc naming schemes result in a three-part name to be interpreted by the provider. In the case of the linked server-based name, the last three parts of the four-part name form the catalog, schema, and object names. In the case of the ad hoc name, the third argument of the `OPENROWSET` function specifies a three-part name that describes the catalog, schema, and object names. One or both of the catalog and schema names can be empty. (A four-part name with an empty catalog name and schema name would look like `<server-name>...<object-name>`.) In such a case, SQL Server uses `NULL` as the corresponding value to look for in the schema rowset tables.

The name resolution rules and the meta data retrieval steps that SQL Server employs depend on whether the provider supports the `IDBSchemaRowset` interface on the `Session` object.

If `IDBSchemaRowset` is supported, `TABLES`, `COLUMNS`, `INDEXES`, and `TABLES_INFO` schema rowsets are used from the `IDBSchemaRowset` interface. (The `TABLES_INFO` schema rowset is defined in OLE DB 2.0.) SQL Server restricts the schema rowsets returned by the `IDBSchemaRowset` interface to look for schema rows that match the specified remote table name parts. The following are the rules related to the restrictions supported by the provider on the schema rowsets and how SQL Server uses them to retrieve a remote table's meta data:

- Restrictions on `TABLE_NAME` and `COLUMN_NAME` columns are always required.

- If the provider supports a restriction on `TABLE_CATALOG` (or `TABLE_SCHEMA`), SQL Server uses that restriction on `TABLE_CATALOG` (or `TABLE_SCHEMA`). If catalog (or schema) name is not specified in the remote table name, a `NULL` value is used as the corresponding restriction value. If a catalog (or schema) name is specified, the provider must support the corresponding restriction on `TABLE_CATALOG` (or `TABLE_SCHEMA`).

- The provider must either support restriction on the `TABLE_SCHEMA` column in both `TABLES` and `COLUMNS` or support them on neither. The provider must either support catalog name restriction on both `TABLES` and `COLUMNS` rowsets or support them on neither.

- If any restrictions are supported on INDEXES, the provider must support schema restriction on both `TABLES` and INDE`XES or support them on neither. The provider must either support catalog name restriction on both `TABLES` and `INDEXES` rowsets or support them on neither.

From the `TABLES` schema rowset, SQL Server retrieves the `TABLE_CATALOG`, `TABLE_SCHEMA`, `TABLE_NAME`, `TABLE_TYPE`, `TABLE_GUID` columns by setting restrictions according to the above rules.

From the `COLUMNS` schema rowset, SQL Server retrieves the `TABLE_CATALOG`, `TABLE_SCHEMA`, `TABLE_NAME`, `COLUMN_NAME`, `COLUMN_GUID`, `ORDINAL_POSITION`, `COLUMN_FLAGS`, `IS_NULLABLE`, `DATA_TYPE`, `TYPE_GUID`, `CHARACTER_MAXIMUM_LENGTH`, `NUMERIC_PRECISION`, and `NUMERIC_SCALE` columns. `COLUMN_NAME`, `DATA_TYPE` and `ORDINAL_POSITION` must return valid non-null values. If `DATA_TYPE` is `DBTYPE_NUMERIC` or `DBTYPE_DECIMAL`, the corresponding `NUMERIC_PRECISION` and `NUMERIC_SCALE` must be valid non-null values.

From the optional `INDEXES` schema rowset, SQL Server looks for indexes on the specified remote table by setting restrictions as per the previous rules. From the matching index entries thus found, SQL Server retrieves the `TABLE_CATALOG`, `TABLE_SCHEMA`, `TABLE_NAME`, `INDEX_CATALOG`, `INDEX_SCHEMA`, `INDEX_NAME`, `PRIMARY_KEY`, `UNIQUE`, `CLUSTERED`, `FILL_FACTOR`, `ORDINAL_POSITION`, `COLUMN_NAME`, `COLLATION`, `CARDINALITY`, and `PAGES` columns.

From the optional `TABLES_INFO` rowset, SQL Server looks for additional information on the specified remote table, such as bookmark support, the type and the length of the bookmark. All columns except the `DESCRIPTION` column of the `TABLES_INFO` rowset are used. The information in `TABLES_INFO` rowset is used as follows:

- The `BOOKMARK_DURABILITY` column is used to implement more efficient keyset cursors. If this column has a value of `BMK_DURABILITY_INTRANSACTION` or a higher durability value, SQL Server uses bookmark-based retrieval and updates of remote table rows for implementing a keyset cursor.

- The `BOOKMARK_TYPE`, `BOOKMARK_DATA TYPE`, and `BOOKMARK_MAXIMUM_LENGTH` columns are used to determine bookmark meta data at query compilation time. If these columns are not supported, SQL Server opens the base table rowset through `IOpenRowset` during compilation to get the bookmark information.

If `IDBSchemaRowset` is not supported and the remote table name includes a catalog or schema name, SQL Server requires `IDBSchemaRowset` and returns an error. However, if neither the catalog nor the schema names are supplied, SQL Server opens the rowset that corresponds to the remote table and retrieves the column meta data from the mandatory `IColumnsInfo` interface of the rowset object.

SQL Server opens the rowset corresponding to the table by calling `IOpenRowset::OpenRowset`. The table name supplied to `OPENROWSET` is constructed from the catalog, schema, and object name parts.

- Each of the name parts (`catalog`, `schema`, `object name`) are quoted with the provider's quoting character (`DBLITERAL_QUOTE`) and then concatenated with the `DBLITERAL_CATALOG_SEPARATOR` character and the `DBLITERAL_SCHEMA_SEPARATOR` character embedded between them. The name construction follows the OLE DB rules in `IOpenRowset`.

- The column meta data for the table is retrieved through `IColumnsInfo::GetColumnInfo` after the rowset object is opened.

If `IDBSchemaRowset` is not supported with TABLES, COLUMNS, and TABLES_INFO rowsets, SQL Server opens the rowset against the base table twice: once during query compilation to retrieve meta data, and once during query execution. Providers that incur side effects from opening the rowset (for example, run code that alters the state of a real-time device, send e-mail, run arbitrary user-supplied code) must be aware of this behavior.

### Statistics retrieval

If the provider supports distribution statistics on the base tables, then SQL Server will use these statistics. There are two kinds of statistics that are of interest to the SQL Server query processor:

- **Column (or tuple) cardinalities**. This is the number of unique values that are in a column (or a combination of columns) of a table. This can be used to estimate the selectivity of predicates against the column(s). A provider supporting distribution statistics should support at least one type of cardinality.

- **Histograms**. If the distribution of values is not uniform, then the no. of unique values is not sufficient to estimate accurately the selectivity of predicates. In this case a histogram can be provided which gives a more fine grained information about the distribution of column values in a table.

The availability of statistics enables the SQL Server query optimizer to better estimate the cardinalities of intermediate operations in a query which allows it to generate better execution plans for them.

The OLE DB provider should support distribution statistics as follows:

- **Mandatory**. Support the properties (1) `DBPROP_TABLESTATISTICS` which indicates whether column or tuple cardinalities are supported and whether histograms are supported and (2) `DBPROP_OPENROWSETSUPPORT` which indicates using the `DBPROPVAL_ORS_HISTOGRAM` bit, whether histograms are supported.

- **Mandatory**. The `TABLE_STATISTICS` schema rowset. The `TABLE_STATISTICS` schema rowset lists the statistics available in a given database. It also includes the column and tuple cardinalities in the schema rowset itself and indicates whether histograms are supported on the specific columns. For SQL Server to use statistics, the columns `TABLE_NAME`, `STATISTICS_NAME`, `STATISTICS_TYPE`, `COLUMN_NAME`, and `ORDINAL_POSITION` are mandatory in this schema rowset. At least one of `COLUMN_CARDINALITY` or `TUPLE_CARDINALITY` are mandatory. If histograms are supported then `NO_OF_RANGES` is also mandatory.

- **Optional**. Optionally, if the provider supports histograms, it should support an enhancement to the `IOpenRowset::OpenRowset` method that allows opening a histogram rowset by specifying the `DBID` of the corresponding statistic.

For complete information on the statistics interfaces, refer to the OLE DB 2.6 specification.

### Constraints

The SQL Server query optimizer also uses the `CHECK` constraints defined on the base tables in a remote data source, if the OLE DB provider supports the OLE DB 2.6 schema rowset `CHECK_CONSTRAINTS_BY_TABLE`. The `CHECK_CLAUSE` column of the schema rowset should return the `CHECK` clause predicate in SQL-92 compliant syntax. The query optimizer uses constraint information in order to eliminate or simplify predicates that are known to be always false or always true because of the presence of a check constraint on the table.

### Transaction Management

SQL Server supports transaction-based access to distributed data by using the provider's `ITransactionLocal` (for local transaction) and `ITransactionJoin` (for distributed transactions) OLE DB interfaces. By starting a local transaction against the provider, SQL Server guarantees atomic write operations. By using distributed transactions, SQL Server ensures that a transaction that involves multiple nodes has the same result (either commit or abort) in all the nodes. If the provider does not support the requisite OLE DB transaction-related interfaces, update operations against that provider are not allowed depending on the local transaction context.

The following table describes what happens when the user executes a distributed query given the capabilities of the provider and a local transaction context. A read operation against a provider refers to either a `SELECT` statement or when the remote table is read into the input side of a `SELECT INTO`, `INSERT`, `UPDATE`, or `DELETE` statement. A write operation against a provider refers to an `INSERT`, `UPDATE`, or `DELETE` statement with a remote table as the destination table.

Results of a distributed query based on provider capabilities and transaction context:

|Distributed query occurs|Provider does not support `ITransactionLocal`|Provider supports `ITransactionLocal` but not `ITransactionJoin`|Provider supports both `ITransactionLocal` and `ITransactionJoin`|
|:-----|:-----|:-----|:-----|
| In a transaction by itself (no user transaction).|By default, only read operations are allowed. When the provider level option `Nontransacted Updates` is enabled, write operations are allowed. (When this option is enabled, SQL Server cannot guarantee atomicity and consistency on the provider's data. This can cause partial effects of a write operation to be reflected in the remote data source without the ability to undo them.)| All statements are allowed against remote data. Keyset cursors are read-only. The local transaction is started on the provider with the current SQL Server session's isolation level and is committed at the end of successful statement evaluation. (The default isolation level for a SQL Server session is `READ COMMITTED` unless it is modified with the `SET TRANSACTION ISOLATION LEVEL` statement. The provider must support the requested isolation level.) | All statements are allowed. Keyset cursors are read-only. The local transaction is started on the provider with the current SQL Server session's isolation level and is committed at the end of a successful statement evaluation. |
| In a user transaction (that is, between `BEGIN TRAN` or `BEGIN DISTRIBUTED TRAN` and `COMMIT`). | If the isolation level of the transaction is `READ COMMITTED` (the default) or below, read operations are allowed. If the isolation level is higher, no distributed queries are allowed. | Only read operations are allowed. New distributed transactions are started on the provider with the current SQL Server session's isolation level. |All statements are allowed. New distributed transaction is started on the provider with the current SQL Server session's isolation level and committed when the user transaction commits. For data modification statements, by default SQL Server starts a nested transaction under the distributed transaction, so that the data modification statement can be stopped under certain error conditions without stopping the surrounding transaction. If the `XACT_ABORT SET` option is on, SQL Server does not require nested transaction support and stops the surrounding transaction in the case of errors during the data modification statement. |

### Data Type Handling in Distributed Queries

OLE DB providers expose their data in terms of the OLE DB-defined data types (indicated by `DBTYPE` in OLE DB). SQL Server processes external data inside the server as native SQL Server types; this results in a mapping of OLE DB data types to SQL Server native types and vice versa as data is consumed by SQL Server or exported by SQL Server, respectively. This mapping is done implicitly, unless otherwise noted.

Data types in distributed queries are handled by using one of two mapping methods:

- Consumption-side mapping maps types from OLE DB data types to SQL Server native data types on the consuming side, when remote tables appear in `SELECT` statements and on the input side of INSERT, UPDATE, and DELETE statements.

- Export-side mapping maps types from SQL Server data types to OLE DB data types on the exporting side, when a remote table appears as the destination table of an `INSERT` or `UPDATE` statement.

SQL Server and OLE-DB data type mapping table.

| OLE DB type | `DBCOLUMNFLAG` | SQL Server data type |
|-----|-----|-----|
|`DBTYPE_I1`*| |`numeric(3, 0)`|
|`DBTYPE_I2`| |`smallint`|
|`DBTYPE_I4`| |`int`|
|`DBTYPE_I8`| |`numeric(19,0)`|
|`DBTYPE_UI1`| |`tinyint`|
|`DBTYPE_UI2`*| |`numeric(5,0)`|
|`DBTYPE_UI4`*| |`numeric(10,0)`|
|`DBTYPE_UI8`*| |`numeric(20,0)`|
|`DBTYPE_R4`| |`float`|
|`DBTYPE_R8`| |`real`|
|`DBTYPE_NUMERIC`| |`numeric`|
|`DBTYPE_DECIMAL`| |`decimal`|
|`DBTYPE_CY`| |`money`|
|`DBTYPE_BSTR`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true`<br>or<br> Max Length > 4000 characters|ntext|
|`DBTYPE_BSTR`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true`|nchar|
|`DBTYPE_BSTR`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=false`|nvarchar|
|`DBTYPE_IDISPATCH`| |Error|
|`DBTYPE_ERROR`| |Error|
|`DBTYPE_BOOL`| |`bit`|
|`DBTYPE_VARIANT`*| |nvarchar|
|`DBTYPE_IUNKNOWN`| |Error|
|`DBTYPE_GUID`| |`uniqueidentifier`|
|`DBTYPE_BYTES`|`DBCOLUMNFLAGS_ISLONG=true` <br>or<br> Max Length > 8000|`image`|
|`DBTYPE_BYTES`|`DBCOLUMNFLAGS_ISROWVER=true`, `DBCOLUMNFLAGS_ISFIXEDLENGTH=true`, Column size = 8 <br>or<br> Max Length not reported. | `timestamp` |
|`DBTYPE_BYTES`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true` | `binary` |
|`DBTYPE_BYTES`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true` | `varbinary`|
|`DBTYPE_STR`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true` | `char`|
|`DBTYPE_STR`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true` | `varchar` |
|`DBTYPE_STR`| `DBCOLUMNFLAGS_ISLONG=true` <br>or<br> Max Length > 8000 characters <br>or<br>   Max Length not reported. | `text`|
|`DBTYPE_WSTR`| `DBCOLUMNFLAGS_ISFIXEDLENGTH=true` |`nchar`|
|`DBTYPE_WSTR` | `DBCOLUMNFLAGS_ISFIXEDLENGTH=false`|`nvarchar`|
|`DBTYPE_WSTR`| `DBCOLUMNFLAGS_ISLONG=true` <br>or<br> Max Length >4000 characters <br>or<br>   Max Length not reported. | `ntext`|
|`DBTYPE_UDT`| |Error|
|`DBTYPE_DATE`* | | `datetime` |
|`DBTYPE_DBDATE` | | `datetime` (explicit conversion required)|
|`DBTYPE_DBTIME`| | `datetime` (explicit conversion required)|
|`DBTYPE_DBTIMESTAMP`* | | `datetime`|
|`DBTYPE_ARRAY` | |Error|
|`DBTYPE_BYREF` | | Ignored |
|`DBTYPE_VECTOR` | |Error|
|`DBTYPE_RESERVED`| |Error|

\* Indicate some form of translation to the SQL Server type's representation, as there is no exact equivalent data type in SQL Server. Such conversions could result in loss of precision, overflow, or underflow. The default implicit mappings can be changed in the future if the corresponding data types are supported by future versions of SQL Server.

>[!NOTE]
>`numeric(p,s)` indicates SQL Server data type `numeric` with precision `p` and scale `s`. The maximum allowed precision for `DBTYPE_NUMERIC` and `DBTYPE_DECIMAL` is 38. The provider must support binding to the `DBTYPE_BSTR` column as `DBTYPE_WSTR` while creating an accessor. `DBTYPE_VARIANT`columns are consumed as Unicode character strings `nvarchar`. This requires support for conversion from `DBTYPE_VARIANT` to `DBTYPE_WSTR` from the provider. The provider is expected to implement this conversion as defined in OLE DB. For more information, see [Data Types of the OLE DB Specification](#appendixa).

#### Interpreting Data Type Mapping

The mapping to a SQL Server type is determined by the OLE DB data type and the DBCOLUMNFLAGS values that describe the column or scalar value. In the case of the `COLUMNS` schema rowset, the `DATA_TYPE` and `COLUMN_FLAGS` columns represent these values. In the case of the `IColumnsInfo::GetColumnInfo` interface, the `wType` and `dwFlags` members of the `DBCOLUMNINFO` structure represent this information.

To use consumption-side mapping for a given column with a specific `DBTYPE` and `DBCOLUMNFLAG` value, look for the corresponding SQL Server type in the table. The type rules for columns from remote tables in expressions can be described by the following simple rule:

>A given remote column value is legal in a Transact-SQL expression if the corresponding mapped SQL Server type in the table is legal in the same context.

The table and the rule define:

- Comparisons and expressions.

In general, `X <op> <remote-column>` is a valid expression if `<op>` is a valid operator on the data type of `X` and the data type that `<remote-column>` maps to.

- Explicit conversions.

`Convert(X, <remote-column>)` is allowed if the `DBTYPE` of `<remote-column>` maps to native data type `Y` (as per table above) and explicit conversion from `Y` to `X` is allowed.

If users want remote data to be converted to a nondefault native data type, they must use an explicit conversion.

To use export-side mapping in the case of `UPDATE` and `INSERT` statements against remote tables, map native SQL Server data types to OLE DB data types using the same table. A mapping from a SQL Server type `S1` to a given OLE DB type `T` is allowed if either of these exist:

- The corresponding mapping can be found in the mapping table directly.

- There is an allowed implicit conversion of `S1` to another SQL Server type `S2` such that `S2` maps to type `T` in the mapping table.

#### Large Object (LOB) Handling

As indicated in the mapping table, if columns of the type `DBTYPE_STR`, `DBTYPE_WSTR`, or `DBTYPE_BSTR` also report `DBCOLUMNFLAGS_ISLONG`, or if their maximum length exceeds 4,000 characters (or if no maximum length is reported), SQL Server treats them as a `text` or `ntext` column as appropriate. Similarly, for `DBTYPE_BYTES` columns, if `DBCOLUMNFLAGS_ISLONG` is set or if the maximum length is higher than 8,000 bytes (or if maximum length is not reported), the columns are treated as `image` columns. `Text`, `ntext` and `image` columns are called LOB columns.

SQL Server does not expose the full text and image functionality on LOBs from an OLE DB provider. `TEXTPTRS` are not supported on large objects from an OLE DB provider; hence, none of the related functionality is supported, for example, the `TEXTPTR` system function and `READTEXT`, `WRITETEXT`, and `UPDATETEXT` statements. `SELECT` statements that retrieve entire LOBs columns are supported, as are `UPDATE`and `INSERT` statements for entire large object columns in remote tables.

SQL Server uses the structured storage interfaces on LOB columns if the provider supports them. The structured storage interfaces in increasing order of preference and functionality are as follows: `ISequentialStream`, `Istream`, or `ILockBytes`. If one or more of these interfaces are supported, the provider must return DBPROPVAL_OO_BLOB as the value of the `DBPROP_OLEOBJECTS` property when it is queried through the `IDBProperties` interface. Also, the provider should indicate support for the interfaces it supports in the `DBPROP_STRUCTUREDSTORAGE` property.

If the provider does not support any of the structured storage interfaces on LOB columns, SQL Server materializes this interface on its own and still exposes them as `text`, `ntext` or `image` columns.

#### Accessing LOB Columns

If the provider supports one of the structured storage interfaces, SQL Server performs the following steps to retrieve LOB columns during query execution:

1. Before opening the rowset through `IOpenRowset::OpenRowset`, SQL Server requests support for one or more of the structured storage interfaces (`ISequentialStream`, `Istream`, and `ILockBytes`) on the large object columns. The first interface supported by the provider is required; additional interfaces are requested as \"set if cheap\" by setting the *dwOptions* element of the corresponding DBPROP structure to DBPROPOPTIONS_SETIFCHEAP. For example, if a provider supports both `ISequentialStream` and `ILockBytes`, `ISequentialStream` is required and `ILockBytes` is requested as \"set if cheap.\"

4. After the rowset is opened, SQL Server uses `IRowsetInfo::GetProperties` to identify the actual interfaces available in the rowset. The last or most preferable interface that the provider returned is used. When SQL Server creates an accessor against the large object column, the column is bound as DBTYPE_IUNKNOWN with the *iid* element of the DBOBJECT structure in the binding set to the interface.

#### Reading from LOB Columns

Use the interface pointer for the requested structured storage interface returned in the row buffer from `IRowset::GetData` to read from the large object column. If the provider does not support multiple open LOBs at the same time (that is, if it does not support `DBPROP_MULTIPLE_STORAGEOBJECTS`) and if the row has multiple large object columns, SQL Server copies the LOB columns into a local work table.

#### `UPDATE` and `INSERT` Statements on LOB Columns

SQL Server passes to the provider a pointer to a new storage object rather than using the provider-supplied interface to modify the storage object. For each LOB column, the value that is updated or inserted on a storage object is created with the chosen structured storage interface. Depending on whether it is an `UPDATE`or an `INSERT` operation, a pointer to the storage object is passed to the provider through `IRowsetChange::SetData` or `IRowsetChange::InsertRow`, respectively.

### Error Handling

When a specific method invocation against an OLE DB provider returns an error code, SQL Server looks for the provider's extended error information before returning information about the error condition to the user.

SQL Server uses the OLE DB error object as specified by OLE DB. Some of the high-level steps are:

1. When a method invocation returns an error code from the provider, SQL Server looks for the `ISupportErrorInfo` interface. If this interface is supported, SQL Server calls `ISupportErrorInfo::InterfaceSupportsErrorInfo` to verify whether error objects are supported by the interface that produced the error code.

<!-- -->

5. If error objects are supported by the interface, SQL Server calls the `GetErrorInfo` function to get an `IErrorInfo` interface pointer on the current error object.

6. SQL Server uses the `IErrorInfo` interface to get a pointer to the `IErrorRecords` interface.

7. SQL Server uses `IErrorRecords` to loop through all the error records in the object and get the error message text corresponding to each record.

For more information about how the provider's error object is used, see your OLE DB documentation.

### Security

When a consumer connects to an OLE DB provider, the provider typically requires a user ID and a password, unless the consumer wants to be authenticated as an integrated security user. In the case of distributed queries, SQL Server acts as the OLE DB provider's consumer on behalf of the SQL Server login that executes the distributed query. SQL Server maps the current SQL Server login to a user ID and password on the linked server.

These mappings can be specified by the user for a given linked server and can be set up and managed by the system stored procedures `sp_addlinkedsrvlogin` and `sp_droplinkedsrvlogin`. By setting the initialization group properties DBPROP_AUTH_USERID and DBPROP_AUTH_PASSWORD through `IDBProperties::SetProperties`, the user ID and password determined by the mapping are passed to the provider during connection establishment.

When a client connects to SQL Server through Windows Authentication, and if the login has a `self` mapping set up using `sp_addlinkedsrvlogin`, then SQL Server attempts to impersonate the client's security context and sets the `DBPROP_AUTH_INTEGRATED` property on the provider during connection establishment. This process is called *delegation*.

After the security context used for the connection is determined, the authentication of this security context and the permission checking for that context against data objects in the data source are entirely up to the OLE DB provider.

For more information, see [`sp_addlinkedsrvlogin`](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md) and [`sp_droplinkedsrvlogin`](../../relational-databases/system-stored-procedures/sp-droplinkedsrvlogin-transact-sql.md).

## Query Execution Scenarios

 When evaluating a distributed query, SQL Server interacts with the OLE DB provider in one or more of these scenarios:

- Remote query

- Indexed access

- Pure table scans

- `UPDATE`and DELETE statements

- `INSERT` statement

- Pass-through queries

### Remote Query

SQL Server generates an SQL query that evaluates a portion of the original query that can be evaluated in its entirety by the provider. This scenario is possible only against SQL Command Providers. The extent to which SQL Server pushes operations to the provider by generating an SQL query depends on the SQL grammar that the provider supports. The provider should indicate its level of SQL support through the following:

1. By indicating SQL Minimum, ODBC Core or SQL-92 Entry level support through the `DBPROP_SQLSUPPORT` property. The SQL Minimum syntax level is a new level that is supported in SQL Server that allows SQL Server to send remote queries to simple providers that support a simple subset of SQL. This level encompasses a basic `SELECT` statement that does not include subqueries, multiple tables in the `FROM` clause (hence no joins) and GROUP BY. For the subset of the SQL grammar that is used by SQL Server for generating remote queries against providers of each of these syntax levels, see [SQL Subset Used for Generating Remote Queries](#appendixb).

1. By supporting various SQL Server specific properties to indicate support for individual SQL features that are not otherwise included in the syntax level as reported by DBPROP_SQLSUPPORT. The list of properties and how they are used by SQL Server are described later in this section.

SQL Server uses parameterized query execution with a question mark (?) as the parameter marker in the Transact-SQL string. Parameterized query execution is used against the SQL Server, Microsoft Jet, and Oracle OLE DB providers. Against other providers, parameterized query execution is used if the provider supports `ICommandWithParameters` on the `Command` object and at least one of the following conditions are met:

- The provider indicates the ODBC Core level of SQL Server support through the `DBPROP_SQLSUPPORT` property.

- The provider indicates support for the question mark (?) parameter marker by supporting the SQLPROP_DYNCMICSQL SQL Server-specific property through `IDBPProperties`. For more information, see the next section on provider properties.

- The administrator sets the `Dynamic Parameters` provider option on the provider to make SQL Server generate parameterized queries.

When SQL Server generates the SQL text to be executed remotely, the table and column names are quoted with the quoting character of the provider as reported through the `DBLITERAL_QUOTE` literal of the `IDBInfo` interface. If this literal is not supported, table and column names are not quoted.

If the provider supports parameterized query execution, SQL Server considers a parameterized query execution strategy to evaluate a join of a remote table with a local table. The parameterized query is executed repeatedly for parameter values generated from each row of the local table. This strategy reduces the number of rows that are retrieved from the provider and is beneficial when a local table with a small number of rows is joined with a remote table with a large number of rows. This remote join strategy can be enforced by the `REMOTE` join optimizer hint. For more information about parameterized query execution, see [How to: Perform Parameterized Queries](../../connect/php/how-to-perform-parameterized-queries.md).

The following are the higher-level steps against the provider in the remote query scenario.

1. SQL Server creates a `Command` object from the `Session` object by using `IDBCreateCommand::CreateCommand`.

9. If the `Remote Query Timeout` server configuration option is set to a value > 0, SQL Server sets the `DBPROP_COMMANDTIMEOUT` property on the `Command` object to the same value by using `ICommandProperties::SetProperties`; `ICommand::SetCommandText` must be called to set the command text to the generated Transact-SQL string.

10. SQL Server calls `ICommandPrepare::Prepare` to prepare the command. If the provider does not support this interface, SQL Server continues with Step 4.

11. If the generated query is parameterized, SQL Server uses `ICommandWithParameters::SetParameterInfo` to describe the parameters and `IAccessor::CreateAccessor` to create accessors for the parameters.

12. SQL Server calls `ICommand::Execute` to execute the command and create the rowset.

13. SQL Server uses the `IRowset` interface to navigate and consume rows from the table. Use `IRowset::GetNextRows` to fetch rows, `IRowset::RestartPosition` to reposition to the beginning of the rowset, and `IRowset::ReleaseRows` to release rows.

#### Provider Properties of Interest for Remote Query Execution

If the provider supports SQL features that are not covered by the syntax level reported in DBPROP_SQLSUPPORT, it can indicate them using various provider-specific properties.

- SQLPROP_GROUPBY. This property is of interest to a provider that supports the SQL-Minimum level. This property indicates that the provider supports the GROUP BY and HAVING clauses in the `SELECT` statement. In addition, this property also indicates that the provider supports the following five aggregate functions MIN, MAX, SUM, COUNT, and AVG. The provider may not support DISTINCT on the argument of these aggregate functions.

- SQLPROP_SUBQUERIES. This property is of interest in a provider that supports the SQL-Minimum level. It indicates that the provider supports subqueries as specified by SQL-92 entry level. This includes subqueries in the `SELECT` list and in the `WHERE` clause with support for correlated subqueries, `IN`, `EXISTS`, `ALL`, and `ANY` operators.

- SQLPROP_DATELITERALS. This property is of interest to any provider (including those that support SQL-92 entry level). Support for standard literal syntax for datetime literals is not part of SQL-92 entry level. This SQL Server-specific property indicates that the provider supports datetime literal syntax as specified by the SQL-92 standard.

- SQLPROP_ANSILIKE. Of interest to a provider that supports the SQL-Minimum level. This property indicates that the provider supports the `LIKE` operator as per SQL-92 entry level (\'%\' and \'_\' as wildcard characters). This will be of use against a provider that supports the SQL-Minimum level because the SQL-Minimum Level does not include `LIKE` support.

- SQLPROP_INNERJOIN. This property is of interest to providers that support the SQL-Minimum level. It indicates support for multiple tables in the `FROM` clause. This will be of use against a provider that supports only the SQL-Minimum level because the SQL-Minimum level does not include support for joins. This does not indicate support for explicit JOIN keywords and does not indicate support for OUTER joins. It indicates only supporting implicit joins through a list of tables in the `FROM` clause.

- SQLPROP_DYNAMICSQL. Indicates support for `?` as a parameter-marker. The parameter marker should be supported in the place of a scalar item in a `WHERE` clause or in the `SELECT` list. Support for `?` parameter markers allows SQL Server to send parameterized queries to the provider.

- SQLPROP_NESTEDQUERIES. Indicates support for nested SELECTs in the `FROM` clause (for example, `SELECT * FROM (SELECT * FROM T))`. In many cases, SQL Server uses nested `SELECT` statements in the `FROM` clause of a query when it generates the query strings to be executed remotely. Because nested `SELECT` support is not required by SQL-92 entry level, SQL Server does not delegate queries with nested `SELECT` statements to the provider unless the provider also sets this property. Alternatively, the administrator can also set the `Nested Queries` provider option for the provider to make SQL Server generate nested queries against the provider.

The provider can support these properties using a SQL Server specific property set called `SQLPROPSET_OPTHINTS` and have defined `PROPID` values. The property set `SQLPROPSET_OPTHINTS` and the two properties are defined by using the following constants:

```
extern const GUID `SQLPROPSET_OPTHINTS` = { 0x2344480c, 0x33a7, 0x11d1, { 0x9b, 0x1a, 0x0, 0x60, 0x8, 0x26, 0x8b, 0x9e } };
enum SQLPROPERTIES {
SQLPROP_NESTEDQUERIES = 0x4,
SQLPROP_DYNAMICSQL = 0x5,
SQLPROP_GROUPBY = 0x6,
SQLPROP_DATELITERALS = 0x7,
SQLPROP_ANSILIKE = 0x8,
SQLPROP_INNERJOIN = 0x9,
SQLPROP_SUBQUERIES = 0x10
};
```

#### Character Set and Sort Order Implications

SQL Server supports specifying a Collation for character data at a per column level. Collation includes both the character set and the sort order specification for non-Unicode character data (`char` and `varchar` columns). For Unicode data (`nchar` and `nvarchar` columns), collation specifies only the sort order.

SQL Server delegates string comparisons to the provider only if the character set (for non-Unicode data), sort order, and string comparison semantics used by the linked server are the same as those used by the local server.

In the case of SQL Server linked servers, SQL Server automatically determines collation compatibility. For other providers, the administrator must indicate to SQL Server the collation of character data from a given linked server. In SQL Server, a new linked server option called `Collation Name` is supported. If the administrator determines that the collation semantics adopted by the linked server is the same as one of the SQL Server standard collations, she can set the `Collation Name` option to that collation name. The `Collation Name` option can be set using the `sp_serveroption` system stored procedure. This option should be set only if both of the following conditions are met:

- The remote sort order and character set are the same as the specified SQL Server collation.

- The string comparison semantics used by the OLE DB provider follow that of SQL-92 standard specifications or equivalently the comparison semantics of SQL Server.

The option Collation Compatible supported in SQL Server 7.0 is still supported, for backward compatibility reasons. Setting it to true is equivalent to setting the Collation Name option to the default collation of the master database of SQL Server. New applications should use the Collation Name option instead of the Collation Compatible option.

### Indexed Access

SQL Server uses an index exposed by the provider to evaluate certain predicates of the distributed query. This scenario is possible only against Index providers and when the user sets the `Index as Access Path` provider option. The following are the major high-level steps that SQL Server performs against the provider while using an index to execute a query:

1. Opens the index rowset through `IOpenRowset::OpenRowset` with the full table name and index name. The full table and index names are generated as described earlier in the Remote Query scenario.

1. Opens the base table rowset through `IOpenRowset::OpenRowset` with the full table name.

1. Sets ranges on the index rowset based on the query predicate through `IRowsetIndex::SetRange`.

1. Scans rows off the index rowset through `IRowset` on the index rowset.

1. Uses the bookmark column from the retrieved index rows to fetch corresponding rows from the base table rowset through `IRowsetLocate::GetRowsByBookmark`.

The rowset properties `DBPROP_IRowsetLocate` and `DBPROP_BOOKMARKS` are required on the rowset opened against the base table.

### Pure Table Scans

SQL Server scans the entire remote table from the provider and performs all query evaluation locally. The rowset corresponding to the table is opened by calling `IOpenRowset::OpenRowset`. SQL Server constructs the table name supplied to `OPENROWSET` from the catalog, schema, and object name parts as follows:

1. Each of the name parts are quoted with the provider's quoting character (`DBLITERAL_QUOTE`) and then concatenated with the `DBLITERAL_CATALOG_SEPARATOR` character embedded between them.

1. After the rowset object is opened, SQL Server uses the `IColumnsInfo` interface to verify that the execution-time meta data is the same as compile-time meta data for the table.

1. SQL Server uses the `IRowset` interface to navigate and consume rows from the table. Use `IRowset::GetNextRows` to fetch rows, `IRowset::RestartPosition` to reposition to the beginning of the rowset, and `IRowset::ReleaseRows` to release rows.

### `UPDATE` and `DELETE` Statements

The following conditions must be satisfied for a remote table to be updated or deleted from a SQL Server distributed query:

- The provider must support bookmarks for the rowset opened through `IOpenRowset` on the table being updated or deleted.

- The provider must support the `IRowsetLocate` and `IRowsetChange` interfaces on the rowset opened through `IOpenRowset` for the table being updated or deleted.

- The `IRowsetChange` interface must support update (`SetData`) and delete (`DeleteRows`) methods.

- If the provider does not support `ITransactionLocal`, `UPDATE` and `DELETE` statements are allowed only if the `Non-transacted` option is set for that provider and if the statement is not in a user transaction.

- If the provider does not support `ITransactionJoin`, an `UPDATE` or `DELETE` statement is allowed only if it is not in a user transaction.

The following rowset properties are required on the rowset opened against the updated table: `DBPROP_IRowsetLocate`, `DBPROP_IRowsetChange`, and `DBPROP_BOOKMARKS`. The `DBPROP_UPDATABILITY` rowset property is set to `DBPROPVAL_UP_CHANGE` or `DBPROPVAL_UP_DELETE` depending on whether the operation performed is an `UPDATE` or a `DELETE`, respectively.

The following high-level steps against the provider for processing an `UPDATE`or `DELETE` operation are performed:

1. SQL Server opens the base table rowset through the `IOpenRowset` interface. SQL Server requires the above-mentioned properties on the rowset.

1. SQL Server determines the set of qualifying rows to be updated or deleted.

1. SQL Server uses the bookmarks to position on the qualifying rows through the `IRowsetLocate` interface.

1. Use `IRowsetChange::SetData` for `UPDATE`operations or `IRowsetChange::DeleteRows` for delete operations to perform the required changes on the qualifying rows.

### `INSERT` Statement

The conditions for supporting `INSERT` statements against a remote table are less stringent than for `UPDATE`and `DELETE` statements:

- The provider must support `IRowsetChange::InsertRow` on the rowset opened on the base table being inserted into.

- If the provider does not support `ITransactionLocal`, `INSERT` statements are allowed only if the `Non-transacted updates` option is set for that linked server and if the statement is not in a user transaction.

- If the provider does not support `ITransactionJoin`, `INSERT` statements are allowed only if they are not in a user transaction.

SQL Server uses `IOpenRowset::OpenRowset` to open a rowset on the base table and calls `IRowsetChange::InsertRow` to insert new rows into the base rowset.

### Pass-through Queries

This scenario is similar to the scenario in remote query except that the command text given to `ICommand` is a command string submitted by the user and is not interpreted by SQL Server. SQL Server uses `DBGUID_DEFAULT` as the dialect identifier when it calls `ICommandText::SetCommandText`. `DBGUID_DEFAULT` indicates that the provider should use its default dialect. If this command text returns more than one result set, for example, if the command invokes a stored procedure that returns multiple result sets, SQL Server would use only the first result set from the command.

For a list of all OLE DB interfaces that SQL Server uses, see [OLE DB Interfaces Consumed by SQL Server](#appendixa).

### Conclusion

Microsoft SQL Server offers the most robust set of tools for accessing data from heterogeneous data sources. By understanding the OLE-DB interfaces exposed by SQL Server, developers can exert a high degree of control and sophistication in distributed queries.

## <a name="appendixa"></a> OLE DB Interfaces Consumed by SQL Server

The following table lists all the OLE DB interfaces that are used by SQL Server. The Required column indicates whether the interface is part of the bare minimum OLE DB functionality that SQL Server needs or whether it is optional. If a given interface is not marked as required, SQL Server can still access the provider, but some specific SQL Server functionality or optimization is not possible against the provider.

In the case of the optional interfaces, the Scenarios column indicates one or more of the six scenarios that use the specified interface. For example, the `IRowsetChange` interface on base table rowsets is an optional interface; this interface is used in the `UPDATE`and DELETE statements and `INSERT` statement scenarios. If this interface is not supported, UPDATE, DELETE, and `INSERT` statements cannot be supported against that provider. Some of the other optional interfaces are marked \"performance\" in the Scenarios column, indicating that the interface results in better general performance. For example, if the `IDBSchemaRowset` interface is not supported, SQL Server must open the rowset twice: once for its meta data and once for query execution. By supporting `IDBSchemaRowset`, SQL Server performance is improved.

|Object|Interface|Required|Comments|Scenarios|
|:-----|:-----|:-----|:-----|:-----|
|Data Source object|`IDBInitialize`|Yes|Initialize and set up data and security context.| |
| |`IDBCreateSession`|Yes|Create DB session object.| |
| |`IDBProperties`|Yes|Get information about capabilities of provider, set initialization properties, required property: DBPROP_INIT_TIMEOUT.| |
| |`IDBInfo`|No|Get quoting literal, catalog, name, part, separator, character, and so on.|Remote query.|
|DB Session object|`IDBSchemaRowset`|No|Get table/column meta data. Rowsets needed: `TABLES`, `COLUMNS`, `PROVIDER_TYPES`; others that are used if available: `INDEXES`, `TABLE_STATISTICS`.|Performance, indexed access.|
| |`IOpenRowset`|Yes|Open a rowset on a table, index or histogram.| |
| |`IGetDataSource`|Yes|Use to get back to the DSO from a DB session object.| |
| |`IDBCreateCommand`|No|Use to create a command object (query) for providers that   support querying.|Remote query, pass-through query.|
| |`ITransactionLocal`|No|Use for transacted updates.|`UPDATE` and `DELETE`, `INSERT` statements.|
| |`ITransactionJoin`|No|Use for distributed transaction support.|`UPDATE` and `DELETE`, `INSERT` statements if in a user transaction.|
|Rowset object|IRowset|Yes|Scan rows.| |
| |`IAccessor`|Yes|Bind to columns in a rowset.| |
| |`IColumnsInfo`|Yes|Get information about columns in a rowset.| |
| |`IRowsetInfo`|Yes|Get information about rowset properties.| |
| |`IRowsetLocate`|No|Needed for `UPDATE`/`DELETE` operations and to do index-based lookups; used to look up rows by bookmarks.|Indexed access, `UPDATE` and `DELETE` statements.|
| |`IRowsetChange`|No|Needed for `INSERTS`/`UPDATES`/ `DELETES` on a rowset. Rowsets against base tables should support this interface for `INSERT`, `UPDATE` and   `DELETE` statements.|`UPDATE` and `DELETE`, `INSERT` statements.|
| |`IConvertType`|Yes|Use to verify whether the rowset supports specific data type conversions on its columns.| |
|Index|`IRowset`|Yes|Scan rows.|Indexed access, performance.|
| |`IAccessor`|Yes|Bind to columns in a rowset.|Indexed access, performance.|
| |`IColumnsInfo`|Yes|Get information about columns in a rowset.|Indexed access, performance.|
| |`IRowsetInfo`|Yes|Get information about rowset properties.|Indexed access, performance.|
| |`IRowsetIndex`|Yes|Needed only for rowsets on an index; used for indexing functionality (set range, seek).|Indexed access, performance.|
|Command|`ICommand`|Yes| |Remote query, pass-through query.|
| |`ICommandText`|Yes|Use for defining the query text.|Remote query, pass-through query.|
| |`IColumnsInfo`|Yes|Use for getting column meta data for query results.|Remote query, pass-through query.|
| |`ICommandProperties`|Yes|Use to specify required properties on rowsets returned by the   command.|Remote query, pass-through query.|
| |`ICommandWithParameters`|No|Use for parameterized query execution.|Remote query, performance.|
| |`ICommandPrepare`|No|Use for preparing a command to get meta data (used in   pass-through queries if available).|Remote query, performance.|
|Error object|`IErrorRecords`|Yes|Use for getting a pointer to an `IErrorInfo` interface corresponding to a single error record.| |
| |`IErrorInfo`|Yes|Use for getting a pointer to an `IErrorInfo` interface corresponding to a single error record.| |
|Any object|`ISupportErrorInfo`|No|Use to verify whether a given interface supports error objects.| |

>[!NOTE]
>The `Index` object, `Command` object, and `Error` object are not mandatory. However, if they are supported, the listed interfaces are mandatory as specified in the Required column.

## <a name="appendixb"></a>SQL Subset Used for Generating Remote Queries

The SQL subset that SQL Server query processor generates against a SQL Command Provider depends on the syntax level that the provider supports as indicated by the `DBPROP_SQLSUPPORT` property.

SQL Command Providers that support SQL Entry level or ODBC Core

SQL Server uses the following subset of the SQL language for queries evaluated by SQL Command Providers that support either SQL-92 Entry level or ODBC Core:

1. `SELECT` statements with `SELECT`, `FROM`, `WHERE`, `GROUP BY`, `UNION`, `UNION ALL`, `ORDER BY DESC`, `ASC`, and `HAVING` clauses.

1. `UNION` and `UNION ALL` are generated only against providers that support SQL-92 entry level, not against those supporting ODBC Core.

1. `SELECT` clause:

   - Scalar subqueries in the `SELECT` list.

   - Column aliases without the `AS` keyword.

1. `FROM` clause:

   - Explicit join keywords are not used; comma-separated table names are used to specify inner joins, and outer joins are not specified in remote queries.

   - Nested queries of the form `FROM` ( `<nested query>` ) `<alias>`.

   - Table aliases without the AS keyword.

1. `WHERE` clause uses subqueries with `NOT` `EXISTS`, `ANY`, `ALL`.

1. Expressions:

   - Aggregate functions used: `MIN([DISTINCT])`, `MAX([DISTINCT])`, `COUNT([DISTINCT])`, `SUM([DISTINCT])`, `AVG([DISTINCT])`, and `COUNT(*)`.

   - Comparison operators: `<`, `=`, `<=`, `>`, `<>`, `>=`, `IS NULL`, and `IS NOT NULL`.

   - Boolean operators: `AND`, `OR`, and `NOT`.

 - Arithmetic operators: `+`, `-`, `*`, and `/`.

1. Constants:

- Numeric and money literals are always surrounded by `( )`.

- Character literals are quoted with `' '`.

SQL Command Providers that support the SQL Minimum level

Against SQL Command Providers that support the SQL Minimum level, SQL Server generates SQL using the following grammar.

This grammar was derived using the SQL Minimum grammar described in ODBC 3.0. All differences from this grammar are highlighted. The items shown in `*bold italics`* are those added to the SQL Minimum grammar described in ODBC 3.0. The items shown deleted in green are those removed from this grammar.

*select-statement* ::=

`SELECT [ALL | DISTINCT] *select-list* FROM *table-reference-list*[WHERE *search-condition*] [order-by-clause]`

`SELECT` clause

select-list ::= `*` | `select-sublist [, select-sublist]...`

select-sublist ::= expression * [`alias`]*

`*alias ::= user-defined-name`*

`FROM clause`

table-reference-list ::= table-reference

table-identifier ::= user-defined-name

table-name ::= table-identifier

table-reference ::= table-name

`WHERE clause`

search-condition ::= boolean-term \[OR search-condition\]

boolean-term ::= boolean-factor \[AND boolean-term\]

boolean-factor ::= \[NOT\] boolean-primary

boolean-primary ::= comparison-predicate \| ( search-condition )

comparison-predicate ::= expression comparison-operator expression

*\| `expression IS \[NOT\] NULL`*

comparison-operator ::= `< \| >` \| `<= \| >`= \| = \| `<>`

`ORDER BY clause`

order-by-clause ::= ORDER BY sort-specification \[, sort-specification\]\...

sort-specification ::= { \| column-name } \[ASC \| DESC\]

`Common syntactic elements`

expression ::= term \| expression {+\|--} term

term ::= factor \| term {\*\|/} factor

factor ::= \[+\|--\] primary

primary ::= column-name

\| literal

\| ( expression )

column-name ::= \[table-name.\]column-identifier

literal ::= character-string-literal

*\| integer-literal*

*\| exact-numeric-literal*

character-string-literal ::= \'{character}...\'

(character is any character in the character set of the driver/data source. To include a single literal quote character (\') in a character-string-literal, use two literal quote characters (\'\').)

`*integer-literal ::=* \[*+ \| -*\] *unsigned-integer`*

`*exact-numeric-literal::=* \[*+ \| -*\] *unsigned-integer* \[*period unsigned-integer*\]`

`*\| period unsigned-integer`*

base-table-name ::= base-table-identifier

base-table-identifier ::= user-defined-name

column-identifier ::= user-defined-name

user-defined-name ::= letter\[digit \| letter \| _\]\...

unsigned-integer ::= {digit}...

digit ::= 0 \| 1 \| 2 \| 3 \| 4 \| 5 \| 6 \| 7 \| 8 \| 9

period ::= . 

## <a name="appendixc"></a>SQL Server-Specific Properties

```
enum SQLPROPERTIES
       {
       SQLPROP_NOHPNEEDED = 0x1,
       SQLPROP_FREETHREADED = 0x2,
       SQLPROP_UMSENABLED = 0x3,
       SQLPROP_NESTEDQUERIES = 0x4,
       SQLPROP_DYNAMICSQL = 0x5,
       SQLPROP_GROUPBY = 0x6,
       SQLPROP_DATELITERALS = 0x7,
       SQLPROP_ANSILIKE = 0x8,
       SQLPROP_INNERJOIN = 0x9,
       SQLPROP_SUBQUERIES = 0x10, 
       SQLPROP_PARALLELSCAN = 0x11,
       SQLPROP_COLUMNCOLLATION = 0x12,
       SQLPROP_CARDINALITY = 0x13,
       SQLPROP_SIMPLEUPDATES = 0x14,
       SQLPROP_SQLLIKE = 0x15,
       SQLPROP_BITREMOTING = 0x16,
       SQLPROP_UNICODELITERALS = 0x17,
       SQLPROP_USELATESTCOLLATIONVERSION = 0x18
       };

```
