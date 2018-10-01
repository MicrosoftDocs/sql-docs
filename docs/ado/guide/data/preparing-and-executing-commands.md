---
title: "Preparing and Executing Commands | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "Command object [ADO], preparing and executing commands"
ms.assetid: 7448d9ee-7f4b-47e3-be54-2df8c9bbac32
author: MightyPen
ms.author: genemi
manager: craigg
---
# Preparing and Executing Commands
Commands are instructions issued to a provider to perform some operations against the underlying data source. An SQL statement, for example, is a command to the Microsoft SQL Data Provider. In ADO, commands are typically represented by **Command** objects, although simple commands can also be issued through **Connection** or **Recordset** objects.  
  
 You can use the **Command** object to request any supported type of operation from the provider, assuming that the provider can interpret the command string properly. A common operation for data providers is to query a database and return records in a **Recordset** object, which that can be thought of as a container to hold the result and a tool to view the result. As with many ADO objects, some **Command** object collections, methods, or properties might generate errors when referenced, depending on the functionality of the provider.  
  
 In addition to using **Command** objects, you can use the **Execute** method on the **Connection** object or the **Open** method on the **Recordset** object to issue a command and have it executed. However, you should use a **Command** object if you need to reuse a command in your code, or if you need to pass detailed parameter information with your command. These scenarios are covered in more detail later in this section.  
  
> [!NOTE]
>  Certain **Command**s can return a result set as a binary stream or as a single **Record** rather than as a **Recordset**, if this is supported by the provider. Also, some **Command**s are not intended to return any result set at all (for example, a SQL Update query). This section will cover the most typical scenario, however: executing **Command**s that return results as a **Recordset** object. For more information about returning results into **Record**s or **Stream**s, see [Records and Streams](../../../ado/guide/data/records-and-streams.md).  
  
 This section contains the following topics.  
  
-   [Command Object Overview](../../../ado/guide/data/command-object-overview.md)  
  
-   [Creating and Executing a Simple Command](../../../ado/guide/data/creating-and-executing-a-simple-command.md)  
  
-   [Command Object Parameters](../../../ado/guide/data/command-object-parameters.md)  
  
-   [Calling a Stored Procedure with a Command](../../../ado/guide/data/calling-a-stored-procedure-with-a-command.md)  
  
-   [Calling a Stored Procedure as a Method on a Connection Object](../../../ado/guide/data/calling-a-stored-procedure-as-a-method-on-a-connection-object.md)  
  
-   [Named Commands](../../../ado/guide/data/named-commands.md)  
  
-   [Passing Parameters to a Named Command](../../../ado/guide/data/passing-parameters-to-a-named-command.md)
