---
title: "Alert Properties: New Alert (Options Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql12.ag.alert.options.f1"
ms.assetid: 6e4f41aa-832d-46ba-b6b5-cf1d3b15d33f
author: stevestein
ms.author: sstein
manager: craigg
---
# Alert Properties: New Alert (Options Page)
  Use this page to view and modify options for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.  
  
## Options  
 **E-mail**  
 Include error text from the event, if any, in e-mail notifications.  
  
 **Pager**  
 Include error text from the event, if any, in pager notifications.  
  
 **Net send**  
 Include error text from the event, if any, in net send notifications.  
  
 **Additional notification message to send**  
 Type any additional text to include in notification messages.  
  
 **Delay between responses**  
 Specify a delay for repeated occurrences of the event. Some events may occur frequently during a short period of time. In this case, you may want to know that the event has occurred, but you may not want a response for every event. Use this option to specify a time-out. With a delay, after the alert responds to an event, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent waits for the delay specified before responding again, regardless of whether the event occurs during the delay.  
  
 **Minutes**  
 Specify a delay in minutes. To respond every time the event occurs, specify 0 minutes and 0 seconds.  
  
 **Seconds**  
 Specify a delay in seconds. To respond every time the event occurs, specify 0 minutes and 0 seconds.  
  
## See Also  
 [Alerts](alerts.md)  
  
  
