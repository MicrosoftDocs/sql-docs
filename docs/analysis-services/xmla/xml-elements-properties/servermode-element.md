---
title: "ServerMode Element | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: c2f8cb39-dad7-433b-b7b7-fb1625f76a84
caps.latest.revision: 5
author: "jeannt"
ms.author: "jeannt"
manager: "erikre"
---
# ServerMode Element
  The **ServerMode** server element specifies the mode the server is operating in.  
  
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
|Parent elements|[Server](../../../analysis-services/scripting/objects/server-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The server operates in either one of the following modes:  
  
|Value|Description|  
|-----------|-----------------|  
|*Multidimensional*|Multidimensional and Data Mining Mode|  
|*Tabular*|Tabular mode|  
|*SharePoint*|SharePoint mode|  
  
## See Also  
 [Server](../../../analysis-services/scripting/objects/server-element-assl.md)  
  
  