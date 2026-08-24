---
title: "Create a Database Mail Account"
description: "Create a Database Mail account."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "Database Mail [SQL Server], accounts"
  - "accounts [Database Mail]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create a Database Mail account

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Use either the **Database Mail Configuration Wizard** or [!INCLUDE[tsql](../../includes/tsql-md.md)] to create a Database Mail account.  


<a id="Prerequisites"></a>

### Prerequisites

-   Determine the server name and port number for the Simple Mail Transfer Protocol (SMTP) server you use to send e-mail.If the SMTP server requires authentication, determine the user name and password for the SMTP server.  

-   Optionally, you might also specify the type of the server and the port number for the server. The server type is always 'SMTP' for outgoing mail. Most SMTP servers use port 25, the default.  

<a id="SSMSProcedure"></a>

<a id="using-database-mail-configuration-wizard"></a>

## Use Database Mail Configuration Wizard to create a Database Mail account

The following steps use SQL Server Management Studio (SSMS). Download the latest version of SSMS at [aka.ms/ssms](https://aka.ms/ssms).

1. Connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  

1. In Object Explorer, connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to configure Database Mail on, and expand the server tree.

1. Expand the **Management** node.

1. Double-click **Database Mail** and open the **Database Mail Configuration Wizard**.  

1. On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles**, and select **Next**.  

1. On the **Manage Profiles and Accounts** page, select **Create a new account** and select **Next**.  

1. On the **New Account** page, specify the account name, description, mail server information, and authentication type. Select **Next**.

1. On the **Complete the Wizard** page, review the actions to be performed and select **Finish** to complete creating the new account.  

<a id="TsqlProcedure"></a>

<a id="using-transact-sql"></a>

## Create a Database Mail account with Transact-SQL

To run T-SQL commands on your SQL Server instance, use [SQL Server Management Studio (SSMS)](https://aka.ms/ssms), the [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md), or your favorite T-SQL querying tool.

 Execute the system stored procedure `msdb.dbo.sysmail_add_account_sp` to create the account and specify the following information:  

1. The name of the account to create.  

1. An optional description of the account.  

1. The e-mail address to show on outgoing e-mail messages.  

1. The display name to show on outgoing e-mail messages.  

1. The server name of the SMTP server.  

1. The user name to use to log on to the SMTP server, if the SMTP server requires authentication.  

1. The password to use to log on to the SMTP server, if the SMTP server requires authentication.  

 The following example creates a new Database Mail account.  

```sql
EXECUTE msdb.dbo.sysmail_add_account_sp  
    @account_name = 'AdventureWorks Administrator',  
    @description = 'Mail account for administrative e-mail.',  
    @email_address = 'dba@Adventure-Works.com',  
    @display_name = 'AdventureWorks Automated Mailer',  
    @mailserver_name = 'smtp.Adventure-Works.com' ;  
```  

## Next step

> [!div class="nextstepaction"]
> [Create a Database Mail Profile](create-a-database-mail-profile.md)
