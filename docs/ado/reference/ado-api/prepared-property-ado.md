---
title: "Prepared Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Command15::Prepared"
helpviewer_keywords: 
  - "Prepared property [ADO]"
ms.assetid: 11ca8825-765e-4bb4-a6ce-3f6564ad8755
author: MightyPen
ms.author: genemi
manager: craigg
---
# Prepared Property (ADO)
Indicates whether to save a compiled version of a [Command](../../../ado/reference/ado-api/command-object-ado.md) before execution.  
  
## Settings and Return Values  
 Sets or returns a **Boolean** value that, if set to **True**, indicates that the command should be prepared.  
  
## Remarks  
 Use the **Prepared** property to have the provider save a prepared (or compiled) version of the query specified in the [CommandText](../../../ado/reference/ado-api/commandtext-property-ado.md) property before a [Command](../../../ado/reference/ado-api/command-object-ado.md) object's first execution. This may slow a command's first execution, but once the provider compiles a command, the provider will use the compiled version of the command for any subsequent executions, which will result in improved performance.  
  
 If the property is **False**, the provider will execute the **Command** object directly without creating a compiled version.  
  
 If the provider does not support command preparation, it may return an error when this property is set to **True**. If the provider does not return an error, it simply ignores the request to prepare the command and sets the **Prepared** property to **False**.  
  
## Applies To  
 [Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)  
  
## See Also  
 [Prepared Property Example (VB)](../../../ado/reference/ado-api/prepared-property-example-vb.md)   
 [Prepared Property Example (VC++)](../../../ado/reference/ado-api/prepared-property-example-vc.md)   
