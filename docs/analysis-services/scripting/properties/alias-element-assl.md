---
title: "Alias Element (ASSL) | Microsoft Docs"
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
  - "Alias Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Alias"
helpviewer_keywords: 
  - "Alias element"
ms.assetid: 674fdb06-e33c-4f35-bd6a-d9bbb13ececa
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Alias Element (ASSL)
  Defines an alias for an [Account](../../../analysis-services/scripting/objects/account-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Aliases>  
      <Alias>...</Alias>  
</Aliases>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Aliases](../../../analysis-services/scripting/collections/aliases-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **Alias** element is used as an alternate name for the account defined by the **Account** parent element, and helps to identify the combination of account type and aggregation function in a user interface.  
  
 The element that corresponds to the parent of the **Aliases** collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Account>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  