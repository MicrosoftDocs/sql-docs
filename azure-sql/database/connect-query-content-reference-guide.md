---
title: Connect and Query
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Links to Azure SQL Database quickstarts showing how to connect to and query Azure SQL Database, and Azure SQL Managed Instance.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 09/21/2026
ms.service: azure-sql
ms.subservice: connect
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
ms.custom: [sqldbrb=1, sfi-image-nochange]
ai-usage: ai-assisted
---

# Azure SQL Database and Azure SQL Managed Instance connect and query articles

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article links to examples that show how to connect to and query Azure SQL Database and Azure SQL Managed Instance. For Transport Layer Security (TLS) recommendations, see [TLS considerations for database connectivity](#tls-considerations-for-database-connectivity).

Watch this video in the [Azure SQL Database essentials series](/shows/azure-sql-database-essentials/) for a brief connect and query overview:

&nbsp;

> [!VIDEO https://learn-video.azurefd.net/vod/player?id=c2edd421-da6b-4598-a142-5980e4f38de9]

## Quickstarts

| Quickstart | Description |
| --- | --- |
| [SQL Server Management Studio (SSMS)](connect-query-ssms.md) | This quickstart demonstrates how to use SSMS to connect to a database, and then use Transact-SQL statements to query, insert, update, and delete data in the database. |
| [Azure portal](connect-query-portal.md) | This quickstart demonstrates how to use the [query editor](query-editor.md) to connect to a database (Azure SQL Database only), and then use Transact-SQL statements to query, insert, update, and delete data in the database. |
| [Visual Studio Code](connect-query-vscode.md) | This quickstart demonstrates how to use Visual Studio Code to connect to a database, and then use Transact-SQL statements to query, insert, update, and delete data in the database. |
| [.NET with Visual Studio](connect-query-dotnet-visual-studio.md) | This quickstart demonstrates how to use .NET and C# with Visual Studio to connect to a database and query data with Transact-SQL statements. |
| [.NET](connect-query-dotnet-core.md) | This quickstart demonstrates how to use .NET on Windows, Linux, or macOS to connect to a database and query data with Transact-SQL statements. |
| [Go](connect-query-go.md) | This quickstart demonstrates how to use Go to connect to a database. Transact-SQL statements to query and modify data are also demonstrated. |
| [Java](connect-query-java.md) | This quickstart demonstrates how to use Java to connect to a database and then use Transact-SQL statements to query data. |
| [Node.js](connect-query-nodejs.md) | This quickstart demonstrates how to use Node.js to create a program to connect to a database and use Transact-SQL statements to query data. |
| [PHP](connect-query-php.md) | This quickstart demonstrates how to use PHP to create a program to connect to a database and use Transact-SQL statements to query data. |
| [Python](connect-query-python.md) | This quickstart demonstrates how to use Python to connect to a database and use Transact-SQL statements to query data. |
| [Ruby](connect-query-ruby.md) | This quickstart demonstrates how to use Ruby to create a program to connect to a database and use Transact-SQL statements to query data. |

## Get server connection information

Get the connection information you need to connect to the database in Azure SQL Database. You need the fully qualified server name or host name, database name, and login information for the upcoming procedures.

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. Navigate to the **SQL Databases** or **SQL Managed Instances** page.

1. On the **Overview** page, review the fully qualified server name next to **Server name** for the database in Azure SQL Database or the fully qualified server name (or IP address) next to **Host** for an Azure SQL Managed Instance or SQL Server on Azure VM. To copy the server name or host name, hover over it and select the **Copy** icon.

> [!NOTE]  
> For connection information for SQL Server on Azure VM, see [Connect to a SQL Server instance](../virtual-machines/windows/sql-vm-create-portal-quickstart.md#connect-to-sql-server).

## Get ADO.NET connection information (optional - SQL Database only)

1. Navigate to the database pane in the Azure portal and, under **Settings**, select **Connection strings**.

1. Review the complete **ADO.NET** connection string.

   :::image type="content" source="media/connect-query-dotnet-core/adonet-connection-string2.png" alt-text="Screenshot showing the ADO.NET connection string." lightbox="media/connect-query-dotnet-core/adonet-connection-string2.png":::

1. Copy the **ADO.NET** connection string if you intend to use it.

## TLS considerations for database connectivity

Transport Layer Security (TLS) is used by all drivers that Microsoft supplies or supports for connecting to databases in Azure SQL Database or Azure SQL Managed Instance. No special configuration is necessary. For all connections to a SQL Server instance, a SQL pool in Azure Synapse Analytics, a database in Azure SQL Database, or an instance of Azure SQL Managed Instance, we recommend that the applications set the following connection parameters or their equivalents:

- `Encrypt = On`
- `TrustServerCertificate = Off`
- Optionally, `HostNameInCertificate = full-hostname-of-service` if the client uses a different address to connect and the TDS driver supports this option.

Some systems use different yet equivalent keywords for those configuration keywords. These configurations ensure that the client driver verifies the identity of the TLS certificate received from the server.

We also recommend that you disable TLS 1.1 and 1.0 on the client if you need to comply with Payment Card Industry - Data Security
Standard (PCI-DSS).

Non-Microsoft drivers might not use TLS by default. This can be a factor when connecting to Azure SQL Database or Azure SQL Managed Instance. Applications with embedded drivers might not allow you to control these connection settings. We recommend that you examine the security of such drivers and applications before using them on systems that interact with sensitive data.

<a id="libraries"></a>
<a id="data-access-frameworks"></a>

## Drivers and frameworks

Use the [Microsoft SQL drivers and frameworks](/sql/connect/sql-connection-libraries/) article to choose a driver or provider and, when applicable, a framework or data access library. To build an application that connects to Azure SQL Database or Azure SQL Managed Instance, use the language quickstarts in this article.

## Related content

- [Azure SQL Database and Azure Synapse Analytics connectivity architecture](connectivity-architecture.md)
- [Quickstart: Use .NET (C#) to query a database](connect-query-dotnet-core.md)
- [Quickstart: Use Go to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-go.md)
- [Quickstart: Use Node.js to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-nodejs.md)
- [Quickstart: Use PHP to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-php.md)
- [Quickstart: Use Python to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-python.md)
- [Quickstart: Use Ruby to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-ruby.md)
- [Use Java and JDBC with Azure SQL Database](connect-query-java.md)
- [Install sqlcmd and bcp the SQL Server command-line tools on Linux](/sql/linux/sql-server-linux-setup-tools)
- [sqlcmd](/sql/ssms/scripting/sqlcmd-use-the-utility)
- [Connect resiliently to SQL with ADO.NET](/sql/connect/ado-net/step-4-connect-resiliently-sql-ado-net)
- [Connect resiliently to SQL with PHP](/sql/connect/php/step-4-connect-resiliently-to-sql-with-php)
