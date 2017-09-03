---
title: "Getting Started with SSMA for Sybase (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: c4098516-f0fc-4690-97bb-3766dfd43156
caps.latest.revision: 10
author: “Shamikg”
ms.author: “Shamikg”
manager: "jhubbard"
---
# Getting Started with SSMA for Sybase (SybaseToSQL)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Migration Assistant (SSMA) for Sybase lets you quickly convert Sybase Adaptive Server Enterprise (ASE) database schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure schemas, upload the resulting schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, and migrate data from Sybase ASE to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.  
  
This topic introduces the installation process, and then helps familiarize you with the SSMA user interface.  
  
## Installing and Licensing SSMA  
To use SSMA, you first must install the SSMA client program on a computer that can access both the source instance of Sybase ASE and the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. To use Server side data migration, you must install extension pack and at least one of the Sybase providers (OLE DB or ADO.NET) on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]. These components support data migration and the emulation of Sybase ASE system functions. For installation instructions, see [Installing SSMA  for Sybase &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-for-sybase-sybasetosql.md).  
  
To start SSMA, click **Start**, point to **All Programs**, point to **[!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Migration Assistant for Sybase**, and then select **[!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Migration Assistant for Sybase**. The first time that you start SSMA, a licensing dialog box will appear. You must license SSMA by using a Windows Live ID before you can use SSMA. Licensing instructions are included with the installation instructions in the [Installing SSMA  for Sybase Client &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-for-sybase-client-sybasetosql.md) topic.  
  
## SSMA for Sybase User Interface  
After SSMA is installed and licensed, you can use SSMA to migrate Sybase ASE databases to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. It helps to become familiar with the SSMA user interface before you start. The following diagram shows the user interface for SSMA, including the metadata explorers, metadata, toolbars, output pane, and error list pane:  
  
![SSMA for Sybase User Interface](../../ssma/sybase/media/ssmaforsybaseuserinterface.jpg "SSMA for Sybase User Interface")  
  
To start a migration, you must first create a new project. Then, you connect to Sybase ASE. After a successful connection, a hierarchy of Sybase ASE databases will appear in Sybase Metadata Explorer. You can then right-click objects in Sybase Metadata Explorer to do tasks such as create reports that assess conversions to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. You can also do these tasks through the toolbars and menus.  
  
You must also connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. After a successful connection, a hierarchy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure databases will appear in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer. After you convert Sybase ASE schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure schemas, select those converted schemas in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer, and then load the schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.  
  
After you load converted schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, you can return to Sybase Metadata Explorer and migrate data from Sybase ASE databases into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure databases.  
  
For more information about these tasks and how to perform them, see [Migrating Sybase ASE Databases to SQL Server - Azure SQL DB &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md).  
  
The following sections describe the features of the SSMA user interface.  
  
### Metadata Explorers  
SSMA contains two metadata explorers to browse and perform actions on Sybase ASE and [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure databases.  
  
#### Sybase Metadata Explorer  
Sybase Metadata Explorer shows information about databases on the source instance of Sybase ASE.  
  
By using Sybase Metadata Explorer, you can perform the following tasks:  
  
-   Browse the tables in each database.  
  
-   Select objects for conversion, and then convert the objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure syntax. For more information, see [Converting Sybase ASE Database Objects &#40;SybaseToSQL&#41;](../../ssma/sybase/converting-sybase-ase-database-objects-sybasetosql.md).  
  
-   Select objects for data migration, and then migrate the data from those objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. For more information, see [Migrating Sybase ASE Data into SQL Server - Azure SQL DB  &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-data-into-sql-server-azure-sql-db-sybasetosql.md).  
  
#### SQL Server or SQL Azure Metadata Explorer  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer shows information about an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. When you connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure, SSMA retrieves metadata about that instance and stores it in the project file.  
  
You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer to select converted Sybase ASE database objects, and then load (synchronize) those objects into the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.  
  
For more information, see [Loading Converted Database Objects into SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/loading-converted-database-objects-into-sql-server-sybasetosql.md).  
  
### Metadata  
To the right of each metadata explorer are tabs that describe the selected object. For example, if you select a table in Sybase Metadata Explorer, six tabs will appear: **Table**, **SQL**, **Type Mapping**, **Data, Properties** and **Report**. The **Report** tab contains information only after you create a report containing the selected object. If you select a table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer, three tabs will appear: **Table**, **SQL**, and **Data**.  
  
Most metadata settings are read-only. However, you can alter the following metadata:  
  
-   In Sybase Metadata Explorer, you can alter procedures and type mappings. You should make these changes before you convert schemas.  
  
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer, you can alter the [!INCLUDE[tsql](../../includes/tsql_md.md)] for stored procedures. You should make these changes before you load the schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
Changes made in a metadata explorer are reflected in the project metadata, not in the source or target databases.  
  
### Toolbars  
SSMA has two toolbars: a project toolbar and a migration toolbar.  
  
#### The Project Toolbar  
The project toolbar contains buttons for working with projects, connecting to Sybase ASE, and connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. These buttons resemble the commands on the **File** menu.  
  
#### Migration Toolbar  
The migration toolbar contains the following commands:  
  
|Button|Function|  
|----------|------------|  
|**Create Report**|Converts the selected Sybase ASE objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] syntax, and then creates a report that shows how successful the conversion was.<br /><br />This command is available only when objects are selected in Sybase Metadata Explorer.|  
|**Convert Schema**|Converts the selected Sybase ASE objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure objects.<br /><br />This command is available only when objects are selected in Sybase Metadata Explorer.|  
|**Migrate Data**|Migrates data from the Sybase ASE database to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure. Before you run this command, you must convert the Sybase schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure schemas, and then load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.<br /><br />This command is available only when objects are selected in Sybase Metadata Explorer.|  
|**Stop**|Stops the current process, such as converting objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure syntax.|  
  
### Menus  
SSMA contains the following menus.  
  
|Menu|Description|  
|--------|---------------|  
|**File**|Contains commands for working with projects, connecting to Sybase ASE, and connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure.|  
|**Edit**|Contains commands for finding and working with text in the details pages, such as copying [!INCLUDE[tsql](../../includes/tsql_md.md)] from the SQL details pane. Also contains the **Manage Bookmarks** option, where you will be able to see a list of existing bookmarks. You can use the buttons on the right side of the dialog to manage the bookmarks.|  
|**View**|Contains the **Synchronize Metadata Explorers** command. This synchronizes the objects between Sybase Metadata Explorer and [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure Metadata Explorer. Also contains commands to display and hide the **Output** and **Error List** panes and an option **Layouts** to manage the Layouts.|  
|**Tools**|Contains commands to create reports, export data, and migrate objects and data. Also provides access to the **Global Settings** and **Project Settings** dialog boxes.|  
|**Tester**|Contains commands to create test cases, view test results, and commands for database backup management.|  
|**Help**|Provides access to SSMA Help and to the **About** dialog box.|  
  
### Output Pane and Error List Pane  
The **View** menu provides commands to toggle the visibility of the Output pane and the Error List pane:  
  
-   The Output pane shows status messages from SSMA during object conversion, object synchronization, and data migration.  
  
-   The Error List pane shows error, warning, and informational messages in a list that you can sort.  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL DB &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
[User Interface Reference &#40;SybaseToSQL&#41;](../../ssma/sybase/user-interface-reference-sybasetosql.md)  
  
