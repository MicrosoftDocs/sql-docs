---
title: "ConnectionTimeout Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::ConnectionTimeout"
helpviewer_keywords: 
  - "ConnectionTimeout property [ADO]"
ms.assetid: 8904a403-1383-4b4b-b53d-5c01d6f5deac
author: MightyPen
ms.author: genemi
manager: craigg
---
# ConnectionTimeout Property (ADO)
Indicates how long to wait while establishing a connection before terminating the attempt and generating an error.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates, in seconds, how long to wait for the connection to open. Default is 15.  
  
## Remarks  
 Use the **ConnectionTimeout** property on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object if delays from network traffic or heavy server use make it necessary to abandon a connection attempt. If the time from the **ConnectionTimeout** property setting elapses prior to the opening of the connection, an error occurs and ADO cancels the attempt. If you set the property to zero, ADO will wait indefinitely until the connection is opened. Make sure the provider to which you are writing code supports the **ConnectionTimeout** functionality.  
  
 The **ConnectionTimeout** property is read/write when the connection is closed and read-only when it is open.  
  
## Applies To  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)  
  
## See Also  
 [ConnectionString, ConnectionTimeout, and State Properties Example (VB)](../../../ado/reference/ado-api/connectionstring-connectiontimeout-and-state-properties-example-vb.md)   
 [ConnectionString, ConnectionTimeout, and State Properties Example (VC++)](../../../ado/reference/ado-api/connectionstring-connectiontimeout-and-state-properties-example-vc.md)   
 [CommandTimeout Property (ADO)](../../../ado/reference/ado-api/commandtimeout-property-ado.md)
