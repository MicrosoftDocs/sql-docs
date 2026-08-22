---
title: "Database Mail Configuration Objects"
description: "Describes the Database Mail configuration objects for configuration of the settings that Database mail should use when sending an email from your database application or SQL Agent."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.sqlimail.profileandaccountmanagement.f1"
  - "sql13.swb.sqlimail.newaccount.f1"
  - "sql13.swb.sqlimail.manageexistingprofile.f1"
  - "sql13.swb.sqlimail.addaccounttoprofile.f1"
  - "sql13.swb.sqlmailconfiguration.f1"
  - "sql13.swb.sqlimail.manageprofilesecurity.profileview.f1"
  - "sql13.swb.sqlimail.welcome.f1"
  - "sql13.swb.sqlimail.manageprofilesecurity.principalview.f1"
  - "sql13.swb.sqlimail.newsqlimailaccount.f1"
  - "sql13.swb.sqlimail.selectconfiguration.f1"
  - "sql13.swb.sqlimail.newprofile.f1"
  - "sql13.swb.sqlimail.manageexistingaccount.f1"
  - "sql13.swb.sqlimail.completewizard.f1"
  - "sql13.swb.sqlimail.configuresystem.f1"
helpviewer_keywords:
  - "Database Mail [SQL Server], configuration objects"
  - "Database Mail [SQL Server], accounts"
  - "configuration objects [Database Mail]"
  - "Database Mail [SQL Server], profiles"
  - "profiles [SQL Server], Database Mail"
  - "accounts [Database Mail]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Database Mail Configuration Objects

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Database configuration objects provide a way for you to configure the settings that Database mail should use when sending an email from your database application or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.  

 User can be granted secure access to Database Mail profiles. Profiles use Database Mail accounts.

<a id="VisualElement"></a> <>

 The following illustration shows two profiles, three accounts, and three users. User 1 has access to Profile 1, which uses Account 1 and Account 2. User 3 has access to Profile 2, which uses Account 2 and Account 3. User 2 has access to both Profile 1 and Profile 2.

 :::image type="content" source="media/database-mail-configuration-objects/database-mail-profile-account.png" alt-text="Illustration of the relationship of users, profiles, and accounts in Database Mail.":::  

<a id="DBAccount"></a>

## Database Mail account

 A Database Mail account contains the information that Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses to send e-mail messages to an SMTP server. Each account contains information for one e-mail server.  

 A Database Mail supports three methods of authentication to communicate with an SMTP server:

- Windows Authentication: Database Mail uses the credentials of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] Windows service account for authentication on the SMTP server.  

