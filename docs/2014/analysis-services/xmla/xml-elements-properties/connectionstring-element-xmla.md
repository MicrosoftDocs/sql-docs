---
title: "ConnectionString Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ConnectionString Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.connectionstring"
  - "urn:schemas-microsoft-com:xml-analysis#ConnectionString"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#ConnectionString"
helpviewer_keywords: 
  - "ConnectionString element"
ms.assetid: 3b0575aa-79ed-4f14-ae7e-dd587af4cdb1
author: minewiskan
ms.author: owend
manager: craigg
---
# ConnectionString Element (XMLA)
  Contains a connection string used by the parent [Location](location-element-xmla.md) or [Source](source-element-xmla.md) element.  
  
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
|Cardinality - Ancestor or Parent|Cardinality|  
|[Location](location-element-xmla.md)|1-1: Required element that occurs once and only once.|  
|[Source](source-element-xmla.md)|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Location](location-element-xmla.md), [Source](source-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 For `Location` elements, the `ConnectionString` element contains the connection string used by the `Restore` or `Synchronize` command to either update a local data source or connect to a remote instance.  
  
 For `Source` elements, the `ConnectionString` element contains the connection string used by the `Synchronize` command to connect to the source instance.  
  
 For more information about backing up and restoring objects, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [Restore Element &#40;XMLA&#41;](../xml-elements-commands/restore-element-xmla.md)   
 [Synchronize Element &#40;XMLA&#41;](../xml-elements-commands/synchronize-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
