---
title: "results Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "results Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.results"
  - "urn:schemas-microsoft-com:xml-analysis#results"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#results"
helpviewer_keywords: 
  - "results element"
ms.assetid: 3249a17a-7bfa-4753-b605-8f611ba7ae2b
author: minewiskan
ms.author: owend
manager: craigg
---
# results Element (XMLA)
  Contains a collection of [root](root-element-xmla.md) elements returned by the [Execute](../xml-elements-methods-execute.md) method using the [Batch](../xml-elements-commands/batch-element-xmla.md) command.  
  
 **Namespace** http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults  
  
## Syntax  
  
```xml  
  
<return>  
   <results>  
      <root>...</root>  
   </results>  
</return>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[return](return-element-xmla.md)|  
|Child elements|[root](root-element-xmla.md)|  
  
## Remarks  
 If a `Batch` command is executed by the `Execute` method, the `return` element contains a single `results` element instead of a single `root` element. The content of the `results` element depends on the settings used to run the `Batch` command.  
  
 For non-transactional `Batch` commands, the `results` element contains one `root` element for each command executed by the `Batch` command, whether the command completes successfully or unsuccessfully. For transactional `Batch` commands, the `results` element contains only one `root` element, which contains the error information for the command that failed within the `Batch` command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
