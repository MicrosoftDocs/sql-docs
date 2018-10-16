---
title: "sp_send_dbmail (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sendmail_sp_TSQL"
  - "sendmail_sp"
  - "SP_SEND_DBMAIL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_send_dbmail"
ms.assetid: f1d7a795-a3fd-4043-ac4b-c781e76dab47
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_send_dbmail (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sends an e-mail message to the specified recipients. The message may include a query result set, file attachments, or both. When mail is successfully placed in the Database Mail queue, **sp_send_dbmail** returns the **mailitem_id** of the message. This stored procedure is in the **msdb** database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_send_dbmail [ [ @profile_name = ] 'profile_name' ]  
    [ , [ @recipients = ] 'recipients [ ; ...n ]' ]  
    [ , [ @copy_recipients = ] 'copy_recipient [ ; ...n ]' ]  
    [ , [ @blind_copy_recipients = ] 'blind_copy_recipient [ ; ...n ]' ]  
    [ , [ @from_address = ] 'from_address' ]  
    [ , [ @reply_to = ] 'reply_to' ]   
    [ , [ @subject = ] 'subject' ]   
    [ , [ @body = ] 'body' ]   
    [ , [ @body_format = ] 'body_format' ]  
    [ , [ @importance = ] 'importance' ]  
    [ , [ @sensitivity = ] 'sensitivity' ]  
    [ , [ @file_attachments = ] 'attachment [ ; ...n ]' ]  
    [ , [ @query = ] 'query' ]  
    [ , [ @execute_query_database = ] 'execute_query_database' ]  
    [ , [ @attach_query_result_as_file = ] attach_query_result_as_file ]  
    [ , [ @query_attachment_filename = ] query_attachment_filename ]  
    [ , [ @query_result_header = ] query_result_header ]  
    [ , [ @query_result_width = ] query_result_width ]  
    [ , [ @query_result_separator = ] 'query_result_separator' ]  
    [ , [ @exclude_query_output = ] exclude_query_output ]  
    [ , [ @append_query_error = ] append_query_error ]  
    [ , [ @query_no_truncate = ] query_no_truncate ]   
    [ , [ @query_result_no_padding = ] @query_result_no_padding ]   
    [ , [ @mailitem_id = ] mailitem_id ] [ OUTPUT ]  
```  
  
## Arguments  
 [ **@profile_name=** ] **'***profile_name***'**  
 Is the name of the profile to send the message from. The *profile_name* is of type **sysname**, with a default of NULL. The *profile_name* must be the name of an existing Database Mail profile. When no *profile_name* is specified, **sp_send_dbmail** uses the default private profile for the current user. If the user does not have a default private profile, **sp_send_dbmail** uses the default public profile for the **msdb** database. If the user does not have a default private profile and there is no default public profile for the database, **@profile_name** must be specified.  
  
 [ **@recipients=** ] **'***recipients***'**  
 Is a semicolon-delimited list of e-mail addresses to send the message to. The recipients list is of type **varchar(max)**. Although this parameter is optional, at least one of **@recipients**, **@copy_recipients**, or **@blind_copy_recipients** must be specified, or **sp_send_dbmail** returns an error.  
  
 [ **@copy_recipients=** ] **'***copy_recipients***'**  
 Is a semicolon-delimited list of e-mail addresses to carbon copy the message to. The copy recipients list is of type **varchar(max)**. Although this parameter is optional, at least one of **@recipients**, **@copy_recipients**, or **@blind_copy_recipients** must be specified, or **sp_send_dbmail** returns an error.  
  
 [ **@blind_copy_recipients=** ] **'***blind_copy_recipients***'**  
 Is a semicolon-delimited list of e-mail addresses to blind carbon copy the message to. The blind copy recipients list is of type **varchar(max)**. Although this parameter is optional, at least one of **@recipients**, **@copy_recipients**, or **@blind_copy_recipients** must be specified, or **sp_send_dbmail** returns an error.  
  
 [ **@from_address=** ] **'***from_address***'**  
 Is the value of the 'from address' of the email message. This is an optional parameter used to override the settings in the mail profile. This parameter is of type **varchar(MAX)**. SMTP security settings determine if these overrides are accepted. If no parameter is specified, the default is NULL.  
  
 [ **@reply_to=** ] **'***reply_to***'**  
 Is the value of the 'reply to address' of the email message. It accepts only one email address as a valid value. This is an optional parameter used to override the settings in the mail profile. This parameter is of type **varchar(MAX)**. SMTP security settings determine if these overrides are accepted. If no parameter is specified, the default is NULL.  
  
 [ **@subject=** ] **'***subject***'**  
 Is the subject of the e-mail message. The subject is of type **nvarchar(255)**. If no subject is specified, the default is 'SQL Server Message'.  
  
 [ **@body=** ] **'***body***'**  
 Is the body of the e-mail message. The message body is of type **nvarchar(max)**, with a default of NULL.  
  
 [ **@body_format=** ] **'***body_format***'**  
 Is the format of the message body. The parameter is of type **varchar(20)**, with a default of NULL. When specified, the headers of the outgoing message are set to indicate that the message body has the specified format. The parameter may contain one of the following values:  
  
-   TEXT  
  
-   HTML  
  
 Defaults to TEXT.  
  
 [ **@importance=** ] **'***importance***'**  
 Is the importance of the message. The parameter is of type **varchar(6)**. The parameter may contain one of the following values:  
  
-   Low  
  
-   Normal  
  
-   High  
  
 Defaults to Normal.  
  
 [ **@sensitivity=** ] **'***sensitivity***'**  
 Is the sensitivity of the message. The parameter is of type **varchar(12)**. The parameter may contain one of the following values:  
  
-   Normal  
  
-   Personal  
  
-   Private  
  
-   Confidential  
  
 Defaults to Normal.  
  
 [ **@file_attachments=** ] **'***file_attachments***'**  
 Is a semicolon-delimited list of file names to attach to the e-mail message. Files in the list must be specified as absolute paths. The attachments list is of type **nvarchar(max)**. By default, Database Mail limits file attachments to 1 MB per file.  
  
 [ **@query=** ] **'***query***'**  
 Is a query to execute. The results of the query can be attached as a file, or included in the body of the e-mail message. The query is of type **nvarchar(max)**, and can contain any valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. Note that the query is executed in a separate session, so local variables in the script calling **sp_send_dbmail** are not available to the query.  
  
 [ **@execute_query_database=** ] **'***execute_query_database***'**  
 Is the database context within which the stored procedure runs the query. The parameter is of type **sysname**, with a default of the current database. This parameter is only applicable if **@query** is specified.  
  
 [ **@attach_query_result_as_file=** ] *attach_query_result_as_file*  
 Specifies whether the result set of the query is returned as an attached file. *attach_query_result_as_file* is of type **bit**, with a default of 0.  
  
 When the value is 0, the query results are included in the body of the e-mail message, after the contents of the **@body** parameter. When the value is 1, the results are returned as an attachment. This parameter is only applicable if **@query** is specified.  
  
 [ **@query_attachment_filename=** ] *query_attachment_filename*  
 Specifies the file name to use for the result set of the query attachment. *query_attachment_filename* is of type **nvarchar(255)**, with a default of NULL. This parameter is ignored when *attach_query_result* is 0. When *attach_query_result* is 1 and this parameter is NULL, Database Mail creates an arbitrary filename.  
  
 [ **@query_result_header=** ] *query_result_header*  
 Specifies whether the query results include column headers. The query_result_header value is of type **bit**. When the value is 1, query results contain column headers. When the value is 0, query results do not include column headers. This parameter defaults to **1**. This parameter is only applicable if **@query** is specified.  
 
   >[!NOTE]
   > The following error may occur when setting @query_result_header to 0 and setting @query_no_truncate to 1: 
   > <br> Msg 22050, Level 16, State 1, Line 12: Failed to initialize sqlcmd library with error number -2147024809.
  
 [ **@query_result_width** = ] *query_result_width*  
 Is the line width, in characters, to use for formatting the results of the query. The *query_result_width* is of type **int**, with a default of 256. The value provided must be between 10 and 32767. This parameter is only applicable if **@query** is specified.  
  
 [ **@query_result_separator=** ] **'***query_result_separator***'**  
 Is the character used to separate columns in the query output. The separator is of type **char(1)**. Defaults to ' ' (space).  
  
 [ **@exclude_query_output=** ] *exclude_query_output*  
 Specifies whether to return the output of the query execution in the e-mail message. **exclude_query_output** is bit, with a default of 0. When this parameter is 0, the execution of the **sp_send_dbmail** stored procedure prints the message returned as the result of the query execution on the console. When this parameter is 1, the execution of the **sp_send_dbmail** stored procedure does not print any of the query execution messages on the console.  
  
 [ **@append_query_error=** ] *append_query_error*  
 Specifies whether to send the e-mail when an error returns from the query specified in the **@query** argument. **append_query_error** is **bit**, with a default of 0. When this parameter is 1, Database Mail sends the e-mail message and includes the query error message in the body of the e-mail message. When this parameter is 0, Database Mail does not send the e-mail message, and **sp_send_dbmail** ends with return code 1, indicating failure.  
  
 [ **@query_no_truncate=** ] *query_no_truncate*  
 Specifies whether to execute the query with the option that avoids truncation of large variable length data types (**varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **text**, **ntext**, **image**, and user-defined data types). When set, query results do not include column headers. The *query_no_truncate* value is of type **bit**. When the value is 0 or not specified, columns in the query truncate to 256 characters. When the value is 1, columns in the query are not truncated. This parameter defaults to 0.  
  
> [!NOTE]  
>  When used with large amounts of data, the @**query_no_truncate** option consumes additional resources and can slow server performance.  
  
 [ **@query_result_no_padding** ] *@query_result_no_padding*  
 The type is bit. The default is 0. When you set to 1, the query results are not padded, possibly reducing the file size.If you set @query_result_no_padding to 1 and you set the @query_result_width parameter, the @query_result_no_padding parameter overwrites the @query_result_width parameter.  
  
 In this case no error occurs.  
 
  >[!NOTE]
  > The following error may occur when setting @query_result_no_padding to 1 and providing a parameter for @query_no_truncate: 
  > <br> Msg 22050, Level 16, State 1, Line 0: Failed to execute the query because the @query_result_no_append and @query_no_truncate    options are mutually exclusive. 
  
 If you set the @query_result_no_padding to 1 and you set the @query_no_truncate parameter, an error is raised.  
  
 [ **@mailitem_id=** ] *mailitem_id* [ OUTPUT ]  
 Optional output parameter returns the *mailitem_id* of the message. The *mailitem_id* is of type **int**.  
  
## Return Code Values  
 A return code of 0 means success. Any other value means failure. The error code for the statement that failed is stored in the @@ERROR variable.  
  
## Result Sets  
 On success, returns the message "Mail queued."  
  
## Remarks  
 Before use, Database Mail must be enabled using the Database Mail Configuration Wizard, or **sp_configure**.  
  
 **sysmail_stop_sp** stops Database Mail by stopping the Service Broker objects that the external program uses. **sp_send_dbmail** still accepts mail when Database Mail is stopped using **sysmail_stop_sp**. To start Database Mail, use **sysmail_start_sp**.  
  
 When **@profile** is not specified, **sp_send_dbmail** uses a default profile. If the user sending the e-mail message has a default private profile, Database Mail uses that profile. If the user has no default private profile, **sp_send_dbmail** uses the default public profile. If there is no default private profile for the user and no default public profile, **sp_send_dbmail** returns an error.  
  
 **sp_send_dbmail** does not support e-mail messages with no content. To send an e-mail message, you must specify at least one of **@body**, **@query**, **@file_attachments**, or **@subject**. Otherwise, **sp_send_dbmail** returns an error.  
  
 Database Mail uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows security context of the current user to control access to files. Therefore, users who are authenticated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication cannot attach files using **@file_attachments**. Windows does not allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to provide credentials from a remote computer to another remote computer. Therefore, Database Mail may not be able to attach files from a network share in cases where the command is run from a computer other than the computer that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs on.  
  
 If both **@query** and **@file_attachments** are specified and the file cannot be found, the query is still executed but the e-mail is not sent.  
  
 When a query is specified, the result set is formatted as inline text. Binary data in the result is sent in hexadecimal format.  
  
 The parameters **@recipients**, **@copy_recipients**, and **@blind_copy_recipients** are semicolon-delimited lists of e-mail addresses. At least one of these parameters must be provided, or **sp_send_dbmail** returns an error.  
  
 When executing **sp_send_dbmail** without a transaction context, Database Mail starts and commits an implicit transaction. When executing **sp_send_dbmail** from within an existing transaction, Database Mail relies on the user to either commit or roll back any changes. It does not start an inner transaction.  
  
## Permissions  
 Execute permissions for **sp_send_dbmail** default to all members of the **DatabaseMailUser** database role in the **msdb** database. However, when the user sending the message does not have permission to use the profile for the request, **sp_send_dbmail** returns an error and does not send the message.  
  
## Examples  
  
### A. Sending an e-mail message  
 This example sends an e-mail message to your friend using the e-mail address `myfriend@Adventure-Works.com`. The message has the subject `Automated Success Message`. The body of the message contains the sentence `'The stored procedure finished successfully'`.  
  
```  
EXEC msdb.dbo.sp_send_dbmail  
    @profile_name = 'Adventure Works Administrator',  
    @recipients = 'yourfriend@Adventure-Works.com',  
    @body = 'The stored procedure finished successfully.',  
    @subject = 'Automated Success Message' ;  
```  
  
### B. Sending an e-mail message with the results of a query  
 This example sends an e-mail message to your friend using the e-mail address `yourfriend@Adventure-Works.com`. The message has the subject `Work Order Count`, and executes a query that shows the number of work orders with a `DueDate` less than two days after April 30, 2004. Database Mail attaches the result as a text file.  
  
```  
EXEC msdb.dbo.sp_send_dbmail  
    @profile_name = 'Adventure Works Administrator',  
    @recipients = 'yourfriend@Adventure-Works.com',  
    @query = 'SELECT COUNT(*) FROM AdventureWorks2012.Production.WorkOrder  
                  WHERE DueDate > ''2004-04-30''  
                  AND  DATEDIFF(dd, ''2004-04-30'', DueDate) < 2' ,  
    @subject = 'Work Order Count',  
    @attach_query_result_as_file = 1 ;  
```  
  
### C. Sending an HTML e-mail message  
 This example sends an e-mail message to your friend using the e-mail address `yourfriend@Adventure-Works.com`. The message has the subject `Work Order List`, and contains an HTML document that shows the work orders with a `DueDate` less than two days after April 30, 2004. Database Mail sends the message in HTML format.  
  
```  
DECLARE @tableHTML  NVARCHAR(MAX) ;  
  
SET @tableHTML =  
    N'<H1>Work Order Report</H1>' +  
    N'<table border="1">' +  
    N'<tr><th>Work Order ID</th><th>Product ID</th>' +  
    N'<th>Name</th><th>Order Qty</th><th>Due Date</th>' +  
    N'<th>Expected Revenue</th></tr>' +  
    CAST ( ( SELECT td = wo.WorkOrderID,       '',  
                    td = p.ProductID, '',  
                    td = p.Name, '',  
                    td = wo.OrderQty, '',  
                    td = wo.DueDate, '',  
                    td = (p.ListPrice - p.StandardCost) * wo.OrderQty  
              FROM AdventureWorks.Production.WorkOrder as wo  
              JOIN AdventureWorks.Production.Product AS p  
              ON wo.ProductID = p.ProductID  
              WHERE DueDate > '2004-04-30'  
                AND DATEDIFF(dd, '2004-04-30', DueDate) < 2   
              ORDER BY DueDate ASC,  
                       (p.ListPrice - p.StandardCost) * wo.OrderQty DESC  
              FOR XML PATH('tr'), TYPE   
    ) AS NVARCHAR(MAX) ) +  
    N'</table>' ;  
  
EXEC msdb.dbo.sp_send_dbmail @recipients='yourfriend@Adventure-Works.com',  
    @subject = 'Work Order List',  
    @body = @tableHTML,  
    @body_format = 'HTML' ;  
```  
  
## See Also  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)   
 [Database Mail Configuration Objects](../../relational-databases/database-mail/database-mail-configuration-objects.md)   
 [Database Mail Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-mail-stored-procedures-transact-sql.md)   
 [sp_addrolemember &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md)  
  
  
