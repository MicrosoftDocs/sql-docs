---
title: Release notes for (SSMS) 19
description: Release notes for SQL Server Management Studio (SSMS) 19.
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: ssms
ms.topic: conceptual
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 05/24/2022
---

# Release notes for SQL Server Management Studio (SSMS) 19

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article provides details about updates, improvements, and bug fixes for the current and previous versions of SSMS.

[!INCLUDE[ssms-connect-aazure-ad](../includes/ssms-connect-azure-ad.md)]

## Current SSMS release

:::image type="icon" source="media/download-icon.png" border="false":::**[Download SQL Server Management Studio (SSMS) 19](https://aka.ms/ssmsfullsetup)**

SSMS 19 is the latest general availability (GA) release of SSMS. If you need a previous version of SSMS, see [previous SSMS releases](release-notes-ssms.md#previous-ssms-releases).

### 19.0

- Release number: 19.0
- Build number: 16.0.19054.0
- Release date: May 24, 2022

[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2189054&clcid=0x40a)

#### What's new in 19.0

| New Item | Details |
|----------|---------|
| Azure Data Studio installation integration | Installation of SSMS installs Azure Data Studio 1.35.0. |
| Data Classification | Introduced Set Microsoft Information Protection Policy menu option. |
| Data Classification | Added M365 authentication window to set MIP policy. |
| Data Classification | Classify Data page and Add Classification page display the M365 sensitivity labels when in MIP policy mode. |
| Data Classification | Added warning icon against the columns if the prior classification and current Information Protection policy mode doesn’t match. |
| Link feature for Azure SQL Managed Instance | Introduced connection wizard to assist with establishing a hybrid link between SQL Server and Managed Instance.  See [Link feature for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/link-feature). |
| Link feature for Azure SQL Managed Instance | Introduced failover wizard to assist with migration of databases from SQL Server to Azure SQL Managed Instance using the link feature. |
| Link feature for Azure SQL Managed Instance | Introduced dashboard to monitor the status of the hybrid link established between SQL Server and Managed Instance. |

#### Known issues (19.0)

| New Item | Details | Workaround |
|----------|---------|------------|
| Analysis Services | In rare cases when using upgrade setup, there may be an "Object not set to instance of an object" error when attempting to open the DAX editor after upgrading SSMS. | Uninstalling and reinstalling SQL Server Management Studio.  If not solved by reinstallation, close all instances of SSMS, backup and then remove `%AppData%\Microsoft\SQL Server Management Studio` and `%LocalAppData%\Microsoft\SQL Server Management Studio`. |
| Availability Group Dashboard | Connecting to the Availability Group Dashboard for an AG on SQL Server 2016 and below results in "unknown property ClusterType" error. | Use SSMS 18.10 to access the AG Dashboard for SQL Server 2016 and earlier. |
| Database Designer | Clicking the Design option for a view that references a table using spatial data causes SSMS to crash. | Use T-SQL to make changes to the view. |
| General SSMS | SSMS Extensions using SMO should be recompiled targeting the new SSMS-specific SMO v161 package. A preview version is available at https://www.nuget.org/packages/Microsoft.SqlServer.SqlManagementObjects.SSMS/ </br></br> Extensions compiled against previous 160 versions of Microsoft.SqlServer.SqlManagementObjects package will still function. | N/A |
| Integration Services | When importing or exporting packages in Integration Services or exporting packages in Azure-SSIS Integration Runtime, scripts are lost for packages containing script tasks/components. | Remove folder "C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\CommonExtensions\MSBuild". |
| Stretch DB | Unable to stretch an existing table using the Stretch DB Wizard. | Use T-SQL or an earlier version of SSMS (18.9.1 or below) to stretch an existing table. |

You can reference [SQL Server user feedback](https://feedback.azure.com/forums/908035-sql-server) for other known issues and to provide feedback to the product team.

## Next steps

For a list of all SQL Server Management Studio downloads, search the [Microsoft Download Center](https://www.microsoft.com/download/search.aspx?q=sql%20server%20management%20studio&p=0&r=10&t=&s=Relevancy~Descending).  
  
For the latest release of SQL Server Management Studio, For details, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).
