---
title: "Detach Element | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Detach command"
ms.assetid: adbc7920-2a4a-4842-9e6f-37006fa19ff8
caps.latest.revision: 10
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Detach Element
  Detaches a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database from the current server instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Detach>  
      <Object>...</Object>  
      <Password>...</Password>  
   </Detach>  
</Command>  
  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../2014/analysis-services/dev-guide/command-element-xmla.md)|  
|Child elements|[Object](../../../2014/analysis-services/dev-guide/object-element-xmla.md)<br /><br /> [Password](../../../2014/analysis-services/dev-guide/password-element-xmla.md)|  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [Attach Element](../../../2014/analysis-services/dev-guide/attach-element.md)   
 [Attach and Detach Analysis Services Databases](../../../2014/analysis-services/attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](../../../2014/analysis-services/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../../2014/analysis-services/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../../2014/analysis-services/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
  