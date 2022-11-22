---
description: "Configure the Web Portal to Pass Custom Authentication Cookies"
title: "Configure the Web Portal to Pass Custom Authentication Cookies | Microsoft Docs"
ms.date: 04/18/2017
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "authentication [Reporting Services]"
  - "extensions [Reporting Services], custom security"
ms.assetid: 91aeb053-149e-4562-ae4c-a688d0e1b2ba
author: maggiesMSFT
ms.author: maggies
---
# Configure the Web Portal to Pass Custom Authentication Cookies

If you are using a custom authentication extension, you should configure the web portal to transmit custom authentication cookies. Otherwise, the web portal will only transmit cookies through HTTP requests specific to the report server. If you want to transmit additional cookies, you must modify the RSReportServer.Config file.

## Modifying the RSReportServer.Config File

You can enable the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to transmit additional cookies through to the report server by adding a \<**PassThroughCookies**> element to the web portal configuration settings in the RSReportServer.config file. Transmitting additional cookies is helpful in a single sign-on authentication solution that requires not only the report server authentication cookies, but also cookies from a third-party authentication system.

To enable additional cookies to be transmitted through HTTP requests when using the web portal, set the following elements in the RSReportServer.config file:
  
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

[Authentication with the Report Server](../../reporting-services/security/authentication-with-the-report-server.md)   
[RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
[Security Extensions Overview](../../reporting-services/extensions/security-extension/security-extensions-overview.md)   
[Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)  
More questions? [Try the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)