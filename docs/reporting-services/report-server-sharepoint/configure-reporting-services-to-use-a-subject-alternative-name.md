---
title: "Configure Reporting Services to use a Subject Alternative Name (SAN) | Microsoft Docs"
description: Learn how to configure SQL Server Reporting Services and Power BI Report Server to use a SAN by modifying the rsreportserver.config file and using the Netsh.exe tool.
ms.date: 06/30/2021
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
---
# Configure Reporting Services to use a Subject Alternative Name (SAN)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

This topic explains how to configure Reporting Services (SSRS) and Power BI Report Server to use a Subject Alternative Name (SAN), by modifying the rsreportserver.config file and using the Netsh.exe tool.

The instructions apply to the Web Service URL as well as the Web Portal URL in the Report Server Configuration Manager tool.

To use a SAN, the TLS/SSL certificate must be registered on the server, signed, and have the private key. You cannot use a self-signed certificate.

URLs in Reporting Services and Power BI Report Server can be configured to use a TLS/SSL certificate. A certificate normally has just a subject name, which allows only one URL for a Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), session. The SAN is an additional field in the certificate that allows a TLS service to listen for many URLs, and to share the TLS port with other applications. For example, a SAN could look something like `www.myreports.com`.

For more information about TLS settings for Reporting Services, see [Configure TLS Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).  
  
## Configure to use a Subject Alternative Name for Web Service URL
  
1. Start Report Server Configuration Manager.  
  
     For more information, see [Report Server Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).  
  
2. On the **Web Service URL** page, select a TLS/SSL port and TLS/SSL Certificate.  
  
     ![Report Server Configuration Manager](../../reporting-services/report-server-sharepoint/media/reportingservices-configurationmanager.png "Report Server Configuration Manager")  
  
     The configuration manager registers the TLS/SSL certificate for the port.  
  
3. Open the rsreportserver.config file.  
  
     For SSRS 2016 Native mode, the file is located by default in the following folder:  
  
    ```  
    \Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\ReportServer  
    ```  
  
     For SSRS 2017 and later, the file is located by default in the following folder:  
  
    ```  
    \Program Files\Microsoft SQL Server Reporting Services\SSRS\ReportServer  
    ```  
    
     For Power BI Report Server, the file is located by default in the following folder:  
  
    ```  
    \Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer  
    ```  
  
4. Copy the URL section for the **ReportServerWebService** application.
  
     For example, the following original URL section is:  
  
    ```  
        <URL>  
         <UrlString>https://+:443</UrlString>  
         <AccountSid>S-1-5-80-2885764129-887777008-271615777-1616004480-2722851051</AccountSid>  
         <AccountName>NT Service\ReportServer</AccountName>  
        </URL>  
  
    ```  
  
     The following modified URL section is:
  
    ```  
    <URL>  
         <UrlString>https://+:443</UrlString>  
         <AccountSid>S-1-5-80-2885764129-887777008-271615777-1616004480-2722851051</AccountSid>  
         <AccountName>NT Service\ReportServer</AccountName>  
        </URL>  
        <URL>  
         <UrlString>https://www.myreports.com:443</UrlString>  
         <AccountSid>S-1-5-80-2885764129-887777008-271615777-1616004480-2722851051/AccountSid>  
         <AccountName>NT Service\ReportServer</AccountName>  
        </URL>  
  
    ```  
  
    > [!TIP]  
    >  * For SSRS 2017 and later, the `AccountSid` value is `S-1-5-80-4050220999-2730734961-1537482082-519850261-379003301` and the `AccountName` value is `NT SERVICE\SQLServerReportingServices`.
    >  * For Power BI Report Server, the `AccountSid` value is `S-1-5-80-1730998386-2757299892-37364343-1607169425-3512908663` and the `AccountName` value is `NT SERVICE\PowerBIReportServer`.

5. Repeat this process for the **ReportServerWebApp** URL section.
5. Save the rsreportserver.config file.  
  
6. Start a command prompt using **Run as Administrator**.
8. Show the existing urlacls by typing the following:
  
    ```  
    Netsh http show urlacl  
    ```  
  
     An entry such as the following appears.  
  
    ```  
    Reserved URL            : https://+:443/ReportServer/  
        User: NT SERVICE\ReportServer  
            Listen: Yes  
            Delegate: No  
            SDDL: D:(A;;GX;;;S-1-5-80-2885764129-887777008-271615777-1616004480-2722851051)  
    ```  
  
     An urlacl is a DACL (Discretionary Access Control List) for a reserved URL.  
  
9. Create a new entry for the Subject Alternative Name, with the same user and SDDL as the existing entry, by typing the following:  
  
    ```  
    netsh http add urlacl  url=https://www.myreports.com:443/ReportServer    
    user="NT Service\ReportServer" sddl=D:(A;;GX;;;S-1-5-80-2885764129-887777008-271615777-1616004480-2722851051)  
  
    ```  
    > [!TIP]
    > If you copy the code to Notepad to edit, rather than typing it manually, remove the CRLF before pasting the code into the command prompt.

10. For the **Web Portal URL**, create a new entry for the Subject Alternative Name by typing the following:

    ```  
    netsh http add urlacl  url=https://www.myreports.com:443/Reports  
    user="NT Service\ReportServer" sddl=D:(A;;GX;;;S-1-5-80-2885764129-887777008-271615777-1616004480-2722851051)  
  
    ```  
    > [!TIP]  
    >  * For SSRS 2017 and later, the `user` value is `NT SERVICE\SQLServerReportingServices` and the `sddl` value is `D:(A;;GX;;;S-1-5-80-4050220999-2730734961-1537482082-519850261-379003301)`.
    >  * For Power BI Report Server, the `user` value is `NT SERVICE\PowerBIReportServer` and the `sddl` value is `S-1-5-80-1730998386-2757299892-37364343-1607169425-3512908663`

    > [!NOTE]  
    > For Power BI Report Server, you need to create two additional entries for the Subject Alternative Name by typing the following:
    >  * `add urlacl url=https://www.myreports.com:443/PowerBI user="NT SERVICE\PowerBIReportServer" sddl=D:(A;;GX;;;S-1-5-80-1730998386-2757299892-37364343-1607169425-3512908663)`
    >  * `add urlacl url=https://www.myreports.com:443/wopi user="NT SERVICE\PowerBIReportServer" sddl=D:(A;;GX;;;S-1-5-80-1730998386-2757299892-37364343-1607169425-3512908663)`

11. On the **Report Server Status** page of the Report Server Configuration Manager, Click **Stop** and then click **Start** to restart the report server.  
  
## See also

 [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
 [Report Server Configuration Manager](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Modify a Reporting Services configuration file](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)   
 [Configure Report Server URLs](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)