---
title: "Domain accounts required for SharePoint farm (Upgrade Advisor) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 90cd6d3e-a271-4cb8-81f2-fc555b2d3cab
author: markingmyname
ms.author: maghan
manager: craigg
---
# Domain accounts required for SharePoint farm (Upgrade Advisor)
  SharePoint products that are configured for a farm environment require that you use domain accounts.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode.|  
  
## Component  
 [!INCLUDE[ssRS](../../includes/ssrs.md)]  
  
### Description  
 SharePoint products that are configured for a farm environment require that you use domain accounts for services and database connections. This includes the account you have specified for the Reporting Services service account.  
  
 SharePoint 2010 Central Administration pages are one place you will see problems if you are not using a domain user account for Reporting Services. When you try to configure Reporting Services integration, you will see an error message similar to the following:  
  
 "The report server is running under the built-in NT AUTHORITY\NETWORK SERVICE account, which is not supported by a SharePoint farm installation. Please reconfigure the report server to run under a domain account."  
  
## Corrective Action  
 For [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and previous versions, use the Reporting Services Configuration Manager to change the account that is assigned as the report server service account.  
  
#### To change the service account from Configuration Manager  
  
1.  From the **Start** menu, select **All Programs**, and then click **Microsoft SQL Server 2008 R2**.  
  
2.  Select **Configuration Tools**, and then click **Reporting Services Configuration Manager**.  
  
3.  In Configuration Manager, select the **Service Account** tab.  
  
4.  Select **Use another account** and enter the credentials for a domain account.  
  
5.  Click **Apply**.  
  
## See Also  
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)  
  
  
