---
title: "Prepared Property (ADO)"
description: "Prepared Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Command15::Prepared"
helpviewer_keywords:
  - "Prepared property [ADO]"
apitype: "COM"
---
# Prepared Property (ADO)
Indicates whether to save a compiled version of a [Command](./command-object-ado.md) before execution.  
  
## Settings and Return Values  
 Sets or returns a **Boolean** value that, if set to **True**, indicates that the command should be prepared.  
  
## Remarks  
 Use the **Prepared** property to have the provider save a prepared (or compiled) version of the query specified in the [CommandText](./commandtext-property-ado.md) property before a [Command](./command-object-ado.md) object's first execution. This may slow a command's first execution, but once the provider compiles a command, the provider will use the compiled version of the command for any subsequent executions, which will result in improved performance.  
  
 If the property is **False**, the provider will execute the **Command** object directly without creating a compiled version.  
  
 If the provider does not support command preparation, it may return an error when this property is set to **True**. If the provider does not return an error, it simply ignores the request to prepare the command and sets the **Prepared** property to **False**.  
  
## Applies To  
 [Command Object (ADO)](./command-object-ado.md)  
  
## See Also  
 [Prepared Property Example (VB)](./prepared-property-example-vb.md)   
 [Prepared Property Example (VC++)](./prepared-property-example-vc.md)