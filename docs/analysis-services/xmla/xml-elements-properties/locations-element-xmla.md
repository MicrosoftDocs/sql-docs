---
title: "Locations Element (XMLA) | Microsoft Docs"
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
  - "Locations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Locations"
  - "urn:schemas-microsoft-com:xml-analysis#Locations"
  - "microsoft.xml.analysis.locations"
helpviewer_keywords: 
  - "Locations element"
ms.assetid: 630929cb-f0dc-472a-86bc-28b67e907c3d
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# Locations Element (XMLA)
  Contains a collection of [Location](../../../analysis-services/xmla/xml-elements-properties/query-element-xmla.md) elements used by the parent [Backup](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md), [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) , or [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
< Backup> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Locations>  
      <Location>...</Location>  
   </Locations>  
   ...  
</Backup>  
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
|Parent elements|[Backup](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md), [Restore](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md) , or [Synchronize](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)|  
|Child elements|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md)|  
  
## Remarks  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  