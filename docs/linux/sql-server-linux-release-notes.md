---
# required metadata

title: Release notes for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11-03-2016
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
# Release notes for SQL Server on Linux
The following release notes apply to SQL Server vNext CTP1 running on Linux.

## Supported operating systems 

- Ubuntu 16.04LTS (EXT4 file system)
- Red Hat Enterprise Linux 7.2: Workstation, Server, and Desktop. (XFS or EXT4 file systems).
- Docker Engine 1.8+ on Windows, Mac or Linux.

## Minimum requirements

### Docker

- Less than 64 cores. Using 64 or more cores will cause an error.
- Less than 32 GB of memory. If more than 32 GB of memory is available, only 32 GB will be used.

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

## See also

[Supported features of SQL Server on Linux](sql-server-linux-supported-features.md)
