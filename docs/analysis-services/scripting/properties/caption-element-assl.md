---
title: "Caption Element (ASSL) | Microsoft Docs"
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
  - "Caption Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Caption"
helpviewer_keywords: 
  - "Caption element"
ms.assetid: ed2be851-0ddc-4fa5-8aee-b2acb2e6d25e
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Caption Element (ASSL)
  Contains the caption for the associated parent element.  
  
## Syntax  
  
```xml  
  
<Action> <!-- or Translation -->  
   ...  
  <Caption>...</Caption>  
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
|Parent element|[Action](../../../analysis-services/scripting/objects/action-element-assl.md), [Translation](../../../analysis-services/scripting/objects/translation-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of **Caption** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Action> and <xref:Microsoft.AnalysisServices.Translation>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  