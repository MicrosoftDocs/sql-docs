---
title: "Report Server Windows Service (MSSQLServer) 107"
description: "In this error reference page, learn about event ID 107: Report Server Windows Service (SQL Server) can't connect to the report server database."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: troubleshooting
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "MSSQLServer 107 error"
---
# Report Server Windows Service (MSSQLServer) 107
    
## Details  
  
|Category|Value|  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|107|  
|Event Source|Report Server Windows Service|  
|Component|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]|  
|Message Text|Report Server Windows Service (MSSQLSERVER) can't connect to the report server database.|  
  
## Explanation  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Report Server service can't connect to the report server database. This error occurs during a service restart if a connection to the report server database can't be established. Conditions under which this error occurs include:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] service isn't running when the Report Server service starts.  
  
-   The connection to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service fails because remote connections or the TCP/IP protocol isn't enabled.  
  
-   The report server database isn't configured correctly.  
  
-   The service account isn't configured correctly, or the account no longer has permissions on the report server database. This issue can occur if you don't use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to set up the account or the report server database.  
  
## User action  
 Start the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service if it isn't running and check that remote connections are enabled for TCP/IP protocol.  
  
 Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to configure the report server database and service account.  
  
## Internal-only  
  
## Related content 

 [Configure the report server service account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)   
 [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   

 [Start and stop the report server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)  
  
  
