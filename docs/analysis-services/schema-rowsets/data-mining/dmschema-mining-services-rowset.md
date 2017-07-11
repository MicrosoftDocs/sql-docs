---
title: "DMSCHEMA_MINING_SERVICES Rowset | Microsoft Docs"
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
  - "DMSCHEMA_MINING_SERVICES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DMSCHEMA_MINING_SERVICES rowset"
ms.assetid: 4a672f2f-d637-4def-a572-c18556f83d34
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DMSCHEMA_MINING_SERVICES Rowset
  Provides a description of each data mining algorithm that the provider supports.  
  
## Rowset Columns  
 The **DMSCHEMA_MINING_SERVICES** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|The name of the algorithm. This column is provider-specific.|  
|**SERVICE_TYPE_ID**|**DBTYPE_UI4**|This column contains a bitmap that describes the mining service. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] populates this column with one of the following values:<br /><br /> **DM_SERVICETYPE_CLASSIFICATION** (**1**)<br /><br /> **DM_SERVICETYPE_CLUSTERING** (**2**)|  
|**SERVICE_DISPLAY_NAME**|**DBTYPE_WSTR**|A localizable display name for the algorithm.|  
|**SERVICE_GUID**|**DBTYPE_GUID**|The GUID for the algorithm.|  
|**DESCRIPTION**|**DBTYPE_WSTR**|A user-friendly description of the algorithm.|  
|**PREDICTION_LIMIT**|**DBTYPE_UI4**|The maximum number of predictions the model and algorithm can provide.|  
|**SUPPORTED_DISTRIBUTION_FLAGS**|**DBTYPE_WSTR**|A comma-delimited list of flags that describe the statistical distributions supported by the algorithm. This column contains one or more of the following values:<br /><br /> "**NORMAL**"<br /><br /> "**LOG NORMAL**"<br /><br /> "**UNIFORM**"|  
|**SUPPORTED_INPUT_CONTENT_TYPES**|**DBTYPE_WSTR**|A comma-delimited list of flags that describe the input content types that are supported by the algorithm. This column contains one or more of the following values:<br /><br /> "**KEY**"<br /><br /> "**DISCRETE**"<br /><br /> "**CONTINUOUS**"<br /><br /> "**DISCRETIZED**"<br /><br /> "**ORDERED**"<br /><br /> "KEY **SEQUENCE**"<br /><br /> "**CYCLICAL**"<br /><br /> "**PROBABILITY**"<br /><br /> "**VARIANCE**"<br /><br /> "**STDEV**"<br /><br /> "**SUPPORT**"<br /><br /> "**PROBABILITY VARIANCE**"<br /><br /> "**PROBABILITY STDEV**"<br /><br /> "**KEY TIME**"|  
|**SUPPORTED_PREDICTION_CONTENT_TYPES**|**DBTYPE_WSTR**|A comma-delimited list of flags that describe the prediction content types that are supported by the algorithm. This column contains one or more of the following values:<br /><br /> "**KEY**"<br /><br /> "**DISCRETE**"<br /><br /> "**CONTINUOUS**"<br /><br /> "**DISCRETIZED**"<br /><br /> "**ORDERED**"<br /><br /> "KEY **SEQUENCE** "<br /><br /> "**CYCLICAL**"<br /><br /> "**PROBABILITY**"<br /><br /> "**VARIANCE**"<br /><br /> "**STDEV**"<br /><br /> "**SUPPORT**"<br /><br /> "**PROBABILITY VARIANCE**"<br /><br /> "**PROBABILITY STDEV**"<br /><br /> "KEY TIME"|  
|**SUPPORTED_MODELING_FLAGS**|**DBTYPE_WSTR**|A comma-delimited list of the modeling flags that are supported by the algorithm. This column contains one or more of the following values:<br /><br /> "**MODEL_EXISTENCE_ONLY**"<br /><br /> "**REGRESSOR**"<br /><br /> <br /><br /> Note that provider-specific flags can also be defined.|  
|**SUPPORTED_SOURCE_QUERY**|**DBTYPE_WSTR**|This column is supported for backward compatibility.|  
|**TRAINING_COMPLEXITY**|**DBTYPE_I4**|The length of time that training is expected to take:<br /><br /> **DM_TRAINING_COMPLEXITY_LOW** indicates that the running time is relatively short, and it is proportional to input.<br /><br /> **DM_TRAINING_COMPLEXITY_MEDIUM** indicates that the running time may be long, but it is generally proportional to input.<br /><br /> **DM_TRAINING_COMPLEXITY_HIGH** indicates that the running time is long and it may grow exponentially in relationship to the number of training cases.|  
|**PREDICTION_COMPLEXITY**|**DBTYPE_I4**|The length of time that prediction is expected to take:<br /><br /> **DM_PREDICTION_COMPLEXITY_LOW** indicates that the running time is relatively short, and it is proportional to input.<br /><br /> **DM_PREDICTION_COMPLEXITY_MEDIUM** indicates that the running time may be long, but it is generally proportional to input.<br /><br /> **DM_PREDICTION_COMPLEXITY_HIGH** indicates that the running time is long and it may grow exponentially in relationship to the number of training cases.|  
|**EXPECTED_QUALITY**|**DBTYPE_I4**|The expected quality of the model produced with this algorithm:<br /><br /> **DM_EXPECTED_QUALITY_LOW**<br /><br /> **DM_EXPECTED_QUALITY_MEDIUM**<br /><br /> **DM_EXPECTED_QUALITY_HIGH**|  
|**SCALING**|**DBTYPE_I4**|The scalability of the algorithm:<br /><br /> **DM_SCALING_LOW**<br /><br /> **DM_SCALING_MEDIUM**<br /><br /> **DM_SCALING_HIGH**|  
|**ALLOW_INCREMENTAL_INSERT**|**DBTYPE_BOOL**|A Boolean that indicates whether the algorithm supports incremental training, i.e., updating the discovered patterns based on new factual data, rather than fully re-discovering the patterns.|  
|**ALLOW_PMML_INITIALIZATION**|**DBTYPE_BOOL**|A Boolean that indicates whether mining models can be created based on an PMML 2.1 string.<br /><br /> If **TRUE**, the mining  algorithm supports initialization from PMML 2.1 content.|  
|**CONTROL**|**DBTYPE_I4**|The support given by the service if training is interrupted:<br /><br /> **DM_CONTROL_NONE** indicates that the algorithm cannot be canceled after it starts to train the model.<br /><br /> **DM_CONTROL_CANCEL** indicates that the algorithm can be canceled after it starts to train the model, but must be restarted to resume training.<br /><br /> **DM_CONTROL_SUSPENDRESUME** indicates that the algorithm can be canceled and resumed at any time, but results are not available until training is complete.<br /><br /> **DM_CONTROL_SUSPENDWITHRESULT** indicates that the algorithm can be canceled and resumed at any time, and any incremental results can be obtained.|  
|**ALLOW_DUPLICATE_KEY**|**DBTYPE_BOOL**|A Boolean that indicates whether cases can contain duplicate keys.<br /><br /> If **VARIANT_TRUE**, cases are allowed to contain duplicate keys.|  
|**VIEWER_TYPE**|**DBTYPE_WSTR**|The recommended viewer for this model.|  
|**HELP_FILE**|**DBTYPE_WSTR**|(Optional) The name of the file that contains the documentation for this service.|  
|**HELP_CONTEXT**|**DBTYPE_I4**|(Optional) The Help context ID for this service.|  
|**MSOLAP_SUPPORTS_ANALYSIS_SERVICES_DDL**|**DBTYPE_WSTR**|The version of DDL supported. 0 indicates no DDL support.|  
|**MSOLAP_SUPPORTS_OLAP_MINING_MODELS**|**DBTYPE_BOOL**|A Boolean that indicates whether OLAP mining models can be created.<br /><br /> If **TRUE**, OLAP mining models can be created. Requires **MSOLAP_SUPPORTS_ANALYSIS_SERVICES_DDL** to be non-zero.|  
|**MSOLAP_SUPPORTS_DATA_MINING_DIMENSIONS**|**DBTYPE_BOOL**|A Boolean that indicates whether data mining dimensions can be created.<br /><br /> If **TRUE**, data mining dimensions can be created.|  
|**MSOLAP_SUPPORTS_DRILLTHROUGH**|**DBTYPE_BOOL**|A Boolean that indicates whether the service supports drillthrough capabilities.<br /><br /> If **TRUE**, the service supports drill-through capabilities.|  
  
## Restriction Columns  
 The **DMSCHEMA_MINING_SERVICES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**SERVICE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SERVICE_TYPE_ID**|**DBTYPE_UI4**|Optional.|  
  
## See Also  
 [Data Mining Schema Rowsets](../../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
  