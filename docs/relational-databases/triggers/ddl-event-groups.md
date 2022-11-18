---
description: "DDL Event Groups"
title: "DDL Event Groups | Microsoft Docs"
ms.custom: ""
ms.date: "03/28/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "event groups"
  - "DDL event groups"
  - "DDL triggers, event groups"
ms.assetid: 12b45cc3-2f91-4609-bb8a-3e82e28bf642
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DDL Event Groups
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The following tables list the DDL event groups that can be used to run a DDL trigger or an event notification, and also the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements they cover. Note the inclusive nature of the event groups. For example, a DDL trigger or event notification that specifies FOR DDL_TABLE_EVENTS (10018) covers the CREATE TABLE, ALTER TABLE and DROP TABLE [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. A DDL trigger or event notification that specifies FOR DDL_TABLE_VIEW_EVENTS (10017) covers all [!INCLUDE[tsql](../../includes/tsql-md.md)] statements under the types DDL_TABLE_EVENTS, DDL_VIEW_EVENTS, DDL_INDEX_EVENTS, and DDL_STATISTICS_EVENTS.  
  
> [!NOTE]  
>  Certain system stored procedures that perform DDL-like operations can also fire DDL triggers or event notifications. Test your DDL triggers and event notifications to determine their responses to system stored procedures that are run. For example, the CREATE TYPE statement and **sp_addtype** stored procedure will both fire a DDL trigger or event notification that is created on a CREATE_TYPE event.  
  
## Events  
 The events listed under DDL_DATABASE_LEVEL_EVENTS execute at the server (instance) or database level. Events listed under DDL_SERVER_LEVEL_EVENTS execute only at the server level.  
  
|parent_type|type|name|  
|-|-|-|
|NULL|296|ALTER_SERVER_CONFIGURATION|  
|NULL|10001|DDL_EVENTS|  
|10001|10016|   DDL_DATABASE_LEVEL_EVENTS|  
|10016|10027|      DDL_ASSEMBLY_EVENTS|  
|10027|102|         ALTER_ASSEMBLY|  
|10027|101|         CREATE_ASSEMBLY|  
|10027|103|         DROP_ASSEMBLY|  
|10016|10029|      DDL_DATABASE_SECURITY_EVENTS|  
|10029|10033|         DDL_APPLICATION_ROLE_EVENTS|  
|10033|138|            ALTER_APPLICATION_ROLE|  
|10033|137|            CREATE_APPLICATION_ROLE|  
|10033|139|            DROP_APPLICATION_ROLE|  
|10029|10038|         DDL_ASYMMETRIC_KEY_EVENTS|  
|10038|248|            ALTER_ASYMMETRIC_KEY|  
|10038|247|            CREATE_ASYMMETRIC_KEY|  
|10038|249|            DROP_ASYMMETRIC_KEY|  
|10029|10036|         DDL_AUTHORIZATION_DATABASE_EVENTS|  
|10036|205|            ALTER_AUTHORIZATION_DATABASE|  
|10029|10030|         DDL_CERTIFICATE_EVENTS|  
|10030|198|            ALTER_CERTIFICATE|  
|10030|197|            CREATE_CERTIFICATE|  
|10030|199|            DROP_CERTIFICATE|  
|10029|10039|         DDL_CRYPTO_SIGNATURE_EVENTS|  
|10039|257|            ADD_SIGNATURE|  
|10039|255|            ADD_SIGNATURE_SCHEMA_OBJECT|  
|10039|258|            DROP_SIGNATURE|  
|10039|256|            DROP_SIGNATURE_SCHEMA_OBJECT|  
|10029|10066|         DDL_DATABASE_AUDIT_SPECIFICATION_EVENTS|  
|10066|291|            ALTER_DATABASE_AUDIT_SPECIFICATION|  
|10066|290|            CREATE_DATABASE_AUDIT_SPECIFICATION|  
|10066|292|            DROP_DATABASE_AUDIT_SPECIFICATION|  
|10029|10062|         DDL_DATABASE_ENCRYPTION_KEY_EVENTS|  
|10062|279|            ALTER_DATABASE_ENCRYPTION_KEY|  
|10062|278|            CREATE_DATABASE_ENCRYPTION_KEY|  
|10062|280|            DROP_DATABASE_ENCRYPTION_KEY|  
|10029|10035|         DDL_GDR_DATABASE_EVENTS|  
|10035|171|            DENY_DATABASE|  
|10035|170|            GRANT_DATABASE|  
|10035|172|            REVOKE_DATABASE|  
|10029|10040|         DDL_MASTER_KEY_EVENTS|  
|10040|253|            ALTER_MASTER_KEY|  
|10040|252|            CREATE_MASTER_KEY|  
|10040|254|            DROP_MASTER_KEY|  
|10029|10032|         DDL_ROLE_EVENTS|  
|10032|207|            ADD_ROLE_MEMBER|  
|10032|135|            ALTER_ROLE|  
|10032|134|            CREATE_ROLE|  
|10032|136|            DROP_ROLE|  
|10032|208|            DROP_ROLE_MEMBER|  
|10029|10034|         DDL_SCHEMA_EVENTS|  
|10034|142|            ALTER_SCHEMA|  
|10034|141|            CREATE_SCHEMA|  
|10034|143|            DROP_SCHEMA|  
|10029|10037|         DDL_SYMMETRIC_KEY_EVENTS|  
|10037|245|            ALTER_SYMMETRIC_KEY|  
|10037|244|            CREATE_SYMMETRIC_KEY|  
|10037|246|            DROP_SYMMETRIC_KEY|  
|10029|10031|         DDL_USER_EVENTS|  
|10031|132|            ALTER_USER|  
|10031|131|            CREATE_USER|  
|10031|133|            DROP_USER|  
|10016|10052|      DDL_DEFAULT_EVENTS|  
|10052|218|         BIND_DEFAULT|  
|10052|220|         CREATE_DEFAULT|  
|10052|231|         DROP_DEFAULT|  
|10052|242|         UNBIND_DEFAULT|  
|10016|10026|      DDL_EVENT_NOTIFICATION_EVENTS|  
|10026|74|         CREATE_EVENT_NOTIFICATION|  
|10026|76|         DROP_EVENT_NOTIFICATION|  
|10016|10053|      DDL_EXTENDED_PROPERTY_EVENTS|  
|10053|211|         ALTER_EXTENDED_PROPERTY|  
|10053|222|         CREATE_EXTENDED_PROPERTY|  
|10053|233|         DROP_EXTENDED_PROPERTY|  
|10016|10054|      DDL_FULLTEXT_CATALOG_EVENTS|  
|10054|212|         ALTER_FULLTEXT_CATALOG|  
|10054|223|         CREATE_FULLTEXT_CATALOG|  
|10054|234|         DROP_FULLTEXT_CATALOG|  
|10016|10067|      DDL_FULLTEXT_STOPLIST_EVENTS|  
|10067|294|         ALTER_FULLTEXT_STOPLIST|  
|10067|293|         CREATE_FULLTEXT_STOPLIST|  
|10067|295|         DROP_FULLTEXT_STOPLIST|  
|10016|10023|      DDL_FUNCTION_EVENTS|  
|10023|62|         ALTER_FUNCTION|  
|10023|61|         CREATE_FUNCTION|  
|10023|63|         DROP_FUNCTION|  
|10016|10049|      DDL_PARTITION_EVENTS|  
|10049|10050|         DDL_PARTITION_FUNCTION_EVENTS|  
|10050|192|            ALTER_PARTITION_FUNCTION|  
|10050|191|            CREATE_PARTITION_FUNCTION|  
|10050|193|            DROP_PARTITION_FUNCTION|  
|10049|10051|         DDL_PARTITION_SCHEME_EVENTS|  
|10051|195|            ALTER_PARTITION_SCHEME|  
|10051|194|            CREATE_PARTITION_SCHEME|  
|10051|196|            DROP_PARTITION_SCHEME|  
|10016|10055|      DDL_PLAN_GUIDE_EVENTS|  
|10055|216|         ALTER_PLAN_GUIDE|  
|10055|228|         CREATE_PLAN_GUIDE|  
|10055|238|         DROP_PLAN_GUIDE|  
|10016|10024|      DDL_PROCEDURE_EVENTS|  
|10024|52|         ALTER_PROCEDURE|  
|10024|51|         CREATE_PROCEDURE|  
|10024|53|         DROP_PROCEDURE|  
|10016|10056|      DDL_RULE_EVENTS|  
|10056|219|         BIND_RULE|  
|10056|229|         CREATE_RULE|  
|10056|239|         DROP_RULE|  
|10056|243|         UNBIND_RULE|  
|10016|10069|      DDL_SEARCH_PROPERTY_LIST_EVENTS|  
|10069|298|         ALTER_SEARCH_PROPERTY_LIST|  
|10069|297|         CREATE_SEARCH_PROPERTY_LIST|  
|10069|299|         DROP_SEARCH_PROPERTY_LIST|  
|10016|10070|      DDL_SEQUENCE_EVENTS|  
|10070|304|         ALTER_SEQUENCE|  
|10070|303|         CREATE_SEQUENCE|  
|10070|305|         DROP_SEQUENCE|  
|10016|10041|      DDL_SSB_EVENTS|  
|10041|10063|         DDL_BROKER_PRIORITY_EVENTS|  
|10063|282|            ALTER_BROKER_PRIORITY|  
|10063|281|            CREATE_BROKER_PRIORITY|  
|10063|283|            DROP_BROKER_PRIORITY|  
|10041|10043|         DDL_CONTRACT_EVENTS|  
|10043|154|            CREATE_CONTRACT|  
|10043|156|            DROP_CONTRACT|  
|10041|10042|         DDL_MESSAGE_TYPE_EVENTS|  
|10042|152|            ALTER_MESSAGE_TYPE|  
|10042|151|            CREATE_MESSAGE_TYPE|  
|10042|153|            DROP_MESSAGE_TYPE|  
|10041|10044|         DDL_QUEUE_EVENTS|  
|10044|158|            ALTER_QUEUE|  
|10044|157|            CREATE_QUEUE|  
|10044|159|            DROP_QUEUE|  
|10041|10047|         DDL_REMOTE_SERVICE_BINDING_EVENTS|  
|10047|175|            ALTER_REMOTE_SERVICE_BINDING|  
|10047|174|            CREATE_REMOTE_SERVICE_BINDING|  
|10047|176|            DROP_REMOTE_SERVICE_BINDING|  
|10041|10046|         DDL_ROUTE_EVENTS|  
|10046|165|            ALTER_ROUTE|  
|10046|164|            CREATE_ROUTE|  
|10046|166|            DROP_ROUTE|  
|10041|10045|         DDL_SERVICE_EVENTS|  
|10045|162|            ALTER_SERVICE|  
|10045|161|            CREATE_SERVICE|  
|10045|163|            DROP_SERVICE|  
|10016|10022|      DDL_SYNONYM_EVENTS|  
|10022|34|         CREATE_SYNONYM|  
|10022|36|         DROP_SYNONYM|  
|10016|10017|      DDL_TABLE_VIEW_EVENTS|  
|10017|10020|         DDL_INDEX_EVENTS|  
|10020|213|            ALTER_FULLTEXT_INDEX|  
|10020|25|            ALTER_INDEX|  
|10020|224|            CREATE_FULLTEXT_INDEX|  
|10020|24|            CREATE_INDEX|  
|10020|274|            CREATE_SPATIAL_INDEX|  
|10020|206|            CREATE_XML_INDEX|  
|10020|235|            DROP_FULLTEXT_INDEX|  
|10020|26|            DROP_INDEX|  
|10017|10021|         DDL_STATISTICS_EVENTS|  
|10021|27|            CREATE_STATISTICS|  
|10021|29|            DROP_STATISTICS|  
|10021|28|            UPDATE_STATISTICS|  
|10017|10018|         DDL_TABLE_EVENTS|  
|10018|22|            ALTER_TABLE|  
|10018|21|            CREATE_TABLE|  
|10018|23|            DROP_TABLE|  
|10017|10019|         DDL_VIEW_EVENTS|  
|10019|42|            ALTER_VIEW|  
|10019|41|            CREATE_VIEW|  
|10019|43|            DROP_VIEW|  
|10016|10025|      DDL_TRIGGER_EVENTS|  
|10025|72|         ALTER_TRIGGER|  
|10025|71|         CREATE_TRIGGER|  
|10025|73|         DROP_TRIGGER|  
|10016|10028|      DDL_TYPE_EVENTS|  
|10028|91|         CREATE_TYPE|  
|10028|93|         DROP_TYPE|  
|10016|10048|      DDL_XML_SCHEMA_COLLECTION_EVENTS|  
|10048|178|         ALTER_XML_SCHEMA_COLLECTION|  
|10048|177|         CREATE_XML_SCHEMA_COLLECTION|  
|10048|179|         DROP_XML_SCHEMA_COLLECTION|  
|10016|241|      RENAME|  
|10001|10002|   DDL_SERVER_LEVEL_EVENTS|  
|10002|214|      ALTER_INSTANCE|  
|10002|10071|      DDL_AVAILABILITY_GROUP_EVENTS|  
|10071|307|         ALTER_AVAILABILITY_GROUP|  
|10071|306|         CREATE_AVAILABILITY_GROUP|  
|10071|308|         DROP_AVAILABILITY_GROUP|  
|10002|10004|      DDL_DATABASE_EVENTS|  
|10004|202|         ALTER_DATABASE|  
|10004|201|         CREATE_DATABASE|  
|10004|203|         DROP_DATABASE|  
|10002|10003|      DDL_ENDPOINT_EVENTS|  
|10003|182|         ALTER_ENDPOINT|  
|10003|181|         CREATE_ENDPOINT|  
|10003|183|         DROP_ENDPOINT|  
|10002|10057|      DDL_EVENT_SESSION_EVENTS|  
|10057|265|         ALTER_EVENT_SESSION|  
|10057|264|         CREATE_EVENT_SESSION|  
|10057|266|         DROP_EVENT_SESSION|  
|10002|10011|      DDL_EXTENDED_PROCEDURE_EVENTS|  
|10011|221|         CREATE_EXTENDED_PROCEDURE|  
|10011|232|         DROP_EXTENDED_PROCEDURE|  
|10002|10012|      DDL_LINKED_SERVER_EVENTS|  
|10012|263|         ALTER_LINKED_SERVER|  
|10012|225|         CREATE_LINKED_SERVER|  
|10012|10013|         DDL_LINKED_SERVER_LOGIN_EVENTS|  
|10013|226|            CREATE_LINKED_SERVER_LOGIN|  
|10013|236|            DROP_LINKED_SERVER_LOGIN|  
|10012|262|         DROP_LINKED_SERVER|  
|10002|10014|      DDL_MESSAGE_EVENTS|  
|10014|215|         ALTER_MESSAGE|  
|10014|227|         CREATE_MESSAGE|  
|10014|237|         DROP_MESSAGE|  
|10002|10015|      DDL_REMOTE_SERVER_EVENTS|  
|10015|217|         ALTER_REMOTE_SERVER|  
|10015|230|         CREATE_REMOTE_SERVER|  
|10015|240|         DROP_REMOTE_SERVER|  
|10002|10058|      DDL_RESOURCE_GOVERNOR_EVENTS|  
|10058|273|         ALTER_RESOURCE_GOVERNOR_CONFIG|  
|10058|10059|         DDL_RESOURCE_POOL|  
|10059|268|            ALTER_RESOURCE_POOL|  
|10059|267|            CREATE_RESOURCE_POOL|  
|10059|269|            DROP_RESOURCE_POOL|  
|10058|10060|         DDL_WORKLOAD_GROUP|  
|10060|271|            ALTER_WORKLOAD_GROUP|  
|10060|270|            CREATE_WORKLOAD_GROUP|  
|10060|272|            DROP_WORKLOAD_GROUP|  
|10002|10005|      DDL_SERVER_SECURITY_EVENTS|  
|10005|209|         ADD_SERVER_ROLE_MEMBER|  
|10005|301|         ALTER_SERVER_ROLE|  
|10005|300|         CREATE_SERVER_ROLE|  
|10005|10008|         DDL_AUTHORIZATION_SERVER_EVENTS|  
|10008|204|            ALTER_AUTHORIZATION_SERVER|  
|10005|10009|         DDL_CREDENTIAL_EVENTS|  
|10009|260|            ALTER_CREDENTIAL|  
|10009|259|            CREATE_CREDENTIAL|  
|10009|261|            DROP_CREDENTIAL|  
|10005|10061|         DDL_CRYPTOGRAPHIC_PROVIDER_EVENTS|  
|10061|276|            ALTER_CRYPTOGRAPHIC_PROVIDER|  
|10061|275|            CREATE_CRYPTOGRAPHIC_PROVIDER|  
|10061|277|            DROP_CRYPTOGRAPHIC_PROVIDER|  
|10005|10007|         DDL_GDR_SERVER_EVENTS|  
|10007|168|            DENY_SERVER|  
|10007|167|            GRANT_SERVER|  
|10007|169|            REVOKE_SERVER|  
|10005|10006|         DDL_LOGIN_EVENTS|  
|10006|145|            ALTER_LOGIN|  
|10006|144|            CREATE_LOGIN|  
|10006|146|            DROP_LOGIN|  
|10005|10064|         DDL_SERVER_AUDIT_EVENTS|  
|10064|285|            ALTER_SERVER_AUDIT|  
|10064|284|            CREATE_SERVER_AUDIT|  
|10064|286|            DROP_SERVER_AUDIT|  
|10005|10065|         DDL_SERVER_AUDIT_SPECIFICATION_EVENTS|  
|10065|288|            ALTER_SERVER_AUDIT_SPECIFICATION|  
|10065|287|            CREATE_SERVER_AUDIT_SPECIFICATION|  
|10065|289|            DROP_SERVER_AUDIT_SPECIFICATION|  
|10005|10010|         DDL_SERVICE_MASTER_KEY_EVENTS|  
|10010|251|            ALTER_SERVICE_MASTER_KEY|  
|10005|302|         DROP_SERVER_ROLE|  
|10005|210|         DROP_SERVER_ROLE_MEMBER|  
  
 The data above can be created by running the following code example.  
  
```
WITH DirectReports(name, parent_type, type, level, sort) AS   
(  
    SELECT CONVERT(varchar(255),type_name), parent_type, type, 1, CONVERT(varchar(255),type_name)  
    FROM sys.trigger_event_types   
    WHERE parent_type IS NULL  
    UNION ALL  
    SELECT  CONVERT(varchar(255), REPLICATE ('|   ' , level) + e.type_name),  
        e.parent_type, e.type, level + 1,  
    CONVERT (varchar(255), RTRIM(sort) + '|   ' + e.type_name)  
    FROM sys.trigger_event_types AS e  
        INNER JOIN DirectReports AS d  
        ON e.parent_type = d.type   
)  
SELECT parent_type, type, name  
FROM DirectReports  
ORDER BY sort;  
```  
  
## See Also  
 [Event Notifications](../../relational-databases/service-broker/event-notifications.md)   
 [DDL Triggers](../../relational-databases/triggers/ddl-triggers.md)   
 [DDL Events](../../relational-databases/triggers/ddl-events.md)  
  
  
