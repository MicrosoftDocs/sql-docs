---
title: "CommandTimeout Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Command15::CommandTimeout"
helpviewer_keywords: 
  - "CommandTimeout property [ADO]"
ms.assetid: c611f857-d6b0-4dca-8925-f4a02e769eb0
author: MightyPen
ms.author: genemi
manager: craigg
---
# CommandTimeout Property (ADO)
Indicates how long to wait while executing a command before terminating the attempt and generating an error.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates, in seconds, how long to wait for a command to execute. Default is 30.  
  
## Remarks  
 Use the **CommandTimeout** property on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object or [Command](../../../ado/reference/ado-api/command-object-ado.md) object to allow the cancellation of an [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method call, due to delays from network traffic or heavy server use. If the interval set in the **CommandTimeout** property elapses before the command completes execution, an error occurs and ADO cancels the command. If you set the property to zero, ADO will wait indefinitely until the execution is complete. Make sure the provider and data source to which you are writing code support the **CommandTimeout** functionality.  
  
 The **CommandTimeout** setting on a **Connection** object has no effect on the **CommandTimeout** setting on a **Command** object on the same **Connection**; that is, the **Command** object's **CommandTimeout** property does not inherit the value of the **Connection** object's **CommandTimeout** value.  
  
 On a **Connection** object, the **CommandTimeout** property remains read/write after the **Connection** is opened.  
  
## Applies To  
  
|||  
|-|-|  
|[Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|  
  
## See Also  
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VB)](../../../ado/reference/ado-api/activeconnection-commandtext-commandtimeout-commandtype-size-example-vb.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VC++)](../../../ado/reference/ado-api/activeconnection-commandtext-commandtimeout-commandtype-size-example-vc.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (JScript)](../../../ado/reference/ado-api/activeconnection-commandtext-timeout-type-size-example-jscript.md)   
 [ConnectionTimeout Property (ADO)](../../../ado/reference/ado-api/connectiontimeout-property-ado.md)
