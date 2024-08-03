---
title: "Email settings in SSRS native mode (Report Server Configuration Manager)"
description: SQL Server Reporting Services includes an email delivery extension so that you can distribute reports through email.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/05/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "SQL13.rsconfigtool.emailsettings.F1"
helpviewer_keywords:
  - "SQL11.rsconfigtool.emailsettings.F1"
#customer intent: As a SQL Server administrator, I want to configure email settings in SSRS so that I can enable report distribution via email.
---
# Email settings in Reporting Services native mode (Report Server Configuration Manager)

Learn how SQL Server Reporting Services (SSRS) includes an email delivery extension so that you can distribute reports through email. Depending on the email subscription configuration, a delivery might consist of a notification, link, attachment, or embedded report. The email delivery extension works with your existing mail server technology, which must be a Simple Mail Transfer Protocol (SMTP) server or forwarder. The report server connects to an SMTP server through Collaboration Data Objects (CDO) libraries (`cdosys.dll`) provided by the operating system.

The report server email delivery extension isn't configured by default. You use the Report Server Configuration Manager to minimally configure the extension. To set advanced properties, edit the `RSReportServer.config` file. If you can't configure the report server to use this extension, you can deliver reports to a shared folder instead. For more information, see [File share delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md).

## Configuration requirements

Report server email delivery is implemented on Collaboration Data Objects (CDO) and requires a local or remote SMTP server or SMTP forwarder. SMTP isn't supported on all Windows operating systems. If you use the Itanium-based edition of Windows Server 2008, SMTP isn't supported. For more information about configuration options provided through CDO, see [Configuration CoClass](/previous-versions/office/developer/exchange-server-2007/aa579722(v=exchg.80)).

The configured authentication account must have permission on the SMTP server to send mail. The email delivery extension uses UTF-8 encoding in email attachments. You can't modify the encoding and the HTML rendering extension only supports UTF-8.

> [!NOTE]
> The default email delivery extension doesn't support digitally signing or encrypting outgoing mail messages.

## Set configuration options for email delivery

Before you can use Report Server email delivery, you must set configuration values that provide information about which SMTP server to use. The following table describes the two ways you can configure a report server for delivery:

|Method|Description|
|------|-----------|
|Report Server Configuration Manager| Use the Report Server Configuration Manager if you're specifying just an SMTP server and a user account that has permission to send email. These settings are the minimum required for configuring the Report Server email delivery extension.|
|`rsreportserver.config` file|This file contains all of the configuration settings for report server email delivery. Use a text editor to configure extra settings if you're using a local SMTP server or if you're restricting email delivery to specific hosts. For more information about finding and modifying configuration files, see [Modify a Reporting Services configuration file (`rsreportserver.config`)](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md).

> [!NOTE]
> Report server email settings are based on CDO. For more detail about specific settings, refer to the CDO production documentation.

## <a name="rsconfigman"></a> Configure report server email by using the Report Server Configuration Manager

1. Start the Report Server Configuration Manager and connect to the report server instance.
1. Go to **E-mail Settings**. 
1. In **Sender Address**, enter the email address to use in the **From:** field of a generated email. Specify a user account that has permission to send mail from the SMTP server. The value you enter for the **Sender Address** is saved in the `<From>` field in the `rsreportserver.config` file.
1. In **SMTP Server**, specify the SMTP server or gateway to use. This value can be:
   - An IP address
   - A NetBIOS name of a computer on your corporate intranet
   - A fully qualified domain name
      The value you type for the **SMTP Server** is saved in the `<SMTPServer>` field in the `rsreportserver.config` file.
1. In the **Authentication** list, specify how to authentication to the SMTP server.
   
   |Authentication type|Description|
   |-------------------|-----------|
   |**No authentication**|Connects anonymously to the mail server. <br><br>This option sets `<SendUsing>` to a value of **2** and `<SMTPAuthenticate>` to a value of **0** in the `rsreportserver.config` file.|
   |**Username and password (Basic)**|Allows you to specify a username and password to connect to the mail server. You can also select **Use secure connection** to have this authentication go over an encrypted connection to your mail server. <br><br>This option sets `<SendUsing>` to a value of **2** and `<SMTPAuthenticate>` to a value of **1** in the `rsreportserver.config` file. Selecting **Use secure connection** sets `SMTPUseSSL` to **True**. **Username** is set in `<SendUserName>` as an encrypted value. **Password** is set in `<SendPassword>` as an encrypted value.|
   |**Report server service account (NTLM)**|Uses the service account you specified for the report server. If using the report server service account for authentication, verify that the service account has **Send As** permissions on the SMTP server.<br><br>This option sets `<SendUsing>` to a value of **2** and `<SMTPAuthenticate>` to a value of **2** in the `rsreportserver.config` file.|
1. Select **Apply**.

If you want to adjust other fields for the email configuration, use the `rsreportserver.config` file as described in the following sections.

## Example report server email configuration in `rsreportserver.config`

The following example illustrates the settings in the `rsreportserver.config` file for a remote SMTP server. For more information about the setting descriptions and valid values, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).

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

## Configuration options for setting the To: field in a message

User-defined subscriptions that are created based on permissions granted by the Manage individual subscriptions task contain a preset user name based on the domain user account. When the user creates the subscription, the recipient name in the **To:** field is self-addressed with the domain user account of the person creating the subscription.

If you use an SMTP server or forwarder with email accounts that are different from the domain user account, the report delivery fails when the SMTP server tries to deliver the report.

