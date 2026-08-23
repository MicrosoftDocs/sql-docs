---
title: "sys.sp_addlinkedserver (Transact-SQL)"
description: sp_addlinkedserver creates a linked server, providing access to distributed, heterogeneous queries against OLE DB data sources.
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf, randolphwest
ms.date: 05/01/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addlinkedserver_TSQL"
  - "sp_addlinkedserver"
helpviewer_keywords:
  - "sp_addlinkedserver"
dev_langs:
  - "TSQL"
---
# sys.sp_addlinkedserver (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a linked server. A linked server provides access to distributed, heterogeneous queries against OLE DB data sources. After a linked server is created by using `sp_addlinkedserver`, distributed queries can be run against this server. If the linked server is defined as an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], remote stored procedures can be executed.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_addlinkedserver
    [ @server = ] N'server'
    [ , [ @srvproduct = ] N'srvproduct' ]
    [ , [ @provider = ] N'provider' ]
    [ , [ @datasrc = ] N'datasrc' ]
    [ , [ @location = ] N'location' ]
    [ , [ @provstr = ] N'provstr' ]
    [ , [ @catalog = ] N'catalog' ]
    [ , [ @linkedstyle = ] linkedstyle ]
[ ; ]
```

## Arguments

#### [ @server = ] N'*server*'

The name of the linked server to create. *@server* is **sysname**, with no default.

#### [ @srvproduct = ] N'*srvproduct*'

The product name of the OLE DB data source to add as a linked server. *@srvproduct* is **nvarchar(128)**, with a default of `NULL`. If the value is `SQL Server`, *@provider*, *@datasrc*, *@location*, *@provstr*, and *@catalog* don't have to be specified.

#### [ @provider = ] N'*provider*'

The unique programmatic identifier (PROGID) of the OLE DB provider that corresponds to this data source. The *@provider* must be unique for the specified OLE DB provider installed on the current computer. *@provider* is **nvarchar(128)**, with a default of `NULL`.

- In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, if *@provider* is omitted, `SQLNCLI` is used. Using `SQLNCLI` will redirect [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to the latest version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider. The OLE DB provider is expected to be registered with the specified PROGID in the registry. Instead of `SQLNCLI`, `MSOLEDBSQL` is recommended.

- Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you must specify a provider name. `MSOLEDBSQL` is recommended. If you omit *@provider*, you can experience unexpected behavior.

- Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], `MSOLEDBSQL` uses Microsoft OLE DB Driver version 19, which adds support for [TDS 8.0](../security/networking/tds-8.md). However, this driver introduces a breaking change. You must now specify the `encrypt` parameter. Use `encrypt` to define whether or not encryption is mandatory. You must provide a valid CA-signed certificate to encrypt your connection to another SQL Server instance, or assign `encrypt=optional` in the *@provstr* argument. If you can't modify the linked server configuration, enable trace flag 17600 to maintain OLE DB version 18 behavior and defaults.

  For details about encryption properties, review [Major version differences](../../connect/oledb/major-version-differences.md).

> [!IMPORTANT]  
> [!INCLUDE [snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

#### [ @datasrc = ] N'*datasrc*'

The name of the data source as interpreted by the OLE DB provider. *@datasrc* is **nvarchar(4000)**, with a default of `NULL`. *@datasrc* is passed as the `DBPROP_INIT_DATASOURCE` property to initialize the OLE DB provider.

#### [ @location = ] N'*location*'

The location of the database as interpreted by the OLE DB provider. *@location* is **nvarchar(4000)**, with a default of `NULL`. *@location* is passed as the `DBPROP_INIT_LOCATION` property to initialize the OLE DB provider.

#### [ @provstr = ] N'*provstr*'

The OLE DB provider-specific connection string that identifies a unique data source. *@provstr* is **nvarchar(4000)**, with a default of `NULL`. The *@provstr* argument is either passed to IDataInitialize or set as the `DBPROP_INIT_PROVIDERSTRING` property to initialize the OLE DB provider.

When the linked server is created against the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, the instance can be specified by using the `SERVER` keyword as `SERVER=servername\instancename` to specify a specific instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The *servername* is the name of the computer on which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running, and *instancename* is the name of the specific instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to which the user will be connected.

- To access a mirrored database, a connection string must contain the database name. This name is necessary to enable failover attempts by the data access provider. The database can be specified in the *@provstr* or *@catalog* parameter. Optionally, the connection string can also supply a failover partner name.

- If you run `sp_addlinkedserver` from a local login, or a login that isn't part of the **sysadmin** role, you might receive the following error:

  ```output
  Access to the remote server is denied because no login-mapping exists.
  ```

  To resolve this issue, add the `User ID` parameter to your connection string. In the following example, `myUser` is the User ID passed to the connection string:

  ```sql
  EXECUTE master.dbo.sp_addlinkedserver
      @server = N'LinkServerName',
      @provider = N'SQLNCLI',
      @srvproduct = 'MS SQL Server',
      @provstr = N'SERVER=serverName\InstanceName;User ID=myUser';

  EXECUTE master.dbo.sp_addlinkedsrvlogin
      @rmtsrvname = N'LinkServerName',
      @locallogin = NULL,
      @useself = N'False',
      @rmtuser = N'myUser',
      @rmtpassword = N'*****';
  ```

  For more information, see [Access to the remote server is denied because no login-mapping exists](/archive/blogs/mdegre/access-to-the-remote-server-is-denied-because-no-login-mapping-exists).

#### [ @catalog = ] N'*catalog*'

The catalog to be used when a connection is made to the OLE DB provider. *@catalog* is **sysname**, with a default of `NULL`. *@catalog* is passed as the `DBPROP_INIT_CATALOG` property to initialize the OLE DB provider. When the linked server is defined against an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], catalog refers to the default database to which the linked server is mapped.

#### [ @linkedstyle = ] *linkedstyle*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

The following table shows the ways that a linked server can be set up for data sources that can be accessed through OLE DB. A linked server can be set up more than one way for a particular data source; there can be more than one row for a data source type. This table also shows the `sp_addlinkedserver` parameter values to be used for setting up the linked server.

| Remote OLE DB data source | OLE DB provider | @srvproduct | @provider | @datasrc | @location | @provstr | @catalog |
| --- | --- | --- | --- | --- | --- | --- | --- |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] <sup>1</sup> (default) | | | | | |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider | | `SQLNCLI` | Network name of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (for default instance) | | | Database name (optional) |
| [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider | | `SQLNCLI` | *servername*\\*instancename* (for specific instance) | | | Database name (optional) |
| Oracle, version 8 and later | Oracle Provider for OLE DB | Any <sup>2</sup> | `OraOLEDB.Oracle` | Alias for the Oracle database | | | |
| Access/Jet | Microsoft OLE DB Provider for Jet | Any <sup>2</sup> | `Microsoft.Jet.OLEDB.4.0` | Full path of Jet database file | | | |
| ODBC data source | Microsoft OLE DB Provider for ODBC | Any <sup>2</sup> | `MSDASQL` <sup>3</sup> | System DSN of ODBC data source | | | |
| ODBC data source | [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for ODBC | Any <sup>2</sup> | `MSDASQL` <sup>3</sup> | | | ODBC connection string | |
| File system | [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for Indexing Service | Any <sup>2</sup> | `MSIDXS` | Indexing Service catalog name | | | |
| [!INCLUDE [msCoName](../../includes/msconame-md.md)] Excel Spreadsheet | [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet | Any <sup>2</sup> | `Microsoft.Jet.OLEDB.4.0` | Full path of Excel file | | Excel 5.0 | |
| IBM Db2 Database | [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for DB2 | Any <sup>2</sup> | `DB2OLEDB` | | | See [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for DB2 documentation. | Catalog name of DB2 database |

<sup>1</sup> This way of setting up a linked server forces the name of the linked server to be the same as the network name of the remote instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use *@datasrc* to specify the server.

<sup>2</sup> "Any" indicates that the product name can be anything.

<sup>3</sup> Linked servers that use `MSDASQL` with a provider string (*@provstr*) might fail with error 7416 on [SQL Server 2022](../../sql-server/sql-server-2022-release-notes.md#linked-server-queries-that-use-msdasql-fail-with-error-7416) and [SQL Server 2025](../../sql-server/sql-server-2025-known-issues.md#linked-server-queries-that-use-msdasql-fail-with-error-7416).

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider is the provider that is used with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] if no provider name is specified or if [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is specified as the product name. Even if you specify the older provider name, SQLOLEDB, it changes to SQLNCLI when persisted to the catalog.

The *@datasrc*, *@location*, *@provstr*, and *@catalog* parameters identify the database or databases the linked server points to. If any one of these parameters is `NULL`, the corresponding OLE DB initialization property isn't set.

In a clustered environment, when you specify file names to point to OLE DB data sources, use the universal naming convention name (UNC) or a shared drive to specify the location.

The stored procedure `sp_addlinkedserver` can't be executed within a user-defined transaction.

> [!IMPORTANT]  
> Azure SQL Managed Instance currently supports only SQL Server, SQL Database, and other SQL managed instances as remote data sources.

> [!IMPORTANT]  
> When a linked server is created by using `sp_addlinkedserver`, a default self-mapping is added for all local logins. For non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] providers, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authenticated logins might be able to gain access to the provider under the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account. Administrators should consider using `sp_droplinkedsrvlogin <linkedserver_name>, NULL` to remove the global mapping.

## Managed identity authentication for SQL Server 2025

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces support for managed identity authentication when creating linked servers between SQL Server instances. This feature allows SQL Server instances running on Azure Virtual Machines or Azure Arc-enabled servers to use managed identities for secure, credential-free authentication to other SQL Server instances.

Managed identity authentication is supported when the following requirements are met:

- The source SQL Server instance is running on an Azure Virtual Machine with a system-assigned or user-assigned managed identity enabled, or on an Azure Arc-enabled server with a system-assigned managed identity configured.
- The destination is another SQL Server instance with Microsoft Entra authentication configured.
- A login matching the source server's name has been created on the destination SQL Server instance from an external provider.
- The connection uses the Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL) with `Authentication=ActiveDirectoryMSI` in the provider string.

For detailed configuration steps, see [Configure managed identity for linked servers](../../sql-server/azure-arc/managed-identity-support-linked-server.md).

## Permissions

The `sp_addlinkedserver` statement requires the `ALTER ANY LINKED SERVER` permission. (The [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **New Linked Server** dialog box is implemented in a way that requires membership in the **sysadmin** fixed server role.)

## Examples

### A. Use the Microsoft SQL Server OLE DB Provider

The following example creates a linked server named `SEATTLESales`. The product name is `SQL Server`, and no provider name is used.

```sql
USE master;
GO

