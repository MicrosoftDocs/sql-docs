---
title: "BeginTransaction Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# BeginTransaction Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Begins a transaction on the current session with an instance of Analysis Services.  
  
## Syntax  
  
```xml  
  
<Command>  
   <BeginTransaction />  
</Command>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The **BeginTransaction** command begins an active transaction on the current session. If an active transaction already exists, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance increments the reference count of transactions for the current session. If not, the instance will begin a new transaction and set the reference count for the current session to 1. If an active transaction is explicitly specified using the **BeginTransaction** command, all subsequent commands are executed inside the explicitly specified transaction.  
  
 When the current session is ended and the reference count for transactions is higher than zero, all active transactions are rolled back.  
  
 If there are no explicitly specified active transactions on the current session, every command issued on the current session is executed inside an implicitly defined transaction. The implicit transaction is committed if the command succeeds, or rolled back if the command fails.  
  
## See also
 [Cancel Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md)   
 [CommitTransaction Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/committransaction-element-xmla.md)   
 [RollbackTransaction Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
