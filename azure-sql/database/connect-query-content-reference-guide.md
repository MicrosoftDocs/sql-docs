---
title: Connect and Query
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Links to Azure SQL Database quickstarts showing how to connect to and query Azure SQL Database, and Azure SQL Managed Instance.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 01/14/2025
ms.service: azure-sql
ms.subservice: connect
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
ms.custom: [sqldbrb=1, sfi-image-nochange]
---

# Azure SQL Database and Azure SQL Managed Instance connect and query articles

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

The following document includes links to Azure examples showing how to connect and query Azure SQL Database and Azure SQL Managed Instance. For some related recommendations for Transport Level Security, see [TLS considerations for database connectivity](#tls-considerations-for-database-connectivity).

Watch this video in the [Azure SQL Database essentials series](/shows/azure-sql-database-essentials/) for a brief connect and query overview:

&nbsp;

> [!VIDEO https://learn-video.azurefd.net/vod/player?id=c2edd421-da6b-4598-a142-5980e4f38de9]

## Quickstarts

| Quickstart | Description |
| --- | --- |
| [SQL Server Management Studio (SSMS)](connect-query-ssms.md) | This quickstart demonstrates how to use SSMS to connect to a database, and then use Transact-SQL statements to query, insert, update, and delete data in the database. |
| [Azure portal](connect-query-portal.md) | This quickstart demonstrates how to use the [query editor](query-editor.md) to connect to a database (Azure SQL Database only), and then use Transact-SQL statements to query, insert, update, and delete data in the database. |
| [Visual Studio Code](connect-query-vscode.md) | This quickstart demonstrates how to use Visual Studio Code to connect to a database, and then use Transact-SQL statements to query, insert, update, and delete data in the database. |
| [.NET with Visual Studio](connect-query-dotnet-visual-studio.md) | This quickstart demonstrates how to use the .NET framework to create a C# program with Visual Studio to connect to a database and use Transact-SQL statements to query data. |
| [.NET core](connect-query-dotnet-core.md) | This quickstart demonstrates how to use .NET Core on Windows/Linux/macOS to create a C# program to connect to a database and use Transact-SQL statements to query data. |
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

## Libraries

You can use various libraries and frameworks to connect to Azure SQL Database or Azure SQL Managed Instance. You can then build an app by using SQL Server on Linux or Windows, or a SQL Server container on Linux.

The following table lists connectivity libraries or *drivers* that client applications can use from various languages to connect to and use SQL Server running on-premises or in the cloud. You can use them on Linux, Windows, or in containers, and use them to connect to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics.

| Language | Platform | Additional resources | Download | Get started |
| :--- | :--- | :--- | :--- | :--- |
| **C#** | Windows, Linux, macOS | [Microsoft ADO.NET for SQL Server and Azure SQL Database](/sql/connect/ado-net/microsoft-ado-net-sql-server) | [Download](https://dotnet.microsoft.com/download) | |
| **C++** | Windows, Linux, macOS | [Microsoft ODBC driver for SQL Server](/sql/connect/odbc/microsoft-odbc-driver-for-sql-server/) | [Download](/sql/connect/odbc/microsoft-odbc-driver-for-sql-server/) | |
| **Go** | Windows, Linux, macOS | [Microsoft Go driver for SQL Server](/sql/connect/golang/microsoft-go-mssqldb-driver) | [Install](/sql/connect/golang/microsoft-go-mssqldb-driver) | [Get started](connect-query-go.md) |
| **Java** | Windows, Linux, macOS | [Microsoft JDBC driver for SQL Server](/sql/connect/jdbc/microsoft-jdbc-driver-for-sql-server/) | [Download](/sql/connect/jdbc/download-microsoft-jdbc-driver-for-sql-server) | |
| **Node.js** | Windows, Linux, macOS | [Node.js driver for SQL Server](/sql/connect/node-js/node-js-driver-for-sql-server/) | [Install](/sql/connect/node-js/step-1-configure-development-environment-for-node-js-development/) | |
| **PHP** | Windows, Linux, macOS | [Microsoft Drivers for PHP for SQL Server](/sql/connect/php/microsoft-php-driver-for-sql-server) | [Download](/sql/connect/php/download-drivers-php-sql-server) | |
| **Python** | Windows, Linux, macOS | [mssql-python](/sql/connect/python/mssql-python/python-sql-driver-mssql-python) | [Install](/sql/connect/python/mssql-python/python-sql-driver-mssql-python-quickstart#install-the-mssql-python-package) | |
| **Ruby** | Windows, Linux, macOS | [Ruby driver for SQL Server](/sql/connect/ruby/ruby-driver-for-sql-server/) | [Install](/sql/connect/ruby/step-1-configure-development-environment-for-ruby-development/) | |

### Data-access frameworks

The following table lists examples of object-relational mapping (ORM) frameworks and web frameworks that client applications can use with SQL Server, Azure SQL Database, Azure SQL Managed Instance, or Azure Synapse Analytics. You can use the frameworks on Linux, Windows, or in containers.

| Language | Platform | ORM |
| :--- | :--- | :--- |
| **C#** | Windows, Linux, macOS | [Entity Framework](/ef); [Entity Framework Core](/ef/core/index) |
| **Go** | Windows, Linux, macOS | [GORM](https://gorm.io/) |
| **Java** | Windows, Linux, macOS | [Hibernate ORM](https://hibernate.org/orm) |
| **Node.js** | Windows, Linux, macOS | [Sequelize ORM](https://sequelize.org/) |
| **PHP** | Windows, Linux, macOS | [Laravel (Eloquent)](https://laravel.com/docs/eloquent); [Doctrine](https://www.doctrine-project.org/projects/orm.html) |
| **Python** | Windows, Linux, macOS | [Django](https://www.djangoproject.com/) |
| **Ruby** | Windows, Linux, macOS | [Ruby on Rails](https://rubyonrails.org/) |

## Related content

- [Azure SQL Database and Azure Synapse Analytics connectivity architecture](connectivity-architecture.md)
- [SQL Server drivers](/sql/connect/sql-connection-libraries/)
- [Quickstart: Use .NET (C#) to query a database](connect-query-dotnet-core.md)
- [Quickstart: Use Golang to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-go.md)
- [Quickstart: Use Node.js to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-nodejs.md)
- [Quickstart: Use PHP to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-php.md)
- [Quickstart: Use Python to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-python.md)
- [Quickstart: Use Ruby to query a database in Azure SQL Database or Azure SQL Managed Instance](connect-query-ruby.md)
- [Use Java and JDBC with Azure SQL Database](connect-query-java.md)
- [Install sqlcmd and bcp the SQL Server command-line tools on Linux](/sql/linux/sql-server-linux-setup-tools)
- [sqlcmd](/sql/ssms/scripting/sqlcmd-use-the-utility)
- [Connect resiliently to SQL with ADO.NET](/sql/connect/ado-net/step-4-connect-resiliently-sql-ado-net)
- [Connect resiliently to SQL with PHP](/sql/connect/php/step-4-connect-resiliently-to-sql-with-php)
