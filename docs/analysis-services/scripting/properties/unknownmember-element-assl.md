---
title: "UnknownMember Element (ASSL) | Microsoft Docs"
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
  - "UnknownMember Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "UnknownMember"
helpviewer_keywords: 
  - "UnknownMember element"
ms.assetid: 5558961e-e3c6-4f4e-817d-5b12b0734c03
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# UnknownMember Element (ASSL)
  Indicates whether the unknown member is visible.  
  
## Syntax  
  
```xml  
  
<Dimension>  
      ...  
   <UnknownMember>...</UnknownMember>  
   ...  
</Dimension>  
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
|Parent element|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Visible*|The unknown member exists and is displayed.|  
|*Hidden*|The unknown member exists but is not displayed.|  
|*None*|The unknown member is not used.|  
  
 The enumeration that corresponds to the allowed values for **UnknownMember** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.UnknownMemberBehavior>.  
  
## See Also  
 [UnknownMemberName Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/unknownmembername-element-assl.md)   
 [UnknownMemberTranslation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/unknownmembertranslation-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  