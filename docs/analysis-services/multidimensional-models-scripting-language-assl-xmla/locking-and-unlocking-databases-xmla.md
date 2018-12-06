---
title: "Locking and Unlocking Databases (XMLA) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Locking and Unlocking Databases (XMLA)
  You can lock and unlock databases using, respectively, the [Lock](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/lock-element-xmla) and [Unlock](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/lock-element-xmla) commands in XML for Analysis (XMLA). Typically, other XMLA commands automatically lock and unlock objects as needed to complete the command during execution. You can explicitly lock or unlock a database to perform multiple commands within a single transaction, such as a [Batch](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/batch-element-xmla) command, while preventing other applications from committing a write transaction to the database.  
  
## Locking Databases  
 The **Lock** command locks an object, either for shared or exclusive use, within the context of the currently active transaction. A lock on an object prevents transactions from committing until the lock is removed. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports two types of locks, shared locks and exclusive locks. For more information about the lock types supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Mode Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/mode-element-xmla).  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] allows only databases to be locked. The [Object](https://docs.microsoft.com/bi-reference/xmla/xml-elements-properties/object-element-xmla) element must contain an object reference to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. If the **Object** element is not specified or if the **Object** element refers to an object other than a database, an error occurs.  
  
> [!IMPORTANT]  
>  Only database administrators or server administrators can explicitly issue a **Lock** command.  
  
 Other commands implicitly issue a **Lock** command on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. Any operation that reads data or metadata from a database, such as any [Discover](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-discover) method or an [Execute](https://docs.microsoft.com/bi-reference/xmla/xml-elements-methods-execute) method running a [Statement](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/statement-element-xmla) command, implicitly issues a shared lock on the database. Any transaction that commits changes in data or metadata to an object on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, such as an **Execute** method running an [Alter](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/alter-element-xmla) command, implicitly issues an exclusive lock on the database.  
  
## Unlocking Objects  
 The **Unlock** command removes a lock established within the context of the currently active transaction.  
  
> [!IMPORTANT]  
>  Only database administrators or server administrators can explicitly issue an **Unlock** command.  
  
 All locks are held in the context of the current transaction. When the current transaction is committed or rolled back, all locks defined within the transaction are automatically released.  
  
## See Also  
 [Lock Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/lock-element-xmla)   
 [Unlock Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/lock-element-xmla)   
 [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  
