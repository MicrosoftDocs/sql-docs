---
title: Microsoft OLE DB Driver for SQL Server
description: The Microsoft OLE DB Driver for SQL Server connects C and C++ applications to SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 08/26/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ms.custom:
  - ignite-2024
ai-usage: ai-assisted
helpviewer_keywords:
  - "MSOLEDBSQL, about OLE DB Driver for SQL Server"
  - "OLE DB Driver for SQL Server, about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server], about OLE DB Driver for SQL Server"
  - "data access [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server"
  - "MSOLEDBSQL"
  - "native data access [OLE DB Driver for SQL Server]"
---

# Microsoft OLE DB Driver for SQL Server

[!INCLUDE [Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

The Microsoft OLE DB Driver for SQL Server is a standalone data access application programming interface (API) that's part of [OLE DB](/cpp/data/oledb/ole-db-programming-overview). It connects C and C++ applications to the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) in Azure SQL Database, SQL database in Microsoft Fabric, Azure SQL Managed Instance, and in supported versions of SQL Server. Microsoft first released it in 2018 as version 18 and included it in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].

`MSOLEDBSQL19` is the current driver. It's generally backward compatible with SQL Server Native Client (SNAC), and provides functionality beyond both SNAC and the SQL Server OLE DB provider that Windows Data Access Components (Windows DAC, formerly Microsoft Data Access Components, or MDAC) supplies.

## Choose your starting point

- To decide whether OLE DB is the right API for your application, start with [When to use OLE DB Driver for SQL Server](when-to-use-oledb-driver-for-sql-server.md).
- To install the driver and start writing code, go to [Download OLE DB Driver for SQL Server](download-oledb-driver-for-sql-server.md), [System requirements](system-requirements-for-oledb-driver-for-sql-server.md), and [Building applications with OLE DB Driver for SQL Server](applications/building-applications-with-oledb-driver-for-sql-server.md).
- To connect to Azure SQL with passwordless authentication, go to [Using Microsoft Entra ID](features/using-azure-active-directory.md) and [Using connection string keywords](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).
- To move from `SQLNCLI` or `SQLOLEDB`, go to [Updating an application to OLE DB Driver for SQL Server from MDAC](applications/updating-an-application-to-oledb-driver-for-sql-server-from-mdac.md) and [Major version differences](major-version-differences.md).
- To use the driver from ADO, go to [Using ADO with OLE DB Driver for SQL Server](applications/using-ado-with-oledb-driver-for-sql-server.md).
- To diagnose a connection or query problem, go to [Accessing diagnostic information in the extended events log](features/accessing-diagnostic-information-in-the-extended-events-log.md) and [Known issues](oledb-driver-for-sql-server-known-issues.md).

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL connection. It loads the server name and database name from application configuration, authenticates with a managed identity so that no secret appears in the connection string, and enables Tabular Data Stream (TDS) 8.0 encryption with full certificate validation. It sets a per-attempt connect timeout, and retries transient failures with exponential backoff and jitter.

The C++ snippet in this article omits includes, COM initialization, and the logging helper for brevity.

```cpp
std::wstring BuildConnectionString(const wchar_t* server, const wchar_t* database) {
    std::wstring cs = L"Provider=MSOLEDBSQL19";
    cs += L";Data Source=tcp:"; cs += server; cs += L",1433";
    cs += L";Initial Catalog="; cs += database;
    cs += L";Authentication=ActiveDirectoryMSI";   // managed identity, no stored secret
    cs += L";Use Encryption for Data=Strict";      // TDS 8.0 with certificate validation
    cs += L";Connect Timeout=30";                  // per-attempt connect timeout, in seconds
    cs += L";Connect Retry Count=3";               // idle connection resiliency, not initial connect
    cs += L";Connect Retry Interval=10";
    return cs;
}

// Transient fault codes documented for Azure SQL, plus the resource governance
// codes. Network termination and timeout errors (64, 233, 258, 10053, 10054,
// 10060) are retried a bounded number of times, which is the documented
// guidance for them. 258 is the code the driver reports for a connect timeout.
// 10053 and 10054 can also mean the encryption handshake failed rather than a
// plain network reset, so read the error text before assuming a network fault.
bool IsTransient(LONG nativeError) {
    switch (nativeError) {
        case 615: case 926: case 4060: case 4221:
        case 10928: case 10929: case 10936:
        case 40197: case 40501: case 40613:
        case 42108: case 42109:
        case 49918: case 49919: case 49920:
        case 40020: case 40143: case 40166: case 40540:   // failover subcodes
        case 64: case 233: case 258:
        case 10053: case 10054: case 10060:
            return true;
        default:
            return false;
    }
}

// Retries only errors that a new connection can clear, with exponential backoff
// plus jitter so that concurrent clients don't retry in lockstep.
HRESULT ConnectWithRetry(IDataInitialize* pDataInit, const std::wstring& connectionString,
                         int maxAttempts, IDBInitialize** ppDbInit) {
    HRESULT hr = E_FAIL;
    *ppDbInit = nullptr;
    for (int attempt = 1; attempt <= maxAttempts; ++attempt) {
        IDBInitialize* pDbInit = nullptr;
        hr = pDataInit->GetDataSource(nullptr, CLSCTX_INPROC_SERVER, connectionString.c_str(),
                                      IID_IDBInitialize, reinterpret_cast<IUnknown**>(&pDbInit));
        if (SUCCEEDED(hr) && SUCCEEDED(hr = pDbInit->Initialize())) {
            Log("INFO", "connected on attempt %d/%d", attempt, maxAttempts);
            *ppDbInit = pDbInit;
            return S_OK;
        }

        // Walks IErrorRecords and returns the first record that carries a real
        // SQL Server error number. Transport and timeout failures report a
        // generic wrapper record first, whose native error is 0. Errors the
        // server returns carry the number on the first record.
        LONG native = LogProviderErrors("connect", hr);
        if (pDbInit) pDbInit->Release();
        if (attempt == maxAttempts || !IsTransient(native)) return hr;

        // Cap the backoff at 64 seconds. This also keeps the shift in range
        // when a caller passes a large maxAttempts.
        int shift = (attempt - 1 < 6) ? attempt - 1 : 6;
        DWORD delayMs = (1UL << shift) * 1000UL + (DWORD)(GetTickCount64() % 500);
        Log("WARN", "retrying in %lu ms (attempt %d/%d)", delayMs, attempt + 1, maxAttempts);
        Sleep(delayMs);
    }
    return hr;
}
```

`Connect Retry Count` and `Connect Retry Interval` enable *idle connection resiliency*, which transparently restores a connection that dropped while idle. They don't retry the initial connect, which is why this snippet also implements application-level retry. Keep both.

This snippet builds its connection string for `IDataInitialize::GetDataSource`, which uses the spaced keyword names shown here, such as `Use Encryption for Data` and `Connect Retry Count`. `IDBInitialize::Initialize` and ADO use different names for the same settings, such as `Encrypt` and `ConnectRetryCount`.

`GetDataSource` accepts a name from the wrong set without raising an error, and the setting never takes effect. The connection then uses the driver default, which can weaken it. `Encrypt=Strict` on this path leaves encryption at `Mandatory` on `MSOLEDBSQL19`, so the connection drops TDS 8.0 and negotiates encryption in the cleartext prelogin, and it leaves encryption off entirely on `MSOLEDBSQL`. Not every case fails open: the wrong-set `TrustServerCertificate` is dropped the same way, which leaves the property at its default `false` and keeps certificate validation on.

Don't count on an error to catch the mistake. A name that belongs to no set, such as `ZzzNotAKeyword`, produces an `Invalid connection string attribute` record, but a name from the wrong set produces nothing. To confirm that a setting took effect, read the property back with `IDBProperties::GetProperties` before you connect. For the keyword set that goes with each API, see [Using connection string keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).

OLE DB reports diagnostics through the error object rather than the `HRESULT` alone, so classify failures before retrying. An authentication or configuration error then fails immediately instead of consuming the whole retry budget.

For more information about each part of this configuration, see:

- [Using connection string keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Using Microsoft Entra ID](features/using-azure-active-directory.md)
- [Encryption and certificate validation](features/encryption-and-certificate-validation.md)
- [Idle connection resiliency](features/idle-connection-resiliency.md)
- [OLE DB Driver for SQL Server support for high availability, disaster recovery](features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)

For the catalog of Azure SQL transient errors, see [transient fault error codes](/azure/azure-sql/database/troubleshoot-common-errors-issues#list-of-transient-fault-error-codes).

## Key features

- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, interactive, and integrated flows.
- **Strict encryption**: TDS 8.0 connections with full certificate validation, and TLS 1.3 in version 19.2.0 and later versions.
- **Idle connection resiliency**: Transparent restoration of a connection that dropped while idle.
- **Multiple active result sets (MARS)**: More than one pending request per connection.
- **Bulk copy**: High-throughput inserts through the bulk copy interfaces.
- **Table-valued parameters**: An entire result set passed to the server as a single parameter.
- **Always On availability groups**: Listener support with `MultiSubnetFailover` for fast failover.
- **UTF-8 and UTF-16 support**: Character data in both encodings.
- **Data classification**: Sensitivity metadata for classified columns.
- **Asynchronous operations**: Nonblocking data source and rowset operations.

## Get started

| Article | Description |
| --- | --- |
| [When to use OLE DB Driver](when-to-use-oledb-driver-for-sql-server.md) | When to choose OLE DB Driver for SQL Server over the other SQL Server drivers. |
| [Download](download-oledb-driver-for-sql-server.md) | Installer downloads for every supported driver version. |
| [System requirements](system-requirements-for-oledb-driver-for-sql-server.md) | Supported operating systems, SQL Server versions, and prerequisites to install first. |
| [Building applications](applications/building-applications-with-oledb-driver-for-sql-server.md) | Header and library files, installation layout, and what changes when you upgrade from MDAC. |
| [Creating an application](ole-db-driver/creating-a-oledb-driver-for-sql-server-application.md) | The call sequence an application follows, from connecting to executing a command to reading results. |
| [Support lifecycle](support-lifecycle.md) | Which driver versions are supported, and when each one leaves support. |

## Configure and connect

| Article | Description |
| --- | --- |
| [Connection string keywords](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md) | Every connection string keyword the driver accepts, with its accepted values. |
| [Data source objects](ole-db-data-source-objects/data-source-objects-ole-db.md) | Create and initialize the data source and session objects that a connection is built from. |
| [Using ADO with the driver](applications/using-ado-with-oledb-driver-for-sql-server.md) | Reach driver features such as MARS, query notifications, and the xml type from ADO. |
| [High availability and disaster recovery](features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | Connect through an availability group listener, and the keywords that control failover behavior. |
| [Idle connection resiliency](features/idle-connection-resiliency.md) | Automatically restore a connection that dropped while it was idle. |
| [LocalDB support](features/oledb-driver-for-sql-server-support-for-localdb.md) | Connect to a LocalDB instance for local development and testing. |

## Authenticate and secure

| Article | Description |
| --- | --- |
| [Using Microsoft Entra ID](features/using-azure-active-directory.md) | The Microsoft Entra authentication modes the driver supports, including managed identity and interactive. |
| [Encryption and certificate validation](features/encryption-and-certificate-validation.md) | Set Encrypt and TrustServerCertificate, and control how the server certificate is validated. |
| [Changing passwords programmatically](features/changing-passwords-programmatically.md) | Handle an expired password and set a new one without leaving your application. |
| [Service principal name (SPN) support in client connections](features/service-principal-name-spn-support-in-client-connections.md) | Set the service principal name on a connection so Kerberos mutual authentication succeeds. |
| [Using data classification](features/using-data-classification.md) | Read the sensitivity labels that SQL Server returns for classified columns. |

## Execute commands and process results

| Article | Description |
| --- | --- |
| [Commands](ole-db-commands/commands.md) | The `ICommand` interface and the command object model that command execution is built on. |
| [Command syntax](ole-db-commands/command-syntax.md) | The mix of ODBC SQL, ISO, and Transact-SQL syntax the driver accepts in command text. |
| [Command parameters](ole-db-commands/command-parameters.md) | Mark parameters in command text, and bind the types the driver supports for each. |
| [Using multiple active result sets (MARS)](features/using-multiple-active-result-sets-mars.md) | Keep more than one pending result set open on a single connection. |
| [Performing asynchronous operations](features/performing-asynchronous-operations.md) | Start an operation without blocking the calling thread, and poll or wait for it to finish. |
| [Working with query notifications](features/working-with-query-notifications.md) | Register for a notification when the result of a query changes on the server. |
| [Processing results how-to articles](ole-db-how-to/results/processing-results-how-to-topics-ole-db.md) | Worked examples that execute a stored procedure or function and read return codes, output parameters, and rows. |

## Work with rowsets and cursors

| Article | Description |
| --- | --- |
| [Rowsets](ole-db-rowsets/rowsets.md) | The rowset interfaces, and the properties that decide which kind of rowset you get. |
| [Fetching rows](ole-db-rowsets/fetching-rows.md) | Use `IRowset` to fetch rows sequentially, read column values, and release rows. |
| [Updating data in rowsets](ole-db-rowsets/updating-data-in-rowsets.md) | Request `IRowsetChange` or `IRowsetUpdate` to get a modifiable rowset, and control its locking. |
| [Bookmarks](ole-db-rowsets/bookmarks.md) | Save a row position and return to it later, instead of refetching sequentially. |
| [Rowsets and SQL Server cursors](ole-db-rowsets/rowsets-and-sql-server-cursors.md) | When the driver uses a default result set and when it opens a server cursor instead. |

## Bulk copy

| Article | Description |
| --- | --- |
| [Performing bulk copy operations](features/performing-bulk-copy-operations.md) | Move large volumes of rows into or out of a table through data files or program variables. |
| [Bulk copy data using IRowsetFastLoad](ole-db-how-to/bulk-copy-data-using-irowsetfastload-ole-db.md) | Bulk copy data into a SQL Server table with the `IRowsetFastLoad` interface. |
| [Send BLOB data using IRowsetFastLoad and ISequentialStream](ole-db-how-to/send-blob-data-to-sql-server-using-irowsetfastload-and-isequentialstream-ole-db.md) | Use `IRowsetFastLoad` to stream varying length BLOB data per row to SQL Server. |

## Table-valued parameters

| Article | Description |
| --- | --- |
| [Table-valued parameters overview](features/table-valued-parameters-oledb-driver-for-sql-server.md) | How table-valued parameters pass multiple rows of data to the server in a single parameter. |
| [Table-valued parameter reference](ole-db-table-valued-parameters/table-valued-parameters-ole-db.md) | Parameter rowset creation and parameter type discovery. |
| [Inserting data into table-valued parameters](ole-db-table-valued-parameters/inserting-data-into-table-valued-parameters.md) | The push model and pull model for supplying table-valued parameter rows. |
| [Use table-valued parameters](ole-db-how-to/use-table-valued-parameters-ole-db.md) | Create a table-valued parameter and pass its rows to a stored procedure. |

## Work with large and binary data

| Article | Description |
| --- | --- |
| [BLOBs and OLE objects](ole-db-blobs/blobs-and-ole-objects.md) | Read and write BLOB columns as streams through `ISequentialStream`. |
| [Getting large data](ole-db-blobs/getting-large-data.md) | Retrieve a large column value in chunks instead of one bound buffer. |
| [Setting large data](ole-db-blobs/setting-large-data.md) | Send a large column value to the server from a consumer storage object. |
| [FILESTREAM support](features/filestream-support.md) | Store large binary values that you can read through SQL Server or through the file system. |
| [FILESTREAM how-to articles](ole-db-how-to/filestream/filestream-and-ole-db.md) | Worked examples that read and write FILESTREAM columns with streaming interfaces. |

## Manage tables, indexes, and stored procedures

| Article | Description |
| --- | --- |
| [Tables and indexes](ole-db-tables-indexes/tables-and-indexes.md) | Create, alter, and drop tables and indexes through `ITableDefinition` and `IIndexDefinition`. |
| [Creating SQL Server tables](ole-db-tables-indexes/creating-sql-server-tables.md) | Define columns and call `ITableDefinition::CreateTable` to create a table. |
| [Creating SQL Server indexes](ole-db-tables-indexes/creating-sql-server-indexes.md) | Define a new index on an existing table with `IIndexDefinition::CreateIndex`. |
| [Stored procedures](ole-db/stored-procedures.md) | Call a stored procedure with ODBC CALL syntax or RPC, and read its return code and output parameters. |

## Data types

| Article | Description |
| --- | --- |
| [Data types overview](ole-db-data-types/data-types-ole-db.md) | How SQL Server types map to OLE DB types when you bind parameters and columns. |
| [Data type mapping in rowsets and parameters](ole-db-data-types/data-type-mapping-in-rowsets-and-parameters.md) | The full type mapping table for rowset columns and command parameters. |
| [Using large value types](features/using-large-value-types.md) | Bind the varchar(max), nvarchar(max), and varbinary(max) types. |
| [Using XML data types](features/using-xml-data-types.md) | Store and retrieve XML documents and fragments in an xml column. |
| [Using user-defined types](features/using-user-defined-types.md) | Bind CLR user-defined types, which the driver exposes as binary values with type metadata. |
| [Sparse columns support](features/sparse-columns-support-in-oledb-driver-for-sql-server.md) | Driver support for sparse columns, which are optimized for storing null values. |
| [UTF-8 support](features/utf-8-support-in-oledb-driver-for-sql-server.md) | Work with UTF-8 server collations and UTF-8 client encoding. |
| [UTF-16 support](features/utf-16-support-in-oledb-driver-for-sql-server.md) | How the driver handles surrogate pairs when it fills a client buffer. |
| [Date and time improvements](ole-db-date-time/date-and-time-improvements-ole-db.md) | Bind the date, time, datetime2, and datetimeoffset types, and the conversions they allow. |

## Transactions

| Article | Description |
| --- | --- |
| [Transactions overview](ole-db-transactions/transactions.md) | Local transaction support, and the Microsoft Distributed Transaction Coordinator for distributed transactions. |
| [Isolation levels](ole-db-transactions/isolation-levels-ole-db.md) | Set the isolation level for a session, and what concurrency each level allows. |
| [Working with snapshot isolation](features/working-with-snapshot-isolation.md) | Use row versioning to raise read concurrency without blocking writers. |
| [Supporting distributed transactions](ole-db-transactions/supporting-distributed-transactions.md) | Enlist a session in a distributed transaction with `ITransactionJoin::JoinTransaction`. |

## Diagnose and troubleshoot

| Article | Description |
| --- | --- |
| [Errors](ole-db-errors/errors.md) | How the driver reports failures, and which interfaces carry the detail. |
| [Retrieving error information](ole-db-errors/retrieving-error-information.md) | Walk the error interfaces to read message text, SQLSTATE, and the native error number. |
| [Accessing diagnostic information in the extended events log](features/accessing-diagnostic-information-in-the-extended-events-log.md) | Turn on driver tracing and read the resulting extended events log. |
| [Known issues](oledb-driver-for-sql-server-known-issues.md) | Open issues in the current driver, with workarounds where one exists. |
| [Release notes](release-notes-for-oledb-driver-for-sql-server.md) | What changed in each driver release, newest first. |

## Migrate to the current driver

There are three generations of Microsoft OLE DB providers for SQL Server. Use `MSOLEDBSQL19` for new and existing applications, and convert existing connection strings to it. The OLE DB provider was [undeprecated](/archive/blogs/sqlnativeclient/announcing-the-new-release-of-ole-db-driver-for-sql-server) and re-released in 2018.

| Generation | Provider string | Status |
| --- | --- | --- |
| Microsoft OLE DB Driver for SQL Server (this article) | `MSOLEDBSQL19`, `MSOLEDBSQL` | Supported. `MSOLEDBSQL19` is the current driver and the one to use for new development. |
| [SQL Server Native Client (SNAC)](../../relational-databases/native-client/sql-server-native-client.md) | `SQLNCLI11`, `SQLNCLI` | Removed from SQL Server 2022 and SQL Server Management Studio 19. Not recommended for new development. |
| [Microsoft OLE DB Provider for SQL Server](/previous-versions/sql/ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server) | `SQLOLEDB` | Ships in [Windows Data Access Components](/previous-versions/windows/desktop/ms692897(v=vs.85)). No longer maintained. Not recommended for new development. |

| Article | Description |
| --- | --- |
| [MSOLEDBSQL major version differences](major-version-differences.md) | Breaking changes between OLE DB Driver 19 and version 18, including encryption defaults, property type changes, and migration steps. |
| [Updating an application from MDAC](applications/updating-an-application-to-oledb-driver-for-sql-server-from-mdac.md) | What changed between the old OLE DB Provider for SQL Server and the current driver, and what to check before you update. |
| [Updating an application from SQL Server 2005 Native Client](applications/updating-an-application-from-sql-server-2005-native-client.md) | The breaking changes in OLE DB Driver for SQL Server since SQL Server Native Client in SQL Server 2005 (9.x). |

## Reference

| Article | Description |
| --- | --- |
| [OLE DB Driver for SQL Server features](features/oledb-driver-for-sql-server-features.md) | Index of the driver-specific features, and where each one is documented. |
| [OLE DB programming](ole-db/oledb-driver-for-sql-server-programming.md) | The COM API model the driver exposes, and how it talks to SQL Server over TDS. |
| [OLE DB how-to articles](ole-db-how-to/ole-db-how-to-topics.md) | Index of the OLE DB how-to articles, grouped by task. |
| [OLE DB interfaces](ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md) | The OLE DB interfaces and methods that exhibit provider-specific behavior in this driver. |
| [Schema rowset support](ole-db/schema-rowset-support-ole-db.md) | Provider-specific schema rowset behavior, including metadata returned from linked servers. |
| [Finding more information](finding-more-oledb-driver-for-sql-server-information.md) | Specifications, samples, and community resources outside this documentation set. |

## Related content

- [Driver feature support matrix](../driver-feature-matrix.md)
- [SNAC lifecycle explained](/archive/blogs/sqlreleaseservices/snac-lifecycle-explained)
