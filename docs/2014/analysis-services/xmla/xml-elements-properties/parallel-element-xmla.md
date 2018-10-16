---
title: "Parallel Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "2015-12-07"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Parallel Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Parallel"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Parallel"
  - "microsoft.xml.analysis.parallel"
helpviewer_keywords: 
  - "Parallel element"
ms.assetid: 04726d94-37ee-460b-9744-d62b45f536b9
author: minewiskan
ms.author: owend
manager: craigg
---
# Parallel Element (XMLA)
  Identifies commands to be run in parallel by the parent [Batch](../xml-elements-commands/batch-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Batch>  
   ....  
   <Parallel maxParallel="Integer">  
      <!-- One or more XMLA commands -->  
   </Parallel>  
   ....  
</Batch>  
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
|Parent elements|[Batch](../xml-elements-commands/batch-element-xmla.md)|  
|Child elements|[Process Element](../xml-elements-commands/process-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|maxParallel|Optional `Integer` attribute. Indicates the maximum number of threads on which to run commands in parallel. If not specified or set to 0, the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] determines an optimal number of threads based on the number of processors available on the computer.|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
