---
title: "Configure TLS connections on a native mode report server"
description: Learn how to configure TLS connections on a native mode report server to establish encrypted connections.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/19/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Secure Sockets Layer (SSL)"
---
# Configure TLS connections on a native mode report server

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode uses the HTTP TLS (Transport Layer Security) service to establish encrypted connections to a report server. TLS was previously known as Secure Sockets Layer (SSL). If you have certificate (.cer) file installed in a local certificate store on the report server computer, you can bind the certificate to a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL reservation to support report server connections through an encrypted channel.

> [!TIP]
> For more information about [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, see SharePoint documentation. For example, [How to enable TLS on a SharePoint 2010 web application](/archive/blogs/sowmyancs/how-to-enable-ssl-on-a-sharepoint-2010-web-application).

Because Internet Information Services (IIS) also uses HTTP TLS, there are significant interoperability issues that you must account for if you run IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on the same computer. Be sure to review the Interoperability Issues with IIS section for guidance on how to address these issues.

## Server certificate requirements

You must have a server certificate installed on the computer (client certificates aren't supported). Reporting Services doesn't provide functionality for requesting, generating, downloading, or installing a certificate. Windows Server 2012 and later provide a Certificates snap-in that you can use to request a certificate from a trusted certificate authority.

For testing purposes, you can generate a certificate locally. If you use the **MakeCert** utility and the sample command as a template, be sure to specify your server name as the host and remove all line breaks before running the command. If you run the command in a DOS window, you might need to increase the buffer size of the window to accommodate the entire command.

If you're running IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] together on the same computer, you can use the IIS Manager console application to get the certificate installed on your computer. IIS Manager includes options for creating and packaging a certificate request (.crt) file for subsequent processing by a trusted certificate authority. The certificate authority that you use generates a certificate (.cer) file and send it back to you. You can use IIS Management console to install the certificate file in the local store. For more information, see [Use SSL to Encrypt Confidential Data](/previous-versions/windows/it-pro/windows-server-2003/cc738495(v=ws.10)).

## Interoperability issues with IIS

 The presence of IIS on the same computer as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] significantly affects TLS connections to a report server:

- If IIS is installed, the World Wide Web (W3SVC) service must always be running. The HTTP TLS service makes a dependency on IIS if it detects that IIS is running. This dependency means that the World Wide Web service (W3SVC) must be running whenever IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are installed on the same computer and you're configuring report server URLs for TLS connections.

- Uninstalling IIS can temporarily disrupt service to a TLS-bound report server URL. For this reason, you should restart the computer after you uninstall IIS.

     You must restart the computer to clear all TLS sessions from cache. Some operating systems cache TLS sessions up to 10 hours, causing an https:// URL to continue to work even after the TLS binding is removed from the URL reservation in HTTP.SYS. Restarting the computer closes any open connections that use the channel.

## Bind TLS to a reporting services URL reservation

 The following steps don't include instructions for requesting, generating, downloading, or installing a certificate. You must have a certificate installed and available to use. The certificate properties that you specify, the certificate authority you obtain it from, and the tools and utilities you use to request and install the certificate are up to you.

 You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to bind the certificate. If the certificate is installed correctly in the local computer store, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool detects it and displays it in the **SSL Certificates** list on the **Web Service URL** and **Web Portal URL** pages.

### Configure a report server URL for TLS

1. Start the Reporting Services Configuration tool and connect to the report server.

1. Select **Web Service URL**.

1. Expand the list of TLS/SSL Certificates. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] detects server authentication certificates in the local store. If you installed a certificate and you don't see it in the list, you might need to restart the service. You can use the **Stop** and **Start** buttons on the **Report Server Status** page in the Reporting Services Configuration tool to restart the service (top page).

1. Select the certificate.

1. Select **Apply**.

1. Select the URL to verify it works.

 Report server database configuration is a requirement for testing the URL. If you aren't yet creating the report server database, do so before testing the URL.

 URL reservations for Web Portal URL and the Report Server Web Services URL are configured independently. If you want to also configure the web portal access through a TLS-encrypted channel, continue with the following steps:

1. Access the **Web Portal URL**.

1. Select **Advanced**.

1. In **Multiple HTTPS Identities for the currently Reporting Service feature**, select **Add**.

1. Choose the certificate, select **OK**, and then select **Apply**.

1. Test the URL to verify it works.

## How certificate bindings are stored

Certificate bindings are stored in HTTP.SYS. A representation of the bindings you defined are stored in the **URLReservations** section of the RSReportServer.config file. The settings in the configuration file are only a representation of actual values that are specified elsewhere. Don't modify the values in the configuration file directly. The configuration settings will appear in the file only after you use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or the Report Server Windows Management Instrumentation (WMI) provider to bind a certificate.

> [!NOTE]
> If you configure a binding with a TLS/SSL certificate in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and you later want to remove the certificate from the computer, make sure to remove the binding from [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] before you remove the certificate from the computer. Otherwise, you will be unable to remove the binding by using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or WMI and you will receive an "Invalid parameter" error. If you have already removed the certificate from the computer, you can use the Httpcfg.exe tool to remove the binding from HTTP.SYS. For more information about Httpcfg.exe, see the Windows product documentation.

TLS bindings are a shared resource in Microsoft Windows. Changes made by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager or other tools like IIS Manager can affect other applications on the same computer. The best practice is to use the same tool to edit bindings that you used to create the bindings. For example if you created TLS bindings by using Configuration Manager, then you should use Configuration Manager to manage the life cycle of the bindings. If you use IIS manager to create bindings, then you should use IIS manager to manage the life cycle of the bindings. If IIS is installed on the computer before [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is installed, it's a good practice to review the TLS configuration in IIS before configuring [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].

If you remove TLS bindings for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] by using the Report Server Configuration Manager, TLS might no longer work for Web sites on a server that is running Internet Information Services (IIS) or on another HTTP.SYS server. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager removes the following registry key: **HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HTTP\Parameters\SslBindingInfo\0.0.0.0:443** When this registry key is removed, the TLS binding for IIS is also removed. Without this binding, TLS isn't provided for the HTTPS protocol. To diagnose this issue, use IIS Manager or the HTTPCFG.exe command line utility. To resolve the issue, restore the TLS binding for your web sites by using IIS Manager. To prevent this issue in the future, use IIS Manager to remove the TLS bindings, and then use IIS Manager to restore the binding for the desired Web sites. For more information, see the knowledge base article [SSL no longer works after you remove an SSL binding from SQL Server 2008 Reporting Services by using the Reporting Services Configuration Management tool](https://web.archive.org/web/20150215042139/http://support.microsoft.com:80/kb/956209).

## Related content

- [Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md)
- [Configure and administer a report server (SSRS Native Mode)](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [Configure report server URLs (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)
