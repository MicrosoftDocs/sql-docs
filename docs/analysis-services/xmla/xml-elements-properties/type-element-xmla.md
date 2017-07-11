---
title: "Type Element (XMLA) | Microsoft Docs"
ms.custom: ""
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
  - "Type Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.type"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Type"
  - "urn:schemas-microsoft-com:xml-analysis#Type"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 5d898123-a635-402a-be86-8249d7304fa4
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Type Element (XMLA)
  Determines the type of processing to be performed by the [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Process>  
...  
   <Type>...</Type>  
...  
</Process>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about processing options available to objects on an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], see [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md).  
  
 The value of the **Type** element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ProcessFull*|Drops all data in the affected object, and then processes the affected object.|  
|*ProcessAdd*|Adds new data to the affected object.|  
|*ProcessUpdate*|Refreshes the data in the affected object.|  
|*ProcessIndexes*|Creates or rebuilds indexes and aggregations in the affected object.|  
|*ProcessScriptCache*|If the cube is processed, the server will rebuild the MDX script cache. If not, an error will be raised.<br /><br /> **Note** Applies only to cube.|  
|*ProcessData*|Processes data only in the affected object.|  
|*ProcessDefault*|Detects the state of the affected object and then performs the appropriate processing option on the affected object to fully optimize it and return it to a fully processed state.|  
|*ProcessClear*|Drops the data in the affected object and all related objects.|  
|*ProcessStructure*|Processes the structure only of the affected object.|  
|*ProcessClearStructureOnly*|Clears data only from the affected object.|  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  