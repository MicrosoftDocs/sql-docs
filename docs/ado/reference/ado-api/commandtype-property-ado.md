---
title: "CommandType Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Command15::CommandType"
helpviewer_keywords: 
  - "CommandType property [ADO]"
ms.assetid: ca44809c-8647-48b6-a7fb-0be70a02f53e
author: MightyPen
ms.author: genemi
manager: craigg
---
# CommandType Property (ADO)
Indicates the type of a [Command](../../../ado/reference/ado-api/command-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns one or more [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) values.  
  
> [!NOTE]
>  Do not use the **CommandTypeEnum** values of **adCmdFile** or **adCmdTableDirect** with **CommandType**. These values can only be used as options with the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) and [Requery](../../../ado/reference/ado-api/requery-method.md) methods of a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
## Remarks  
 Use the **CommandType** property to optimize evaluation of the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) property.  
  
 If the **CommandType** property value is set to the default value, **adCmdUnknown**, you may experience diminished performance because ADO must make calls to the provider to determine if the **CommandText** property is an SQL statement, a stored procedure, or a table name. If you know what type of command you are using, setting the **CommandType** property instructs ADO to go directly to the relevant code. If the **CommandType** property does not match the type of command in the **CommandText** property, an error occurs when you call the [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method.  
  
## Applies To  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)  
  
## See Also  
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VB)](../../../ado/reference/ado-api/activeconnection-commandtext-commandtimeout-commandtype-size-example-vb.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VC++)](../../../ado/reference/ado-api/activeconnection-commandtext-commandtimeout-commandtype-size-example-vc.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (JScript)](../../../ado/reference/ado-api/activeconnection-commandtext-timeout-type-size-example-jscript.md)
