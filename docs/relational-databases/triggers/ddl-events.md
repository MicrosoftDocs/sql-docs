---
title: "DDL Events | Microsoft Docs"
ms.custom: ""
ms.date: "11/01/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "DDL events"
  - "DDL triggers, events"
  - "events [SQL Server], DDL"
ms.assetid: 62ef24b4-3553-4aed-b62a-670980bae501
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DDL Events
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]
  The following tables list the DDL events that can be used to fire a DDL trigger or event notification. Note that each event corresponds to a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or stored procedure, with the statement syntax modified to include an underscore character (_) between keywords.  
  
> [!IMPORTANT]  
>  System stored procedures that perform DDL-like operations also fire DDL triggers and event notifications. Test your DDL triggers and event notifications to determine their responses to system stored procedures that are run. For example, the CREATE TYPE statement and **sp_addtype** stored procedure will both fire a DDL trigger or event notification that is created on a CREATE_TYPE event.  
  
## DDL Statements That Have Server or Database Scope  
 DDL triggers or event notifications can be created to fire in response to the following events when they occur in the database in which the trigger or event notification is created, or anywhere in the server instance.  
  
||||  
|-|-|-|  
|CREATE_APPLICATION_ROLE (Applies to the CREATE APPLICATION ROLE statement and **sp_addapprole**. If a new schema is created, this event also triggers a CREATE_SCHEMA event.)|ALTER_APPLICATION_ROLE (Applies to the ALTER APPLICATION ROLE statement and **sp_approlepassword**.)|DROP_APPLICATION_ROLE (Applies to the DROP APPLICATION ROLE statement and **sp_dropapprole**.)|  
|CREATE_ASSEMBLY|ALTER_ASSEMBLY|DROP_ASSEMBLY|  
|CREATE_ASYMMETRIC_KEY|ALTER_ASYMMETRIC_KEY|DROP_ASYMMETRIC_KEY|  
|ALTER_AUTHORIZATION|ALTER_AUTHORIZATION_DATABASE (Applies to the ALTER AUTHORIZATION statement when ON DATABASE is specified, and **sp_changedbowner**.)||  
|CREATE_BROKER_PRIORITY|CREATE_BROKER_PRIORITY|CREATE_BROKER_PRIORITY|  
|CREATE_CERTIFICATE|ALTER_CERTIFICATE|DROP_CERTIFICATE|  
|CREATE_CONTRACT|DROP_CONTRACT||  
|CREATE_CREDENTIAL|ALTER_CREDENTIAL|DROP_CREDENTIAL|  
|GRANT_DATABASE|DENY_DATABASE|REVOKE_DATABASE|  
|CREATE_DATABASE_AUDIT_SPEFICIATION|ALTER_DATABASE_AUDIT_SPEFICIATION|DENY_DATABASE_AUDIT_SPEFICIATION|  
|CREATE_DATABASE_ENCRYPTION_KEY|ALTER_DATABASE_ENCRYPTION_KEY|DROP_DATABASE_ENCRYPTION_KEY|  
|CREATE_DEFAULT|DROP_DEFAULT||  
|BIND_DEFAULT (Applies to **sp_bindefault**.)|UNBIND_DEFAULT (Applies to **sp_unbindefault**.)||  
|CREATE_EVENT_NOTIFICATION|DROP_EVENT_NOTIFICATION||  
|CREATE_EXTENDED_PROPERTY (Applies to **sp_addextendedproperty**.)|ALTER_EXTENDED_PROPERTY (Applies to **sp_updateextendedproperty**.)|DROP_EXTENDED_PROPERTY (Applies to **sp_dropextendedproperty**.)|  
|CREATE_FULLTEXT_CATALOG (Applies to the CREATE FULLTEXT CATALOG statement and **sp_fulltextcatalog** when *create* is specified.)|ALTER_FULLTEXT_CATALOG (Applies to the ALTER FULLTEXT CATALOG statement, **sp_fulltextcatalog** when *start_incremental*, *start_full*, *Stop*, or *Rebuild* is specified, and **sp_fulltext_database** when *enable* is specified.)|DROP_FULLTEXT_CATALOG (Applies to the DROP FULLTEXT CATALOG statement and **sp_fulltextcatalog** when *drop* is specified.)|  
|CREATE_FULLTEXT_INDEX (Applies to the CREATE FULLTEXT INDEX statement and **sp_fulltexttable** when *create* is specified.)|ALTER_FULLTEXT_INDEX (Applies to the ALTER FULLTEXT INDEX statement, **sp_fulltextcatalog** when *start_full*, *start_incremental*, or *stop* is specified, **sp_fulltext_column**, and **sp_fulltext_table** when any action other than *create* or *drop* is specified.)|DROP_FULLTEXT_INDEX (Applies to the DROP FULLTEXT INDEX statement and **sp_fulltexttable** when *drop* is specified.)|  
|CREATE_FULLTEXT_STOPLIST|ALTER_FULLTEXT_STOPLIST|DROP_FULLTEXT_STOPLIST|  
|CREATE_FUNCTION|ALTER_FUNCTION|DROP_FUNCTION|  
|CREATE_INDEX|ALTER_INDEX (Applies to the ALTER INDEX statement and **sp_indexoption**.)|DROP_INDEX|  
|CREATE_MASTER_KEY|ALTER_MASTER_KEY|DROP_MASTER_KEY|  
|CREATE_MESSAGE_TYPE|ALTER_MESSAGE_TYPE|DROP_MESSAGE_TYPE|  
|CREATE_PARTITION_FUNCTION|ALTER_PARTITION_FUNCTION|DROP_PARTITION_FUNCTION|  
|CREATE_PARTITION_SCHEME|ALTER_PARTITION_SCHEME|DROP_PARTITION_SCHEME|  
|CREATE_PLAN_GUIDE (Applies to **sp_create_plan_guide**.)|ALTER_PLAN_GUIDE (Applies to **sp_control_plan_guide** when ENABLE, ENABLE ALL, DISABLE, or DISABLE ALL is specified.)|DROP_PLAN_GUIDE (Applies to **sp_control_plan_guide** when DROP or DROP ALL is specified.)|  
|CREATE_PROCEDURE|ALTER_PROCEDURE (Applies to the ALTER PROCEDURE statement and **sp_procoption**.)|DROP_PROCEDURE|  
|CREATE_QUEUE|ALTER_QUEUE|DROP_QUEUE|  
|CREATE_REMOTE_SERVICE_BINDING|ALTER_REMOTE_SERVICE_BINDING|DROP_REMOTE_SERVICE_BINDING|  
|CREATE_SPATIAL_INDEX|||  
|RENAME (Applies to **sp_rename**)|||  
|CREATE_ROLE (Applies to the CREATE ROLE statement, **sp_addrole**, and **sp_addgroup**.)|ALTER_ROLE|DROP_ROLE (Applies to the DROP ROLE statement, **sp_droprole**, and **sp_dropgroup**.)|  
|ADD_ROLE_MEMBER|DROP_ROLE_MEMBER||  
|CREATE_ROUTE|ALTER_ROUTE|DROP_ROUTE|  
|CREATE_RULE|DROP_RULE||  
|BIND_RULE (Applies to **sp_bindrule**.)|UNBIND_RULE (Applies to **sp_unbindrule**.)||  
|CREATE_SCHEMA (Applies to the CREATE SCHEMA statement, **sp_addrole**, **sp_adduser**, **sp_addgroup**, and **sp_grantdbaccess**.)|ALTER_SCHEMA (Applies to the ALTER SCHEMA statement and **sp_changeobjectowner**.)|DROP_SCHEMA|  
|CREATE_SEARCH_PROPERTY_LIST|ALTER_SEARCH_PROPERTY_LIST|DROP_SEARCH_PROPERTY_LIST|  
|CREATE_SEQUENCE_EVENTS|CREATE_SEQUENCE_EVENTS|CREATE_SEQUENCE_EVENTS|  
|CREATE_SERVER_ROLE|ALTER_SERVER_ROLE|DROP_SERVER_ROLE|  
|CREATE_SERVICE|ALTER_SERVICE|DROP_SERVICE|  
|ALTER_SERVICE_MASTER_KEY|BACKUP_SERVICE_MASTER_KEY|RESTORE_SERVICE_MASTER_KEY|  
|ADD_SIGNATURE (for signature operations on non-schema scoped objects; database, assembly, trigger)|DROP_SIGNATURE||  
|ADD_SIGNATURE_SCHEMA_OBJECT (for schema scoped objects; stored procedures, functions)|DROP_SIGNATURE_SCHEMA_OBJECT||  
|CREATE_SPATIAL_INDEX|ALTER_INDEX can be used for spatial indexes.|DROP_INDEX can be used for spatial indexes.|  
|CREATE_STATISTICS|DROP_STATISTICS|UPDATE_STATISTICS|  
|CREATE_SYMMETRIC_KEY|ALTER_SYMMETRIC_KEY|DROP_SYMMETRIC_KEY|  
|CREATE_SYNONYM|DROP_SYNONYM||  
|CREATE_TABLE|ALTER_TABLE (Applies to the ALTER TABLE statement and **sp_tableoption**.)|DROP_TABLE|  
|CREATE_TRIGGER|ALTER_TRIGGER (Applies to the ALTER TRIGGER statement and **sp_settriggerorder**.)|DROP_TRIGGER|  
|CREATE_TYPE (Applies to the CREATE TYPE statement and **sp_addtype**.)|DROP_TYPE (Applies to the DROP TYPE statement and **sp_droptype**.)||  
|CREATE_USER (Applies to the CREATE USER statement, **sp_adduser**, and **sp_grantdbaccess**.)|ALTER_USER (Applies to ALTER USER statement and **sp_change_users_login**.)|DROP_USER (Applies to the DROP USER statement, **sp_dropuser**, and **sp_revokedbaccess**.)|  
|CREATE_VIEW|ALTER_VIEW|DROP_VIEW|  
|CREATE_XML_INDEX|ALTER_INDEX can be used for XML indexes.|DROP_INDEX can be used for XML indexes.|  
|CREATE_XML_SCHEMA_COLLECTION|ALTER_XML_SCHEMA_COLLECTION|DROP_XML_SCHEMA_COLLECTION|  
  
