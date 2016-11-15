---
# required metadata

title: Release notes for SQL Server on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: This topic contains the release notes and supported features for SQL Server vNext CTP1 running on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/09/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 1314744f-fcaf-46db-800e-2918fa7e1b6c

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Release notes for SQL Server on Linux
The following release notes apply to SQL Server vNext CTP1 running on Linux. This release supports many of the SQL Server database engine features for Linux. See the information in the following sections for supported platforms, tools, features, and known issues.
    - SQL Server Engine version: 14.0.1.246
    - RPM package version: 14.0.1.246-6. 
    - Debian package version: 14.0.1.246-6

Download the [mssql-server 14.0.1.246-6 Engine Debian package](https://preview.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.1.246-6_amd64.deb)
Download the [mssql-server 14.0.1.246-6 High Availability Debian package](https://preview.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.1.246-6_amd64.deb)
Download the [mssql-server 14.0.1.246-6 Engine RPM package](https://preview.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.1.246-6.x86_64.rpm)
Download the [mssql-server 14.0.1.246-6 High Availability RPM package](https://preview.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.1.246-6.x86_64.rpm)

## Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Ubuntu 16.04LTS | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Red Hat Enterprise Linux 7.2 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

## Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows](https://msdn.microsoft.com/library/mt238290.aspx) | 13.0.11000.78 |
| [SQL Server Data Tools for Visual Studio](https://msdn.microsoft.com/library/mt204009.aspx) | 14.0.60203.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/vscodemssql) | Latest (0.1.5) |

## Unsupported features and services
The following features and services are not available on Linux at this time. The support of these features will be increasingly enabled during the monthly updates cadence of the preview program.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Full-text Search |
| &nbsp; | Replication |
| &nbsp; | Stretch DB |
| &nbsp; | Polybase |
| &nbsp; | Distributed Query |
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable |
| **High Availability** | Always On Availability Groups |
| &nbsp; | Database mirroring  |
| **Security** | Active Directory authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TWL |
| **Services** | SQL Server Agent |
| &nbsp; | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Integration Services | 
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

## Known issues
The following sections describe known issues with this release of SQL Server vNext CTP1 on Linux.

### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server.

    - **Resolution**: Restart SQL Server.

- Some time zone names in Linux don’t map exactly to Windows time zone names.

    - **Resolution**: Use time zone names from TZID column in the ‘Mapping for: Windows’ section table on the [Unicode.org documentation page](http://www.unicode.org/cldr/charts/latest/supplemental/zone_tzid.html).

- SQL Server Engine expects lines in text files to be terminated with CR-LF (Windows-style line formatting).

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- All log files and error logs are encoded in UTF-16.

- SQL Server Configuration Manager can’t connect to SQL Server on Linux.

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM <bits>** method instead for now.

### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\" notation when referred. For more information, visit the [In-memory OLTP Topic](https://stage.docs.microsoft.com/en-us/sql/linux/sql-server-linux-performance-get-started#use-in-memory-oltp).  

### SqlPackage
- Using SqlPackage requires to specify an absolute path for files. Using relative paths will map the files under the“/tmp/sqlpackage.\<code\>/system/system32” folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a “C:\” prefix.

### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  “C:\” scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\ paths with Linux paths. Then execute the script manually in SSMS.

## Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).