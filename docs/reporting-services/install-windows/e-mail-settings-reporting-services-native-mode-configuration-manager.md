---
title: "E-Mail Settings - Reporting Services Native mode (Configuration Manager) | Microsoft Docs"
ms.date: 06/01/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"

ms.topic: conceptual
f1_keywords: 
  - "SQL13.rsconfigtool.emailsettings.F1"
helpviewer_keywords: 
  - "SQL11.rsconfigtool.emailsettings.F1"
ms.assetid: cdad1529-bfa6-41fb-9863-d9ff1b802577
author: maggiesMSFT
ms.author: maggies
---
# E-Mail Settings - Reporting Services Native mode (Configuration Manager)
Reporting Services includes an e-mail delivery extension so that you can distribute reports through e-mail. Depending on how you define the e-mail subscription, a delivery might consist of a notification, link, attachment, or embedded report. The e-mail delivery extension works with your existing mail server technology. The mail server must be an SMTP server or forwarder. The report server connects to an SMTP server through Collaboration Data Objects (CDO) libraries (cdosys.dll) that are provided by the operating system.

The report server e-mail delivery extension is not configured by default. You must use the Reporting Services Configuration Manager to minimally configure the extension. To set advanced properties, you must edit the RSReportServer.config file. If you cannot configure the report server to use this extension, you can deliver reports to a shared folder instead. For more information, see File Share Delivery in Reporting Services.

## Configuration Requirements

