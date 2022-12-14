---
title: "Converting Access Database Objects (AccessToSQL) | Microsoft Docs"
description: Learn how to select Access database objects after you connect to SQL Server/Azure SQL Database, and then convert the schemas to SQL Server/SQL Database schemas.
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Access databases"
  - "Access databases, converting schemas"
  - "conversion"
  - "conversion, converting schemas"
  - "indexes, altering"
  - "metadata"
  - "metadata, altering"
  - "metadata, converting"
  - "migrating databases, one-click"
  - "one-click migration"
  - "schemas"
  - "schemas, converting"
  - "SQL"
  - "SQL, converting"
  - "syntax"
  - "syntax, converting"
  - "tables, altering"
  - "translating Access to SQL Azure"
  - "translating Access to SQL Server"
ms.assetid: e0ef67bf-80a6-4e6c-a82d-5d46e0623c6c
author: cpichuka 
ms.author: cpichuka 
---
# Converting Access Database Objects (AccessToSQL)
After you have added Access databases and connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, SSMA displays metadata for Access and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database objects. You can now select Access database objects, and then convert the schemas into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure schemas.  
  
## The Conversion Process  
Converting database objects takes the object definitions from the Access metadata, converts them into equivalent [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, and then loads this information into the project. You can then view the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects and their properties by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer.  
  
> [!IMPORTANT]  
> Converting objects does not create the objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. It only converts the object definitions and stores the information in the SSMA project.  
  
During the conversion, SSMA prints status to the Output pane, and error, warning, and informational messages to the Error List pane. Use this information to determine whether you need to modify your Access databases or your conversion process to obtain the desired conversion results. You can also use the information in the [Preparing Access Databases for Migration](preparing-access-databases-for-migration-accesstosql.md) topic to determine what will and will not be converted.  
  
## Setting Conversion Options  
Before converting objects, review the project conversion options in the **Project Settings** dialog box. By using this dialog box, you can set how SSMA converts indexed memo columns, primary keys, foreign key constraints, timestamps, and tables without indexes. For more information, see [Project Settings (Conversion)](./project-settings-conversion-accesstosql.md)  
  
## Conversion Results  
The following table shows which Access objects are converted, and the resulting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects:  
  
|Access Object|Resulting SQL Server Object|  
|-----------------|-------------------------------|  
|table|table|  
|column|column|  
|index|index|  
|foreign key|foreign key|  
|query|view<br /><br />Most SELECT queries are converted to views. Other queries, such as UPDATE queries, are not migrated.<br /><br />SELECT queries that take parameters are not converted, nor are cross-tab queries.|  
|report|not converted|  
|form|not converted|  
|macro|not converted|  
|module|not converted|  
|default value|default value|  
|allow zero length column property|check constraint|  
|column validation rule|check constraint|  
|table validation rule|check constraint|  
|primary key|primary key|  
  
## Converting Access Objects  
To convert Access database objects, you first must select the objects you want to convert, and then have SSMA do the conversion. To view output messages during the conversion, on the **View** menu, select **Output**.  
  
**To select and convert Access database objects to SQL Server or SQL Azure syntax**  
  
1.  In Access Metadata Explorer, expand **access-metabase**, and then expand **Databases**.  
  
2.  Do one or more of the following:  
  
    -   To convert all databases, select the check box next to **Databases**.  
  
    -   To convert or omit individual databases, select or clear the check box next to the database name.  
  
    -   To convert or omit queries, expand the database, and then select or clear the **Queries** check box.  
  
    -   To convert or omit individual tables, expand the database, expand **Tables**, and then select or clear the check box next to the table.  
  
3.  Do one of the following:  
  
    -   To convert schemas, right-click **Databases** and select **Convert Schema**.  
  
        You can also convert individual objects. To convert an object, regardless of which objects are selected, right-click the object and select **Convert Schema**.  
  
        When an object has been converted, it appears bold in Access Metadata Explorer.  
  
    -   To convert, load, and migrate schemas and data in one step, right-click Databases and select **Convert, Load, and Migrate**.  
  
4.  Review messages in the **Output** pane and any errors and warnings in the **Error List** pane.  
  
## Altering Tables and Indexes  
After you convert Access metadata to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure metadata, and before you load the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure, you can alter [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure tables and indexes.  
  
**To alter table or index properties**  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure Metadata Explorer, select the table or index you want to alter.  
  
2.  On the **Table** tab, click the property you want to alter and then enter or select the new setting. For example, you can change nvarchar(15) to nvarchar(20), or select a check box to make a table column nullable.  
  
    Move the cursor out of the changed property cell. You can do this by clicking another row or pressing the Tab key.  
  
3.  Click **Apply**.  
  
You can now view the changes in the code on the **SQL** tab.  
  
## Next steps  
The next step in the migration process is [load converted database objects into SQL Server](loading-converted-database-objects-into-sql-server-accesstosql.md)  
  
## See Also  
[Migrating Access Databases to SQL Server](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)  
