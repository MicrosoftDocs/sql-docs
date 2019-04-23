---
title: "General database mail troubleshooting | Microsoft Docs"
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
# General database mail troubleshooting steps 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Troubleshooting Database Mail involves checking the following general areas of the Database Mail system. These procedures are presented in a logical order, but can be evaluated in any order.

## Permissions

You must be a member of the sysadmin fixed server role to troubleshoot all aspects of Database Mail. Users who are not members of the sysadmin fixed server role can only obtain information about the e-mails they attempt to send, not about e-mails sent by other users.

## Is database mail enabled

1. In SQL Server Management Studio, connect to an instance of SQL Server by using a query editor window, and then execute the following code:

    ```sql
    sp_configure 'show advanced', 1; 
    GO
    RECONFIGURE;
    GO
    sp_configure;
    GO
    ```

   In the results pane, confirm that the run_value for [Database Mail XPs](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) is set to 1.
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

## Are users properly configured to send mail

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
 
## Is database mail started

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
  
   The mail queue should have the state of RECEIVES_OCCURRING. The status queue may vary from moment to moment. If the mail queue state is not RECEIVES_OCCURRING, try stopping the queue using sysmail_stop_sp and then starting the queue using sysmail_start_sp.

  > [!NOTE]
  >  Use the length column in the result set of sysmail_help_queue_sp to determine the number of e-mails in the Mail queue.

## Do problems affect some or all accounts

1. If you have determined that some but not all profiles can send mail, then you may have problems with the Database Mail accounts used by the problem profiles. To determine which accounts are successful in sending mail, execute the following statement:

    ```sql
    SELECT sent_account_id, sent_date FROM msdb.dbo.sysmail_sentitems;
    ```

1. If a non-working profile does not use any of the accounts listed, then it is possible that all the accounts available to the profile are not working properly. To test individual accounts, use the Database Mail Configuration Wizard to create a new profile with a single account, and then use the Send Test E-Mail dialog box to send mail using the new account. 
1. To view the error messages returned by Database Mail, execute the following statement:

    ```sql
    SELECT * FROM msdb.dbo.sysmail_event_log;
    ```

   > [!NOTE]
   > Database Mail considers mail to be sent, when it is successfully delivered to a SMTP mail server. Subsequent errors, such as an invalid recipient e-mail address, can still prevent mail from being delivered, but will not be contained in the Database Mail log.

## Retry mail delivery

1. If you have determined that the Database Mail is failing because the SMTP server cannot be reliably reached, you may be able to increase your successful mail delivery rate by increasing the number of times Database Mail attempts to send each message. Start the Database Mail Configuration Wizard, and select the View or change system parameters option. Alternatively, you can associate more accounts to the profile so upon failover from the primary account, Database Mail will use the failover account to send e-mails.
1. On the Configure System Parameters page, the default values of five times for the Account Retry Attempts and 60 seconds for the Account Retry Delay means that message delivery will fail if the SMTP server cannot be reached in 5 minutes. Increase these parameters to lengthen the amount of time before message delivery fails.

    > [!NOTE]
    > When large numbers of messages are being sent, large default values may increase reliability, but will substantially increase the use of resources as many messages are attempted to be delivered over and over again. Address the root problem by resolving the network or SMTP server problem that prevents Database Mail from contacting the SMTP server promptly.



##  <a name="RelatedContent"></a> Database Mail Component Topics  
  
-   [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)  
  
-   [Database Mail Messaging Objects](../../relational-databases/database-mail/database-mail-messaging-objects.md)  
  
-   [Database Mail External Program](../../relational-databases/database-mail/database-mail-external-program.md)  
  
-   [Database Mail Log and Audits](../../relational-databases/database-mail/database-mail-log-and-audits.md)  
  
-   [Configure SQL Server Agent Mail to Use Database Mail](../../relational-databases/database-mail/configure-sql-server-agent-mail-to-use-database-mail.md)  
  
  
  
