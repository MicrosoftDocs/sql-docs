---
title: "Create Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "Create element (DTA)"
ms.assetid: 9d076c90-f933-45f4-b6d9-447793f6528b
author: stevestein
ms.author: sstein
manager: craigg
---
# Create Element (DTA)
  Contains information about the indexes, statistics, or heap structures in a user-specified configuration.  
  
## Syntax  
  
```  
  
<Recommendation>  
    <Create>  
    ...code removed here...  
    </Create>  
</Recommendation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|None.|  
|**Default value**|None.|  
|**Occurrence**|Required once for each physical design structure type (indexes, statistics, or heap structures).|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[Recommendation Element &#40;DTA&#41;](recommendation-element-dta.md)|  
|**Child elements**|[Index Element &#40;DTA&#41;](index-element-dta.md)<br /><br /> `Statistics` Element (see [Database Engine Tuning Advisor XML schema](https://schemas.microsoft.com/sqlserver/) for information)<br /><br /> `Heap` Element (see [Database Engine Tuning Advisor XML schema](https://schemas.microsoft.com/sqlserver/) for information)|  
  
## Remarks  
 This element is of the **CreateTypecomplexType** name in the Database Engine Tuning Advisor XML schema. It is used to create indexes, statistics, and heap structures for a user-specified configuration. Do not confuse this `Create` element with the other types that can be used to create views (`CreateViewType`) or partitioning (`CreatePType`). Refer to the [Database Engine Tuning Advisor XML schema](https://schemas.microsoft.com/sqlserver/) for information about these other `Create` element types.  
  
## Example  
 For a usage example of this element, see the [XML Input File Sample with User-specified Configuration &#40;DTA&#41;](xml-input-file-sample-with-user-specified-configuration-dta.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
