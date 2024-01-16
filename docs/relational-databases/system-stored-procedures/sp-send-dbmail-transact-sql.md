---
title: "sp_send_dbmail (Transact-SQL)"
description: "Sends an e-mail message to the specified recipients."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sendmail_sp_TSQL"
  - "sendmail_sp"
  - "SP_SEND_DBMAIL_TSQL"
helpviewer_keywords:
  - "sp_send_dbmail"
dev_langs:
  - "TSQL"
---
# sp_send_dbmail (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Sends an e-mail message to the specified recipients. The message might include a query result set, file attachments, or both. When mail is successfully placed in the Database Mail queue, `sp_send_dbmail` returns the `mailitem_id` of the message. This stored procedure is in the `msdb` database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_send_dbmail [ [ @profile_name = ] 'profile_name' ]
    [ , [ @recipients = ] 'recipients [ ; ...n ]' ]
    [ , [ @copy_recipients = ] 'copy_recipient [ ; ...n ]' ]
    [ , [ @blind_copy_recipients = ] 'blind_copy_recipient [ ; ...n ]' ]
    [ , [ @from_address = ] 'from_address' ]
    [ , [ @reply_to = ] 'reply_to' ]
    [ , [ @subject = ] N'subject' ]
    [ , [ @body = ] N'body' ]
    [ , [ @body_format = ] 'body_format' ]
    [ , [ @importance = ] 'importance' ]
    [ , [ @sensitivity = ] 'sensitivity' ]
    [ , [ @file_attachments = ] N'attachment [ ; ...n ]' ]
    [ , [ @query = ] N'query' ]
    [ , [ @execute_query_database = ] 'execute_query_database' ]
    [ , [ @attach_query_result_as_file = ] attach_query_result_as_file ]
    [ , [ @query_attachment_filename = ] N'query_attachment_filename' ]
    [ , [ @query_result_header = ] query_result_header ]
    [ , [ @query_result_width = ] query_result_width ]
    [ , [ @query_result_separator = ] 'query_result_separator' ]
    [ , [ @exclude_query_output = ] exclude_query_output ]
    [ , [ @append_query_error = ] append_query_error ]
    [ , [ @query_no_truncate = ] query_no_truncate ]
    [ , [ @query_result_no_padding = ] @query_result_no_padding ]
    [ , [ @mailitem_id = ] mailitem_id ] [ OUTPUT ]
