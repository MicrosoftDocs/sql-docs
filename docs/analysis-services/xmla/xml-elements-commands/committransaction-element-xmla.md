---
title: "CommitTransaction Element (XMLA) | Microsoft Docs"
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
# CommitTransaction Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Commits a transaction on the current session with a Analysis Services instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <CommitTransaction />  
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
 The **CommitTransaction** command commits an active transaction, explicitly defined using the **BeginTransaction** element, on the current session. If an active transaction does not already exist, an error occurs. If an active transaction already exists, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance decrements the reference count of transactions for the current session. If the reference count of explicitly defined active transactions reaches zero, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance commits the transaction.  
  
## See also
 [BeginTransaction Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/begintransaction-element-xmla.md)   
 [Cancel Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/cancel-element-xmla.md)   
 [RollbackTransaction Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/rollbacktransaction-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
