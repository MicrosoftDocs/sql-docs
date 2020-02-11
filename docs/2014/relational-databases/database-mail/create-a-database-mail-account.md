---
title: "Create a Database Mail Account | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Mail [SQL Server], accounts"
  - "accounts [Database Mail]"
ms.assetid: c07abbc6-fc6a-470b-8fa3-532f2e06b16a
author: stevestein
ms.author: sstein
manager: craigg
---
# Create a Database Mail Account
  Use either the **Database Mail Configuration Wizard** or [!INCLUDE[tsql](../../includes/tsql-md.md)] to create a Database Mail account.  
  
-   **Before you begin:**  [Prerequisites](#Prerequisites)  
  
-   **To Create a Database Mail Account, using:**  [Database Mail Configuration Wizard](#SSMSProcedure), [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [Next Steps to Configure the Database Mail](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Determine the server name and port number for the Simple Mail Transfer Protocol (SMTP) server you use to send e-mail.If the SMTP server requires authentication, determine the user name and password for the SMTP server.  
  
-   Optionally, you may also specify the type of the server and the port number for the server. The server type is always 'SMTP' for outgoing mail. Most SMTP servers use port 25, the default.  
  
##  <a name="SSMSProcedure"></a> Using Database Mail Configuration Wizard  
 **To create a Database Mail account using a Wizard**  
  
-   In Object Explorer, connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to configure Database Mail on, and expand the server tree.  
  
-   Expand the **Management** node  
  
-   Double Click Database Mail to open the Database Mail Configuration Wizard.  
  
-   On the **Select Configuration Task** page, select **Manage Database Mail accounts and profiles**, and click **Next**.  
  
-   On the **Manage Profiles and Accounts** page, select **Create a new account** and click **Next**.  
  
-   On the **New Account** page, specify the account name, description, mail server information, and authentication type. Click **Next**  
  
-   On the **Complete the Wizard** page, review the actions to be performed and click **Finish** to complete creating the new account.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To Create a Database Mail account using Transact-SQL**  
  
 Execute the stored procedure **msdb.dbo.sysmail_add_account_sp** to create the account and specify the following information:  
  
-   The name of the account to create.  
  
-   An optional description of the account.  
  
-   The e-mail address to show on outgoing e-mail messages.  
  
-   The display name to show on outgoing e-mail messages.  
  
-   The server name of the SMTP server.  
  
-   The user name to use to log on to the SMTP server, if the SMTP server requires authentication.  
  
-   The password to use to log on to the SMTP server, if the SMTP server requires authentication.  
  
 The following example creates a new Database Mail account.  
  
```  
EXECUTE msdb.dbo.sysmail_add_account_sp  
    @account_name = 'AdventureWorks Administrator',  
    @description = 'Mail account for administrative e-mail.',  
    @email_address = 'dba@Adventure-Works.com',  
    @display_name = 'AdventureWorks Automated Mailer',  
    @mailserver_name = 'smtp.Adventure-Works.com' ;  
```  
  
##  <a name="FollowUp"></a> Follow Up: Next Steps to Configuring the Database Mail  
  
-   [Create a Database Mail Profile](create-a-database-mail-profile.md)  
  
  
