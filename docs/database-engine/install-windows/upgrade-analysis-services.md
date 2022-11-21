---
title: "Upgrade Analysis Services"
description: "Upgrade Analysis Services"
author: "Minewiskan"
ms.author: "owend"
ms.date: "12/09/2020"
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "upgrading databases"
  - "databases [Analysis Services], upgrading"
  - "installing Analysis Services, side-by-side installations"
  - "Analysis Services upgrades"
  - "Analysis Services upgrades, about upgrading Analysis Services"
  - "SSAS, database migration"
  - "upgrading Analysis Services"
  - "installing Analysis Services, upgrading"
  - "SSAS, upgrading"
monikerRange: ">=sql-server-2016"
---
# Upgrade Analysis Services

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  
  Analysis Services instances can be upgraded to a SQL Server version of the same server mode to take advantage of features introduced in the current release, as described in [What's new in Analysis Services](/analysis-services/what-s-new-in-analysis-services).  
  
 You can upgrade each instance in-place, independently of other instances running on the same hardware. However, most administrators choose to install a new instance of the new version for application testing before transferring production workloads onto the new server. But for development or test servers, an in-place upgrade might be more convenient.  
  
## Server upgrade  
 There are two basic approaches for upgrading servers and databases:  
  
> [!NOTE]
> The compatibility levels of databases that are attached to a given server remain the same unless you manually change them.
   
  
### In-place upgrade  
 The upgrade process automatically migrates existing databases from the old instance to the new instance. Because the metadata and binary data is compatible between the two versions, you will retain the data after you upgrade and you do not have to manually migrate the data.  
  
 To upgrade an existing instance, run Setup and specify the name of the existing instance as the name of the new instance.  
  
### Side-by-side upgrade  
  
-   Backup all databases and verify that each can be restored. To learn more, see [Backup and restore Analysis Services databases](/analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases).  
  
-   Identify a subset of reports, spreadsheets, or dashboard snapshots to use later as the basis for confirming post-upgrade server operations. If possible, collect performance measurements so that  you can run comparisons against the same workloads on an upgraded server.  
  
-   Install a new instance of Analysis Services, choosing the same server mode (tabular or multidimensional) as the server you intend to replace. 
  
     Follow post-installation tasks for configuring ports and adding server administrators. To learn more, see [Post-install configuration &#40;Analysis Services&#41;](/analysis-services/instances/post-install-configuration-analysis-services).  
  
-   Attach or restore each database.  
  
-   Run DBCC to check for database integrity. Tabular models undergo more thorough checking, with tests for orphaned objects throughout the model hierarchy. For multidimensional models, only the partition indexes are checked. To learn more, see [Database Consistency Checker &#40;DBCC&#41; for Analysis Services tabular and multidimensional databases](/analysis-services/instances/database-consistency-checker-dbcc-for-analysis-services).  
  
-   Test reports, spreadsheets, and dashboards to confirm there is no adverse change to behavior or calculations. You should see faster performance for both multidimensional and tabular workloads.  
  
-   Test processing operations, correcting any login or permission issues. If you are using default service account for connections, the new service runs under a different account. To learn more, see [Configure service accounts &#40;Analysis Services&#41;](/analysis-services/instances/configure-service-accounts-analysis-services).  
  
-   Test backup and restore operations on the upgraded server, adjusting scripts to use the new server name.  
  
## Database upgrade  
 Databases that were created in previous versions run on the upgraded server with the original compatibility level setting. Generally, you can upgrade a database or model to operate at a higher compatibility level to gain access to new features, but be aware that doing so binds you to a specific server version.  
  
 To upgrade a database, you typically upgrade the model in SQL Server Data Tools (SSDT) and then deploy the solution to an upgraded server instance.
  
 Tabular and multidimensional databases follow different version paths. It's coincidental that both multidimensional and tabular models have similar numbered compatibility levels.  Modes will advance at different rates if feature changes impact only one of them.  
  
 For background purposes, the following table summarizes the compatibility levels, but you should review the detail articles to understand what each level provides.  
  
|Database model|Compatibility level|Compatible versions|  
|-|-|-|  
|Tabular|1500|SQL Server 2019|
|Tabular|1400|SQL Server 2017|
|Tabular|1200|SQL Server 2016|  
|Tabular|1103|SQL Server 2014|  
|Tabular|1100|SQL Server 2012|  
|Multidimensional|1100|SQL Server 2012 and later|  
|Multidimensional|1050|SQL Server 2005, 2008, 2008 R2|  
  
 To learn more, see [Compatibility level of a multidimensional database &#40;Analysis Services&#41;](/analysis-services/multidimensional-models/compatibility-level-of-a-multidimensional-database-analysis-services) and [Compatibility level for Analysis Services tabular models](/analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services) for more information.  
  
## See also  
 [Planning a SQL Server installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Upgrade Power Pivot for SharePoint](../../database-engine/install-windows/upgrade-power-pivot-for-sharepoint.md)   
  
