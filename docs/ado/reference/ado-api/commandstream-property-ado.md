---
description: "CommandStream Property (ADO)"
title: "CommandStream Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: ado
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Command::CommandStream"
helpviewer_keywords: 
  - "CommandStream property [ADO]"
ms.assetid: f78f61b6-87e0-48dc-961e-83d0e20da58e
author: rothja
ms.author: jroth
---
# CommandStream Property (ADO)
Indicates the stream used as the input for a [Command](./command-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns the stream used as the input for a **Command** object. The format for this stream is provider-specific; see your provider's documentation for details. This property is similar to the [CommandText](./commandtext-property-ado.md) property, which used to specify a string for the input of a **Command**.  
  
## Remarks  
 **CommandStream** and **CommandText** are mutually exclusive. When the user sets the **CommandStream** property, the **CommandText** property will be set to the empty string (""). If the user sets the **CommandText** property, the **CommandStream** property will be set to **Nothing**.  
  
 The behaviors of the **Command.Parameters.Refresh** and **Command.Prepare** methods are defined by the provider. The values of parameters in a stream can not be refreshed.  
  
 The input stream is not available to other ADO objects that return the source of a **Command**. For example, if the [Source](./source-property-ado-recordset.md) of a [Recordset](./recordset-object-ado.md) is set to a **Command** object that has a stream as its input, **Recordset.Source** continues to return the **CommandText** property, which contains an empty string (""), instead of the stream contents of the **CommandStream** property.  
  
 When using a command stream (as specified by **CommandStream**), the only valid [CommandTypeEnum](./commandtypeenum.md) values for the [CommandType](./commandtype-property-ado.md) property are **adCmdText** and **adCmdUnknown**. Any other value causes an error.  
  
## Applies To  
 [Command Object (ADO)](./command-object-ado.md)  
  
## See Also  
 [CommandText Property (ADO)](./commandtext-property-ado.md)   
 [Dialect Property](./dialect-property.md)   
 [CommandTypeEnum](./commandtypeenum.md)