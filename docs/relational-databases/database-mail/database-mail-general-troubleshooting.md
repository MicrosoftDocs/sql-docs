---
title: "General Database mail troubleshooting steps | Microsoft Docs"
ms.custom: ""
ms.date: "04/22/2019"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# General Database mail troubleshooting steps 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Troubleshooting Database Mail involves checking the following general areas of the Database Mail system. These procedures are presented in a logical order, but can be evaluated in any order.

## Determine if database mail is enabled

1. In SQL Server Management Studio, connect to an instance of SQL Server by using a query editor window, and then execute the following code:

    ```sql
    sp_configure 'show advanced', 1; 
    GO
    RECONFIGURE;
    GO
    sp_configure;
    GO
    ```

   In the results pane, confirm that the run_value for Database Mail XPs is set to 1.
   If the run_value is not 1, Database Mail is not enabled. Database Mail is not automatically enabled to reduce the number of features available for attack by a malicious user. For more information, see [Understanding Surface Area Configuration](../security/surface-area-configuration.md).

1. If you decide that it is appropriate to enable Database Mail, execute the following code:

    ```sql
    sp_configure 'Database Mail XPs', 1; 
    GO
    RECONFIGURE;
    GO
    ```

1. To restore the sp_configure procedure to its default state, which does not show advanced options, execute the following code:

    ```sql 
    sp_configure 'show advanced', 0; 
    GO
    RECONFIGURE;
    GO
    ```

## Determine if users are properly configured to send database mail

1. To send Database Mail, users must be a member of the DatabaseMailUserRole. Members of the sysadmin fixed server role and msdbdb_owner role are automatically members of the DatabaseMailUserRole role. To list all other members of the DatabaseMailUserRole execute the following statement:

    ```sql
    EXEC msdb.sys.sp_helprolemember 'DatabaseMailUserRole';
    ```

1. To add users to the DatabaseMailUserRole role, use the following statement:

    ```sql
    sp_addrolemember @rolename = 'DatabaseMailUserRole'
   ,@membername = '<database user>';
    ```

1. To send Database Mail, users must have access to at least one Database Mail profile. To list the users (principals) and the profiles to which they have access, execute the following statement.

    ```sql
    EXEC msdb.dbo.sysmail_help_principalprofile_sp;
    ```

1. Use the Database Mail Configuration Wizard to create profiles and grant access to profiles to users.
 
## Confirm that database mail is started

1. The [Database Mail External Program](database-mail-external-program.md) is activated when there are e-mail messages to be processed. When there have been no messages to send for the specified time-out period, the program exits. To confirm the Database Mail activation is started, execute the following statement:

    ```sql
    EXEC msdb.dbo.sysmail_help_status_sp;
    ```
1. If the Database Mail activation is not started, execute the following statement to start it:

    ```sql
    EXEC msdb.dbo.sysmail_start_sp;
    ```

1. If the Database Mail external program is started, check the status of the mail queue with the following statement:

    ```sql
    EXEC msdb.dbo.sysmail_help_queue_sp @queue_type = 'mail';
    ```
  
##  <a name="RelatedContent"></a> Database Mail Component Topics  
  
-   [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)  
  
-   [Database Mail Messaging Objects](../../relational-databases/database-mail/database-mail-messaging-objects.md)  
  
-   [Database Mail External Program](../../relational-databases/database-mail/database-mail-external-program.md)  
  
-   [Database Mail Log and Audits](../../relational-databases/database-mail/database-mail-log-and-audits.md)  
  
-   [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)  
  
  
  
