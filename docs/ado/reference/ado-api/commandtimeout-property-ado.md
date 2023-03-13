---
title: "CommandTimeout Property (ADO)"
description: "CommandTimeout Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Command15::CommandTimeout"
helpviewer_keywords:
  - "CommandTimeout property [ADO]"
apitype: "COM"
---
# CommandTimeout Property (ADO)
Indicates how long to wait while executing a command before terminating the attempt and generating an error.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates, in seconds, how long to wait for a command to execute. Default is 30.  
  
## Remarks  
 Use the **CommandTimeout** property on a [Connection](./connection-object-ado.md) object or [Command](./command-object-ado.md) object to allow the cancellation of an [Execute](./execute-method-ado-command.md) method call, due to delays from network traffic or heavy server use. If the interval set in the **CommandTimeout** property elapses before the command completes execution, an error occurs and ADO cancels the command. If you set the property to zero, ADO will wait indefinitely until the execution is complete. Make sure the provider and data source to which you are writing code support the **CommandTimeout** functionality.  
  
 The **CommandTimeout** setting on a **Connection** object has no effect on the **CommandTimeout** setting on a **Command** object on the same **Connection**; that is, the **Command** object's **CommandTimeout** property does not inherit the value of the **Connection** object's **CommandTimeout** value.  
  
 On a **Connection** object, the **CommandTimeout** property remains read/write after the **Connection** is opened.  
  
## Applies To  

:::row:::
    :::column:::
        [Command Object (ADO)](./command-object-ado.md)  
    :::column-end:::
    :::column:::
        [Connection Object (ADO)](./connection-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VB)](./activeconnection-commandtext-commandtimeout-commandtype-size-example-vb.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VC++)](./activeconnection-commandtext-commandtimeout-commandtype-size-example-vc.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (JScript)](./activeconnection-commandtext-timeout-type-size-example-jscript.md)   
 [ConnectionTimeout Property (ADO)](./connectiontimeout-property-ado.md)