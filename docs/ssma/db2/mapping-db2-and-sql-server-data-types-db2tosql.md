---
description: "Mapping DB2 and SQL Server Data Types (DB2ToSQL)"
title: "Mapping DB2 and SQL Server Data Types (DB2ToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: e7e939a8-5e76-4509-beaf-5acd1cab505e
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
    - "ssma.db2.typemappingeditform.f1"

---
# Mapping DB2 and SQL Server Data Types (DB2ToSQL)
DB2 database types differ from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database types. When you convert DB2 database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects, you must specify how to map data types from DB2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can accept the default data type mappings, or you can customize the mappings as shown in the following sections.  
  
## Default Mappings  
SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings &#40;Type Mapping&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-type-mapping-db2tosql.md).  
  
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
  
    Or, to customize data type mapping at the database, table, view, or stored procedure level, select the database, object category, or object in DB2 Metadata Explorer:  
  
    1.  In DB2 Metadata Explorer, select the folder or object to customize.  
  
    2.  In the right pane, click the **Type Mapping** tab.  
  
2.  To add a new mapping, do the following:  
  
    1.  Click **Add**.  
  
    2.  Under **Source type**, select the DB2 data type to map.  
  
    3.  If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace with** box.  
  
    5.  Select **OK**.
  
3.  To modify a data type mapping, do the following:  
  
    1.  Click **Edit**.  
  
    2.  Under **Source type**, select the DB2 data type to map.  
  
    3.  If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace with** box, and then select **OK**.
  
4.  To remove a custom data type mapping, do the following:  
  
    1.  Select the row in the type mapping list that contains the data type mapping you want to remove.  
  
    2.  Click **Remove**.  
  
        You cannot remove inherited mappings. However, inherited mappings are overridden by custom mappings on a specific object or object category.  
  
## Next Steps  
The next step in the migration process is to either [Assessment Report &#40;DB2ToSQL&#41;](../../ssma/db2/assessment-report-db2tosql.md) or [Converting DB2 Schemas &#40;DB2ToSQL&#41;](../../ssma/db2/converting-db2-schemas-db2tosql.md). If you create an assessment report, DB2 objects are automatically converted during the assessment.  
  
## See Also  
[Migrating DB2 Databases to SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/migrating-db2-databases-to-sql-server-db2tosql.md)  
  
