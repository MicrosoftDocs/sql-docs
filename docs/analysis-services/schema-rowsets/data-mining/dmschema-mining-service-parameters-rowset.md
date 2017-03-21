---
title: "DMSCHEMA_MINING_SERVICE_PARAMETERS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DMSCHEMA_MINING_SERVICE_PARAMETERS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_SERVICE_PARAMETERS rowset"
ms.assetid: 5994e66b-84d0-4279-9f50-d92fd829dd83
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_SERVICE_PARAMETERS Rowset
  Describes the parameters for the algorithms on the server.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_SERVICE_PARAMETERS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|The name of the algorithm.|  
|**PARAMETER_NAME**|**DBTYPE_WSTR**|The name of the parameter.|  
|**PARAMETER_TYPE**|**DBTYPE_WSTR**|The type of the parameter.|  
|**IS_REQUIRED**|**DBTYPE_BOOL**|A Boolean that returns **TRUE** if the parameter is required.|  
|**PARAMETER_FLAGS**|**DBTYPE_UI4**|A bitmask that describes the characteristics of the parameter:<br /><br /> **DM_PARAMETER_TRAINING** (**0x0000001**) indicates the parameter is used for training<br /><br /> **DM_PARAMETER_PREDICTION** (**0x00000002**) indicates the parameter is used for prediction<br /><br /> **DM_PARAMETER_CONTENT** (**0x00000003**) indicates the parameter is used for content restriction|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A user-friendly description of the parameter.|  
|**DEFAULT_VALUE**|**DBTYPE_WSTR**|The default value of the parameter. Returns **NULL** if the default value is not a simple data type.|  
|**VALUE_ENUMERATION**|**DBTYPE_WSTR**|An enumerator of possible values for the parameter.|  
|**HELP_FILE**|**DBTYPE_WSTR**|The name of the file that contains this parameter's documentation.|  
|**HELP_CONTEXT**|**DBTYPE_I4**|The Help context ID for this function.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_SERVICE_PARAMETERS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**PARAMETER_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  