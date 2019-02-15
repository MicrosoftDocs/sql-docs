---
title: "Reporting Services SharePoint Mode Authentication | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Upgrade SharePoint Mode [Reporting Services]"
  - "SharePoint integration"
  - "SharePoint Mode"
ms.assetid: 2c19794a-dd55-4fe5-b901-6dd93e9f6beb
author: markingmyname
ms.author: maghan
manager: craigg
---
# Reporting Services SharePoint Mode Authentication
  Use the **Reporting Services SharePoint Mode Authentication** page of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to specify the credentials of the service account that is used in the current [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation. The credentials will be used to create a new SharePoint application pool. Also, one new [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint service application will be created. The service application name will contain the name of the previous [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance name.  
  
## Options  
  
-   The **SSRS Application Pool Account Name:** option is read only. The value is automatically populated with the current value from the existing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation that you are upgrading.  
  
-   The **SSRS Application Pool Account Password:** option will be disabled if the application pool account does not require a password. For example, "NT Authority\NetworkService". If the application pool account does require a password, you cannot continue with upgrade until you type the correct password.  
  
 For more information, see [Upgrade and Migrate Reporting Services](https://go.microsoft.com/fwlink/?LinkID=245628) (https://go.microsoft.com/fwlink/?LinkID=245628).  
  
## See Also  
 [Upgrade and Migrate Reporting Services](https://go.microsoft.com/fwlink/?LinkID=245628)  
  
  
