---
title: "Authentication with the report server"
description: "Authentication with the report server"
author: maggiesMSFT
ms.author: maggies
ms.date: 05/30/2017
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "connections [Reporting Services], configuring"
  - "connections [Reporting Services], accounts"
  - "Windows authentication [Reporting Services]"
  - "authentication [Reporting Services]"
  - "Forms authentication"
---

# Authentication with the report server

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] (SSRS) offers several configurable options for authenticating users and client applications against the report server. By default, the report server uses Windows Integrated authentication and assumes trusted relationships where client and network resources are in the same domain or in a trusted domain. Depending on your network topology and the needs of your organization, you can customize the authentication protocol that is used for Windows Integrated authentication. Or, you can use Basic authentication or a custom forms-based authentication extension that you provide. Each of the authentication types can be turned on or off individually. You can enable more than one authentication type if you want the report server to accept requests of multiple types.
  
 All users or applications who request access to report server content or operations must be authenticated before access is allowed.  
  
## Authentication types  
 All users or applications who request access to report server content or operations must be authenticated using the authentication type configured on the report server before access is allowed. The following table describes the authentication types supported by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
|AuthenticationType Name|HTTP Authentication Layer value|Used by default|Description|  
|-----------------------------|-------------------------------------|---------------------|-----------------|  
|RSWindowsNegotiate|Negotiate|Yes|Attempts to use Kerberos for Windows Integrated authentication first, but falls back to NTLM if Active Directory can't grant a ticket for the client request to the report server. Negotiate only falls back to NTLM if the ticket isn't available. If the first attempt results in an error rather than a missing ticket, the report server doesn't make a second attempt.|  
|RSWindowsNTLM|NTLM|Yes|Uses NTLM for Windows Integrated authentication.<br /><br /> The credentials aren't delegated or impersonated on other requests. Subsequent requests follow a new challenge-response sequence. Depending on network security settings, a user might be prompted for credentials or the authentication request are handled transparently.|  
|RSWindowsKerberos|Kerberos|No|Uses Kerberos for Windows Integrated authentication. You must configure Kerberos by setting up setup service principal names (SPNs) for your service accounts, which requires domain administrator privileges. You can set up identity delegation with Kerberos. When you do, the token of the user who is requesting a report can also be used on another connection to the external data sources that provide data to reports.<br /><br /> Before you specify RSWindowsKerberos, be sure that the browser type you're using actually supports it. If you're using Microsoft Edge, or Internet Explorer, Kerberos authentication is only supported through Negotiate. Microsoft Edge, or Internet Explorer, doesn't formulate an authentication request that specifies Kerberos directly.|  
|RSWindowsBasic|Basic|No|Basic authentication is defined in the HTTP protocol and can only be used to authenticate HTTP requests to the report server.<br /><br /> Credentials are passed in the HTTP request in base64 encoding. If you use Basic authentication, use Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL) to encrypt user account information before the information is sent across the network. SSL provides an encrypted channel for sending a connection request from the client to the report server over an HTTP TCP/IP connection. For more information, see [Using SSL to Encrypt Confidential Data](/previous-versions/windows/it-pro/windows-server-2003/cc738495(v=ws.10)) on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] TechNet Web site.|  
|Custom|(Anonymous)|No|Anonymous authentication directs the report server to ignore authentication header in an HTTP request. The report server accepts all requests, but call on a custom [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] Forms authentication that you provide to authenticate the user.<br /><br /> Specify **Custom** only if you're deploying a custom authentication module that handles all authentication requests on the report server. You can't use the Custom authentication type with the default Windows Authentication extension.|  
  
## Unsupported authentication methods  
 The following authentication methods and requests aren't supported.  
  
