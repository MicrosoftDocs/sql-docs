---
title: "Control Report Distribution | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], report distribution"
  - "subscriptions [Reporting Services], e-mail"
  - "subscriptions [Reporting Services], file share delivery"
  - "file share delivery [Reporting Services]"
  - "e-mail [Reporting Services]"
  - "subscriptions [Reporting Services], security"
  - "mail [Reporting Services]"
ms.assetid: 8f15e2c6-a647-4b05-a519-1743b5d8654c
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Control Report Distribution
  You can configure a report server to reduce the security risks associated with e-mail and file share distribution.  
  
## Securing Reports  
 The first step in controlling report distribution is to secure the report against unauthorized access. To be used in a subscription, a report must use a stored set of credentials that do not vary for individual deliveries. Any user who can access the report on the report server can run it and possibly distribute it. To prevent this from occurring, you must limit report access to only those users who require it. For more information, see [Secure Reports and Resources](security/secure-reports-and-resources.md) and [Secure Folders](security/secure-folders.md).  
  
 Highly confidential reports that use database security to authorize access cannot be distributed by way of subscription.  
  
> [!IMPORTANT]  
>  Reports are transported as files. The risks and safeguards that apply to files apply equally to reports that are saved to disk or sent as attachments. Any user who has access to a file can distribute or use the file at his or her discretion.  
  
## Controlling E-Mail Delivery  
 You can configure a report server to limit e-mail distribution to specific host domains. For example, you can prevent a report server from delivering a report to all domains except those listed in the RSReportServer configuration file.  
  
 You can also set configuration settings to hide the **To** field in a subscription. In this case, reports are delivered only to the user defining the subscription. However, after a report is sent to a user, you cannot explicitly prevent it from being forwarded.  
  
 The most effective way to control report distribution is to configure a report server to send only a report server URL. The report server uses Windows Authentication and a role-based authorization model to control access to a report. If a user accidentally receives through e-mail a report that he or she is not authorized to view, the report server will not display the report.  
  
## Controlling File Share Delivery  
 File share delivery is used to send a report to a file on a hard disk. After the file has been saved to disk, it is no longer subject to the role-based security model that the report server uses to control user access. To secure a report that has been delivered to disk, you can place Access Control Lists (ACLs) on the file itself or on the folder that contains it. Additional security options might be available, depending on your operating system.  
  
## See Also  
 [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Create and Manage Subscriptions for Native Mode Report Servers](../../2014/reporting-services/create-manage-subscriptions-native-mode-report-servers.md)  
  
  
