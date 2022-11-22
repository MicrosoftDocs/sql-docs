---
title: "Reporting Services Delivery Extension Settings | Microsoft Docs"
description: Reporting Services offers e-mail delivery to send reports to users and file share delivery to send reports to a share. Learn about delivery extension settings.
ms.date: 03/03/2017
ms.prod: reporting-services
ms.technology: report-server-web-service


ms.topic: reference
helpviewer_keywords: 
  - "XML Web service [Reporting Services], delivery extension settings"
  - "Report Server Web service, delivery extension settings"
  - "delivery [Reporting Services]"
  - "network share delivery [Reporting Services]"
  - "file share delivery [Reporting Services]"
  - "e-mail [Reporting Services]"
  - "mailing reports [Reporting Services]"
  - "sharing reports"
  - "extensions [Reporting Services], delivery"
  - "mail [Reporting Services]"
  - "Web service [Reporting Services], delivery extension settings"
ms.assetid: 68c31a85-261c-4ec4-b8df-1f9842b46f8a
author: maggiesMSFT
ms.author: maggies
---
# Reporting Services Delivery Extension Settings
  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] includes an e-mail delivery extension and a file share delivery extension. E-mail delivery provides a way to send a report to individual users or groups through e-mail. File share delivery enables you to automatically send rendered reports to a share on your network. You can use either one of the supported delivery extensions with standard subscriptions or data-driven subscriptions. You pass delivery settings that are specific to the type of delivery extension whenever you call the <xref:ReportService2010.ReportingService2010.CreateSubscription%2A>,<xref:ReportService2010.ReportingService2010.CreateDataDrivenSubscription%2A>,<xref:ReportService2010.ReportingService2010.SetSubscriptionProperties%2A>, and <xref:ReportService2010.ReportingService2010.SetDataDrivenSubscriptionProperties%2A> methods. To retrieve a list of delivery settings programmatically, use the <xref:ReportService2010.ReportingService2010.GetExtensionSettings%2A> method.  
  
> [!NOTE]  
>  Delivery extension settings are case-sensitive.  
  
## E-Mail Delivery Settings  
 The following table lists the e-mail delivery settings for subscriptions that use report server e-mail.  
  
|Setting|Value|  
|-------------|-----------|  
|**TO**|The e-mail address that appears on the **To** line of the e-mail message. Multiple e-mail addresses are separated by semicolons. Required.|  
|**CC**|The e-mail address that appears on the **Cc** line of the e-mail message. Multiple e-mail addresses are separated by semicolons. Optional.|  
|**BCC**|The e-mail address that appears on the **Bcc** line of the e-mail message. Multiple e-mail addresses are separated by semicolons. Optional.|  
|**ReplyTo**|The e-mail address that appears in the **Reply-To** header of the e-mail message. The value must be a single e-mail address. Optional.|  
|**IncludeReport**|A value that indicates whether to include the report in the e-mail delivery. A value of **true** indicates that the report is delivered in the body of the e-mail message.|  
|**RenderFormat**|The name of the rendering extension to use to generate the rendered report. The name must correspond to one of the visible rendering extensions installed on the report server. This value is required if the **IncludeReport** setting is set to a value of **true**.|  
|**Priority**|The priority with which the e-mail message is sent. Valid values are **LOW**, **NORMAL**, and **HIGH**. The default value is **NORMAL**.|  
|**Subject**|The text in the subject line of the e-mail message.|  
|**Comment**|The text included in the body of the e-mail message.|  
|**IncludeLink**|A value that indicates whether to include a link to the report in the body of the e-mail.|  
  
## File Share Delivery Settings  
 The following table lists the file share delivery settings for subscriptions.  
  
|Setting|Value|  
|-------------|-----------|  
|**FILENAME**|The name of the file that is saved to disk.|  
|**FILEEXTN**|Indicates whether to include a file extension for the rendered report. The value is either **true** or **false**.|  
|**PATH**|The folder path or UNC file share path to which to save the report.|  
|**RENDER_FORMAT**|The format of the report that is saved to disk.|  
|**USERNAME**|The username required to access the network resource or disk.|  
|**PASSWORD**|The password required to access the network resource or disk.|  
|**WRITEMODE**|The write mode to use when accessing the disk. Valid values are **None**, **Overwrite**, and **AutoIncrement**.|  
  
## See Also  
 [Technical Reference &#40;SSRS&#41;](../../../reporting-services/technical-reference-ssrs.md)   
 [Building Applications Using the Web Service and the .NET Framework](../../../reporting-services/report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)  
  
  
