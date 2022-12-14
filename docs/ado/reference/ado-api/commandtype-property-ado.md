---
title: "CommandType Property (ADO)"
description: "CommandType Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Command15::CommandType"
helpviewer_keywords:
  - "CommandType property [ADO]"
apitype: "COM"
---
# CommandType Property (ADO)
Indicates the type of a [Command](./command-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns one or more [CommandTypeEnum](./commandtypeenum.md) values.  
  
> [!NOTE]
>  Do not use the **CommandTypeEnum** values of **adCmdFile** or **adCmdTableDirect** with **CommandType**. These values can only be used as options with the [Open](./open-method-ado-recordset.md) and [Requery](./requery-method.md) methods of a [Recordset](./recordset-object-ado.md).  
  
## Remarks  
 Use the **CommandType** property to optimize evaluation of the [CommandText](./commandtext-property-ado.md) property.  
  
 If the **CommandType** property value is set to the default value, **adCmdUnknown**, you may experience diminished performance because ADO must make calls to the provider to determine if the **CommandText** property is an SQL statement, a stored procedure, or a table name. If you know what type of command you are using, setting the **CommandType** property instructs ADO to go directly to the relevant code. If the **CommandType** property does not match the type of command in the **CommandText** property, an error occurs when you call the [Execute](./execute-method-ado-command.md) method.  
  
## Applies To  
 [Command Object (ADO)](./command-object-ado.md)  
  
## See Also  
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VB)](./activeconnection-commandtext-commandtimeout-commandtype-size-example-vb.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VC++)](./activeconnection-commandtext-commandtimeout-commandtype-size-example-vc.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (JScript)](./activeconnection-commandtext-timeout-type-size-example-jscript.md)