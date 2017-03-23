---
title: "DMSCHEMA_MINING_FUNCTIONS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DMSCHEMA_MINING_FUNCTIONS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_FUNCTIONS rowset"
ms.assetid: 9ace7493-a7b1-45ca-93de-3cb2f3597017
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_FUNCTIONS Rowset
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
  
  