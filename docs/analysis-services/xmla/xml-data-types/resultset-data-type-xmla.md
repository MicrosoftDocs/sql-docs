---
title: "Resultset Data Type (XMLA) | Microsoft Docs"
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
  - "Resultset Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Resultset"
  - "urn:schemas-microsoft-com:xml-analysis#Resultset"
  - "Resultset"
helpviewer_keywords: 
  - "Resultset data type"
ms.assetid: 45e7d7d6-1f89-4dc8-b39d-9270ea2db541
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Resultset Data Type (XMLA)
  Defines an abstract primitive data type that represents data returned from a [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) or [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method call.  
  
 **Namespace** urn:schemas-microsoft-com:xml-analysis:resultset  
  
## Syntax  
  
```xml  
  
<Resultset>  
   <Exception>...</Exception>  
   <Messages>...</Messages>  
</Resultset>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[MDDataSet](../../../analysis-services/xmla/xml-data-types/mddataset-data-type-xmla.md), [olapxmla_EmptyResult](../../../analysis-services/xmla/xml-data-types/emptyresult-data-type-xmla.md), [Rowset](../../../analysis-services/xmla/xml-data-types/rowset-data-type-xmla.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Exception](../../../analysis-services/xmla/xml-elements-properties/exception-element-xmla.md), [Messages](../../../analysis-services/xmla/xml-elements-properties/messages-element-xmla.md)|  
|Derived elements|None|  
  
## Remarks  
 The **Resultset** data type is a self-describing XML result set that can include both schema and data, depending on the type of information to be returned.  
  
## See Also  
 [XML Data Types &#40;XMLA&#41;](../../../analysis-services/xmla/xml-data-types/xml-data-types-xmla.md)  
  
  