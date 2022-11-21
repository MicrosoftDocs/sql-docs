---
description: "Setting Project Options (SybaseToSQL)"
title: "Setting Project Options (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Project Options Setting"
ms.assetid: 97b70fc8-1f68-4f15-8e22-db5b784ea4ec
author: cpichuka 
ms.author: cpichuka 
---
# Setting Project Options (SybaseToSQL)
For each SSMA project, you can set project level options. These options specify object conversion, object loading, SQL azure, user interface, and data migration settings. Before you convert objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure or migrate data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, verify that the configuration options are appropriate for the project.  
  
SSMA lets you configure default options for all projects. These options are applied to any new project you create. You can then customize the options for each project.  
  
## Configuration Options and Modes  
SSMA has five sets of project settings:  
  
1.  Project Information  
  
2.  General (Conversion, Migration and Collecting Data)  
  
3.  Synchronization  
  
4.  GUI  
  
5.  Type Mapping  
  
It also has four modes for configuring these settings:  
  
1.  Default  
  
2.  Optimistic  
  
3.  Full  
  
4.  Custom  
  
The Default mode is recommended for most users. The Optimistic mode keeps more of the current Sybase Adaptive Server Enterprise (ASE) syntax, and is easier to read. However, keeping current syntax might not be accurate. If the ASE syntax must be converted to equivalent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure syntax, the Full mode performs a complete conversion, but the resulting code might be more difficult to read. In the Custom mode, you set the options.  
  
The settings are described in the User Interface Reference section of this documentation. For more information about the settings and how the settings are applied in each mode, see the following topics:  
  
-   [Project Settings &#40;Conversion&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-conversion-sybasetosql.md)  
  
-   [Project Settings &#40;Migration&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-migration-sybasetosql.md)  
  
-   [Project Settings &#40;Synchronization&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-synchronization-sybasetosql.md)  
  
-   [Project Settings &#40;GUI&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-gui-sybasetosql.md)  
  
-   [Project Settings &#40;Type Mapping&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-type-mapping-sybasetosql.md)  
  
-   [Project Settings &#40;Azure SQL Database &#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-azure-sql-db-sybasetosql.md)  
  
## Setting Project Options  
In SSMA, you can configure default settings for all projects. These settings are saved to the SSMA configuration file and applied to any new project that you create.  
  
**To set default project options**  
  
1.  On the **Tools** menu, select **Default Project Settings**.  
  
2.  In the **Default Project Settings** dialog box, use one of the following procedures:  
  
    -   Select migration project type for which settings are required to be viewed or changed from **Migration Target Version** drop down click General at the bottom of the left pane, and then select Conversion or Migration or SQL Azure.  
  
    -   To select a predefined mode, in the **Mode** drop-down box, select **Default**, **Optimistic**, or **Full**.  
  
    -   To specify custom settings, simply select or enter the new settings or values.  
  
3.  Click **OK** to save the settings.  
  
You can also customize settings for the current project. These settings are saved to the current project file.  
  
**To customize settings for the current project**  
  
1.  On the **Tools** menu, select **Project Settings**.  
  
2.  In the **Project Settings** dialog box, use one of the following procedures:  
  
    -   To select a predefined mode, in the **Mode** drop-down box, select **Default**, **Optimistic**, or **Full**.  
  
    -   To specify a custom mode, in the **Mode** drop down box, select **Custom**, select an option in the left pane, click the setting or value in the right pane, and then select or enter the new setting or value.  
  
3.  Click **OK** to save the settings.  
  
## Next Steps  
The next step in the migration depends on your project needs:  
  
-   If you want to custom the mapping of source and target data types, see [Mapping Sybase ASE and SQL Server Data Types &#40;SybaseToSQL&#41;](../../ssma/sybase/mapping-sybase-ase-and-sql-server-data-types-sybasetosql.md).  
  
-   Otherwise, you can convert the Sybase database object definitions into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure object definitions. For more information, see [Converting Sybase ASE Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/converting-sybase-ase-database-objects-sybasetosql.md).  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