|Authentication method|Explanation|  
|---------------------------|-----------------|  
|Anonymous|The report server doesn't accept unauthenticated requests from an anonymous user, except for those deployments that include a custom authentication extension.<br /><br /> Report Builder accepts unauthenticated requests if you enable Report Builder access on a report server that is configured for Basic authentication.<br /><br /> For all other cases, anonymous requests are rejected with an HTTP Status 401 Access Denied error before the request reaches [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]. Clients receiving 401 Access Denied must reformulate the request with a valid authentication type.|  
|Single sign-on technologies (SSO)|There's no native support for single sign-on technologies in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. If you want to use a single sign-on technology, you must create a custom authentication extension.<br /><br /> The report server hosting environment doesn't support ISAPI filters. If the SSO technology you're using is implemented as an ISAPI filter, consider using the ISA Server built-in support for RSASecueID or the RADIUS protocol. Otherwise, you can create an ISA Server ISAPI or an HTTPModule for RS, but you should use ISA Server directly.|  
|Passport|Not supported in SQL Server Reporting Services.|  
|Digest|Not supported in SQL Server Reporting Services.|  
  
## Configure authentication settings  
 Authentication settings are configured for default security when the report server URL is reserved. If you modify these settings incorrectly, the report server returns HTTP 401 Access Denied errors for HTTP requests that can't be authenticated. Choosing an authentication type requires that you already know how Windows Authentication is supported in your network. At least one authentication type must be specified. Multiple authentication types can be specified for RSWindows. RSWindows authentication types (that is, **RSWindowsBasic**, **RSWindowsNTLM**, **RSWindowsKerberos**, and **RSWindowsNegotiate**) are mutually exclusive with Custom.  
  
> [!IMPORTANT]  
>  Reporting Services does not validate the settings you specify to determine whether they are correct for your computing environment. It is possible that default security will not work for your installation, or that you will specify configuration settings that are not valid for your security infrastructure. For this reason, it is important that you carefully test your report server deployment in controlled test environment before making it available to your larger organization.  
  
 The Report Server Web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] always use the same authentication type. You can't configure different authentication types for the feature areas of the Report Server service. If you have a scale-out deployment, be sure to duplicate all of your changes on all nodes in the deployment. You can't configure different nodes in the same scale-out to use different authentication types.  
  
 Background processing doesn't accept requests from end-users, however it does authenticate all requests for unattended execution purposes. It always uses Windows Authentication and it authenticates requests using the Report Server service or the unattended execution account if authentication is configured.  
  
## In this section  
  
-   [Configure Windows authentication on the report server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md)  
  
-   [Configure Basic authentication on the report server](../../reporting-services/security/configure-basic-authentication-on-the-report-server.md)  
  
-   [Configure custom or forms authentication on the report server](../../reporting-services/security/configure-custom-or-forms-authentication-on-the-report-server.md)  
  
## Related tasks  
  
|Task Descriptions|Links|  
|-----------------------|-----------|  
|Configure the Windows Integrated authentication type.|[Configure Windows authentication on the report server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md)|  
|Configure the Basic authentication type.|[Configure Basic authentication on the report server](../../reporting-services/security/configure-basic-authentication-on-the-report-server.md)|  
|Configure forms authentication or otherwise a Custom authentication type.|[Configure custom or forms authentication on the report server](../../reporting-services/security/configure-custom-or-forms-authentication-on-the-report-server.md)|  
|Enable the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to handle the custom authentication scenario.|[Configure the web portal to pass custom authentication cookies](configure-the-web-portal-to-pass-custom-authentication-cookies.md)|  

## Related content

[Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)    
[RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)    
[Create and manage role assignments](../../reporting-services/security/create-and-manage-role-assignments.md)    
[Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)    
[Implement a security extension](../../reporting-services/extensions/security-extension/implementing-a-security-extension.md)    
[Configure TLS connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md)    
[Security extensions overview](../../reporting-services/extensions/security-extension/security-extensions-overview.md)   
[Authentication in Reporting Services](../../reporting-services/extensions/security-extension/authentication-in-reporting-services.md)    
[Authorization in Reporting Services](../../reporting-services/extensions/security-extension/authorization-in-reporting-services.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
