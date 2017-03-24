---
title: "ConnectionString Element (XMLA) | Microsoft Docs"
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
  - "ConnectionString Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "microsoft.xml.analysis.connectionstring"
  - "urn:schemas-microsoft-com:xml-analysis#ConnectionString"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ConnectionString"
helpviewer_keywords: 
  - "ConnectionString element"
ms.assetid: 3b0575aa-79ed-4f14-ae7e-dd587af4cdb1
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# ConnectionString Element (XMLA)
  Contains a connection string used by the parent [Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md) or [Source](../../../analysis-services/xmla/xml-elements-properties/source-element-xmla.md) element.  
  
## Syntax  
  
```xml  
  
<Location> <!-- or Source -->  
   ...  
   <ConnectionString>...</ConnectionString>  
   ...  
</Location>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|See the table below.|  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|[Source](../../../analysis-services/xmla/xml-elements-properties/source-element-xmla.md)|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Location](../../../analysis-services/xmla/xml-elements-properties/location-element-xmla.md), [Source](../../../analysis-services/xmla/xml-elements-properties/source-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For **Location** elements, the **ConnectionString** element contains the connection string used by the **Restore** or **Synchronize** command to either update a local data source or connect to a remote instance.  
  
 For **Source** elements, the **ConnectionString** element contains the connection string used by the **Synchronize** command to connect to the source instance.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [Restore Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/synchronize-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/xml-elements-properties.md)  
  
  