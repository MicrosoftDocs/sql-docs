---
title: "DMSCHEMA_MINING_MODEL_CONTENT_PMML Rowset | Microsoft Docs"
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
  - "DMSCHEMA_MINING_MODEL_CONTENT_PMML"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_MODEL_CONTENT_PMML rowset"
ms.assetid: fa05bb08-a955-4c8d-b57f-ffcd82470220
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_MODEL_CONTENT_PMML Rowset
  Returns the XML structure of the mining model. The format of the XML string follows the Predictive Model Markup Language (PMML 2.1) standard.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_MODEL_CONTENT_PMML** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**||The catalog name that is populated with the name of the database of which the model is a member.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**||The unqualified schema name. This column is not supported by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]; it always contains **NULL**.|  
|**MODEL_NAME**|**DBTYPE_WSTR**||The model name. This column cannot contain **NULL**.|  
|**MODEL_TYPE**|**DBTYPE_WSTR**||The model type. It is a provider-specific string. It can be **NULL**.|  
|**MODEL_GUID**|**DBTYPE_GUID**||The GUID that identifies the model. Providers that do not use GUIDs to identify tables return **NULL**.|  
|**MODEL_PMML**|**DBTYPE_WSTR**||An XML representation of the model's content in PMML format.|  
|**SIZE**|**DBTYPE_UI4**||The number of bytes in the XML string.|  
|**LOCATION**|**DBTYPE_WSTR**||The location of the XML file. It is **NULL** if no location is available.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_MODEL_CONTENT_PMML** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**MODEL_CATALOG**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_SCHEMA**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MODEL_TYPE**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  