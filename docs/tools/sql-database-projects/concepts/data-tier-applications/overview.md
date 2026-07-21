---
title: Data-Tier Applications (DAC) Overview
description: Data-tier applications, or dacpacs, represent a database model.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier, wiassaf, maghan, tsiddique
ms.date: 03/13/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ms.collection:
  - data-tools
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql.data.tools.DacTableChooser"
  - "sql.data.tools.DacPublishDialog"
  - "sql.data.tools.DacPropertiesDialog"
  - "sql.data.tools.DacExtractDialog"
helpviewer_keywords:
  - "designing DACs"
  - "How to [DAC]"
  - "data-tier application [SQL Server], designing"
  - "wizard [DAC]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Data-tier applications (DAC) overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

A data-tier application (DAC) is a logical database entity that defines all of the [!INCLUDE [ssNoVersion](../../../../includes/ssnoversion-md.md)] objects - such as tables, views, and instance objects, including logins - associated with a user's database. A data-tier application is a self-contained unit of the entire database model and is portable in both `.dacpac` and `.bacpac` packages. [Tooling support](#data-tier-application-tools) for data-tier applications enables developers and database administrators to apply `.dacpac` and `.bacpac` files to new or existing databases or generate new files from existing databases.

## Operations

### BACPAC operations

The `.bacpac` file format is a related artifact that by default encapsulates the database schema and the data stored in the database. Objects in the `.bacpac` database model are limited to the surface area of Azure SQL Database. The primary use case for a `.bacpac` is to move a database from one server to another - or to [migrate a database from a local server to the cloud](/azure/azure-sql/database/migrate-to-database-from-sql-server) - and archiving an existing database in an open format.

- **Export** - the user can export a database to a `.bacpac` file. For more information, see [SqlPackage export](../../../sqlpackage/sqlpackage-export.md) and [Export a BACPAC file](export-bacpac-file.md).
- **Import** - the user can import a `.bacpac` file into a new database. For more information, see [SqlPackage import](../../../sqlpackage/sqlpackage-import.md) and [Import a BACPAC file to create a new database](import-bacpac-file-create-new-database.md).

Learn more about database portability from the [SqlPackage portability documentation](../../../sqlpackage/sqlpackage.md#portability).

### DACPAC operations

The `.dacpac` data-tier application package is the build artifact from [SQL database projects](../../sql-database-projects.md) You can use it as part of a comprehensive database lifecycle management and DevOps strategy. Data isn't included in a `.dacpac` by default, but you can choose to include data from user tables when you extract a `.dacpac` from a live SQL Server or Azure SQL Database. As an integral part of the SQL database project workflow and database development lifecycle, `.dacpac` files are used in several operations. The primary operations are:

- **Extract** - extract a database into a `.dacpac`. For more information, see [SqlPackage extract](../../../sqlpackage/sqlpackage-extract.md) and [Extract a DACPAC from a database](extract-dacpac-from-database.md).
- **Deploy**/**Publish** - deploy a `.dacpac` to a host server. When you deploy to an existing database, the difference between the database and the DAC is dynamically calculated and applied as an incremental update. The term *publish* is often used interchangeably with *deploy*. For more information, see [SqlPackage publish](../../../sqlpackage/sqlpackage-publish.md) and [Deploy a data-tier application](deploy-data-tier-application.md).

You can find these capabilities in the SqlPackage CLI, SQL Server Management Studio, Visual Studio Code, and SQL Server Data Tools.

In addition to publish and extract, you can also track the database model in the system metadata by utilizing the *dac registration* functionality:

- **Register** - register a database as a data-tier application. Register stores a representation of the current state of the database schema in system metadata.
- **Unregister** - unregister a database previously registered as a DAC.
- **Upgrade** - upgrade a database by using a `.dacpac`.

## Data-tier application tools

Tooling support for data-tier applications enables developers and database administrators to work with `.dacpac` and `.bacpac` files from both graphical and command line interfaces. In addition to released tools, data-tier application APIs are available in the [Data-tier Application Framework (DACFx)](https://www.nuget.org/packages/Microsoft.SqlServer.DacFx/) for .NET development and database lifecycle customization.

### DACPAC and BACPAC packages

> [!IMPORTANT]  
> Protect your `.bacpac` and `.dacpac` files by securing them appropriately. The data contained in these files is compressed but not encrypted. `.bacpac` files contain the data from a database by default, and a `.dacpac` can contain data when the option is specified during extract.

The following tools support the `.dacpac` and `.bacpac` formats:

- [SqlPackage CLI](../../../sqlpackage/sqlpackage.md)
- [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms)
- [Data-tier Application (DACPAC and BACPAC) import and export](../../../visual-studio-code-extensions/mssql/mssql-data-tier-application.md)

In these tools, you can extract a database to a `.dacpac` or export it to a `.bacpac`. Conversely, you can import a `.bacpac` into a new database, or publish a `.dacpac` to a new or existing database.

### DACPAC and SQL projects

The following tools support the `.dacpac` file format and provide editing capabilities for SQL database projects:

- [SQL Server Data Tools](../../../../ssdt/sql-server-data-tools.md)
- [SQL Database Projects extension](../../../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)
- [Data-tier Application (DACPAC and BACPAC) import and export](../../../visual-studio-code-extensions/mssql/mssql-data-tier-application.md)

Developers can use these tools to design a database in an unconnected, client-side development environment. For more information, see the [SQL projects tools](../../sql-projects-tools.md) article.

## Related content

- [SqlPackage](../../../sqlpackage/sqlpackage.md)
