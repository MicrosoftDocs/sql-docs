---
title: "Content Element (ASSL) | Microsoft Docs"
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
  - "Content Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Content"
helpviewer_keywords: 
  - "Content element"
ms.assetid: 221addef-2f88-49c5-b8f5-9eee330497a9
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Content Element (ASSL)
  Describes the content of the column in the [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ScalarMiningStructureColumn>  
   ...  
   <Content>...</Content>  
   ...  
</ScalarMiningStructureColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 This enumeration describes the type of content represented by a mining structure column, and can be extended as needed by mining algorithm providers. For more information about content types, see [Content Types &#40;Data Mining&#41;](../../../analysis-services/data-mining/content-types-data-mining.md).  
  
 The values listed in the following table are typically supported by all mining algorithm providers.  
  
|Value|Description|  
|-----------|-----------------|  
|*Discrete*|The column contains discrete values.|  
|*Continuous*|The values for the column define a continuous set of numeric data.|  
|*Discretized*|The values in the column represent groups (or buckets) of values derived from a continuous column.|  
|*Ordered*|The values for the column define an ordered set.|  
|*Cyclical*|The values for the column define a cyclical ordered set.|  
|*Probability*|The values for the column specify a probability for the columns contained in the [ClassifiedColumns](../../../analysis-services/scripting/collections/classifiedcolumns-element-assl.md) element of the parent **ScalarMiningStructureColumn**.|  
|*Variance*|The values for the column specify a variance for the columns contained in the **ClassifiedColumns** element of the parent **ScalarMiningStructureColumn**.|  
|*StdDev*|The values for the column specify a standard deviation for the columns contained in the **ClassifiedColumns** element of the parent **ScalarMiningStructureColumn**.|  
|*ProbabilityVariance*|The values for the column specify a probability variance for the columns contained in the **ClassifiedColumns** element of the parent **ScalarMiningStructureColumn**.|  
|*ProbabilityStdDev*|The values for the column specify a probability standard deviation for the columns contained in the **ClassifiedColumns** element of the parent **ScalarMiningStructureColumn**.|  
|*Support*|The values for the column specify support information for the column contained in the **ClassifiedColumns** element of the parent **ScalarMiningStructureColumn**.<br /><br /> Note: This column is provided as part of the standard for third party mining algorithm providers. Microsoft provided algorithms do not make use of this column.|  
|*Key*|The column is a key column.<br /><br /> Note: This content type is applicable only to key columns, in which the **IsKey** element is set to **True**.|  
  
 In addition to these standard values, mining algorithm providers included with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] support the values in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Key Sequence*|The column is a key column, and the values for the column represent a sequence of events.<br /><br /> Note: This content type is applicable only to key columns, in which the **IsKey** element is set to **True**.|  
|*Key Time*|The column is a key column, and the values for the column represent time measurement units.<br /><br /> Note: This content type is applicable only to key columns, in which the **IsKey** element is set to **True**.|  
|*Sequence*|The values for the column represent a sequence of events.|  
|*Time*|The values for the column represent time measurement units.|  
  
 The enumeration corresponding to the allowed values for **Content** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [ClassifiedColumns Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/classifiedcolumns-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  