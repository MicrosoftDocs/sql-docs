---
title: "Password Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Password Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Password element"
ms.assetid: ee756b01-fb08-4a9a-8c2a-7c04af0f8658
author: minewiskan
ms.author: owend
manager: craigg
---
# Password Element (ASSL)
  Contains the password of the user account for the [ImpersonationInfo](../data-type/impersonationinfo-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ImpersonationInfo  
   ...  
  <Password>...</Password>  
   ...  
</ImpersonationInfo>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ImpersonationInfo](../data-type/impersonationinfo-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `Password` element, as well as the value of the [Account](account-element-impersonationinfo-assl.md) element, is used for impersonation purposes if the value of the [ImpersonationMode](impersonationmode-element-assl.md) element for any element derived from the `ImpersonationInfo` data type is set to *ImpersonateAccount*.  
  
 Only members of the server administrator role for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance can provide a blank value for the `Password` element  
  
## See Also  
 [DataSourceImpersonationInfo Element &#40;ASSL&#41;](impersonationinfo-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
