---
title: Extract, transform, and load data on Linux with SSIS | Microsoft Docs
description: This article describes SQL Server Integration Services (SSIS) for Linux computers
author: leolimsft 
ms.author: lle 
ms.reviewer: douglasl
manager: craigg
ms.date: 01/09/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
---
# Extract, transform, and load data on Linux with SSIS

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article describes how to run SQL Server Integration Services (SSIS) packages on Linux. SSIS solves complex data integration problems by extracting data from multiple sources and formats, transforming and cleansing the data, and loading the data into multiple destinations. 

SSIS packages running on Linux can connect to Microsoft SQL Server running on Windows on-premises or in the cloud, on Linux, or in Docker. They can also connect to Azure SQL Database, Azure SQL Data Warehouse, ODBC data sources, flat files, and other data sources including ADO.NET sources, XML files, and OData services.

For more info about the capabilities of SSIS, see [SQL Server Integration Services](../integration-services/sql-server-integration-services.md).

## Prerequisites

To run SSIS packages on a Linux computer, first you have to install SQL Server Integration Services. SSIS is not included in the installation of SQL Server for Linux computers. For installation instructions, see [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md).

You also have to have a Windows computer to create and maintain packages. The SSIS design and management tools are Windows applications that are not currently available for Linux computers. 

## Run an SSIS package

To run an SSIS package on a Linux computer, do the following things:

1.  Copy the SSIS package to the Linux computer.
2.  Run the following command:
    ```
    $ dtexec /F \<package name \> /DE <protection password>
    ```

## Run an encrypted (password-protected) package
There are three ways to run an SSIS package that's encrypted with a password:

1.  Set the value of the environment variable `SSIS_PACKAGE_DECRYPT`, as shown in the following example:

    ```
    SSIS_PACKAGE_DECRYPT=test /opt/ssis/bin/dtexec /f package.dtsx
    ```

2.  Specify the `/de[crypt]` option to enter the password interactively, as shown in the following example:

    ```
    /opt/ssis/bin/dtexec /f package.dtsx /de
    
    Enter decryption password:
    ```

3.  Specify the `/de` option to provide the password on the command line, as shown in the following example. This method is not recommended because it stores the decryption password with the command in the command history.

    ```
    opt/ssis/bin/dtexec /f package.dtsx /de test
    
    Warning: Using /De[crypt] <password> may store decryption password in command history.
    
    You can use /De[crypt] instead to enter interactive mode,
    or use environment variable SSIS_PACKAGE_DECRYPT to set decryption password.
    ```

## Design packages

**Connect to ODBC data sources**. With SSIS on Linux CTP 2.1 Refresh and later, SSIS packages can use ODBC connections on Linux. This functionality has been tested with the SQL Server and the MySQL ODBC drivers, but is also expected to work with any Unicode ODBC driver that observes the ODBC specification. At design time, you can provide either a DSN or a connection string to connect to the ODBC data; you can also use Windows authentication. For more info, see the [blog post announcing ODBC support on Linux](https://blogs.msdn.microsoft.com/ssis/2017/06/16/odbc-is-supported-in-ssis-on-linux-ssis-helsinki-ctp2-1-refresh/).

**Paths**. Provide Windows-style paths in your SSIS packages. SSIS on Linux does not support Linux-style paths, but maps Windows-style paths to Linux-style paths at run time. Then, for example, SSIS on Linux maps the Windows-style path `C:\test` to the Linux-style path `/test`.

## Deploy packages
You can only store packages in the file system on Linux in this release. The SSIS Catalog database and the legacy SSIS service are not available on Linux for package deployment and storage.

## Schedule packages
You can use Linux system scheduling tools such as `cron` to schedule packages. You can't use SQL Agent on Linux to schedule package execution in this release. For more info, see [Schedule SSIS packages on Linux with cron](sql-server-linux-schedule-ssis-packages.md).

## Limitations and known issues

For detailed info about the limitations and known issues of SSIS on Linux, see [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md).

## More info about SSIS on Linux

For more info about SSIS on Linux, see the following blog posts:

-   [SSIS on Linux is available in SQL Server CTP2.1](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/)
-   [ODBC is supported in SSIS on Linux (SQL Server CTP 2.1 refresh)](https://blogs.msdn.microsoft.com/ssis/2017/06/16/odbc-is-supported-in-ssis-on-linux-ssis-helsinki-ctp2-1-refresh/)

## More info about SSIS

Microsoft SQL Server Integration Services (SSIS) is a platform for building high-performance data integration solutions, including extraction, transformation, and loading (ETL) packages for data warehousing. For more info about SSIS, see [SQL Server Integration Services](/sql/integration-services/sql-server-integration-services).

SSIS includes the following features:
- Graphical tools and wizards for building and debugging packages on Windows
- A variety of tasks for performing workflow functions such as FTP operations, executing SQL statements, and sending e-mail messages
- A variety of data sources and destinations for extracting and loading data
- A variety of transformations for cleaning, aggregating, merging, and copying data
- Application programming interfaces (APIs) for extending SSIS with your own custom scripts and components

To get started with SSIS, download the latest version of [SQL Server Data Tools (SSDT)](../integration-services/ssis-how-to-create-an-etl-package.md).

To learn more about SSIS, see the following articles:
- [Learn more about SQL Server Integration Services](../integration-services/sql-server-integration-services.md)
- [SQL Server Integration Services (SSIS) Development and Management Tools](../integration-services/integration-services-ssis-development-and-management-tools.md)
- [SQL Server Integration Services Tutorials](../integration-services/integration-services-tutorials.md)

## Related content about SSIS on Linux
-   [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
-   [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md)
-   [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md)
-   [Schedule SQL Server Integration Services package execution on Linux with cron](sql-server-linux-schedule-ssis-packages.md)
