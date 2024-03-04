---
title: "Release notes for (SSMS) 20 Preview"
description: "Release notes for SQL Server Management Studio (SSMS) 20 Preview."
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 02/29/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---
# Release notes for SQL Server Management Studio (SSMS) 20 Preview

[!INCLUDE [sql-asdb-asdbmi-asa](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article details updates, improvements, and bug fixes for the current and previous versions of SSMS.

[!INCLUDE [ssms-connect-aazure-ad](../includes/ssms-connect-azure-ad.md)]

## Current SSMS release

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server Management Studio (SSMS) 20 Preview 1](https://go.microsoft.com/fwlink/?linkid=2262204)**

[!INCLUDE [ssms20-md](../includes/ssms20-md.md)] is the latest preview release of SSMS. If you need a previous version of SSMS, see [previous SSMS releases](release-notes-ssms.md#previous-ssms-releases).

### 20.0 Preview 1

- Release number: 20.0 Preview 1
- Build number: 20.0.65.0
- Release date: February 29, 2024

Available languages:

- [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x804)
- [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x404)
- [English (United States)](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x409)
- [French](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x40c)
- [German](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x407)
- [Italian](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x410)
- [Japanese](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x411)
- [Korean](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x412)
- [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x416)
- [Russian](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x419)
- [Spanish](https://go.microsoft.com/fwlink/?linkid=2262204&clcid=0x40a)

#### What's new in 20.0 Preview 1

| Feature | Details |
| --- | --- |
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.48. |
| Connection | The connection security properties **Encryption** and **Trust server certificate** now exist on the main sign in page in the Connection dialog for easier access. For more information, see [Connect with SQL Server Management Studio](quickstarts/ssms-connect.md). |
| Connection | A new property, **Host Name in Certificate**, used with the *Strict (SQL Server 2022 and Azure SQL)* and *Mandatory* **Encryption** options, has been added to the Login page of the Connection dialog. |
| Connection | Added icons to the Query Editor status bar to indicate the encryption method used for the connection. |
| Connection | Added Microsoft Entra sign in options to Replication wizards. |
| Drivers | Updated Microsoft.Data.SqlClient version to 5.1.4, which includes support for Strict encryption and TLS 1.3. |
| Libraries | Updated Server Management Objects (SMO) version to 171.1.x. |
| Libraries | Updated DacFx version to 162.1.x. |
| Options | Introduced a new option, **Trust server certificate for imported connections**, in **Tools > Options > SQL Server Object Explorer > Commands** under **Connection security**. For more information, see [Options (SQL Server Object Explorer - Commands)](menu-help/options-sql-server-object-explorer-commands.md). |

#### Bug fixes in 20.0 Preview 1

| Feature | Details |
| --- | --- |
| Connection | Resolved an issue with SSMS crashing when trying to connect to Azure Storage because the user didn't have access to any containers within the storage account. |
| Connection | Fixed an issue where users couldn't change their password with **Trust server certificate** enabled. |
| Link feature for Azure SQL Managed Instance | Improved handling for importing and deleting certificates. |
| Link feature for Azure SQL Managed Instance | Addressed issues related to text and images in the Link wizards. |
| Security | Addressed vulnerability for OpenSSL library. |

#### Known issues 20.0 Preview 1

| Feature | Details | Workaround |
| --- | --- | --- |
| Analysis Services | When you connect to Analysis Services with Microsoft Entra MFA, if you add a new role or open properties for a role, the message "the identity of the user being added to the role isn't fetched properly" appears. | This error is benign and can be ignored. The error is addressed within the Azure infrastructure, and no updates to SSMS are required. |
| Analysis Services | After adding a new role, or when opening properties for an existing role, you can't use **Search by name or email address** to add a user. | A user can be added with the **Manual Entry** option. |
| Database Designer | Selecting the Design option for a view referencing a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| Database Mirroring | If you launch the Database Mirroring Monitor from the mirrored node, the primary node isn't listed. | Register the mirrored node from Database Mirroring Monitoring, or use SSMS 18.12.1 to monitor from the mirrored node. |
| General SSMS | Import settings from SSMS 17 option not available. | Settings can be imported from SSMS 18. |
| Link feature for Azure SQL Managed Instance | After you remove an existing mirroring endpoint certificate on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], link creation through the wizard might fail due to unestablished trust between [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and Azure SQL Managed Instance, even though all checks are successful. | Use PowerShell command `Get-AzSqlInstanceServerTrustCertificate` to check whether [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] mirroring endpoint certificate named `<SQL_Server_Instance_Name>` exists in the Azure SQL Managed Instance. If so, use PowerShell command `Remove-AzSqlInstanceServerTrustCertificate` to remove it before a new link creation attempt. |
| Linked servers | Creating a linked server to Azure SQL Database with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] selected as Server type connects to the `master` database. | To create a linked server to Azure SQL Database, select **Other data source** for the **Server type**, and select **Microsoft OLE DB Provider for SQL Server** or **Microsoft OLE DB Driver for SQL Server** as the **Provider**. Enter the logical server name in the Data source field and the database name in the Catalog field. |
| Maintenance Plans | Selecting the **Files and Filegroups** radio button in the Backup Database Task causes the dialog to close unexpectedly. | No current alternative. |
| PolyBase | PolyBase node isn't visible in Object Explorer when you connect to [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. | Use SSMS 18.12.1. |
| Profiler | The Profiler menu isn't localized. | No current alternative. |
| Replication | If Azure SQL Managed Instance is the publisher and SSMS is running on a machine that isn't in the same virtual network as the publisher, you aren't able to insert a tracer token via Replication Monitor. | To insert tracer tokens, use Replication Monitor in SSMS on a machine that is in the same virtual network as the Azure SQL Managed Instance publisher. |
| Stretch DB | Removed Stretch DB Wizard. | Use T-SQL to configure Stretch DB or use SSMS 18.9.1 or earlier to use the Stretch DB Wizard. |

#### Known issues using Strict Encryption 20.0 Preview 1

| Feature | Details | Workaround |
| --- | --- | --- |
| Connection | When SQL Server is configured with **Force Strict Encryption**, selecting **Azure Data Studio -> New Query** from the server or database menu generates the error, "A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: TCP provider, error: 0 - An existing connection was forcibly closed by the remote host.) | Update the connection to use *Strict* instead of *Mandatory* for the **Encrypt** property in Azure Data Studio, and then connect. |
| Connection | Connecting to SQL Server with *Strict (SQL 2022 and Azure SQL)* selected for **Encryption** and a non-TCP/IP network protocol generates the error, "Cannot connect to SERVERNAME. A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: Shared Memory Provider, error: 15 - Function not supported) (Microsoft SQL Server, Error: 50)  The request is not supported" | Change the **Network protocol** connection property to use *TCP/IP*, or enable the TCP/IP protocol for the SQL Server. |
| Database Tuning Advisor | When SQL Server is configured with **Force Strict Encryption**, connection to the server from the Database Tuning Advisor isn't supported. | No alternative. |
| Maintenance Plans | When you connect to a server with *Strict (SQL Server 2022 and Azure SQL)* encryption, modifying an existing maintenance plan generates the error "Failed to connect to SERVERNAME. (Microsoft.SqlServer.ConnectionInfo) A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The target principal name is incorrect.)" | The problem doesn't occur when you connect with *Mandatory* or *Optional* encryption. |
| Profiler | When SQL Server is configured with **Force Strict Encryption**, connection to the server from Profiler isn't supported, and the error "Cannot connect to SERVERNAME. Class not registered (pfutil)" is generated. | Install MSOLEDBSQL version 19, available from [Download Microsoft OLE DB Driver for SQL Server](../connect/oledb/download-oledb-driver-for-sql-server.md). |
| PowerShell | When you connect to a server with *Strict (SQL Server 2022 and Azure SQL)* encryption, selecting **Start Powershell** from a node in Object Explorer generates the error "SQL Server PowerShell provider error: Could not connect to SERVERNAME. [Failed to connect to server SERVERNAME. --> A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.) --> An existing connection was forcibly closed by the remote host]". | No current alternative. |
| Profiler | When you connect to a server with *Strict (SQL Server 2022 and Azure SQL)* encryption and MSOLEDBSQL version 19 installed, traces can't be saved to, or loaded from, a database table. | No alternative. |
| SQL Server Logs | When SQL Server is configured with **Force Strict Encryption**, you can't view the SQL Server ERRORLOG files via Object Explorer, or executing `master.dbo.sp_enumerrorlogs` or `sys.xp_enumerrorlogs` via the Query Editor. | View the ERRORLOG files in the Log folder using File Explorer. |

You can reference [SQL user feedback](https://aka.ms/ssms-feedback) for other known issues (filter on **Tooling** under **Groups**) and to provide feedback to the product team.

## Related content

- [Microsoft Download Center](https://www.microsoft.com/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending)
- [Download SQL Server Management Studio (SSMS) 20 Preview](download-sql-server-management-studio-ssms-20.md)
