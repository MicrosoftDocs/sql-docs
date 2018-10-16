---
title: "MergePartitions Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MergePartitions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#MergePartitions"
  - "microsoft.xml.analysis.mergepartitions"
  - "urn:schemas-microsoft-com:xml-analysis#MergePartitions"
helpviewer_keywords: 
  - "MergePartitions command"
ms.assetid: cf538189-0629-49b3-8e01-32afba7b020d
author: minewiskan
ms.author: owend
manager: craigg
---
# MergePartitions Element (XMLA)
  Merges the data of one or more source partitions into a target partition, and then deletes the source partitions.  
  
## Syntax  
  
```xml  
  
<Command>  
   <MergePartitions>  
      <Sources>...</Sources>  
      <Target>...</Target>  
   </MergePartitions>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Sources](../xml-elements-properties/sources-element-xmla.md), [Target](../xml-elements-properties/target-element-xmla.md)|  
  
## Remarks  
 All object references in the `Sources` and `Target` elements must point to distinct partitions in the same measure group. Otherwise, an error occurs.  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
