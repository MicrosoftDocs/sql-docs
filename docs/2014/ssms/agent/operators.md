---
title: "Operators | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "operators (users) [Database Engine]"
  - "fail-safe operator [SQL Server]"
  - "aliases [SQL Server], operators"
  - "SQL Server Agent alerts, operators"
  - "contact information [SQL Server Agent]"
  - "net send [SQL Server]"
  - "e-mail [SQL Server], SQL Server Agent operators"
  - "pager notifications [SQL Server Agent]"
  - "mail [SQL Server], SQL Server Agent operators"
  - "operators (users) [Database Engine], defining"
  - "jobs [SQL Server Agent], operators"
  - "alerts [SQL Server], operators"
ms.assetid: 38e8488f-2669-4cea-b9c3-5f394a663678
author: stevestein
ms.author: sstein
manager: craigg
---
# Operators
  Operators are aliases for people or groups that can receive electronic notification when jobs have completed or alerts have been raised. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service supports the notification of administrators through operators. Operators enable notification and monitoring capabilities of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  
  
## Operator Attributes and Concepts  
 The primary attributes of an operator are:  
  
-   Operator name  
  
-   Contact information  
  
### Naming an Operator  
 Every operator must have a name. Operator names must be unique within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and can be no longer than **128** characters.  
  
### Contact Information  
 An operator's contact information defines how the operator is notified. Operators can be notified by e-mail, pager, or through the **net send** command:  
  
> [!IMPORTANT]  
>  The Pager and **net send** options will be removed from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent in a future version of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features.  
  
-   **E-mail notification**  
  
     E-mail notification sends an e-mail message to the operator. For e-mail notification, you provide the e-mail address for the operator.  
  
-   **Pager notification**  
  
     Paging is implemented by e-mail. For pager notification, you provide the e-mail address where the operator receives pager messages. To set up pager notification, you must install software on the mail server that processes inbound mail and converts it to a pager message. The software can take one of several approaches, including:  
  
    -   Forwarding the mail to a remote mail server at the site of the pager provider.  
  
         The pager provider must offer this service, although the software required is generally available as part of the local mail system. For more information, see your pager documentation.  
  
    -   Routing the e-mail by way of the Internet to an e-mail server at the pager provider's site.  
  
         This is a variation on the first approach.  
  
    -   Processing the inbound e-mail and dialing the pager by using an attached modem.  
  
         This software is proprietary to pager service providers. The software acts as a e-mail client that periodically processes its inbox either by interpreting all or part of the e-mail address information as a pager number, or by matching the e-mail name to a pager number in a translation table.  
  
         If all of the operators share a pager provider, you can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to specify any special e-mail formatting that is required by the pager-to-e-mail system. The special formatting can be a prefix or a suffix and can be included in the following lines of the e-mail:  
  
         **Subject:**  
  
         **Cc**:  
  
         **To**:  
  
    > [!NOTE]  
    >  If you use a low-capacity alphanumeric paging system, you can shorten the text that is sent by excluding the error text from the pager notification. An example of a low-capacity alphanumeric paging system is one that is limited to 64 characters per page.  
  
-   **net sendnotification**  
  
     This sends a message to the operator by means of the **net send** command. For **net send**, specify the recipient (computer or user) of a network message.  
  
    > [!NOTE]  
    >  The **net send** command uses Microsoft Windows Messenger. To successfully send alerts, this service must be running on both the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running and the computer that the operator uses.  
  
## Alerting and Fail-Safe Operators  
 You can choose which operators are to be notified in response to an alert. For example, you can assign rotating responsibilities for operator notification by scheduling alerts. For example, Individual A is notified of alerts that occur on Monday, Wednesday, or Friday, and Individual B is notified of alerts that occur on Tuesday, Thursday, or Saturday.  
  
 The fail-safe operator receives an alert notification after all pager notifications to the designated operators have failed. For example, if you have defined three operators for pager notifications and none of the designated operators can be paged, the fail-safe operator is notified.  
  
 The fail-safe operator is notified when:  
  
-   The operators responsible for the alert could not be paged.  
  
     Reasons for failure to reach primary operators include incorrect pager addresses and off-duty operators.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent cannot access system tables in the **msdb** database.  
  
     The **sysnotifications** system table specifies operator responsibilities for alerts.  
  
 The fail-safe operator is a security feature. You cannot delete the operator assigned to fail-safe duty without reassigning fail-safe duty to another operator, or deleting the fail-safe assignment altogether.  
  
## Notifying an Operator  
 You must set up one or more of the following in order to notify an operator:  
  
-   To send e-mail with Database Mail functionality, you must have access to an e-mail server that supports SMTP.  
  
-   For paging, you must have third-party pager-to-e-mail software and/or hardware.  
  
-   To use **net send**, the operator must be logged on to the specified computer, and the specified computer must allow messages from Windows Messenger.  
  
## Related Tasks  
  
|||  
|-|-|  
|**Tasks**|**Topic**|  
|Tasks related to creating an operator|[Create an Operator](create-an-operator.md)<br /><br /> [Designate a Fail-Safe Operator](designate-a-fail-safe-operator.md)|  
|Tasks related to assigning alerts|[Assign Alerts to an Operator](assign-alerts-to-an-operator.md)<br /><br /> [Define the Response to an Alert &#40;SQL Server Management Studio&#41;](define-the-response-to-an-alert-sql-server-management-studio.md)<br /><br /> [sp_add_notification &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-add-notification-transact-sql)<br /><br /> [Assign Alerts to an Operator](assign-alerts-to-an-operator.md)|  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)  
  
  
