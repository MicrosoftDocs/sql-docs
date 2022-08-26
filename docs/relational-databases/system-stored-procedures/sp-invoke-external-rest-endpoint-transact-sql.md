---
description: "sp_invoke_external_rest_endpoint (Transact-SQL)"
title: "sp_invoke_external_rest_endpoint (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 2022-08-26
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

WIP

## Syntax  
  
```  
EXEC @returnValue = sp_invoke_external_rest_endpoint 
  @url
  [ , [ @payload = ] N'json_payload' ]
  [ , [ @headers = ] N'http_headers_as_json_array' ]
  [ , [ @method = ] 'GET'|'POST'|'PUT'|'PATCH'|'DELETE' ]
  [ , [ @type = ] 'SYNC' ]
	[ , [ @timeout = ] seconds ]
  [ , [ @credential = ] credential]
  [ , @response OUTPUT]

```  
  
## Arguments  

WIP

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
