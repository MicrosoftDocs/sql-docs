---
title: "Create, Modify, and Delete Standard Subscriptions (Reporting Services in Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "standard subscriptions [Reporting Services]"
  - "subscriptions [Reporting Services], standard"
ms.assetid: 5ab1c661-9bfa-434a-b315-faac34ed12b1
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Create, Modify, and Delete Standard Subscriptions (Reporting Services in Native Mode)
  A standard subscription is one that is created by individual users who want to have a report delivered through e-mail or to a shared folder. A standard subscription is always defined through the report on which it is based.  
  
 A user who creates a subscription owns that subscription. Each user can modify or delete the subscriptions that he or she owns.  
  
> [!NOTE]  
>  Starting with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)][!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] you can transfer the ownership of a subscription programmatically. There is no user interface you can use to transfer ownership of subscriptions. For more information, see <xref:ReportService2010.ReportingService2010.ChangeSubscriptionOwner%2A>  
  
 Depending on **RSReportServer.config** configuration file settings, users may be able to add more users to a subscription (for example, a manager adds the e-mail addresses of his or her direct reports so that they each receive a copy of the report). Whether this is supported depends on whether the To: field is visible when defining individual subscriptions. For more information, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).  
  
 This topic provides information about standard subscriptions that are created and managed by individual users. Data-driven subscriptions have different requirements and steps, and are discussed in a separate topic. For more information, see [Create, Modify, and Delete a Data-Driven Subscription](data-driven-subscriptions.md).  
  
 In this topic:  
  
