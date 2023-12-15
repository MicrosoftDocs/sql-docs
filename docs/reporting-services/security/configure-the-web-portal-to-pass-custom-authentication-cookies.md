---
title: "Configure the web portal to pass custom authentication cookies"
description: "Configure the web portal to pass custom authentication cookies"
author: maggiesMSFT
ms.author: maggies
ms.date: 04/18/2017
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "authentication [Reporting Services]"
  - "extensions [Reporting Services], custom security"
---
# Configure the web portal to pass custom authentication cookies

If you're using a custom authentication extension, you should configure the web portal to transmit custom authentication cookies. Otherwise, the web portal transmits cookies through HTTP requests specific to the report server. If you want to transmit other cookies, you must modify the RSReportServer.Config file.

## Modify the RSReportServer.Config file

You can enable the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to transmit other cookies through to the report server by adding a `<PassThroughCookies>` element to the web portal configuration settings in the RSReportServer.config file. Transmitting other cookies is helpful in a single sign-on authentication solution that requires not only the report server authentication cookies, but also cookies from a third-party authentication system.

To enable other cookies to be transmitted through HTTP requests by using the web portal, set the following elements in the RSReportServer.config file:
  
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
  
## Related content

[Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md)   
[RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
[Security extensions overview](../../reporting-services/extensions/security-extension/security-extensions-overview.md)   
[Configure and administer a report server &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)  
More questions? [Try the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
