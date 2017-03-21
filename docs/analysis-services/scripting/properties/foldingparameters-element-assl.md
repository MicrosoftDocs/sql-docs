---
title: "FoldingParameters Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "FoldIndex"
  - "FoldCount"
  - "MaxCases"
  - "FoldingParameters"
  - "FoldTargetAttribute"
helpviewer_keywords: 
  - "FoldingParameters element"
ms.assetid: 5f5c5a3e-4aed-48fb-bca5-e67f421bef2f
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# FoldingParameters Element (ASSL)
  Specifies the parameters used by the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] server when it performs cross-validation of mining models.  
  
> [!NOTE]  
>  These parameters are for internal use only. The information provided here is for reference only.  
  
## Syntax  
  
```xml  
  
<MiningModel>  
   ...  
   <ddl100_100:FoldingParameters>...  
      <ddl100_100:FoldIndex>...</ddl100_100:FoldIndex>  
      <ddl100_100:FoldCount>...</ddl100_100:FoldCount>  
      <ddl100_100:MaxCases>...</ddl100_100:MAxCases>  
      <ddl100_100:FoldTargetAttribute>...</ddl100_100:FoldTargetAttribute  
...</ddl100_100:FoldingParameters>  
</MiningStructure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|*FoldIndex*|Integer that indicates the starting position of the partition that is used for cross-validation.|  
|*FoldCount*|Integer that indicates the number of partitions in the model after cross-validation.|  
|*MaxCases*|Integer that indicates how many model cases are used for cross-validation.<br /><br /> A value of 0 indicates that all cases are used.|  
|*FoldTargetAttribute*|String that indicates the ID of the model column that contains the predictable attribute.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md)|  
|Child elements|*FoldIndex*<br /><br /> *FoldCount*<br /><br /> *MaxCases*<br /><br /> *FoldTargetAttribute*|  
  
## Remarks  
 These properties are for internal use only, and are not supported for use in DDL statements.  
  
 For information about how to use cross-validation in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], see [Measures in the Cross-Validation Report](../../../analysis-services/data-mining/measures-in-the-cross-validation-report.md).  
  
 For information about how to perform cross-validation by using [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] stored procedures, see [Data Mining Stored Procedures &#40;Analysis Services - Data Mining&#41;](../../../analysis-services/data-mining/data-mining-stored-procedures-analysis-services-data-mining.md).  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  