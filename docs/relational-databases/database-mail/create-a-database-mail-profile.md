---
title: "Create a Database Mail Profile"
description: "Create a Database Mail Profile"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "Database Mail [SQL Server], public profiles"
  - "profiles [SQL Server], Database Mail"
  - "public profiles [Database Mail]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Create a Database Mail Profile

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use either the **Database Mail Configuration Wizard** or [!INCLUDE[tsql](../../includes/tsql-md.md)] to create Database Mail public and private profiles. For more information about mail profiles, see [Database Mail Configuration Objects](database-mail-configuration-objects.md).

> [!TIP]
> Creating a database mail profile is not necessary in [!INCLUDE[ssazuremi-md](../../includes/ssazuremi-md.md)], which is already configured to look for a profile called `AzureManagedInstance_dbmail_profile`. For more information and a sample script, see [Azure SQL Managed Instance SQL Agent job notifications](/azure/azure-sql/managed-instance/job-automation-managed-instance#sql-agent-job-notifications).

<a id="Prerequisites"></a>

## Prerequisites

 Create one or more Database Mail accounts for the profile. For more information about creating Database Mail accounts, see [Create a Database Mail Account](create-a-database-mail-account.md).  

<a id="Security"></a>

## Security

 A public profile allows any user with access to the `msdb` database to send e-mail using that profile. A private profile can be used by a user or by a role. Granting roles access to profiles creates a more easily maintained architecture. To send mail you must be a member of the **DatabaseMailUserRole** in the `msdb` database, and have access to at least one Database Mail profile.  

<a id="Permissions"></a>

### Permissions

 The user creating the profiles accounts and executing stored procedures should be a member of the sysadmin fixed server role.  

<a id="SSMSProcedure"></a>

## Use Database Mail Configuration Wizard to create a Database Mail profile

The following steps use SQL Server Management Studio (SSMS). Download the latest version of SSMS at [aka.ms/ssms](https://aka.ms/ssms).

1. In **Object Explorer**, connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance where you want to configure Database Mail, and expand the server tree.

1. Expand the **Management** node  

1. Double-click **Database Mail** to open the Database Mail Configuration Wizard.  

1. On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles** option and select **Next**.  

1. On the **Manage Profiles and Accounts** page, select **Create a new profile** option, and select **Next**.  

1. On the **New Profile** page, specify the Profile name, Description and add accounts to be included in the profile, and select **Next**.  

1. On the **Complete the Wizard** page, review the actions to be performed and select **Finish** to complete creating the new profile.

### To configure a Database Mail private profile

1. Open the Database Mail Configuration Wizard.  

1. On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles** option, and select **Next**.  

1. On the **Manage Profiles and Accounts** page, select **Manage profile security** option and select **Next**.  

1. In the **Private Profiles** tab, select the check box for the profile you would like to configure and select **Next**.  

1. On the **Complete the Wizard** page, review the actions to be performed and select **Finish** to complete configuring the profile.  

### To configure a Database Mail public profile

1. Open the Database Mail Configuration Wizard.  

1. On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles** option, and select **Next**.  

1. On the **Manage Profiles and Accounts** page, select **Manage profile security** option and select **Next**.  

1. In the **Public Profiles** tab, select the check box for the profile you would like to configure and select **Next**.  

1. On the **Complete the Wizard** page, review the actions to be performed and select **Finish** to complete configuring the profile.  

<a id="PrivateProfile"></a> <a id="use-transact-sql"></a>

## Use Transact-SQL to create a database mail profile

To run T-SQL commands on your SQL Server instance, use [SQL Server Management Studio (SSMS)](https://aka.ms/ssms), the [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md), or your favorite T-SQL querying tool.

### Create a private database mail profile with T-SQL

1. Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Open a new query window.

1. To create a new profile, run the system stored procedure [sysmail_add_profile_sp](../system-stored-procedures/sysmail-add-profile-sp-transact-sql.md):

    ```sql
    EXECUTE msdb.dbo.sysmail_add_profile_sp
      @profile_name = 'Profile Name'  
    , @description = 'Description';
    ```

    In the previous script, `@profile_name` is the name of the profile, and `@description` is an optional friendly description of the profile. 

1. For each account, run the system stored procedure [sysmail_add_profileaccount_sp](../system-stored-procedures/sysmail-add-profileaccount-sp-transact-sql.md):  

    ```sql
    EXECUTE msdb.dbo.sysmail_add_profileaccount_sp
      @profile_name = 'Profile Name'
    , @account_name = 'Name of the account'  
    , @sequence_number = 'sequence number of the account within the profile.';
    ```

    In the previous sample script, `@profile_name` is the name of the profile, and `@account_name` is the name of the account to add to the profile, `@sequence_number` determines the order in which the accounts are used in the profile.  

1. For each database role or user that will send mail using this profile, grant access to the profile. To do this, run the system stored procedure [sysmail_add_principalprofile_sp](../system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md):  

    ```sql
    EXECUTE msdb.dbo.sysmail_add_principalprofile_sp
     @profile_name = 'Name of the profile'
    , @principal_name = 'Name of the database user or role'  
    , @is_default = 'Default profile enabled';
    ```  

    In the previous sample script, `@profile_name` is the name of the profile, `@principal_name` is the name of the database user or role, and `@is_default` determines whether this profile is the default for the database user or role.  

 The following example creates a Database Mail account, creates a Database Mail private profile, then adds the account to the profile and grants access to the profile to the **DBMailUsers** database role in the `msdb` database.  

```sql
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

<a id="PublicProfile"></a>

### Create a database mail public profile with T-SQL

1. Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Open a new query window. 

1. To create a new profile, run the system stored procedure [sysmail_add_profile_sp (Transact-SQL)](../system-stored-procedures/sysmail-add-profile-sp-transact-sql.md):

    ```sql
    EXECUTE msdb.dbo.sysmail_add_profile_sp
      @profile_name = 'Profile Name'  
    , @description = 'Description';
    ```

    In the previous script, `@profile_name` is the name of the profile, and `@description` is an optional description of the profile.

1. For each account, run the stored procedure [sysmail_add_profileaccount_sp (Transact-SQL)](../system-stored-procedures/sysmail-add-profileaccount-sp-transact-sql.md):

    ```sql
    EXECUTE msdb.dbo.sysmail_add_profileaccount_sp
      @profile_name = 'Name of the profile'  
    , @account_name* = 'Name of the account'  
    , @sequence_number* = 'sequence number of the account within the profile.'  
    ```

    In the previous sample script, `@profile_name` is the name of the profile, and `@account_name` is the name of the account to add to the profile, `@sequence_number` determines the order in which the accounts are used in the profile.  

1. To grant public access, run the stored procedure [sysmail_add_principalprofile_sp (Transact-SQL)](../system-stored-procedures/sysmail-add-principalprofile-sp-transact-sql.md):

    ```sql
    EXECUTE msdb.dbo.sysmail_add_principalprofile_sp
      @profile_name = 'Name of the profile' 
    , @principal_name = 'public or 0'  
    , @is_default = 'Default Profile enabled';
    ```

    In the previous sample script, `@profile_name` is the name of the profile, and `@principal_name` to indicate this is a public profile, `@is_default` determines whether this profile is the default for the database user or role.  

The following example creates a Database Mail account, creates a Database Mail private profile, then adds the account to the profile and grants public access to the profile.

```sql
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

## Related content

- [Configure SQL Server Agent](/ssms/agent/configure-sql-server-agent)
- [Configure SQL Server Agent mail to use Database Mail](configure-sql-server-agent-mail-to-use-database-mail.md)
- [General database mail troubleshooting steps](database-mail-general-troubleshooting.md)
