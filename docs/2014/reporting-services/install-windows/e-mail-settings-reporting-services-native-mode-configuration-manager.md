---
title: "E-mail Settings - Configuration Manager (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rsconfigtool.emailsettings.f1"
helpviewer_keywords: 
  - "SQL11.rsconfigtool.emailsettings.F1"
ms.assetid: cdad1529-bfa6-41fb-9863-d9ff1b802577
author: markingmyname
ms.author: maghan
manager: kfile
---
# E-mail Settings - Configuration Manager (SSRS Native Mode)
  Use this page to specify Simple Mail Transport Protocol (SMTP) settings that enable report server e-mail delivery from the report server. You can use the Report Server E-Mail delivery extension to distribute reports or report processing notifications through e-mail subscriptions. The Report Server E-Mail delivery extension requires an SMTP server and an e-mail address to use in the From: field.  
  
 Additional configuration settings can be used to further customize e-mail subscriptions, including settings that restrict mail server hosts and rendering extension availability. You cannot specify these settings in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. To configure the additional settings, you must manually edit the RSReportServer.config file. For more information, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md) and [Reporting Services Configuration Files](../report-server/reporting-services-configuration-files.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 To open this page, start the Reporting Services Configuration Manager and click **E-mail Settings** in the navigation pane. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
## Options  
 **Sender Address**  
 Specifies the e-mail address to use in the From: field of a generated e-mail. You must specify a user account that has permission to send mail from the SMTP server.  
  
 **Current SMTP Delivery Method**  
 Specifies that report server e-mail is routed through an SMTP server. This is the only delivery method that you can specify in the Reporting Services Configuration Manager.  
  
 An alternative delivery method is to use a local SMTP service pickup directory. You can use this delivery method if a network SMTP service is not available. To specify a local SMTP service pickup directory, you must edit the RSReportServer.config file. If you edit the configuration file to use a local SMTP service pickup directory, the Reporting Services Configuration Manager sets the **Delivery method** option to *custom* to indicate that the delivery method is specified in the configuration file.  
  
 In the configuration file, the delivery method is set through the `SendUsing` configuration setting. For more information about specifying the `SendUsing` setting, see [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md).  
  
 **SMTP Server**  
 Specify the SMTP server or gateway to use. You can use a local server or an SMTP server on your network.  
  
## See Also  
 [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)   
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)  
  
  
