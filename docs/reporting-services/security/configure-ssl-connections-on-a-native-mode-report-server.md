---
description: "Configure TLS Connections on a Native Mode Report Server"
title: "Configure TLS Connections on a Native Mode Report Server | Microsoft Docs"
ms.date: 09/27/2020
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "Secure Sockets Layer (SSL)"
ms.assetid: 212f2042-456a-4c0a-8d76-480b18f02431
author: maggiesMSFT
ms.author: maggies
---
# Configure TLS connections on a native mode report server

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode uses the HTTP SSL (Secure Sockets Layer) service to establish encrypted connections to a report server. Transport Layer Security (TLS) was previously known as Secure Sockets Layer (SSL). If you have certificate (.cer) file installed in a local certificate store on the report server computer, you can bind the certificate to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL reservation to support report server connections through an encrypted channel.  
  
> [!TIP]  
>  If you are using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, see SharePoint documentation for more information. For example [How to enable TLS on a SharePoint 2010 web application](/archive/blogs/sowmyancs/how-to-enable-ssl-on-a-sharepoint-2010-web-application).  
  
 Because Internet Information Services (IIS) also uses HTTP SSL, there are significant interoperability issues that you must account for if you run IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on the same computer. Be sure to review the Interoperability Issues with IIS section for guidance on how to address these issues.  
  
## Server certificate requirements  
 You must have a server certificate installed on the computer (client certificates are not supported). Reporting Services does not provide functionality for requesting, generating, downloading, or installing a certificate. Windows Server 2012 and later provide a Certificates snap-in that you can use to request a certificate from a trusted certificate authority.  
  
 For testing purposes, you can generate a certificate locally. If you use the **MakeCert** utility and the sample command as a template, be sure to specify your server name as the host and remove all line breaks before running the command. If you run the command in a DOS window, you might need to increase the buffer size of the window to accommodate the entire command.  
  
 If you are running IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] together on the same computer, you can use the IIS Manager console application to get the certificate installed on your computer. IIS Manager includes options for creating and packaging a certificate request (.crt) file for subsequent processing by a trusted certificate authority. The certificate authority that you are using will generate a certificate (.cer) file and send it back to you. You can use IIS Management console to install the certificate file in the local store. For more information, see [Using SSL to Encrypt Confidential Data](/previous-versions/windows/it-pro/windows-server-2003/cc738495(v=ws.10)) on Technet.  
  
## Interoperability issues with IIS  
 The presence of IIS on the same computer as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] will significantly affect TLS connections to a report server:  
  
-   If IIS is installed, the World Wide Web (W3SVC) service must always be running. The HTTP SSL service will make a dependency on IIS if it detects that IIS is running. This means that the World Wide Web service (W3SVC) must be running whenever IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are installed on the same computer and you are configuring report server URLs for TLS connections.  
  
-   Uninstalling IIS can temporarily disrupt service to a TLS-bound report server URL. For this reason, it is recommended that you restart the computer after you uninstall IIS.  
  
     Rebooting the computer is necessary to clear all TLS sessions from cache. Some operating systems cache TLS sessions up to 10 hours, causing an https:// URL to continue to work even after the TLS binding has been removed from the URL reservation in HTTP.SYS. Rebooting the computer closes any open connections that use the channel.  
  
## Bind TLS to a reporting services URL reservation  
 The following steps do not include instructions for requesting, generating, downloading, or installing a certificate. You must have a certificate installed and available to use. The certificate properties that you specify, the certificate authority you obtain it from, and the tools and utilities you use to request and install the certificate are up to you.  
  
 You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to bind the certificate. If the certificate is installed correctly in the local computer store, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool will detect it and display it in the **SSL Certificates** list on the **Web Service URL** and **Web Portal URL** pages.  
  
### To configure a report server URL for TLS  
  
1.  Start the Reporting Services Configuration tool and connect to the report server.  
  
2.  Select **Web Service URL**.  
  
3.  Expand the list of TLS/SSL Certificates. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] detects server authentication certificates in the local store. If you installed a certificate and you do not see it in the list, you might need to restart the service. You can use the **Stop** and **Start** buttons on the **Report Server Status** page in the Reporting Services Configuration tool to restart the service (top page).  
  
4.  Select the certificate.  
  
5.  Click **Apply**.  
  
6.  Click the URL to verify it works.  
  
 Report server database configuration is a requirement for testing the URL. If you have not yet created the report server database, do so before testing the URL.  
  
 URL reservations for Web Portal URL and the Report Server Web Services URL are configured independently. If you want to also configure the web portal access through a TLS-encrypted channel, continue with the following steps:  
  
1.  Access the **Web Portal URL**.
  
2.  Select **Advanced**.  
  
3.  In **Multiple HTTPS Identities for the currently Reporting Service feature**, select **Add**.  
  
4.  Select the certificate, select **OK**, and then select **Apply**.  
  
5.  Test the URL to verify it works.  
  
## How certificate bindings are stored  
 Certificate bindings will be stored in HTTP.SYS. A representation of the bindings you defined will also be stored in the **URLReservations** section of the RSReportServer.config file. The settings in the configuration file are only a representation of actual values that are specified elsewhere. Do not modify the values in the configuration file directly. The configuration settings will appear in the file only after you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or the Report Server Windows Management Instrumentation (WMI) provider to bind a certificate.  
  
> [!NOTE]  
>  If you configure a binding with a TLS/SSL certificate in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and you later want to remove the certificate from the computer, make sure to remove the binding from [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] before you remove the certificate from the computer. Otherwise, you will be unable to remove the binding by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or WMI and you will receive an "Invalid parameter" error. If you have already removed the certificate from the computer, you can use the Httpcfg.exe tool to remove the binding from HTTP.SYS. For more information about Httpcfg.exe, see the Windows product documentation.  
  
 TLS bindings are a shared resource in Microsoft Windows. Changes made by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager or other tools like IIS Manager can impact other applications on the same computer. It is a best practice to use the same tool to edit bindings that you used to create the bindings.  For example if you created TLS bindings using Configuration Manager, then it is recommended you use Configuration Manager to manage the life cycle of the bindings. If you use IIS manager to create bindings, then it is recommended you use IIS manager to manage the life cycle of the bindings. If IIS is installed on the computer before [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is installed, it is a good practice to review the TLS configuration in IIS before configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 If you remove TLS bindings for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] using the Report Server Configuration Manager, TLS may no longer work for Web sites on a server that is running Internet Information Services (IIS) or on another HTTP.SYS server. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager removes the following registry key: **HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HTTP\Parameters\SslBindingInfo\0.0.0.0:443** When this registry key is removed, the TLS binding for IIS is also removed. Without this binding, TLS is not provided for the HTTPS protocol. To diagnose this issue, use IIS Manager or the HTTPCFG.exe command line utility. To resolve the issue, restore the TLS binding for your web sites using IIS Manager. To prevent this issue in the future, use IIS Manager to remove the TLS bindings and then use IIS Manager to restore the binding for the desired Web sites. For more information, see the knowledge base article [SSL no longer works after you remove an SSL binding (https://support.microsoft.com/kb/956209/n)](https://web.archive.org/web/20150215042139/http://support.microsoft.com:80/kb/956209).  
  
## See also  
 [Authentication with the Report Server](../../reporting-services/security/authentication-with-the-report-server.md)   
 [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)   
 [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [Configure Report Server URLs  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
  
