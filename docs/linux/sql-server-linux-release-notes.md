---
# required metadata

title: Release notes for SQL Server 2017 on Linux | Microsoft Docs
description: This topic contains the release notes and supported features for SQL Server 2017 running on Linux. Release notes are included for CTP 2.1 and prior versions.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 05/23/2017
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
# Release notes for SQL Server 2017 on Linux

The following release notes apply to SQL Server 2017 running on Linux. This release supports many of the SQL Server database engine features for Linux. The topic below is broken into sections for each release, beginning with the most recent release, CTP 2.1. See the information in each section for supported platforms, tools, features, and known issues.

The following table lists the releases of SQL Server 2017 covered in this topic.

| Release | Version | Release date |
|-----|-----|-----|
| [CTP 2.1](#ctp21) | 14.0.600.250 | 5-2017 |
| [CTP 2.0](#ctp20) | 14.0.500.272 | 4-2017 |
| [CTP 1.4](#ctp14) | 14.0.405.198 | 3-2017 |
| [CTP 1.3](#ctp13) | 14.0.304.138 | 2-2017 |
| [CTP 1.2](#ctp12) | 14.0.200.24 | 1-2017 |
| [CTP 1.1](#ctp11) | 14.0.100.187 | 12-2016 |
| [CTP 1.0](#ctp10) | 14.0.1.246 | 11-2016 |

## <a id="ctp21"> CTP 2.1 (May 2017) </a>
The SQL Server engine version for this release is 14.0.600.250.

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | EXT4 | [Installation guide](sql-server-linux-setup-suse-linux-enterprise-server.md) |
| Ubuntu 16.04LTS and 16.10 | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE]
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has been tested up to 1 TB of memory at this time.

### Package details
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Sever package](sql-server-linux-setup.md)
- [Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.600.250-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.600.250-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.600.250-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-fts-14.0.600.250-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-agent-14.0.600.250-2.x86_64.rpm) | 
| SLES RPM package | 14.0.600.250-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-14.0.600.250-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-ha-14.0.600.250-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-fts-14.0.600.250-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-agent-14.0.600.250-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.600.250-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.600.250-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.600.250-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.600.250-2_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.600.250-2_amd64.deb) |
| Ubuntu 16.10 Debian package | 14.0.600.250-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.600.250-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.600.250-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.600.250-2_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.600.250-2_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows](https://go.microsoft.com/fwlink/?linkid=847722) | 17.0 |
| [SQL Server Data Tools for Visual Studio](https://go.microsoft.com/fwlink/?linkid=846626) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (1.12) |

### Unsupported features and services
The following features and services are not available on Linux at this time. The support of these features will be increasingly enabled during the monthly updates cadence of the preview program.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Replication |
| &nbsp; | Stretch DB |
| &nbsp; | Polybase |
| &nbsp; | Distributed Query |
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable |
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Database mirroring  |
| **Security** | Active Directory Authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TLS |
| **Services** | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

### Known issues
The following sections describe known issues with this release of SQL Server 2017 CTP 2.1 on Linux.

#### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server.

    - **Resolution**: Restart SQL Server.

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- SQL Server Configuration Manager can’t connect to SQL Server on Linux.

- The default language of the **sa** login is English.

    - **Resolution**: Change the language of the **sa** login with the **ALTER LOGIN** statement.

#### Databases
- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### Always On Availability Group
- `sys.fn_hadr_backup_is_preffered_replica` does not work for `CLUSTER_TYPE=NONE` or `CLUSTER_TYPE=EXTERNAL` because it relies on the WSFC-replicated cluster registry key which not available. We are working on providing a similar functionality through a different function. 

#### Full-Text Search
- Not all filters are available with this release, including filters for Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md#filters).

#### SQL Agent
- The following components and subsystems of SQL Agent jobs are not currently supported on Linux:

    - Subsystems: CmdExec, PowerShell, Replication Distributor, Snapshot, Merge, Queue Reader, SSIS, SSAS, SSRS
    - Alerts
    - DB Mail
    - Log Reader Agent 
    - Change Data Capture

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the "/tmp/sqlpackage.\<code\>/system/system32" folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS are not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- Number of log files to retain cannot be modified.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp20"> CTP 2.0 (April 2017) </a>
The SQL Server engine version for this release is 14.0.500.272.

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | EXT4 | [Installation guide](sql-server-linux-setup-suse-linux-enterprise-server.md) |
| Ubuntu 16.04LTS and 16.10 | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has been tested up to 1 TB of memory at this time.

### Package details
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the following installation guides:

- [Install SQL Sever package](sql-server-linux-setup.md)
- [Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.500.272-2 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.500.272-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.500.272-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-fts-14.0.500.272-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-agent-14.0.500.272-2.x86_64.rpm) | 
| SLES RPM package | 14.0.500.272-2 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-14.0.500.272-2.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-ha-14.0.500.272-2.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-fts-14.0.500.272-2.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-agent-14.0.500.272-2.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.500.272-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.500.272-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.500.272-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.500.272-2_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.500.272-2_amd64.deb) |
| Ubuntu 16.10 Debian package | 14.0.500.272-2 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.500.272-2_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.500.272-2_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.500.272-2_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.500.272-2_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows - Release Candidate 2](https://go.microsoft.com/fwlink/?linkid=840957) | 17.0 |
| [SQL Server Data Tools for Visual Studio - Release Candidate 2](https://go.microsoft.com/fwlink/?linkid=837939) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (0.2.1) |

> [!NOTE] 
> The SQL Server Management Studio and SQL Server Data Tools versions specified above are Release Candidates, hence not recommended for use in production.

### Unsupported features and services
The following features and services are not available on Linux at this time. The support of these features will be increasingly enabled during the monthly updates cadence of the preview program.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Replication |
| &nbsp; | Stretch DB |
| &nbsp; | Polybase |
| &nbsp; | Distributed Query |
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable |
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Database mirroring  |
| **Security** | Active Directory Authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TLS |
| **Services** | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Integration Services | 
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

### Known issues
The following sections describe known issues with this release of SQL Server 2017 CTP 2.0 on Linux.

#### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server.

    - **Resolution**: Restart SQL Server.

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- SQL Server Configuration Manager can’t connect to SQL Server on Linux.

- The default language of the **sa** login is English.

    - **Resolution**: Change the language of the **sa** login with the **ALTER LOGIN** statement.

#### Databases
- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### Always On Availability Group
- All HA configurations - meaning availability group is added as a resource to a Pacemaker cluster - created with pre CTP2.0 packages are not backwards compatible with the new package. Delete all previousely configured clustered resources and create new availability groups with `CLUSTER_TYPE=EXTERNAL`. See [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md).
- Availability groups created with `CLUSTER_TYPE=NONE` and not added as resources in the cluster will continue working after upgrade. Use for read-scale scenarios. See [Configure read-scale availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md).
- `sys.fn_hadr_backup_is_preffered_replica` does not work for `CLUSTER_TYPE=NONE` or `CLUSTER_TYPE=EXTERNAL` because it relies on the WSFC-replicated cluster registry key which not available. We are working on providing a similar functionality through a different function. 

#### Full-Text Search
- Not all filters are available with this release, including filters for Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md#filters).

- The Korean word breaker takes several seconds to load and generates an error on first use. After this initial error, it should work normally.

#### SQL Agent
- The following components and subsystems of SQL Agent jobs are not currently supported on Linux:

    - Subsystems: CmdExec, PowerShell, Replication Distributor, Snapshot, Merge, Queue Reader, SSIS, SSAS, SSRS
    - Alerts
    - DB Mail
    - Log Reader Agent 
    - Change Data Capture

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the "/tmp/sqlpackage.\<code\>/system/system32" folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS are not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- Number of log files to retain cannot be modified.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp14"> CTP 1.4 (March 2017) </a>
The SQL Server engine version for this release is 14.0.405.198.

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | EXT4 | [Installation guide](sql-server-linux-setup-suse-linux-enterprise-server.md) |
| Ubuntu 16.04LTS and 16.10 | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has been tested up to 1 TB of memory at this time.

### Package details
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the installation guides below
-	[Install SQL Sever package](sql-server-linux-setup.md)
-	[Install Full-text Search package](sql-server-linux-setup-full-text-search.md)
-	[Install SQL Server Agent package](sql-server-linux-setup-sql-agent.md)

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.405.200-1 | [Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.405.200-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.405.200-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-fts-14.0.405.200-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-agent-14.0.405.200-1.x86_64.rpm) | 
| SLES RPM package | 14.0.405.200-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-14.0.405.200-1.x86_64.rpm)</br>[High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-ha-14.0.405.200-1.x86_64.rpm)</br>[Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-fts-14.0.405.200-1.x86_64.rpm)</br>[SQL Server Agent RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-agent-14.0.405.200-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.405.200-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.405.200-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.405.200-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.405.200-1_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.405.200-1_amd64.deb) |
| Ubuntu 16.10 Debian package | 14.0.405.200-1 | [Engine Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.405.200-1_amd64.deb)</br>[High Availability Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.405.200-1_amd64.deb)</br>[Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.405.200-1_amd64.deb)</br>[SQL Server Agent Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-agent/mssql-server-agent_14.0.405.200-1_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows - Release Candidate 2](https://go.microsoft.com/fwlink/?linkid=840957) | 17.0 |
| [SQL Server Data Tools for Visual Studio - Release Candidate 2](https://go.microsoft.com/fwlink/?linkid=837939) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (0.2.1) |

> [!NOTE] 
> The SQL Server Management Studio and SQL Server Data Tools versions specified above are Release Candidates, hence not recommended for use in production.

### Unsupported features and services
The following features and services are not available on Linux at this time. The support of these features will be increasingly enabled during the monthly updates cadence of the preview program.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Replication |
| &nbsp; | Stretch DB |
| &nbsp; | Polybase |
| &nbsp; | Distributed Query |
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable |
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Database mirroring  |
| **Security** | Active Directory Authentication |
| &nbsp; | Windows Authentication |
| &nbsp; | Extensible Key Management |
| &nbsp; | Use of user-provided certificate for SSL or TLS |
| **Services** | SQL Server Browser |
| &nbsp; | SQL Server R services |
| &nbsp; | StreamInsight |
| &nbsp; | Analysis Services |
| &nbsp; | Reporting Services |
| &nbsp; | Integration Services | 
| &nbsp; | Data Quality Services |
| &nbsp; | Master Data Services |

### Known issues
The following sections describe known issues with this release of SQL Server 2017 CTP 1.4 on Linux.

#### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Do not run the command `ALTER SERVICE MASTER KEY REGENERATE`. There is a known bug that will cause SQL Server to become unstable. If you need to regenerate the Service Master Key, you should back up your database files, uninstall and then re-install SQL Server, and then restore your database files again.

- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server.

    - **Resolution**: Restart SQL Server.

- Some time zone names in Linux don’t map exactly to Windows time zone names.

    - **Resolution**: Use time zone names from TZID column in the ‘Mapping for: Windows’ section table on the [Unicode.org documentation page](http://www.unicode.org/cldr/charts/latest/supplemental/zone_tzid.html).

- SQL Server Engine expects lines in text files to be terminated with CR-LF (Windows-style line formatting).

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- All log files and error logs are encoded in UTF-16.

- SQL Server Configuration Manager can’t connect to SQL Server on Linux.

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM \<bits\>** method instead for now. 

#### Databases
- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### Always On Availability Group
- Always On Availability Group clustered resources on Linux that were created with CTP 1.3 will fail after you upgrade HA package (mssql-server-ha). 

   - **Resolution**: Before you upgrade the HA package, set the cluster resource parameter `notify=true`. 
   
      - The following example sets the cluster resource parameter on a resource named **ag1** on RHEL or Ubuntu: 

      ```bash
      sudo pcs resource update ag1-master notify=true
      ```

      - For SLES, update availability group resource configuration to add `notify=true`.  

      ```bash
      crm configure edit ms-ag_cluster 
      ```

      Add `notify=true` and save the resource configuration.

- Always On Availability Groups in Linux may be subject to data loss if replicas are in synchronous commit mode. See details as appropriate for your Linux distribution. 

   - [RHEL](sql-server-linux-availability-group-cluster-rhel.md)
   - [SLES](sql-server-linux-availability-group-cluster-sles.md)
   - [Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md)

#### Full-Text Search
- Not all filters are available with this release, including filters for Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md#filters).

- The Korean word breaker takes several seconds to load and generates an error on first use. After this initial error, it should work normally.

#### SQL Agent
- The following components and subsystems of SQL Agent jobs are not currently supported on Linux:
    - Subsystems: CmdExec, PowerShell, Replication Distributor, Snapshot, Merge, Queue Reader, SSIS, SSAS, SSRS
    - Alerts
    - DB Mail
    - Log Shipping
    - Log Reader Agent 
    - Change Data Capture

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the "/tmp/sqlpackage.\<code\>/system/system32" folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The file browser is restricted to the  "C:\\" scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\\ paths with Linux paths. Then execute the script manually in SSMS.

- Number of log files to retain cannot be modified.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp13"> CTP 1.3 (February 2017) </a>
The SQL Server engine version for this release is 14.0.304.138.

### Supported platforms 

| Platform | File System | Installation Guide |
|-----|-----|-----|
| Red Hat Enterprise Linux 7.3 Workstation, Server, and Desktop | XFS or EXT4 | [Installation guide](sql-server-linux-setup-red-hat.md) | 
| SUSE Enterprise Linux Server v12 SP2 | EXT4 | [Installation guide](sql-server-linux-setup-suse-linux-enterprise-server.md) |
| Ubuntu 16.04LTS and 16.10 | EXT4 | [Installation guide](sql-server-linux-setup-ubuntu.md) | 
| Docker Engine 1.8+ on Windows, Mac, or Linux | N/A | [Installation guide](sql-server-linux-setup-docker.md) | 

> [!NOTE] 
> You need at least 3.25GB of memory to run SQL Server on Linux.
> SQL Server Engine has been tested up to 1 TB of memory at this time.

### Package details
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Package version | Downloads |
|-----|-----|-----|
| Red Hat RPM package | 14.0.304.138-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-14.0.304.138-1.x86_64.rpm)</br>[mssql-server-ha High Availability RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-ha-14.0.304.138-1.x86_64.rpm)</br>[mssql-server-fts Full-text Search RPM package](https://packages.microsoft.com/rhel/7/mssql-server/mssql-server-fts-14.0.304.138-1.x86_64.rpm) | 
| SLES RPM package | 14.0.304.138-1 | [mssql-server Engine RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-14.0.304.138-1.x86_64.rpm)</br>[mssql-server-ha High Availability RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-ha-14.0.304.138-1.x86_64.rpm)</br>[mssql-server-fts Full-text Search RPM package](https://packages.microsoft.com/sles/12/mssql-server/mssql-server-fts-14.0.304.138-1.x86_64.rpm) | 
| Ubuntu 16.04 Debian package | 14.0.304.138-1 | [mssql-server Engine Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.304.138-1_amd64.deb)</br>[mssql-server-ha High Availability Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.304.138-1_amd64.deb)</br>[mssql-server-fts Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.04/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.304.138-1_amd64.deb) |
| Ubuntu 16.10 Debian package | 14.0.304.138-1 | [mssql-server Engine Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server/mssql-server_14.0.304.138-1_amd64.deb)</br>[mssql-server-ha High Availability Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-ha/mssql-server-ha_14.0.304.138-1_amd64.deb)</br>[mssql-server-fts Full-text Search Debian package](https://packages.microsoft.com/ubuntu/16.10/mssql-server/pool/main/m/mssql-server-fts/mssql-server-fts_14.0.304.138-1_amd64.deb) |

### Supported client tools

| Tool | Minimum version |
|-----|-----|
| [SQL Server Management Studio (SSMS) for Windows - Release Candidate 2](https://go.microsoft.com/fwlink/?linkid=840957) | 17.0 |
| [SQL Server Data Tools for Visual Studio - Release Candidate 2](https://go.microsoft.com/fwlink/?linkid=837939) | 17.0 |
| [Visual Studio Code](https://code.visualstudio.com) with the [mssql extension](https://aka.ms/mssql-marketplace) | Latest (0.2.1) |

> [!NOTE] 
> The SQL Server Management Studio and SQL Server Data Tools versions specified above are Release Candidates, hence not recommended for use in production.

### Unsupported features and services
The following features and services are not available on Linux at this time. The support of these features will be increasingly enabled during the monthly updates cadence of the preview program.

| Area | Unsupported feature or service |
|-----|-----|
| **Database engine** | Replication |
| &nbsp; | Stretch DB |
| &nbsp; | Polybase |
| &nbsp; | Distributed Query |
| &nbsp; | System extended stored procedures (XP_CMDSHELL, etc.) |
| &nbsp; | Filetable |
| &nbsp; | CLR assemblies with the EXTERNAL_ACCESS or UNSAFE permission set |
| **High Availability** | Database mirroring  |
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
The following sections describe known issues with this release of SQL Server 2017 CTP 1.3 on Linux.

#### General
- The length of the hostname where SQL Server is installed needs to be 15 characters or less. 

    - **Resolution**: Change the name in /etc/hostname to something 15 characters long or less.

- Do not run the command `ALTER SERVICE MASTER KEY REGENERATE`. There is a known bug that will cause SQL Server to become unstable. If you need to regenerate the Service Master Key, you should back up your database files, uninstall and then re-install SQL Server, and then restore your database files again.

- Manually setting the system time backwards in time will cause SQL Server to stop updating the internal system time within SQL Server.

    - **Resolution**: Restart SQL Server.

- Some time zone names in Linux don’t map exactly to Windows time zone names.

    - **Resolution**: Use time zone names from TZID column in the ‘Mapping for: Windows’ section table on the [Unicode.org documentation page](http://www.unicode.org/cldr/charts/latest/supplemental/zone_tzid.html).

- SQL Server Engine expects lines in text files to be terminated with CR-LF (Windows-style line formatting).

- Only single instance installations are supported.

    - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers. 

- All log files and error logs are encoded in UTF-16.

- SQL Server Configuration Manager can’t connect to SQL Server on Linux.

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM \<bits\>** method instead for now. 

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

- Always On Availability Groups in Linux may be subject to data loss if replicas are in synchronous commit mode. See 
 
   - [RHEL](sql-server-linux-availability-group-cluster-rhel.md)
   - [SLES](sql-server-linux-availability-group-cluster-sles.md)
   - [Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md)

   
#### Full-Text Search
- Not all filters are available with this release, including filters for Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md#filters).

- The Korean word breaker takes several seconds to load and generates an error on first use. After this initial error, it should work normally.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the "/tmp/sqlpackage./<code/>/system/system32" folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

#### Sqlcmd/BCP & ODBC 
- SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql) depends on a custom unixODBC Driver Manager. This causes conflicts if you have a previously installed unixODBC Driver Manager. 

    - **Resolution**: On Ubuntu, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC`. We are working on fixing this limitation for RHEL and SUSE and should have an update for you soon.  
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  "C:\\" scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\\ paths with Linux paths. Then execute the script manually in SSMS.

- Number of log files to retain cannot be modified.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp12"> CTP 1.2 (January 2017)
The SQL Server engine version for this release is 14.0.200.24.

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
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Package version | Downloads |
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
The following sections describe known issues with this release of SQL Server 2017 CTP 1.2 on Linux.

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

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM \<bits\>** method instead for now. 

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\\" notation when referred. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the “/tmp/sqlpackage.\<code\>/system/system32” folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

#### Sqlcmd/BCP & ODBC 
- If you have an older version of SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql), you might have installed a custom unixODBC Driver Manager (unixODBC-utf16). This could casue a potential conflict as we no longer use a custom driver manager. 

    - **Resolution**: On Ubuntu and SLES, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC-utf16 unixODBC-utf16-devel` and retry the install.
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  "C:\\" scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\\ paths with Linux paths. Then execute the script manually in SSMS.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp11"> CTP 1.1 (December 2016)
The SQL Server engine version for this release is 14.0.100.187.

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
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Package version | Downloads |
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
The following sections describe known issues with this release of SQL Server 2017 CTP 1.1 on Linux.

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

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM \<bits\>** method instead for now. 

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\" notation when referred. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires specifying an absolute path for files. Using relative paths will map the files under the "/tmp/sqlpackage.\<code\>/system/system32" folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

#### Sqlcmd/BCP & ODBC 
- SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql) depends on a custom unixODBC Driver Manager. This causes conflicts if you have a previously installed unixODBC Driver Manager. 

    - **Resolution**: On Ubuntu, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC`. We are working on fixing this limitation for RHEL and SUSE and should have an update for you soon.  
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  "C:\\" scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\\ paths with Linux paths. Then execute the script manually in SSMS.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
<br/>
<br/>

![Separation bar grapic](./media/sql-server-linux-release-notes/seperationbar3.png)

## <a id="ctp10"> CTP 1.0 (November 2016)
The SQL Server engine version for this release is 14.0.1.246.

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
Package details and download locations for the RPM and Debian packages are listed in the following table. Note that you do not need to download these packages directly if you use the steps in the [installation guides](sql-server-linux-setup.md).

| Package | Package version | Downloads |
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
The following sections describe known issues with this release of SQL Server 2017 CTP1 on Linux.

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

- **CREATE ASSEMBLY** will not work when trying to use a file. Use the **FROM \<bits\>** method instead for now.

#### Databases
- Changing the locations of TempDB data and log files is not supported.

- System databases can not be moved with the mssql-conf utility.

- When restoring a database that was backed up on SQL Server on Windows, you must use the **WITH MOVE** clause in the Transact-SQL statement.

- Distributed transactions requiring the Microsoft Distributed Transaction Coordinator service are not supported on SQL Server running on Linux. SQL Server to SQL Server distributed transactions are supported.

#### In-Memory OLTP
- In-Memory OLTP databases can only be created in the /var/opt/mssql directory. These databases also need to have the "C:\\" notation when referred. For more information, visit the [In-memory OLTP Topic](sql-server-linux-performance-get-started.md#use-in-memory-oltp).  

#### SqlPackage
- Using SqlPackage requires to specify an absolute path for files. Using relative paths will map the files under the "/tmp/sqlpackage.\<code\>/system/system32" folder. 

    - **Resolution**: Use absolute file paths.

- SqlPackage shows the location of files with a "C:\\" prefix.

#### Sqlcmd/BCP & ODBC 
- SQL Server Command Line tools (mssql-tools) and the ODBC Driver (msodbcsql) depends on a custom unixODBC Driver Manager. This causes conflicts if you have a previously installed unixODBC Driver Manager. 

    - **Resolution**: On Ubuntu, the conflict will be resolved automatically. When prompted if you would like to unisntall the existing unixODBC Driver Manager, type 'y' and proceed with the installation. On RedHat, you will have to remove the existing unixODBC Driver Manager manually using `yum remove unixODBC`. We are working on fixing this limitation for RHEL and SUSE and should have an update for you soon.  
    
#### SQL Server Management Studio (SSMS)
The following limitations apply to SSMS on Windows connected to SQL Server on Linux.

- Maintenance plans are not supported.

- Management Data Warehouse (MDW) and the data collector in SSMS is not supported. 

- SSMS UI components that have Windows Authentication or Windows event log options do not work with Linux. You can still use these features with other options, such as SQL logins. 

- The SQL Server Agent is not supported yet. Therefore, SQL Server Agent functionality in SSMS does not work on Linux at the moment.

- The file browser is restricted to the  "C:\\" scope, which resolves to /var/opt/mssql/ on Linux. To use other paths, generate scripts of the UI operation and replace the C:\\ paths with Linux paths. Then execute the script manually in SSMS.

### Next steps
To begin using SQL Server on Linux, see [Get started with SQL Server on Linux](sql-server-linux-get-started-tutorial.md).
