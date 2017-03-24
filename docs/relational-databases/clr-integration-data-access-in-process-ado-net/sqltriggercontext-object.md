---
title: "SqlTriggerContext Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "SqlTriggerContext object"
  - "triggers [CLR integration]"
  - "context [CLR integration]"
ms.assetid: 472a2d0b-64ae-4877-8f11-a5620aa698b7
caps.latest.revision: 18
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SqlTriggerContext Object
  The **SqlTriggerContext** class provides context information about the trigger. This contextual information includes the type of action that caused the trigger to fire, which columns were modified in an UPDATE operation, and, in the case of a data definition language (DDL) trigger, an XML **EventData** structure that describes the triggering operation. For more information and examples of how to use the **SqlTriggerContext** class, see [CLR Triggers](http://msdn.microsoft.com/library/302a4e4a-3172-42b6-9cc0-4a971ab49c1c).  
  
 For more information, see the **Microsoft.SqlServer.Server.SqlTriggerContext** class reference documenation in the .NET Framework SDK documentation.  
  
## See Also  
 [CLR Triggers](http://msdn.microsoft.com/library/302a4e4a-3172-42b6-9cc0-4a971ab49c1c)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  