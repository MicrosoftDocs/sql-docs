---
title: "SQL Server 2019 Release Notes | Microsoft Docs"
ms.date: 10/07/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
ms.assetid: 13942af8-5a40-4cef-80f5-918386767a47
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-ver15 || = sqlallproducts-allversions"
---
# SQL Server 2019 preview release notes
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes limitations and known issues for the [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)]. For related information, see:

>[What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-ver15.md)

>[!NOTE]
>The content is published for the [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] release candidate. The release candidate is pre-release software. The information is subject to change. For information about support scenarios, refer to [Support](#support).

## [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] release candidate (RC)

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RC is the latest public release of [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)].

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RC is available only as Evaluation Edition. No other editions are available.

Complete details about support and licensing for release candidate software are in `license_Eval.rtf` with your installation media.

[!INCLUDE[ctp-support-exclusion](../includes/ctp-support-exclusion.md)]

## Documentation

- **Issue and customer impact**: Documentation for SQL Server 2019 (15.x) is limited and content is included with the [!INCLUDE[ssSQL17](../includes/sssql17-md.md)] documentation set. Content in articles that is specific to SQL Server 2019 (15.x) is noted with **Applies to**.

- **Issue and customer impact**: [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] documentation can be filtered by version. Use the control at the top left of each documentation page to filter for your requirements.

- **Issue and customer impact**: No offline content is available for SQL Server 2019 (15.x).

## Build number

The build number for SQL Server 2019 RC on Windows, Linux, and containers is `15.0.1900.25`.  The build number for SQL Server 2019 RC used in big data clusters is `15.0.1900.47`.

## Hardware and software requirements

- **Issue and customer impact**: Hardware and software requirements are still being reviewed and not final for the product release.

  - **Hardware**
    - [Windows - processor, memory, and operating system requirements](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#pmosr)
    - [Linux - system requirements](../linux/sql-server-linux-setup.md#system)
  - **Software**
    - Windows Server 2016 or later. For additional requirements, see [Requirements for Installing SQL Server](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
    - Microsoft .NET Framework 4.6.2. Available from [Download Center](https://www.microsoft.com/download/details.aspx?id=53344).
    - For Linux, refer to [Linux - supported platforms](../linux/sql-server-linux-setup.md#supportedplatforms)

## SQL Server installation may fail if SSMS 18.x is installed

- **Issue and customer impact**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] installation fails when the following installations happen in this order:
  1. SQL Server Management Studio (SSMS) version 18.0, 18.1, 18.2, or 18.3 is installed on the server.
  1. [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] installation is attempted from removable media. For example, the installation media is a DVD.

- **Workaround**:
  1. Uninstall any version of SSMS older than SSMS 18.3.1.
  1. Install a newer version of SSMS (18.3.1 or later). For the latest version, see [Download SSMS](../ssms/download-sql-server-management-studio-ssms.md).
  1. Install [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] normally.

  >[!NOTE]
  >Uninstall is required.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] release candidate.

## Updated compiler

- **Issue and customer impact**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] is built with an updated compiler. CTP 2.1 had a known issue where results for floating point and other conversion scenarios may have returned a different value than previous versions because of the updated compiler. CTP 2.2 includes work to ensure that the affected scenarios return the same results as previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. As of the release candidate release we do not know any remaining issues. Please report any result anomalies compared to [!INCLUDE[ss2017](../includes/sssqlv14-md.md)] to [[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] team](https://aka.ms/sqlfeedback) immediately.

- **Workaround**: N/A

- **Applies to**: Release candidate

## Installation Wizard may wait between EULA pages

- **Issue and customer impact**: During installation with Installation Wizard, the process may wait an extended amount of time between the end user license agreement (EULA) for R Services and the EULA for Python.

- **Workaround**: Wait for the Installation Wizard to proceed. The time to wait may exceed 30 minutes.

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.0

## UTF-8 collations

- **Issue and customer impact**: UTF-8 enabled collations cannot be used with some other [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features. UTF-8 is not supported when the following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features are in use:

  - In-memory OLTP
  - External Table for PolyBase ([!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RC 1)
  - Always Encrypted (Up to [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] RC 1)
  - Linked Servers (up to [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.2)

  > [!Note]
  > There is currently no UI support to choose UTF-8 enabled collations in Azure Data Studio or SQL Server Data Tools (SSDT). The latest [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] (SSMS) version 18 supports choice of UTF-8 enabled collations in the UI.
 
- **Workaround**: No workaround for [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTPs.


- **Applies to**: All CTP releases.

## Always Encrypted with secure enclaves

- **Issue and customer impact**: Rich computations are pending performance optimizations and error-handling enhancements, and are currently disabled by default.

- **Workaround**: To enable rich computations, run `DBCC traceon(127,-1)`. 

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] release candidate, CTP 3.2, CTP 3.1

## SQL Server Configuration Manager may not start

- **Issue and customer impact**: SQL Server Configuration Manager (SSCM) doesn't start on a machine without the VCRuntime 140 (VCRUNTIME140.dll) file. When starting SSCM, the user may see the following dialog: 


  `MMC could not create the snap-in. The snap-in might not have been installed correctly.`

- **Workaround**:  Install the latest VC Runtime 2013 (x86):

  - [Verbose](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads)
  - [Direct](https://support.microsoft.com/help/4032938/update-for-visual-c-2013-redistributable-package)

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] CTP 3.1, CTP 3.0, CTP 2.5.

## Always On Availability Group Kubernetes operator not supported

- **Issue and customer impact**: The Kubernetes operator for Always On Availability Groups is not supported in this release candidate and will not be available at RTM. 

- **Workaround**: None

- **Applies to**: [!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] Release candidate

## Master Data Service notification email contains broken link

- **Issue and customer impact**: The notification email from Master Data Services (MDS) contains a broken link. The link navigates to a page that returns an error like the following message:

   `The view 'Index' or its master was not found or no view engine supports the searched locations.`

- **Workaround**: Open the MDS portal and go to the resource manually.

- **Applies to**: SQL Server 2019 release candidate.

## Machine Learning Services

For issues in SQL Server Machine Learning Services, see [Known issues in SQL Server Machine Learning Services](../advanced-analytics/known-issues-for-sql-server-machine-learning-services.md).

[!INCLUDE[get-help-options-msft-only](../includes/paragraph-content/get-help-options.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)
