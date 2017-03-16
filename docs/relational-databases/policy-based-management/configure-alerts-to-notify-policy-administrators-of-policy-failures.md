---
title: "Configure Alerts to Notify Policy Administrators of Policy Failures | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Policy-Based Management, configure alerts"
ms.assetid: e8e60159-d5b0-49d5-91f3-af8e9cb994c1
caps.latest.revision: 6
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Configure Alerts to Notify Policy Administrators of Policy Failures
  When Policy-Based Management policies are executed in one of the three automated evaluation modes, if a policy violation occurs, a message is written to the event log. To be notified when this message is written to the event log, you can create an alert to detect the message and perform an action. The alert should detect the messages as shown in the following table.  
  
|Execution mode|Message number|  
|--------------------|--------------------|  
|On change: prevent<br /><br /> (if automatic)|34050|  
|On change: prevent<br /><br /> (if On demand)|34051|  
|On schedule|34052|  
|On change|34053|  
  
 To set up an alert to respond to the Policy-Based Management error messages, see the following topics:  
  
-   [Create an Operator](http://msdn.microsoft.com/library/1359d790-5905-4927-a208-e7155e7768a2)  
  
-   [Create an Alert Using an Error Number](http://msdn.microsoft.com/library/03dd7fac-5073-4f86-babd-37e45a86023c)  
  
-   [Assign Alerts to an Operator](http://msdn.microsoft.com/library/aa818155-6fa2-4565-a09f-5c7e31c89754)  
  
## Permissions  
 When policies are evaluated on demand, they execute in the security context of the user. To write to the error log, the user must have ALTER TRACE permissions or be a member of the sysadmin fixed server role. Policies that are evaluated by a user that has less privileges will not write to the event log, and will not fire an alert.  
  
 The automated execution modes execute as a member of the sysadmin role. This allows the policy to write to the error log and raise an alert.  
  
## Additional Considerations About Alerts  
 Be aware of the following additional considerations about alerts:  
  
-   Alerts are raised only for policies that are enabled. Because **On demand** policies cannot be enabled, alerts are not raised for policies that are executed on demand.  
  
-   If the action you want to take includes sending an e-mail message, you must configure a mail account. We recommend that you use Database Mail. For more information about how to set up Database Mail, see [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md).  
  
  