- Basic Authentication: Database Mail uses the username and password specified to authenticate on the SMTP server. 

  -   [Basic authentication for Exchange Online is disabled in all tenants](/exchange/clients-and-mobile-in-exchange-online/deprecation-of-basic-authentication-exchange-online#re-enabling-and-opting-out-of-proactive-protection).

-   Anonymous Authentication: The SMTP server does not require any authentication. Database Mail will not use any credentials to authenticate on the SMTP server.  

 Account information is stored in the `msdb` system database, including the name, description, e-mail address, reply-to email, email server and server authentication information.
 
 The Database Mail Configuration Wizard provides a convenient way to create and manage accounts. You can also use the configuration stored procedures in `msdb` to create and manage accounts.  

<a id="DBProfile"></a>

## Database Mail profile

 A Database Mail profile is an ordered collection of related Database Mail accounts.  Applications that send e-mail using Database Mail specify profiles, instead of using accounts directly. 

 Separating information about the individual e-mail servers from the objects that the application uses improves flexibility and reliability: profiles provide automatic failover, so that if one e-mail server is unresponsive, Database Mail can automatically send mail to another e-mail server. Database administrators can add, remove, or reconfigure accounts without requiring changes to application code or job steps.  

 Profiles also help database administrators control access to e-mail. Membership in the **DatabaseMailUserRole** is required to send Database Mail. Profiles provide additional flexibility for administrators to control who sends mail and which accounts are used.  

 A profile can be public or private. By default, a profile is private, and allows access only to members of the **sysadmin** fixed server role.

 - **Public profiles** are available for all members of the **DatabaseMailUserRole** database role in the `msdb` system database. They allow all members of the **DatabaseMailUserRole** role to send e-mail using the profile.  

 - **Private profiles** are defined for security principals in the `msdb` database. They allow only specified database users, roles, and members of the **sysadmin** fixed server role to send e-mail using the profile. To use a private profile, **sysadmin** must grant users permission to use the profile. Additionally, EXECUTE permission on the `sp_send_dbmail` stored procedure is only granted to members of the **DatabaseMailUserRole**. A system administrator must add the user to the **DatabaseMailUserRole** database role for the user to send e-mail messages.  

### Profile sequence numbers

 Profiles improve reliability in cases where an e-mail server becomes unreachable or unable to process messages. Each account in the profile has a sequence number. The sequence number determines the order in which Database Mail uses accounts in the profile. 

 For a new e-mail message, Database Mail uses the last account that sent a message successfully, or the account that has the lowest sequence number if no message has yet been sent. Should that account fail, Database Mail uses the account with the next highest sequence number, and so on until either Database Mail sends the message successfully, or the account with the highest sequence number fails. If the account with the highest sequence number fails, the Database Mail pauses attempts to send the mail for the amount of time configured in the **AccountRetryDelay** parameter of **sysmail_configure_sp**, then starts the process of attempting to send the mail again, starting with the lowest sequence number. 

 Use the `AccountRetryAttempts` parameter of `sysmail_configure_sp` to configure the number of times that the external mail process attempts to send the e-mail message using each account in the specified profile.  

 If more than one account exists with the same sequence number, Database Mail only uses one of those accounts for a given e-mail message. In this case, Database Mail makes no guarantees as to which of the accounts is used for that sequence number or that the same account is used from message to message.

<a id="RelatedTasks"></a>

## Database Mail configuration Tasks

 The following table describes the Database Mail configuration tasks.  

|Configuration Task|Topic Link|  
|------------------------|----------------|  
|Describes how to create a Database Mail accounts|[Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md)|  
|Describes how to Create Database Mail profiles|[Create a Database Mail Profile](../../relational-databases/database-mail/create-a-database-mail-profile.md)|  
|Describes how to Configure Database mail|[Configure Database Mail](../../relational-databases/database-mail/configure-database-mail.md)|  

<a name="Add_Tasks"></a>

### Additional Database Configuration tasks (system stored procedures)  
 Database Mail configuration stored procedures are located in the `msdb` database.  

 The following tables list the stored procedures used for configuring and managing Database Mail.  

#### Database Mail settings

|Name|Description|  
|----------|-----------------|  
|[sysmail_configure_sp (Transact-SQL)](../system-stored-procedures/sysmail-configure-sp-transact-sql.md)|Changes configuration settings for Database Mail.|  
|[sysmail_help_configure_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-configure-sp-transact-sql.md)|Displays configuration settings for Database Mail.|  

#### Accounts and profiles

|Name|Description|  
|----------|-----------------|  
|[sysmail_add_profileaccount_sp (Transact-SQL)](../system-stored-procedures/sysmail-add-profileaccount-sp-transact-sql.md)|Adds a mail account to a Database Mail profile.|  
|[sysmail_delete_account_sp (Transact-SQL)](../system-stored-procedures/sysmail-delete-account-sp-transact-sql.md)|Deletes a Database Mail account.|  
|[sysmail_delete_profile_sp (Transact-SQL)](../system-stored-procedures/sysmail-delete-profile-sp-transact-sql.md)|Deletes a Database Mail profile.|  
|[sysmail_delete_profileaccount_sp (Transact-SQL)](../system-stored-procedures/sysmail-delete-profileaccount-sp-transact-sql.md)|Removes an account from a Database Mail profile.|  
|[sysmail_help_account_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-account-sp-transact-sql.md)|Lists information about Database Mail accounts.|  
|[sysmail_help_profile_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-profile-sp-transact-sql.md)|Lists information about one or more Database Mail profiles.|  
|[sysmail_help_profileaccount_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-profileaccount-sp-transact-sql.md)|Lists the accounts associated with one or more Database Mail profiles.|  
|[sysmail_update_account_sp (Transact-SQL)](../system-stored-procedures/sysmail-update-account-sp-transact-sql.md)|Updates the information in an existing Database Mail account.|  
|[sysmail_update_profile_sp (Transact-SQL)](../system-stored-procedures/sysmail-update-profile-sp-transact-sql.md)|Changes the description or name of a Database Mail profile.|  
|[sysmail_update_profileaccount_sp (Transact-SQL)](../system-stored-procedures/sysmail-update-profileaccount-sp-transact-sql.md)|Updates the sequence number of an account within a Database Mail profile.|  

#### Security

|Name|Description|  
|----------|-----------------|  
|[sysmail_add_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md)|Grants permission for a database principal to use a Database Mail profile.|  
|[sysmail_delete_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-delete-principalprofile-sp-transact-sql.md)|Removes permission for a database user to use a public or private Database Mail profile.|  
|[sysmail_help_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-principalprofile-sp-transact-sql.md)|Lists Database Mail profile information for a given database user.|  
|[sysmail_update_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-update-principalprofile-sp-transact-sql.md)|Updates the permission information for a given database user.|  

#### System state

|Name|Description|  
|----------|-----------------|  
|[sysmail_start_sp (Transact-SQL)](../system-stored-procedures/sysmail-start-sp-transact-sql.md)|Starts the Database Mail external program and the associated SQL Service Broker queue.|  
|[sysmail_stop_sp (Transact-SQL)](../system-stored-procedures/sysmail-stop-sp-transact-sql.md)|Stops the Database Mail external program and the associated SQL Service Broker queue.|  
|[sysmail_help_status_sp (Transact-SQL)](../system-stored-procedures/sysmail-help-status-sp-transact-sql.md)|Indicates if Database Mail is started.|  

<a id="RelatedContent"></a>

## Related content

- [Database Mail log and audits](database-mail-log-and-audits.md)
