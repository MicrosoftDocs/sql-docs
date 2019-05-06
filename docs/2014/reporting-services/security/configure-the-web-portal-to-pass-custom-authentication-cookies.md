---
title: "Configure Report Manager to Pass Custom Authentication Cookies | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "authentication [Reporting Services]"
  - "extensions [Reporting Services], custom security"
ms.assetid: 91aeb053-149e-4562-ae4c-a688d0e1b2ba
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure Report Manager to Pass Custom Authentication Cookies
  If you are using a custom authentication extension, you should configure Report Manager to transmit custom authentication cookies. Otherwise, Report Manager will only transmit cookies through HTTP requests specific to the report server. If you want to transmit additional cookies, you must modify the RSReportServer.Config file.  
  
## Modifying the RSReportServer.Config File  
 You can enable Report Manager to transmit additional cookies through to the report server by adding a <`PassThroughCookies`> element to the Report Manager configuration settings in the RSReportServer.config file. Transmitting additional cookies is helpful in a single sign-on authentication solution that requires not only the report server authentication cookies, but also cookies from a third-party authentication system.  
  
 To enable additional cookies to be transmitted through HTTP requests when using Report Manager, set the following elements in the RSReportServer.config file:  
  
```  
<UI>  
   <CustomAuthenticationUI>  
      ...  
      <PassThroughCookies>  
         <PassThroughCookie>cookiename1</PassThroughCookie>  
         <PassThroughCookie>cookiename2</PassThroughCookie>  
      </PassThroughCookies>  
   </CustomAuthenticationUI>  
      ...  
</UI>  
```  
  
## See Also  
 [Authentication with the Report Server](authentication-with-the-report-server.md)   
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)   
 [Security Extensions Overview](../extensions/security-extension/security-extensions-overview.md)   
 [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)  
  
  
