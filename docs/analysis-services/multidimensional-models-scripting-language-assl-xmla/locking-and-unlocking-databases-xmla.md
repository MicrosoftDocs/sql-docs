---
title: "Locking and Unlocking Databases (XMLA) | Microsoft Docs"
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
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "locking [XML for Analysis]"
  - "XML for Analysis, locking"
  - "XMLA, locking"
  - "unlocking objects"
ms.assetid: 451afa58-ce03-4ecc-8dd3-9e7e8559b5f1
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Locking and Unlocking Databases (XMLA)
  You can lock and unlock databases using, respectively, the [Lock](../../analysis-services/xmla/xml-elements-commands/lock-element-xmla.md) and [Unlock](../../analysis-services/xmla/xml-elements-commands/unlock-element-xmla.md) commands in XML for Analysis (XMLA). Typically, other XMLA commands automatically lock and unlock objects as needed to complete the command during execution. You can explicitly lock or unlock a database to perform multiple commands within a single transaction, such as a [Batch](../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) command, while preventing other applications from committing a write transaction to the database.  
  
## Locking Databases  
 The **Lock** command locks an object, either for shared or exclusive use, within the context of the currently active transaction. A lock on an object prevents transactions from committing until the lock is removed. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports two types of locks, shared locks and exclusive locks. For more information about the lock types supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Mode Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-properties/mode-element-xmla.md).  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] allows only databases to be locked. The [Object](../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md) element must contain an object reference to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. If the **Object** element is not specified or if the **Object** element refers to an object other than a database, an error occurs.  
  
> [!IMPORTANT]  
>  Only database administrators or server administrators can explicitly issue a **Lock** command.  
  
 Other commands implicitly issue a **Lock** command on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. Any operation that reads data or metadata from a database, such as any [Discover](../../analysis-services/xmla/xml-elements-methods-discover.md) method or an [Execute](../../analysis-services/xmla/xml-elements-methods-execute.md) method running a [Statement](../../analysis-services/xmla/xml-elements-commands/statement-element-xmla.md) command, implicitly issues a shared lock on the database. Any transaction that commits changes in data or metadata to an object on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, such as an **Execute** method running an [Alter](../../analysis-services/xmla/xml-elements-commands/alter-element-xmla.md) command, implicitly issues an exclusive lock on the database.  
  
## Unlocking Objects  
 The **Unlock** command removes a lock established within the context of the currently active transaction.  
  
> [!IMPORTANT]  
>  Only database administrators or server administrators can explicitly issue an **Unlock** command.  
  
 All locks are held in the context of the current transaction. When the current transaction is committed or rolled back, all locks defined within the transaction are automatically released.  
  
## See Also  
 [Lock Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/lock-element-xmla.md)   
 [Unlock Element &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-commands/unlock-element-xmla.md)   
 [Developing with XMLA in Analysis Services](../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)  
  
  