To work around this issue, modify configuration settings that allow users to enter a name in the **To:** field:

1. Open `RSReportServer.config` with a text editor.
1. Set `<SendEmailToUserAlias>` to **False**.
1. Set `<DefaultHostName>` to the Domain Name System (DNS) name or IP address of the SMTP server or forwarder.
1. Save the file.

## Configuration options for remote SMTP service

The following configuration settings determine the connection between the report server and an SMTP server or forwarder:

|Setting|Description|
|-------|-----------|
|`<SendUsing>`|Specifies a method for sending messages. You can choose between a network SMTP service or a local SMTP service pickup directory. To use a remote SMTP service, set this value to **2** in the `RSReportServer.config` file.|
|`<SMTPServer>`|Specifies the remote SMTP server or forwarder. This value is a required value if you're using a remote SMTP server or forwarder.|
|`<From>`|Sets the value that appears in the **From:** line of an email message. This value is a required value if you use a remote SMTP server or forwarder.|

The following table shows the other values that are used for remote SMTP service:

|Setting|Description|
|-------|-----------|
|`<SMTPServerPort>`|Configured for port 25 by default.|
|`<SMTPAuthenticate>`|Specifies how the report server connects to the remote SMTP server. The default value is **0** (or no authentication). In this case, the connection is made through Anonymous access. Depending on your domain configuration, the report server and the SMTP server might need to be members of the same domain.<br><br>To send email to restricted distribution lists (for example, distribution lists that accept incoming messages only from authenticated accounts), set `<SMTPAuthenticate>` to **1** or **2**. If you set it to **1**, you also need to set `<SendUserName>` and `<SendPassword>`. The best practice is to change this setting through the Report Server Configuration Manager as it encrypts the values for `<SendUserName>` and `<SendPassword>`.|

> [!NOTE]
> You don't need to specify these values unless you want to override the default values.

### Configure a remote SMTP service for the report server

> [!NOTE]
> The best practice is to configure the mail server through the Report Server Configuration Manager.

1. Verify that the Report Server Windows service has **Send As** permissions on the SMTP server.
1. Open the `RSReportServer.config` file in a text editor.
1. Verify that `<UrlRoot>` is set to the report server URL address. This value is set when you configure the report server and it should be filled in already. If it isn't set, enter the report server URL address.
1. Set `<SMTPServer>` to the name of the SMTP server. This value can be an IP address, a Universal Naming Convention (UNC) name of a computer on your corporate intranet, or a fully qualified domain name.
1. Set `<SendUsing>` to a value of **2** to use the service account for the report server. Set `<SendUsing>` to a value of **1** for basic authentication. If you set it to **1**, you need to also supply a value for `<SendUserName>` and `<SendPassword>`. If you want those values to be encrypted, set the authentication within the Report Server Configuration Manager.
1. Set `<SMTPAuthenticate>` to a value of **1** if you set `<SendUsing>` to either 1 or 2.
1. Set `<From>` to a user account that has permission to send mail from the SMTP server.
1. Save the file.

The report server uses the new settings automatically. You don't need to restart the service. You can specify other SMTP settings to further configure how the SMTP server is used for report server email delivery.

## Configuration options for local SMTP service

Configuring a local SMTP service is useful if you're testing or troubleshooting report server email delivery. The local SMTP service isn't enabled by default.

The following configuration settings determine the connection between the report server and a local SMTP server or forwarder:

- `<SendUsing>` is set to **1**.
- `<SMTPServerPickupDirectory>` is set to a folder on the local drive.
   > [!NOTE]
   > Be sure that you don't set `<SMTPServer>` if you're using a local SMTP server.
- `<From>` sets the value that appears in the **From:** line of an email message. This value is required.

### Configure a local SMTP service for the report server

1. In **Control Panel**, select **Turn Windows features on or off** to start the **Add Roles and Features Wizard**.
1. Select **Role-based or feature-based installation** and select **Next**.
1. Select the server to install Internet Information Server (IIS) onto and select **Next**.
1. Select **Next** on the **Server Roles** page.
1. On the **Features** page, select **SMTP Server** and then select **Next**.
     If you receive the prompt to add features that are required for SMTP Server, select **Add Features**.
1. Select **Next** on the **Web Server Role (IIS)** page.
1. Select **Next** on the **Role Services** page.
1. Select **Install** on the **Confirm installation selections** page.
1. Verify that the **Simple Mail Transfer Protocol (SMTP)** windows service is running in the Services console.
     To configure the local SMTP server, you need to use the IIS 6.0 Manager under Admin tools.
1. Open the `RSReportServer.config` file in a text editor.
1. Verify that `<UrlRoot>` is set to the report server URL address. This value is set when you configure the report server and it should be filled in already. If it isn't set, enter the Web Service URL address for your report server.
1. Make sure `<SMTPServer>` is present, but empty.
1. Set `<SendUsing>` to **1**.
1. Set `<SMTPAuthenticate>` to **0**.
1. Set `<SMTPServerPickupDirectory>` to the **SMTP Service Pickup** folder.
     Default location is `C:\inetpub\mailroot\Pickup`.
1. Set `<From>` to a user account that has permission to send mail from the SMTP server. This sets the value that appears in the **From:** line of an email message.
1. Save the file.

## Related content

- [Reporting Services Configuration Manager (Native Mode)](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)
- [Modify a Reporting Services configuration file (rsreportserver.config)](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md)
- [Rsreportserver.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
