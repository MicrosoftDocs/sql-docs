---
title: "sys.sp_rda_test_connection (Transact-SQL) | Microsoft Docs"
description: Learn to use sys.sp_rda_test_connection to test the connection from SQL Server to the remote Azure server and reports problems that may prevent data migration.
ms.custom: ""
ms.date: 07/25/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords: 
  - "sys.sp_rda_test_connection"
  - "sys.sp_rda_test_connection_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_rda_test_connection stored procedure"
ms.assetid: e2ba050c-d7e3-4f33-8281-c9b525b4edb4
author: markingmyname
ms.author: maghan
---
# sys.sp_rda_test_connection (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Tests the connection from SQL Server to the remote Azure server and reports problems that may prevent data migration.  
  
## Syntax  
  
```  
  
EXECUTE sys.sp_rda_test_connection  
   @database_name = N'db_name',   
   @server_address = N'azure_server_fully_qualified_address',  
   @azure_username = N'azure_username',   
   @azure_password = N'azure_password',  
   @credential_name = N'credential_name'  
  
```  
  
## Arguments  
 @database_name = N'*db_name*'  
 The name of the Stretch-enabled SQL Server database. This parameter is optional.  

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]
  
 @server_address = N'*azure_server_fully_qualified_address*'  
 The fully qualified address of the Azure server.  
  
-   If you provide a value for **\@database_name**, but the specified database is not Stretch-enabled, then you have to provide a value for **\@server_address**.  
  
-   If you provide a value for **\@database_name**, and the specified database is Stretch-enabled, then you don't have to provide a value for **\@server_address**. If you provide a value for **\@server_address**, the stored procedure ignores it and uses existing Azure server already associated with the Stretch-enabled database.  
  
 @azure_username = N'*azure_username*  
 The user name for the remote Azure server.  
  
 @azure_password = N'*azure_password*'  
 The password for the remote Azure server.  
  
 @credential_name = N'*credential_name*'  
 Instead of providing a user name and password, you can provide the name of a credential stored in the Stretch-enabled database.  
  
## Return Code Values  
 In case of **success**, sp_rda_test_connection returns error 14855 (STRETCH_MAJOR, STRETCH_CONNECTION_TEST_PROC_SUCCEEDED) with severity EX_INFO and a success return code.  
  
 In case of **failure**, sp_rda_test_connection returns error 14856 (STRETCH_MAJOR, STRETCH_CONNECTION_TEST_PROC_FAILED) with severity EX_USER and an error return code.  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|link_state|int|One of the following values, which correspond to the values for **link_state_desc**.<br /><br /> -   0<br />-   1<br />-   2<br />-   3<br />-   4|  
|link_state_desc|varchar(32)|One of the following values, which correspond to the preceding values for **link_state**.<br /><br /> -   HEALTHY<br />     The between SQL Server and the remote Azure server is healthy.<br />-   ERROR_AZURE_FIREWALL<br />     The Azure firewall is preventing the link  between SQL Server and the remote Azure server.<br />-   ERROR_NO_CONNECTION<br />     SQL Server  can't  make a connection to the remote Azure server.<br />-   ERROR_AUTH_FAILURE<br />     An authentication failure is preventing the link  between SQL Server and the remote Azure server.<br />-   ERROR<br />     An error that's not an authentication issue, a connectivity issue, or a firewall issue is preventing the link  between SQL Server and the remote Azure server.|  
|error_number|int|The number of the error. If there is no error, this field is NULL.|  
|error_message|nvarchar(1024)|The error message. If there is no error, this field is NULL.|  
  
## Permissions  
 Requires db_owner permissions.  
  
## Examples  
  
### Check the connection from SQL Server to the remote Azure server  
  
```sql  
EXECUTE sys.sp_rda_test_connection @database_name = N'<Stretch-enabled database>'  
GO  
  
```  
  
 The results show that SQL Server can't connect to the remote Azure server.  
  
|link_state|link_state_desc|error_number|error_message|  
|-----------------|-----------------------|-------------------|--------------------|  
|2|ERROR_NO_CONNECTION|*\<connection-related error number>*|*\<connection-related error message>*|  
  
### Check the Azure firewall  
  
```sql  
USE <Stretch-enabled database>  
GO  
EXECUTE sys.sp_rda_test_connection  
GO  
  
```  
  
 The results show that the Azure firewall is preventing the link  between SQL Server and the remote Azure server.  
  
|link_state|link_state_desc|error_number|error_message|  
|-----------------|-----------------------|-------------------|--------------------|  
|1|ERROR_AZURE_FIREWALL|*\<firewall-related error number>*|*\<firewall-related error message>*|  
  
### Check authentication credentials  
  
```sql  
USE <Stretch-enabled database>  
GO  
EXECUTE sys.sp_rda_test_connection  
GO  
  
```  
  
 The results show that an authentication failure is preventing the link  between SQL Server and the remote Azure server.  
  
|link_state|link_state_desc|error_number|error_message|  
|-----------------|-----------------------|-------------------|--------------------|  
|3|ERROR_AUTH_FAILURE|*\<authentication-related error number>*|*\<authentication-related error message>*|  
  
### Check the status of the remote Azure server  
  
```sql  
USE <SQL Server database>  
GO  
EXECUTE sys.sp_rda_test_connection   
    @server_address = N'<server name>.database.windows.net',   
    @azure_username = N'<user name>',   
    @azure_password = N'<password>'  
GO  
  
```  
  
 The results show that the connection is healthy and that you can enable Stretch Database for the specified database.  
  
|link_state|link_state_desc|error_number|error_message|  
|-----------------|-----------------------|-------------------|--------------------|  
|0|HEALTHY|NULL|NULL|  
  
  