- Report server e-mail delivery is implemented on Collaboration Data Objects (CDO) and requires a local or remote Simple Mail Transfer Protocol (SMTP) server or SMTP forwarder. SMTP is not supported on all Windows operating systems. If you are using the Itanium-based edition of Windows Server 2008, SMTP is not supported. For more information about configuration options provided through CDO, see [Configuration CoClass](https://go.microsoft.com/fwlink/?LinkId=98237) on MSDN.

The configured authentication account must have permission on the SMTP server to send mail.

- The e-mail delivery extension uses UTF-8 encoding in e-mail attachments. You cannot modify the encoding; the HTML rendering extension only supports UTF-8.

> [!NOTE] 
> The default e-mail delivery extension does not provide support for digitally signing or encrypting outgoing mail messages.

## Setting Configuration Options for E-Mail Delivery
Before you can use Report Server e-mail delivery, you must set configuration values that provide information about which SMTP server to use.

To configure a report server for e-mail delivery, do the following:

- Use the Reporting Services Configuration Manager if you are specifying just an SMTP server and a user account that has permission to send e-mail. These are the minimum settings that are required for configuring the Report Server e-mail delivery extension.

- (Optionally) Use a text editor to specify additional settings in the RSreportserver.config file. This file contains all of the configuration settings for Report Server e-mail delivery. Specifying additional settings in these files is required if you are using a local SMTP server or if you are restricting e-mail delivery to specific hosts. For more information about finding and modifying configuration files, see [Modify a Reporting Services Configuration File (RSreportserver.config)](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md) in SQL Server Books Online.

> [!NOTE] 
> Report server e-mail settings are based on CDO. If you want more detail about specific settings, you can refer to the CDO production documentation.

## <a name="rsconfigman"/>Configure report server e-mail using the Reporting Services Configuration Manager

1. Start the Reporting Services Configuration Manager and connect to the report server instance.

2. In **Sender Address**, enter the e-mail address to use in the **From:** field of a generated e-mail. 

     You must specify a user account that has permission to send mail from the SMTP server. The value you type for the **Sender Address** is saved in the `<From>` field in the rsreportserver.config file.  

3.  In **SMTP Server**, specify the SMTP server or gateway to use. 

     This value can be an IP address, a NetBIOS name of a computer on your corporate intranet, or a fully qualified domain name. The value you type for the **SMTP Server** is saved in the `<SMTPServer>` field in the rsreportserver.config file.

4. Use the **Authentication** drop down to specify how to authentication to the SMTP server. This 

     - **No authentication** means you will connect anonymously to the mail server that was specified.
     
          Selecting this option will set `<SendUsing>` to a value of **2** and `<SMTPAuthenticate>` to a value of **0** in the rsreportserver.config.
     
     - **Username and password (Basic)** allows you to specify a username and password to connect to the mail server. You can also select **Use secure connection** to have this go over an encrypted connection to your mail server.
     
          Selecting this option will set `<SendUsing>` to a value of **2** and `<SMTPAuthenticate>` to a value of **1** in the rsreportserver.config. Selecting **Use secure connection** will set `SMTPUseSSL` to **True**. **Username** will be set in `<SendUserName>` as an encrypted value. **Password** will be set in `<SendPassword>` as an encrypted value.
     
     - **Report server service account (NTLM)** will use the service account you specified for the report server. If using the report server service account for authentication, verify that the service account has **Send As** permissions on the SMTP server.
     
          Selecting this option will set `<SendUsing>` to a value of **2** and `<SMTPAuthenticate>` to a value of **2** in the rsreportserver.config.

5. Select **Apply**.

6. You can optionally adjust additional fields, for the email configuration, within the rsreportserver.config.

## Example Report Server E-Mail Configuration
The following example illustrates the settings in the RSreportserver.config file for a remote SMTP server. To read about the setting descriptions and valid values, see [Rsreportserver.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md) in SQL Server Books Online.

```
<RSEmailDPConfiguration>
     <SMTPServer>mySMTPServer.Adventure-Works.com</SMTPServer>
     <SMTPServerPort></SMTPServerPort>
     <SMTPAccountName></SMTPAccountName>
     <SMTPConnectionTimeout></SMTPConnectionTimeout>
     <SMTPServerPickupDirectory></SMTPServerPickupDirectory>
     <SMTPUseSSL>False</SMTPUseSSL>
     <SendUsing>2</SendUsing>
     <SMTPAuthenticate>2</SMTPAuthenticate>
     <From>my-rs-email-account@Adventure-Works.com</From>
     <EmbeddedRenderFormats>
          <RenderingExtension>MHTML</RenderingExtension>
     </EmbeddedRenderFormats>
     <PrivilegedUserRenderFormats></PrivilegedUserRenderFormats>
     <ExcludedRenderFormats>
          <RenderingExtension>HTMLOWC</RenderingExtension>
          <RenderingExtension>NULL</RenderingExtension>
          <RenderingExtension>RGDI</RenderingExtension>
     </ExcludedRenderFormats>
     <SendEmailToUserAlias>True</SendEmailToUserAlias>
     <DefaultHostName></DefaultHostName>
     <PermittedHosts>
          <HostName>Adventure-Works.com</HostName>
          <HostName>hotmail.com</HostName>
     </PermittedHosts>
     <SendUserName></SendUserName>
     <SendPassword></SendPassword>
</RSEmailDPConfiguration>
```
## Configuration Options for Setting the To: Field in a Message
User-defined subscriptions that are created according to the permissions granted by the Manage individual subscriptions task contain a pre-set user name that is based on the domain user account. When the user creates the subscription, the recipient name in the **To:** field is self-addressed using the domain user account of the person creating the subscription.

If you are using an SMTP server or forwarder that uses e-mail accounts that are different from the domain user account, the report delivery will fail when the SMTP server tries to deliver the report to that user.

To workaround this issue, you can modify configuration settings that allow users to enter a name in the **To:** field:

1. Open RSReportServer.config with a text editor.

2. Set `<SendEmailToUserAlias>` to **False**.

3. Set `<DefaultHostName>` to the Domain Name System (DNS) name or IP address of the SMTP server or forwarder.

4. Save the file.

## Configuration Options for Remote SMTP Service
The connection between the report server and an SMTP server or forwarder is determined by the following configuration settings:

- `<SendUsing>` specifies a method for sending messages. You can choose between a network SMTP service or a local SMTP service pickup directory. To use a remote SMTP service, this value must be set to **2** in the RSReportServer.config file.
- `<SMTPServer>` specifies the remote SMTP server or forwarder. This value is a required value if you are using a remote SMTP server or forwarder.
- `<From>` sets the value that appears in the **From:** line of an e-mail message. This value is a required value if you are using a remote SMTP server or forwarder.

Other values that are used for remote SMTP service include the following (note that you do not need to specify these values unless you want to override the default values).

- `<SMTPServerPort>` is configured for port 25 by default.
- `<SMTPAuthenticate>` specifies how the report server connects to the remote SMTP server. The default value is **0** (or no authentication). In this case, the connection is made through Anonymous access. Depending on your domain configuration, the report server and the SMTP server may need to be members of the same domain.
- To send e-mail to restricted distribution lists (for example, distribution lists that accept incoming messages only from authenticated accounts), set `<SMTPAuthenticate>` to **1** or **2**. If you set it to **1**, you will also need to set `<SendUserName>` and `<SendPassword>`. It is recommended to do this through the Reporting Services Configuration manager as it will encrypt the values for `<SendUserName>` and `<SendPassword>`.

### To configure a remote SMTP Service for the report server

> [!NOTE] 
> It is recommended that you configure the mail server through the Reporting Services Configuration Manager.

1. Verify that the Report Server Windows service has **Send As** permissions on the SMTP server.

2. Open the RSReportServer.config file in a text editor.

3. Verify that `<UrlRoot>` is set to the report server URL address. This value is set when you configure the report server and it should be filled in already. If it is not set, type the report server URL address.

4. In the Delivery section, find `<RSEmailDPConfiguration>`.
     
5. In `<SMTPServer>`, type the name of the SMTP server. This value can be an IP address, a UNC name of a computer on your corporate intranet, or a fully qualified domain name.

6. Set `<SendUsing>` to a value of **2** to use the service account for the report server. Set `<SendUsing>` to a value of **1** for basic authentication. If you set it to **1**, you will need to additionally supply a value for `<SendUserName>` and `<SendPassword>`. If you want those values to be encrypted, set the authentication within the Reporting Services Configuration Manager.

7. Set `<SMTPAuthenticate>` to a value of **1** if you set `<SendUsing>` to either 1 or 2.

7. Set `<From>`. You must specify a user account that has permission to send mail from the SMTP server.

8. Save the file.

     The report server will use the new settings automatically; you do not need to restart the service. You can specify additional SMTP settings to further configure how the SMTP server is used for report server e-mail delivery.

## Configuration Options for Local SMTP Service
Configuring a local SMTP service is useful if you are testing or troubleshooting report server e-mail delivery. The local SMTP service is not enabled by default.

The connection between the report server and a local SMTP server or forwarder is determined by the following configuration settings:

- **SendUsing** is set to **1**.
- **SMTPServerPickupDirectory** is set to a folder on the local drive.

  > [!NOTE] 
  > Be sure that you do not set SMTPServer if you are using a local SMTP server.

- **From** sets the value that appears in the **From:** line of an e-mail message. This value is required.

### To configure a local SMTP Service for the report server

1. In Control Panel, select **Turn Windows features on or off** to start the **Add Roles and Features Wizard**.

2. Select **Role-based or feature-based installation** and select **Next**.

3. Select the server to install Internet Information Server (IIS) onto and select **Next**.

4. Select **Next** on the *Server Roles* page.
     
5. On the *Features* page, select **SMTP Server** and then select **Next**.

     If you are prompted to add features that are required for SMTP Server, select **Add Features**.

6. Select **Next** on the *Web Server Role (IIS)* page.

7. Select **Next** on the *Role Services* page.

8. Select **Install** on the **Confirmation** page.

9. Verify that the **Simple Mail Transfer Protocol (SMTP)** windows service is running in the Services console.

     To configure the local SMTP server, you will need to use the IIS 6.0 Manager under Admin tools.

10. Open the RSReportServer.config file in a text editor.

11. Verify that `<UrlRoot>` is set to the report server URL address. This value is set when you configure the report server and it should be filled in already. If it is not set, type the Web Service URL address for your report server.

12. In the Delivery section, find `<RSEmailDPConfiguration>`.
     
13. Make sure `<SMTPServer>` is present, but empty.
     
14. Set `<SendUsing>` to 1.
     
14. Set `<SMTPAuthenticate>` to 0.
     
15. Set `<SMTPServerPickupDirectory>` to the SMTP Service Pickup folder.
     
     Default location will be *C:\inetpub\mailroot\Pickup*.
     
16. Set `<From>`. This sets the value that appears in the **From:** line of an e-mail message.
     
17. Save the file.
  
## See Also  
[Reporting Services Configuration Manager (Native Mode)](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)  
[Modify a Reporting Services Configuration File (rsreportserver.config)](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)  
[Rsreportserver.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
  
  
