---
description: "Setting Conversion and Migration Options (AccessToSQL)"
title: "Setting Conversion and Migration Options (AccessToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "conversion, setting options"
  - "migration options"
  - "modes"
  - "options, conversion settings"
  - "project settings"
  - "schemas"
ms.assetid: 0a7304df-2f35-4453-96ef-7ac83dea1167
author: cpichuka 
ms.author: cpichuka 
---
# Setting Conversion and Migration Options (AccessToSQL)
For each SSMA project, you can set project-level options. These options specify how objects are converted, how data is migrated, and how source data types map to target data types. Before you convert objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure or migrate data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, verify that the configuration options are appropriate for the project.  
  
## Configuration Options and Modes  
SSMA has four sets of configuration settings and four modes for configuring these settings: Default, Optimistic, Full, and Custom. The Default mode is recommended for most users. Use the Optimistic mode for simple conversions. Use the Full mode if you want to see all messages. In the Custom mode, you set the options.  
  
The settings are described in the "User Interface Reference" section of this documentation. For more information about the settings and how the settings are applied in each mode, see the following topics :  
  
-   [Project Settings (Conversion)](./project-settings-conversion-accesstosql.md)  
  
-   [Project Settings (Migration)](./project-settings-migration-accesstosql.md)  
  
-   [Project Settings (GUI)](../sybase/project-settings-gui-sybasetosql.md)  
  
-   [Project Settings (Type Mapping)](./project-settings-type-mapping-accesstosql.md)  
  
-   [Project Settings (SQL Azure)](./project-settings-azure-sql-db-accesstosql.md)  
  
## Setting Project Options  
In SSMA, you can configure default settings for all projects. These settings are saved to the SSMA configuration file and applied to any new project that you create.  
  
**To set default project options**  
  
1.  On the **Tools** menu, select **Default Project Settings**.  
  
2.  In the **Default Project Settings** dialog box, do one of the following:  
  
    -   Select migration project type for which settings are required to be viewed / changed from **Migration Target Version** drop down, click **General** at the bottom of the left pane, and then select **Conversion or Migration or SQL Azure**.  
  
        > [!NOTE]  
        > SQL Azure option is available in the **General** tab only if the project type created is SQL Azure.  
  
    -   To select a pre-defined mode, select **Default**, **Optimistic**, or **Full** in the **Mode** drop-down box.  
  
    -   To specify a custom mode, select **Custom** in the **Mode** box, select an option in the left pane, click the setting or value in the right pane, and then select or enter the new setting or value.  
  
3.  Click **OK** to save the settings.  
  
You can also customize settings for the current project. These settings are saved to the current project file.  
  
**To customize settings for the current project**  
  
1.  On the **Tools** menu, select **Project Settings**.  
  
2.  In the **Project Settings** dialog box, do one of the following:  
  
    -   To select a pre-defined mode, select **Default**, **Optimistic**, or **Full** in the **Mode** drop-down box.  
  
    -   To specify a custom mode, select **Custom** in the **Mode** box, select an option in the left pane, click the setting or value in the right pane, and then select or enter the new setting or value.  
  
3.  Click **OK** to save the settings.  
  
## Next Steps  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping of source and target data types, see [Mapping Source and Target Data Types](mapping-source-and-target-data-types-accesstosql.md)  
  
-   To customize the mapping of source and target databases, see [Mapping Source and Target Databases](mapping-source-and-target-databases-accesstosql.md)  
  
-   Otherwise, you can convert the Access database object definitions into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure object definitions. For more information, see [Converting Access Database Objects](converting-access-database-objects-accesstosql.md)  
  
## See Also  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
