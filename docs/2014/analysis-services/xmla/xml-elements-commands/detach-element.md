---
title: "Detach Element | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
helpviewer_keywords: 
  - "Detach command"
ms.assetid: adbc7920-2a4a-4842-9e6f-37006fa19ff8
author: minewiskan
ms.author: owend
manager: craigg
---
# Detach Element
  Detaches a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database from the current server instance.  
  
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
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Object](../xml-elements-properties/object-element-xmla.md)<br /><br /> [Password](../xml-elements-properties/password-element-xmla.md)|  
  
## See Also  
 <xref:Microsoft.AnalysisServices.Database.Detach%2A>   
 [Attach Element](attach-element.md)   
 [Attach and Detach Analysis Services Databases](../../multidimensional-models/attach-and-detach-analysis-services-databases.md)   
 [Move an Analysis Services Database](../../multidimensional-models/move-an-analysis-services-database.md)   
 [Database ReadWriteModes](../../multidimensional-models/database-readwritemodes.md)   
 [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md)  
  
  