## DDL Statements That Have Server Scope  
 DDL triggers or event notifications can be created to fire in response to the following events when they occur anywhere in the server instance.  
  
||||  
|-|-|-|  
|ALTER_AUTHORIZATION_SERVER|ALTER_SERVER_CONFIGURATION|ALTER_INSTANCE (Applies to **sp_configure** and **sp_addserver** when a local server instance is specified.)|  
|CREATE_AVAILABILITY_GROUP|ALTER_AVAILABILITY_GROUP|DROP_AVAILABILITY_GROUP|  
|CREATE_CREDENTIAL|ALTER_CREDENTIAL|DROP_CREDENTIAL|  
|CREATE_CRYPTOGRAPHIC_PROVIDER|ALTER_CRYPTOGRAPHIC_PROVIDER|DROP_CRYPTOGRAPHIC_PROVIDER|  
|CREATE_DATABASE|ALTER_DATABASE (Applies to the ALTER DATABASE statement and **sp_fulltext_database**.)|DROP_DATABASE|  
|CREATE_ENDPOINT|ALTER_ENDPOINT|DROP_ENDPOINT|  
|CREATE_EVENT_SESSION|ALTER_EVENT_SESSION|DROP_EVENT_SESSION|  
|CREATE_EXTENDED_PROCEDURE (Applies to **sp_addextendedproc**.)|DROP_EXTENDED_PROCEDURE (Applies to **sp_dropextendedproc**.)||  
|CREATE_LINKED_SERVER (Applies to **sp_addlinkedserver**.)|ALTER_LINKED_SERVER (Applies to **sp_serveroption**.)|DROP_LINKED_SERVER (Applies to **sp_dropserver** when a linked server is specified.)|  
|CREATE_LINKED_SERVER_LOGIN (Applies to **sp_addlinkedsrvlogin**.)|DROP_LINKED_SERVER_LOGIN (Applies to **sp_droplinkedsrvlogin**.)||  
|CREATE_LOGIN (Applies to the CREATE LOGIN statement, **sp_addlogin**, **sp_grantlogin**, **xp_grantlogin**, and **sp_denylogin** when used on a nonexistent login that must be implicitly created.)|ALTER_LOGIN (Applies to the ALTER LOGIN statement, **sp_defaultdb**, **sp_defaultlanguage**, **sp_password**, and **sp_change_users_login** when *Auto_Fix* is specified.)|DROP_LOGIN (Applies to the DROP LOGIN statement, **sp_droplogin**, **sp_revokelogin**, and **xp_revokelogin**.)|  
|CREATE_MESSAGE (Applies to **sp_addmessage**.)|ALTER_MESSAGE (Applies to **sp_altermessage**.)|DROP_MESSAGE (Applies to **sp_dropmessage**.)|  
|CREATE_REMOTE_SERVER (Applies to **sp_addserver**.)|ALTER_REMOTE_SERVER (Applies to **sp_setnetname**.)|DROP_REMOTE_SERVER (Applies to **sp_dropserver** when a remote server is specified.)|  
|CREATE_RESOURCE_POOL|ALTER_RESOURCE_POOL|DROP_RESOURCE_POOL|  
|GRANT_SERVER|DENY_SERVER|REVOKE_SERVER|  
|ADD_SERVER_ROLE_MEMBER|DROP_SERVER_ROLE_MEMBER||  
|CREATE_SERVER_AUDIT|ALTER_SERVER_AUDIT|DROP_SERVER_AUDIT|  
|CREATE_SERVER_AUDIT_SPECIFICATION|ALTER_SERVER_AUDIT_SPECIFICATION|DROP_SERVER_AUDIT_SPECIFICATION|  
|CREATE_WORKLOAD_GROUP|ALTER_WORKLOAD_GROUP|DROP_WORKLOAD_GROUP|  
  
## See Also  
 [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md)   
 [Event Notifications](../../relational-databases/service-broker/event-notifications.md)   
 [DDL Event Groups](../../relational-databases/triggers/ddl-event-groups.md)  
  
  
