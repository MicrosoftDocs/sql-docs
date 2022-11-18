---
description: "DDL Events"
title: "DDL Events | Microsoft Docs"
ms.custom: ""
ms.date: "11/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: conceptual
helpviewer_keywords: 
  - "DDL events"
  - "DDL triggers, events"
  - "events [SQL Server], DDL"
ms.assetid: 62ef24b4-3553-4aed-b62a-670980bae501
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DDL Events
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The following tables list the DDL events that can be used to fire a DDL trigger or event notification. Note that each event corresponds to a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or stored procedure, with the statement syntax modified to include an underscore character (_) between keywords.  
  
> [!IMPORTANT]  
>  System stored procedures that perform DDL-like operations also fire DDL triggers and event notifications. Test your DDL triggers and event notifications to determine their responses to system stored procedures that are run. For example, the CREATE TYPE statement and **sp_addtype** stored procedure will both fire a DDL trigger or event notification that is created on a CREATE_TYPE event.  
  
## DDL Statements That Have Server or Database Scope  
 DDL triggers or event notifications can be created to fire in response to the following events when they occur in the database in which the trigger or event notification is created, or anywhere in the server instance.  
  
:::row:::
    :::column:::
        CREATE_APPLICATION_ROLE (Applies to the CREATE APPLICATION ROLE statement and **sp_addapprole**. If a new schema is created, this event also triggers a CREATE_SCHEMA event.)
    :::column-end:::
    :::column:::
        ALTER_APPLICATION_ROLE (Applies to the ALTER APPLICATION ROLE statement and **sp_approlepassword**.)
    :::column-end:::
    :::column:::
        DROP_APPLICATION_ROLE (Applies to the DROP APPLICATION ROLE statement and **sp_dropapprole**.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_ASSEMBLY
    :::column-end:::
    :::column:::
        ALTER_ASSEMBLY
    :::column-end:::
    :::column:::
        DROP_ASSEMBLY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_ASYMMETRIC_KEY
    :::column-end:::
    :::column:::
        ALTER_ASYMMETRIC_KEY
    :::column-end:::
    :::column:::
        DROP_ASYMMETRIC_KEY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        ALTER_AUTHORIZATION
    :::column-end:::
    :::column:::
        ALTER_AUTHORIZATION_DATABASE (Applies to the ALTER AUTHORIZATION statement when ON DATABASE is specified, and **sp_changedbowner**.)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_BROKER_PRIORITY
    :::column-end:::
    :::column:::
        CREATE_BROKER_PRIORITY
    :::column-end:::
    :::column:::
        CREATE_BROKER_PRIORITY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_CERTIFICATE
    :::column-end:::
    :::column:::
        ALTER_CERTIFICATE
    :::column-end:::
    :::column:::
        DROP_CERTIFICATE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_CONTRACT
    :::column-end:::
    :::column:::
        DROP_CONTRACT
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_CREDENTIAL
    :::column-end:::
    :::column:::
        ALTER_CREDENTIAL
    :::column-end:::
    :::column:::
        DROP_CREDENTIAL
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        GRANT_DATABASE
    :::column-end:::
    :::column:::
        DENY_DATABASE
    :::column-end:::
    :::column:::
        REVOKE_DATABASE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_DATABASE_AUDIT_SPECIFICATION
    :::column-end:::
    :::column:::
        ALTER_DATABASE_AUDIT_SPECIFICATION
    :::column-end:::
    :::column:::
        DROP_DATABASE_AUDIT_SPECIFICATION
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_DATABASE_ENCRYPTION_KEY
    :::column-end:::
    :::column:::
        ALTER_DATABASE_ENCRYPTION_KEY
    :::column-end:::
    :::column:::
        DROP_DATABASE_ENCRYPTION_KEY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_DEFAULT
    :::column-end:::
    :::column:::
        DROP_DEFAULT
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        BIND_DEFAULT (Applies to **sp_bindefault**.)
    :::column-end:::
    :::column:::
        UNBIND_DEFAULT (Applies to **sp_unbindefault**.)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_EVENT_NOTIFICATION
    :::column-end:::
    :::column:::
        DROP_EVENT_NOTIFICATION
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_EXTENDED_PROPERTY (Applies to **sp_addextendedproperty**.)
    :::column-end:::
    :::column:::
        ALTER_EXTENDED_PROPERTY (Applies to **sp_updateextendedproperty**.)
    :::column-end:::
    :::column:::
        DROP_EXTENDED_PROPERTY (Applies to **sp_dropextendedproperty**.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_FULLTEXT_CATALOG (Applies to the CREATE FULLTEXT CATALOG statement and **sp_fulltextcatalog** when *create* is specified.)
    :::column-end:::
    :::column:::
        ALTER_FULLTEXT_CATALOG (Applies to the ALTER FULLTEXT CATALOG statement, **sp_fulltextcatalog** when *start_incremental*, *start_full*, *Stop*, or *Rebuild* is specified, and **sp_fulltext_database** when *enable* is specified.)
    :::column-end:::
    :::column:::
        DROP_FULLTEXT_CATALOG (Applies to the DROP FULLTEXT CATALOG statement and **sp_fulltextcatalog** when *drop* is specified.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_FULLTEXT_INDEX (Applies to the CREATE FULLTEXT INDEX statement and **sp_fulltexttable** when *create* is specified.)
    :::column-end:::
    :::column:::
        ALTER_FULLTEXT_INDEX (Applies to the ALTER FULLTEXT INDEX statement, **sp_fulltextcatalog** when *start_full*, *start_incremental*, or *stop* is specified, **sp_fulltext_column**, and **sp_fulltext_table** when any action other than *create* or *drop* is specified.)
    :::column-end:::
    :::column:::
        DROP_FULLTEXT_INDEX (Applies to the DROP FULLTEXT INDEX statement and **sp_fulltexttable** when *drop* is specified.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_FULLTEXT_STOPLIST
    :::column-end:::
    :::column:::
        ALTER_FULLTEXT_STOPLIST
    :::column-end:::
    :::column:::
        DROP_FULLTEXT_STOPLIST
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_FUNCTION
    :::column-end:::
    :::column:::
        ALTER_FUNCTION
    :::column-end:::
    :::column:::
        DROP_FUNCTION
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_INDEX
    :::column-end:::
    :::column:::
        ALTER_INDEX (Applies to the ALTER INDEX statement and **sp_indexoption**.)
    :::column-end:::
    :::column:::
        DROP_INDEX
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_MASTER_KEY
    :::column-end:::
    :::column:::
        ALTER_MASTER_KEY
    :::column-end:::
    :::column:::
        DROP_MASTER_KEY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_MESSAGE_TYPE
    :::column-end:::
    :::column:::
        ALTER_MESSAGE_TYPE
    :::column-end:::
    :::column:::
        DROP_MESSAGE_TYPE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_PARTITION_FUNCTION
    :::column-end:::
    :::column:::
        ALTER_PARTITION_FUNCTION
    :::column-end:::
    :::column:::
        DROP_PARTITION_FUNCTION
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_PARTITION_SCHEME
    :::column-end:::
    :::column:::
        ALTER_PARTITION_SCHEME
    :::column-end:::
    :::column:::
        DROP_PARTITION_SCHEME
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_PLAN_GUIDE (Applies to **sp_create_plan_guide**.)
    :::column-end:::
    :::column:::
        ALTER_PLAN_GUIDE (Applies to **sp_control_plan_guide** when ENABLE, ENABLE ALL, DISABLE, or DISABLE ALL is specified.)
    :::column-end:::
    :::column:::
        DROP_PLAN_GUIDE (Applies to **sp_control_plan_guide** when DROP or DROP ALL is specified.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_PROCEDURE
    :::column-end:::
    :::column:::
        ALTER_PROCEDURE (Applies to the ALTER PROCEDURE statement and **sp_procoption**.)
    :::column-end:::
    :::column:::
        DROP_PROCEDURE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_QUEUE
    :::column-end:::
    :::column:::
        ALTER_QUEUE
    :::column-end:::
    :::column:::
        DROP_QUEUE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_REMOTE_SERVICE_BINDING
    :::column-end:::
    :::column:::
        ALTER_REMOTE_SERVICE_BINDING
    :::column-end:::
    :::column:::
        DROP_REMOTE_SERVICE_BINDING
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SPATIAL_INDEX
    :::column-end:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        RENAME (Applies to **sp_rename**)
    :::column-end:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_ROLE (Applies to the CREATE ROLE statement, **sp_addrole**, and **sp_addgroup**.)
    :::column-end:::
    :::column:::
        ALTER_ROLE
    :::column-end:::
    :::column:::
        DROP_ROLE (Applies to the DROP ROLE statement, **sp_droprole**, and **sp_dropgroup**.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        ADD_ROLE_MEMBER
    :::column-end:::
    :::column:::
        DROP_ROLE_MEMBER
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_ROUTE
    :::column-end:::
    :::column:::
        ALTER_ROUTE
    :::column-end:::
    :::column:::
        DROP_ROUTE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_RULE
    :::column-end:::
    :::column:::
        DROP_RULE
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        BIND_RULE (Applies to **sp_bindrule**.)
    :::column-end:::
    :::column:::
        UNBIND_RULE (Applies to **sp_unbindrule**.)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SCHEMA (Applies to the CREATE SCHEMA statement, **sp_addrole**, **sp_adduser**, **sp_addgroup**, and **sp_grantdbaccess**.)
    :::column-end:::
    :::column:::
        ALTER_SCHEMA (Applies to the ALTER SCHEMA statement and **sp_changeobjectowner**.)
    :::column-end:::
    :::column:::
        DROP_SCHEMA
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SEARCH_PROPERTY_LIST
    :::column-end:::
    :::column:::
        ALTER_SEARCH_PROPERTY_LIST
    :::column-end:::
    :::column:::
        DROP_SEARCH_PROPERTY_LIST
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SEQUENCE_EVENTS
    :::column-end:::
    :::column:::
        CREATE_SEQUENCE_EVENTS
    :::column-end:::
    :::column:::
        CREATE_SEQUENCE_EVENTS
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SERVER_ROLE
    :::column-end:::
    :::column:::
        ALTER_SERVER_ROLE
    :::column-end:::
    :::column:::
        DROP_SERVER_ROLE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SERVICE
    :::column-end:::
    :::column:::
        ALTER_SERVICE
    :::column-end:::
    :::column:::
        DROP_SERVICE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        ALTER_SERVICE_MASTER_KEY
    :::column-end:::
    :::column:::
        BACKUP_SERVICE_MASTER_KEY
    :::column-end:::
    :::column:::
        RESTORE_SERVICE_MASTER_KEY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        ADD_SIGNATURE (for signature operations on non-schema scoped objects; database, assembly, trigger)
    :::column-end:::
    :::column:::
        DROP_SIGNATURE
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        ADD_SIGNATURE_SCHEMA_OBJECT (for schema scoped objects; stored procedures, functions)
    :::column-end:::
    :::column:::
        DROP_SIGNATURE_SCHEMA_OBJECT
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SPATIAL_INDEX
    :::column-end:::
    :::column:::
        ALTER_INDEX can be used for spatial indexes.
    :::column-end:::
    :::column:::
        DROP_INDEX can be used for spatial indexes.
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_STATISTICS
    :::column-end:::
    :::column:::
        DROP_STATISTICS
    :::column-end:::
    :::column:::
        UPDATE_STATISTICS
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SYMMETRIC_KEY
    :::column-end:::
    :::column:::
        ALTER_SYMMETRIC_KEY
    :::column-end:::
    :::column:::
        DROP_SYMMETRIC_KEY
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SYNONYM
    :::column-end:::
    :::column:::
        DROP_SYNONYM
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_TABLE
    :::column-end:::
    :::column:::
        ALTER_TABLE (Applies to the ALTER TABLE statement and **sp_tableoption**.)
    :::column-end:::
    :::column:::
        DROP_TABLE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_TRIGGER
    :::column-end:::
    :::column:::
        ALTER_TRIGGER (Applies to the ALTER TRIGGER statement and **sp_settriggerorder**.)
    :::column-end:::
    :::column:::
        DROP_TRIGGER
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_TYPE (Applies to the CREATE TYPE statement and **sp_addtype**.)
    :::column-end:::
    :::column:::
        DROP_TYPE (Applies to the DROP TYPE statement and **sp_droptype**.)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_USER (Applies to the CREATE USER statement, **sp_adduser**, and **sp_grantdbaccess**.)
    :::column-end:::
    :::column:::
        ALTER_USER (Applies to ALTER USER statement and **sp_change_users_login**.)
    :::column-end:::
    :::column:::
        DROP_USER (Applies to the DROP USER statement, **sp_dropuser**, and **sp_revokedbaccess**.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_VIEW
    :::column-end:::
    :::column:::
        ALTER_VIEW
    :::column-end:::
    :::column:::
        DROP_VIEW
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_XML_INDEX
    :::column-end:::
    :::column:::
        ALTER_INDEX can be used for XML indexes.
    :::column-end:::
    :::column:::
        DROP_INDEX can be used for XML indexes.
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_XML_SCHEMA_COLLECTION
    :::column-end:::
    :::column:::
        ALTER_XML_SCHEMA_COLLECTION
    :::column-end:::
    :::column:::
        DROP_XML_SCHEMA_COLLECTION
    :::column-end:::
:::row-end:::
 
## DDL Statements That Have Server Scope  
 DDL triggers or event notifications can be created to fire in response to the following events when they occur anywhere in the server instance.  
  
:::row:::
    :::column:::
        ALTER_AUTHORIZATION_SERVER
    :::column-end:::
    :::column:::
        ALTER_SERVER_CONFIGURATION
    :::column-end:::
    :::column:::
        ALTER_INSTANCE (Applies to **sp_configure** and **sp_addserver** when a local server instance is specified.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_AVAILABILITY_GROUP
    :::column-end:::
    :::column:::
        ALTER_AVAILABILITY_GROUP
    :::column-end:::
    :::column:::
        DROP_AVAILABILITY_GROUP
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_CREDENTIAL
    :::column-end:::
    :::column:::
        ALTER_CREDENTIAL
    :::column-end:::
    :::column:::
        DROP_CREDENTIAL
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_CRYPTOGRAPHIC_PROVIDER
    :::column-end:::
    :::column:::
        ALTER_CRYPTOGRAPHIC_PROVIDER
    :::column-end:::
    :::column:::
        DROP_CRYPTOGRAPHIC_PROVIDER
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_DATABASE
    :::column-end:::
    :::column:::
        ALTER_DATABASE (Applies to the ALTER DATABASE statement and **sp_fulltext_database**.)
    :::column-end:::
    :::column:::
        DROP_DATABASE
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_ENDPOINT
    :::column-end:::
    :::column:::
        ALTER_ENDPOINT
    :::column-end:::
    :::column:::
        DROP_ENDPOINT
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_EVENT_SESSION
    :::column-end:::
    :::column:::
        ALTER_EVENT_SESSION
    :::column-end:::
    :::column:::
        DROP_EVENT_SESSION
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_EXTENDED_PROCEDURE (Applies to **sp_addextendedproc**.)
    :::column-end:::
    :::column:::
        DROP_EXTENDED_PROCEDURE (Applies to **sp_dropextendedproc**.)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_LINKED_SERVER (Applies to **sp_addlinkedserver**.)
    :::column-end:::
    :::column:::
        ALTER_LINKED_SERVER (Applies to **sp_serveroption**.)
    :::column-end:::
    :::column:::
        DROP_LINKED_SERVER (Applies to **sp_dropserver** when a linked server is specified.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_LINKED_SERVER_LOGIN (Applies to **sp_addlinkedsrvlogin**.)
    :::column-end:::
    :::column:::
        DROP_LINKED_SERVER_LOGIN (Applies to **sp_droplinkedsrvlogin**.)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_LOGIN (Applies to the CREATE LOGIN statement, **sp_addlogin**, **sp_grantlogin**, **xp_grantlogin**, and **sp_denylogin** when used on a nonexistent login that must be implicitly created.)
    :::column-end:::
    :::column:::
        ALTER_LOGIN (Applies to the ALTER LOGIN statement, **sp_defaultdb**, **sp_defaultlanguage**, **sp_password**, and **sp_change_users_login** when *Auto_Fix* is specified.)
    :::column-end:::
    :::column:::
        DROP_LOGIN (Applies to the DROP LOGIN statement, **sp_droplogin**, **sp_revokelogin**, and **xp_revokelogin**.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_MESSAGE (Applies to **sp_addmessage**.)
    :::column-end:::
    :::column:::
        ALTER_MESSAGE (Applies to **sp_altermessage**.)
    :::column-end:::
    :::column:::
        DROP_MESSAGE (Applies to **sp_dropmessage**.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_REMOTE_SERVER (Applies to **sp_addserver**.)
    :::column-end:::
    :::column:::
        ALTER_REMOTE_SERVER (Applies to **sp_setnetname**.)
    :::column-end:::
    :::column:::
        DROP_REMOTE_SERVER (Applies to **sp_dropserver** when a remote server is specified.)
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_RESOURCE_POOL
    :::column-end:::
    :::column:::
        ALTER_RESOURCE_POOL
    :::column-end:::
    :::column:::
        DROP_RESOURCE_POOL
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        GRANT_SERVER
    :::column-end:::
    :::column:::
        DENY_SERVER
    :::column-end:::
    :::column:::
        REVOKE_SERVER
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        ADD_SERVER_ROLE_MEMBER
    :::column-end:::
    :::column:::
        DROP_SERVER_ROLE_MEMBER
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SERVER_AUDIT
    :::column-end:::
    :::column:::
        ALTER_SERVER_AUDIT
    :::column-end:::
    :::column:::
        DROP_SERVER_AUDIT
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_SERVER_AUDIT_SPECIFICATION
    :::column-end:::
    :::column:::
        ALTER_SERVER_AUDIT_SPECIFICATION
    :::column-end:::
    :::column:::
        DROP_SERVER_AUDIT_SPECIFICATION
    :::column-end:::
:::row-end:::  
:::row:::
    :::column:::
        CREATE_WORKLOAD_GROUP
    :::column-end:::
    :::column:::
        ALTER_WORKLOAD_GROUP
    :::column-end:::
    :::column:::
        DROP_WORKLOAD_GROUP
    :::column-end:::
:::row-end:::
 
## See Also  
 [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md)   
 [Event Notifications](../../relational-databases/service-broker/event-notifications.md)   
 [DDL Event Groups](../../relational-databases/triggers/ddl-event-groups.md)  
  
  
