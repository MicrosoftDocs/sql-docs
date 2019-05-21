---
title: "SQL Server 2019 Release Notes | Microsoft Docs"
ms.date: 05/22/2019
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

## CTP 3.0

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.0 is the latest public release of [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)].

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.0 is available only as Evaluation Edition. No other editions are available. 

Complete details about support and licensing for CTP releases are in `license_Eval.rtf` with your installation media.

[!INCLUDE[ctp-support-exclusion](../includes/ctp-support-exclusion.md)]

## Documentation

- **Issue and customer impact**: Documentation for SQL Server 2019 (15.x) is limited and content is included with the [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] documentation set. Content in articles that is specific to SQL Server 2019 (15.x) is noted with **Applies to**.

- **Issue and customer impact**: [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] documentation can be filtered by version. Use the control at the top left of each documentation page to filter for your requirements.

- **Issue and customer impact**: No offline content is available for SQL Server 2019 (15.x).

## Hardware and software requirements

- **Issue and customer impact**: Hardware and software requirements are still being reviewed and not final for the product release.

  - **Hardware**
    - [Windows - processor, memory, and operating system requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#pmosr)
    - [Linux - system requirements](../linux/sql-server-linux-setup.md#system)
  - **Software**
    - Windows Server 2016 or later. For additional requirements, see [Requirements for Installing SQL Server](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
    - Microsoft .NET Framework 4.6.2. Available from [Download Center](https://www.microsoft.com/download/details.aspx?id=53344).
    - For Linux, refer to [Linux - supported platforms](../linux/sql-server-linux-setup.md#supportedplatforms)

## <a name = "release-notes"></a>Features excluded from support

- **Issue and customer impact**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] excludes support for the following components, features, and scenarios:
  - SQL Sever Analysis Services
  - SQL Server Reporting Services
  - Always On availability groups on Kubernetes
  - Accelerated database recovery
  - Memory-optimized tempdb metadata

- **Workaround**: None. Exclusion applies to all customers, including participants in SQL Early Adopter Program.

- **Applies to**: CTP 3.0

## Updated compiler

- **Issue and customer impact**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] is built with an updated compiler. CTP 2.1 had a known issue where results for floating point and other conversion scenarios may have returned a different value than previous versions because of the updated compiler. CTP 2.2 includes work to ensure that the affected scenarios return the same results as previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. As of CTP 3.0 release we do not know any remaining issues. Please report any result anomalies compared to [!INCLUDE[ss2017](../includes/sssqlv14-md.md)] to [[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] team](http://aka.ms/sqlfeedback) immediately.

- **Workaround**: N/A

- **Applies to**: SQL Server 2019 CTP 3.0, CTP 2.5,CTP 2.4, CTP 2.3, CTP 2.2, CTP 2.1

## Installation Wizard may wait between EULA pages

- **Issue and customer impact**: During installation with Installation Wizard, the process may wait an excessive amount of time between the end user license agreement (EULA) for R Services and the EULA for Python.

- **Workaround**: Wait for the Installation Wizard to proceed. The time to wait may exceed 30 minutes.

- **Applies to**: SQL Server 2019 CTP 3.0

## UTF-8 collations

- **Issue and customer impact**: UTF-8 enabled collations cannot be used with some other [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features. UTF-8 is not supported when the following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features are in use:

  - In-memory OLTP
  - External Table for PolyBase
  - Always Encrypted

  > [!Note]
  > There is currently no UI support to choose UTF-8 enabled collations in Azure Data Studio or SQL Server Data Tools (SSDT). The latest [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] (SSMS) version 18 supports choice of UTF-8 enabled collations in the UI.
 
- **Workaround**: No workaround for [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTPs.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.0, CTP 2.5, CTP 2.4, CTP 2.3, CTP 2.2, CTP 2.1, CTP 2.0.

## Always Encrypted with secure enclaves

- **Issue and customer impact**: Rich computations are pending several performance optimizations, include limited functionality (no indexing, etc.), and are currently disabled by default.

- **Workaround**: To enable rich computations, run `DBCC traceon(127,-1)`. For details, see  [Enable rich computations](../relational-databases/security/encryption/configure-always-encrypted-enclaves.md#configure-a-secure-enclave).

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.0, CTP 2.5, CTP 2.4, CTP 2.3, 2.2, CTP 2.1, 2.0.

[!INCLUDE[get-help-options-msft-only](../includes/paragraph-content/get-help-options.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
