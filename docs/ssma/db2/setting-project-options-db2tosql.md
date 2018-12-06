---
title: "Setting Project Options (DB2ToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: f325a606-97ac-48bc-b344-b55f5e086a48
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Setting Project Options (DB2ToSQL)
For each SSMA project you can set project level options. These options specify object conversion, object loading, user interface and data migration settings. Before you convert objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or migrate data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], verify that the configuration options are appropriate for the project.  
  
SSMA lets you configure default options for all projects. These options are applied to any new project that you create. You can then customize the options for each project.  
  
## Configuration Options and Modes  
SSMA has five sets of project settings:  
  
-   Project Information  
  
-   General (Conversion, Migration, Loading Objects)  
  
-   Synchronization  
  
-   GUI  
  
-   Type Mapping  
  
It also has four modes for configuring these settings:  
  
-   Default  
  
-   Optimistic  
  
-   Full  
  
-   Custom  
  
The Default mode is recommended for most users. The Optimistic mode keeps more of the current DB2 syntax, and is easier to read. However, keeping current syntax might not be accurate. If the DB2 syntax must be converted to equivalent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, the Full mode performs the most complete conversion, but the resulting code might be more difficult to read. In the Custom mode, you set the options.  
  
For more information about the settings and how the settings are applied in each mode, see the following topics:  
  
-   [Project Settings &#40;Conversion&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-conversion-db2tosql.md)  
  
-   [Project Settings &#40;Migration&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-migration-db2tosql.md)  
  
-   [Project Settings&#40;Synchronization&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-synchronization-db2tosql.md)  
  
-   [Project Settings &#40;GUI&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-gui-db2tosql.md)  
  
-   [Project Settings &#40;Type Mapping&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-type-mapping-db2tosql.md)  
  
## Setting Project Options  
In SSMA, you can configure default settings for all projects. These settings are saved to the SSMA configuration file and applied to any new project that you create.  
  
**To set default project options**  
  
1.  On the **Tools** menu, click **Default Project Settings**.  
  
2.  In the **Default Project Settings** dialog box, use one of the following procedures:  
  
    -   Select migration project type for which settings are required to be viewed or changed from **Migration Target Version** drop down click **General** at the bottom of the left pane, and then select Conversion or Migration.  
  
    -   To select a predefined mode, in the **Mode** drop-down box, select **Default**, **Optimistic**, or **Full**.  
  
    -   To specify custom settings, select or enter the new settings or values.  
  
3.  Click **OK** to save the settings.  
  
You can also customize settings for the current project. These settings are saved to the current project file.  
  
**To customize settings for the current project**  
  
1.  On the **Tools** menu, click **Project Settings**.  
  
2.  In the **Project Settings** dialog box, use one of the following procedures:  
  
    -   To select a predefined mode, in the **Mode** drop-down box, select **Default**, **Optimistic**, or **Full**.  
  
    -   To specify a custom mode, in the **Mode** box, select **Custom**, and then select the appropriate project settings.  
  
3.  Click **OK** to save the settings.  
  
## Next Steps  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping of source and target data types, see [Mapping DB2 and SQL Server Data Types &#40;DB2ToSQL&#41;](../../ssma/db2/mapping-db2-and-sql-server-data-types-db2tosql.md).  
  
-   Otherwise, you can convert the DB2 database object definitions into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object definitions. For more information, see [Converting DB2 Schemas &#40;DB2ToSQL&#41;](../../ssma/db2/converting-db2-schemas-db2tosql.md).  
  
## See Also  
[Mapping DB2 and SQL Server Data Types &#40;DB2ToSQL&#41;](../../ssma/db2/mapping-db2-and-sql-server-data-types-db2tosql.md)  
  