EXECUTE sp_addlinkedserver N'SEATTLESales', N'SQL Server';
GO
```

The following example creates a linked server `S1_instance1` on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] OLE DB driver 18 and earlier versions.

```sql
EXECUTE sp_addlinkedserver
    @server = N'S1_instance1',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @datasrc = N'S1\instance1';
```

The following example creates the linked server `S1_instance1` but uses the Microsoft OLE DB Driver Version 19 in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], with the `encrypt=optional` parameter:

```sql
EXECUTE sp_addlinkedserver
    @server = N'S1_instance1',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @provstr = N'encrypt=optional',
    @datasrc = N'S1\instance1';
```

The following example creates the linked server `S1_instance1` using the Microsoft OLE DB Driver Version 19 in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], with the `encrypt=mandatory` parameter. This option requires a valid certificate. The self-signed certificate isn't accepted.

```sql
EXECUTE sp_addlinkedserver
    @server = N'S1_instance1',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @provstr = N'encrypt=mandatory',
    @datasrc = N'S1\instance1';
```

The following example creates the linked server `S1_instance1` using the Microsoft OLE DB Driver Version 19 in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], with `encrypt=mandatory` and `trustservercertificate=yes`. Because **Trust Server Certificate** is set to `yes`, self-signed certificates are accepted.

```sql
EXECUTE sp_addlinkedserver
    @server = N'S1_instance1',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @provstr = N'encrypt=mandatory;trustservercertificate=yes',
    @datasrc = N'S1\instance1';
