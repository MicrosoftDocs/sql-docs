---
title: "Types of Events | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "EventComplete event [ADO]"
  - "events [ADO], types"
  - "Will events [ADO]"
  - "complete events [ADO]"
  - "WillEvent event [ADO]"
ms.assetid: f3327ea0-635a-43d4-bd78-c1674f62f1a2
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Types of Events
There are two basic types of events. "Will Events," which are called before an operation starts, usually include "Will" in their names — for example, **WillChangeRecordset** or **WillConnect**. Events that are called after an event has been completed usually include "Complete" in their names — for example, **RecordChangeComplete** or **ConnectComplete**. Exceptions exist — such as **InfoMessage** — but these occur after the associated operation has completed.  
  
## Will Events  
 Event handlers called before the operation starts offer you the opportunity to examine or modify the operation parameters, and then either cancel the operation or allow it to complete. These event-handler routines usually have names of the form **Will*Event***.  
  
## Complete Events  
 Event handlers called after an operation completes can notify your application that an operation has concluded. Such an event handler is also notified when a Will event handler cancels a pending operation. These event-handler routines usually have names of the form ***Event*Complete**.  
  
 Will and Complete events are typically used in pairs.  
  
## Other Events  
 The other event handlers — that is, events whose names are not of the form **Will*Event*** or ***Event*Complete** — are called only after an operation completes. These events are **Disconnect**, **EndOfRecordset**, and **InfoMessage**.  
  
## See Also  
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)   
 [ADO Event Instantiation by Language](../../../ado/guide/data/ado-event-instantiation-by-language.md)   
 [Event Parameters](../../../ado/guide/data/event-parameters.md)   
 [How Event Handlers Work Together](../../../ado/guide/data/how-event-handlers-work-together.md)