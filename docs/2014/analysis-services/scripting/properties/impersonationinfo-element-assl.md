---
title: "ImpersonationInfo Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ImpersonationInfo Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ImpersonationInfo element"
ms.assetid: d4b9c372-1023-43f7-97e9-b0a90f544fbb
author: minewiskan
ms.author: owend
manager: craigg
---
# ImpersonationInfo Element (ASSL)
  Contains the information that is used to determine impersonation behavior when accessing or executing an assembly.  
  
## Syntax  
  
```xml  
  
<Assembly> <!-- or one of the elements listed in the Element Relationships table -->  
   <ImpersonationInfo>  
      <!-- Child elements are only those that are inherited from ImpersonationInfo -->  
   </ImpersonationInfo>  
</Assembly>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[ImpersonationInfo](../data-type/impersonationinfo-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Assembly](../data-type/assembly-data-type-assl.md), [DataSource](../data-type/datasource-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
