---
title: Connectivity libraries and frameworks
description: Get the connectivity drivers for client apps to connect to Azure SQL, Microsoft SQL Server, running on-premises, in the cloud, on Linux or Windows, or in containers.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/11/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Connectivity libraries and frameworks for Microsoft SQL Server

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Check out the [Getting Started guides](../connect/sql-data-developer.md) to quickly get started with programming languages such as C#, Java, Node.js, PHP, and Python and build an app using [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, Windows, or Docker on macOS.

The following table lists connectivity libraries or *drivers* that client applications can use from various languages to connect to and use Microsoft [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on-premises or in the cloud, on Linux, Windows, or Docker, and also to Azure SQL Database and Azure Synapse Analytics.

| Language | Platform | Additional resources | Download | Get started |
| :-- | :-- | :-- | :-- | :-- |
| C# | Windows, Linux, macOS | [Microsoft ADO.NET for SQL Server](../connect/ado-net/microsoft-ado-net-sql-server.md) | [Download](https://msdn.microsoft.com/vstudio/aa496123.aspx) | [Get started](../connect/ado-net/microsoft-ado-net-sql-server.md) |
| Java | Windows, Linux, macOS | [Microsoft JDBC Driver for SQL Server](../connect/jdbc/microsoft-jdbc-driver-for-sql-server.md) | | [Get started](../connect/jdbc/microsoft-jdbc-driver-for-sql-server.md) |
| PHP | Windows, Linux, macOS | [PHP SQL Driver for SQL Server](../connect/php/microsoft-php-driver-for-sql-server.md) | Operating System:<br /><br />- [Windows](https://www.microsoft.com/download/details.aspx?id=20098)<br />- [Linux](https://github.com/Microsoft/msphpsql/tree/dev#install-unix)<br />\- [macOS](https://github.com/Microsoft/msphpsql/tree/dev#install-unix) | [Get started](../connect/php/microsoft-php-driver-for-sql-server.md) |
| Node.js | Windows, Linux, macOS | [Node.js Driver for SQL Server](../connect/node-js/node-js-driver-for-sql-server.md) | | [Get started](../connect/node-js/node-js-driver-for-sql-server.md) |
| Python | Windows, Linux, macOS | [Python SQL Driver](../connect/python/python-driver-for-sql-server.md)<br /><br />- [pyodbc](../connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development.md) | | [Get started](../connect/python/python-driver-for-sql-server.md) |
| Ruby | Windows, Linux, macOS | [Ruby Driver for SQL Server](../connect/ruby/ruby-driver-for-sql-server.md) | | [Get started](../connect/ruby/ruby-driver-for-sql-server.md) |
| C++ | Windows, Linux, macOS | [Microsoft ODBC Driver for SQL Server](../connect/odbc/microsoft-odbc-driver-for-sql-server.md) | [Download](../connect/odbc/microsoft-odbc-driver-for-sql-server.md) | |

The following table lists a few examples of Object Relational Mapping (ORM) frameworks and web frameworks, that client applications can use with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on-premises or in the cloud, on Linux, Windows, or Docker, and also to Azure SQL Database and Azure Synapse Analytics.

| Language | Platform | ORM |
| :-- | :-- | :-- |
| C# | Windows, Linux, macOS | [Entity Framework](/ef)<br />[Entity Framework Core](/ef/core/index) |
| Java | Windows, Linux, macOS | [Hibernate ORM](https://hibernate.org/orm) |
| PHP | Windows, Linux | [Laravel (Eloquent)](https://laravel.com/docs/5.0/eloquent) |
| Node.js | Windows, Linux, macOS | [Sequelize ORM](http://sequelize.org/) |
| Python | Windows, Linux, macOS | [Django](https://www.djangoproject.com/) |
| Ruby | Windows, Linux, macOS | [Ruby on Rails](https://rubyonrails.org/) |

## Related content

- [SQL Server Drivers](../connect/sql-connection-libraries.md)
