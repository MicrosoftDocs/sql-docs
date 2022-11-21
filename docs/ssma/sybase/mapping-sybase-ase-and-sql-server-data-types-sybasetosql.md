---
description: "Mapping Sybase ASE and SQL Server Data Types (SybaseToSQL)"
title: "Mapping Sybase ASE and SQL Server Data Types (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Mapping Sybase ASE Schemas to SQL Server Schemas"
  - "Type Mapping Settings"
ms.assetid: 784365d3-df4e-47ab-8ee0-d8392b52f510
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.sybase.typemappingeditform.f1"
    - "ssma.sybase.typemappingdatagridview.f1"
---
# Mapping Sybase ASE and SQL Server Data Types (SybaseToSQL)
Sybase Adaptive Server Enterprise (ASE) database types differ from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database types. When you convert ASE database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure objects, you must specify how to map data types from ASE to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. You can accept the default data type mappings, or you can customize the mappings as shown in the following sections.  
  
## Default Mappings  
SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings &#40;Type Mapping&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-type-mapping-sybasetosql.md).  
  
## Type Mapping Inheritance  
You can customize type mappings at the project level, object category level (such as all stored procedures), or object level. Settings are inherited from the higher level unless overridden at a lower level. For example, if you map **smallmoney** to **money** at the projects level, all objects in the project will use this mapping unless you customize the mapping at the object category level or object level.  
  
When you view the **Type Mapping** tab in SSMA, the background is color-coded to show which type mappings are inherited. The background of a type mapping is yellow for any inherited type mapping, and white for any mapping specified at the current level.  
  
## Customizing Data Type Mappings  
The following procedure shows how to map data types at the project, database, or object level.  
  
**To map data types**  
  
1.  To customize data type mapping for the whole project, open the **Project Settings** dialog box:  
  
    1.  On the **Tools** menu, select **Project Settings**.  
  
    2.  In the left pane, select **Type Mapping**.  
  
        The type mapping chart and buttons appear in the right pane.  
  
    Or, to customize data type mapping at the database, table, view, or stored procedure level, select the database, object category, or object in Sybase Metadata Explorer:  
  
    1.  In Sybase Metadata Explorer, select the folder or object that you want to customize.  
  
    2.  In the right pane, click the **Type Mapping** tab.  
  
2.  To add a new mapping, do the following:  
  
    1.  Click **Add**.  
  
    2.  Under **Source type**, select the ASE data type to map.  
  
    3.  If the type requires a length, specify the minimum data length for the mapping in the **From** box, and specify the maximum data length for the mapping in the **To** box.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace with** box.  
  
    5.  Click **OK**.  
  
3.  To edit a data type mapping, do the following:  
  
    1.  Click **Edit**.  
  
    2.  Under **Source type**, select the ASE data type to map.  
  
    3.  If the type requires a length, specify the minimum data length for the mapping in the **From** box, and specify the maximum data length for the mapping in the **To** box.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace with** box, and then click **OK**.  
  
4.  To remove a custom data type mapping, do the following:  
  
    1.  Select the row in the type mapping list that contains the data type mapping you want to remove.  
  
    2.  Click **Remove**.  
  
        You cannot remove inherited mappings. However, inherited mappings are overridden by custom mappings on a specific object or object category.  
  
## Next Steps  
The next step in the migration process is to either [Create an assessment report](assessing-sybase-ase-database-objects-for-conversion-sybasetosql.md) or [Convert Sybase ASE database objects to SQL Server or SQL Azure syntax](converting-sybase-ase-database-objects-sybasetosql.md). If you create an assessment report, Sybase ASE objects are automatically converted during the assessment.  
  
## See Also  
[Migrating Sybase ASE Databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
  
