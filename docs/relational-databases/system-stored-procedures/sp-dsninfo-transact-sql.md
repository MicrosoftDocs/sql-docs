---
title: "sp_dsninfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dsninfo"
  - "sp_dsninfo_TSQL"
helpviewer_keywords: 
  - "sp_dsninfo"
ms.assetid: 34648615-814b-42bc-95a3-50e86b42ec4d
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dsninfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns ODBC or OLE DB data source information from the Distributor associated with the current server. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dsninfo [ @dsn =] 'dsn'   
    [ , [ @infotype =] 'info_type']   
    [ , [ @login =] 'login']   
    [ , [ @password =] 'password']  
    [ , [ @dso_type=] dso_type]  
```  
  
## Arguments  
 [ **@dsn =**] **'***dsn***'**  
 Is the name of the ODBC DSN or OLE DB linked server. *dsn* is **varchar(128)**, with no default.  
  
 [ **@infotype =**] **'***info_type***'**  
 Is the type of information to return. If *info_type* is not specified or if NULL is specified, all information types are returned. *info_type* is **varchar(128)**, with a default of NULL, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**DBMS_NAME**|Specifies the data source vendor name.|  
|**DBMS_VERSION**|Specifies the data source version.|  
|**DATABASE_NAME**|Specifies the database name.|  
|**SQL_SUBSCRIBER**|Specifies the data source can be a Subscriber.|  
  
 [ **@login =**] **'***login***'**  
 Is the login for the data source. If the data source includes a login, specify NULL or omit the parameter. *login*is **varchar(128)**, with a default of NULL.  
  
 [ **@password =**] **'***password***'**  
 Is the password for the login. If the data source includes a login, specify NULL or omit the parameter. *password*is **varchar(128)**, with a default of NULL.  
  
 [ **@dso_type=**] *dso_type*  
 Is the data source type. *dso_type* is **int**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**1** (default)|ODBC data source|  
|**3**|OLE DB data source|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**Information Type**|**nvarchar(64)**|Information types such as DBMS_NAME, DBMS_VERSION, DATABASE_NAME, SQL_SUBSCRIBER.|  
|**Value**|**nvarchar(512)**|Value of the associated information type.|  
  
## Remarks  
 **sp_dsninfo** is used in all types of replication.  
  
 **sp_dsninfo** retrieves ODBC or OLE DB data source information that shows whether the database can be used for replication or querying.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_dsninfo**.  
  
## See Also  
 [sp_enumdsn &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-enumdsn-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
