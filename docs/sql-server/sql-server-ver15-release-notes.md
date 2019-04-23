---
title: "SQL Server 2019 Release Notes | Microsoft Docs"
ms.date: 04/23/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "= sql-server-ver15 || = sqlallproducts-allversions"
---
# SQL Server 2019 preview release notes
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes limitations and known issues for the [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] Community Technology Preview (CTP) releases. For related information, see:
- [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md)

> [!NOTE]
> Preview releases of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] are made available for you to experience the features of the upcoming release. They are not supported or licensed for production use. The following scenarios are explicitly unsupported:
>
> - Side-by-side installation with other versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]
> - Upgrade an existing instance of SQL Server from any version

**Try [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)]!**
- [![Download from Evaluation Center](../includes/media/download2.png)](https://go.microsoft.com/fwlink/?LinkID=862101) [Download [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] to install on Windows](https://go.microsoft.com/fwlink/?LinkID=862101)
- Install on Linux for [Red Hat Enterprise Server](../linux/quickstart-install-connect-red-hat.md), [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md), and [Ubuntu](../linux/quickstart-install-connect-ubuntu.md).
- [Run on SQL Server 2019 on Docker](../linux/quickstart-install-connect-docker.md).

## CTP 2.5

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.5 is the latest public release of [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)].

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.5 is available only as Evaluation Edition. No other editions are available. Support for CTP releases is described in `license_Eval.rtf` with your installation media.

Limited support may be found at one of the following locations:

- Forums
  - [SQL Server Feedback](https://aka.ms/sqlfeedback)
  - [Getting started with SQL Server](https://social.msdn.microsoft.com/Forums/sqlserver/en-US/home?forum=sqlgetstarted)
  - [Transact-SQL](https://social.msdn.microsoft.com/Forums/sqlserver/en-US/home?forum=transactsql)
  - [SQL Server Documentation](https://social.msdn.microsoft.com/Forums/sqlserver/en-US/home?forum=sqldocumentation)

- Or tweet [@SQLServer](https://twitter.com/SQLServer) with [#sqlhelp](https://twitter.com/search?q=%23sqlhelp)

### Documentation (CTP 2.5)

- **Issue and customer impact**: Documentation for SQL Server 2019 (15.x) is limited and content is included with the [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] documentation set. Content in articles that is specific to SQL Server 2019 (15.x) is noted with **Applies to**.

- **Issue and customer impact**: [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] documentation can be filtered by version. Use the control at the top left of each documentation page to filter for your requirements.

- **Issue and customer impact**: No offline content is available for SQL Server 2019 (15.x).

### Hardware and software requirements (CTP 2.5)

- **Issue and customer impact**: Hardware and software requirements are still being reviewed and not final for the product release.

  - **Hardware**
    - [Windows - processor, memory, and operating system requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#pmosr)
    - [Linux - system requirements](../linux/sql-server-linux-setup.md#system)
  - **Software**
    - Windows Server 2016 or later. For additional requirements, see [Requirements for Installing SQL Server](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
    - Microsoft .NET Framework 4.6.2. Available from [Download Center](https://www.microsoft.com/download/details.aspx?id=53344).
    - For Linux, refer to [Linux - supported platforms](../linux/sql-server-linux-setup.md#supportedplatforms)

### Updated compiler

- **Issue and customer impact**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] is built with an updated compiler. CTP 2.1 had a known issue where results for floating point and other conversion scenarios may have returned a different value than previous versions because of the updated compiler. CTP 2.2 includes work to ensure that the affected scenarios return the same results as previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. As of CTP 2.5 release we do not know any remaining issues. Please report any result anomalies compared to [!INCLUDE[ss2017](../includes/sssqlv14-md.md)] to [[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] team](http://aka.ms/sqlfeedback) immediately.

- **Workaround**: N/A

- **Applies to**: SQL Server 2019 CTP 2.5, CTP 2.4, CTP 2.3, CTP 2.2, CTP 2.1

### UTF-8 collations

- **Issue and customer impact**: UTF-8 enabled collations cannot be used with some other [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features. UTF-8 is not supported when the following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features are in use:

  - Linked Server
  - In-memory OLTP
  - External Table for PolyBase
  - Always Encrypted

  > [!Note]
  > There is currently no UI support to choose UTF-8 enabled collations in Azure Data Studio or SQL Server Data Tools (SSDT). The latest [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] (SSMS) version supports choice of UTF-8 enabled collations in the UI.
 
- **Workaround**: No workaround for [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTPs.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.4, CTP 2.3, CTP 2.2, CTP 2.1, CTP 2.0.

### SQL Graph

- **Issue and customer impact**: Tools that are dependent on DacFx like import-export, will not work for the new graph features - Edge Constraints or Merge DML. Scripting in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] may not work.

- **Workaround**: Writing [!INCLUDE[tsql](../includes/tsql-md.md)] scripts and running them against the server using [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or SQLCMD will work. Exporting or Importing database objects that create Edge constraints, have the new merge DML syntax, or create derived tables/views on graph objects will not work. Users will have to manually create such objects in their database using [!INCLUDE[tsql](../includes/tsql-md.md)] scripts. 

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.4, CTP 2.3, CTP 2.2, CTP 2.1, 2.0.

### Always Encrypted with secure enclaves

- **Issue and customer impact**: Rich computations are pending several performance optimizations, include limited functionality (no indexing, etc.), and are currently disabled by default.

- **Workaround**: To enable rich computations, run `DBCC traceon(127,-1)`. For details, see  [Enable rich computations](../relational-databases/security/encryption/configure-always-encrypted-enclaves.md#configure-a-secure-enclave).

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)]  CTP 2.4, CTP 2.3, 2.2, CTP 2.1, 2.0.

### System Dynamic Management Views

- **Issue and customer impact**: The system table-valued function [sys.dm_db_objects_disabled_on_compatibility_level_change](../relational-databases/system-dynamic-management-views/spatial-data-sys-dm-db-objects-disabled-on-compatibility-level-change.md) returns random values in the `dependency` column.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)]  CTP 2.4, CTP 2.3.

### SQL Server Integration Services (SSIS)

- **Issue / customer impact**: The `SQL Server Destination` in an SSIS Package reports the following error when connecting to a database on [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 2.5.

  `SSIS Error Code DTS_E_OLEDBERROR.  An OLE DB error has occurred. Error code: 0x80040E14. An OLE DB record is available.  Source: "Microsoft OLE DB Driver for SQL Server"  Hresult: 0x80040E14  Description: "Cannot fetch a row from OLE DB provider "BULK" for linked server "(null)".`

  `An OLE DB record is available.  Source: "Microsoft OLE DB Driver for SQL Server"  Hresult: 0x80040E14  Description: "The OLE DB provider "BULK" for linked server "(null)" reported an error. The provider did not give any information about the error."`

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP2.5.

[!INCLUDE[get-help-options-msft-only](../includes/paragraph-content/get-help-options.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
