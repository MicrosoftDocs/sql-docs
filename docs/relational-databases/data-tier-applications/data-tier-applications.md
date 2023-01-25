---
description: "A data-tier application (DAC) is a logical database entity that defines all of the SQL Server objects - such as tables, views, and instance objects, including logins - associated with a database."
title: "Data-tier applications (DAC)"
ms.custom: ""
ms.date: 7/12/2022
ms.service: sql
ms.reviewer: dzsquared
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
    - "designing DACs"
    - "How to [DAC]"
    - "data-tier application [SQL Server], designing"
    - "wizard [DAC]"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Data-tier applications (DAC)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
A data-tier application (DAC) is a logical database entity that defines all of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects - such as tables, views, and instance objects, including logins - associated with a user's database. A DAC is a self-contained unit of the entire database model and is portable in an artifact known as a DAC package, or *.dacpac*. [Tooling support](#dac-tools) for data-tier applications enables developers and database administrators to deploy dacpacs to new or existing databases. Deployments to an existing database updates the database model from the existing state to match the contents of the dacpac. Developers build DACs from SQL database projects, a declarative development concept for building SQL objects that enables source control on the database schema.

A *.bacpac* is a related artifact that by default encapsulates the database schema and the data stored in the database. The primary use case for a BACPAC is to move a database from one server to another - or to [migrate a database from a local server to the cloud](/azure/azure-sql/database/migrate-to-database-from-sql-server) - and archiving an existing database in an open format.

## Benefits of data-tier applications
The lifecycle of a database application may involve developers and DBAs exchanging scripts and sharing single use integration notes for application update activities. While this process is acceptable in some circumstances, it can be difficult to integrate with DevOps pipelines and general development processes.

Data-tier applications enable declarative database development, simplifying the development process and providing a more consistent and predictable development experience. A developer can author a database with SQL database projects in their choice of integrated development environment (IDE). A SQL database project can be compiled to a DAC package locally or in a DevOps pipeline. The DAC package is in turn deployed to a test, staging or production database through an automated process or manually with a CLI or GUI tool. The *.dacpac* can be used to update a database with new or modified objects, to revert to a previous version of the database, or to provision an entirely new database. Conversely, a *.dacpac* can be generated from an existing database and used to establish a SQL database project based on the current database schema.

The advantage of a DAC-driven deployment over a migration-driven process is that the process enables the identification and validation of behaviors from different source and target databases. Tooling used during database deployment/upgrades has options to flag risky actions such as column size changes that might cause data loss and the ability to directly script the upgrade plan. This plan can be evaluated manually before proceeding with the update.

## Operations

A DAC simplifies the development, deployment, and management of data-tier elements that support an application.

### DACPAC

A DAC supports the following operations:

- **EXTRACT** - the user can extract a database into a *.dacpac*. For more information, see [SqlPackage extract](../../tools/sqlpackage/sqlpackage-extract.md) and [Extract a DAC From a Database](../../relational-databases/data-tier-applications/extract-a-dac-from-a-database.md).

