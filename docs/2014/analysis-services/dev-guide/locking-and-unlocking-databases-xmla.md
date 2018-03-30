---
title: "Locking and Unlocking Databases (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "locking [XML for Analysis]"
  - "XML for Analysis, locking"
  - "XMLA, locking"
  - "unlocking objects"
ms.assetid: 451afa58-ce03-4ecc-8dd3-9e7e8559b5f1
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Locking and Unlocking Databases (XMLA)
  You can lock and unlock databases using, respectively, the [Lock](../../../2014/analysis-services/dev-guide/lock-element-xmla.md) and [Unlock](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md) commands in XML for Analysis (XMLA). Typically, other XMLA commands automatically lock and unlock objects as needed to complete the command during execution. You can explicitly lock or unlock a database to perform multiple commands within a single transaction, such as a [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md) command, while preventing other applications from committing a write transaction to the database.  
  
## Locking Databases  
 The `Lock` command locks an object, either for shared or exclusive use, within the context of the currently active transaction. A lock on an object prevents transactions from committing until the lock is removed. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports two types of locks, shared locks and exclusive locks. For more information about the lock types supported by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Mode Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/mode-element-xmla.md).  
  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] allows only databases to be locked. The [Object](../../../2014/analysis-services/dev-guide/object-element-xmla.md) element must contain an object reference to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. If the `Object` element is not specified or if the `Object` element refers to an object other than a database, an error occurs.  
  
> [!IMPORTANT]  
>  Only database administrators or server administrators can explicitly issue a `Lock` command.  
  
 Other commands implicitly issue a `Lock` command on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. Any operation that reads data or metadata from a database, such as any [Discover](../../../2014/analysis-services/dev-guide/discover-method-xmla.md) method or an [Execute](../../../2014/analysis-services/dev-guide/execute-method-xmla.md) method running a [Statement](../../../2014/analysis-services/dev-guide/statement-element-xmla.md) command, implicitly issues a shared lock on the database. Any transaction that commits changes in data or metadata to an object on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, such as an `Execute` method running an [Alter](../../../2014/analysis-services/dev-guide/alter-element-xmla.md) command, implicitly issues an exclusive lock on the database.  
  
## Unlocking Objects  
 The `Unlock` command removes a lock established within the context of the currently active transaction.  
  
> [!IMPORTANT]  
>  Only database administrators or server administrators can explicitly issue an `Unlock` command.  
  
 All locks are held in the context of the current transaction. When the current transaction is committed or rolled back, all locks defined within the transaction are automatically released.  
  
## See Also  
 [Lock Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/lock-element-xmla.md)   
 [Unlock Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/unlock-element-xmla.md)   
 [Developing with XMLA in Analysis Services](../../../2014/analysis-services/dev-guide/developing-with-xmla-in-analysis-services.md)  
  
  