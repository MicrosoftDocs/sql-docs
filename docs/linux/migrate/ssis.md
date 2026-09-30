---
title: Extract, Transform, and Load Data on Linux with SSIS
description: Learn how to run SQL Server Integration Services (SSIS) packages on Linux. Also learn where to find more information about the capabilities of SSIS.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: lle, amitkh, atsingh, maghan
ms.date: 11/18/2025
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
  - ignite-2025
---
# Extract, transform, and load data on Linux with SSIS

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article describes how to run SQL Server Integration Services (SSIS) packages on Linux. SSIS solves complex data integration problems by extracting data from multiple sources and formats, transforming and cleansing the data, and loading the data into multiple destinations.

SSIS packages running on Linux can connect to Microsoft SQL Server running on Windows on-premises or in the cloud, on Linux, or in Docker. They can also connect to Azure SQL Database, Azure Synapse Analytics, ODBC data sources, flat files, and other data sources including ADO.NET sources, XML files, and OData services.

For more info about the capabilities of SSIS, see [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md).

## Prerequisites

To run SSIS packages on a Linux computer, first you have to install SQL Server Integration Services. SSIS isn't included in the installation of SQL Server for Linux computers. For installation instructions, see [Install SQL Server Integration Services (SSIS) on Linux](../install-upgrade/setup-ssis.md).

You also have to have a Windows computer to create and maintain packages. The SSIS design and management tools are Windows applications that aren't currently available for Linux computers.

## Run an SSIS package

To run an SSIS package on a Linux computer, do the following things:

1. Copy the SSIS package to the Linux computer.
1. Run the following command:

   ```bash
   dtexec /F <package name>
   ```

## Run an encrypted (password-protected) package

There are three ways to run an SSIS package that's encrypted with a password:

1. Set the value of the environment variable `SSIS_PACKAGE_DECRYPT`, as shown in the following example:

   ```bash
   SSIS_PACKAGE_DECRYPT=test /opt/ssis/bin/dtexec /f package.dtsx
   ```

1. Specify the `/de[crypt]` option to enter the password interactively, as shown in the following example:

   ```bash
   /opt/ssis/bin/dtexec /f package.dtsx /de

   Enter decryption password:
   ```

1. Specify the `/de` option to provide the password on the command line, as shown in the following example. This method isn't recommended because it stores the decryption password with the command in the command history.

   ```bash
   /opt/ssis/bin/dtexec /f package.dtsx /de test

   Warning: Using /De[crypt] <password> may store decryption password in command history.

   You can use /De[crypt] instead to enter interactive mode,
   or use environment variable SSIS_PACKAGE_DECRYPT to set decryption password.
   ```

## Design packages

**Connect to ODBC data sources**. SSIS packages can use ODBC connections on Linux. This functionality has been tested with the SQL Server and the MySQL ODBC drivers, but is also expected to work with any Unicode ODBC driver that observes the ODBC specification. At design time, you can provide either a DSN or a connection string to connect to the ODBC data; you can also use Windows authentication. For more info, see the [blog post announcing ODBC support on Linux](https://techcommunity.microsoft.com/blog/ssis/odbc-is-supported-in-ssis-on-linux-sql-server-2017-ctp-2-1-refresh/388346).

**Paths**. Provide Windows-style paths in your SSIS packages. SSIS on Linux doesn't support Linux-style paths, but maps Windows-style paths to Linux-style paths at run time. Then, for example, SSIS on Linux maps the Windows-style path `C:\test` to the Linux-style path `/test`.

## Deploy packages

You can only store packages in the file system on Linux. The SSIS Catalog database and the legacy SSIS service aren't available on Linux for package deployment and storage.

## Schedule packages

You can use Linux system scheduling tools such as `cron` to schedule packages. You can't use SQL Server Agent on Linux to schedule package execution. For more info, see [Schedule SQL Server Integration Services package execution on Linux with cron](schedule-ssis-packages.md).

## Limitations and known issues

For detailed info about the limitations and known issues of SSIS on Linux, see [Feature support and considerations for SQL Server Integration Services (SSIS) on Linux](ssis-known-issues.md).

## More info about SSIS

Microsoft SQL Server Integration Services (SSIS) is a platform for building high-performance data integration solutions, including extraction, transformation, and loading (ETL) packages for data warehousing. For more info about SSIS, see [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md).

SSIS includes the following features:

- Graphical tools and wizards for building and debugging packages on Windows
- A variety of tasks for performing workflow functions such as FTP operations, executing SQL statements, and sending e-mail messages
- A variety of data sources and destinations for extracting and loading data
- A variety of transformations for cleaning, aggregating, merging, and copying data
- Application programming interfaces (APIs) for extending SSIS with your own custom scripts and components

To get started with SSIS, follow the [SSIS How to Create an ETL Package](../../integration-services/ssis-how-to-create-an-etl-package.md) tutorial.

To learn more about SSIS, see the following articles:

- [SQL Server Integration Services](../../integration-services/sql-server-integration-services.md)
- [Integration Services (SSIS) Development and Management Tools](../../integration-services/integration-services-ssis-development-and-management-tools.md)
- [Integration Services Tutorials](../../integration-services/integration-services-tutorials.md)

## Related content

- [Install SQL Server Integration Services (SSIS) on Linux](../install-upgrade/setup-ssis.md)
- [Configure SQL Server Integration Services on Linux with ssis-conf](configure-ssis.md)
- [Feature support and considerations for SQL Server Integration Services (SSIS) on Linux](ssis-known-issues.md)
- [Schedule SQL Server Integration Services package execution on Linux with cron](schedule-ssis-packages.md)
