---
title: "Source Element (Synchronize) (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Source Element (Synchronize)"
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
ms.assetid: 0a857f91-771f-4c5e-8bf7-4bf17442d4df
author: minewiskan
ms.author: owend
manager: craigg
---
# Source Element (Synchronize) (XMLA)
  Represents a source database from which to synchronize a target database during a [Synchronize](../xml-elements-commands/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Synchronize>  
   <Source>  
      <Object>...</Object>  
      <ConnectionString>...</ConnectionString>  
   </Source>  
</Synchronize>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Synchronize](../xml-elements-commands/synchronize-element-xmla.md)|  
|Child elements|[ConnectionString](connectionstring-element-xmla.md), [Object](object-element-xmla.md)|  
  
## Remarks  
 The `Synchronize` command uses the `Source` element to establish a connection to and identify a database on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] with which to synchronize the target database.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
