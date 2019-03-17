---
title: "Configure a Report Server for E-Mail Delivery (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], distributing"
  - "report servers [Reporting Services], e-mail delivery"
  - "remote SMTP service [Reporting Services]"
  - "distributing reports [Reporting Services], e-mail"
  - "CDO"
  - "Collaboration Data Objects"
  - "SMTP settings [Reporting Services]"
  - "e-mail [Reporting Services]"
  - "sending reports"
  - "mail [Reporting Services]"
  - "local SMTP service [Reporting Services]"
ms.assetid: b838f970-d11a-4239-b164-8d11f4581d83
author: markingmyname
ms.author: maghan
manager: craigg
---
# Configure a Report Server for E-Mail Delivery (SSRS Configuration Manager)


  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes an e-mail delivery extension so that you can distribute reports through e-mail. Depending on how you define the e-mail subscription, a delivery might consist of a notification, link, attachment, or embedded report. The e-mail delivery extension works with your existing mail server technology. The mail server must be an SMTP server or forwarder. The report server connects to an SMTP server through Collaboration Data Objects (CDO) libraries (cdosys.dll) that are provided by the operating system.  
  
 The report server e-mail delivery extension is not configured by default. You must use the Reporting Services Configuration Manager to minimally configure the extension. To set advanced properties, you must edit the `RSReportServer.config` file. If you cannot configure the report server to use this extension, you can deliver reports to a shared folder instead. For more information, see [File Share Delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md).  
  
