---
title: "Source Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Source Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Source"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Source"
  - "microsoft.xml.analysis.source"
helpviewer_keywords: 
  - "Source element"
ms.assetid: 4d4665ae-e20f-4baf-ab0f-848660caf500
author: minewiskan
ms.author: owend
manager: craigg
---
# Source Element (XMLA)
  Represents a source partition to be merged during a [MergePartitions](../xml-elements-commands/mergepartitions-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Sources>  
   <Source>  
      <DatabaseID>...</DatabaseID>  
      <CubeID>...</CubeID>  
      <MeasureGroupID>...</MeasureGroupID>  
      <PartitionID>...</PartitionID>  
   </Source>  
</Sources>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Sources](sources-element-xmla.md)|  
|Child elements|[CubeID](id-element-xmla.md), [DatabaseID](databaseid-element-xmla.md), [MeasureGroupID](measuregroupid-element-xmla.md), [PartitionID](partitionid-element-xmla.md)|  
  
## Remarks  
 The `Source` element is an object reference to a single partition to be merged into a target partition specified by the `Target` element of the parent `MergePartitions` element.  
  
## Example  
 The following example combines all four partitions of the `Internet Sales` measure group into the `Internet_Sales_2004` target partition. The example refers to the **Adventure Works** cube of the [!INCLUDE[ssAWDWsp](../../../includes/ssawdwsp-md.md)] sample [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database.  
  
```  
<MergePartitions xmlns="http://schemas.microsoft.com/analysisservices/2003/engine">  
  <Sources>  
     <Source>        <DatabaseID>Adventure Works DW Multidimensional 2012</DatabaseID>        <CubeID>Adventure Works</CubeID>        <MeasureGroupID>Fact Internet Sales 1</MeasureGroupID>        <PartitionID>Internet_Sales_2001</PartitionID>     </Source>     <Source>        <DatabaseID>Adventure Works DW Multidimensional 2012</DatabaseID>        <CubeID>Adventure Works</CubeID>      <MeasureGroupID>Fact Internet Sales 1</MeasureGroupID>      <PartitionID>Internet_Sales_2002</PartitionID>    </Source>    <Source>      <MeasureGroupID>Fact Internet Sales 1</MeasureGroupID>      <PartitionID>Internet_Sales_2003</PartitionID>    </Source>  
  </Sources>  
  <Target>  
    <DatabaseID Adventure Works DW Multidimensional 2012</DatabaseID>  
    <CubeID>Adventure Works</CubeID>  
    <MeasureGroupID>Fact Internet Sales 1</MeasureGroupID>  
    <PartitionID>Internet_Sales_2004</PartitionID>  
  </Target>  
</MergePartitions>  
```  
  
## See Also  
 [Target Element &#40;XMLA&#41;](../xml-elements-properties/target-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
