---
title: "InstanceSelection Element (ASSL) | Microsoft Docs"
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
  - "InstanceSelection Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "InstanceSelection element"
ms.assetid: 908a2da9-274c-40d2-87dc-4641cb8d77e6
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# InstanceSelection Element (ASSL)
  Provides a hint to client applications to suggest how a list of items should be displayed, based on the expected number of items in the list.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <InstanceSelection>...</InstanceSelection>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*None*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the following strings:  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|Do not display a selection list. Allow users to enter values directly.|  
|*DropDown*|The number of items is small enough to display in a dropdown list.|  
|*List*|The number of items is too large for a dropdown list but does not require filtering.|  
|*FilteredList*|The number of items is large enough to require users to filter the items to be displayed.|  
|*MandatoryFilter*|The number of items is so large that the display must always be filtered.|  
  
 The enumeration that corresponds to the allowed values for **InstanceSelection** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.InstanceSelection>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  