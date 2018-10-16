---
title: "Mapping Oracle and SQL Server Data Types (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Type Mapping Inheritance"
ms.assetid: 05da1495-63b9-47b7-86e2-6746394a2d8a
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Mapping Oracle and SQL Server Data Types (OracleToSQL)
Oracle database types differ from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database types. When you convert Oracle database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects, you must specify how to map data types from Oracle to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can accept the default data type mappings, or you can customize the mappings as shown in the following sections.  
  
## Default Mappings  
SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings &#40;Type Mapping&#41; &#40;OracleToSQL&#41;](../../ssma/oracle/project-settings-type-mapping-oracletosql.md).  
  
## Type Mapping Inheritance  
You can customize type mappings at the project level, object category level (such as all stored procedures), or object level. Settings are inherited from the higher level unless they are overridden at a lower level. For example, if you map **smallmoney** to **money** at the project level, all objects in the project will use this mapping unless you customize the mapping at the object or category level.  
  
When you view the **Type Mapping** tab in SSMA, the background is color-coded to show which type mappings are inherited. The background of a type mapping is yellow for any inherited type mapping, and white for any mapping that is specified at the current level.  
  
## Customizing Data Type Mappings  
The following procedure shows how to map data types at the project, database, or object level:  
  
**To map data types**  
  
1.  To customize data type mapping for the whole project, open the **Project Settings** dialog box:  
  
    1.  On the **Tools** menu, select **Project Settings**.  
  
    2.  In the left pane, select **Type Mapping**.  
  
        The type mapping chart and buttons appear in the right pane.  
  
    Or, to customize data type mapping at the database, table, view, or stored procedure level, select the database, object category, or object in Oracle Metadata Explorer:  
  
    1.  In Oracle Metadata Explorer, select the folder or object to customize.  
  
    2.  In the right pane, click the **Type Mapping** tab.  
  
2.  To add a new mapping, do the following:  
  
    1.  Click **Add**.  
  
    2.  Under **Source type**, select the Oracle data type to map.  
  
    3.  If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace with** box.  
  
    5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
3.  To modify a data type mapping, do the following:  
  
    1.  Click **Edit**.  
  
    2.  Under **Source type**, select the Oracle data type to map.  
  
    3.  If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace with** box, and then [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
4.  To remove a custom data type mapping, do the following:  
  
    1.  Select the row in the type mapping list that contains the data type mapping you want to remove.  
  
    2.  Click **Remove**.  
  
        You cannot remove inherited mappings. However, inherited mappings are overridden by custom mappings on a specific object or object category.  
  
## Next Steps  
The next step in the migration process is to either [create an assessment report](assessing-oracle-schemas-for-conversion-oracletosql.md) or [convert Oracle database objects to SQL Server syntax](converting-oracle-schemas-oracletosql.md). If you create an assessment report, Oracle objects are automatically converted during the assessment.  
  
## See Also  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
  
