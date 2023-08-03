---
title: "Size Property (ADO Parameter)"
description: "Size Property (ADO Parameter)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Parameter::Size"
helpviewer_keywords:
  - "Size property [ADO Parameter]"
apitype: "COM"
---
# Size Property (ADO Parameter)
Indicates the maximum size, in bytes or characters, of a [Parameter](./parameter-object.md) object.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates the maximum size in bytes or characters of a value in a **Parameter** object.  
  
## Remarks  
 Use the **Size** property to determine the maximum size for values written to or read from the [Value](./value-property-ado.md) property of a **Parameter** object.  
  
 If you specify a variable-length data type for a **Parameter** object (for example, any **String** type, such as **adVarChar**), you must set the object's **Size** property before appending it to the [Parameters](./parameters-collection-ado.md) collection; otherwise, an error occurs.  
  
 If you have already appended the **Parameter** object to the **Parameters** collection of a [Command](./command-object-ado.md) object and you change its type to a variable-length data type, you must set the **Parameter** object's **Size** property before executing the **Command** object; otherwise, an error occurs.  
  
 If you use the [Refresh](./refresh-method-ado.md) method to obtain parameter information from the provider and it returns one or more variable-length data type **Parameter** objects, ADO may allocate memory for the parameters based on their maximum potential size, which could cause an error during execution. To prevent an error, you should explicitly set the **Size** property for these parameters before executing the command.  
  
 The **Size** property is read/write.  
  
## Applies To  
 [Parameter Object](./parameter-object.md)  
  
## See Also  
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VB)](./activeconnection-commandtext-commandtimeout-commandtype-size-example-vb.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (VC++)](./activeconnection-commandtext-commandtimeout-commandtype-size-example-vc.md)   
 [ActiveConnection, CommandText, CommandTimeout, CommandType, Size, and Direction Properties Example (JScript)](./activeconnection-commandtext-timeout-type-size-example-jscript.md)   
 [Size Property (ADO Stream)](./size-property-ado-stream.md)