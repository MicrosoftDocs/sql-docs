---
title: "Upgrade Analysis Services | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: 
  - "sql-server-2016"
  - "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
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
ms.assetid: a131d329-386e-4470-aaa9-ffcde4e5ec0c
caps.latest.revision: 79
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Upgrade Analysis Services
  Analysis Services instances can be upgraded to a SQL Server version of the same server mode to take advantage of features introduced in the current release, as described in [What's New in Analysis Services](../../analysis-services/what-s-new-in-analysis-services.md).  
  
 You can upgrade each instance in-place, independently of other instances running on the same hardware. However, most administrators choose to install a new instance of the new version  for application testing before transferring production workloads onto the new server. But for development or test servers, an in-place upgrade might be more convenient.  
  
 Before upgrading to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], review the following:  

- [SQL Server 2017 Release Notes](../../sql-server/sql-server-2017-release-notes.md) describes known problems and workarounds.  
- [SQL Server 2016 Release Notes](../../sql-server/sql-server-2016-release-notes.md) describes known problems and workarounds.  
- [Analysis Services Backward Compatibility](../../analysis-services/analysis-services-backward-compatibility.md) summarizes discontinued, deprecated, and changed features. You should review these lists periodically to assess the impact of product changes to your models, scripts, or custom code. Typically, feature transitions are announced during pre-release of the next major release.  
  
## Server Upgrade  
 There are two basic approaches for upgrading servers and databases:  
  
-   **In-place upgrades** replace the existing program files with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] program files. Databases remain in the same location. Program folders are updated to reflect the new name.  
  
-   **Side-by-side upgrades** create a new installation of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], usually on the same computer unless you are upgrading hardware at the same time. This approach requires you to move databases over to the new instance, and then optionally uninstall the previous version to free up disk space.  
  
 The compatibility levels of databases that are attached to a given server remain the same unless you manually change them.  
  
### In-place upgrade  
 You can upgrade an existing instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and, as part of the upgrade process, automatically migrate existing databases from the old instance to the new instance. Because the metadata and binary data is compatible between the two versions, you will retain the data after you upgrade and you do not have to manually migrate the data.  
  
 To upgrade an existing instance, run Setup and specify the name of the existing instance as the name of the new instance.  
  
### Side-by-side upgrade  
  
-   Backup all databases and verify that each can be restored. See [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md).  
  
-   Identify a subset of reports, spreadsheets, or dashboard snapshots to use later as the basis for confirming post-upgrade server operations. If possible, collect performance measurements so that  you can run comparisons against the same workloads on an upgraded server.  
  
