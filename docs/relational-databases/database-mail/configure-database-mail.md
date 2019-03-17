---
title: "Configure Database Mail | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
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
ms.assetid: 7edc21d4-ccf3-42a9-84c0-3f70333efce6
author: stevestein
ms.author: sstein
manager: craigg
---
# Configure Database Mail
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to enable and configure Database Mail using the Database Mail Configuration Wizard, and create a Database Mail Configuration script using templates.  
  
-   **Before you begin:**  [Limitations and Restrictions](#Restrictions), [Security](#Security)  
  
-   **To configure Database Mail, using:**  [Database Mail Configuration Wizard](#DBWizard), [Using Templates](#Template)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
 Use the **DatabaseMail XPs** option to enable Database Mail on this server. For more information, see [Database Mail XPs Server Configuration Option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) reference topic.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Enabling [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service Broker in any database requires a database lock. If Service Broker was deactivated in **msdb**, to enable Database Mail, first stop [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent so Service Broker can obtain the necessary lock.  
  
###  <a name="Security"></a> Security  
 To configure Database Mail you must be a member of the **sysadmin** fixed server role. To send Database Mail you must be a member of the **DatabaseMailUserRole** database role in the **msdb** database.  
  
##  <a name="DBWizard"></a> Using Database Mail Configuration Wizard  
 **To configure Database Mail using a wizard**  
  
1.  In Object Explorer, expand the node for the instance  you want to configure Database mail.  
  
2.  Expand the **Management** node.  
  
3.  Right-click **Database Mail**, and then click **Configure Database Mail**.  
  
4.  Complete the Wizard dialogs  
  
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
  
###  <a name="Welcome"></a> Welcome Page  
 This page describes the steps to configuring Database Mail.  
  
 **Do not show this page again** - Check this to skip this welcome page from displaying in the future.  
  
 **Next** - Proceeds to the **Select a configuration task** page.  
  
 **Cancel** - Terminates the wizard without configuring Database Mail  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="ConfigTask"></a> Select Configuration Task  
 Use the **Select Configuration Task** page, to indicate which task you will complete each time you use the wizard. If you change your mind before you complete the wizard, use the **Back** button to return to this page and select a different task.  
  
> [!NOTE]  
>  If Database Mail has not been enabled, you will receive the message: **The Database Mail feature is not available.  Would you like to enable this feature?** Responding **Yes**, is equivalent to enabling Database Mail by using the [Database Mail XPs option](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the **sp_configure** system stored procedure.  
  
 **Set up Database Mail by performing the following tasks**  
 Perform all of the tasks required to set up Database Mail for the first time. This option includes all of the other three options.  
  
 **Manage Database Mail accounts and profiles**  
 Create new Database Mail accounts and profiles or to view, change, or delete existing Database Mail accounts and profiles.  
  
 **Manage profile security**  
 Configure which users have access to Database Mail profiles.  
  
 **View or change system parameters**  
 Configure Database Mail system parameters such as the maximum file size for attachments.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="NewAccount"></a> New Account Page  
 Use this page to create a new Database Mail account. A Database Mail account contains information for sending e-mail to an SMTP server.  
  
 A Database Mail account contains the information that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses to send e-mail messages to an SMTP server. Each account contains information for one e-mail server.  
  
 A Database Mail account is only used for Database Mail. A Database Mail account does not correspond to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account or a Microsoft Windows account. Database Mail can be sent using the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], using other credentials that you supply, or anonymously. When using basic authentication, the user name and password in a Database Mail account are only used for authentication with the e-mail server. An account need not correspond to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user or a user on the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Account name**  
 Type the name of the new account.  
  
 **Description**  
 Type a description of the account. The description is optional.  
  
 **E-mail address**  
 Type the name of the e-mail address for the account. This is the e-mail address that e-mail is sent from. For example, an account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may send e-mail from the address SqlAgent@Adventure-Works.com.  
  
 **Display name**  
 Type the name to show on e-mail messages sent from this account. The display name is optional. This is the name displayed on messages sent from this account. For example, an account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may display the name "SQL Server Agent Automated Mailer" on e-mail messages.  
  
 **Reply e-mail**  
 Type the e-mail address that will be used for replies to e-mail messages sent from this account. The reply e-mail is optional. For example, replies to an account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent may go to the database administrator, danw@Adventure-Works.com.  
  
 **Server name**  
 Type the name or IP address of the SMTP server the account uses to send e-mail. Typically this is in a format similar to **smtp.**_<your_company>_**.com**. For help with this, consult your mail administrator.  
  
 **Port number**  
 Type the port number of the SMTP server for this account. Most SMTP servers use port 25.  
  
 **This server requires a secure connection (SSL)**  
 Encrypts communication using Secure Sockets Layer.  
  
 **Windows Authentication using Database Engine service credentials**  
 Connection is made to the SMTP server using the credentials configured for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service.  
  
 **Basic Authentication**  
 Specify the user name and password required by the SMTP server.  
  
 **User name**  
 Type the user name that Database Mail uses to log in to the SMTP server. The user name is required if the SMTP server requires basic authentication.  
  
 **Password**  
 Type the password that Database Mail uses to log in to the SMTP server. The password is required if the SMTP server requires basic authentication.  
  
 **Confirm password**  
 Type the password again to confirm the password. The password is required if the SMTP server requires basic authentication.  
  
 **Anonymous authentication**  
 Mail is sent to the SMTP server without login credentials. Use this option when the SMTP server does not require authentication.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="ExistingAccount"></a> Manage Existing Account Page  
 Use this page to manage an existing Database Mail account.  
  
 **Account name**  
 Select the account to view, update or delete.  
  
 **Delete**  
 Delete the selected account. You must remove this account from associated profiles, or delete such profiles, before you delete the selected account.  
  
 **Description**  
 View or update the description of the account. The description is optional.  
  
 **E-mail address**  
 View or update the name of the e-mail address for the account. This is the e-mail address that e-mail is sent from. For example, an account for Microsoft SQL Server Agent may send e-mail from the address **SqlAgent@Adventure-Works.com**.  
  
 **Display name**  
 View or update the name to show on e-mail messages sent from this account. The display name is optional. This is the name displayed on messages sent from this account. For example, an account for SQL Server Agent may display the name **SQL Server Agent Automated Mailer** on e-mail messages.  
  
 **Reply e-mail**  
 View or update the e-mail address that will be used for replies to e-mail messages sent from this account. The reply e-mail is optional. For example, replies to an account for SQL Server Agent may go to the database administrator, **danw@Adventure-Works.com**.  
  
 **Server name**  
 View or update the name of the SMTP server the account uses to send e-mail. Typically this is in a format similar to **smtp.<your_company>.com**. For help with this, consult your mail administrator.  
  
 **Port number**  
 View or update the port number of the SMTP server for this account. Most SMTP servers use port 25.  
  
 **This server requires a secure connection (SSL)**  
 Encrypts communication using Secure Sockets Layer.  
  
 **Windows Authentication using Database Engine service credentials**  
 Connection is made to the SMTP server using the credentials configured for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service.  
  
 **Basic Authentication**  
 Specify the user name and password required by the SMTP server.  
  
 **User name**  
 View or update the user name that Database Mail uses to log in to the SMTP server. The user name is required if the SMTP server requires basic authentication.  
  
 **Password**  
 Change the password that Database Mail uses to log in to the SMTP server. The password is required if the SMTP server requires basic authentication.  
  
 **Confirm password**  
 Type the password again to confirm the password. The password is required if the SMTP server requires basic authentication.  
  
 **Anonymous authentication**  
 Mail is sent to the SMTP server without login credentials. Use this option when the SMTP server does not require authentication.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="NewProfile"></a> New Profile Page  
 Use this page to create a Database Mail profile. A Database Mail profile is a collection of Database Mail accounts. Profiles improve reliability in cases where an e-mail server becomes unreachable, by providing alternative Database Mail accounts. At least one Database Mail account is required. For more information about setting the priority of Database Mail accounts in the profile, see [Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md).  
  
 Use the **Move Up** and **Move Down** buttons to change the order in which Database Mail accounts are used. This order is determined by a value called the sequence number. **Move Up** lowers the sequence number and **Move Down** increases the sequence number. The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that has the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the Database Mail **AccountRetryDelay** parameter, then starts the process of attempting to send the mail again, starting with the lowest sequence number. Use the Database Mail **AccountRetryAttempts** parameter, to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile. You can configure the **AccountRetryDelay** and **AccountRetryAttempts** parameters on the **Configure System Parameters** page of the Database Mail Configuration Wizard.  
  
 **Profile name**  
 Type the name for the new profile. The profile is created with this name. Do not use the name of an existing profile.  
  
 **Description**  
 Type a description for the profile. The description is optional.  
  
 **SMTP accounts**  
 Choose one or more accounts for the profile. The priority sets the order in which Database Mail uses the accounts. If no accounts are listed, you must click **Add** to continue, and add a new SMTP account.  
  
 **Add**  
 Add an account to the profile.  
  
 **Remove**  
 Remove the selected account from the profile.  
  
 **Move Up**  
 Increase the priority of the selected account.  
  
 **Move Down**  
 Decrease the priority of the selected account.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="ExistingProfile"></a> Manage Existing Profile Page  
 Use this page to manage an existing Database Mail profile. A Database Mail profile is a collection of Database Mail accounts. Profiles improve reliability in cases where an e-mail server becomes unreachable, by providing alternative Database Mail accounts. At least one Database Mail account is required. For more information about setting the priority of Database Mail accounts in the profile, see [Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md).  
  
 Use the **Move Up** and **Move Down** buttons to change the order in which Database Mail accounts are used. This order is determined by a value called the sequence number. **Move Up** lowers the sequence number and **Move Down** increases the sequence number. The sequence number determines the order in which Database Mail uses accounts in the profile. For a new e-mail message, Database Mail starts with the account that has the lowest sequence number. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the Database Mail **AccountRetryDelay** parameter, then starts the process of attempting to send the mail again, starting with the lowest sequence number. Use the Database Mail **AccountRetryAttempts** parameter, to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile. You can configure the **AccountRetryDelay** and **AccountRetryAttempts** parameters on the **Configure System Parameters** page of the Database Mail Configuration Wizard.  
  
 **Profile name**  
 Select the name of the profile to manage.  
  
 **Delete**  
 Delete the selected profile. You will be prompted to select **Yes** to delete the selected profile and to fail any unsent messages, or to select **No** to delete the selected profile only if there are no unsent messages.  
  
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
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="AddAccount"></a> Add Account to Profile Page  
 Use this page to choose the account to add to the profile. Either choose an existing account from the **Account name** box, or click **New Account**.  
  
 **Account name**  
 Select the name of the account to add to the profile.  
  
 **E-mail address**  
 View the e-mail address for the selected account. You cannot change the e-mail address from this page. To change the e-mail address for the account, go back to the main page of the wizard and select the **Manage Database Mail accounts and profiles** option.  
  
 **Server name**  
 View the mail server name for the selected account. You cannot change the server name from this page. To change the server name for the account, go back to the main page of the wizard and select the **Manage Database Mail accounts and profiles** option.  
  
 **New Account**  
 Create a new account.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="AccountsProfiles"></a> Manage Accounts and Profiles Page  
 Use this page to choose a task for managing a profile or account.  
  
 **Create a new account**  
 Create a new account.  
  
 **View, change or delete an existing account**  
 Manage or delete an existing account.  
  
 **Create a new profile**  
 Create a new profile.  
  
 **View, change or delete an existing profile. You can also manage accounts associated with the profile.**  
 Update or delete an existing profile. This option also allows you to manage accounts associated with the profile.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="ProfileSecurityPublic"></a> Manage Profile Security, Public Tab  
 Use this page to configure a public profile.  
  
 Profiles are either public or private. A private profile is accessible only to specific users or roles. A public profile allows any user or role with access to the mail host database (**msdb**) to send e-mail using that profile.  
  
 A profile may be a default profile. In this case, users or roles can send e-mail using the profile without explicitly specifying the profile. If the user or role sending the e-mail message has a default private profile, Database Mail uses that profile. If the user or role has no default private profile, **sp_send_dbmail** uses the default public profile for the **msdb** database. If there is no default private profile for the user or role and no default public profile for the database, **sp_send_dbmail** returns an error. Only one profile can be marked as the default profile.  
  
 **Public**  
 Select this option to make the specified profile public.  
  
 **Profile Name**  
 Displays the name of the profile.  
  
 **Default Profile**  
 Select this option to make the specified profile the default profile.  
  
 **Show only existing public profiles**  
 Select this option to show only public profiles in the specified database.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="ProfileSecurityPrivate"></a> Manage Profile Security, Private Tab  
 Use this page to configure a private profile.  
  
 Profiles are either public or private. A private profile is accessible only to specific users or roles. A public profile allows any user or role with access to the mail host database (**msdb**) to send e-mail using that profile.  
  
 A profile may be a default profile. In this case, users or roles can send e-mail using the profile without explicitly specifying the profile. If the user or role sending the e-mail message has a default private profile, Database Mail uses that profile. If the user or role has no default private profile, **sp_send_dbmail** uses the default public profile for the **msdb** database. If there is no default private profile for the user or role and no default public profile for the database, **sp_send_dbmail** returns an error.  
  
 **User name**  
 Select the name of a user or role in the **msdb** database.  
  
 **Access**  
 Select whether the user or role has access to the specified profile.  
  
 **Profile name**  
 View the name of the profile.  
  
 **Is Default Profile**  
 Select whether the profile is the default profile for the user or role. Each user or role may have only one default profile.  
  
 **Show only existing private profiles for this user**  
 Select this option to display only profiles that the specified user or role already has access to.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="SystemParameters"></a> Configure System Parameters  
 Use this page to specify Database Mail system parameters. View the system parameters and the current value of each parameter. Select a parameter to view a short description in the information pane.  
  
 **Account Retry Attempts**  
 The number of times that the external mail process attempts to send the e-mail message using each account in the specified profile.  
  
 **Account Retry Delay (seconds)**  
 The amount of time, in seconds, for the external mail process to wait after it tries to deliver a message using all accounts in the profile before it attempts all accounts again.  
  
 **Maximum File Size (Bytes)**  
 The maximum size of an attachment, in bytes.  
  
 **Prohibited Attachment File Extensions**  
 A comma-separated list of extensions which cannot be sent as an attachment to an e-mail message. Click the browse button (**...**) to add additional extensions.  
  
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
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="CompleteWizard"></a> Complete the Wizard Page  
 Use this page to review the actions that **Database Mail Configuration Wizard** will perform. No changes are made until you finish the wizard.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
###  <a name="TestEmail"></a> Send Test E-Mail Page  
 Use the **Send Test E-Mail from**_<instance_name>_ page to send an e-mail message using the specified Database Mail profile. Only members of the **sysadmin** fixed server role can send test e-mail using this page.  
  
 **Database Mail Profile**  
 Select a Database Mail profile from the list. This is a required field. If no profiles are shown, there are no profiles or you do not have permission to a profile. Use the **Database Mail Configuration Wizard** to create and configure profiles. If no profiles are listed, use the Database Mail Configuration Wizard to create a profile for your use.  
  
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
 Click to open Books Online to the [Troubleshooting Database Mail](https://msdn.microsoft.com/library/ms188663.aspx)topic.  
  
 [Database Mail Configuration Wizard](#DBWizard)  
  
##  <a name="Template"></a> Using Templates  
 **To create a Database Mail configuration script**  
  
1.  On the **View** menu, select **Template Explorer**.  
  
2.  In the **Template Explorer** window, expand the **Database Mail** folder.  
  
3.  Double-click **Simple Database Mail Configuration**. The template opens in a new query window.  
  
4.  On the **Query** menu, select **Specify Values for Template Parameters**. The **Replace Template Parameters** window opens.  
  
5.  Type values for the **profile_name**, **account_name**, **SMTP_servername**, **email_address**, and **display_name**. SQL Server Management Studio fills in the template with the values you provide.  
  
6.  Execute the script to create the configuration.  
  
7.  The script does not grant any database users access to the profile. Therefore, by default, the profile can only be used by members of the **sysadmin** fixed security role. For more information about granting access to profiles, see [sysmail_add_principalprofile_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md)  
  
  