[ ; ]
```

## Arguments

#### [ @profile_name = ] '*profile_name*'

The name of the profile to send the message from. The *@profile_name* is of type **sysname**, with a default of `NULL`. The *@profile_name* must be the name of an existing Database Mail profile. When no *@profile_name* is specified, `sp_send_dbmail` uses the default private profile for the current user. If the user doesn't have a default private profile, `sp_send_dbmail` uses the default public profile for the `msdb` database. If the user doesn't have a default private profile and there's no default public profile for the database, *@profile_name* must be specified.

#### [ @recipients = ] '*recipients*'

A semicolon-delimited list of e-mail addresses to send the message to. The recipients list is of type **varchar(max)**. Although this parameter is optional, at least one of *@recipients*, *@copy_recipients*, or *@blind_copy_recipients* must be specified, or `sp_send_dbmail` returns an error.

#### [ @copy_recipients = ] '*copy_recipients*'

A semicolon-delimited list of e-mail addresses to carbon copy the message to. The copy recipients list is of type **varchar(max)**. Although this parameter is optional, at least one of *@recipients*, *@copy_recipients*, or *@blind_copy_recipients* must be specified, or `sp_send_dbmail` returns an error.

#### [ @blind_copy_recipients = ] '*blind_copy_recipients*'

A semicolon-delimited list of e-mail addresses to blind carbon copy the message to. The blind copy recipients list is of type **varchar(max)**. Although this parameter is optional, at least one of *@recipients*, *@copy_recipients*, or *@blind_copy_recipients* must be specified, or `sp_send_dbmail` returns an error.

#### [ @from_address = ] '*from_address*'

The value of the 'from address' of the email message. This is an optional parameter used to override the settings in the mail profile. This parameter is of type **varchar(max)**. SMTP security settings determine if these overrides are accepted. If no parameter is specified, the default is `NULL`.

#### [ @reply_to = ] '*reply_to*'

The value of the 'reply to address' of the email message. It accepts only one email address as a valid value. This is an optional parameter used to override the settings in the mail profile. This parameter is of type **varchar(max)**. SMTP security settings determine if these overrides are accepted. If no parameter is specified, the default is `NULL`.

#### [ @subject = ] N'*subject*'

The subject of the e-mail message. The subject is of type **nvarchar(255)**. If no subject is specified, the default is 'SQL Server Message'.

#### [ @body = ] N'*body*'

The body of the e-mail message. The message body is of type **nvarchar(max)**, with a default of `NULL`.

#### [ @body_format = ] '*body_format*'

The format of the message body. The parameter is of type **varchar(20)**, with a default of `NULL`. When specified, the headers of the outgoing message are set to indicate that the message body has the specified format. The parameter might contain one of the following values:

- TEXT (default)
- HTML

#### [ @importance = ] '*importance*'

The importance of the message. The parameter is of type **varchar(6)**. The parameter might contain one of the following values:

- `Low`
- `Normal` (default)
- `High`

#### [ @sensitivity = ] '*sensitivity*'

The sensitivity of the message. The parameter is of type **varchar(12)**. The parameter might contain one of the following values:

- `Normal` (default)
- `Personal`
- `Private`
- `Confidential`

#### [ @file_attachments = ] N'*file_attachments*'

A semicolon-delimited list of file names to attach to the e-mail message. Files in the list must be specified as absolute paths. The attachments list is of type **nvarchar(max)**. By default, Database Mail limits file attachments to 1 MB per file.

> [!IMPORTANT]  
> This parameter isn't available in Azure SQL Managed Instance, because it can't access the local file system.

#### [ @query = ] N'*query*'

A query to execute. The results of the query can be attached as a file, or included in the body of the e-mail message. The query is of type **nvarchar(max)**, and can contain any valid [!INCLUDE [tsql](../../includes/tsql-md.md)] statements. The query is executed in a separate session, so local variables in the script calling `sp_send_dbmail` aren't available to the query.

When you use the *@query* parameter, the principal which executes `sp_send_dbmail` must be connected as an individual, not as part of a group, whether a Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) or Windows Active Directory group. SQL Server logins, Windows identities, and Microsoft Entra identities can execute the query, but group members can't, due to Azure SQL Managed Instance impersonation and [EXECUTE AS limitations](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#logins-and-users).

#### [ @execute_query_database = ] '*execute_query_database*'

The database context within which the stored procedure runs the query. The parameter is of type **sysname**, with a default of the current database. This parameter is only applicable if *@query* is specified.

#### [ @attach_query_result_as_file = ] *attach_query_result_as_file*

Specifies whether the result set of the query is returned as an attached file. *@attach_query_result_as_file* is of type **bit**, with a default of `0`.

When the value is `0`, the query results are included in the body of the e-mail message, after the contents of the *@body* parameter. When the value is `1`, the results are returned as an attachment. This parameter is only applicable if *@query* is specified.

#### [ @query_attachment_filename = ] N'*query_attachment_filename*'

Specifies the file name to use for the result set of the query attachment. *@query_attachment_filename* is of type **nvarchar(255)**, with a default of `NULL`. This parameter is ignored when *@attach_query_result_as_file* is `0`. When *@attach_query_result_as_file* is `1` and this parameter is `NULL`, Database Mail creates an arbitrary filename.

#### [ @query_result_header = ] *query_result_header*

Specifies whether the query results include column headers. The query_result_header value is of type **bit**. When the value is `1`, query results contain column headers. When the value is `0`, query results don't include column headers. This parameter defaults to `1`. This parameter is only applicable if *@query* is specified.

The following error might occur when setting *@query_result_header* to `0` and setting *@query_no_truncate* to `1`:

```output
Msg 22050, Level 16, State 1, Line 12: Failed to initialize sqlcmd library with error number -2147024809.
```

#### [ @query_result_width = ] *query_result_width*

The line width, in characters, to use for formatting the results of the query. The *@query_result_width* is of type **int**, with a default of `256`. The value provided must be between `10` and `32767`. This parameter is only applicable if *@query* is specified.

#### [ @query_result_separator = ] '*query_result_separator*'

The character used to separate columns in the query output. The separator is of type **char(1)**. Defaults to `' '` (space).

#### [ @exclude_query_output = ] *exclude_query_output*

Specifies whether to return the output of the query execution in the e-mail message. *@exclude_query_output* is **bit**, with a default of `0`. When this parameter is `0`, the execution of the `sp_send_dbmail` stored procedure prints the message returned as the result of the query execution on the console. When this parameter is `1`, the execution of the `sp_send_dbmail` stored procedure doesn't print any of the query execution messages on the console.

#### [ @append_query_error = ] *append_query_error*

Specifies whether to send the e-mail when an error returns from the query specified in the *@query* argument. *@append_query_error* is **bit**, with a default of `0`. When this parameter is `1`, Database Mail sends the e-mail message and includes the query error message in the body of the e-mail message. When this parameter is `0`, Database Mail doesn't send the e-mail message, and `sp_send_dbmail` ends with return code `1`, indicating failure.

#### [ @query_no_truncate = ] *query_no_truncate*

Specifies whether to execute the query with the option that avoids truncation of large variable length data types (**varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **text**, **ntext**, **image**, and user-defined data types). When set, query results don't include column headers. The *@query_no_truncate* value is of type **bit**. When the value is `0` or not specified, columns in the query truncate to 256 characters. When the value is `1`, columns in the query aren't truncated. This parameter defaults to `0`.

> [!NOTE]  
> When used with large amounts of data, the *@query_no_truncate* option consumes additional resources and can slow server performance.

#### [ @query_result_no_padding = ] *query_result_no_padding*

The type is **bit**. The default is `0`. When you set to `1`, the query results aren't padded, possibly reducing the file size. If you set @query_result_no_padding to `1` and you set the *@query_result_width* parameter, the *@query_result_no_padding* parameter overwrites the *@query_result_width* parameter.

In this case, no error occurs.

The following error might occur when setting *@query_result_no_padding* to `1` and providing a parameter for *@query_no_truncate*:

```output
Msg 22050, Level 16, State 1, Line 0: Failed to execute the query because the @query_result_no_append and @query_no_truncate options are mutually exclusive.
```

If you set the *@query_result_no_padding* to `1` and you set the *@query_no_truncate* parameter, an error is raised.

#### [ @mailitem_id = ] *mailitem_id* [ OUTPUT ]

Optional output parameter returns the `mailitem_id` of the message. *@mailitem_id* is of type **int**.

## Return code values

A return code of `0` means success. Any other value means failure. The error code for the statement that failed is stored in the `@@ERROR` variable.

## Result set

On success, returns the message "Mail queued."

## Remarks

Before use, Database Mail must be enabled using the Database Mail Configuration Wizard, or `sp_configure`.

`sysmail_stop_sp` stops Database Mail by stopping the Service Broker objects that the external program uses. `sp_send_dbmail` still accepts mail when Database Mail is stopped using `sysmail_stop_sp`. To start Database Mail, use `sysmail_start_sp`.

When *@profile* isn't specified, `sp_send_dbmail` uses a default profile. If the user sending the e-mail message has a default private profile, Database Mail uses that profile. If the user has no default private profile, `sp_send_dbmail` uses the default public profile. If there's no default private profile for the user and no default public profile, `sp_send_dbmail` returns an error.

`sp_send_dbmail` doesn't support e-mail messages with no content. To send an e-mail message, you must specify at least one of *@body*, *@query*, *@file_attachments*, or *@subject*. Otherwise, `sp_send_dbmail` returns an error.

Database Mail uses the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows security context of the current user to control access to files. Therefore, users who are authenticated with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication can't attach files using *@file_attachments*. Windows doesn't allow [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to provide credentials from a remote computer to another remote computer. Therefore, Database Mail may not be able to attach files from a network share in cases where the command is run from a computer other than the computer that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] runs on.

If both *@query* and *@file_attachments* are specified and the file can't be found, the query is still executed but the e-mail isn't sent.

When a query is specified, the result set is formatted as inline text. Binary data in the result is sent in hexadecimal format.

The parameters *@recipients*, *@copy_recipients*, and *@blind_copy_recipients* are semicolon-delimited lists of e-mail addresses. At least one of these parameters must be provided, or `sp_send_dbmail` returns an error.

When executing `sp_send_dbmail` without a transaction context, Database Mail starts and commits an implicit transaction. When executing `sp_send_dbmail` from within an existing transaction, Database Mail relies on the user to either commit or roll back any changes. It doesn't start an inner transaction.

## Permissions

Execute permissions for `sp_send_dbmail` default to all members of the **DatabaseMailUser** database role in the `msdb` database. However, when the user sending the message doesn't have permission to use the profile for the request, `sp_send_dbmail` returns an error and doesn't send the message.

## Examples

### A. Send an e-mail message

This example sends an e-mail message to your friend using the e-mail address `myfriend@adventure-works.com`. The message has the subject `Automated Success Message`. The body of the message contains the sentence `The stored procedure finished successfully`.

```sql
EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Adventure Works Administrator',
    @recipients = 'yourfriend@adventure-works.com',
    @body = 'The stored procedure finished successfully.',
    @subject = 'Automated Success Message';
