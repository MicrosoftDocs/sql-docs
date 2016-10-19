---
# required metadata

title: Release Notes for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-18-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

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
# Release Notes for SQL Server on Linux

## Release 13.0.2990.31 - September 30, 2016

The following release notes apply to SQL Server vNext CTP1 running on Linux.

## Package details

### Debian

- **Package**: mssql-server
- **Size**: 255413904
- **Installed-Size**: 1094290
- **Version**: 13.0.2990.31-8
- **Architecture**: amd64
- **Depends**: libunwind8, libc6, adduser, libjemalloc1, libc++1, gdb, debconf, libcurl3, hostname
-  **SHA256**: 096dd77f31f28fada0fe62044dfb80e408c515461763f0b3d658ecb14d44f7c4

### RPM

- **Name**: mssql-server
- **Architecture**: x86_64
- **Version**: 13.0.2990.31

## Package file locations

| Distribution | Download |
|-----|-----|
| Red Hat Enterprise Linux 7.2 | [https://private-repo.microsoft.com/rhel7/mssql-private-preview/mssql-server-13.0.2990.31-12.x86_64.rpm](https://private-repo.microsoft.com/rhel7/mssql-private-preview/mssql-server-13.0.2990.31-12.x86_64.rpm) |
| Ubuntu 16.04 | [https://private-repo.microsoft.com/ubuntu/mssql-private-preview/pool/main/m/mssql-server/mssql-server_13.0.2990-31-8_amd64.deb](https://private-repo.microsoft.com/ubuntu/mssql-private-preview/pool/main/m/mssql-server/mssql-server_13.0.2990-31-8_amd64.deb) |

## Supported operating systems 

- Ubuntu 16.04LTS (EXT4 file system)
- Red Hat Enterprise Linux 7.2: Workstation, Server and Desktop. (XFS or EXT4 file systems).
- Docker Engine 1.8+ on Windows, Mac or Linux.
- Less than 64 cores. Using 64 or more cores will cause an error.
- Less than 32 GB of memory. If more than 32 GB of memory is available, only 32 GB will be used.

> [!NOTE] 
> Any file systems that are not specified in the previous list are not supported.

## Known issues

- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server. **Resolution**: Restart SQL Server.
- Some time zone names in Linux don’t map exactly to Windows time zone names. **Resolution**: Use time zone names from TZID column in the ‘Mapping for: Windows’ section table on the [Unicode.org documentation page](http://www.unicode.org/cldr/charts/latest/supplemental/zone_tzid.html).
- The length of the hostname where SQL Server is installed needs to be 15 characters or less.
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 
- Dynamic Management Views, including dm_os_volume_stats, are currently unsupported.
- Paths in T-SQL queries must use "C:\" as the root of the path. “C:\” is mapped to /var/opt/mssql/ on the Linux host. ** Resolution**: Use this mapping in T-SQL queries. For example: C:\data maps to /var/opt/mssql/data.
- SQL Server Engine expects lines in text files to be terminated with CR-LF (Windows-style line formatting).
- The operating system version displayed in SQL Management Studio for the SQL Server properties will say ‘Windows NT’ even though SQL Server is running on Linux.
- Only database files that are backed up from SQL Server 2016 RTM and lower can be restored.
- Only single instance installations are supported.
- All log files and error logs are encoded in UTF-16.
- Using SqlPackage requires to specify an absolute path for files. Using relative paths will map the files under the“/tmp/sqlpackage.\<code\>/system/system32” folder. **Resolution**: Use absolute file paths.
- SqlPackage shows the location of files with a “C:\” prefix.
- Changing the locations of TempDB data and log files is not supported.
- SQL Server Configuration Manager can’t connect to SQL Server on Linux.
