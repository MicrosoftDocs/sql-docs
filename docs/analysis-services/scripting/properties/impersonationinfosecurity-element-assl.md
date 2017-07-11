---
title: "ImpersonationInfoSecurity Element (ASSL) | Microsoft Docs"
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
  - "ImpersonationInfoSecurity Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "ImpersonationInfoSecurity element"
ms.assetid: 583fec36-90ef-4d6a-9888-ece6ae865c53
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ImpersonationInfoSecurity Element (ASSL)
  Contains a read-only value that indicates if any changes were made to the security credentials that are supplied in the [ImpersonationInfo](../../../analysis-services/scripting/data-type/impersonationinfo-data-type-assl.md) data type.  
  
## Syntax  
  
```xml  
  
<ImpersonationInfo>  
   ...  
   <ImpersonationInfoSecurity>...</ImpersonationInfoSecurity>  
  
</ImpersonationInfo>...  
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
|Parent element|[ImpersonationInfo](../../../analysis-services/scripting/data-type/impersonationinfo-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*PasswordRemoved*|Password information has been removed from the supplied security credentials.|  
|*Unchanged*|No changes have been made to the supplied security credentials.|  
  
 The enumeration that corresponds to the allowed values for **ImpersonationInfoSecurity** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ImpersonationInfoSecurity>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  