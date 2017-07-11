---
title: "NotificationTechnique Element (ASSL) | Microsoft Docs"
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
  - "NotificationTechnique Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "NotificationTechnique element"
ms.assetid: 80c43de3-f147-4bf5-bb85-da9d182ce415
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NotificationTechnique Element (ASSL)
  Specifies whether [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or an external client application processes the notifications.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingBinding>  
   <NotificationTechnique>...</NotificationTechnique>  
</ProactiveCachingBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Client*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ProactiveCachingBinding](../../../analysis-services/scripting/data-type/proactivecachingbinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Client*|External client application processes the notification.|  
|*Server*|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] processes the notification.|  
  
 The element that corresponds to the parent of **NotificationTechnique** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCachingBinding>.  
  
 The enumeration that corresponds to the allowed values for **NotificationTechnique** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.NotificationTechnique>.  
  
## See Also  
 [ProactiveCachingBinding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/proactivecachingbinding-data-type-assl.md)  
  
  