- **DEPLOY**/**PUBLISH** - the user can deploy a *.dacpac* to a host server. When the deployment is done to an existing database, the difference between the database and the DAC is applied to the database as an object update operation.  The term "publish" is often used interchangeably with "deploy". For more information, see [SqlPackage publish](../../tools/sqlpackage/sqlpackage-publish.md), [Deploy a Data-tier Application](../../relational-databases/data-tier-applications/deploy-a-data-tier-application.md), and [Deploy a Database By Using a DAC](../../relational-databases/data-tier-applications/deploy-a-database-by-using-a-dac.md).

These capabilities can be found in SqlPackage, SQL Server Management Studio, Azure Data Studio, and SQL Server Data Tools.

### SQL database projects
A SQL project supports the following operations:

- **BUILD** - the user can build a SQL database project into a *.dacpac*.

- **PUBLISH** - the user can publish a SQL database project to a host server.

- **EXTRACT** - the user can extract a database into a SQL database project.

These capabilities can be found in Azure Data Studio, Visual Studio Code, and SQL Server Data Tools.

### BACPAC
A *.bacpac*, on the other hand, is focused on capturing schema and data supporting two main operations:

- **EXPORT**- The user can export the schema and the data of a database to a *.bacpac*. For more information, see [SqlPackage export](../../tools/sqlpackage/sqlpackage-export.md) and [Export a Data-tier Application](../../relational-databases/data-tier-applications/export-a-data-tier-application.md).

- **IMPORT** - The user can import the schema and the data into a new database. For more information, see [SqlPackage import](../../tools/sqlpackage/sqlpackage-import.md) and [Import a BACPAC File to Create a New User Database](../../relational-databases/data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md).

These capabilities are supported by the tools SqlPackage, SQL Server Management Studio, Azure Data Studio, and the Azure portal.


## DAC tools
Data-tier application artifacts and SQL projects can be used across multiple tools. These tools address the requirements of different user personas.

### DACPAC and BACPAC
The following tools support the DAC package and BAC package format:
- [SqlPackage CLI](../../tools/sqlpackage/sqlpackage.md)
- [SQL Server Management Studio](../../ssms/sql-server-management-studio-ssms.md)
- [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md)

In these tools, a database can be extracted to a *.dacpac* or exported to a *.bacpac*. Conversely, a *.bacpac* can be imported into a new database or a *.dacpac* can be published to a new or existing database.

### DACPAC and SQL projects
The following tools support the DAC package format in addition to providing editing of SQL database projects:
- [SQL Server Data Tools in Visual Studio](../../ssdt/sql-server-data-tools.md)
- [Azure Data Studio](../../azure-data-studio/extensions/sql-database-project-extension.md)
- [Visual Studio Code (VS Code)](../../azure-data-studio/extensions/sql-database-project-extension.md)

In these tools, developers can design a database in an unconnected, client-side development environment. The tools can be used to create a DAC package, deploy a DAC package to a database, and import a database package into a SQL project.


## DAC concepts

### Version support

In general, DAC tools are capable of reading *.dacpac* files generated by DAC tools from previous [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] versions, and can also deploy DAC packages to previous versions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. However, DAC tools from earlier versions can't read *.dacpac* files generated by DAC tools from later versions. At a minimum, DAC tools support the in-support versions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] at the time of their release.

### File format
A *.dacpac* is a compressed folder with a .dacpac extension, and similarly a *.bacpac* is a compressed folder with a .bacpac extension. An advanced user can unpack the file to view the multiple XML sections representing details of the origin, the objects in the database, and other characteristics. To unpack a *.dacpac* or *.bacpac*, replace the file extension with *.zip* and use a file compression utility to unzip the file.


### Data-tier application registration
In SQL Server Management Studio other actions can be taken on a database to register it as a data-tier application.

- **REGISTER** - the user can register a database as a data-tier application.

- **UNREGISTER** - a database previously registered as a DAC can be unregistered.

- **UPGRADE** - a database can be upgraded using a *.dacpac*.

For more information about these actions, see the below tasks.

|Task|Article link|
|----------------------|-----------|
|Describes how to use a new DAC package file to upgrade an instance to a new version of the DAC.|[Upgrade a Data-tier Application](../../relational-databases/data-tier-applications/upgrade-a-data-tier-application.md)|
|Describes how to remove a DAC instance. You can choose to also detach or drop the associated database, or leave the database intact.|[Delete a Data-tier Application](../../relational-databases/data-tier-applications/delete-a-data-tier-application.md)|
|Describes how to view the health of currently deployed DACs by using the SQL Server Utility.|[Monitor Data-tier Applications](../../relational-databases/data-tier-applications/monitor-data-tier-applications.md)|
|Describes how to promote an existing database to be a DAC instance. A DAC definition is built and stored in the system databases.|[Register a Database As a DAC](../../relational-databases/data-tier-applications/register-a-database-as-a-dac.md)|
|Describes how to review the contents of a DAC package and the actions a DAC upgrade will perform before using the package in a production system.|[Validate a DAC Package](../../relational-databases/data-tier-applications/validate-a-dac-package.md)|


## Next steps
- [SqlPackage CLI](../../tools/sqlpackage/sqlpackage.md)
- [DAC Support For SQL Server Objects and Versions](/previous-versions/sql/sql-server-2012/ee210549(v=sql.110))

