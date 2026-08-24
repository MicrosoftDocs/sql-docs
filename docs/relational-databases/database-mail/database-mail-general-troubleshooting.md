---
title: "General Database Mail Troubleshooting"
description: "Troubleshoot database mail with these steps."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf
ms.date: 02/20/2026
ms.service: sql
ms.topic: troubleshooting
helpviewer_keywords:
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# General database mail troubleshooting steps

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Troubleshooting Database Mail involves checking the following general areas of the Database Mail system. These procedures are presented in a logical order, but can be evaluated in any order.

## Permissions

You must be a member of the **sysadmin** fixed server role to troubleshoot all aspects of Database Mail. Users who aren't members of the **sysadmin** fixed server role can only obtain information about the e-mails they attempt to send, not about e-mails sent by other users.

## Verify service broker is enabled for msdb

Database mail requires the Service Broker to be enabled for the `msdb` database. 

1. In [SQL Server Management Studio](https://aka.ms/ssms), connect to an instance of SQL Server by using a query editor window. Verify if the service broker is enabled on `msdb` with the following T-SQL script:

   ```sql
   SELECT is_broker_enabled FROM sys.databases WHERE name = 'msdb' ; -- should be 1
   ```

   - If enabled, continue to [Verify database mail is enabled](#verify-database-mail-is-enabled) and [Verify database mail is started](#verify-database-mail-is-started).
   - If not enabled, the service broker must be enabled. 

   The following sample script requires exclusive access to the `msdb` system databases, however, so this might not be feasible to execute during typical business hours. 

1. Stop the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service so Service Broker can obtain the necessary lock. 

1. Execute the following to enable Service Broker on `msdb`. For more information, see [ALTER DATABASE ... SET ENABLE_BROKER](../../t-sql/statements/alter-database-transact-sql-set-options.md#enable_broker).

   ```sql
   ALTER DATABASE msdb SET ENABLE_BROKER;
   ```

1. Restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service.

<a id="is-database-mail-enabled"></a>

## Verify database mail is enabled

1. In [SQL Server Management Studio](https://aka.ms/ssms), connect to an instance of SQL Server by using a query editor window. Verify database mail is enabled with the following code:

    ```sql
    sp_configure 'show advanced', 1; 
    GO
    RECONFIGURE;
    GO
    sp_configure;
    GO
    ```

   In the results pane, confirm that the `run_value` for [Database Mail XPs (server configuration option)](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) is set to `1`.
 
   - If the `run_value` is `1`, continue to [Verify database mail is started](#verify-database-mail-is-started).
   - If the `run_value` isn't `1`, Database Mail isn't enabled. 

   Database Mail isn't automatically enabled to reduce the number of features available for attack by a malicious user. For more information, see [Surface area configuration](../security/surface-area-configuration.md).

1. If you decide that it's appropriate to enable Database Mail, execute the following code:

    ```sql
    sp_configure 'show advanced', 1; 
    GO
    RECONFIGURE;
    GO
    sp_configure 'Database Mail XPs', 1; 
    GO
    RECONFIGURE;
    GO
    ```

    To restore the `sp_configure` procedure to its default state, which doesn't show advanced options, execute the following code:

    ```sql
    sp_configure 'show advanced', 0; 
    GO
    RECONFIGURE;
    GO
    ```

<a id="is-database-mail-started"></a>

## Verify database mail is started

The [Database Mail External Program](database-mail-external-program.md) is activated when there are e-mail messages to be processed. When there have been no messages to send for the specified time-out period, the program exits.

1. In [SQL Server Management Studio](https://aka.ms/ssms), connect to an instance of SQL Server by using a query editor window. To verify the Database Mail external program is started, execute the following statement:

    ```sql
    EXEC msdb.dbo.sysmail_help_status_sp;
    ```

1. If the Database Mail status is not `STARTED`, execute the following statement to start it:

    ```sql
    EXEC msdb.dbo.sysmail_start_sp;
    ```

1. If the Database Mail external program is started, check the status of the mail queue with the following statement:

    ```sql
    EXEC msdb.dbo.sysmail_help_queue_sp @queue_type = 'mail';
    ```

    The mail queue should have the state of `RECEIVES_OCCURRING`. The status queue might vary from moment to moment. If the mail queue state isn't `RECEIVES_OCCURRING`, try restarting the queue. Stop the queue using the following statement:

    ```sql
    EXEC msdb.dbo.sysmail_stop_sp;
    ```

    Then start the queue using the following statement:

    ```sql
    EXEC msdb.dbo.sysmail_start_sp;
    ```
        
    > [!NOTE]
    > Use the `length` column in the result set of `sysmail_help_queue_sp` to determine the number of e-mails in the mail queue.

## Are users properly configured to send mail?

1. To send Database Mail, users must be a member of the **DatabaseMailUserRole** database role in the `msdb` database. Members of the sysadmin fixed server role and `msdb` **db_owner** role are automatically members of the **DatabaseMailUserRole** role. To list all other members of the **DatabaseMailUserRole** execute the following statement:

    ```sql
    EXEC msdb.sys.sp_helprolemember 'DatabaseMailUserRole';
    ```

1. To add users to the **DatabaseMailUserRole** role, use the following statement:

    ```sql
    USE msdb;
    GO

    sp_addrolemember @rolename = 'DatabaseMailUserRole'
    ,@membername = '<database user>';
    ```

1. To send Database Mail, users must have access to at least one Database Mail profile. To list the users (principals) and the profiles to which they have access, execute the following statement.

    ```sql
    EXEC msdb.dbo.sysmail_help_principalprofile_sp;
    ```

1. Use the Database Mail Configuration Wizard to [create profiles](create-a-database-mail-profile.md) and grant access to profiles to users.

## Do problems affect some or all accounts?

If you've determined that some but not all profiles can send mail, then you might have problems with the Database Mail accounts used by the problem profiles. 

1. To determine which accounts are successful in sending mail, execute the following statement:

    ```sql
    SELECT sent_account_id, sent_date FROM msdb.dbo.sysmail_sentitems;
    ```

1. If a non-working profile doesn't use any of the accounts listed, then it's possible that all the accounts available to the profile aren't working properly. To test individual accounts, use the Database Mail Configuration Wizard to create a new profile with a single account, and then use the Send Test E-Mail dialog box to send mail using the new account. 

1. To view the error messages returned by Database Mail, execute the following statement:

    ```sql
    SELECT * FROM msdb.dbo.sysmail_event_log;
    ```

   > [!NOTE]
   > Database Mail considers mail to be sent when it is successfully delivered to a SMTP mail server. Subsequent errors, such as an invalid recipient e-mail address, can still prevent mail from being delivered, but will not be contained in the Database Mail log.

## Retry mail delivery

1. If you've determined that the Database Mail is failing because the SMTP server can't be reliably reached, you might increase your successful mail delivery rate by increasing the number of times Database Mail attempts to send each message. Start the Database Mail Configuration Wizard, and select the View or change system parameters option. Alternatively, you can associate more accounts to the profile so upon failover from the primary account, Database Mail uses the failover account to send e-mails.
1. On the Configure System Parameters page, the default values of five times for the Account Retry Attempts and 60 seconds for the Account Retry Delay means that message delivery will fail if the SMTP server can't be reached in 5 minutes. Increase these parameters to lengthen the amount of time before message delivery fails.

    > [!NOTE]
    > When large numbers of messages are being sent, large default values might increase reliability, but will substantially increase the use of resources as many messages are attempted to be delivered over and over again. Address the root problem by resolving the network or SMTP server problem that prevents Database Mail from contacting the SMTP server promptly.

<a id="RelatedContent"></a>

## Related content

- [Database Mail Configuration Objects](database-mail-configuration-objects.md)
- [Database Mail Messaging Objects](database-mail-messaging-objects.md)
- [Database Mail external program](database-mail-external-program.md)
- [Database Mail log and audits](database-mail-log-and-audits.md)
- [Configure SQL Server Agent](/ssms/agent/configure-sql-server-agent)
- [Configure SQL Server Agent mail to use Database Mail](configure-sql-server-agent-mail-to-use-database-mail.md)
