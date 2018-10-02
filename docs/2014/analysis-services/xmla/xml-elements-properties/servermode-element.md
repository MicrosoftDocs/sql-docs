---
title: "ServerMode Element | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
ms.assetid: c2f8cb39-dad7-433b-b7b7-fb1625f76a84
author: minewiskan
ms.author: owend
manager: craigg
---
# ServerMode Element
  The `ServerMode` server element specifies the mode the server is operating in.  
  
## Syntax  
  
```xml  
  
<Server>  
...  
   <ddl300:ServerMode>...</ddl300:ServerMode>  
...  
</Server>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|(none)|  
|Cardinality|0-1: Optional element that can occur only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Server](../../scripting/objects/server-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The server operates in either one of the following modes:  
  
|Value|Description|  
|-----------|-----------------|  
|*Multidimensional*|Multidimensional and Data Mining Mode|  
|*Tabular*|Tabular mode|  
|*SharePoint*|SharePoint mode|  
  
## See Also  
 [Server](../../scripting/objects/server-element-assl.md)  
  
  