||  
|-|  
|[!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode|  
  
 
  
##  <a name="bkmk_configuration_requirements"></a> Configuration Requirements  
  
-   Report server e-mail delivery is implemented on Collaboration Data Objects (CDO) and requires a local or remote Simple Mail Transfer Protocol (SMTP) server or SMTP forwarder. SMTP is not supported on all Windows operating systems. If you are using the Itanium-based edition of Windows Server 2008, SMTP is not supported. For more information about configuration options provided through CDO, see [Configuration CoClass](https://go.microsoft.com/fwlink/?LinkId=98237) on MSDN.  
  
-   The Report Server service account must have permission on the SMTP server to send mail.  
  
-   The e-mail delivery extension uses UTF-8 encoding in e-mail attachments. You cannot modify the encoding; the HTML rendering extension only supports UTF-8.  
  
> [!NOTE]  
>  The default e-mail delivery extension does not provide support for digitally signing or encrypting outgoing mail messages.  
  
 
  
##  <a name="bkmk_configure_for_local_or_remote_SMTP"></a> Configuring a Report Server for Local or Remote SMTP Service  
 You can use a local SMTP service or a remote SMTP server or forwarder to support e-mail delivery. If you have access to an existing remote SMTP server, you should consider using it. If there is no SMTP server available or if you subsequently encounter report delivery errors that can be attributed to computer connection failures, you should switch to using a local SMTP service. Details about how to configure a report server for local or remote service are provided further on in this topic.  
  
  
  
##  <a name="bkmk_setting_email_delivery"></a> Setting Configuration Options for E-Mail Delivery  
 Before you can use Report Server e-mail delivery, you must set configuration values that provide information about which SMTP server to use.  
  
 To configure a report server for e-mail delivery, do the following:  
  
-   Use the Reporting Services Configuration Manager if you are specifying just an SMTP server and a user account that has permission to send e-mail. These are the minimum settings that are required for configuring the Report Server e-mail delivery extension. For more information, see [E-mail Settings - Configuration Manager &#40;SSRS Native Mode&#41;](../../reporting-services/install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md) and [E-Mail Delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md).  
  
-   (Optionally) Use a text editor to specify additional settings in the RSreportserver.config file. This file contains all of the configuration settings for Report Server e-mail delivery. Specifying additional settings in these files is required if you are using a local SMTP server or if you are restricting e-mail delivery to specific hosts. For more information about finding and modifying configuration files, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md) in SQL Server Books Online.  
  
> [!NOTE]  
>  Report server e-mail settings are based on CDO. If you want more detail about specific settings, you can refer to the CDO production documentation.  
  

  
##  <a name="bkmk_example_config_file"></a> Example Report Server E-Mail Configuration  
 The following example illustrates the settings in the RSreportserver.config file for a remote SMTP server. To read about the setting descriptions and valid values, see [RSReportServer Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online or the CDO product documentation.  
  
```  
<RSEmailDPConfiguration>  
     <SMTPServer>mySMTPServer.Adventure-Works.com</SMTPServer>  
     <SMTPServerPort></SMTPServerPort>  
     <SMTPAccountName></SMTPAccountName>  
     <SMTPConnectionTimeout></SMTPConnectionTimeout>  
     <SMTPServerPickupDirectory></SMTPServerPickupDirectory>  
     <SMTPUseSSL></SMTPUseSSL>  
     <SendUsing>2</SendUsing>  
     <SMTPAuthenticate></SMTPAuthenticate>  
     <From>my-rs-email-account@Adventure-Works.com</From>  
     <EmbeddedRenderFormats>  
          <RenderingExtension>MHTML</RenderingExtension>  
     </EmbeddedRenderFormats>  
     <PrivilegedUserRenderFormats></PrivilegedUserRenderFormats>  
     <ExcludedRenderFormats>  
          <RenderingExtension>HTMLOWC</RenderingExtension>  
          <RenderingExtension>NULL</RenderingExtension>  
     </ExcludedRenderFormats>  
     <SendEmailToUserAlias>True</SendEmailToUserAlias>  
     <DefaultHostName></DefaultHostName>  
     <PermittedHosts>  
          <HostName>Adventure-Works.com</HostName>  
          <HostName>hotmail.com</HostName>  
     </PermittedHosts>  
</RSEmailDPConfiguration>  
```  
  

  
##  <a name="bkmk_setting_TO_field"></a> Configuration Options for Setting the To: Field in a Message  
 User-defined subscriptions that are created according to the permissions granted by the **Manage individual subscriptions** task contain a pre-set user name that is based on the domain user account. When the user creates the subscription, the recipient name in the **To:** field is self-addressed using the domain user account of the person creating the subscription.  
  
 If you are using an SMTP server or forwarder that uses e-mail accounts that are different from the domain user account, the report delivery will fail when the SMTP server tries to deliver the report to that user.  
  
 To workaround this issue, you can modify configuration settings that allow users to enter a name in the **To:** field:  
  
1.  Open RSReportServer.config with a text editor.  
  
2.  Set `SendEmailToUserAlias` to `False`.  
  
3.  Set `DefaultHostName` to the Domain Name System (DNS) name or IP address of the SMTP server or forwarder.  
  
4.  Save the file.  
  
  
  
##  <a name="bkmk_options_remote_SMTP"></a> Configuration Options for Remote SMTP Service  
 The connection between the report server and an SMTP server or forwarder is determined by the following configuration settings:  
  
-   `SendUsing` specifies a method for sending messages. You can choose between a network SMTP service or a local SMTP service pickup directory. To use a remote SMTP service, this value must be set to **2** in the RSReportServer.config file.  
  
-   `SMTPServer` specifies the remote SMTP server or forwarder. This value is a required value if you are using a remote SMTP server or forwarder.  
  
-   `From` sets the value that appears in the **From:** line of an e-mail message. This value is a required value if you are using a remote SMTP server or forwarder.  
  
 Other values that are used for remote SMTP service include the following (note that you do not need to specify these values unless you want to override the default values).  
  
-   **SMTPServerPort** is configured for port 25.  
  
-   **SMTPAuthenticate** specifies how the report server connects to the remote SMTP server. The default value is 0 (or no authentication). In this case, the connection is made through Anonymous access. Depending on your domain configuration, the report server and the SMTP server may need to be members of the same domain.  
  
     To send e-mail to restricted distribution lists (for example, distribution lists that accept incoming messages only from authenticated accounts), set **SMTPAuthenticate** to **2**.  
  

  
##  <a name="bkmk_options_local_SMTP"></a> Configuration Options for Local SMTP Service  
 Configuring a local SMTP service is useful if you are testing or troubleshooting report server e-mail delivery. The local SMTP service is not enabled by default. For instructions on how to enable it, see [Configure a Report Server for E-Mail Delivery (SSRS Configuration Manager)](../../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md) and [E-mail Settings - Configuration Manager &#40;SSRS Native Mode&#41;](../../reporting-services/install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md).  
  
 The connection between the report server and a local SMTP server or forwarder is determined by the following configuration settings:  
  
-   `SendUsing` is set to **1**.  
  
-   **SMTPServerPickupDirectory** is set to a folder on the local drive.  
  
    > [!NOTE]  
    >  Be sure that you do not set `SMTPServer` if you are using a local SMTP server.  
  
-   `From` sets the value that appears in the **From:** line of an e-mail message. This value is required.  
  
 
  
##  <a name="bkmk_use_configuration_manager"></a> To configure report server e-mail using the Reporting Services Configuration Manager  
  
1.  Verify that the Report Server Windows service has `Send As` permissions on the SMTP server.  
  
2.  Start the Reporting Services Configuration Manager and connect to the report server instance.  
  
3.  On the Email Settings page, enter the name of the SMTP server. This value can be an IP address, a UNC name of a computer on your corporate intranet, or a fully qualified domain name.  
  
4.  In **Sender Address**, enter the name an account that has permission to send e-mail from the SMTP server.  
  
5.  Click **Apply**.  
  

  
##  <a name="bkmk_confiugre_remote_SMTP"></a> To configure a remote SMTP Service for the report server  
  
1.  Verify that the Report Server Windows service has `Send As` permissions on the SMTP server.  
  
2.  Open the RSReportServer.config file in a text editor.  
  
3.  Verify that <`UrlRoot`> is set to the report server URL address. This value is set when you configure the report server and it should be filled in already. If it is not set, type the report server URL address.  
  
4.  In the Delivery section, find <`ReportServerEmail`>.  
  
5.  In <`SMTPServer`>, type the name of the SMTP server. This value can be an IP address, a UNC name of a computer on your corporate intranet, or a fully qualified domain name.  
  
6.  Verify that <`SendUsing`> is set to 2. If it is set another value, the report server is not configured to use a remote SMTP service.  
  
7.  In <`From`>, type the name an account that has permission to send e-mail from the SMTP server.  
  
8.  Save the file.  
  
     The report server will use the new settings automatically; you do not need to restart the service. You can specify additional SMTP settings to further configure how the SMTP server is used for report server e-mail delivery. For more information, see [Configuring a Report Server for E-Mail Delivery](../../../2014/sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md) and [RSReportServer Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  

  
##  <a name="bkmk_confiugre_local_SMTP"></a> To configure a local SMTP Service for the report server  
  
1.  In Control Panel, click **Add or Remove Programs**.  
  
2.  Click **Add/Remove Windows Components** to start the Windows Component Wizard.  
  
3.  Select **Application Server** and click **Details**.  
  
4.  Select **Internet Information Services (IIS)** and click **Details**.  
  
5.  Select the **SMTP Service** checkbox and click **OK**.  
  
6.  On the Windows Component Wizard, click **Next**. Click **Finish**.  
  
7.  Verify that the service is running in the **Services** console.  
  
8.  Open the **RSReportServer.config** file in a text editor.  
  
9. Verify that `<UrlRoot>` is set to the report server URL address. This value is set when you configure the report server and it should be filled in already. If it is not set, type the report server URL address.  
  
10. In the Delivery section, find `<ReportServerEmail>.`  
  
11. In `<SMTPServer>`, clear any values for this setting, but do not delete the tags.  
  
12. Set `<SendUsing>` to 1. If it is set another value, the report server is not configured to use a local SMTP service.  
  
13. Set `<SMTPServerPickupDirectory>` to a folder on the local drive.  
  
14. Set `<From>` to an account that has permission to send e-mail from the SMTP server.  
  
15. Save the file.  
  
 
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md)  
  
  
