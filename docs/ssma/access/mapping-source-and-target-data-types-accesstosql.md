---
title: "Mapping Source and Target Data Types (AccessToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
helpviewer_keywords: 
  - "customizing data type mappings"
  - "data types, mapping"
  - "mapping, data types"
  - "source data types"
  - "target data types"
ms.assetid: b362a075-16e7-423f-b63f-e1e9f02844a9
caps.latest.revision: 14
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Mapping Source and Target Data Types (AccessToSQL)
Access database types differ from [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] database types. When you convert Access database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] objects, you must specify how to map data types from Access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]. You can accept the default data type mappings, or you can customize the mappings as shown in the following procedures.  
  
## Default Mappings  
SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings (Type Mapping)](http://msdn.microsoft.com/en-us/b87b9683-abed-4677-8c50-18bdba704655).  
  
## Customizing Data Type Mappings  
By using the **Project Settings** dialog box, you can customize how types are mapped for all databases and database objects in a project. The type mappings for a project apply to all databases and database objects that do not have custom type mappings.  
  
You can also customize data type mapping at the database or table level.  
  
The following procedure shows how to map data types at the project, database, or database object level.  
  
**To map data types**  
  
1.  To customize data type mapping for the whole project, open the **Project Settings** dialog box:  
  
    1.  On the **Tools** menu, select **Project Settings**.  
  
    2.  In the left pane, select **Type Mapping**.  
  
        The type mapping chart and buttons appear in the right pane.  
  
    Or, to customize data type mapping at the database or table level, select the database or table in the Access Metadata Explorer pane:  
  
    1.  In the Access Metadata Explorer pane, expand **access-metabase**, and then expand **Databases**.  
  
    2.  Select the database or table for which you want to customize the data type mapping.  
  
    3.  In the right pane, click **Type Mapping**.  
  
2.  To add a new mapping, do the following:  
  
    1.  In the Type Mapping pane, click **Add**.  
  
    2.  In the **New Type Mapping** dialog box, under **Source type**, select the Access data type to map.  
  
    3.  If the type requires a length, specify the minimum and maximum data lengths for the mapping by selecting the **From** and **To** check boxes, and then entering the values.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace With** box, and then click **OK**.  
  
3.  To edit a data type mapping, do the following:  
  
    1.  In the Type Mapping pane, click **Edit**.  
  
    2.  In the **Type Mapping List** dialog box, under **Source type**, select the Access data type to map.  
  
    3.  If the type requires a length, specify the minimum and maximum data lengths for the mapping by selecting the **From** and **To** check boxes, and then entering the values.  
  
        This lets you customize the data mapping for smaller and larger values of the same data type.  
  
    4.  Under **Target type**, select the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] data type.  
  
        Some types require a target data type length. If it is required, enter the new data length in the **Replace With** box, and then click **OK**.  
  
4.  To remove a data type mapping, do the following:  
  
    1.  In the Type Mapping pane, select the row in the type mapping list that contains the data type mapping you want to remove.  
  
    2.  Click **Remove**.  
  
## Next Steps  
The next step in the migration process is [convert access database objects to SQL Server objects](http://msdn.microsoft.com/en-us/e0ef67bf-80a6-4e6c-a82d-5d46e0623c6c)  
  
## See Also  
[Migrating Access Databases to SQL Server](http://msdn.microsoft.com/en-us/76a3abcf-2998-4712-9490-fe8d872c89ca)  
  
