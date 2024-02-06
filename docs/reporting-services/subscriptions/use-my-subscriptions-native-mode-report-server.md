---
title: "Use My Subscriptions (native mode report server)"
description: Learn to use the My Subscriptions page in the Reporting Services web portal to view, modify, enable, disable, or delete existing subscriptions.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/01/2016
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], My Subscriptions page"
  - "My Subscriptions page [Reporting Services]"
---
# Use My Subscriptions (native mode report server)
The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Web portal includes a **My Subscriptions** page that organizes all of your subscriptions into one place. You can use *My Subscriptions* to view, modify, enable, disable, and delete existing subscriptions. However, you can't use it to create subscriptions.  My Subscriptions shows only the subscriptions that you created, even if you're added as a subscriber to subscriptions that others own. It also doesn't show data-driven subscriptions.
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode|  
  
The search field dynamically filters the list of subscriptions as youYou can't search for subscriptions by name, nor can you search for subscriptions based on trigger information, status information, and so forth. For more information, see [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).
  
## Open the My Subscriptions page  
1. Open the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] Web portal.
2. Select settings :::image type="icon" source="../../reporting-services/subscriptions/media/ssrs-portal-settings-gear.png"::: in the toolbar.
3. Select **My Subscriptions**.

For more information, see [Web portal (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md).

## Use Windows PowerShell to list My Subscriptions  
  
 The following PowerShell script returns the list of subscriptions and subscription properties for the current user. For more information, see [ReportingService2010.ListMySubscriptions method](/dotnet/api/reportservice2010.reportingservice2010.listmysubscriptions).  
  
```  
#server -  all subscriptions of the current user at the given server or site  
$server="[server name]/reportserver"  
$rs2010 = New-WebServiceProxy -Uri "https://$server/ReportService2010.asmx" -Namespace SSRS.ReportingService2010 -UseDefaultCredential;  
  
$subscriptions=ListMySubscriptions(ItemPathOrSiteURL)  
$subscriptions | select Path, report, Description, Owner, SubscriptionID, lastexecuted,Status  
#uncomment the following to list all the subscription properties  
#$subscriptions

```  
  
## Related content
 [Data-driven subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md)   
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [Create and manage subscriptions for native mode report servers](./create-and-manage-subscriptions-for-native-mode-report-servers.md)  
  
