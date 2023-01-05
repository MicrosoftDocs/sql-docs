---
description: "Setting Project Options (MySQLToSQL)"
title: "Setting Project Options (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Setting project options,configuration options"
ms.assetid: 08820d88-e157-4d49-9401-38580dd7ec2d
author: cpichuka 
ms.author: cpichuka 
---
# Setting Project Options (MySQLToSQL)
For each SSMA project, you can set project-level options. These options specify how objects are converted, how data is migrated, and how source data types map to target data types.  Before you convert objects to SQL Server or SQL Azure or migrate data into SQL Server or SQL Azure, verify that the configuration options are appropriate for the project.  
  
SSMA lets you configure the default options for all projects. These options are applied to any new project that you create. You can then customize the options for each project.  
  
## Configuration Options and Modes  
SSMA has five sets of project settings:  
  
-   Project Information  
  
-   General (Conversion, Migration and SQL Azure)  
  
-   Synchronization  
  
-   GUI  
  
-   Type Mapping  
  
The project settings can be configured in four ways:  
  
-   Default  
  
-   Optimistic  
  
-   Full  
  
-   Custom  
  
The Default mode is recommended for most users. The Optimistic mode keeps more of the current MySQL syntax, and is easier to read. However, keeping current syntax might not be accurate. If the MySQL syntax must be converted to equivalent SQL Server or SQL Azure syntax, the Full mode performs the most complete conversion. The resulting code, however, might be more difficult to read. In the Custom mode, you can set the options.  
  
For more information about the settings and how the settings are applied in each mode, see the following topics:  
  
-   [Project Settings &#40;Conversion&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-conversion-mysqltosql.md)  
  
-   [Project Settings &#40;Migration&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-migration-mysqltosql.md)  
  
-   [Project Settings (GUI) (SSMA Common)](../sybase/project-settings-gui-sybasetosql.md)  
  
-   [Project Settings &#40;Type Mapping&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-type-mapping-mysqltosql.md)  
  
-   [Project Settings &#40;Synchronization&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-synchronization-mysqltosql.md)  
  
-   [Project Settings &#40;Azure SQL Database&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-azure-sql-db-mysqltosql.md)  
  
## Setting Project Options  
In SSMA, you can configure default settings for all projects. These settings are saved to the SSMA configuration file and applied to any new project that you create.  
  
**To set default project options**  
  
1.  On the **Tools** menu, click **Default Project Settings**.  
  
2.  In the **Default Project Settings** dialog box, use one of the following procedures:  
  
    1.  Select migration project type for which settings are required to be viewed / changed from **Migration Target Version** drop down, click **General** at the bottom of the left pane, and then select **Conversion or Migration or SQL Azure** option.  
  
    2.  To select a predefined mode, select **Default**, **Optimistic**, or **Full** from the **Mode** drop-down box.  
  
    3.  To specify custom settings, select or enter the new settings or values.  
  
3.  Click **OK** to save the settings.  
  
You can also customize the settings for the current project. The settings get saved to the current project file.  
  
**To customize settings for the current project**  
  
1.  On the **Tools** menu, click **ProjectSettings**.  
  
2.  In the **ProjectSettings** dialog box, use one of the following procedures:  
  
    1.  To select a predefined mode, select **Default**, **Optimistic**, or **Full** from the **Mode** drop-down box.  
  
    2.  To specify a custom mode, select **Custom** from the **Mode** drop-down box. And then select the appropriate project settings.  
  
3.  Click **OK** to save the settings.  
  
## Next Step  
The next step in the migration depends on your project needs:  
  
-   To customize the mapping of source and target data types, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md)  
  
-   Otherwise, you can convert the MySQL database object definitions into SQL Server or SQL Azure object definitions. For more information, see [Converting MySQL Databases &#40;MySQLToSQL&#41;](../../ssma/mysql/converting-mysql-databases-mysqltosql.md)  
  
## See Also  
[Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md)  
