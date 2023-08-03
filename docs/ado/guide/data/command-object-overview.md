---
title: "Command Object Overview"
description: "Command Object Overview"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "command object [ADO]"
---
# Command Object Overview
With a **Command** object, you can do the following:  
  
-   Define the executable text of the command (for example, a SQL statement or a stored procedure) by using the **CommandText** property.  
  
-   Define parameterized queries or stored procedure arguments by using **Parameter** objects and the **Parameters** collection.  
  
-   Execute a command and return a **Recordset** object, if appropriate, by using the **Execute** method.  
  
-   Specify the type of command by using the **CommandType** property prior to execution to optimize performance.  
  
-   Specify specific information about the command text by using the **Dialect** property of the **Command** object.  
  
-   Control whether the provider saves a prepared (or compiled) version of the command prior to execution by using the **Prepared** property.  
  
-   Set the number of seconds that a provider will wait for a command to execute by using the **CommandTimeout** property.  
  
-   Associate an open connection with a **Command** object by setting its **ActiveConnection** property.  
  
-   Set the **Name** property to identify the **Command** object as a method on the associated **Connection** object.  
  
-   Pass a **Command** object to the **Source** property of a **Recordset** in order to obtain data.  
  
-   Pass a **Stream** object containing a command (for example, an XML command) to a provider that supports it.
