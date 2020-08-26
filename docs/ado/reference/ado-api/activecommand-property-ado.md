---
description: "ActiveCommand Property (ADO)"
title: "ActiveCommand Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset20::ActiveCommand"
helpviewer_keywords: 
  - "ActiveCommand property [ADO]"
ms.assetid: fb4088d5-5968-42d6-aeaa-3955046bb4da
author: rothja
ms.author: jroth
---
# ActiveCommand Property (ADO)
Indicates the [Command](./command-object-ado.md) object that created the associated [Recordset](./recordset-object-ado.md) object.  
  
## Return Value  
 Returns a **Variant** that contains a **Command** object. Default is a null object reference.  
  
## Remarks  
 The **ActiveCommand** property is read-only.  
  
 If a **Command** object was not used to create the current **Recordset**, then a **Null** object reference is returned.  
  
 Use this property to find the associated **Command** object when you are given only the resulting **Recordset** object.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [ActiveCommand Property Example (VB)](./activecommand-property-example-vb.md)   
 [ActiveCommand Property Example (JScript)](./activecommand-property-example-jscript.md)   
 [ActiveCommand Property Example (VC++)](./activecommand-property-example-vc.md)   
 [Command Object (ADO)](./command-object-ado.md)