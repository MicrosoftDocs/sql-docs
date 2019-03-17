---
title: "State Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Command25::State"
helpviewer_keywords: 
  - "State property [ADO]"
ms.assetid: 0b993bac-2653-40b1-bcbb-5b57b6aae2bf
author: MightyPen
ms.author: genemi
manager: craigg
---
# State Property (ADO)
Indicates for all applicable objects whether the state of the object is open or closed. If the object is executing an asynchronous method, indicates whether the current state of the object is connecting, executing, or retrieving.  
  
## Return Value  
 Returns a **Long** value that can be an [ObjectStateEnum](../../../ado/reference/ado-api/objectstateenum.md) value. The default value is **adStateClosed**.  
  
## Remarks  
 You can use the **State** property to determine the current state of a given object at any time.  
  
 The object's **State** property can have a combination of values. For example, if a statement is executing, this property will have a combined value of **adStateOpen** and **adStateExecuting**.  
  
 The **State** property is read-only.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|[Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)|  
|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|[Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)||  
  
## See Also  
 [ConnectionString, ConnectionTimeout, and State Properties Example (VB)](../../../ado/reference/ado-api/connectionstring-connectiontimeout-and-state-properties-example-vb.md)   
 [ConnectionString, ConnectionTimeout, and State Properties Example (VC++)](../../../ado/reference/ado-api/connectionstring-connectiontimeout-and-state-properties-example-vc.md)   
