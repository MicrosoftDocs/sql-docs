---
title: "DMSCHEMA_MINING_FUNCTIONS Rowset | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DMSCHEMA_MINING_FUNCTIONS Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Describes the data mining functions that are supported by the data mining algorithms available on a server that is running [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_FUNCTIONS** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**SERVICE_NAME**|**DBTYPE_WSTR**||The name of the algorithm.|  
|**FUNCTION_NAME**|**DBTYPE_WSTR**||The name of the function.|  
|**FUNCTION_SIGNATURE**|**DBTYPE_WSTR**||The signature of the function.|  
|**RETURNS_TABLE**|**DBTYPE_BOOL**||**FALSE** if the function returns scalar content (such as the length of the character argument); **TRUE** if the function returns a table (such as a histogram table).|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A user-friendly description of the function.|  
|**HELP_FILE**|**DBTYPE_WSTR**||The name of the file that contains this function's documentation.|  
|**HELP_CONTEXT**|**DBTYPE_I4**||The Help context ID for this function.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_FUNCTIONS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**FUNCTION_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  
