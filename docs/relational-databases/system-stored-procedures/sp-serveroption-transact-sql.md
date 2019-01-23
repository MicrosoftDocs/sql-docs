---
title: "sp_serveroption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/11/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_serveroption_TSQL"
  - "sp_serveroption"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "7343 (Database Engine error)"
  - "sp_serveroption"
ms.assetid: 47d04a2b-dbf0-4f15-bd9b-81a2efc48131
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_serveroption (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sets server options for remote servers and linked servers.  
  
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_serveroption [@server = ] 'server'   
      ,[@optname = ] 'option_name'       
      ,[@optvalue = ] 'option_value' ;  
```  
  
## Arguments  
 [ **@server =** ] **'**_server_**'**  
 Is the name of the server for which to set the option. *server* is **sysname**, with no default.  
  
 [ **@optname =** ] **'**_option_name_**'**  
 Is the option to set for the specified server. *option_name* is **varchar(**35**)**, with no default. *option_name* can be any of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**collation compatible**|Affects Distributed Query execution against linked servers. If this option is set to **true**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assumes that all characters in the linked server are compatible with the local server, with regard to character set and collation sequence (or sort order). This enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to send comparisons on character columns to the provider. If this option is not set, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always evaluates comparisons on character columns locally.<br /><br /> This option should be set only if it is certain that the data source corresponding to the linked server has the same character set and sort order as the local server.|  
|**collation name**|Specifies the name of the collation used by the remote data source if **use remote collation** is **true** and the data source is not a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source. The name must be one of the collations supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> Use this option when accessing an OLE DB data source other than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], but whose collation matches one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations.<br /><br /> The linked server must support a single collation to be used for all columns in that server. Do not set this option if the linked server supports multiple collations within a single data source, or if the linked server's collation cannot be determined to match one of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations.|  
|**connect timeout**|Time-out valuein seconds for connecting to a linked server.<br /><br /> If **0**, use the **sp_configure** default.|  
|**data access**|Enables and disables a linked server for distributed query access. Can be used only for **sys.server** entries added through **sp_addlinkedserver**.|  
|**dist**|Distributor.|  
|**lazy schema validation**|Determines whether the schema of remote tables will be checked.<br /><br /> If **true**, skip schema checking of remote tables at the beginning of the query.|  
|**pub**|Publisher.|  
|**query timeout**|Time-out value for queries against a linked server.<br /><br /> If **0**, use the **sp_configure** default.|  
|**rpc**|Enables RPC from the given server.|  
|**rpc out**|Enables RPC to the given server.|  
|**sub**|Subscriber.|  
|**system**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**use remote collation**|Determines whether the collation of a remote column or of a local server will be used.<br /><br /> If **true**, the collation of remote columns is used for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources, and the collation specified in **collation name** is used for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources.<br /><br /> If **false**, distributed queries will always use the default collation of the local server, while **collation name** and the collation of remote columns are ignored. The default is **false**. (The **false** value is compatible with the collation semantics used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0.)|  
|**remote proc transaction promotion**|Use this option to protect the actions of a server-to-server procedure through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) transaction. When this option is TRUE (or ON) calling a remote stored procedure starts a distributed transaction and enlists the transaction with MS DTC. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] making the remote stored procedure call is the transaction originator and controls the completion of the transaction. When a subsequent COMMIT TRANSACTION or ROLLBACK TRANSACTION statement is issued for the connection, the controlling instance requests that MS DTC manage the completion of the distributed transaction across the computers involved.<br /><br /> After a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction has been started, remote stored procedure calls can be made to other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that have been defined as linked servers. The linked servers are all enlisted in the [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction, and MS DTC ensures that the transaction is completed against each linked server.<br /><br /> If this option is set to FALSE (or OFF), a local transaction will not be promoted to a distributed transaction while calling a remote procedure call on a linked server.<br /><br /> If before making a server-to-server procedure call, the transaction is already a distributed transaction, then this option does not have effect. The procedure call against linked server will run under the same distributed transaction.<br /><br /> If before making a server-to-server procedure call, there is no transaction active in the connection, then this option does not have effect. The procedure then runs against linked server without active transactions.<br /><br /> The default value for this option is TRUE (or ON).|  
  
 [ **@optvalue =**] **'**_option_value_**'**  
 Specifies whether or not the *option_name* should be enabled (**TRUE** or **on**) or disabled (**FALSE** or **off**). *option_value* is **varchar(**10**)**, with no default.  
  
 *option_value* may be a nonnegative integer for the **connect timeout** and **query timeout** options. For the **collation name** option, *option_value* may be a collation name or NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 If the **collation compatible** option is set to TRUE, **collation name** automatically will be set to NULL. If **collation name** is set to a nonnull value, **collation compatible** automatically will be set to FALSE.  
  
## Permissions  
 Requires ALTER ANY LINKED SERVER permission on the server.  
  
## Examples  
 The following example configures a linked server corresponding to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], `SEATTLE3`, to be collation compatible with the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
USE master;  
EXEC sp_serveroption 'SEATTLE3', 'collation compatible', 'true';  
```  
  
## See Also  
 [Distributed Queries Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)   
 [sp_adddistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_dropdistpublisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md)   
 [sp_helpserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
