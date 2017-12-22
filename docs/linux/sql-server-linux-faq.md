---
title:  SQL Server on Linux FAQ | Microsoft Docs
description: This article provides answers to frequently asked questions about SQL Server running on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 12/21/2017
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: sql-linux
ms.suite: "sql"
ms.custom: ""
ms.technology: database-engine
ms.workload: "Active"
---

# SQL Server on Linux Frequently Asked Questions (FAQ)

The following sections provide common questions and answers for SQL Server running on Linux.

## General Questions

1. **What Linux platforms are supported?**

   SQL Server is currently supported on Red Hat Enterprise Server, SUSE Linux Enterprise Server, and Ubuntu. For the latest information about the supported versions, see [Supported platforms](sql-server-linux-setup.md#supportedplatforms).

1. **What SQL Server features are supported on Linux?**

   For a complete list of supported features and known issues, see the [Release notes](sql-server-linux-release-notes.md).

1. **What is the support policy for SQL Server?**

   To understand the support policy, review the [Technical Support Policy for SQL Server](https://support.microsoft.com/help/4047326/support-policy-for-microsoft-sql-server).

1. **I am coming from a Windows SQL Server background. Are there resources to help learn how to use SQL Server on Linux?**

   The [quickstarts](sql-server-linux-setup.md#platforms) provide step-by-step instructions on how to install SQL Server on Linux and run Transact-SQL queries. Other tutorials provide additional instructions on using SQL Server on Linux. For a third-party list of tips, see the [MSSQLTIPS list of SQL Server on Linux Tips](https://www.mssqltips.com/sql-server-tip-category/226/sql-server-on-linux/).

## Installation

1. **How do I get SQL Server installed on my Linux servers?**

   Microsoft maintains package repositories for installing SQL Server and supports installation via native package managers such as yum, zypper, and apt. To quickly install, see one of the [quickstarts](sql-server-linux-setup.md#platforms).

1. **Can I install SQL Server on the Linux Subsystem for Windows 10?**

   No. Linux running on Windows 10 is currently not a supported platform for SQL Server and related tools.

1. **Which Linux file systems can SQL Server 2017 use for data files?**

   Currently SQL Server on Linux supports ext4 and XFS. Support for other file systems will be added as needed in the future.

1. **Can I download the installation packages to install SQL Server offline?**

   Yes. For more information, see the package download links in the [Release notes](sql-server-linux-release-notes.md). Also, review the [instructions for offline installations](sql-server-linux-setup.md#offline).

1. **Can I perform an unattended installation of SQL Server on Linux?**

   Yes. For a discussion of unattended installation, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md#unattended). See the sample scripts for [Red Hat](sample-unattended-install-redhat.md), [SUSE Linux Enterprise Server](sample-unattended-install-suse.md), and [Ubuntu](sample-unattended-install-ubuntu.md). You can also review [this sample script](https://blogs.msdn.microsoft.com/sqlcat/2017/10/03/unattended-install-and-configuration-for-sql-server-2017-on-linux/) created by the SQL Server Customer Advisory Team.

## Tools

1. **Can I use the SQL Server Management Studio client on Windows to access SQL Server on Linux?**

   Yes, you can use all your existing tools that run on Windows to access SQL Server on Linux. These include tools from Microsoft such as SQL Server Management Studio (SSMS), SQL Server Data Tools (SSDT), and OSS and third-party tools.

1. **Is there a tool like SSMS that runs on Linux?**

   The new Microsoft SQL Operations Studio (preview) is a cross-platform tool for managing SQL Server. For more information, see [What is Microsoft SQL Operations Studio (preview)](../sql-operations-studio/what-is.md).

1. **Are commands like sqlcmd and bcp available on Linux?**

   Yes, [sqlcmd and bcp](sql-server-linux-setup-tools.md) are natively available on Linux, macOS, and Windows. In addition, use the new [mssql-scripter](https://github.com/Microsoft/mssql-scripter) command-line tool on Linux, macOS, or Windows to generate T-SQL scripts for your SQL database running anywhere. Also, see the preview release for [mssql-cli](https://blogs.technet.microsoft.com/dataplatforminsider/2017/12/12/try-mssql-cli-a-new-interactive-command-line-tool-for-sql-server/).

1. **Is it possible to view Activity Monitor when connected through SSMS on Windows for an instance running on Linux?**

   Yes, you can use SSMS on Windows to connect remotely, and use tools/ features such as Activity Monitor commands on a Linux instance.

1. **What tools are available to monitor SQL Server performance on Linux?**

   You can use [system dynamic management views (DMVs)](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) to collect various types of information about SQL Server, including Linux process information. You can use [Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) to improve query performance. Other tools, such as the built-in [Performance Dashboard](https://blogs.msdn.microsoft.com/sql_server_team/new-in-ssms-performance-dashboard-built-in/), work remotely in SQL Server Management Studio (SSMS) from Windows.

## Administration

1. **Has Microsoft created an app like the SQL Server Configuration Manager on Linux?**

   Yes, there is a configuration tool for SQL Server on Linux: [mssql-conf](sql-server-linux-configure-mssql-conf.md).

1. **Does SQL Server on Linux support multiple instances on the same host?**

   We recommend running multiple containers on a host to have multiple distinct instances. Each container will need to listen on a different port. For more information, see [Run multiple SQL Server containers](sql-server-linux-configure-docker.md#run-multiple-sql-server-containers).

1. **Is Active Directory Authentication supported on Linux?**

   Yes. For more information, see [Active Directory Authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md).

1. **Are Always On and clustering supported in Linux?**

   Failover clustering and high availability on Linux are achieved with Pacemaker on Linux. For more information, see [Business continuity and database recovery - SQL Server on Linux](sql-server-linux-business-continuity-dr.md).

1. **Is it possible to configure replication from Linux to Windows and vice versa?**

   Read-scale replicas can be used between Windows and Linux for one-way data replication.

1. **Is it possible to migrate existing databases in older versions of SQL Server from Windows to Linux?**

   Yes, there are [several methods](sql-server-linux-migrate-overview.md) of achieving this.

1. **Can I migrate my data from Oracle and other database engines to SQL Server on Linux?**

   Yes. SSMA supports migration from several types of database engines: Microsoft Access, DB2, MySQL, Oracle, and SAP ASE (formerly SAP Sybase ASE). For an example of how to use SSMA, see [Migrate an Oracle schema to SQL Server 2017 on Linux with the SQL Server Migration Assistant](../ssma/oracle/sql-server-linux-convert-from-oracle.md?toc=%2fsql%2flinux%2ftoc.json).

1. **What permissions are required for SQL Server files?**

   All files in the `/var/opt/mssql` file folder should be owned by the **mssql** user and belong to the **mssql** group. Both the **mssql** user and group should have read-write permissions of all files and directories. Note the following special scenarios involving file and directory permissions:

   * Permissions for mssql owner and group are required for mounted network shares that are used to store SQL Server files.
   * If you locate database files or backups in a non-default directory, you must also set permissions for that directory.
   * If you change the default root umask from 0022, SQL Server configuration fails after installation. You must then manually apply required permissions to SQL Server startup account.

1. **Can I change the ownership of SQL Server files and directories from the installed mssql account and group?**

   We do not support changing the ownership of SQL Server directory and files from the default installation. The mssql account and group is specifically used for SQL Server and has no interactive login access.

## Next steps

For more information about running SQL Server on Linux, see the [Overview of SQL Server on Linux](sql-server-linux-overview.md).