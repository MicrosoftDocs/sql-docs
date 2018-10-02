---
title: "RollbackTransaction Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RollbackTransaction Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#RollbackTransaction"
  - "microsoft.xml.analysis.rollbacktransaction"
  - "urn:schemas-microsoft-com:xml-analysis#RollbackTransaction"
helpviewer_keywords: 
  - "RollbackTransaction command"
ms.assetid: 40e7dc00-656f-412f-97f0-d05bf7caa196
author: minewiskan
ms.author: owend
manager: craigg
---
# RollbackTransaction Element (XMLA)
  Rolls back a transaction on the current session with an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Syntax  
  
```xml  
  
<Command>  
   <RollbackTransaction />  
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
|Child elements|None|  
  
## Remarks  
 The `RollbackTransaction` command rolls back all active transactions, explicitly defined using the `BeginTransaction` element, on the current session. If an active transaction does not already exist, an error occurs. If an active transaction already exists, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance decrements the reference count of transactions for the current session to zero, rolling back all active transactions.  
  
## See Also  
 [BeginTransaction Element &#40;XMLA&#41;](begintransaction-element-xmla.md)   
 [Cancel Element &#40;XMLA&#41;](cancel-element-xmla.md)   
 [CommitTransaction Element &#40;XMLA&#41;](committransaction-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