```

### B. Send an e-mail message with the results of a query

This example sends an e-mail message to your friend using the e-mail address `yourfriend@adventure-works.com`. The message has the subject `Work Order Count`, and executes a query that shows the number of work orders with a `DueDate` less than two days after April 30, 2022. Database Mail attaches the result as a text file.

```sql
EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Adventure Works Administrator',
    @recipients = 'yourfriend@adventure-works.com',
    @query = 'SELECT COUNT(*) FROM AdventureWorks2022.Production.WorkOrder
                  WHERE DueDate > ''2022-04-30''
                  AND  DATEDIFF(dd, ''2022-04-30'', DueDate) < 2',
    @subject = 'Work Order Count',
    @attach_query_result_as_file = 1;
```

### C. Send an HTML e-mail message

This example sends an e-mail message to your friend using the e-mail address `yourfriend@adventure-works.com`. The message has the subject `Work Order List`, and contains an HTML document that shows the work orders with a `DueDate` less than two days after April 30, 2022. Database Mail sends the message in HTML format.

```sql
DECLARE @tableHTML NVARCHAR(MAX);

SET @tableHTML = N'<H1>Work Order Report</H1>' + N'<table border="1">'
    + N'<tr><th>Work Order ID</th><th>Product ID</th>'
    + N'<th>Name</th><th>Order Qty</th><th>Due Date</th>'
    + N'<th>Expected Revenue</th></tr>'
    + CAST((
            SELECT td = wo.WorkOrderID, '',
                td = p.ProductID, '',
                td = p.Name, '',
                td = wo.OrderQty, '',
                td = wo.DueDate, '',
                td = (p.ListPrice - p.StandardCost) * wo.OrderQty
            FROM AdventureWorks.Production.WorkOrder AS wo
            INNER JOIN AdventureWorks.Production.Product AS p
                ON wo.ProductID = p.ProductID
            WHERE DueDate > '2022-04-30'
                AND DATEDIFF(dd, '2022-04-30', DueDate) < 2
            ORDER BY DueDate ASC,
                (p.ListPrice - p.StandardCost) * wo.OrderQty DESC
            FOR XML PATH('tr'),
                TYPE
            ) AS NVARCHAR(MAX))
    + N'</table>';

EXEC msdb.dbo.sp_send_dbmail @recipients = 'yourfriend@adventure-works.com',
    @subject = 'Work Order List',
    @body = @tableHTML,
    @body_format = 'HTML';
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail Configuration Objects](../database-mail/database-mail-configuration-objects.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
- [sp_addrolemember (Transact-SQL)](sp-addrolemember-transact-sql.md)
