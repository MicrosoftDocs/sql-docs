---
title: "Getting Started with SSMA for Oracle (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "SSMA for Oracle, Metadata Explorers"
  - "SSMA for Oracle, Toolbars"
ms.assetid: df79664c-972e-4bef-865a-ce609789fee7
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Getting Started with SSMA for Oracle (OracleToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle lets you quickly convert Oracle database schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schemas, upload the resulting schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrate data from Oracle to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
This topic introduces the installation process, and then helps familiarize you with the SSMA user interface.  
  
## Installing SSMA  
To use SSMA, you first must install the SSMA client program on a computer that can access both the source Oracle database and the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You then must install an extension pack and at least one of the Oracle providers (OLE DB or [!INCLUDE[vstecado](../../includes/vstecado_md.md)]) on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These components support data migration and the emulation of Oracle system functions. For installation instructions, see [Installing SSMA  for Oracle &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-for-oracle-oracletosql.md).  
  
To start SSMA, click **Start**, point to **All Programs**, point to **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for Oracle**, and then click **[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant for Oracle**.  
  
## SSMA for Oracle User Interface  
After SSMA is installed, you can use SSMA to migrate Oracle databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It helps to become familiar with the SSMA user interface before you start. The following diagram shows the user interface for SSMA, including the metadata explorers, metadata, toolbars, output pane, and error list pane:  
  
![SSMA for Oracle UI](../../ssma/oracle/media/ssma_oracle_ui.jpg "SSMA for Oracle UI")  
  
To start a migration, you must first create a new project. Then, you connect to an Oracle database. After a successful connection, Oracle schemas will appear in Oracle Metadata Explorer. You can then right-click objects in Oracle Metadata Explorer to perform tasks such as create reports that assess conversions to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can also perform these tasks by using the toolbars and menus.  
  
You must also connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After a successful connection, a hierarchy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases will appear in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer. After you convert Oracle schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schemas, select those converted schemas in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, and then synchronize the schemas with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
After you synchronize converted schemas with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can return to Oracle Metadata Explorer and migrate data from Oracle schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
For more information about these tasks and how to perform them, see [Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md).  
  
The following sections describe the features of the SSMA user interface.  
  
### Metadata Explorers  
SSMA contains two metadata explorers to browse and perform actions on Oracle and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
#### Oracle Metadata Explorer  
Oracle Metadata Explorer shows information about Oracle schemas. By using Oracle Metadata Explorer, you can perform the following tasks:  
  
-   Browse the objects in each schema.  
  
-   Select objects for conversion, and then convert the objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax. For more information, see [Converting Oracle Schemas &#40;OracleToSQL&#41;](../../ssma/oracle/converting-oracle-schemas-oracletosql.md).  
  
-   Select tables for data migration, and then migrate the data from those tables to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Migrating Oracle Data into SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-data-into-sql-server-oracletosql.md).  
  
#### SQL Server Metadata Explorer  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer shows information about an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA retrieves metadata about that instance and stores it in the project file.  
  
You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer to select converted Oracle database objects, and then synchronize those objects with the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
For more information, see [Loading Converted Database Objects into SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/loading-converted-database-objects-into-sql-server-oracletosql.md).  
  
### Metadata  
To the right of each metadata explorer are tabs that describe the selected object. For example, if you select a table in Oracle Metadata Explorer, six tabs will appear: **Table**, **SQL**, **Type Mapping,Report**, **Properties**, and **Data**. The **Report** tab contains information only after you create a report that contains the selected object. If you select a table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, three tabs will appear: **Table**, **SQL**, and **Data**.  
  
Most metadata settings are read-only. However, you can alter the following metadata:  
  
-   In Oracle Metadata Explorer, you can alter procedures and type mappings. To convert the altered procedures and type mappings, make changes before you convert schemas.  
  
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer, you can alter the [!INCLUDE[tsql](../../includes/tsql-md.md)] for stored procedures. To see these changes in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make these changes before you load the schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
Changes made in a metadata explorer are reflected in the project metadata, not in the source or target databases.  
  
### Toolbars  
SSMA has two toolbars: a project toolbar and a migration toolbar.  
  
#### The Project Toolbar  
The project toolbar contains buttons for working with projects, connecting to Oracle, and connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These buttons resemble the commands on the **File** menu.  
  
#### Migration Toolbar  
The following table shows the migration toolbar commands:  
  
|Button|Function|  
|------|--------|  
|**Create Report**|Converts the selected Oracle objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] syntax, and then creates a report that shows how successful the conversion was.<br /><br />This command is disabled unless objects are selected in Oracle Metadata Explorer.|  
|**Convert Schema**|Converts the selected Oracle objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.<br /><br />This command is disabled unless objects are selected in Oracle Metadata Explorer.|  
|**Migrate Data**|Migrates data from the Oracle database to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Before you run this command, you must convert the Oracle schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schemas, and then load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />This command is disabled unless objects are selected in Oracle Metadata Explorer.|  
|**Stop**|Stops the current process.|  
  
### Menus  
The following table shows the SSMA menus.  
  
|Menu|Description|  
|----|-----------|  
|**File**|Contains commands for working with projects, connecting to Oracle, and connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**Edit**|Contains commands for finding and working with text in the details pages, such as copying [!INCLUDE[tsql](../../includes/tsql-md.md)] from the SQL details pane. Also contains the **Manage Bookmarks** option, where you will be able to see a list of existing bookmarks. You can use the buttons on the right side of the dialog to manage the bookmarks.|  
|**View**|Contains the **Synchronize Metadata Explorers** command. That synchronizes the objects between Oracle Metadata Explorer and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer. Also contains commands to show and hide the **Output** and **Error List** panes and an option **Layouts** to manage the Layouts.|  
|**Tools**|Contains commands to create reports, and migrate objects and data. Also provides access to the **Global Settings** and **Project Settings** dialog boxes.|  
|**Tester**|Contains commands for creating and working with test cases, repository, and backup management system.|  
|**Help**|Provides access to SSMA Help and to the **About** dialog box.|  
  
### Output Pane and Error List Pane  
The **View** menu provides commands to toggle the visibility of the Output pane and the Error List pane:  
  
-   The output pane shows status messages from SSMA during object conversion, object synchronization, and data migration.  
  
-   The Error List pane shows error, warning, and informational messages in a sortable list.  
  
## See Also  
[Migrating Oracle Data into SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-data-into-sql-server-oracletosql.md)  
[User Interface Reference &#40;OracleToSQL&#41;](../../ssma/oracle/user-interface-reference-oracletosql.md)  
  
