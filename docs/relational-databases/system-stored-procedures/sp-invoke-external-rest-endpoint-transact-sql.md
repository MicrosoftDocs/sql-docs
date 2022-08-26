---
description: "sp_invoke_external_rest_endpoint (Transact-SQL)"
title: "sp_invoke_external_rest_endpoint (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 8/26/2022
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "sp_invoke_external_rest_endpoint_TSQL"
  - "sys.sp_invoke_external_rest_endpoint"
  - "sys.sp_invoke_external_rest_endpoint_TSQL"
  - "sp_invoke_external_rest_endpoint"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_invoke_external_rest_endpoint"
author: yorek
ms.author: damauri
monikerRange: "=azuresqldb-current"
---
# sp_invoke_external_rest_endpoint (Transact-SQL)

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

The **sp_invoke_external_rest_endpoint** stored procedure invokes a HTTPS REST endpoint provided as an input argument to the procedure. 

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
EXEC @returnValue = sp_invoke_external_rest_endpoint 
  @url
  [ , [ @payload = ] N'json_payload' ]
  [ , [ @headers = ] N'http_headers_as_json_array' ]
  [ , [ @method = ] 'GET'|'POST'|'PUT'|'PATCH'|'DELETE' ]
  [ , [ @timeout = ] seconds ]
  [ , [ @credential = ] credential]
  [ , @response OUTPUT]
```  
  
## Arguments  

#### `@url = N'url' `
 URL of the HTTPS REST endpoint to be called. *url* is **nvarchar(4000)** with no default.

#### `[ @payload =  N'payload' ]`
 Payload that must be sent to the HTTPS REST endpoint. *payload* is **nvarchar(max)** with no default. Optional.

#### `[ @headers =  N'headers' ]`
 Headers that must be sent as part of the request to the HTTPS REST endpoint. Headers must be specified using a flat JSON (a JSON document without nested structures) format. *headers* is **nvarchar(4000)** with no default. Optional.

#### `[ @method =  N'method' ]`
 HTTP method to be used to call the URL. Can only be one of the following values: `GET`, `POST`, `PUT`, `PATCH`, `DELETE`. *method* is **nvarchar(6)** with no `POST` as default value. Optional.

#### `[ @timeout = seconds ]`
 Time in seconds allowed for the HTTPS call to run. If full HTTP request and response cannot be sent and received within the defined timeout in seconds, stored procedure execution will be halted, and an exception will raised. Timeout starts when HTTP connection starts and ends when the response, payload included if any, has been received. *timeout* is a positive **smallint** with a default value 5. Optional.Accepted values: 1 to 230.

#### `[ @credential =  credential ]`
 Indicate what DATABASE SCOPED CREDENTIAL object will be used to inject authentication info in the HTTPS request. *credential* is a **sysname** with no default value. Optional.

#### `[ @response = @variable OUTPUT ]`
 Allow the response to be passed into the specified variable. *response* is a **nvarchar(max)**. Optional.

## Return Code Values  

WIP

## Remarks  

WIP

## Permissions  
   
WIP

## Examples  
  
WIP

## See Also  

WIP 
