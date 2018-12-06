---
title: "Configure Alerts to Notify Policy Administrators of Policy Failures | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, configure alerts"
ms.assetid: e8e60159-d5b0-49d5-91f3-af8e9cb994c1
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Configure Alerts to Notify Policy Administrators of Policy Failures
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When Policy-Based Management policies are executed in one of the three automated evaluation modes, if a policy violation occurs, a message is written to the event log. To be notified when this message is written to the event log, you can create an alert to detect the message and perform an action. The alert should detect the messages as shown in the following table.  
  
|Execution mode|Message number|  
|--------------------|--------------------|  
|On change: prevent<br /><br /> (if automatic)|34050|  
|On change: prevent<br /><br /> (if On demand)|34051|  
|On schedule|34052|  
|On change|34053|  
  
 To set up an alert to respond to the Policy-Based Management error messages, see the following topics:  
  
-   [Create an Operator](../../ssms/agent/create-an-operator.md)  
  
-   [Create an Alert Using an Error Number](../../ssms/agent/create-an-alert-using-an-error-number.md)  
  
-   [Assign Alerts to an Operator](../../ssms/agent/assign-alerts-to-an-operator.md)  
  
## Permissions  
 When policies are evaluated on demand, they execute in the security context of the user. To write to the error log, the user must have ALTER TRACE permissions or be a member of the sysadmin fixed server role. Policies that are evaluated by a user that has less privileges will not write to the event log, and will not fire an alert.  
  
 The automated execution modes execute as a member of the sysadmin role. This allows the policy to write to the error log and raise an alert.  
  
## Additional Considerations About Alerts  
 Be aware of the following additional considerations about alerts:  
  
-   Alerts are raised only for policies that are enabled. Because **On demand** policies cannot be enabled, alerts are not raised for policies that are executed on demand.  
  
-   If the action you want to take includes sending an e-mail message, you must configure a mail account. We recommend that you use Database Mail. For more information about how to set up Database Mail, see [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md).  
  
  
