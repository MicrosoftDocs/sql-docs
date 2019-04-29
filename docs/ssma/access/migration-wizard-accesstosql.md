---
title: "Migration Wizard (AccessToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Migration Wizard dialog box"
  - "Migration Wizard, adding Access databases"
  - "Migration Wizard, Connect to SQL Azure"
  - "Migration Wizard, Connect to SQL Server"
  - "Migration Wizard, Link Tables"
  - "Migration Wizard, Migration status"
  - "Migration Wizard, New Project"
  - "Migration Wizard, Selecting objects to migrate"
ms.assetid: 5bab5914-b2ae-4795-8cf5-83e42d64bef2
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Migration Wizard (AccessToSQL)
The Migration Wizard guides you through the migration of one or more databases from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Or SQL Azure. By using the wizard, you will create a project, add databases to the project, select objects to migrate, and connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Or SQL Azure. You will also convert, load, and migrate Access schemas and data. Optionally, you can link Access tables to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Or SQL Azure tables.  
  
Most of the Migration Wizard pages contain the same options as existing SSMA dialog boxes. Therefore, the wizard pages are described here, and then links are provided so that you can learn more about individual options. If a page contains unique options, they are documented here.  
  
## Starting the Migration Wizard  
By default, the Migration Wizard appears when you start SSMA. You can also start the wizard on the **File** menu by selecting **Migration Wizard**.  
  
## Welcome Page  
The Welcome page introduces the Migration Wizard and provides the following option for starting the wizard.  
  
**Launch this wizard at startup.**  
By default, SSMA will start the Migration Wizard when you start SSMA. To prevent the automatic start of the wizard, clear this check box.  
  
## Create New Project Page  
The Create New Project page is where you enter the project file name, location and migration project type (the version of target SQL Server used for migration). For more information, see [New Project (SSMA)](https://msdn.microsoft.com/ca294f6d-eeb5-42ca-9306-156281a3f0f3)  
  
## Add Access Databases Page  
The Add Access Databases page is where you add one or more Access databases to the project. You can add individual databases by clicking **Add Databases**, and then selecting the databases from the **Open** window. Or, you can find databases by using the **Find Databases** button. For more information, see the following topics:  
  
-   [Adding and Removing Access Database Files](adding-and-removing-access-database-files-accesstosql.md)  
  
-   [Find Databases Wizard (Select Locations)](https://msdn.microsoft.com/00b2d32a-998b-47a7-b25c-589b5bd6777a)  
  
-   [Find Databases Wizard (Select Files)](https://msdn.microsoft.com/2f574a34-4bab-40a4-89a8-ad4907ffc3fd)  
  
-   [Find Databases Wizard (Verify Selection)](https://msdn.microsoft.com/62e20e03-50cc-4ac8-8072-524d194d2ec3)  
  
## Select Objects to Migrate Page  
On the Select Objects to Migrate page, you select objects to convert. You can select all objects, groups of objects, or individual objects.  
  
**To select objects**  
  
1.  Expand **Access-metabase**, and then expand **Databases**.  
  
2.  Do one or more of the following:  
  
    -   To convert all databases, select the check box next to **Databases**.  
  
    -   To convert or omit individual databases, select or clear the check box next to the database name.  
  
    -   To convert or omit queries, expand the database, and then select or clear the **Queries** check box.  
  
    -   To convert or omit individual tables, expand the database, expand **Tables**, and then select or clear the check box next to the table.  
  
If you have many objects, you might want to use the **Advanced Object Selection** options in the right pane to filter Access database objects. For example, if you select **Tables** in the left pane, you can then filter the list of tables by entering strings in the **Filter** box. You can then select or clear the filtered tables for migration by using the buttons at the top of the pane.  
  
For more information about filtering, see the Options section of [Advanced Object Selection (SSMA Common)](https://msdn.microsoft.com/f53b0c79-5473-410a-a0dc-d8f544f7a63c).  
  
## Connect to SQL Server Page  
On the Connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] page, you specify connection properties, and then connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Connect to SQL Server](connect-to-sql-server-accesstosql.md).
  
> [!IMPORTANT]  
> As soon as the connection succeeds, you will encounter **Link Tables** page where you have an option of linking the tables. Click **Next** and the migration starts.  
  
## Connect to SQL Azure Page  
On the Connect to SQL Azure page, you specify connection properties, and then connect to SQL Azure. To create a new azure database, you can do so by using **Create Azure Database** option that appears on the click of **Browse** button. For more information, see [Connect to SQL Azure](connect-to-azure-sql-db-accesstosql.md)  
  
> [!IMPORTANT]  
> As soon as the connection succeeds, you will encounter **Link Tables** page where you have an option of linking the tables. Click **Next** button on the Links page to start migration.  
  
## Link Tables Page  
The Link Tables page lets you link your original Access tables to the migrated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables. Linking tables modifies your Access database so that your queries, forms, reports, and data access pages use the data in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure database instead of the data in your Access database.  
  
**Link tables**  
Select the **Link tables** check box to link Access tables to the migrated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables. To start migration you should click **Next** button.  
  
## Migration Status Page  
The Migration Status page shows the progress of converting the Access schemas to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure schemas, loading the converted schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, and then migrating data.  
  
For more information about this page, see [Convert, Load, and Migrate](https://msdn.microsoft.com/4ec83e96-88a5-4b7b-8d5a-f3429d9a936b)  
  
## See Also  
[Getting Started with SQL Server Migration Assistant for Access &#40;AccessToSQL&#41;](../../ssma/access/getting-started-with-sql-server-migration-assistant-for-access-accesstosql.md)  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
[User Interface Reference(Access)](https://msdn.microsoft.com/af24c303-4a41-449b-9c86-d6558a97e839)  
  
