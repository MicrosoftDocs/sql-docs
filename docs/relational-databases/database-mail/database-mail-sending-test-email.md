---
title: "Send a Test Email with Database Mail"
description: "Send a test email with database mail"
author: MashaMSFT
ms.author: mathoma
ms.date: 05/16/2025
ms.service: sql
ms.topic: how-to
helpviewer_keywords:
  - "architecture [SQL Server], Database Mail"
  - "Database Mail [SQL Server], architecture"
  - "Database Mail [SQL Server], components"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Send a test email with database mail

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Use the Send Test E-Mail dialog box to test the ability to send mail using a specific profile.

## Permissions

You must be a member of the sysadmin fixed server role to use the Send Test E-Mail dialog box. Users who are not members of the sysadmin fixed server role can test Database Mail using the [sp_send_dbmail](../system-stored-procedures/sp-send-dbmail-transact-sql.md) procedure.

## Procedure

1. Using Object Explorer in [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms), connect to an instance of SQL Server Database Engine where Database Mail is configured, expand Management, right-click Database Mail, and then select Send Test E-Mail. If no Database Mail profiles exist, a dialog prompts the user to create a profile and opens the Database Mail Configuration Wizard.
1. In the **Send Test E-Mail** dialog box, in the Database Mail Profile box select the profile you want to test.
1. In the **To** box, type the e-mail name of the recipient of the test e-mail.
1. In the **Subject** box, type the subject line for the test e-mail. Change the default subject to better identify your e-mail for troubleshooting.
1. In the **Body** box, type to body of the test e-mail. Change the default subject to better identify your e-mail for troubleshooting.
1. Select **Send Test E-Mail** to send the test e-mail to the Database Mail queue.
1. Sending the test e-mail opens the Database Mail Test E-Mail dialog box. Make a note of the number displayed in the Sent e-mail box. This is the mailitem_id of the test message. Select OK.
1. On the Toolbar select New Query to open a Query Editor window. Run the following T-SQL statement to determine the status of the test e-mail message:

    ```sql
    SELECT * FROM msdb.dbo.sysmail_allitems 
    WHERE mailitem_id = <the mailitem_id from the previous step> ;
    ```

    The `sent_status` column indicates if the test e-mail message was sent.

1. If errors occurred, execute the following statement to view the error message:

    ```sql
    SELECT * FROM msdb.dbo.sysmail_event_log 
    WHERE mailitem_id = <the mailitem_id from the previous step> ;
    ```

<a id="RelatedContent"></a>

## Related content

- [Database Mail Configuration Objects](database-mail-configuration-objects.md)
- [Database Mail Messaging Objects](database-mail-messaging-objects.md)
- [Database Mail External Program](database-mail-external-program.md)
- [Database Mail Log and Audits](database-mail-log-and-audits.md)
- [Configure SQL Server Agent mail to use Database Mail](configure-sql-server-agent-mail-to-use-database-mail.md)