-   Install a new instance of Analysis Services, choosing the same server mode (Tabular or Multidimensional) as the server you intend to replace. See [Install Analysis Services](../../analysis-services/instances/install-windows/install-analysis-services.md).  
  
     Follow post-installation tasks for configuring ports and adding server administrators. See [Post-install Configuration &#40;Analysis Services&#41;](../../analysis-services/instances/post-install-configuration-analysis-services.md).  
  
-   Attach or restore each database.  
  
-   Run DBCC to check for database integrity. Tabular models undergo more thorough checking, with tests for orphaned objects throughout the model hierarchy. For multidimensional models, only the partition indexes are checked. See [Database Consistency Checker &#40;DBCC&#41; for Analysis Services tabular and multidimensional databases](../../analysis-services/instances/database-consistency-checker-dbcc-for-analysis-services.md).  
  
-   Test reports, spreadsheets, and dashboards to confirm there is no adverse change to behavior or calculations. You should see faster performance for both multidimensional and tabular workloads.  
  
-   Test processing operations, correcting any login or permission issues. If you are using default service account for connections, the new service runs under a different account. See [Configure Service Accounts &#40;Analysis Services&#41;](../../analysis-services/instances/configure-service-accounts-analysis-services.md) for more information about startup accounts for Analysis Services.  
  
-   Test backup and restore operations on the upgraded server, adjusting scripts to use the new server name.  
  
## Database upgrade  
 Databases that were created in previous versions of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] run on the upgraded server under an older database compatibility level setting. Generally, you can upgrade a database or model to operate at a higher compatibility level to gain access to new features, but be aware that doing so binds you to a specific server version.  
  
 To upgrade a database, you typically upgrade the model in SQL Server Data Tools (SSDT) and then deploy the solution to an upgraded server instance. See [Download SQL Server Data Tools](https://msdn.microsoft.com/library/mt204009.aspx) to get the newest version.  
  
 Tabular and multidimensional databases follow different version paths. It's coincidental that both multidimensional and tabular models have compatibility level 1100.  Modes will advance at different rates if feature changes impact only one of them.  
  
 For background purposes, the following table summarizes the compatibility levels, but you should review the detail topics to understand what each level provides.  
  
||||  
|-|-|-|  
|Tabular|1200|SQL Server 2016|  
|Tabular|1103|SQL Server 2014|  
|Tabular|1100|SQL Server 2012|  
|Multidimensional|1100|SQL Server 2012 and later|  
|Multidimensional|1050|SQL Server 2005, 2008, 2008 R2|  
  
 See [Compatibility Level of a Multidimensional Database &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/compatibility-level-of-a-multidimensional-database-analysis-services.md) and [Compatibility Level for Tabular models in Analysis Services](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md) for more information.  
  
## Tabular model upgrade to 1200 Compatibility Level  
 Tabular models and databases benefit the most from SQL Server 2016. This release offers a revised DirectQuery mode for Tabular models at compatibility level 1200, simplified by the removal of hybrid mode, the addition of query statements for  retrieving a subset of data at design-time, and row-level security via DAX instead of row permissions in the backend database.  
  
 A second reason to upgrade is the new tabular metadata construction inside the model. A Tabular model at the new compatibility level 1200, whether created at or upgraded to that level, uses native terminology for object definitions, such as model, table, relationships, and columns, to describe its major elements.  
  
 To upgrade a Tabular model, use a version of [SQL Server Data Tools (SSDT)](https://msdn.microsoft.com/library/mt204009.aspx) built for this release to change the **Compatibility Level** property to **SQL Server 2016 RTM (1200)**.  
  
 Do not use SSMS, code, or script to change the **CompatibilityLevel**. By itself, changing the property does nothing. Metadata conversion occurs in SSDT in response to the property update, followed by reopening project.  
  
 As always, be sure to save a backup of your model before upgrading in case you need to revert to the pre-upgraded version.  
  
1.  In SSDT > Solution Explorer, right-click **model.bim**, choose **View Code**, acknowledge that the model will be closed and reopened in a new window (the code window).  
  
2.  The model opens as an XMLA document. For comparison purposes post-conversion, copy the contents to another file (you can open a new XML file in SSDT).  
  
3.  Right-click **model.bim** and change it back to **View Designer**.  
  
4.  Set the **CompatibilityLevel** to  **SQL Server 2016 RTM (1200)**.  
  
5.  This step cannot be reversed so you are asked to confirm the action. Click **Yes** to proceed. The project will be refreshed.  
  
6.  Right-click **model.bim** and change it back to **View Code**.  
  
     Notice the model definition is now in JSON, using tabular metadata.  
  
 **Metadata Conversion**  
  
 Comparing post and pre-conversion metadata, you will notice that metadata is converted to JSON and trimmed of redundant definitions.  
  
 The model retains all functionality: data bindings, partition slices, expressions, object identifiers, object names, descriptions, captions, translations, and annotations are intact. But if you have code or script that references specific objects, part of the code rewrite will include removing references to objects that no longer exist. For example, a 1050 or 1103 model will have sections for dimensions that are external to the cube, whereas a 1200 model defines a table as a single object.  
  
> [!NOTE]  
>  Older Tabular compatibility levels of 1050 and 1103 are supported but deprecated. In some future release of SQL Server, tabular models cast as multidimensional objects will no longer be supported. See [Deprecated Analysis Services Features in SQL Server 2016](../../analysis-services/deprecated-analysis-services-features-in-sql-server-2016.md) for the announcement.  
  
## Post-upgrade for Tabular models at 1200 CompatibilityLevel  
 After the model is converted, you'll use [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md) rather than XMLA to script database operations. TMSL is generated automatically in SSMS when the model is 1200. Custom code that targets Tabular 1200 databases should use the API defined in the Microsoft.AnalysisServces.Tabular namespace. Script and code must be written from scratch; there is no mechanism for built-in conversion. See [Analysis Services Developer Documentation](../../analysis-services/analysis-services-developer-documentation.md) for help in getting started.  
  
 You can also add the following features to a Tabular model, supported only at compatibility level 1200:  
  
-   A DirectQuery implementation that supports row-level security via DAX in the model, more data sources, data subsets for modeling purposes, and simpler configuration.  
  
-   Calculated Columns  
  
-   Display Folders  
  
## Upgrade Tabular models in DirectQuery mode  
 You cannot do an in-place upgrade of  older Tabular models configured for DirectQuery. The new implementation of DirectQuery has a smaller configuration footprint and not all settings can be ported.  
  
1.  In SSDT, turn off **DirectQuery** mode so that the model uses in-memory storage. See [Enable DirectQuery Design Mode (SSAS Tabular)](https://msdn.microsoft.com/library/hh270245.aspx) for instructions.  
  
2.  Set **CompatibilityLevel** to SQL Server 2016 (RTM) 1200.  
  
3.  Save and rebuild or deploy the model.  
  
4.  Turn **DirectQuery** back on. See [DirectQuery for Tabular 1200 models](http://msdn.microsoft.com/library/4227977e-7368-4d45-b78f-24076882e7a8) for more guidance.  
  
## See Also  
 [DirectQuery Mode &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md)   
 [What's New in Analysis Services](../../analysis-services/what-s-new-in-analysis-services.md)   
 [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md)   
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Upgrade Power Pivot for SharePoint](../../database-engine/install-windows/upgrade-power-pivot-for-sharepoint.md)   
 [Install Analysis Services in Multidimensional and Data Mining Mode](http://msdn.microsoft.com/library/8a1f33e8-2bd6-4fb8-bd46-c86f2a067f60)   
 [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)  
  
  
