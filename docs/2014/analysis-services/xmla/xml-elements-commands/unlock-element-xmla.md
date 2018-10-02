---
title: "Unlock Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Unlock Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Unlock"
  - "urn:schemas-microsoft-com:xml-analysis#Unlock"
  - "microsoft.xml.analysis.unlock"
helpviewer_keywords: 
  - "Unlock command"
ms.assetid: 46425b33-baa2-41ad-803a-34d2fb4b2cab
author: minewiskan
ms.author: owend
manager: craigg
---
# Unlock Element (XMLA)
  Unlocks a specified lock on a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Unlock>  
      <ID>...</ID>  
   </Unlock>  
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
|Child elements|[ID](../xml-elements-properties/id-element-xmla.md)|  
  
## Remarks  
 The `Unlock` command removes a lock established within the context of the currently active transaction. Only database administrators or server administrators can explicitly issue an `Unlock` command.  
  
 All locks are held in the context of the current transaction. When the current transaction is committed or rolled back, all locks defined within the transaction are automatically released.  
  
## See Also  
 [Lock Element &#40;XMLA&#41;](lock-element-xmla.md)   
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