```

The following example creates the linked server `S1_instance1` using the Microsoft OLE DB Driver Version 19 [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], with `encrypt=strict` for TDS 8.0 support.

```sql
EXECUTE sp_addlinkedserver
    @server = N'S1_instance1',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @provstr = N'encrypt=strict',
    @datasrc = N'S1\instance1';
```

The following example creates a linked server `S1_instance1` on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider.

> [!IMPORTANT]  
> SQL Server Native Client OLE DB provider (SQLNCLI) remains deprecated and it isn't recommended to use it for new development work. Instead, use the new [Microsoft OLE DB Driver for SQL Server](../../connect/oledb/oledb-driver-for-sql-server.md) (MSOLEDBSQL) which will be updated with the most recent server features.

```sql
EXECUTE sp_addlinkedserver
    @server = N'S1_instance1',
    @srvproduct = N'',
    @provider = N'SQLNCLI',
    @datasrc = N'S1\instance1';
```

### B. Use the Microsoft OLE DB Provider for Microsoft Access

The Microsoft.Jet.OLEDB.4.0 provider connects to Microsoft Access databases that use the 2002-2003 format. The following example creates a linked server named `SEATTLE Mktg`.

> [!NOTE]  
> This example assumes that both [!INCLUDE [msCoName](../../includes/msconame-md.md)] Access and the sample `Northwind` database are installed and that the `Northwind` database resides in C:\Msoffice\Access\Samples on the same server as the SQL Server instance.

```sql
EXECUTE sp_addlinkedserver
    @server = N'SEATTLE Mktg',
    @provider = N'Microsoft.Jet.OLEDB.4.0',
    @srvproduct = N'OLE DB Provider for Jet',
    @datasrc = N'C:\MSOffice\Access\Samples\Northwind.mdb';
