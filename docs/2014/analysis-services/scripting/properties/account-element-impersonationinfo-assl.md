---
title: "Account Element (ImpersonationInfo) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Account Element (ImpersonationInfo)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Account element"
ms.assetid: aa3a1281-e42a-4926-875b-e6b81f4599c3
author: minewiskan
ms.author: owend
manager: craigg
---
# Account Element (ImpersonationInfo) (ASSL)
  Contains the name of the user account for the [ImpersonationInfo](../data-type/impersonationinfo-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<ImpersonationInfo  
   ...  
  <Account>...</Account>  
   ...  
</Action>  
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
|Parent elements|[ImpersonationInfo](../data-type/impersonationinfo-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `Account` element, as well as the value of the [Password](password-element-assl.md) element, is used for impersonation purposes if the value of the [ImpersonationMode](impersonationmode-element-assl.md) element for any element derived from the `ImpersonationInfo` data type is set to *ImpersonateAccount*.  
  
## See Also  
 [DataSourceImpersonationInfo Element &#40;ASSL&#41;](impersonationinfo-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
