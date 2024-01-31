---
title: "Configure Database Mail"
description: "Configure Database Mail using the Database Mail Configuration Wizard or T-SQL commands."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/15/2023
ms.service: sql
ms.topic: conceptual
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
---
# Configure Database Mail

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This article describes how to enable and configure [Database Mail](database-mail.md) using the Database Mail Configuration Wizard, and create a Database Mail Configuration script using templates.

## <a id="BeforeYouBegin"></a> Before You Begin

 Use the **DatabaseMail XPs** option to enable Database Mail on this server. For more information, see [Database Mail XPs Server Configuration Option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) reference article.  

### <a id="Restrictions"></a> Limitations and Restrictions

 Enabling [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Service Broker in any database requires a database lock. If Service Broker was deactivated in `msdb`, to enable Database Mail, first stop [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent so Service Broker can obtain the necessary lock.  

### <a id="Security"></a> Security

 To configure Database Mail, you must be a member of the **sysadmin** fixed server role. To send an email with Database Mail, you must be a member of the **DatabaseMailUserRole** database role in the `msdb` database.  

## <a id="troubleshooting"></a> Troubleshoot

For troubleshooting Database Mail, visit:

- [Troubleshoot Database Mail issues](/troubleshoot/sql/tools/troubleshoot-database-mail-issues)
- [General database mail troubleshooting steps](database-mail-general-troubleshooting.md)
- [Database mail: Mail queued, not delivered](database-mail-mail-queued-not-delivered.md)
- [Common errors with database mail](database-mail-common-errors.md)

## <a id="DBWizard"></a> Use the Database Mail Configuration Wizard

 **To configure Database Mail using a wizard**  

The following steps use SQL Server Management Studio (SSMS). Download the latest version of SSMS at [aka.ms/ssms](https://aka.ms/ssms).

1. In Object Explorer, expand the node for the instance where you want to configure Database Mail.  

1. Expand the **Management** node.  

1. Right-click **Database Mail**, and then select **Configure Database Mail**.  

1. Complete the wizard dialogs.

    -   [Welcome Page](#Welcome)  
    -   [Select Configuration Task Page](#ConfigTask)  
    -   [New Account Page](#NewAccount)  
    -   [Manage Existing Account Page](#ExistingAccount)  
    -   [New Profile Page](#NewProfile)  
    -   [Manage Existing Profile Page](#ExistingProfile)  
    -   [Add account to Profile Page](#AddAccount)  
    -   [Manage Accounts and Profiles Page](#AccountsProfiles)  
    -   [Manage Profile Security, Public Tab](#ProfileSecurityPublic)  
    -   [Manage Profile Security, Private Tab](#ProfileSecurityPrivate)  
    -   [Configure System Parameters Page](#SystemParameters)  
    -   [Complete the Wizard Page](#CompleteWizard)  
    -   [Send Test E-mail Page](#TestEmail)  

### <a id="Welcome"></a> Welcome Page

 This page describes the steps to configuring Database Mail.  

 **Do not show this page again** - Check this to skip this welcome page from displaying in the future.  

 **Next** - Proceeds to the **Select a configuration task** page.  

 **Cancel** - Terminates the wizard without configuring Database Mail.


### <a id="ConfigTask"></a> Select Configuration Task

 Use the **Select Configuration Task** page, to indicate which task you complete each time you use the wizard. If you change your mind before you complete the wizard, use the **Back** button to return to this page and select a different task.  

> [!NOTE]  
>  If Database Mail has not been enabled, you will receive the message: **The Database Mail feature is not available. Would you like to enable this feature?** Responding **Yes**, is equivalent to enabling Database Mail by using the [Database Mail XPs option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the `sp_configure` system stored procedure.  

 **Set up Database Mail by performing the following tasks**  
 Perform all of the tasks required to set up Database Mail for the first time. This option includes all of the other three options.  

 **Manage Database Mail accounts and profiles**  
 Create new Database Mail accounts and profiles or to view, change, or delete existing Database Mail accounts and profiles.  

 **Manage profile security**  
 Configure which users have access to Database Mail profiles.  

 **View or change system parameters**  
 Configure Database Mail system parameters such as the maximum file size for attachments.  

### <a id="NewAccount"></a> New Account Page

 Use this page to create a new Database Mail account. A Database Mail account contains information for sending e-mail to an SMTP server.  

 A Database Mail account contains the information that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses to send e-mail messages to an SMTP server. Each account contains information for one e-mail server.  

 A Database Mail account is only used for Database Mail. A Database Mail account doesn't correspond to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] account or a Microsoft Windows account. Database Mail can be sent using the credentials of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], using other credentials that you supply, or anonymously. When using basic authentication, the user name and password in a Database Mail account are only used for authentication with the e-mail server. An account does not need to correspond to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] user or a user on the computer running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  

 **Account name**  
 Type the name of the new account.  

 **Description**  
 Type a description of the account. The description is optional.  

 **E-mail address**  
 Type the name of the e-mail address for the account. This is the e-mail address that e-mail is sent from. For example, an account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent can send e-mail from the address SqlAgent@Adventure-Works.com.  

 **Display name**  
 Type the name to show on e-mail messages sent from this account. The display name is optional. This is the name displayed on messages sent from this account. For example, an account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent can display the name "SQL Server Agent Automated Mailer" on e-mail messages.  

 **Reply e-mail**  
 Type the e-mail address that is used for replies to e-mail messages sent from this account. The reply e-mail is optional. For example, replies to an account for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent can go to the database administrator, danw@Adventure-Works.com.  

 **Server name**  
 Type the name or IP address of the SMTP server the account uses to send e-mail. Typically this is in a format similar to `smtp.<your_company>.com`. For help with this, consult your mail administrator.  

 **Port number**  
 Type the port number of the SMTP server for this account. Most SMTP servers use port 25 or 587, or port 465 for SSL connections.

 **This server requires a secure connection (SSL)**  
 Encrypts communication using Secure Sockets Layer.  

 **Windows Authentication using Database Engine service credentials**  
 Connection is made to the SMTP server using the credentials configured for the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service. Authentication to the mail server with the service credentials is performed via NTLM authentication, an option only available to Exchange on-premises. For more information, see [Authentication and EWS in Exchange](/exchange/client-developer/exchange-web-services/authentication-and-ews-in-exchange).

 **Basic Authentication**  
 Specify the user name and password required by the SMTP server.  

 **User name**  
 Type the user name that Database Mail uses to sign in to the SMTP server. The user name is required if the SMTP server requires basic authentication.  

 **Password**  
 Type the password that Database Mail uses to sign in to the SMTP server. The password is required if the SMTP server requires basic authentication.  

 **Confirm password**  
 Type the password again to confirm the password. The password is required if the SMTP server requires basic authentication.  

 **Anonymous authentication**  
 Mail is sent to the SMTP server without login credentials. Use this option when the SMTP server doesn't require authentication.  

### <a id="ExistingAccount"></a> Manage Existing Account Page

 Use this page to manage an existing Database Mail account.  

 **Account name**  
 Select the account to view, update or delete.  

 **Delete**  
 Delete the selected account. You must remove this account from associated profiles, or delete such profiles, before you delete the selected account.  

 **Description**  
 View or update the description of the account. The description is optional.  

 **E-mail address**  
 View or update the name of the e-mail address for the account. This is the e-mail address that e-mail is sent from. For example, an account for Microsoft SQL Server Agent can send e-mail from the address **SqlAgent\@Adventure-Works.com**.  

 **Display name**  
 View or update the name to show on e-mail messages sent from this account. The display name is optional. This is the name displayed on messages sent from this account. For example, an account for SQL Server Agent can display the name **SQL Server Agent Automated Mailer** on e-mail messages.  

 **Reply e-mail**  
 View or update the e-mail address that will be used for replies to e-mail messages sent from this account. The reply e-mail is optional. For example, replies to an account for SQL Server Agent can go to the database administrator, **danw\@Adventure-Works.com**.  

 **Server name**  
 View or update the name of the SMTP server the account uses to send e-mail. Typically this is in a format similar to `smtp.<your_company>.com`. For help with this, consult your mail administrator.  

 **Port number**  
 View or update the port number of the SMTP server for this account. Most SMTP servers use port 25 or 587, or port 465 for SSL connections.  

 **This server requires a secure connection (SSL)**  
 Encrypts communication using Secure Sockets Layer.  

 **Windows Authentication using Database Engine service credentials**  
 Connection is made to the SMTP server using the credentials configured for the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] service.  

 **Basic Authentication**  
 Specify the user name and password required by the SMTP server.  

 **User name**  
 View or update the user name that Database Mail uses to sign in to the SMTP server. The user name is required if the SMTP server requires basic authentication.  

 **Password**  
 Change the password that Database Mail uses to sign in to the SMTP server. The password is required if the SMTP server requires basic authentication.  

 **Confirm password**  
 Type the password again to confirm the password. The password is required if the SMTP server requires basic authentication.  

 **Anonymous authentication**  
 Mail is sent to the SMTP server without login credentials. Use this option when the SMTP server doesn't require authentication.  

### <a id="NewProfile"></a> New Profile page

 Use this page to create a Database Mail profile. A Database Mail profile is a collection of Database Mail accounts. Profiles improve reliability in cases where an e-mail server becomes unreachable, by providing alternative Database Mail accounts. At least one Database Mail account is required. For more information about setting the priority of Database Mail accounts in the profile, see [Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md).  

 Use the **Move Up** and **Move Down** buttons to change the order in which Database Mail accounts are used. This order is determined by a value called the sequence number. **Move Up** lowers the sequence number and **Move Down** increases the sequence number. The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that has the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on, until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the Database Mail **AccountRetryDelay** parameter, then starts the process of attempting to send the mail again, starting with the lowest sequence number. Use the Database Mail **AccountRetryAttempts** parameter, to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile. You can configure the **AccountRetryDelay** and **AccountRetryAttempts** parameters on the **Configure System Parameters** page of the Database Mail Configuration Wizard.  

 **Profile name**  
 Type the name for the new profile. The profile is created with this name. Do not use the name of an existing profile.  

 > [!NOTE]  
 > To send e-mail using SQL Agent jobs in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], SQL Server Agent can use only one Database Mail profile, and it must be called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

 **Description**  
 Type a description for the profile. The description is optional.  

 **SMTP accounts**  
 Choose one or more accounts for the profile. The priority sets the order in which Database Mail uses the accounts. If no accounts are listed, you must select **Add** to continue, and add a new SMTP account.  

 **Add**  
 Add an account to the profile.  

 **Remove**  
 Remove the selected account from the profile.  

 **Move Up**  
 Increase the priority of the selected account.  

 **Move Down**  
 Decrease the priority of the selected account.  

### <a id="ExistingProfile"></a> Manage Existing Profile page

 Use this page to manage an existing Database Mail profile. A Database Mail profile is a collection of Database Mail accounts. Profiles improve reliability in cases where an e-mail server becomes unreachable, by providing alternative Database Mail accounts. At least one Database Mail account is required. For more information about setting the priority of Database Mail accounts in the profile, see [Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md).  

 Use the **Move Up** and **Move Down** buttons to change the order in which Database Mail accounts are used. This order is determined by a value called the sequence number. **Move Up** lowers the sequence number and **Move Down** increases the sequence number. The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that has the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on, until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the Database Mail **AccountRetryDelay** parameter, then starts the process of attempting to send the mail again, starting with the lowest sequence number. Use the Database Mail **AccountRetryAttempts** parameter, to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile. You can configure the **AccountRetryDelay** and **AccountRetryAttempts** parameters on the **Configure System Parameters** page of the Database Mail Configuration Wizard.  

 **Profile name**  
 Select the name of the profile to manage.  

 **Delete**  
 Delete the selected profile. You'll be prompted to select **Yes** to delete the selected profile and to fail any unsent messages, or to select **No** to delete the selected profile only if there are no unsent messages.  

 **Description**  
 View or change the description of the selected profile. The description is optional.  

 **SMTP accounts**  
 Choose one or more accounts for the profile. The failover priority sets the order in which Database Mail uses the account in case of a failover.  

 **Add**  
 Add an account to the profile.  

 **Remove**  
 Remove the selected account from the profile.  

 **Move Up**  
 Increase the failover priority of the selected account.  

 **Move Down**  
 Decrease the failover priority of the selected account.  

 **Priority**  
 View the current failover priority of the account.  

 **Account name**  
 View the name of the account.  

 **E-mail Address**  
 View the e-mail address of the account.  

### <a id="AddAccount"></a> Add Account to Profile Page

 Use this page to choose the account to add to the profile. Either choose an existing account from the **Account name** box, or select **New Account**.  

 **Account name**  
 Select the name of the account to add to the profile.  

 **E-mail address**  
 View the e-mail address for the selected account. You can't change the e-mail address from this page. To change the e-mail address for the account, go back to the main page of the wizard and select the **Manage Database Mail accounts and profiles** option.  

 **Server name**  
 View the mail server name for the selected account. You can't change the server name from this page. To change the server name for the account, go back to the main page of the wizard and select the **Manage Database Mail accounts and profiles** option.  

 **New Account**  
 Create a new account.  

### <a id="AccountsProfiles"></a> Manage Accounts and Profiles Page

 Use this page to choose a task for managing a profile or account.  

 **Create a new account**  
 Create a new account.  

 **View, change or delete an existing account**  
 Manage or delete an existing account.  

 **Create a new profile**  
 Create a new profile.  

 **View, change or delete an existing profile. You can also manage accounts associated with the profile.**  
 Update or delete an existing profile. This option also allows you to manage accounts associated with the profile.  

### <a id="ProfileSecurityPublic"></a> Manage profile security, public tab

 Use this page to configure a public profile.  

 Profiles are either public or private. A private profile is accessible only to specific users or roles. A public profile allows any user or role with access to the mail host database (`msdb`) to send e-mail using that profile.  

 A profile can be a default profile. In this case, users or roles can send e-mail using the profile without explicitly specifying the profile. If the user or role sending the e-mail message has a default private profile, Database Mail uses that profile. If the user or role has no default private profile, `sp_send_dbmail` uses the default public profile for the `msdb` database. If there's no default private profile for the user or role and no default public profile for the database, `sp_send_dbmail` returns an error. Only one profile can be marked as the default profile.  

 **Public**  
 Select this option to make the specified profile public.  

 **Profile Name**  
 Displays the name of the profile.  

> [!NOTE]  
> To send e-mail using SQL Agent jobs in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], SQL Server Agent can use only one Database Mail profile, and it must be called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

 **Default Profile**  
 Select this option to make the specified profile the default profile.  

 **Show only existing public profiles**  
 Select this option to show only public profiles in the specified database.  

### <a id="ProfileSecurityPrivate"></a> Manage profile security, private tab

 Use this page to configure a private profile.  

 Profiles are either public or private. A private profile is accessible only to specific users or roles. A public profile allows any user or role with access to the mail host database (`msdb`) to send e-mail using that profile.  

 A profile can be a default profile. In this case, users or roles can send e-mail using the profile without explicitly specifying the profile. If the user or role sending the e-mail message has a default private profile, Database Mail uses that profile. If the user or role has no default private profile, `sp_send_dbmail` uses the default public profile for the `msdb` database. If there's no default private profile for the user or role and no default public profile for the database, `sp_send_dbmail` returns an error.  

 **User name**  
 Select the name of a user or role in the `msdb` database.  

 **Access**  
 Select whether the user or role has access to the specified profile.  

 **Profile name**  
 View the name of the profile.  

 **Is Default Profile**  
 Select whether the profile is the default profile for the user or role. Each user or role can have only one default profile.  

 **Show only existing private profiles for this user**  
 Select this option to display only profiles that the specified user or role already has access to.  

### <a id="SystemParameters"></a> Configure System Parameters

 Use this page to specify Database Mail system parameters. View the system parameters and the current value of each parameter. Select a parameter to view a short description in the information pane.  

 **Account Retry Attempts**  
 The number of times that the external mail process attempts to send the e-mail message using each account in the specified profile.  

 **Account Retry Delay (seconds)**  
 The amount of time, in seconds, for the external mail process to wait after it tries to deliver a message using all accounts in the profile before it attempts all accounts again.  

 **Maximum File Size (Bytes)**  
 The maximum size of an attachment, in bytes.  

 **Prohibited Attachment File Extensions**  
 A comma-separated list of extensions that can't be sent as an attachment to an e-mail message. Select the browse button (**...**) to add additional extensions.  

 **Database Mail Executable Minimum Lifetime (seconds)**  
 The minimum amount of time, in seconds, that the external mail process remains active. The process remains active as long as there are e-mails in the Database Mail queue. This parameter specifies the time the process remains active if there are no messages to process.  

 **Logging level**  
 Specify which messages are recorded in the Database Mail log. Possible values are:  

-   Normal - logs only errors  

-   Extended - logs errors, warnings, and informational messages  

-   Verbose - logs errors, warnings, informational messages, success messages, and additional internal messages. Use verbose logging for troubleshooting.  

 Default value is Extended.  

 **Reset All**  
 Select this option to reset the values on the page to the default values.  

### <a id="CompleteWizard"></a> Complete the Wizard Page

 Use this page to review the actions that **Database Mail Configuration Wizard** performs. No changes are made until you finish the wizard.  

### <a id="TestEmail"></a> Send Test E-Mail Page

 For a quick tutorial to sending a test email, see [Send a test email with database mail](database-mail-sending-test-email.md).

 Use the **Send Test E-Mail from**_<instance_name>_ page to send an e-mail message using the specified Database Mail profile. Only members of the **sysadmin** fixed server role can send test e-mail using this page.  

 **Database Mail Profile**  
 Select a Database Mail profile from the list. This is a required field. If no profiles are shown, there are no profiles or you don't have permission to a profile. Use the **Database Mail Configuration Wizard** to create and configure profiles. If no profiles are listed, use the Database Mail Configuration Wizard to create a profile for your use.  

 **To**  
 The e-mail address of the message recipients. At least one recipient is required.  

 **Subject**  
 The subject line for the test e-mail. Change the default subject to better identify your email for troubleshooting.  

 **Body**  
 The body of the test e-mail. Change the default subject to better identify your email for troubleshooting.  

 The **Database Mail Test E-Mail** dialog box confirms that the test message that Database Mail attempted to send the message and provides the **mailitem_id** for the test e-mail message. Check with the recipient to determine if the e-mail arrived. Normally e-mail is received in a few minutes, but the e-mail can be delayed because of slow network performance, a backlog of messages at the mail server, or if the server is temporarily unavailable. Use the **mailitem_id** for troubleshooting.  

 **Sent e-mail**  

 The **mailitem_id** of the test e-mail message.  

 **Troubleshoot**

 Selecting this button brings you to this document, [Configure Database Mail](configure-database-mail.md).

## <a id="Template"></a> Use SQL Server Management Studio templates to generate T-SQL

 **To create a Database Mail configuration T-SQL script**  

1. In SQL Server Management Studio (SSMS), on the **View** menu, select **Template Explorer**.  

1. In the **Template Explorer** window, expand the **Database Mail** folder.  

1. Double-click **Simple Database Mail Configuration**. The template opens in a new query window.  

1. On the **Query** menu, select **Specify Values for Template Parameters**. The **Replace Template Parameters** window opens.  

1. Type values for the **profile_name**, **account_name**, **SMTP_servername**, **email_address**, and **display_name**. SQL Server Management Studio fills in the template with the values you provide.  

1. Execute the script to create the configuration.  

1. The script doesn't grant any database users access to the profile. Therefore, by default, the profile can only be used by members of the **sysadmin** fixed security role. For more information about granting access to profiles, see [sysmail_add_principalprofile_sp (Transact-SQL)](../../relational-databases/system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md)  

## Related content

- [Configure SQL Server Agent mail to use Database Mail](configure-sql-server-agent-mail-to-use-database-mail.md)
- [Automate management tasks using SQL Agent jobs in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/job-automation-managed-instance)
- [Configure SQL Server Agent](../../ssms/agent/configure-sql-server-agent.md)
- [General database mail troubleshooting steps](database-mail-general-troubleshooting.md)