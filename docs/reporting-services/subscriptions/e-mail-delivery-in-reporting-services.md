---
title: "Email delivery in Reporting Services"
description: In this article, learn to use the email delivery extension, which provides a way to email a report to individual users or groups.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/15/2023
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], email"
  - "email [Reporting Services]"
  - "mail [Reporting Services]"
---
# Email delivery in Reporting Services

**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode

  SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes an email delivery extension that provides a way to email a report to individual users or groups. To distribute a report by email, first, you configure the report server for email delivery. Then, define either a standard subscription or a data-driven subscription. A single subscription can't deliver multiple reports in a single email message. However you can create multiple subscriptions.  
  
 The report server connects with an email server through a standard connection. It doesn't use communication that is encrypted by using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). The email server must be a remote or local Simple Mail Transport Protocol (SMTP) server on the same network as the report server.  
  
 For detailed steps that walk you through creating a subscription, see the following articles:  
  
-   [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)  
  
-   [Create and manage subscriptions for SharePoint mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-sharepoint-mode-report-servers.md)  
  
## Email delivery options  
 Report server email delivery can deliver reports in the following way  
  
-   Send a notification and a hyperlink to the generated report.  
  
-   Send a notification in the Subject: line of an email message. By default, the Subject: line in the subscription definition includes the following variables. Report-specific information replaces the variables when the subscription is processed:  
  
     **\@ReportName** specifies the name of the report.  
  
     **\@ExecutionTime** specifies when the report was executed.  
  
     You can combine these variables with static text or modify the text in the Subject: line for each subscription.  
  
-   Send an embedded or attached report. The rendering format and browser determine whether the report is embedded or attached.  
  
     If your browser supports HTML 4.0 and MHTML, and you choose the Web archive rendering format, the report is embedded as part of the message. All other rendering formats (CSV, PDF, and so on) deliver reports as attachments. For native mode report servers, you can disable this functionality in the RSReportServer.config configuration file.  
  
     [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't check the size of the attachment or message before sending the report. If the attachment or message exceeds the maximum limit allowed by your mail server, the report isn't delivered. Choose one of the other delivery options (such as URL or notification) if for large reports.  
  
 You set delivery options that determine how a report is delivered when you create the subscription. For example, if you select **Include Link** in the subscription, the email message includes a hyperlink to the report.  
  
## Native mode role-based email settings  
 In a Native mode report server environment, the email delivery settings you work with vary. The settings vary depending on whether your role includes the "Manage individual subscriptions" task or the "Manage all subscriptions" task.  
  
|Task|Available settings|  
|----------|------------------------|  
|Manage individual subscriptions|Shows fields that enable users to automate and deliver a report to themselves. In this mode, fields that accept email aliases aren't available.|  
|Manage all subscriptions|Shows fields that support wider distribution, including **To:**, **Cc:**, **Bcc:**, and **Reply-To:** fields, providing more ways to route a report to more subscribers. The availability of email alias fields is defined through the RSReportServer configuration file settings.| 

::: moniker range="<=sql-server-2017"

> [!NOTE]  
> In versions prior to Reporting Services 2019, the Comment field is only available for roles that include the ‘Manage all subscriptions’ task.

::: moniker-end

## Specify email addresses in a subscription  
 If you're distributing reports within an intranet and you use an SMTP gateway to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Exchange server, enter the email alias. For example, enter the alias as if you were sending email to a coworker. If delivery is to an external email account, enter the full email address. If you specify more email addresses to add others to your subscription, subscribers get an exact copy of the report that is produced from this subscription.  
  
 The report server doesn't validate email addresses or obtain email addresses from an email server. You must know in advance which email addresses you want to use. By default, you can email reports to any valid email account within or outside of your organization. Configuration settings can be used, however, to restrict email delivery to mail server hosts that you identify by name. You can specify other hosts if you want to support email delivery to people that aren't members of your organization.  
  
 The email message used to deliver the report must be sent from an email account that is defined on the email server. A configuration setting specifies the email account. The email account is used for all reports delivered by the email delivery extension; you can't specify multiple accounts or vary the account for individual reports.  
  
## Control email delivery  
 You can configure a report server to limit email distribution to specific host domains. For example, you can prevent a Native report server from delivering a report to all domains except those domains listed in the **RSReportServer.config** configuration file.  
  
 You can also set configuration settings to hide the **To** field in a subscription. In this case, reports are delivered only to the user defining the subscription. However, after a report is sent to a user, you can't explicitly prevent it from being forwarded.  

 The most effective way to control report distribution is to configure a report server to send only a report server URL. The report server uses Windows Authentication and a role-based authorization model to control access to a report. If users accidentally receive through email a report that they're not authorized to view, the report server doesn't display the report. For more information about subscriptions, see the following section.  
  
## Email server configuration  
 For a Native mode report server, the email delivery extension is configured through the Native mode [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and by editing the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration files. For a SharePoint mode report server, the email delivery extension is configured in SharePoint management pages and PowerShell scripts.  
  
For information on how to configure a native mode report server, see [Email settings - Reporting Services native mode (Configuration Manager)](../install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md).
 
For information on how to configure a SharePoint mode report server, see [Create and manage subscriptions for SharePoint mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-sharepoint-mode-report-servers.md). 
  
## Related content  
 [Tasks and permissions](../../reporting-services/security/tasks-and-permissions.md)   
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Data-driven subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md)   
 [Role assignments](../../reporting-services/security/role-assignments.md)  
  
  
