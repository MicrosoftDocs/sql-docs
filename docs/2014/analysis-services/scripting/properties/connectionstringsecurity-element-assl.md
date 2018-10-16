---
title: "ConnectionStringSecurity Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ConnectionStringSecurity Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ConnectionStringSecurity"
helpviewer_keywords: 
  - "ConnectionStringSecurity element"
ms.assetid: f25c4448-bb0d-4945-bc84-9c015eefa0eb
author: minewiskan
ms.author: owend
manager: craigg
---
# ConnectionStringSecurity Element (ASSL)
  Specifies whether the user's password is stripped from the data source connection string for security purposes.  
  
## Syntax  
  
```xml  
  
<DataSource>  
   ...  
   <ConnectionStringSecurity>...</ConnectionStringSecurity>  
   ...  
</DataSource>  
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
|Parent element|[DataSource](../objects/datasource-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*PasswordRemoved*|The password has been stripped from the connection string.|  
|*Unchanged*|The connection string has not been modified.|  
  
 The enumeration corresponding to the allowed values for `ConnectionStringSecurity` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ConnectionStringSecurity>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
