---
title: "MDSCHEMA_FUNCTIONS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "MDSCHEMA_FUNCTIONS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "MDSCHEMA_FUNCTIONS rowset"
ms.assetid: 5253fa8c-b1ce-4504-aff6-a246b5e675c7
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MDSCHEMA_FUNCTIONS Rowset
  Describes the functions available to client applications connected to the database.  
  
## Rowset Columns  
 The **MDSCHEMA_FUNCTIONS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**FUNCTION_NAME**|**DBTYPE_WSTR**|The name of the function.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A description of the function.|  
|**PARAMETER_LIST**|**DBTYPE_WSTR**|A comma delimited list of parameters formatted as in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Visual Basic. For example, a parameter might be Name as String.|  
|**RETURN_TYPE**|**DBTYPE_I4**|The **VARTYPE** of the return data type of the function.|  
|**ORIGIN**|**DBTYPE_I4**|The origin of the function:<br /><br /> 1 for MDX functions.<br /><br /> 2 for user-defined functions.|  
|**INTERFACE_NAME**|**DBTYPE_WSTR**|The name of the interface for user-defined functions<br /><br /> The group name for Multidimensional Expressions (MDX) functions.|  
|**LIBRARY_NAME**|**DBTYPE_WSTR**|The name of the type library for user-defined functions. **NULL** for MDX functions.|  
|**DLL_NAME**|**DBTYPE_WSTR**|(Optional) The name of the assembly that implements the user-defined function.<br /><br /> Returns **VT_NULL** for MDX functions.|  
|**HELP_FILE**|**DBTYPE_WSTR**|(Optional) The name of the file that contains the help documentation for the user-defined function.<br /><br /> Returns **VT_NULL** for MDX functions.|  
|**HELP_CONTEXT**|**DBTYPE_I4**|(Optional) Returns the Help context ID for this function.|  
|**OBJECT**|**DBTYPE_WSTR**|(Optional) The generic name of the object class to which a property applies. For example, the rowset corresponding to the <level_name>.Members function returns "**Level**".<br /><br /> Returns **VT_NULL** for user-defined functions, or non-property MDX functions.|  
|**CAPTION**|**DBTYPE_WSTR**|The display caption for the function.|  
  
 The rowset is sorted on **ORIGIN**, **INTERFACE_NAME**, **FUNCTION_NAME**.  
  
## Restriction Columns  
 The **MDSCHEMA_FUNCTIONS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**LIBRARY_NAME**|**DBTYPE_WSTR**|Optional.|  
|**INTERFACE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**FUNCTION_NAME**|**DBTYPE_WSTR**|Optional.|  
|**ORIGIN**|**DBTYPE_I4**|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  