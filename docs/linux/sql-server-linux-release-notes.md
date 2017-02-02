---
# required metadata

title: Release notes for SQL Server vNext on Linux | Microsoft Docs
description: This topic contains the release notes and supported features for SQL Server vNext running on Linux. Release notes for 1.2, 1.1, and 1.0 are included.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 02/02/2017
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
# Release notes for SQL Server vNext on Linux
The following release notes apply to SQL Server vNext running on Linux. This release supports many of the SQL Server database engine features for Linux. The topic below is broken into three sections: [CTP 1.2](#ctp12), [CTP 1.1](#ctp11), and [CTP 1.0](#ctp10) release notes. See the information in the each section for supported platforms, tools, features, and known issues.

## <a id="ctp12"> CTP 1.2 (January 2017)

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | EXT4 | [Installation guide](sql-server-linux-setup-suse-linux-enterprise-server.md) |
| Ubuntu 16.04LTS and 16.10 | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has only been tested up to 256GB of memory at this time.

### Package details
The SQL Server engine version for this release is 14.0.200.24. Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Version | Downloads |
|-----|-----|-----|
| RPM package | 14.0.200.24-2 | [mssql-server 14.0.200.24-2 Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.200.24-2.x86_64.rpm)</br>[mssql-server 14.0.200.24-2 High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.200.24-2.x86_64.rpm) | 
| Debian package | 14.0.200.24-2 | [mssql-server 14.0.200.24-2 Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.200.24-2_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows - Release Candidate 1](https://go.microsoft.com/fwlink/?LinkID=835608) | 17.0 |
| [SQL Server Data Tools for Visual Studio - Release Candidate 1](https://go.microsoft.com/fwlink/?LinkID=835150) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (0.2) |

> [!NOTE] 
> The SQL Server Management Studio and SQL Server Data Tools versions specified above are Release Candidates, hence not recommended for use in production.

### Unsupported features and services
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
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Always On Availability Groups |
| &nbsp; | Database mirroring  |
| **Security** | Active Directory Authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TLS |
| **Services** | SQL Server Agent |
| &nbsp; | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Integration Services | 
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

### Known issues
The following sections describe known issues with this release of SQL Server vNext CTP 1.2 on Linux.

#### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Do not run the command `ALTER SERVICE MASTER KEY REGENERATE`. There is a known bug that will cause SQL Server to become unstable. If you need to regenerate the Service Master Key, you should back up your database files, uninstall and then re-install SQL Server, and then restore your database files again.

- Resource name for SQL resource changed from ocf:sql:fci to ocf:mssql:fci. More details about configuring a shared disk failover cluster you can find [here](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-shared-disk-cluster-red-hat-7-configure).

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

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\" notation when referred. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the“/tmp/sqlpackage.\<code\>/system/system32” folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a “C:\” prefix.

#### Sqlcmd/BCP & ODBC 
- SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql) depends on a custom unixODBC Driver Manager. This causes conflicts if you have a previously installed unixODBC Driver Manager. 

    - **Resolution**: On Ubuntu, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC`. We are working on fixing this limitation for RHEL and SUSE and should have an update for you soon.  
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  “C:\” scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\ paths with Linux paths. Then execute the script manually in SSMS.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp11"> CTP 1.1 (December 2016)

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| Ubuntu 16.04LTS and 16.10 | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has only been tested up to 256GB of memory at this time.

### Package details
The SQL Server engine version for this release is 14.0.100.187. Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Version | Downloads |
|-----|-----|-----|
| RPM package | 14.0.100.187-1 | [mssql-server 14.0.100.187-1 Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.100.187-1.x86_64.rpm)</br>[mssql-server 14.0.100.187-1 High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.100.187-1.x86_64.rpm) | 
| Debian package | 14.0.100.187-1 | [mssql-server 14.0.100.187-1 Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.100.187-1_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows - Release Candidate 1](https://go.microsoft.com/fwlink/?LinkID=835608) | 17.0 |
| [SQL Server Data Tools for Visual Studio - Release Candidate 1](https://go.microsoft.com/fwlink/?LinkID=835150) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (0.2) |

> [!NOTE] 
> The SQL Server Management Studio and SQL Server Data Tools versions specified above are Release Candidates, hence not recommended for use in production.

### Unsupported features and services
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
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Always On Availability Groups |
| &nbsp; | Database mirroring  |
| **Security** | Active Directory Authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TLS |
| **Services** | SQL Server Agent |
| &nbsp; | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Integration Services | 
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

### Known issues
The following sections describe known issues with this release of SQL Server vNext CTP 1.1 on Linux.

#### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Do not run the command `ALTER SERVICE MASTER KEY REGENERATE`. There is a known bug that will cause SQL Server to become unstable. If you need to regenerate the Service Master Key, you should back up your database files, uninstall and then re-install SQL Server, and then restore your database files again.

- Resource name for SQL resource changed from ocf:sql:fci to ocf:mssql:fci. More details about configuring a shared disk failover cluster you can find [here](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-shared-disk-cluster-red-hat-7-configure).

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

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\" notation when referred. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the“/tmp/sqlpackage.\<code\>/system/system32” folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a “C:\” prefix.

#### Sqlcmd/BCP & ODBC 
- SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql) depends on a custom unixODBC Driver Manager. This causes conflicts if you have a previously installed unixODBC Driver Manager. 

    - **Resolution**: On Ubuntu, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC`. We are working on fixing this limitation for RHEL and SUSE and should have an update for you soon.  
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  “C:\” scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\ paths with Linux paths. Then execute the script manually in SSMS.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp10"> CTP 1.0 (November 2016)

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.2 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| Ubuntu 16.04LTS | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has only been tested up to 256GB of memory at this time.

### Package details
The SQL Server engine version for this release is 14.0.1.246. Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Version | Downloads |
|-----|-----|-----|
| RPM package | 14.0.1.246-6 | [mssql-server 14.0.1.246-6 Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.1.246-6.x86_64.rpm)</br>[mssql-server 14.0.1.246-6 High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.1.246-6.x86_64.rpm) | 
| Debian package | 14.0.1.246-6 | [mssql-server 14.0.1.246-6 Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.1.246-6_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows - Release Candidate 1](https://go.microsoft.com/fwlink/?LinkID=835608) | 17.0 |
| [SQL Server Data Tools for Visual Studio - Release Candidate 1](https://go.microsoft.com/fwlink/?LinkID=835150) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (0.1.5) |

> [!NOTE] 
> The SQL Server Management Studio and SQL Server Data Tools versions specified above are Release Candidates, hence not recommended for use in production.

### Unsupported features and services
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
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Always On Availability Groups |
| &nbsp; | Database mirroring  |
| **Security** | Active Directory authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TLS |
| **Services** | SQL Server Agent |
| &nbsp; | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Integration Services | 
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

### Known issues
The following sections describe known issues with this release of SQL Server vNext CTP1 on Linux.

#### General
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

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\" notation when referred. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires to specify an absolute path for files. Using relative paths will map the files under the“/tmp/sqlpackage.\<code\>/system/system32” folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a “C:\” prefix.

#### Sqlcmd/BCP & ODBC 
- SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql) depends on a custom unixODBC Driver Manager. This causes conflicts if you have a previously installed unixODBC Driver Manager. 

    - **Resolution**: On Ubuntu, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC`. We are working on fixing this limitation for RHEL and SUSE and should have an update for you soon.  
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  “C:\” scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\ paths with Linux paths. Then execute the script manually in SSMS.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
