---
title: "SQL Server Audit Action Groups| Microsoft Docs"
description: Learn about server-level, database-level, and audit-level groups of actions and individual actions in SQL Server Audit. 
ms.custom: ""
ms.date: "07/30/2021"
ms.prod: sql
ms.prod_service: security
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "audit"
helpviewer_keywords: 
  - "audit actions [SQL Server]"
  - "audits [SQL Server], groups"
  - "server-level audit actions [SQL Server]"
  - "SQL Server Audit"
  - "database-level audit actions [SQL Server]"
  - "audit action groups [SQL Server]"
  - "audits [SQL Server], actions"
ms.assetid: b7422911-7524-4bcd-9ab9-e460d5897b3d
author: rupp29
ms.author: arupp
---
# SQL Server Audit Action Groups
  
 Audits can have the following categories of actions:  
  
-   Server-level. These actions include server operations, such as management changes and logon and logoff operations.  
  
-   Database-level. These actions encompass data manipulation languages (DML) and data definition language (DDL) operations.  
  
|Audit Group|SQL 2014|SQL 2016|SQL 2017|SQL 2019|SQL DB   |
|-----------|--------|--------|--------|--------|---------|
|**Vesion** |**12.x**|**13.x**|**14.x**|**15.x**|**Azure**|
|**Compatibility Level**|**120**|**130**|**140**|**150**|**Azure**|
|APPLICATION_ROLE_CHANGE_PASSWORD_GROUP|Y|Y|Y|Y|Y|
|AUDIT_CHANGE_GROUP|Y|Y|Y|Y|N|
|BACKUP_RESTORE_GROUP|Y|Y|Y|Y|Y|
|BATCH_COMPLETED_GROUP|N|N|N|Y|Y|
|BATCH_STARTED_GROUP|N|N|N|Y|Y|
|BROKER_LOGIN_GROUP|Y|Y|Y|Y|N|
|DATABASE_CHANGE_GROUP|Y|Y|Y|Y|N|
|DATABASE_LOGOUT_GROUP|Y|Y|Y|Y|Y|
|DATABASE_MIRRORING_LOGIN_GROUP|Y|Y|Y|Y|N|
|DATABASE_OBJECT_ACCESS_GROUP|Y|Y|Y|Y|N|
|DATABASE_OBJECT_CHANGE_GROUP|Y|Y|Y|Y|Y|
|DATABASE_OBJECT_OWNERSHIP_CHANGE_GROUP|Y|Y|Y|Y|Y|
|DATABASE_OBJECT_PERMISSION_CHANGE_GROUP|Y|Y|Y|Y|Y|
|DATABASE_OPERATION_GROUP|Y|Y|Y|Y|Y|
|DATABASE_OWNERSHIP_CHANGE_GROUP|Y|Y|Y|Y|N|
|DATABASE_PERMISSION_CHANGE_GROUP|Y|Y|Y|Y|Y|
|DATABASE_PRINCIPAL_CHANGE_GROUP|Y|Y|Y|Y|Y|
|DATABASE_PRINCIPAL_IMPERSONATION_GROUP|Y|Y|Y|Y|Y|
|DATABASE_ROLE_MEMBER_CHANGE_GROUP|Y|Y|Y|Y|Y|
|DBCC_GROUP|Y|Y|Y|Y|N|
|FAILED_DATABASE_AUTHENTICATION_GROUP|Y|Y|Y|Y|Y|
|FAILED_LOGIN_GROUP|Y|Y|Y|Y|N|
|FEATURE_RESTRICTION_CHANGE_GROUP|N|N|N|Y|N|
|FULLTEXT_GROUP|Y|Y|Y|Y|N|
|GLOBAL_TRANSACTIONS_LOGIN_GROUP|N|Y|Y|Y|N|
|LOGIN_CHANGE_PASSWORD_GROUP|Y|Y|Y|Y|N|
|LOGOUT_GROUP|Y|Y|Y|Y|N|
|SCHEMA_OBJECT_ACCESS_GROUP|Y|Y|Y|Y|Y|
|SCHEMA_OBJECT_CHANGE_GROUP|Y|Y|Y|Y|Y|
|SCHEMA_OBJECT_OWNERSHIP_CHANGE_GROUP|Y|Y|Y|Y|Y|
|SCHEMA_OBJECT_PERMISSION_CHANGE_GROUP|Y|Y|Y|Y|Y|
|SENSITIVITY_CLASSIFICATION_CHANGE_GROUP|N|N|N|Y|N|
|SERVER_OBJECT_CHANGE_GROUP|Y|Y|Y|Y|N|
|SERVER_OBJECT_OWNERSHIP_CHANGE_GROUP|Y|Y|Y|Y|N|
|SERVER_OBJECT_PERMISSION_CHANGE_GROUP|Y|Y|Y|Y|N|
|SERVER_OPERATION_GROUP|Y|Y|Y|Y|N|
|SERVER_PERMISSION_CHANGE_GROUP|Y|Y|Y|Y|N|
|SERVER_PRINCIPAL_CHANGE_GROUP|Y|Y|Y|Y|N|
|SERVER_PRINCIPAL_IMPERSONATION_GROUP|Y|Y|Y|Y|N|
|SERVER_ROLE_MEMBER_CHANGE_GROUP|Y|Y|Y|Y|N|
|SERVER_STATE_CHANGE_GROUP|Y|Y|Y|Y|N|
|STATEMENT_ROLLBACK_GROUP|N|Y|Y|Y|N|
|STORAGE_LOGIN_GROUP|N|N|Y|Y|N|
|SUCCESSFUL_DATABASE_AUTHENTICATION_GROUP|Y|Y|Y|Y|Y|
|SUCCESSFUL_LOGIN_GROUP|Y|Y|Y|Y|N|
|TRACE_CHANGE_GROUP|Y|Y|Y|Y|N|
|TRANSACTION_BEGIN_GROUP|N|Y|Y|Y|N|
|TRANSACTION_COMMIT_GROUP|N|Y|Y|Y|N|
|TRANSACTION_ROLLBACK_GROUP|N|Y|Y|Y|N|
|USER_CHANGE_PASSWORD_GROUP|Y|Y|Y|Y|Y|
|USER_DEFINED_AUDIT_GROUP|Y|Y|Y|Y|N|
|LEDGER_OPERATION_GROUP|N|N|N|N|Y|

