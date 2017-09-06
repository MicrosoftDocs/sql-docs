---
title: "Project Settings (Type Mapping) (AccessToSQL) | Microsoft Docs"
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
  - "Access data types"
  - "data types, default mappings"
  - "default data type mappings"
  - "Project Settings dialog box, Type Mapping"
  - "SQL Server data types"
  - "Type Mapping settings"
ms.assetid: b87b9683-abed-4677-8c50-18bdba704655
caps.latest.revision: 16
author: “Shamikg”
ms.author: “Shamikg”
manager: "jhubbard"
---
# Project Settings (Type Mapping) (AccessToSQL)
The Type Mapping project settings let you set default type mappings for the SSMA project. You can also specify type mappings for individual database objects. For more information, see [Mapping Source and Target Data Types](http://msdn.microsoft.com/en-us/b362a075-16e7-423f-b63f-e1e9f02844a9).  
  
Type mapping is available in the **Project Settings** and **Default Project Settings** dialog boxes:  
  
-   Use the **Project Settings** dialog box to set configuration options for the current project. To access the type mapping settings, on the **Tools** menu, select **Project Settings**, and then click **Type Mapping** in the left pane.  
  
-   Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the type mapping settings, on the **Tools** menu, select **Default Project Settings**, select migration project type for which settings are required to be viewed /changed from **Migration Target Version** drop down and then click **Type Mapping** in the left pane.  
  
## Options  
**Source Type**  
The Access data type to map.  
  
**Target Type**  
The target [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] or SQL Azure data type for the specified Access data type.  
  
The following table shows the default mapping between source and target data types.  
  
|Access Data Type|SQL Server Data Type|  
|--------------------|------------------------|  
|**binary[\*..\*]**|**varbinary[\*]**|  
|**boolean**|**bit**|  
|**byte**|**tinyint**|  
|**currency**|**money**|  
|**date**|**datetime**|  
|**decimal**|**float**|  
|**double**|**float**|  
|**guid**|**uniqueidentifier**|  
|**integer**|**smallint**|  
|**long**|**int**|  
|**longbinary**|**varbinary(max)**|  
|**memo**|**nvarchar(max)**|  
|**memo** - for Access 97|**varchar(max)**|  
|**single**|**real**|  
|**text[\*..\*]**|**nvarchar[\*]**|  
|**text[\*..\*]** - for Access 97|**varchar[\*]**|  
  
**Add**  
Click to add a data type to the mapping list.  
  
**Edit**  
Click to edit a data type in the mapping list.  
  
**Remove**  
Click to remove the selected data type mapping from the mapping list.  
  
**Reset to Default**  
Click to reset all data type mappings to the SSMA defaults.  
  
## See Also  
[Mapping Source and Target Data Types](http://msdn.microsoft.com/en-us/b362a075-16e7-423f-b63f-e1e9f02844a9)  
[User Interface Reference(Access)](http://msdn.microsoft.com/en-us/af24c303-4a41-449b-9c86-d6558a97e839)  
  
