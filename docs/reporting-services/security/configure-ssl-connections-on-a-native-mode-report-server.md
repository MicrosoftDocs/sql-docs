---
title: "Configure TLS connections on a native mode report server"
description: See how to configure TLS connections on a native mode report server so that you can encrypt connections to the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/23/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Secure Sockets Layer (SSL)"

#customer intent: As a SQL Server user, I want to learn how to configure TLS connections on my native mode report server so that I can encrypt my connections to the server.
---



# Configure TLS connections on a native mode report server

[!INCLUDE[SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE[SSRS applies to 2016 and later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[SSRS applies to Power BI Report Server](../../includes/ssrs-appliesto-pbirs.md)]

In SQL Server Reporting Services (SSRS) native mode, you can use the Transport Layer Security (TLS) protocol to establish encrypted connections to a report server. TLS was previously known as Secure Sockets Layer (SSL). If you have a certificate (.cer) file installed in a local certificate store on the report server, you can bind the certificate to an SSRS URL reservation to support report server connections through an encrypted channel.

> [!TIP]
> For more information about SSRS SharePoint mode, see [Reporting Services Report Server (SharePoint mode)](/sql/reporting-services/report-server-sharepoint/reporting-services-report-server-sharepoint-mode).
  
Because Internet Information Services (IIS) also uses TLS, there are significant interoperability issues that you must account for if you run IIS and SSRS on the same computer. For guidance on how to address these issues, review the [Interoperability issues with IIS](#interoperability-issues-with-iis) section, later in this article.

## Prerequisites

- A configured native mode report server
- A server certificate installed on your computer
  
## Install a server certificate

You must install a server certificate on your report server in the local store. Client certificates aren't supported.

SSRS doesn't provide functionality for requesting, generating, downloading, or installing a certificate. It's up to you to choose the properties that you specify for the certificate and the certificate authority that you obtain it from. You also decide on the tools and utilities that you use to request and install the certificate.

A few possibilities exist for obtaining a certificate:

- Windows Server provides a Certificates snap-in that you can use to request a certificate from a trusted certificate authority.

- If you want to generate a certificate locally for testing purposes, you can run the following PowerShell script.

  - Run the script as an administrator.
  - Replace the placeholders with values that are appropriate for your environment.

  ```powershell
  # Create a self-signed certificate in the local store.
  $newCertificate = New-SelfSignedCertificate -CertStoreLocation cert:\LocalMachine\My -DnsName <server-name> -FriendlyName "<friendly-name>"

  # Convert the report server password to a secure string.
  $secureStringPassword = ConvertTo-SecureString "<server-password>" -AsPlainText -Force

  # Set up a temporary folder if one doesn't exist.
  $folderPath = "C:\Temp"
  if (-not (Test-Path -Path $folderPath)) {
      New-Item -Path $folderPath -ItemType Directory
  }

  # Set up a variable for the path of a certificate file.
  $certificateFilePath = $folderPath, "\certificate-export.pfx" -join ""

  # Set up a variable for the path of the certificate's store location.
  $certificateStoreLocation = "cert:\LocalMachine\My\", $newCertificate.Thumbprint -join ""

  # Export the certificate to the file.
  Export-PFXCertificate -Cert $certificateStoreLocation -File $certificateFilePath -Password $secureStringPassword

  # Import the certificate from the file to the trusted root to avoid problems with the certificate not being trusted. 
  Import-PfxCertificate -FilePath $certificateFilePath cert:\LocalMachine\Root -Password $secureStringPassword
  ```

  The certificate is now available for use in Report Server Configuration Manager.

- If you run IIS and SSRS together on the same computer, you can use the IIS Manager console application to request and install a certificate:
  1. In IIS Manager, create and package a certificate request (.crt) file for subsequent processing by a trusted certificate authority. For more information, see [Request a Server Certificate](/previous-versions/windows/it-pro/windows-server-2003/cc757493(v=ws.10)).
  1. After the certificate authority sends you the certificate (.cer) file, use IIS Management to install the certificate file in the local store. For more information, see [Install a Server Certificate](/previous-versions/windows/it-pro/windows-server-2003/cc740068(v=ws.10)).

## Interoperability issues with IIS  

The presence of IIS on the same computer as SSRS significantly affects TLS connections to a report server:

- A dependency is created on IIS when you configure report server URLs for TLS connections. IIS, in turn, has a dependency on the World Wide Web Publishing Service. As a result, the World Wide Web Publishing Service must be running when you configure report server URLs for TLS connections.
- Uninstalling IIS can temporarily disrupt service to a TLS-bound report server URL. After you uninstall IIS, you should restart the computer to clear all TLS sessions from the cache. Some operating systems cache TLS sessions up to 10 hours. As a result, a TLS-bound URL can continue to work after the TLS binding is removed from the URL reservation in the HTTP.sys repository. Restarting the computer closes any open connections that use the channel.

## Bind TLS to an SSRS URL reservation  

Reservations for the report server web service URL and web portal URL are configured independently.

### Configure web service access through a TLS-encrypted channel

To configure a URL that you can use to access the report server, take the following steps:

1. Open Report Server Configuration Manager and connect to the report server.  

1. In the **Report Server Status** page, select **Stop**.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/stop-report-server.png" alt-text="Screenshot of the Report Server Status page in the configuration tool. The Stop button is highlighted.":::

   A message in the **Results** window indicates that the server is stopped.

1. Select **Start**. A message in the **Results** window indicates that the server is running.

1. In the **Connect** pane, select **Web Service URL**.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/select-web-service-url.png" alt-text="Screenshot of the configuration tool. On the left, Web Service URL is highlighted.":::

1. In the **HTTPS Certificate** list, select the certificate that you installed.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/select-web-service-url-certificate.png" alt-text="Screenshot of the configuration tool. In the HTTPS Certificate list, a certificate named Test certificate is highlighted.":::

1. Select **Apply**. Messages in the **Results** window indicate the progress of the configuration tool.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/reserve-web-service-url-results.png" alt-text="Screenshot of the Results window in the configuration tool. Messages indicate a successful certificate binding.":::

1. Under **Report Server Web Service URLs**, select the URL that you reserved. If prompted, enter credentials for your report server. A browser window opens to provide access to the report server.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/access-report-server.png" alt-text="Screenshot of a browser window that shows the main page of the ReportServer folder of the report server.":::

   If the URL doesn't connect to the report server, check the report server database. For the URL to work, you first need to create and configure the database.  

### Configure web portal access through a TLS-encrypted channel

If you also want to configure access to the web portal through a TLS-encrypted channel, take the following steps:

1. In Report Server Configuration Manager, in the **Connect** pane, select **Web Portal URL**.

1. Select **Advanced**.  
  
1. Under **Multiple HTTPS Identities for the currently Reporting Services feature**, select **Add**.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/add-https-identities.png" alt-text="Screenshot of the Advanced Multiple Web Site Configuration page, with the Add button highlighted for multiple HTTPS identities.":::

1. In the **Certificate** list, select the certificate you installed, and then select **OK**.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/select-web-portal-url-certificate.png" alt-text="Screenshot of the Add a Report Server HTTPS Binding window. The OK button and a certificate in the Certificate list are highlighted.":::

   Messages in the **Results** window indicate the progress of the configuration tool.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/reserve-web-portal-url-results.png" alt-text="Screenshot of the Results window in the configuration tool. Messages indicate a successful certificate binding for the web portal.":::

1. Under **Web Portal Site Identification**, select the URL that you reserved. If prompted, enter credentials for your report server. A browser window opens and displays the SSRS home page.

   :::image type="content" source="media/configure-ssl-connections-on-a-native-mode-report-server/access-web-portal.png" alt-text="Screenshot of a browser window that shows the main folder of SSRS.":::

## How certificate bindings are stored  

Certificate bindings are stored in the HTTP.sys repository. A representation of the bindings that you define are stored in the **URLReservations** section of the RSReportServer.config file.

The settings in the configuration file are only a representation of actual values that are specified elsewhere. Don't modify the values in the configuration file directly.

The configuration settings appear in the file only after you use Report Server Configuration Manager or the Report Server Windows Management Instrumentation (WMI) provider to bind a certificate.
  
> [!NOTE]  
> If you configure a binding with a TLS certificate in SSRS and you later want to remove the certificate from the computer, remove the binding from SSRS before you remove the certificate from the computer. Otherwise, it's not possible to remove the binding by using Report Server Configuration Manager or WMI, and you receive an "Invalid parameter" message.
>
> After you remove the certificate from the computer, you can use the HttpCfg.exe tool to remove the binding from the HTTP.sys repository. For more information, see [HttpCfg.exe](/windows/win32/http/httpcfg-exe).
  
TLS bindings are a shared resource in Windows. When you use Report Server Configuration Manager or other tools like IIS Manager to change the bindings, those changes can affect other applications on the same computer.

The best practice is to use the same tool to create and edit the bindings. For example, if you create TLS bindings by using Report Server Configuration Manager, also use Report Server Configuration Manager to manage the lifecycle of the bindings. If you use IIS manager to create bindings, use IIS manager to manage the lifecycle of the bindings.

If you install IIS on your computer before you install SSRS, it's a good practice to review the TLS configuration in IIS before you configure SSRS.
  
## Related content

- [Authentication in a report server](../../reporting-services/security/authentication-with-the-report-server.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [Configure report server URLs (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)
