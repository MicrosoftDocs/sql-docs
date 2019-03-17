---
title: "Mapping MySQL and SQL Server Data Types (MySQLToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Mapping, customize data type mapping"
  - "Mapping, Type mapping"
ms.assetid: 14f98054-13b4-4231-a6b0-2452f3b9941d
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Mapping MySQL and SQL Server Data Types (MySQLToSQL)
MySQL database types differ from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure database types. When you convert MySQL database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects, you must specify how to map data types from MySQL to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can accept the default data type mappings, or you can customize the mappings as shown in the following procedures.  
  
## Default Mappings  
SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings &#40;Type Mapping&#41; &#40;MySQLToSQL&#41;](../../ssma/mysql/project-settings-type-mapping-mysqltosql.md).  
  
## Type Mapping Inheritance  
You can customize type mappings at the project level, object category level (such as all stored procedures), or object level. Settings are inherited from the higher level unless they are overridden at a lower level. For example, if you map **smallint** to **int** at the project level, all objects in the project will use this mapping unless you customize the mapping at the object or category level.  
  
When you view the **Type Mapping** tab in SSMA, the background is color-coded to show which type mappings are inherited. The background of a type mapping is yellow for any inherited type mapping, and white for any mapping that is specified at the current level.  
  
## Customizing Data Type Mappings  
  
-   **To map data types:**  
  
    The following procedures show how to map data types at the project, database, or database object level:  
  
    1.  To customize data type mapping for the whole project, open the **Project Settings** dialog box. On the Tools menu, select **Project Settings**.  
  
        In the left pane, select **Type Mapping**. The type mapping chart and buttons appear in the right pane.  
  
    2.  To customize data type mappings at the database or table level, select the database or table in the MySQL Metadata Explorer. In the MySQL Metadata Explorer, select the folder or object to customize.  
  
        In the right pane, click **Type Mapping**.  
  
-   **To add a new mapping, do the following:**  
  
    1.  In the Type Mapping pane, click **Add** .  
  
    2.  In the New Type Mapping dialog box, under **Source type**, select the MySQL data type to map.  
  
    3.  If the type requires a length, specify the minimum and maximum data lengths for the mapping by selecting the **From** and **To** check boxes, and then entering the values.  
  
    4.  This lets you customize the data mapping for smaller and larger values of the same data type. Under **Target type**, select the target SQL Server or SQL Azure data type.  
  
        1.  Some types require a target data type length. If required, enter the new data length in the **Replace With** box, and then click **OK**.  
  
        2.  Some types require a target data type **precision** and **scale**. If required, enter the new precision and scale in the **Replace With** box, and then click **OK**.  
  
-   **To edit a type mapping, do the following:**  
  
    1.  In the Type Mapping pane, click **Edit**.  
  
    2.  In the Type Mapping List dialog box, under **Source type**, select the MySQL data type to map.  
  
    3.  If the type requires a length, specify the minimum and maximum data lengths for the mapping by selecting the **From** and **To** check boxes, and then entering the values.  
  
    This lets you customize the data mapping for smaller and larger values of the same data type. Under **Target type**, select the target SQL Server or SQL Azure data type.  
  
    1.  Some types require a target data type length. If required, enter the new data length in the **Replace With** box, and then click **OK**.  
  
    2.  Some types require a target data type **precision** and **scale** . If required, enter the new precision and scale in the **Replace With** box, and then click **OK** .  
  
-   **To remove a data type mapping, do the following:**  
  
    1.  In the Type Mapping pane, select the row in the type mapping list that contains the data type mapping you want to remove.  
  
    2.  Click **Remove**.  
  
## Next Step  
The next step in the migration process is to either [Create an assessment report](assessing-mysql-databases-for-conversion-mysqltosql.md) or [Convert MySQL database objects into SQL Server or SQL Azure syntax](converting-mysql-databases-mysqltosql.md). If you create a report, MySQL objects are automatically converted during the assessment.  
  
## See Also  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
