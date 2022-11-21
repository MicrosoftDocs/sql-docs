---
title: "Working with SSMA Projects (OracleToSQL) | Microsoft Docs"
description: Learn how to create an SSMA project that contains metadata for Oracle databases to migrate and SQL Server, along with settings and connection information.
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Customizing Project Settings"
ms.assetid: ee5d94c0-c7a6-4779-bd32-729bdaf61e1b
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.oracle.workplacedialog.f1"
---
# Working with SSMA Projects (OracleToSQL)
To migrate Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you first create an SSMA project. The project is a file that contains the following information:  
  
-   Metadata about the Oracle databases you want to migrate to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Metadata about the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that will receive the migrated objects and data.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection information.  
  
-   Project settings.  
  
When you open a project, it is disconnected from Oracle and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. That lets you work offline. For information about reconnecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Connecting to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-sql-server-oracletosql.md).  
  
## Reviewing Default Project Settings  
SSMA contains several settings for converting and loading database objects, migrating data, and synchronizing SSMA with Oracle and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The default settings are appropriate for many users. However, before you create a new SSMA project, you should review the settings. If you want to, you can change the default settings that will be used for all your new projects.  
  
**To review default project settings**  
  
1.  On the **Tools** menu, click **Default Project Settings**.  
  
2.  Select the project type in **Migration Target Version** drop-down for which settings are required to be viewed or changed and then Click **General** tab.  
  
3.  In the left pane, click **Conversion**.  
  
4.  In the right pane, review and change the settings as necessary. For more information about these settings, see [Project Settings &#40;Conversion&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-conversion-oracletosql.md).  
  
5.  Repeat Steps 1-3 for the Migration, Synchronization, Loading System Objects, GUI, and Type Mapping pages.  
  
    -   For information about migration settings, see [Project Settings &#40;Migration&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-migration-oracletosql.md).  
  
    -   For information about system object settings, see [Project Settings&#40;Loading System objects&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-loading-system-objects-oracletosql.md).  
  
    -   For information about settings for synchronization to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Project Settings&#40;Synchronization&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-synchronization-oracletosql.md).  
  
    -   For information about GUI settings, see [Project Settings &#40;GUI&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-gui-oracletosql.md).  
  
    -   For information about data type mapping settings, see [Project Settings &#40;Type Mapping&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-type-mapping-oracletosql.md).  
  
## Creating New Projects  
To migrate data from Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must first create a project.  
  
**To create a project**  
  
1.  On the **File** menu, click **New Project**.  
  
    The **New Project** dialog box appears.  
  
2.  In the **Name** box, enter a name for your project.  
  
3.  In the **Location** box, enter or select a folder for the project, and then click **OK**.  
  
4.  In the **Migration To** drop down, select the version of target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] used for migration. The options available are:  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016  
  
    -   Azure SQL Database  
  
## Customizing Project Settings  
In addition to defining default project settings that apply to all new SSMA projects, you can customize the settings for each project. For more information, see [Setting Project Options &#40;OracleToSQL&#41;](../../ssma/oracle/setting-project-options-oracletosql.md).  
  
When you customize data type mappings between source and target databases, you can define mappings at the project, database, or object level. For more information, see [Mapping Oracle and SQL Server Data Types &#40;OracleToSQL&#41;](../../ssma/oracle/mapping-oracle-and-sql-server-data-types-oracletosql.md).  
  
## Saving Projects  
When you save a project, SSMA retains the project settings, and optionally the database metadata, to the project file.  
  
**To save a project**  
  
-   On the **File** menu, click **Save Project**.  
  
    If schemas in the project have changed or have not been converted, SSMA will prompt you to load and save metadata. Loading and saving metadata will let you work offline. It also lets you send a complete project file to other people, such as technical support personnel. If you're prompted to save metadata, do the following:  
  
    1.  For each schema that shows a status of **Metadata missing**, select the check box next to the database name.  
  
        Saving metadata might take several minutes. If you don't want to save metadata yet, don't select any check boxes.  
  
    2.  Click the **Save** button.  
  
        SSMA will parse the Oracle schemas and save the metadata to the project file.  
  
## Opening Projects  
When you open a project, it is disconnected from Oracle and from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. That lets you work offline. To update metadata, load database objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To migrate data, you must reconnect to Oracle and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**To open a project**  
  
1.  Use one of the following procedures:  
  
    -   On the **File** menu, point to **Recent Projects**, and then click the project you want to open.  
  
    -   On the **File** menu, select **Open Project**, locate the .o2ssproj project file, select the file, and then click **Open**.  
  
2.  To reconnect to Oracle, on the **File** menu, click **Reconnect to Oracle**.  
  
3.  To reconnect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], on the **File** menu, click **Reconnect to SQL Server**.  
  
## Next Step  
The next step in the migration process is to [Connecting to Oracle Database (OracleToSQL)](./connecting-to-oracle-database-oracletosql.md).  
  
## See Also  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
[Connecting to Oracle Database &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-oracle-database-oracletosql.md)  
[Connecting to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/connecting-to-sql-server-oracletosql.md)  
