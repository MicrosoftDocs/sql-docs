---
title: "Bindings Element (XMLA) | Microsoft Docs"
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
  - "Bindings Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.bindings"
  - "urn:schemas-microsoft-com:xml-analysis#Bindings"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Bindings"
helpviewer_keywords: 
  - "Bindings element"
ms.assetid: caa34cab-f61f-4f39-b800-af1601714daa
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Bindings Element (XMLA)
  Contains a collection of [Binding](../../../analysis-services/xmla/xml-elements-properties/binding-element-xmla.md) elements for the parent [Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) or [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Batch> <!-- or Process>  
...  
   <Bindings>  
      <Binding>...</Binding>  
   </Bindings>  
...  
</Alter>  
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
|Parent elements|[Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md), [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)|  
|Child elements|[Binding](../../../analysis-services/xmla/xml-elements-properties/binding-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  