GO
```

### C. Use the Microsoft OLE DB Provider for ODBC with the `datasrc` parameter

The following example creates a linked server named `SEATTLE Payroll` that uses the [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for ODBC (`MSDASQL`) and the *@datasrc* parameter.

> [!NOTE]  
> The specified ODBC data source name must be defined as System DSN in the server before you use the linked server.

```sql
EXECUTE sp_addlinkedserver
    @server = N'SEATTLE Payroll',
    @srvproduct = N'',
    @provider = N'MSDASQL',
    @datasrc = N'LocalServer';
GO
```

### D. Use the Microsoft OLE DB Provider for Excel spreadsheet

To create a linked server definition using the [!INCLUDE [msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet to access an Excel spreadsheet in the 1997 - 2003 format, first create a named range in Excel by specifying the columns and rows of the Excel worksheet to select. The name of the range can then be referenced as a table name in a distributed query.

```sql
EXECUTE sp_addlinkedserver 'ExcelSource',
   'Jet 4.0',
   'Microsoft.Jet.OLEDB.4.0',
   'c:\MyData\DistExcl.xls',
   NULL,
   'Excel 5.0';
GO
```

To access data from an Excel spreadsheet, associate a range of cells with a name. The following query can be used to access the specified named range `SalesData` as a table by using the linked server set up previously.

```sql
SELECT *
FROM ExcelSource...SalesData;
GO
```

If [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running under a domain account that's access to a remote share, a UNC path can be used instead of a mapped drive.

```sql
EXECUTE sp_addlinkedserver 'ExcelShare',
   'Jet 4.0',
   'Microsoft.Jet.OLEDB.4.0',
   '\\MyServer\MyShare\Spreadsheets\DistExcl.xls',
   NULL,
   'Excel 5.0';
```

### E. Use the Microsoft OLE DB Provider for Jet to access a text file

The following example creates a linked server for directly accessing text files, without linking the files as tables in an Access .mdb file. The provider is `Microsoft.Jet.OLEDB.4.0` and the provider string is `Text`.

The data source is the full path of the directory that contains the text files. A schema.ini file, which describes the structure of the text files, must exist in the same directory as the text files. For more information about how to create a schema.ini file, see the Jet Database Engine documentation.

First, create a linked server.

```sql
EXECUTE sp_addlinkedserver txtsrv, N'Jet 4.0',
   N'Microsoft.Jet.OLEDB.4.0',
   N'c:\data\distqry',
   NULL,
   N'Text';
```

Set up login mappings.

```sql
EXECUTE sp_addlinkedsrvlogin txtsrv, FALSE, Admin, NULL;
```

List the tables in the linked server.

```sql
EXECUTE sp_tables_ex txtsrv;
```

Query one of the tables, in this case `file1#txt`, using a four-part name.

```sql
SELECT * FROM txtsrv...[file1#txt];
```

### F. Use the Microsoft OLE DB Provider for DB2

The following example creates a linked server named `DB2` that uses the Microsoft OLE DB Provider for DB2.

