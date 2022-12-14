---
title: "SqlTriggerContext Object"
description: In SQL Server CLR integration, the SqlTriggerContext class provides context information for a trigger including type of action and columns modified in operation.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "SqlTriggerContext object"
  - "triggers [CLR integration]"
  - "context [CLR integration]"
ms.assetid: 472a2d0b-64ae-4877-8f11-a5620aa698b7
---
# SqlTriggerContext Object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SqlTriggerContext** class provides context information about the trigger. This contextual information includes the type of action that caused the trigger to fire, which columns were modified in an UPDATE operation, and, in the case of a data definition language (DDL) trigger, an XML **EventData** structure that describes the triggering operation. For more information and examples of how to use the **SqlTriggerContext** class, see [CLR Triggers](/dotnet/framework/data/adonet/sql/clr-triggers).  
  
 For more information, see the **Microsoft.SqlServer.Server.SqlTriggerContext** class reference documentation in the .NET Framework SDK documentation.  
  
## See Also  
 [CLR Triggers](/dotnet/framework/data/adonet/sql/clr-triggers)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
