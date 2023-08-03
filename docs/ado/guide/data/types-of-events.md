---
title: "Types of Events"
description: "Types of Events"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "EventComplete event [ADO]"
  - "events [ADO], types"
  - "Will events [ADO]"
  - "complete events [ADO]"
  - "WillEvent event [ADO]"
---
# Types of Events
There are two basic types of events. "Will Events," which are called before an operation starts, usually include "Will" in their names - for example, **WillChangeRecordset** or **WillConnect**. Events that are called after an event has been completed usually include "Complete" in their names - for example, **RecordChangeComplete** or **ConnectComplete**. Exceptions exist - such as **InfoMessage** - but these occur after the associated operation has completed.  
  
## Will Events  
 Event handlers called before the operation starts offer you the opportunity to examine or modify the operation parameters, and then either cancel the operation or allow it to complete. These event-handler routines usually have names of the form <strong>Will*Event*</strong>.  
  
## Complete Events  
 Event handlers called after an operation completes can notify your application that an operation has concluded. Such an event handler is also notified when a Will event handler cancels a pending operation. These event-handler routines usually have names of the form <strong>*Event*Complete</strong>.  
  
 Will and Complete events are typically used in pairs.  
  
## Other Events  
 The other event handlers - that is, events whose names are not of the form <strong>Will*Event*</strong> or <strong>*Event*Complete</strong> - are called only after an operation completes. These events are **Disconnect**, **EndOfRecordset**, and **InfoMessage**.  
  
## See Also  
 [ADO Event Handler Summary](../../../ado/guide/data/ado-event-handler-summary.md)   
 [ADO Event Instantiation by Language](../../../ado/guide/data/ado-event-instantiation-by-language.md)   
 [Event Parameters](../../../ado/guide/data/event-parameters.md)   
 [How Event Handlers Work Together](../../../ado/guide/data/how-event-handlers-work-together.md)
