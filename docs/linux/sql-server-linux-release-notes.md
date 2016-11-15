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

## Supported operating systems 

| Platform | File System |
|-----|-----|
| Ubuntu 16.04LTS | EXT4 |
| Red Hat Enterprise Linux 7.2 Workstation, Server, and Desktop | XFS or EXT4 |
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A |

## Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows](https://msdn.microsoft.com/library/mt238290.aspx) | 13.0.11000.78 |
| [SQL Server Data Tools for Visual Studio](https://msdn.microsoft.com/library/mt204009.aspx) | 14.0.60203.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [vscode-mssql extension](https://aka.ms/vscodemssql) | Latest |

## Unsupported features and services
The following features and services are not available on Linux at this time.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Full-text Search |
| &nbsp; | Replication|
| &nbsp; | Stretch DB|
| &nbsp; | Polybase|
| &nbsp; | Distributed Query|
| &nbsp; | Filestream|
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable|
| **High Availability** | Always On Availability Groups |
| &nbsp; | Clustering |
| &nbsp; | Database mirroring |
| **Security** | Active Directory authentication |
| &nbsp; | Windows Authentication |
| **Services** | SQL Server Agent |
| &nbsp; | SQL Server Browser|
| &nbsp; | SQL Server R services|
| &nbsp; | StreamInsight|
| &nbsp; | Business Intelligence Suite|
| &nbsp; | Analysis Services|
| &nbsp; | Reporting Services|
| &nbsp; | Integration Services|
| &nbsp; | Data Quality Services|
| &nbsp; | Master Data Services |

## Known issues
The following sections describe known issues with this release of SQL Server vNext CTP1 on Linux.

### General
- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server.

    - **Resolution**: Restart SQL Server.

- Some time zone names in Linux don’t map exactly to Windows time zone names.

    - **Resolution**: Use time zone names from TZID column in the ‘Mapping for: Windows’ section table on the [Unicode.org documentation page](http://www.unicode.org/cldr/charts/latest/supplemental/zone_tzid.html).

- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- SQL Server Engine expects lines in text files to be terminated with CR-LF (Windows-style line formatting).

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- All log files and error logs are encoded in UTF-16.

- SQL Server Configuration Manager can’t connect to SQL Server on Linux.

- SQL Server Engine has only been tested up to 256GB of memory at this time.

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM <bits>** method instead for now.

### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database backed up on SQL Server on Linux, you must use the **WITH MOVE** clause in the Transact-SQL statement.

### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. If you require more space than what is available at that location, one solution is to mount a drive under var/opt/mssql. 

### SqlPackage
- Using SqlPackage requires to specify an absolute path for files. Using relative paths will map the files under the“/tmp/sqlpackage.\<code\>/system/system32” folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a “C:\” prefix.

### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent only supports TSQL-based jobs. Agent functionality in SSMS which relies on other job types do not work on Linux.

- The file browser is restricted to the  “C:\” scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\ paths with Linux paths. Then execute the script manually in SSMS.

## Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).