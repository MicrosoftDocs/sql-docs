---
title: Microsoft SQL Drivers and Frameworks
description: Choose a driver, provider, framework, or data access library for applications that connect to Microsoft SQL products.
author: David-Engel
ms.author: davidengel
ms.reviewer: vanto, randolphwest
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---
# Microsoft SQL drivers and frameworks

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Synapse Analytics FabricSQLDB](../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Applications use a driver or data provider to connect to Microsoft SQL products. Many applications also use a framework, object-relational mapper (ORM), or other data access library on top of the driver.

- A **driver or provider** handles network communication, authentication, commands, transactions, parameters, and results.
- A **framework or data access library** uses a driver or provider and adds capabilities such as object mapping, query generation, change tracking, migrations, or integration with an application framework.

Choose the driver or provider for your language and platform first. Then, if your application needs a higher-level programming model, choose a compatible framework or library. A framework doesn't replace the underlying driver.

Support for the driver and support for the framework are separate. Microsoft support for a driver doesn't include support for a third-party framework that uses the driver.

<a id="anchor-20-drivers-relational-access"></a>

## Choose a driver or provider

The following table lists the primary drivers and providers covered in this documentation. Review the linked product documentation for supported operating systems, runtime versions, SQL products, authentication methods, and features.

| Language or platform | Driver or provider | Ownership and support |
| --- | --- | --- |
| .NET | [Microsoft.Data.SqlClient](ado-net/microsoft-ado-net-sql-server.md) | Microsoft provider for new .NET development. |
| C and C++ | [Microsoft ODBC Driver for SQL Server](odbc/microsoft-odbc-driver-for-sql-server.md) | Microsoft ODBC driver. |
| C and C++ | [Microsoft OLE DB Driver for SQL Server](oledb/oledb-driver-for-sql-server.md) | Microsoft OLE DB provider. |
| Go | [go-mssqldb](golang/microsoft-go-mssqldb-driver.md) | Official Microsoft Go driver that implements the standard `database/sql` interfaces. |
| Java | [Microsoft JDBC Driver for SQL Server](jdbc/microsoft-jdbc-driver-for-sql-server.md) | Microsoft JDBC driver. |
| Node.js | [Tedious](node-js/node-js-driver-for-sql-server.md) | Community-supported driver. Microsoft contributes to the project, but it doesn't come with Microsoft support. |
| PHP | [Microsoft Drivers for PHP for SQL Server](php/microsoft-php-driver-for-sql-server.md) | Microsoft SQLSRV and PDO_SQLSRV extensions. The extensions use Microsoft ODBC Driver for SQL Server. |
| Python | [mssql-python](python/mssql-python/python-sql-driver-mssql-python.md) | Microsoft Python driver. |
| Ruby | [TinyTDS](ruby/ruby-driver-for-sql-server.md) | Community-supported driver. Ruby and TinyTDS don't come with Microsoft support. |

For Apache Spark, use the built-in Spark JDBC data source with [Microsoft JDBC Driver for SQL Server](jdbc/microsoft-jdbc-driver-for-sql-server.md). The Microsoft SQL Spark connector is [archived](spark/connector.md) and isn't under active development.

<a id="anchor-40-drivers-orm-access"></a>

## Choose a framework or data access library

The following table lists frameworks and data access libraries that support Microsoft SQL products through a provider, adapter, or database dialect. The list isn't exhaustive.

| Language | Framework or library | Microsoft SQL integration | Ownership and support |
| --- | --- | --- | --- |
| .NET | [Entity Framework Core SQL Server provider](/ef/core/providers/sql-server/) | `Microsoft.Data.SqlClient` | Microsoft-maintained provider in Entity Framework Core. |
| .NET | [Entity Framework 6 SQL Server provider](/ef/ef6/what-is-new/microsoft-ef6-sqlserver) | `Microsoft.Data.SqlClient` | Microsoft provider for existing Entity Framework 6 applications. |
| .NET | [Dapper](https://github.com/DapperLib/Dapper) | Extends ADO.NET `DbConnection`; use with `Microsoft.Data.SqlClient`. | Third-party micro-ORM. |
| Go | [GORM](golang/use-go-orm-library-with-go-mssqldb.md) | `go-mssqldb` | Third-party ORM. The linked article shows how to use its Microsoft SQL dialect. |
| Go | [sqlx](golang/use-go-sql-extensions-library-with-go-mssqldb.md) | `go-mssqldb` | Third-party library that extends `database/sql` with mapping and query helpers. |
| Java | [Hibernate ORM](https://docs.jboss.org/hibernate/orm/current/javadocs/org/hibernate/dialect/SQLServerDialect.html) | Microsoft SQL dialect over JDBC; use with Microsoft JDBC Driver for SQL Server. | Third-party ORM. |
| Node.js and TypeScript | [Prisma ORM](https://www.prisma.io/docs/orm/v7/core-concepts/supported-databases/sql-server) | Built-in `sqlserver` provider; Prisma 7 can use its `node-mssql` driver adapter. | Third-party ORM. |
| Node.js and TypeScript | [Sequelize](https://sequelize.org/docs/v7/databases/mssql/) | Microsoft SQL dialect based on Tedious. | Third-party ORM. |
| Node.js and TypeScript | [TypeORM](https://github.com/typeorm/typeorm/blob/master/docs/docs/drivers/microsoft-sqlserver.md) | Microsoft SQL data source based on `node-mssql` and Tedious. | Third-party ORM. |
| PHP | [Laravel Eloquent](https://laravel.com/docs/database) | Microsoft SQL connection through SQLSRV and PDO_SQLSRV. | Third-party web framework and ORM. |
| PHP | [Doctrine DBAL and ORM](https://www.doctrine-project.org/projects/doctrine-dbal/en/latest/reference/configuration.html) | Microsoft SQL connection through the `pdo_sqlsrv` or `sqlsrv` driver. | Third-party database abstraction layer and ORM. |
| Python | [mssql-django](python/mssql-django/python-sql-driver-mssql-django.md) | `mssql-python` or `pyodbc` in mssql-django 2.0. | Microsoft Django database backend. |
| Python | [SQLAlchemy](python/mssql-python/sqlalchemy-integration.md) | Depends on the selected Microsoft SQL dialect. | Third-party toolkit and ORM. Review the linked integration guidance and its version requirements. |
| Ruby | [Active Record SQL Server Adapter](https://github.com/rails-sqlserver/activerecord-sqlserver-adapter) | Microsoft SQL adapter for Ruby on Rails based on TinyTDS. | Community-supported adapter. |

When you evaluate a framework or library:

- Confirm that its provider, adapter, or dialect supports the Microsoft SQL product, authentication method, and features your application needs.
- Check the framework's maintenance status, release policy, and support channel.
- For connectivity problems, reproduce the issue with the underlying driver when possible. A direct-driver reproduction helps determine whether the issue belongs to the driver or the higher-level framework.

## Related content

- [Driver feature support matrix](driver-feature-matrix.md)
- [History of Microsoft SQL connectivity](connect-history.md)
