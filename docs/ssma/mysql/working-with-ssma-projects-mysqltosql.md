---
description: "Working with SSMA Projects (MySQLToSQL)"
title: "Working with SSMA Projects (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Working with SSMA projects, create new project"
  - "Working with SSMA projects, customize settings"
  - "Working with SSMA projects, Open project"
  - "Working with SSMA projects, Save project"
ms.assetid: 9e4394e9-f177-41d9-839e-5d53a9c9b840
author: cpichuka 
ms.author: cpichuka 
---
# Working with SSMA Projects (MySQLToSQL)
To migrate MySQL databases to SQL Server or SQL Azure, you must first create an SSMA project. The project is a file that contains the following information:  
  
-   Metadata about the MySQL databases that you want to migrate to SQL Server or SQL Azure.  
  
-   Metadata about the target instance of SQL Server or SQL Azure that will receive the migrated objects and data.  
  
-   SQL Server or SQL Azure connection information.  
  
-   Project settings.  
  
When you open a project, it is disconnected from MySQL and SQL Server or SQL Azure. That lets you work offline. For more information about reconnecting to SQL Server, see [Connecting to SQL Server &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-sql-server-mysqltosql.md)  
  
## Reviewing Default Project Settings  
SSMA contains several settings for converting and loading database, migrating data, and synchronizing SSMA with MySQL and SQL Server or SQL Azure. The default settings are appropriate for many users. However, before you create a new SSMA project, you should review the settings. If required, you can change the default settings that will be used for all your new projects.  
  
##### To review default project settings  
  
1.  Select **Default Project Settings** from the **Tools** menu.  
  
2.  Select the project type in **Migration Target Version** drop down for which settings are to be viewed/ changed and then click **General** tab.  
  
3.  In the left pane, click **Conversion**.  
  
4.  In the right pane, review and change the settings as necessary. For more information about these settings, see [Project Settings &#40;Conversion&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-conversion-mysqltosql.md) .  
  
5.  Repeat steps 1-3 for the Migration, Synchronization, SQL Azure, GUI, and Type Mapping pages.  
  
-   For information about migration settings, see [Project Settings &#40;Migration&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-migration-mysqltosql.md).  
  
-   For information about settings for Synchronization to SQL Server, see [Project Settings &#40;Synchronization&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-synchronization-mysqltosql.md).  
  
-   For information about GUI settings, see [Project Settings (GUI) (SSMA Common)](../sybase/project-settings-gui-sybasetosql.md).  
  
-   For information about data type mapping settings, see [Project Settings &#40;Type Mapping&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-type-mapping-mysqltosql.md).  
  
-   For information about SQL Azure settings, see [Project Settings &#40;Azure SQL Database&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-azure-sql-db-mysqltosql.md).  
  
> [!NOTE]  
> The SQL Azure settings will be displayed only when you select **Migration to SQL Azure** while creating a project.  
  
## Creating New Projects  
To migrate data from MySQL databases to SQL Server or SQL Azure, you must create a project.  
  
##### To create a new project  
  
1.  Select **New Project** from the **File** menu. The **New Project** dialog box appears. On the **File** menu, select **New Project**. The **New Project** dialog box appears.  
  
2.  In the **Name** box, enter a name for your project.  
  
3.  In the **Location** box, enter or select a folder for the project.  
  
4.  In the **Migration To** drop down, select the version of target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] used for migration. The options available are:  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2005  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2012  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014  
  
    -   Azure SQL Database  
  
And then Click **OK**  
  
SSMA creates the project file.  
  
## Customizing Project Settings  
In addition to defining the default project settings that apply to all the new SSMA projects you can also customize the settings for each project. For more information, see [Setting Project Options &#40;MySQLToSQL&#41;](../../ssma/mysql/setting-project-options-mysqltosql.md).  
  
When you customize data type mappings between the source and target databases, you can define mappings at the project, database, or object level. For more information, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md).  
  
## Saving Projects  
The Saving Projects feature allows the user to essentially save the project settings and, optionally, the database metadata to the SSMA project file.  
  
##### To save a project  
  
-   On the **File** menu, select **Save** Project.  
  
If databases within the project have changed or have not been converted, SSMA will prompt you to load and save metadata. Loading and saving metadata lets you work offline. It also lets you send a complete project file to other people, such as technical support personnel. If you are prompted to save metadata, do the following:  
  
1.  For each database that shows a status of **Metadata missing**, select the check box next to the database name. Saving metadata might take several minutes. If you do not want to save metadata at this point, do not select any check boxes.  
  
2.  Click **Save**.  
  
SSMA will parse the MySQL schemas and save the metadata to the project file.  
  
## Opening Projects  
When you open a project, it is disconnected from MySQL and from SQL Server or SQL Azure. This lets you work offline. To update metadata, load database objects into SQL Server or SQL Azure. To migrate data, you must reconnect to SQL Server or SQL Azure.  
  
##### To open a project  
  
1.  Use one of the following procedures:  
  
    1.  On the **File** menu, point to **Recent Projects**.  
  
    2.  Select the project you want to open.  
  
    3.  On the **File** menu, select **Open Project**, locate the .m2ssproj project file, select the file, and then click **Open**.  
  
2.  To reconnect to MySQL, on the **File** menu, select **Reconnect to MySQL**.  
  
3.  To reconnect to SQL Server, on the **File** menu, select **Reconnect to SQL Server**.  
  
4.  To reconnect to SQL Azure, on the **File** menu, select **Reconnect to SQL Azure.**  
  
## Next Step  
The next step in the migration process is [Connecting to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-mysql-mysqltosql.md)  
  
## See Also  
[Connecting to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-mysql-mysqltosql.md)  
[Migrating MySQL Databases to SQL Server - Azure SQL Database &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
[Connecting to SQL Server &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-sql-server-mysqltosql.md)  
[Connecting to Azure SQL Database &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-azure-sql-db-mysqltosql.md)  