-   [To Create a Subscription](#bkmk_create_subscription)  
  
-   [To Create a File Share Subscription](#bkmk_create_fileshare_subscription)  
  
-   [To Create an E-mail Subscription](#bkmk_create_email_subscription)  
  
-   [To Modify a Subscription](#bkmk_modify_subscription)  
  
-   [To Delete a Subscription](#bkmk_delete_subscription)  
  
##  <a name="bkmk_create_subscription"></a> To Create a Subscription  
 To create a subscription, choose the tool and approach that is valid for your report server deployment:  
  
-   The content in this topic explains how to create subscriptions on a native mode report server using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report Manager. After you define a subscription, you can access it in Report Manager through the My Subscriptions page or the **Subscriptions** tab of a specific report.  
  
-   [Create and Manage Subscriptions for SharePoint Mode Report Servers](create-and-manage-subscriptions-for-sharepoint-mode-report-servers.md) explains how to use the application pages in a SharePoint site to subscribe to reports on a report server that runs in SharePoint integrated mode.  
  
 This topic provides instructions for creating an e-mail subscription and a file share delivery subscription.  
  
-   To use e-mail delivery, the report server must be configured for an SMTP server or gateway connection before you create the subscription.  
  
-   To use file share delivery, you must have target folder already defined. For more information, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).  
  
 Before you can subscribe to a report, the report data source must be configured to use stored credentials or no credentials. For more information, see [Store Credentials in a Reporting Services Data Source](../report-data/store-credentials-in-a-reporting-services-data-source.md). If it does not, the **New Subscription** button is not available.  
  
 This topic does not explain how to create a data-driven subscription. For instructions on how to create a data-driven subscription, see [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](../create-a-data-driven-subscription-ssrs-tutorial.md) or the online help for the Create a Data-driven Subscription page in Report Manager.  
  
###  <a name="bkmk_create_fileshare_subscription"></a> To Create a File Share Subscription  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, on the **Contents** page, navigate to the report you want to subscribe to. Click the report to open it.  
  
3.  Click the **Subscriptions** tab, and then click **New Subscription**.  
  
4.  In **Delivered by**, select **Windows File Share**.  
  
5.  In **File Name**, type a file name for the report.  
  
6.  Select **Add a file extension when the file is created**. This option adds a three-character file extension to the file name. The file extension is determined by the report output format you select.  
  
7.  In the **Path** text box, type a Universal Naming Convention (UNC) path to an existing folder where you want to deliver the reports (for example, \\\\<servername\>\\<myreports\>). Include double backslash characters at the beginning of the path. Do not specify a trailing backslash.  
  
8.  In Render Format, select a report output format for file delivery. Choose a format that corresponds to the desktop application that will be used to open the report. Avoid formats that do not render a report in a single stream or that introduce interactivity that cannot be supported in a static file (for example, HTML 4.0).  
  
9. In the **User name** and **Password** text boxes, specify the credentials required to access the file share, using the format *\<domain>*\\*\<user name>* for the user name.  
  
10. Specify overwrite options. If you click **Do not overwrite the file if a previous version exists**, the delivery will not occur if an existing file is detected. If you click **Increment file names as newer versions are added**, the report server appends a number to the file name to distinguish it from existing files of the same name.  
  
11. Specify when you want the report delivered:  
  
    -   To schedule a delivery time, click **When the scheduled report run is complete** and click the **Select Schedule** button. A schedule page opens.  
  
    -   To select a predefined shared schedule that already has the date, time, and recurrence information that you want to use, click **On a shared schedule**, and then select the schedule to use.  
  
    -   To deliver the report when a report snapshot is updated with a newer version,click**When the report content is refreshed**. If you are subscribing to a report that retrieves data at scheduled intervals, the schedule used to refresh the data determines when your subscription is processed.  
  
        > [!NOTE]  
        >  This option is available only for snapshots that are already associated with an update schedule.  
  
12. For parameterized reports, specify parameters to use for the report for this subscription. The parameters can be different from those used to run the report on demand or in other scheduled operations.  
  
 The report is delivered as a static file. If the report includes interactive features (for example, links to additional rows and columns), those features are not available.  
  
###  <a name="bkmk_create_email_subscription"></a> To Create an E-mail Subscription  
  
1.  In Report Manager, on the **Contents** page, navigate to the report you want to subscribe to. Click the report to open it.  
  
2.  Click the **Subscriptions** tab, and then click **New Subscription**.  
  
3.  In **Delivered by**, select **E-Mail**. If this delivery type is not available, your report server has not been configured for e-mail subscriptions.  
  
4.  In the **To** text box, the recipient name in the To: field is self-addressed using your domain user account. Report server configuration settings determine whether the **To** field is self-addressed with your user account. For more information about changing the configuration settings e-mail addresses, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).  
  
    > [!NOTE]  
    >  Depending on your permissions, you might be able to type the e-mail address you want the report delivered to. To specify multiple e-mail addresses, separate them with a semicolon (;). You can also type additional e-mail addresses in the **Cc**, **Bcc**, and **Reply-To** text boxes. This requires that you have permission to manage all subscriptions.  
  
5.  Select the delivery options as follows:  
  
    -   To embed or attach a copy of the report, select **Include Report**. The format of the report is determined by the rendering format you select. Do not choose this option if you think the report size will exceed the limit defined for your e-mail system.  
  
    -   To include a URL link to the report in the body of the e-mail message, select **Include Link**.  
  
    > [!NOTE]  
    >  If you clear both of these options, only the notification text in the Subject line is sent.  
  
6.  Choose a rendering format from the **Render Format** list box. This option is available if you select **Include Report** to embed or attach a copy of the report.  
  
    -   To embed the report in the body of the e-mail message, select **Web archive**.  
  
    -   To send the report as an attachment, choose any of the other rendering formats.  
  
7.  Select a priority from the **Priority** list box. In [!INCLUDE[msCoName](../../includes/msconame-md.md)] Exchange, this setting sets a flag for the importance level of the e-mail message.  
  
8.  Specify when you want the report delivered:  
  
    -   To schedule a delivery time, click **When the scheduled report run is complete** and click **Select Schedule**. A schedule page opens for you.  
  
    -   To select a predefined shared schedule that already has the date, time, and recurrence information that you want to use, click **On a shared schedule**, and then select the schedule to use.  
  
    -   To deliver the report when a report snapshot is updated with a newer version,click**When the report content is refreshed**. If you are subscribing to a report that retrieves data at scheduled intervals, the schedule used to refresh the data determines when your subscription is processed.  
  
    > [!NOTE]  
    >  This option is available only for snapshots that are already associated with an update schedule.  
  
9. For parameterized reports, specify parameters to use for the report for this subscription. The parameters that you specify can be different from those used to run the report on demand or in other scheduled operations.  
  
##  <a name="bkmk_modify_subscription"></a> To Modify a Subscription  
 You can modify a subscription at any time. If you modify a subscription while it is being processed, the updated settings are used if they are saved to the report server database before the delivery extension receives the subscription data. Otherwise, the existing settings are used.  
  
 To locate a subscription, use the **My Subscriptions** page or view the subscription definitions that are associated with a report. You cannot search for subscriptions directly, nor can you search for a subscription based on owner name, trigger information, status information, and so forth.  
  
 Subscriptions can also be modified or deleted by report server administrators.  
  
> [!NOTE]  
>  A report server administrator cannot manage from one place all the individual subscriptions that are in use on a given report server. However, report server administrators can access each individual subscription to modify or delete it.  
  
##  <a name="bkmk_delete_subscription"></a> To Delete a Subscription  
 To delete a subscription"  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, click **My Subscriptions** on the global toolbar and navigate to the subscription you want to modify or delete.  
  
3.  Alternatively, on the **Subscriptions** tab of an open report, find the subscription that you want to modify or delete. Perform one of the following:  
  
    1.  To modify a subscription, click **Edit**.  
  
    2.  To delete a subscription, select the check box next to the subscription, and then click **Delete**.  
  
 This topic does not explain how to cancel a subscription that is currently processing on the report server. For more information about canceling subscriptions, see [Manage a Running Process](manage-a-running-process.md)  
  
 If you want to end a subscription and you cannot locate the subscription easily, make a note of the report you are receiving and search for it by name. Once you access the report, you can remove yourself from the subscription. If you cannot locate the subscription, the subscription may be a data-driven subscription. For more information, see your report server administrator.  
  
 A subscription is deleted automatically if the underlying report is deleted. If you delete a subscription while it is being processed, the subscription stops if the delete operation occurs before the delivery extension receives subscription data. Otherwise, the subscription continues to be processed.  
  
## See Also  
 [Tasks and Permissions](../security/tasks-and-permissions.md)   
 [Create and Manage Subscriptions for SharePoint Mode Report Servers](create-and-manage-subscriptions-for-sharepoint-mode-report-servers.md)   
 [Create and Manage Subscriptions for Native Mode Report Servers](../create-manage-subscriptions-native-mode-report-servers.md)   
 [Data-Driven Subscriptions](data-driven-subscriptions.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions-and-delivery-reporting-services.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)   
 [Use My Subscriptions](use-my-subscriptions-native-mode-report-server.md)  
  
  
