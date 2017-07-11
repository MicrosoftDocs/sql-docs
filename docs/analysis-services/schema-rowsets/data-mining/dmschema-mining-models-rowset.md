---
title: "DMSCHEMA_MINING_MODELS Rowset | Microsoft Docs"
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
  - "DMSCHEMA_MINING_MODELS"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_MODELS rowset"
ms.assetid: 1636f4cf-b342-4e2e-93b4-04136e2d41ef
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_MODELS Rowset
  Enumerates the data mining models in the current catalog. The **DMSCHEMA_MINING_MODELS** rowset includes information such as model names, processing date, and the mining algorithm associated with each mining model.  
  
 . The **DMSCHEMA_MINING_MODELS** schema rowset is very similar to the [DBSCHEMA_TABLES](../../../analysis-services/schema-rowsets/ole-db/dbschema-tables-rowset.md) schema rowset and can be used the same way.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_MODELS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**|The catalog name. Populated with the name of the database of which the model is a member.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**|The unqualified schema name. This column is not supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**MODEL_NAME**|**DBTYPE_WSTR**|The mining model name. This column contains the name of the mining model, and it is never empty.|  
|**MODEL_TYPE**|**DBTYPE_WSTR**|The model type.|  
|**MODEL_GUID**|**DBTYPE_GUID**|The GUID of the model.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A user-friendly description of the model.|  
|**MODEL_PROPID**|**DBTYPE_UI4**|The property ID of the model. This column is not supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**DATE_CREATED**|**DBTYPE_DBTIMESTAMP**|The date on which the model was created.|  
|**DATE_MODIFIED**|**DBTYPE_DBTIMESTAMP**|The date on which the model definition was last modified.|  
|**SERVICE_TYPE_ID**|**DBTYPE_UI4**|An enumeration that identifies the type of data mining algorithm that is used by the model. This type can be one of the following values:<br /><br /> **DM_SERVICETYPE_CLASSIFICATION** (1)<br /><br /> **DM_SERVICETYPE_SEGMENTATION**(2)<br /><br /> **DM_SERVICETYPE_ ASSOCIATION**(4)<br /><br /> **DM_SERVICETYPE_ DENSITY_ESTIMATE**(8)<br /><br /> **DM_SERVICETYPE_SEQUENCE**(16)|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|The provider-specific name for the data mining algorithm that is used by the model.|  
|**CREATION_STATEMENT**|**DBTYPE_WSTR**|The statement that was used to create the mining model.|  
|**PREDICTION_ENTITY**|**DBTYPE_WSTR**|A comma-delimited list indicating which mining columns can be predicted.|  
|**IS_POPULATED**|**DBTYPE_BOOL**|A Boolean flag that indicates whether the model is populated.<br /><br /> **TRUE** if the model is populated; otherwise, **FALSE**.|  
|**MINING_PARAMETERS**|**DBTYPE_WSTR**|A comma-separated list of the parameters that were used when the model was created.|  
|**MINING_STRUCTURE**|**DBTYPE_WSTR**|The ID of the mining structure on which the model is based.|  
|**LAST_PROCESSED**|**DBTYPE_DBTIMESTAMP**|The date the model was last processed.|  
|**MSOLAP_IS_DRILLTHROUGH_ENABLED**|**DBTYPE_BOOL**|A Boolean flag that indicates whether the model supports drillthrough.|  
|**FILTER**|**DBTYPE_WSTR**|The filter expression that is associated with the mining model.<br /><br /> NULL or empty string indicates that no filter is applied.|  
|**TRAINING_SET_SIZE**|**DBTYPE_UIS**|The number of cases that are contained in the mining model training set after the structure has been processed and after any filters have been applied to the model.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_MODELS** rowset can be restricted on the columns in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_TYPE**|**DBTYPE_WSTR**|Optional.|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SERVICE_TYPE_ID**|**DBTYPE_UI4**|Optional.|  
|**MINING_STRUCTURE**|**DBTYPE_WSTR**|Optional.|  
  
 For examples of how to query this rowset, see [Query the Parameters Used to Create a Mining Model](../../../analysis-services/data-mining/query-the-parameters-used-to-create-a-mining-model.md).  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  