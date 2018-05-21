---
title: "Password Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# Password Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the password of the user account for the [ImpersonationInfo](../../../analysis-services/scripting/data-type/impersonationinfo-data-type-assl.md) element.  
  
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
|Parent element|[ImpersonationInfo](../../../analysis-services/scripting/data-type/impersonationinfo-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **Password** element, as well as the value of the [Account](../../../analysis-services/scripting/properties/account-element-impersonationinfo-assl.md) element, is used for impersonation purposes if the value of the [ImpersonationMode](../../../analysis-services/scripting/properties/impersonationmode-element-assl.md) element for any element derived from the **ImpersonationInfo** data type is set to *ImpersonateAccount*.  
  
 Only members of the server administrator role for the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance can provide a blank value for the **Password** element  
  
## See Also  
 [DataSourceImpersonationInfo Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/datasourceimpersonationinfo-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
