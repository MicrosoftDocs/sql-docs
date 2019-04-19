---
title: "Use My Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], My Subscriptions page"
  - "My Subscriptions page [Reporting Services]"
ms.assetid: e96623ba-677e-4748-8787-f32bed3b5c12
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Use My Subscriptions
  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Report Manager includes a **My Subscriptions** page that organizes all of your subscriptions into one place. You can use My Subscriptions to view, modify, and delete existing subscriptions. However, you cannot use it to create subscriptions.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] Native mode|  
  
 Within My Subscriptions, you can sort subscriptions by folder, report, description, trigger, last run, or status. All values are sorted alphabetically except for Last Run, which is in chronological order.  
  
 My Subscriptions shows only the subscriptions that you create. It does not list subscriptions that are owned by other users, even if you are added as a subscriber to those subscriptions, nor does it show data-driven subscriptions.  
  
 You cannot search for subscriptions by name, nor can you search for subscriptions based on trigger information, status information, and so forth. For more information, see [Create, Modify, and Delete Standard Subscriptions &#40;Reporting Services in Native Mode&#41;](create-and-manage-subscriptions-for-native-mode-report-servers.md).  
  
## How to Use My Subscriptions  
 My Subscriptions is available through Report Manager. To access My Subscriptions, click **My Subscriptions** on the Report Manager global toolbar.  
  
## Use Windows PowerShell to list MySubscriptions  
 ![PowerShell related content](../media/rs-powershellicon.jpg "PowerShell related content")  
  
 The following PowerShell script will return the list of subscriptions and subscription properties for the current user. For more information, see [ReportingService2010.ListMySubscriptions Method](https://technet.microsoft.com/library/reportservice2010.reportingservice2010.listmysubscriptions.aspx).  
  
```  
#server -  all subscriptions of the current user at the given server or site  
$server="[server name]/reportserver"  
$rs2010 = New-WebServiceProxy -Uri "http://$server/ReportService2010.asmx" -Namespace SSRS.ReportingService2010 -UseDefaultCredential ;  
  
$subscriptions=ListMySubscriptions(ItemPathOrSiteURL)  
$subscriptions | select Path, report, Description, Owner, SubscriptionID, lastexecuted,Status  
#uncomment the following to list all the subscription properties  
#$subscriptions  
  
```  
  
## See Also  
 [Data-Driven Subscriptions](data-driven-subscriptions.md)   
 [Subscriptions and Delivery &#40;Reporting Services&#41;](subscriptions-and-delivery-reporting-services.md)   
 [Create and Manage Subscriptions for Native Mode Report Servers](../create-manage-subscriptions-native-mode-report-servers.md)  
  
  
