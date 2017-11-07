---
title: "WritebackTableCreation Element (XMLA) | Microsoft Docs"
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
  - "WritebackTableCreation Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#WritebackTableCreation"
  - "microsoft.xml.analysis.writebacktablecreation"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#WritebackTableCreation"
helpviewer_keywords: 
  - "WritebackTableCreation element"
ms.assetid: e9579d63-e28c-4d4e-9f4a-21c5da24c276
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# WritebackTableCreation Element (XMLA)
  Determines whether a writeback table is created during the [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) operation.  
  
## Syntax  
  
```xml  
  
<Process>  
...  
   <WritebackTableCreation>...</WritebackTableCreation>  
...  
</Process>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about processing options available to objects on an Analysis Services instance, see [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The value of the **WritebackTableCreation** element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Create*|Create a new writeback table, if one does not exist. If a writeback table already exists, an error occurs.|  
|*CreateAlways*|Create a new writeback table, overwriting any existing writeback table.|  
|*UseExisting*|Use the existing writeback table, if one already exists. If one does not exist, an error occurs.|  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  