---
title: "Configure Database Mail"
description: "Configure Database Mail using the Database Mail Configuration Wizard or T-SQL commands."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: erinstellato
ms.date: 02/20/2026
ms.service: sql
ms.topic: how-to
f1_keywords:
  - "sql13.swb.sqlimail.profileandaccountmanagement.f1"
  - "sql13.swb.sqlimail.newaccount.f1"
  - "sql13.swb.dbmail. manageprofilesecurity.profileview.f1"
  - "sql13.swb.sqlimail.manageexistingprofile.f1"
  - "sql13.swb.sqlimail.addaccounttoprofile.f1"
  - "sql13.swb.dbmail.manageexistingaccount.f1"
  - "sql13.swb.sqlimail.manageprofilesecurity.profileview.f1"
  - "sql13.swb.sqlimail.welcome.f1"
  - "sql13.swb.sqlimail.manageprofilesecurity.principalview.f1"
  - "sql13.swb.sqlimail.newsqlimailaccount.f1"
  - "sql13.swb.sqlimail.selectconfiguration.f1"
  - "sql13.swb.dbmail.completewizard.f1"
  - "sql13.swb.dbmail.sendtestemail.test.f1"
  - "sql13.swb.sqlimail.newprofile.f1"
  - "sql13.swb.dbmail.addaccounttoprofile.f1"
  - "sql13.swb.dbmail.newprofile.f1"
  - "sql13.swb.sqlimail.manageexistingaccount.f1"
  - "sql13.swb.dbmail.welcome.f1"
  - "sql13.swb.dbmail.newaccount.f1"
  - "sql13.swb.dbmail.profileandaccountmanagement.f1"
  - "sql13.swb.dbmail.selectconfiguration.f1"
  - "sql13.swb.dbmail.sendtestemail.f1"
  - "sql13.swb.sqlimail.completewizard.f1"
  - "sql13.swb.dbmail.configuresystem.f1"
  - "sql13.swb.sqlimail.configuresystem.f1"
  - "sql13.swb.dbmail.newsqlimailaccount.f1"
  - "sql13.swb.dbmail.manageexistingprofile.f1"
  - "sql13.swb.dbmail.manageprofilesecurity.principalview.f1"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Configure database mail

 [!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  This article describes how to enable and configure [Database Mail](database-mail.md) using the Database Mail Configuration Wizard, and create a Database Mail Configuration script using templates.

 Use the `DatabaseMail XPs` server configuration option to enable Database Mail on this server. For more information, see [Database Mail XPs (server configuration option)](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md).  

## Prerequisites

 - To configure Database Mail, you must be a member of the **sysadmin** fixed server role. 
    - To send an email with Database Mail, you must be a member of the **DatabaseMailUserRole** database role in the `msdb` database.
 - Service Broker must be enabled in the `msdb` database.
     - Enabling [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Service Broker in any database requires a database lock. For steps and more information, see [Verify service broker is enabled for msdb](database-mail-general-troubleshooting.md#verify-service-broker-is-enabled-for-msdb).
 - The following steps use SQL Server Management Studio (SSMS). Download the latest version of SSMS at [aka.ms/ssms](https://aka.ms/ssms).

<a id="DBWizard"></a>

## Use the Database Mail Configuration Wizard to configure Database Mail

1. In **Object Explorer**, expand the node for the instance where you want to configure Database Mail.  

1. Expand the **Management** node.

1. Right-click **Database Mail**, and then select **Configure Database Mail**. The **Database Mail Configuration Wizard** launches.

   <a id="Welcome"></a>
   <a id="welcome-page"></a>

1. Select **Next** on the Welcome page to begin.

   <a id="ConfigTask"></a>
   <a id="select-configuration-task"></a>

1. Use the **Select Configuration Task** page to select **Set up Database Mail by performing the following tasks...**. This option includes all of the other three options. 

   If you want to manage an existing profile or account, profile security, or system parameters, select the appropriate option. 

   > [!NOTE]  
   > If the Database Mail feature has not been enabled, you will receive the message: **The Database Mail feature is not available. Would you like to enable this feature?**
   >
   > 1. First, [Verify service broker is enabled for msdb](database-mail-general-troubleshooting.md#verify-service-broker-is-enabled-for-msdb).
   > 1. Then select **Yes** to enable Database mail, which uses the [Database Mail XPs (server configuration option)](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the `sp_configure` system stored procedure.

   <a id="NewProfile"></a>
   <a id="new-profile-page"></a>

1. On the **New Profile** page, you'll create a new Database Mail profile. A Database Mail profile is a collection of Database Mail accounts. 

   Profiles improve reliability in cases where an e-mail server becomes unreachable, by providing alternative Database Mail accounts. At least one Database Mail account is required. 

   - For more information about setting the priority of Database Mail accounts in the profile, see [Create a Database Mail Profile](create-a-database-mail-profile.md).

   - Use the **Move Up** and **Move Down** buttons to change the order in which Database Mail accounts are used. This order is determined by a value called the sequence number. **Move Up** lowers the sequence number and **Move Down** increases the sequence number. The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that has the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on, until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the Database Mail **AccountRetryDelay** parameter, then starts the process of attempting to send the mail again, starting with the lowest sequence number. Use the Database Mail **AccountRetryAttempts** parameter, to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile. You can configure the **AccountRetryDelay** and **AccountRetryAttempts** parameters on the **Configure System Parameters** page of the Database Mail Configuration Wizard.  

   1. Provide a **Profile name** and **Description** (optional) for the new profile. The profile name is different from the name of the server or the email account that will be used.

      > [!NOTE]  
      > To send e-mail using SQL Agent jobs in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], SQL Server Agent can use only one Database Mail profile, and it must be called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

   1. In the table of **SMTP accounts**, choose an existing account or select **Add** to a new SMTP account. This guide continues to creating a new SMTP account.

   <a id="NewAccount"></a>
   <a id="new-account-page"></a>

1. In the **Add Account to Profile** popup, select **New Account...**. 
1. On the **New Database Mail Account** popup, you'll create a new Database Mail account for sending e-mail to an SMTP server.  

   A Database Mail account contains the information that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses to send e-mail messages to an SMTP server. Each account contains information for one e-mail server.  

   A Database Mail account is only used for Database Mail. A Database Mail account doesn't correspond to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] account or a Microsoft Windows account. Database Mail can be sent using the credentials of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], using other credentials that you supply, or anonymously. When using basic authentication, the user name and password in a Database Mail account are only used for authentication with the e-mail server. An account does not need to correspond to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] user or a user on the computer running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  

   1. Provide the **Account name**, **Description** (optional), and **E-mail address**.  
      The e-mail address for the account to send email *from*.   
   1. Provide the **Display name** (optional), which will be the name displayed on messages sent from this account. For example, an account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent can display the name "SQL Server Agent Automated Mailer" on e-mail messages.  

      If the display name contains backslash characters (`\`), you must escape them by using double backslashes (`\\`). For example, to display `SERVER\SQL`, enter `SERVER\\SQL` in the **Display Name** field. Single backslashes are interpreted as escape characters and won't appear in sent emails.

   1. Provide the **Reply e-mail** (optional), used for replies to e-mail messages sent from this account.
   1. Provide the **Server name** or IP address of the SMTP server the account uses to send e-mail. Typically this is in a format similar to `smtp.<your_company>.com` or `smtp.<cloud service provider>.net`. For help with this, consult your mail administrator.

       Your server name might need to be added to an SMTP Allow list in order to successfully send emails.

   1. Provide the **Port number** of the SMTP server for this account. Most SMTP servers use port 25 or 587, or port 465 for SSL connections.
   1. Select the option **This server requires a secure connection (SSL)** to enable encrypted communication using Secure Sockets Layer.  
   1. Under **SMTP Authentication**, you have options.

      - **Windows Authentication using Database Engine service credentials**

        Connection is made to the SMTP server using the credentials configured for the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service. Authentication to the mail server with the service credentials is performed via NTLM authentication, an option only available to Exchange on-premises. For more information, see [Authentication and EWS in Exchange](/exchange/client-developer/exchange-web-services/authentication-and-ews-in-exchange).

      - **Basic Authentication**  

         Specify the user name and password required by the SMTP server.

      - **Anonymous authentication**

         Mail is sent to the SMTP server without login credentials. Use this option when the SMTP server doesn't require authentication.  

   1. Select **OK**, **OK**, and **Next** to continue.

   <a id="ProfileSecurityPublic"></a>
   <a id="manage-profile-security-public-tab"></a>

1. In the **Manage profile security**, you'll configure a public and private profile security. 

   Profiles are either public or private. A private profile is accessible only to specific users or roles. A public profile allows any user or role with access to the instance's mail host database (`msdb`) to send e-mail using that profile.  

   A profile can also be the default profile. In this case, users or roles can send e-mail using the profile without explicitly specifying the profile. If the user or role sending the e-mail message has a default private profile, Database Mail uses that profile. If the user or role has no default private profile, `sp_send_dbmail` uses the default public profile for the `msdb` database. If there's no default private profile for the user or role and no default public profile for the database, `sp_send_dbmail` returns an error. Only one profile can be marked as the default profile.  

   - In the **Public Profiles** tab, you can select which profiles should be public on this instance, and if any one of them should be the **Default Profile**. 

   > [!NOTE]  
   > To send e-mail using SQL Agent jobs in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], SQL Server Agent can use only one Database Mail profile, and it must be called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

   <a id="manage-profile-security-private-tab"></a>

   In the **Private Profiles** tab, first select a user from the dropdown list. A private profile is accessible only to specific users or roles. Select which profiles should be available on this instance to the user, and if any one of them should be the **Default Profile**. 

1. Select **Next** to continue.

   <a id="SystemParameters"></a>
   <a id="configure-system-parameters"></a>

1. On the **Configure System Parameters** page, you can view or change the system parameters. Select a parameter to view a short description in the information pane.  

    - **Account Retry Attempts**  
     The number of times that the external mail process attempts to send the e-mail message using each account in the specified profile.  

    - **Account Retry Delay (seconds)**  
     The amount of time, in seconds, for the external mail process to wait after it tries to deliver a message using all accounts in the profile before it attempts all accounts again.  

    - **Maximum File Size (Bytes)**  
     The maximum size of an attachment, in bytes.  

    - **Prohibited Attachment File Extensions**  
     A comma-separated list of extensions that can't be sent as an attachment to an e-mail message. Select the browse button (**...**) to add additional extensions.  

    - **Database Mail Executable Minimum Lifetime (seconds)**  
     The minimum amount of time, in seconds, that the external mail process remains active. The process remains active as long as there are e-mails in the Database Mail queue. This parameter specifies the time the process remains active if there are no messages to process.  

    - **Logging level**  
     Specify which messages are recorded in the Database Mail log. Possible values are:  

        - Normal - logs only errors  

        - Extended (Default) - logs errors, warnings, and informational messages  

        - Verbose - logs errors, warnings, informational messages, success messages, and additional internal messages. Use verbose logging for troubleshooting.  

    - **Reset All**  
     Select this option to reset the values on the page to the default values.  

1. The default system parameters are recommended. Select **Next** to continue.

   <a id="CompleteWizard"></a>
   <a id="complete-the-wizard-page"></a>

1. Select **Finish** to review the new configuration of Database Mail. Review the progress of the wizard, then select **Close**.

   <a id="TestEmail"></a>
   <a id="send-test-e-mail-page"></a>

1. Return to the **Object Explorer** and send a test email to verify your configuration. For a quick tutorial to sending a test email, see [Send a test email with database mail](database-mail-sending-test-email.md).

    Only members of the **sysadmin** fixed server role can send test e-mail using this page.  

1. To use Database Mail to send emails from SQL Agent jobs, continue to [Configure SQL Server Agent](/ssms/agent/configure-sql-server-agent)
and [Configure SQL Server Agent mail to use Database Mail](configure-sql-server-agent-mail-to-use-database-mail.md).

## Related content

- [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance)
- [General database mail troubleshooting steps](database-mail-general-troubleshooting.md)