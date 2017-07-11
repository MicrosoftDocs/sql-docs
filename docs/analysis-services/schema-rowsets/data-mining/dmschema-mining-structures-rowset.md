---
title: "DMSCHEMA_MINING_STRUCTURES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DMSCHEMA_MINING_STRUCTURES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_STRUCTURES rowset"
ms.assetid: 6224556b-08a0-496e-bd7c-632c3e833e26
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_STRUCTURES Rowset
  Enumerates information about the mining structures in the current catalog.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_STRUCTURES** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**STRUCTURE_CATALOG**|**DBTYPE_WSTR**||The catalog name.|  
|**STRUCTURE_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name. **NULL** if schemas are not supported by the provider.|  
|**STRUCTURE_NAME**|**DBTYPE_WSTR**||The structure name. This column cannot contain **NULL**.|  
|**STRUCTURE_GUID**|**DBTYPE_GUID**||A GUID that uniquely identifies the structure. **NULL** if not supported by the provider.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A concise description of the structure. **NULL** if no description is associated with the structure.|  
|**STRUCTURE_PROPID**|**DBTYPE_UI4**||The property ID of the structure. **NULL** if not supported by the provider.|  
|**DATE_CREATED**|**DBTYPE_DBTIMESTAMP**||The date when the structure was created. **NULL** if not available from the provider.|  
|**DATE_MODIFIED**|**DBTYPE_DBTIMESTAMP**||The date when the structure was last modified. **NULL** if not available from the provider.|  
|**CREATION_STATEMENT**|**DBTYPE_WSTR**||(Optional) The statement that was used to create the original data mining model.|  
|**IS_POPULATED**|**DBTYPE_BOOL**||A Boolean that indicates whether the structure is populated.<br /><br /> **VARIANT_TRUE** if the structure is populated; **VARIANT_FALSE** otherwise.|  
|**LAST_PROCESSED**|**DBTYPE_DBTIMESTAMP**||The date when the structure was last processed. **NULL** if not available from the provider.|  
|**HOLDOUT_MAXPERCENT**|**DBTYPE_ UI1**||User-specified value that indicates the maximum percentage of the input cases held out as the test set.<br /><br /> 0 or **NULL** indicates no limit.|  
|**HOLDOUT_MAXCASES**|**DBTYPE_UI8**||User-specified value that indicates the maximum number of the input cases held out as the test set.<br /><br /> 0 or **NULL** indicates no limit.|  
|**HOLDOUT_SEED**|**DBTYPE_UI8**||User-specified value that is used as the seed for repeatable partitioning.<br /><br /> 0 indicates that a hash of the mining structure ID is used as the seed.|  
|**HOLDOUT_ACTUAL_SIZE**|**DBTYPE_UI8**||If the mining structure is processed, this indicates the actual size of the test data set, expressed in number of cases.<br /><br /> **NULL** indicates that the mining structure is not processed.|  
  
 The rowset is sorted on **STRUCTURE_CATALOG**, **STRUCTURE_SCHEMA**, **STRUCTURE_NAME**.  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_STRUCTURES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**STRUCTURE_CATALOG**|**DBTYPE_WSTR**|Optional.|  
|**STRUCTURE_SCHEMA**|**DBTYPE_WSTR**|Optional.|  
|**STRUCTURE_NAME**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  