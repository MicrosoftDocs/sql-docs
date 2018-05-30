---
title: "NullKeyConvertedToUnknown Element (ASSL) | Microsoft Docs"
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
# NullKeyConvertedToUnknown Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Specifies the action to be taken when a null conversion error is encountered.  
  
## Syntax  
  
```xml  
  
<ErrorConfiguration>  
   ...  
      <NullKeyConvertedToUnknown>...</NullKeyConvertedToUnknown>  
   ...  
</ErrorConfiguration>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*IgnoreError*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ErrorConfiguration](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Null conversion errors occur when a null value is encountered in a key column and interpreted as the **Unknown** member. However, this error occurs only if the [NullProcessing](../../../analysis-services/scripting/properties/nullprocessing-element-assl.md) element for the [DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md) ancestor of the **ErrorConfiguration** parent element is set to *UnknownMember*.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*IgnoreError*|Processing ignores the error and continues.|  
|*ReportAndContinue*|Processing reports the error and continues.|  
|*ReportAndStop*|Processing reports the error and stops.|  
  
 The enumeration that corresponds to the allowed values for **NullKeyConvertedToUnknown** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ErrorOption>.  
  
## See Also  
 [ErrorConfiguration Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
