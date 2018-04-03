---
title: "PartitionID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "PartitionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#PartitionID"
  - "urn:schemas-microsoft-com:xml-analysis#PartitionID"
  - "microsoft.xml.analysis.partitionid"
helpviewer_keywords: 
  - "PartitionID element"
ms.assetid: 19f06454-9719-488e-aeb6-3fc879313351
caps.latest.revision: 11
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# PartitionID Element (XMLA)
  Identifies a partition within a parent element that contains an object reference.  
  
## Syntax  
  
```xml  
  
<Object> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <PartitionID>...</PartitionID>  
   ...  
</Object>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|If Ancestor or Parent=<br />                        [Source](../../../2014/analysis-services/dev-guide/source-element-xmla.md), [Target](../../../2014/analysis-services/dev-guide/target-element-xmla.md)<br /><br /> Cardinality= 1-1: Required element that occurs once and only once.<br /><br /> If Ancestor or Parent= All others:<br /><br /> Cardinality = 0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Object](../../../2014/analysis-services/dev-guide/object-element-xmla.md), [ParentObject](../../../2014/analysis-services/dev-guide/parentobject-element-xmla.md), [Source](../../../2014/analysis-services/dev-guide/source-element-xmla.md), [Target](../../../2014/analysis-services/dev-guide/target-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  