```sql
EXECUTE sp_addlinkedserver
    @server = N'DB2',
    @srvproduct = N'Microsoft OLE DB Provider for DB2',
    @catalog = N'DB2',
    @provider = N'DB2OLEDB',
    @provstr = N'Initial Catalog=pubs;
       Data Source=DB2;
       HostCCSID=1252;
       Network Address=XYZ;
       Network Port=50000;
       Package Collection=admin;
       Default Schema=admin;';
```

### G. Add an Azure SQL database as a linked server for use with distributed queries on cloud and on-premises databases

You can add an Azure SQL database as a linked server and then use it with distributed queries that span the on-premises and cloud databases. This is a component for database hybrid solutions spanning on-premises corporate networks and the Azure cloud.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] box product contains the distributed query feature, which allows you to write queries to combine data from local data sources and data from remote sources (including data from non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data sources) defined as linked servers. Every Azure SQL database (except the logical server's `master` database) can be added as an individual linked server and then used directly in your database applications as any other database.

The benefits of using [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] include manageability, high availability, scalability, working with a familiar development model, and a relational data model. The requirements of your database application determine how it would use [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] in the cloud. You can move all of your data at once to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], or progressively move some of your data while keeping the remaining data on-premises. For such a hybrid database application, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] can now be added as linked servers and the database application can issue distributed queries to combine data from [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and on-premises data sources.

Here's an example explaining how to connect to an Azure SQL database using distributed queries.

First, add one Azure SQL database as linked server, using the using SQL Server Native Client.

```sql
EXECUTE sp_addlinkedserver
    @server = 'LinkedServerName',
    @srvproduct = '',
    @provider = 'sqlncli',
    @datasrc = 'ServerName.database.windows.net',
    @location = '',
    @provstr = '',
    @catalog = 'DatabaseName';
```

Add credentials and options to this linked server. Replace `<password>` with a valid password.

```sql
EXECUTE sp_addlinkedsrvlogin
    @rmtsrvname = 'LinkedServerName',
    @useself = 'false',
    @rmtuser = 'LoginName',
    @rmtpassword = '<password>';

EXECUTE sp_serveroption 'LinkedServerName', 'rpc out', true;
```

Now, use the linked server to execute queries using four-part names, even to create a new table and insert data.

```sql
EXECUTE ('CREATE TABLE SchemaName.TableName(col1 int not null CONSTRAINT PK_col1 PRIMARY KEY CLUSTERED (col1) )') AT LinkedServerName;

EXECUTE ('INSERT INTO SchemaName.TableName VALUES(1),(2),(3)') AT LinkedServerName;
```

Query the data using four-part names:

```sql
SELECT * FROM LinkedServerName.DatabaseName.SchemaName.TableName;
```

<a id="h-create-sql-managed-instance-linked-server-with-managed-identity-azure-ad-authentication"></a>

<a id="managed-identity-authentication"></a>

### H. Create Azure SQL Managed Instance linked server with managed identity authentication

[!INCLUDE [entra-id](../../includes/entra-id.md)]

To create a linked server with managed identity authentication, execute the following T-SQL, replacing `<managed_instance>` with your own SQL managed instance. The authentication method uses `ActiveDirectoryMSI` in the *@provstr* parameter. Consider optionally using `@locallogin = NULL` to allow all local logins.

```sql
EXECUTE master.dbo.sp_addlinkedserver
    @server = N'MyLinkedServer',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @provstr = N'Server=<mi_name>.<dns_zone>.database.windows.net,1433;Authentication=ActiveDirectoryMSI;';

EXECUTE master.dbo.sp_addlinkedsrvlogin
    @rmtsrvname = N'MyLinkedServer',
    @useself = N'False',
    @locallogin = N'user1@contoso.com';
```

To enable authentication with managed identities, a managed identity assigned to the Azure SQL Managed Instance needs to be added as a login to the remote managed instance. Both system-assigned and user-assigned managed identities are supported.

If a primary identity is set, it's used, otherwise the system-assigned managed identity is used. If the managed identity is recreated with the same name, the login on the remote instance also needs to be recreated, because the new managed identity Application ID and SQL Managed Instance service principal SID no longer match. To verify these two values match, convert SID to application ID with following query.

```sql
SELECT CONVERT (UNIQUEIDENTIFIER, sid) AS MSEntraApplicationID
FROM sys.server_principals
WHERE name = '<managed_instance_name>';
```

<a id="i-create-sql-managed-instance-linked-server-with-pass-through-azure-ad-authentication"></a>

<a id="pass-through-authentication"></a>

### I. Create SQL Managed Instance linked server with pass-through Microsoft Entra authentication

To create a linked server with pass-through authentication, execute following T-SQL, replacing `<managed_instance>` with your own SQL managed instance server:

```sql
EXECUTE master.dbo.sp_addlinkedserver
    @server = N'MyLinkedServer',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @datasrc = N'<mi_name>.<dns_zone>.database.windows.net,1433';
```

With pass-through authentication, the security context of the local login is carried over to the remote instance. Pass-through authentication requires the Microsoft Entra principal to be added as a login on both the local and remote Azure SQL Managed Instance. Both managed instances need to be in a [server trust group](/azure/azure-sql/managed-instance/server-trust-group-overview). When the requirements are met, user can sign in to a local instance and query the remote instance via the linked server object.

### J. Use Microsoft SQL Server OLE DB Provider version 19

The following example creates a linked server named `SQLSales`, targeting a SQL Server named `LABSQL2025` with instance name `SQL2025`, using OLE DB version 19, encryption is disabled.

```sql
EXECUTE sp_addlinkedserver
    @server = N'SQLSales',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL19',
    @datasrc = N'LABSQL2025\SQL2025',
    @provstr = N'Encrypt=No;';
```

For more information, see [Microsoft OLE DB Driver for SQL Server (MSOLEDBSQL) (recommended)](../../connect/oledb/oledb-driver-for-sql-server.md#1-microsoft-ole-db-driver-for-sql-server-msoledbsql-recommended).

### K. Create a linked server with managed identity authentication for SQL Server 2025

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

The following example creates a linked server from a source [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance to a destination SQL Server instance using managed identity authentication. This scenario requires that the source SQL Server instance is running on an Azure Virtual Machine or Azure Arc-enabled server with a managed identity enabled, and that the destination SQL Server has Microsoft Entra authentication configured.

Before creating the linked server on the source, you must create a login on the destination SQL Server that matches the source server's name:

```sql
-- Run on the destination SQL Server instance
USE [master];
GO

CREATE LOGIN [SourceServerName]
FROM EXTERNAL PROVIDER;
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [SourceServerName];
GO
```

On the source SQL Server, create the linked server using the Microsoft OLE DB Driver for SQL Server with `ActiveDirectoryMSI` authentication:

```sql
-- Run on the source SQL Server instance
EXECUTE master.dbo.sp_addlinkedserver
    @server = N'DestinationSQLServer',
    @srvproduct = N'',
    @provider = N'MSOLEDBSQL',
    @datasrc = N'DestinationSQLServer',
    @provstr = N'Authentication=ActiveDirectoryMSI';
GO
```

Configure the linked server login mapping:

```sql
EXECUTE master.dbo.sp_addlinkedsrvlogin
    @rmtsrvname = N'DestinationSQLServer',
    @useself = N'False',
    @locallogin = NULL,
    @rmtuser = NULL,
    @rmtpassword = NULL;
GO
```

Test the linked server connection:

```sql
-- Test the connection
EXECUTE master.dbo.sp_testlinkedserver DestinationSQLServer;
GO

-- Query the remote server
SELECT * FROM [DestinationSQLServer].[master].[sys].[databases];
GO
```

For complete configuration details including managed identity setup, Microsoft Entra authentication configuration, and permissions, see [Configure managed identity for linked servers](../../sql-server/azure-arc/managed-identity-support-linked-server.md).

## Related content

- [Distributed Queries stored procedures (Transact-SQL)](distributed-queries-stored-procedures-transact-sql.md)
- [sys.sp_addlinkedsrvlogin (Transact-SQL)](sp-addlinkedsrvlogin-transact-sql.md)
- [sys.sp_addserver (Transact-SQL)](sp-addserver-transact-sql.md)
- [sys.sp_dropserver (Transact-SQL)](sp-dropserver-transact-sql.md)
- [sys.sp_serveroption (Transact-SQL)](sp-serveroption-transact-sql.md)
- [sys.sp_setnetname (Transact-SQL)](sp-setnetname-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [System Tables (Transact-SQL)](../system-tables/system-tables-transact-sql.md)
