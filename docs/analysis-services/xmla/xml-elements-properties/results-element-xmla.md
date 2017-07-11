---
title: "results Element (XMLA) | Microsoft Docs"
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
apiname: 
  - "results Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.results"
  - "urn:schemas-microsoft-com:xml-analysis#results"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#results"
helpviewer_keywords: 
  - "results element"
ms.assetid: 3249a17a-7bfa-4753-b605-8f611ba7ae2b
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# results Element (XMLA)
  Contains a collection of [root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md) elements returned by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method using the [Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) command.  
  
 **Namespace** `http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults`  
  
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
|Parent elements|[return](../../../analysis-services/xmla/xml-elements-properties/return-element-xmla.md)|  
|Child elements|[root](../../../analysis-services/xmla/xml-elements-properties/root-element-xmla.md)|  
  
## Remarks  
 If a **Batch** command is executed by the **Execute** method, the **return** element contains a single **results** element instead of a single **root** element. The content of the **results** element depends on the settings used to run the **Batch** command.  
  
 For non-transactional **Batch** commands, the **results** element contains one **root** element for each command executed by the **Batch** command, whether the command completes successfully or unsuccessfully. For transactional **Batch** commands, the **results** element contains only one **root** element, which contains the error information for the command that failed within the **Batch** command.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  