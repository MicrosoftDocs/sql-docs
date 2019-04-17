---
title: "Create a Database Mail Profile | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Mail [SQL Server], public profiles"
  - "profiles [SQL Server], Database Mail"
  - "public profiles [Database Mail]"
ms.assetid: 58ae749d-6ada-4f9c-bf00-de7c7a992a2d
author: stevestein
ms.author: sstein
manager: craigg
---
# Create a Database Mail Profile
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use either the **Database Mail Configuration Wizard** or [!INCLUDE[tsql](../../includes/tsql-md.md)] to create Database Mail public and private profiles. For more information about mail profiles see [Database Mail Profile](database-mail-configuration-objects.md).
  
-   **Before you Begin:** [Prerequisites](#Prerequisites), , [Security](#Security)  
  
-   **To Create a Database Mail private profile using:**  [Database Mail Configuration Wizard](#SSMSProcedure), [Transact-SQL](#PrivateProfile)  
  
-   **To Create a Database Mail public profile using:**  [Database Mail Configuration Wizard](#SSMSProcedure), [Transact-SQL](#PublicProfile)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Create one or more Database Mail accounts for the profile. For more information about creating Database Mail accounts, see [Create a Database Mail Account](../../relational-databases/database-mail/create-a-database-mail-account.md).  
  
###  <a name="Security"></a> Security  
 A public profile allows any user with access to the **msdb** database to send e-mail using that profile. A private profile can be used by a user or by a role. Granting roles access to profiles creates a more easily maintained architecture. To send mail you must be a member of the **DatabaseMailUserRole** in the **msdb** database, and have access to at least one Database Mail profile.  
  
####  <a name="Permissions"></a> Permissions  
 The user creating the profiles accounts and executing stored procedures should be a member of the sysadmin fixed server role.  
  
##  <a name="SSMSProcedure"></a> Using Database Mail Configuration Wizard  
 **To Create a Database Mail profile**  
  
-   In Object Explorer, connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to configure Database Mail on, and expand the server tree.  
  
-   Expand the **Management** node  
  
-   Double click Database Mail to open the Database Mail Configuration Wizard.  
  
-   On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles** option and click **Next**.  
  
-   On the **Manage Profiles and Accounts** page, select **Create a new profile** option, and click **Next**.  
  
-   On the **New Profile** page, specify the Profile name, Description and add accounts to be included in the profile, and click **Next**.  
  
-   On the **Complete the Wizard** page, review the actions to be performed and click **Finish** to complete creating the new profile.  
  
-   **To configure a Database Mail private profile:**  
  
    -   Open the Database Mail Configuration Wizard.  
  
    -   On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles** option, and click **Next**.  
  
    -   On the **Manage Profiles and Accounts** page, select **Manage profile security** option and click **Next**.  
  
    -   In the **Private Profiles** tab, select the check box for the profile you would like to configure and click **Next**.  
  
    -   On the **Complete the Wizard** page, review the actions to be performed and click **Finish** to complete configuring the profile.  
  
-   **To configure a Database Mail public profile:**  
  
    -   Open the Database Mail Configuration Wizard.  
  
    -   On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles** option, and click **Next**.  
  
    -   On the **Manage Profiles and Accounts** page, select **Manage profile security** option and click **Next**.  
  
    -   In the **Public Profiles** tab, select the check box for the profile you would like to configure and click **Next**.  
  
    -   On the **Complete the Wizard** page, review the actions to be performed and click **Finish** to complete configuring the profile.  
  
## Using Transact-SQL  
  
###  <a name="PrivateProfile"></a> To Create a Database Mail private profile  
  
-   Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   To create a new profile, run the system stored procedure [sysmail_add_profile_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-profile-sp-transact-sql.md) as follows:  
  
     **EXECUTEmsdb.dbo.sysmail_add_profile_sp**  
  
     *@profile_name* = '*Profile Name*'  
  
     *@description* = '*Desciption*'  
  
     where *@profile_name* is the name of the profile, and *@description* is the description of the profile. This parameter is optional.  
  
-   For each account, run the stored procedure [sysmail_add_profileaccount_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-profileaccount-sp-transact-sql.md) as follows:  
  
     **EXECUTEmsdb.dbo.sysmail_add_profileaccount_sp**  
  
     *@profile_name* = '*Name of the profile*'  
  
     *@account_name* = '*Name of the account*'  
  
     *@sequence_number* = '*sequence number of the account within the profile.* '  
  
     where *@profile_name* is the name of the profile, and *@account_name* is the name of the account to add to the profile, *@sequence_number* determines the order in which the accounts are used in the profile.  
  
-   For each database role or user that will send mail using this profile, grant access to the profile. To do this, run the stored procedure [sysmail_add_principalprofile_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md) as follows:  
  
     **EXECUTEmsdb.sysmail_add_principalprofile_sp**  
  
     *@profile_name* = '*Name of the profile*'  
  
     *@ principal_name* = '*Name of the database user or role*'  
  
     *@is_default* = '*Default Profile status* '  
  
     where *@profile_name* is the name of the profile, and *@principal_name* is the name of the database user or role, *@is_default* determines the whether this profile is the default for the database user or role.  
  
 The following example creates a Database Mail account, creates a Database Mail private profile, then adds the account to the profile and grants access to the profile to the **DBMailUsers** database role in the **msdb** database.  
  
```  
-- Create a Database Mail account  
EXECUTE msdb.dbo.sysmail_add_account_sp  
    @account_name = 'AdventureWorks Administrator',  
    @description = 'Mail account for administrative e-mail.',  
    @email_address = 'dba@Adventure-Works.com',  
    @replyto_address = 'danw@Adventure-Works.com',  
    @display_name = 'AdventureWorks Automated Mailer',  
    @mailserver_name = 'smtp.Adventure-Works.com' ;  
  
-- Create a Database Mail profile  
EXECUTE msdb.dbo.sysmail_add_profile_sp  
    @profile_name = 'AdventureWorks Administrator Profile',  
    @description = 'Profile used for administrative mail.' ;  
  
-- Add the account to the profile  
EXECUTE msdb.dbo.sysmail_add_profileaccount_sp  
    @profile_name = 'AdventureWorks Administrator Profile',  
    @account_name = 'AdventureWorks Administrator',  
    @sequence_number =1 ;  
  
-- Grant access to the profile to the DBMailUsers role  
EXECUTE msdb.dbo.sysmail_add_principalprofile_sp  
    @profile_name = 'AdventureWorks Administrator Profile',  
    @principal_name = 'ApplicationUser',  
    @is_default = 1 ;  
```  
  
###  <a name="PublicProfile"></a> To Create a Database Mail public profile  
  
-   Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
-   To create a new profile, run the system stored procedure [sysmail_add_profile_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-profile-sp-transact-sql.md) as follows:  
  
     **EXECUTEmsdb.dbo.sysmail_add_profile_sp**  
  
     *@profile_name* = '*Profile Name*'  
  
     *@description* = '*Desciption*'  
  
     where *@profile_name* is the name of the profile, and *@description* is the description of the profile. This parameter is optional.  
  
-   For each account, run the stored procedure [sysmail_add_profileaccount_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-profileaccount-sp-transact-sql.md) as follows:  
  
     **EXECUTEmsdb.dbo.sysmail_add_profileaccount_sp**  
  
     *@profile_name* = '*Name of the profile*'  
  
     *@account_name* = '*Name of the account*'  
  
     *@sequence_number* = '*sequence number of the account within the profile.* '  
  
     where *@profile_name* is the name of the profile, and *@account_name* is the name of the account to add to the profile, *@sequence_number* determines the order in which the accounts are used in the profile.  
  
-   To grant public access, run the stored procedure [sysmail_add_principalprofile_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md) as follows:  
  
     **EXECUTEmsdb.sysmail_add_principalprofile_sp**  
  
     *@profile_name* = '*Name of the profile*'  
  
     *@ principal_name* = '**public** or **0**'  
  
     *@is_default* = '*Default Profile status* '  
  
     where *@profile_name* is the name of the profile, and *@principal_name* to indicate this is a public profile, *@is_default* determines the whether this profile is the default for the database user or role.  
  
 The following example creates a Database Mail account, creates a Database Mail private profile, then adds the account to the profile and grants public access to the profile.  
  
```  
-- Create a Database Mail account  
  
EXECUTE msdb.dbo.sysmail_add_account_sp  
    @account_name = 'AdventureWorks Public Account',  
    @description = 'Mail account for use by all database users.',  
    @email_address = 'db_users@Adventure-Works.com',  
    @replyto_address = 'danw@Adventure-Works.com',  
    @display_name = 'AdventureWorks Automated Mailer',  
    @mailserver_name = 'smtp.Adventure-Works.com' ;  
  
-- Create a Database Mail profile  
  
EXECUTE msdb.dbo.sysmail_add_profile_sp  
    @profile_name = 'AdventureWorks Public Profile',  
    @description = 'Profile used for administrative mail.' ;  
  
-- Add the account to the profile  
  
EXECUTE msdb.dbo.sysmail_add_profileaccount_sp  
    @profile_name = 'AdventureWorks Public Profile',  
    @account_name = 'AdventureWorks Public Account',  
    @sequence_number =1 ;  
  
-- Grant access to the profile to all users in the msdb database  
  
EXECUTE msdb.dbo.sysmail_add_principalprofile_sp  
    @profile_name = 'AdventureWorks Public Profile',  
    @principal_name = 'public',  
    @is_default = 1 ;  
```  
  
  
