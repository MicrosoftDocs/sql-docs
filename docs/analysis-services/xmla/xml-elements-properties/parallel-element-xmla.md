---
title: "Parallel Element (XMLA) | Microsoft Docs"
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
  - "Parallel Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#Parallel"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Parallel"
  - "microsoft.xml.analysis.parallel"
helpviewer_keywords: 
  - "Parallel element"
ms.assetid: 04726d94-37ee-460b-9744-d62b45f536b9
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Parallel Element (XMLA)
  Specifies how many processing jobs can run in parallel using the parent [Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Batch>  
   ....  
   <Parallel maxParallel="Integer">  
      <!-- An XMLA process command -->  
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
|Parent elements|[Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md)|  
|Child elements|[Process Element](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)|  
  
## Attributes  
  
|Attribute|Description|  
|---------------|-----------------|  
|maxParallel|Optional **Integer** attribute. Indicates the maximum number of threads on which to run commands in parallel. If not specified or set to 0, the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] determines an optimal number of threads based on the number of processors available on the computer.|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  