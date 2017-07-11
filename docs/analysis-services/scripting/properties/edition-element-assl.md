---
title: "Edition Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Edition Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Edition"
helpviewer_keywords: 
  - "Edition element"
ms.assetid: 521e1286-097e-494f-b036-61047096e87e
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Edition Element (ASSL)
  Contains the read-only edition of the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] represented by the [Server](../../../analysis-services/scripting/objects/server-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Server>  
      ...  
      <Edition>...</Edition>  
   ...  
</Server>  
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
|Parent elements|[Server](../../../analysis-services/scripting/objects/server-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **Edition** element describes which edition of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] is installed. The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Standard*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Standard edition for 32-bit processors.|  
|*Enterprise*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Enterprise edition for 32-bit processors.|  
|*EnterpriseCore*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Enterprise edition: Core-based Licensing for 32-bit processors.|  
|*BusinessIntelligence*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Business Intelligence edition for 32-bit processors.|  
|*Developer*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Developer edition for 32-bit processors|  
|*Evaluation*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Evaluation edition for 64-bit processors.|  
|*Local*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Local edition for 32-bit processors.|  
|*Standard64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Standard edition for 64-bit processors.|  
|*Enterprise64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Enterprise edition for 64-bit processors.|  
|*EnterpriseCore64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Enterprise edition: Core-based Licensing for 64-bit processors.|  
|*BusinessIntelligence64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Business Intelligence edition for 64-bit processors.|  
|*Developer64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Developer edition for 64-bit processors|  
|*Evaluation64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Evaluation edition for 64-bit processors.|  
|*Local64*|[!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Local edition for 64-bit processors.|  
  
 The enumeration corresponding to the allowed values for **ServerEdition** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerEdition>.  
  
## See Also  
 [Version Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/version-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  