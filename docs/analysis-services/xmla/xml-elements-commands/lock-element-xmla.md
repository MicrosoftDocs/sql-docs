---
title: "Lock Element (XMLA) | Microsoft Docs"
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
# Lock Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Locks a specified object on a Analysis Services instance.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Lock>  
      <ID>...</ID>  
      <Object>...</Object>  
      <Mode>...</Mode>  
   </Lock>  
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
|Child elements|[ID](../../../analysis-services/xmla/xml-elements-properties/id-element-xmla.md), [Mode](../../../analysis-services/xmla/xml-elements-properties/mode-element-xmla.md), [Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md)|  
  
## Remarks  
 The **Lock** command locks an object, either for shared or exclusive use, within the context of the currently active transaction. Only database administrators or server administrators can explicitly issue a **Lock** command. A lock on an object prevents transactions from committing until the lock is removed. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports two types of locks, shared locks and exclusive locks. For more information about the lock types supported by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], see [Mode Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/mode-element-xmla.md).  
  
 [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] allows only databases to be locked. The **Object** element must contain an object reference to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database. If the **Object** element is not specified or if the **Object** element refers to an object other than a database, an error occurs.  
  
 Other commands implicitly issue a **Lock** command on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database. Any operation that reads data or metadata from a database, such as any **Discover** method or an **Execute** method running a **Statement** command, implicitly issues a shared lock on the database. Any transaction that commits changes in data or metadata to an object on an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database, such as an **Execute** method running an **Alter** command, implicitly issues an exclusive lock on the database.  
  
 All locks are held in the context of the current transaction. When the current transaction is committed or rolled back, all locks defined within the transaction are automatically released.  
  
## See also
 [Unlock Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/unlock-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
