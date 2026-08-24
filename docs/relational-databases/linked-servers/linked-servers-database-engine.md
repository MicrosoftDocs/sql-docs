---
title: "Linked Servers (Database Engine)"
description: Linked servers enable SQL Server and Azure SQL Managed Instance to read from and execute data in remote data sources.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: vanto, randolphwest
ms.date: 12/05/2025
ms.service: sql
ms.topic: concept-article
helpviewer_keywords:
  - "OLE DB, linked servers"
  - "OLE DB provider, linked servers"
  - "server management [SQL Server], linked servers"
  - "linked servers [SQL Server]"
  - "distributed queries [SQL Server], linked servers"
  - "servers [SQL Server], linked"
  - "remote servers [SQL Server], linked servers"
  - "linked servers [SQL Server], about linked servers"
ai-usage: ai-assisted
---
# Linked servers (Database Engine)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Linked servers enable the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] to read data from remote data sources and execute commands against remote database servers (for example, OLE DB data sources), outside of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Typically, you configure linked servers to enable the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to execute a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement that includes tables in another instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or another database product such as Oracle. You can configure many types of OLE DB data sources as linked servers, including third-party database providers and Azure Cosmos DB.

> [!NOTE]  
> Linked servers are available in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)] (with some [constraints](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#linked-servers)). Linked servers aren't available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

## When to use linked servers?

Linked servers enable you to implement distributed databases that can fetch and update data in other databases. Use linked servers in scenarios where you need to implement database sharding without creating custom application code or directly loading from remote data sources. Linked servers offer the following advantages:

- The ability to access data from outside of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- The ability to issue distributed queries, updates, commands, and transactions on heterogeneous data sources across the enterprise.

- The ability to address diverse data sources similarly.

You can configure a linked server by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using the [sp_addlinkedserver](../system-stored-procedures/sp-addlinkedserver-transact-sql.md) statement. OLE DB providers vary greatly in the type and number of parameters required. For example, some providers require you to provide a security context for the connection using [sp_addlinkedsrvlogin](../system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md). Some OLE DB providers allow [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to update data on the OLE DB source. Others provide only read-only data access. For information about each OLE DB provider, consult documentation for that OLE DB provider.

## Linked server components

A linked server definition specifies the following objects:

- An OLE DB provider

- An OLE DB data source

An *OLE DB provider* is a DLL that manages and interacts with a specific data source. An *OLE DB data source* identifies the specific database that you can access through OLE DB. Although data sources queried through linked server definitions are ordinarily databases, OLE DB providers exist for various files and file formats. These files include plain text, spreadsheet data, and the results of full-text content searches.

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], the [Microsoft OLE DB Driver for SQL Server](../../connect/oledb/oledb-driver-for-sql-server.md) (PROGID: MSOLEDBSQL) is the default OLE DB provider. In earlier versions, the [SQL Server Native Client](../native-client/sql-server-native-client.md) (PROGID: SQLNCLI11) was the default OLE DB provider.

> [!IMPORTANT]  
> [!INCLUDE [snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

Microsoft supports linked servers to Excel and Access sources only when you use the 32-bit Microsoft.JET.OLEDB.4.0 OLE DB provider.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] distributed queries work with any OLE DB provider that implements the required OLE DB interfaces. However, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has been tested against the default OLE DB provider.

## Linked server details

The following illustration shows the basics of a linked server configuration.

:::image type="content" source="media/linked-servers-database-engine/configuration.png" alt-text="Diagram showing client tier, server tier, and database server tier.":::

Typically, you use linked servers to handle distributed queries. When a client application executes a distributed query through a linked server, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] parses the command and sends requests to OLE DB. The rowset request might be in the form of executing a query against the provider or opening a base table from the provider.

For a data source to return data through a linked server, the OLE DB provider (DLL) for that data source must be present on the same server as the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Linked servers support Active Directory pass-through authentication when using full delegation. Starting with [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU17, pass-through authentication with constrained delegation is also supported; however, [resource-based constrained delegation](/windows-server/security/kerberos/kerberos-constrained-delegation-overview) isn't supported.

> [!IMPORTANT]  
> When you use an OLE DB provider, the account under which the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have read and execute permissions for the directory, and all subdirectories, in which the provider is installed. This requirement applies to Microsoft-released providers and any third-party providers.

<a id="managing-providers"></a>

## Manage providers

There's a set of options that control how [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] loads and uses OLE DB providers that are specified in the registry.

<a id="managing-linked-server-definitions"></a>

## Manage linked server definitions

When you set up a linked server, register the connection information and data source information with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. After it's registered, you can refer to that data source with a single logical name.

Use stored procedures and catalog views to manage linked server definitions:

- Create a linked server definition by running `sp_addlinkedserver`.

- View information about the linked servers defined in a specific instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by running a query against the `sys.servers` system catalog view.

- Delete a linked server definition by running `sp_dropserver`. You can also use this stored procedure to remove a remote server.

You can also define linked servers by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. In Object Explorer, right-click **Server Objects**, select **New**, and select **Linked Server**. You can delete a linked server definition by right-clicking the linked server name and selecting **Delete**.

When you execute a distributed query against a linked server, include a fully qualified, four-part table name for each data source to query. This four-part name should be in the form `<linked_server_name>.<catalog>.<schema>.<object_name>`.

References to temporary objects always resolve to the local instance's `tempdb` where applicable, even when prefixing with the linked server name.

You can define linked servers to point back (loop back) to the server on which you define them. Loopback servers are most useful when testing an application that uses distributed queries on a single server network. Loopback linked servers are intended for testing and aren't supported for many operations, such as distributed transactions.

## Linked servers with Azure SQL Managed Instance

[Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview) linked servers support both SQL authentication and authentication with Microsoft Entra ID.

To use SQL Agent jobs on Azure SQL Managed Instance to query a remote server through a linked server, use [sp_addlinkedsrvlogin](../system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md) to create a mapping from a login on the local server to a login on the remote server. When the SQL Agent job connects to the remote server through the linked server, it executes the T-SQL query in the context of the remote login. For more information, see [SQL Agent jobs with Azure SQL Managed Instance](/ssms/agent/implement-sql-server-agent-security#linked-servers).

<a id="limitations-of-azure-ad-authentication"></a>

### Microsoft Entra authentication

Two supported Microsoft Entra authentication modes are: managed identity and pass-through. Use managed identity authentication to allow local logins to query remote linked servers. Use pass-through authentication to allow a principal that can authenticate with a local instance to access a remote instance through a linked server.

To use Microsoft Entra pass-through authentication for a linked server in Azure SQL Managed Instance, you need the following prerequisites:

- The same principal is added as a login on the remote server.
- Both instances are members of the [SQL trust group](/azure/azure-sql/managed-instance/server-trust-group-overview).

> [!NOTE]  
> Existing definitions of linked servers that you configured for pass-through mode support Microsoft Entra authentication. The only requirement is to add SQL Managed Instance to the [Server trust group](/azure/azure-sql/managed-instance/server-trust-group-overview).

The following limitations apply to Microsoft Entra authentication for linked servers in Azure SQL Managed Instance:

- Microsoft Entra authentication isn't supported for SQL managed instances in different Microsoft Entra tenants.
- Microsoft Entra authentication for linked servers is supported only with OLE DB driver version 18.2.1 and higher.

## SQL Server 2025 and MSOLEDBSQL version 19

Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], the MSOLEDBSQL provider uses Microsoft OLE DB Driver 19 by default. This updated driver introduces significant security enhancements, including support for [TDS 8.0](../security/networking/tds-8.md) and [TLS 1.3](../security/networking/tls-1-3.md).

TDS 8.0 improves security by adding a new encryption option and introduces a breaking change: the `Encryption` parameter is no longer optional. You must set it in your connection string when targeting another SQL Server instance.

> [!NOTE]  
> Without the `Encrypt` parameter, linked servers in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] default to `Encrypt=Mandatory` and require a valid certificate. Connections without a valid certificate fail.

The `Encryption` parameter offers three distinct settings:

- `Yes`, or `True`, or `Mandatory`
- `No`, or `False`, or `Optional`
- `Strict`

The `Strict` option mandates the usage of TDS 8.0, and requires a server certificate for secure connections. For `Yes`/`True`/`Mandatory`, a trusted certificate is expected. You can't use a self-signed certificate.

| OLE DB version | Encryption parameter | Possible values | Default value |
| --- | --- | --- | --- |
| OLE DB 18 | **Optional** | `True` or `Mandatory`, `False` or `No` | `No` |
| OLE DB 19 | **Required** | `No` or `False`, `Yes` or `Mandatory`, `Strict` (new) | `Yes` |

The `TrustServerCertificate` parameter is supported, but not recommended. Setting **Trust Server Certificate** to `Yes` disables certificate validation, weakening the security of encrypted connections. To use **Trust Server Certificate** the client must also enable it in the machine registry. For information about enabling **Trust Server Certificate**, see [Registry settings](../../connect/oledb/features/registry-settings.md). Setting `TrustServerCertificate=Yes` isn't recommended for production environments.

When you use `Encrypt=False` or `Encrypt=Optional`:

- No certificate is required.
- If a trusted certificate is provided, the driver doesn't validate it.
- The connection doesn't provide any encryption.

When you use `Encrypt=True` or `Encrypt=Mandatory`, and don't use `TrustServerCertificate=Yes`:

- The connection requires a valid CA-signed certificate.
- The certificate must match the server's FQDN.
- If the alternate name in the certificate differs from the SQL Server Host name, then `HostNameInCertificate` must be set to the FQDN.
- The certificate must be installed in the **Trusted Root Certification Authorities** store on the client machine.

When you use `Encrypt=Strict`:

- The connection enforces TDS 8.0.
- The connection requires a valid CA-signed certificate with FQDN match.
- `HostNameInCertificate` must be set to the FQDN.
- The certificate must be trusted by the client system.
- `TrustServerCertificate` configuration isn't supported. A valid certificate must be present.

| Trust Server Certificate client setting | Connection string/connection attribute Trust Server Certificate | Certificate validation |
| --- | --- | --- |
| 0 | `No` (default) | Yes |
| 0 | `Yes` | Yes |
| 1 | `No` (default) | Yes |
| 1 | `Yes` | No |

You must correctly specify these settings in the connection string when configuring linked server connections, to ensure compatibility and security with the new driver.

<a id="updating-from-previous-oledb-versions"></a>

#### Update from previous OLEDB versions

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

When you migrate from previous editions of SQL Server to [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] with Microsoft OLE DB Driver 19, existing linked server configurations might fail. Different default values for the encryption parameter might cause this failure unless you provide a valid certificate.

Alternatively, you can recreate the linked server and include `Encrypt=Optional` in the connection string. If you can't modify the linked server configuration, enable trace flag `17600` to maintain OLE DB 18 behavior and defaults.

In the SQL Server Management Studio (SSMS) Linked Server Creation Wizard, use the option **Other Data Sources** to manually configure the linked server Encryption options.

For more information about OLE DB 19 and encryption, certificate and Trust Server Certificate behavior for OLE DB 19, see [Encryption and certificate validation in OLE DB](../../connect/oledb/features/encryption-and-certificate-validation.md).

## Related content

- [Create linked servers (SQL Server Database Engine)](create-linked-servers-sql-server-database-engine.md)
- [sys.servers (Transact-SQL)](../system-catalog-views/sys-servers-transact-sql.md)
- [sp_linkedservers (Transact-SQL)](../system-stored-procedures/sp-linkedservers-transact-sql.md)
- [sys.sp_addlinkedserver (Transact-SQL)](../system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sys.sp_addlinkedsrvlogin (Transact-SQL)](../system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md)
- [sys.sp_dropserver (Transact-SQL)](../system-stored-procedures/sp-dropserver-transact-sql.md)
