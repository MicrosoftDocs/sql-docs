---
title: "Configure SSL Connections on a Native Mode Report Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Secure Sockets Layer (SSL)"
ms.assetid: 212f2042-456a-4c0a-8d76-480b18f02431
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure SSL Connections on a Native Mode Report Server
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode uses the HTTP SSL (Secure Sockets Layer) service to establish encrypted connections to a report server. If you have certificate (.cer) file installed in a local certificate store on the report server computer, you can bind the certificate to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL reservation to support report server connections through an encrypted channel.  
  
> [!TIP]  
>  If you are using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, see SharePoint documentation for more information. For example [How to enable SSL on a SharePoint 2010 web application (https://blogs.msdn.com/b/sowmyancs/archive/2010/02/12/how-to-enable-ssl-on-a-sharepoint-web-application.aspx)](https://blogs.msdn.com/b/sowmyancs/archive/2010/02/12/how-to-enable-ssl-on-a-sharepoint-web-application.aspx).  
  
 Because Internet Information Services (IIS) also uses HTTP SSL, there are significant interoperability issues that you must account for if you run IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on the same computer. Be sure to review the Interoperability Issues with IIS section for guidance on how to address these issues.  
  
## Server Certificate Requirements  
 You must have a server certificate installed on the computer (client certificates are not supported). Reporting Services does not provide functionality for requesting, generating, downloading, or installing a certificate. [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] provides a Certificates snap-in that you can use to request a certificate from a trusted certificate authority.  
  
 For testing purposes, you can generate a certificate locally. If you use the **MakeCert** utility and the sample command as a template, be sure to specify your server name as the host and remove all line breaks before running the command. If you run the command in a DOS window, you might need to increase the buffer size of the window to accommodate the entire command.  
  
 If you are running IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] together on the same computer, you can use the IIS Manager console application to get the certificate installed on your computer. IIS Manager includes options for creating and packaging a certificate request (.crt) file for subsequent processing by a trusted certificate authority. The certificate authority that you are using will generate a certificate (.cer) file and send it back to you. You can use IIS Management console to install the certificate file in the local store. For more information, see [Using SSL to Encrypt Confidential Data](https://go.microsoft.com/fwlink/?LinkId=71123) on Technet.  
  
## Interoperability Issues with IIS  
 The presence of IIS on the same computer as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] will significantly affect SSL connections to a report server:  
  
-   If IIS is installed, the World Wide Web (W3SVC) service must always be running. The HTTP SSL service will make a dependency on IIS if it detects that IIS is running. This means that the World Wide Web service (W3SVC) must be running whenever IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are installed on the same computer and you are configuring report server URLs for SSL connections.  
  
-   Uninstalling IIS can temporarily disrupt service to an SSL-bound report server URL. For this reason, it is strongly recommended that you restart the computer after you uninstall IIS.  
  
     Rebooting the computer is necessary to clear all SSL sessions from cache. Some operating systems cache SSL sessions up to 10 hours, causing an https:// URL to continue to work even after the SSL binding has been removed from the URL reservation in HTTP.SYS. Rebooting the computer closes any open connections that use the channel.  
  
## Bind SSL to a Reporting Services URL Reservation  
 The following steps do not include instructions for requesting, generating, downloading, or installing a certificate. You must have a certificate installed and available to use. The certificate properties that you specify, the certificate authority you obtain it from, and the tools and utilities you use to request and install the certificate are up to you.  
  
 You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to bind the certificate. If the certificate is installed correctly in the local computer store, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool will detect it and display it in the **SSL Certificates** list on the **Web Service URL** and **Report Manager URL** pages.  
  
### To configure a report server URL for SSL  
  
1.  Start the Reporting Services Configuration tool and connect to the report server.  
  
2.  Click **Web Service URL**.  
  
3.  Expand the list of SSL Certificates. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] detects server authentication certificates in the local store. If you installed a certificate and you do not see it in the list, you might need to restart the service. You can use the **Stop** and **Start** buttons on the **Report Server Status** page in the Reporting Services Configuration tool to restart the service.  
  
4.  Select the certificate.  
  
5.  Click **Apply**.  
  
6.  Click the URL to verify it works.  
  
 Report server database configuration is a requirement for testing the URL. If you have not yet created the report server database, do so before testing the URL.  
  
 URL reservations for Report Manager and the Report Server Web service are configured independently. If you want to also configure Report Manager access through an SSL-encrypted channel, continue with the following steps:  
  
1.  Click **Report Manager URL**.  
  
2.  Click **Advanced**.  
  
3.  In **Multiple SSL Identities for Report Manager**, click **Add**.  
  
4.  Select the certificate, click **OK**, and then click **Apply**.  
  
5.  Click the URL to verify it works.  
  
## How Certificate Bindings Are Stored  
 Certificate bindings will be stored in HTTP.SYS. A representation of the bindings you defined will also be stored in the `URLReservations` section of the RSReportServer.config file. The settings in the configuration file are only a representation of actual values that are specified elsewhere. Do not modify the values in the configuration file directly. The configuration settings will appear in the file only after you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or the Report Server Windows Management Instrumentation (WMI) provider to bind a certificate.  
  
> [!NOTE]  
>  If you configure a binding with an SSL certificate in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and you later want to remove the certificate from the computer, make sure to remove the binding from [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] before you remove the certificate from the computer. Otherwise, you will be unable to remove the binding by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or WMI and you will receive an "Invalid parameter" error. If you have already removed the certificate from the computer, you can use the Httpcfg.exe tool to remove the binding from HTTP.SYS. For more information about Httpcfg.exe, see the Windows product documentation.  
  
 SSL bindings are a shared resource in Microsoft Windows. Changes made by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager or other tools like IIS Manager can impact other applications on the same computer. It is a best practice to use the same tool to edit bindings that you used to create the bindings.  For example if you created SSL bindings using Configuration Manager, then it is recommended you use Configuration Manager to manage the life-cycle of the bindings. If you use IIS manager to create bindings, then it is recommended you use IIS manager to manage the life-cycle of the bindings. If IIS is installed on the computer before [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is installed, it is a good practice to review the SSL configuration in IIS before configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 If you remove SSL bindings for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] using the Reporting Services Configuration Manager, SSL may no longer work for Web sites on a server that is running Internet Information Services (IIS) or on another HTTP.SYS server. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager removes the following registry key. When this registry key is removed, the SSL binding for IIS is also removed. Without this binding, SSL is not provided for the HTTPS protocol. To diagnose this issue, use IIS Manager or the HTTPCFG.exe command line utility.To resolve the issue, restore the SSL binding for your web sites using IIS Manager.To prevent this issue in the future, use IIS manger to remove the SSL bindings and then use IIS Manager to restore the binding for the desired Web sites. For more information, see the knowledge base article [SSL no longer works after you remove an SSL binding (https://support.microsoft.com/kb/956209/n)](https://support.microsoft.com/kb/956209/n).  
  
## See Also  
 [Authentication with the Report Server](authentication-with-the-report-server.md)   
 [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](../report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)   
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)   
 [Reporting Services Configuration Manager &#40;del&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)   
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
  
  
