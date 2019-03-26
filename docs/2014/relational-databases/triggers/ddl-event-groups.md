---
title: "DDL Event Groups | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "event groups"
  - "DDL event groups"
  - "DDL triggers, event groups"
ms.assetid: 12b45cc3-2f91-4609-bb8a-3e82e28bf642
author: rothja
ms.author: jroth
manager: craigg
---
# DDL Event Groups
  The following tables list the DDL event groups that can be used to run a DDL trigger or an event notification, and also the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements they cover. Note the inclusive nature of the event groups. For example, a DDL trigger or event notification that specifies FOR DDL_TABLE_EVENTS (10018) covers the CREATE TABLE, ALTER TABLE and DROP TABLE [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. A DDL trigger or event notification that specifies FOR DDL_TABLE_VIEW_EVENTS (10017) covers all [!INCLUDE[tsql](../../includes/tsql-md.md)] statements under the types DDL_TABLE_EVENTS, DDL_VIEW_EVENTS, DDL_INDEX_EVENTS, and DDL_STATISTICS_EVENTS.  
  
> [!NOTE]  
>  Certain system stored procedures that perform DDL-like operations can also fire DDL triggers or event notifications. Test your DDL triggers and event notifications to determine their responses to system stored procedures that are run. For example, the CREATE TYPE statement and **sp_addtype** stored procedure will both fire a DDL trigger or event notification that is created on a CREATE_TYPE event.  
  
## Events  
 The events listed under DDL_DATABASE_LEVEL_EVENTS execute at the server (instance) or database level. Events listed under DDL_SERVER_LEVEL_EVENTS execute only at the server level.  
  
||||  
|-|-|-|  
|parent_type|type|name|  
|NULL|296|ALTER_SERVER_CONFIGURATION|  
|NULL|10001|DDL_EVENTS|  
|10001|10016|&#124;    DDL_DATABASE_LEVEL_EVENTS|  
|10016|10027|&#124;    &#124;    DDL_ASSEMBLY_EVENTS|  
|10027|102|&#124;    &#124;    &#124;    ALTER_ASSEMBLY|  
|10027|101|&#124;    &#124;    &#124;    CREATE_ASSEMBLY|  
|10027|103|&#124;    &#124;    &#124;    DROP_ASSEMBLY|  
|10016|10029|&#124;    &#124;    DDL_DATABASE_SECURITY_EVENTS|  
|10029|10033|&#124;    &#124;    &#124;    DDL_APPLICATION_ROLE_EVENTS|  
|10033|138|&#124;    &#124;    &#124;    &#124;    ALTER_APPLICATION_ROLE|  
|10033|137|&#124;    &#124;    &#124;    &#124;    CREATE_APPLICATION_ROLE|  
|10033|139|&#124;    &#124;    &#124;    &#124;    DROP_APPLICATION_ROLE|  
|10029|10038|&#124;    &#124;    &#124;    DDL_ASYMMETRIC_KEY_EVENTS|  
|10038|248|&#124;    &#124;    &#124;    &#124;    ALTER_ASYMMETRIC_KEY|  
|10038|247|&#124;    &#124;    &#124;    &#124;    CREATE_ASYMMETRIC_KEY|  
|10038|249|&#124;    &#124;    &#124;    &#124;    DROP_ASYMMETRIC_KEY|  
|10029|10036|&#124;    &#124;    &#124;    DDL_AUTHORIZATION_DATABASE_EVENTS|  
|10036|205|&#124;    &#124;    &#124;    &#124;    ALTER_AUTHORIZATION_DATABASE|  
|10029|10030|&#124;    &#124;    &#124;    DDL_CERTIFICATE_EVENTS|  
|10030|198|&#124;    &#124;    &#124;    &#124;    ALTER_CERTIFICATE|  
|10030|197|&#124;    &#124;    &#124;    &#124;    CREATE_CERTIFICATE|  
|10030|199|&#124;    &#124;    &#124;    &#124;    DROP_CERTIFICATE|  
|10029|10039|&#124;    &#124;    &#124;    DDL_CRYPTO_SIGNATURE_EVENTS|  
|10039|257|&#124;    &#124;    &#124;    &#124;    ADD_SIGNATURE|  
|10039|255|&#124;    &#124;    &#124;    &#124;    ADD_SIGNATURE_SCHEMA_OBJECT|  
|10039|258|&#124;    &#124;    &#124;    &#124;    DROP_SIGNATURE|  
|10039|256|&#124;    &#124;    &#124;    &#124;    DROP_SIGNATURE_SCHEMA_OBJECT|  
|10029|10066|&#124;    &#124;    &#124;    DDL_DATABASE_AUDIT_SPECIFICATION_EVENTS|  
|10066|291|&#124;    &#124;    &#124;    &#124;    ALTER_DATABASE_AUDIT_SPECIFICATION|  
|10066|290|&#124;    &#124;    &#124;    &#124;    CREATE_DATABASE_AUDIT_SPECIFICATION|  
|10066|292|&#124;    &#124;    &#124;    &#124;    DROP_DATABASE_AUDIT_SPECIFICATION|  
|10029|10062|&#124;    &#124;    &#124;    DDL_DATABASE_ENCRYPTION_KEY_EVENTS|  
|10062|279|&#124;    &#124;    &#124;    &#124;    ALTER_DATABASE_ENCRYPTION_KEY|  
|10062|278|&#124;    &#124;    &#124;    &#124;    CREATE_DATABASE_ENCRYPTION_KEY|  
|10062|280|&#124;    &#124;    &#124;    &#124;    DROP_DATABASE_ENCRYPTION_KEY|  
|10029|10035|&#124;    &#124;    &#124;    DDL_GDR_DATABASE_EVENTS|  
|10035|171|&#124;    &#124;    &#124;    &#124;    DENY_DATABASE|  
|10035|170|&#124;    &#124;    &#124;    &#124;    GRANT_DATABASE|  
|10035|172|&#124;    &#124;    &#124;    &#124;    REVOKE_DATABASE|  
|10029|10040|&#124;    &#124;    &#124;    DDL_MASTER_KEY_EVENTS|  
|10040|253|&#124;    &#124;    &#124;    &#124;    ALTER_MASTER_KEY|  
|10040|252|&#124;    &#124;    &#124;    &#124;    CREATE_MASTER_KEY|  
|10040|254|&#124;    &#124;    &#124;    &#124;    DROP_MASTER_KEY|  
|10029|10032|&#124;    &#124;    &#124;    DDL_ROLE_EVENTS|  
|10032|207|&#124;    &#124;    &#124;    &#124;    ADD_ROLE_MEMBER|  
|10032|135|&#124;    &#124;    &#124;    &#124;    ALTER_ROLE|  
|10032|134|&#124;    &#124;    &#124;    &#124;    CREATE_ROLE|  
|10032|136|&#124;    &#124;    &#124;    &#124;    DROP_ROLE|  
|10032|208|&#124;    &#124;    &#124;    &#124;    DROP_ROLE_MEMBER|  
|10029|10034|&#124;    &#124;    &#124;    DDL_SCHEMA_EVENTS|  
|10034|142|&#124;    &#124;    &#124;    &#124;    ALTER_SCHEMA|  
|10034|141|&#124;    &#124;    &#124;    &#124;    CREATE_SCHEMA|  
|10034|143|&#124;    &#124;    &#124;    &#124;    DROP_SCHEMA|  
|10029|10037|&#124;    &#124;    &#124;    DDL_SYMMETRIC_KEY_EVENTS|  
|10037|245|&#124;    &#124;    &#124;    &#124;    ALTER_SYMMETRIC_KEY|  
|10037|244|&#124;    &#124;    &#124;    &#124;    CREATE_SYMMETRIC_KEY|  
|10037|246|&#124;    &#124;    &#124;    &#124;    DROP_SYMMETRIC_KEY|  
|10029|10031|&#124;    &#124;    &#124;    DDL_USER_EVENTS|  
|10031|132|&#124;    &#124;    &#124;    &#124;    ALTER_USER|  
|10031|131|&#124;    &#124;    &#124;    &#124;    CREATE_USER|  
|10031|133|&#124;    &#124;    &#124;    &#124;    DROP_USER|  
|10016|10052|&#124;    &#124;    DDL_DEFAULT_EVENTS|  
|10052|218|&#124;    &#124;    &#124;    BIND_DEFAULT|  
|10052|220|&#124;    &#124;    &#124;    CREATE_DEFAULT|  
|10052|231|&#124;    &#124;    &#124;    DROP_DEFAULT|  
|10052|242|&#124;    &#124;    &#124;    UNBIND_DEFAULT|  
|10016|10026|&#124;    &#124;    DDL_EVENT_NOTIFICATION_EVENTS|  
|10026|74|&#124;    &#124;    &#124;    CREATE_EVENT_NOTIFICATION|  
|10026|76|&#124;    &#124;    &#124;    DROP_EVENT_NOTIFICATION|  
|10016|10053|&#124;    &#124;    DDL_EXTENDED_PROPERTY_EVENTS|  
|10053|211|&#124;    &#124;    &#124;    ALTER_EXTENDED_PROPERTY|  
|10053|222|&#124;    &#124;    &#124;    CREATE_EXTENDED_PROPERTY|  
|10053|233|&#124;    &#124;    &#124;    DROP_EXTENDED_PROPERTY|  
|10016|10054|&#124;    &#124;    DDL_FULLTEXT_CATALOG_EVENTS|  
|10054|212|&#124;    &#124;    &#124;    ALTER_FULLTEXT_CATALOG|  
|10054|223|&#124;    &#124;    &#124;    CREATE_FULLTEXT_CATALOG|  
|10054|234|&#124;    &#124;    &#124;    DROP_FULLTEXT_CATALOG|  
|10016|10067|&#124;    &#124;    DDL_FULLTEXT_STOPLIST_EVENTS|  
|10067|294|&#124;    &#124;    &#124;    ALTER_FULLTEXT_STOPLIST|  
|10067|293|&#124;    &#124;    &#124;    CREATE_FULLTEXT_STOPLIST|  
|10067|295|&#124;    &#124;    &#124;    DROP_FULLTEXT_STOPLIST|  
|10016|10023|&#124;    &#124;    DDL_FUNCTION_EVENTS|  
|10023|62|&#124;    &#124;    &#124;    ALTER_FUNCTION|  
|10023|61|&#124;    &#124;    &#124;    CREATE_FUNCTION|  
|10023|63|&#124;    &#124;    &#124;    DROP_FUNCTION|  
|10016|10049|&#124;    &#124;    DDL_PARTITION_EVENTS|  
|10049|10050|&#124;    &#124;    &#124;    DDL_PARTITION_FUNCTION_EVENTS|  
|10050|192|&#124;    &#124;    &#124;    &#124;    ALTER_PARTITION_FUNCTION|  
|10050|191|&#124;    &#124;    &#124;    &#124;    CREATE_PARTITION_FUNCTION|  
|10050|193|&#124;    &#124;    &#124;    &#124;    DROP_PARTITION_FUNCTION|  
|10049|10051|&#124;    &#124;    &#124;    DDL_PARTITION_SCHEME_EVENTS|  
|10051|195|&#124;    &#124;    &#124;    &#124;    ALTER_PARTITION_SCHEME|  
|10051|194|&#124;    &#124;    &#124;    &#124;    CREATE_PARTITION_SCHEME|  
|10051|196|&#124;    &#124;    &#124;    &#124;    DROP_PARTITION_SCHEME|  
|10016|10055|&#124;    &#124;    DDL_PLAN_GUIDE_EVENTS|  
|10055|216|&#124;    &#124;    &#124;    ALTER_PLAN_GUIDE|  
|10055|228|&#124;    &#124;    &#124;    CREATE_PLAN_GUIDE|  
|10055|238|&#124;    &#124;    &#124;    DROP_PLAN_GUIDE|  
|10016|10024|&#124;    &#124;    DDL_PROCEDURE_EVENTS|  
|10024|52|&#124;    &#124;    &#124;    ALTER_PROCEDURE|  
|10024|51|&#124;    &#124;    &#124;    CREATE_PROCEDURE|  
|10024|53|&#124;    &#124;    &#124;    DROP_PROCEDURE|  
|10016|10056|&#124;    &#124;    DDL_RULE_EVENTS|  
|10056|219|&#124;    &#124;    &#124;    BIND_RULE|  
|10056|229|&#124;    &#124;    &#124;    CREATE_RULE|  
|10056|239|&#124;    &#124;    &#124;    DROP_RULE|  
|10056|243|&#124;    &#124;    &#124;    UNBIND_RULE|  
|10016|10069|&#124;    &#124;    DDL_SEARCH_PROPERTY_LIST_EVENTS|  
|10069|298|&#124;    &#124;    &#124;    ALTER_SEARCH_PROPERTY_LIST|  
|10069|297|&#124;    &#124;    &#124;    CREATE_SEARCH_PROPERTY_LIST|  
|10069|299|&#124;    &#124;    &#124;    DROP_SEARCH_PROPERTY_LIST|  
|10016|10070|&#124;    &#124;    DDL_SEQUENCE_EVENTS|  
|10070|304|&#124;    &#124;    &#124;    ALTER_SEQUENCE|  
|10070|303|&#124;    &#124;    &#124;    CREATE_SEQUENCE|  
|10070|305|&#124;    &#124;    &#124;    DROP_SEQUENCE|  
|10016|10041|&#124;    &#124;    DDL_SSB_EVENTS|  
|10041|10063|&#124;    &#124;    &#124;    DDL_BROKER_PRIORITY_EVENTS|  
|10063|282|&#124;    &#124;    &#124;    &#124;    ALTER_BROKER_PRIORITY|  
|10063|281|&#124;    &#124;    &#124;    &#124;    CREATE_BROKER_PRIORITY|  
|10063|283|&#124;    &#124;    &#124;    &#124;    DROP_BROKER_PRIORITY|  
|10041|10043|&#124;    &#124;    &#124;    DDL_CONTRACT_EVENTS|  
|10043|154|&#124;    &#124;    &#124;    &#124;    CREATE_CONTRACT|  
|10043|156|&#124;    &#124;    &#124;    &#124;    DROP_CONTRACT|  
|10041|10042|&#124;    &#124;    &#124;    DDL_MESSAGE_TYPE_EVENTS|  
|10042|152|&#124;    &#124;    &#124;    &#124;    ALTER_MESSAGE_TYPE|  
|10042|151|&#124;    &#124;    &#124;    &#124;    CREATE_MESSAGE_TYPE|  
|10042|153|&#124;    &#124;    &#124;    &#124;    DROP_MESSAGE_TYPE|  
|10041|10044|&#124;    &#124;    &#124;    DDL_QUEUE_EVENTS|  
|10044|158|&#124;    &#124;    &#124;    &#124;    ALTER_QUEUE|  
|10044|157|&#124;    &#124;    &#124;    &#124;    CREATE_QUEUE|  
|10044|159|&#124;    &#124;    &#124;    &#124;    DROP_QUEUE|  
|10041|10047|&#124;    &#124;    &#124;    DDL_REMOTE_SERVICE_BINDING_EVENTS|  
|10047|175|&#124;    &#124;    &#124;    &#124;    ALTER_REMOTE_SERVICE_BINDING|  
|10047|174|&#124;    &#124;    &#124;    &#124;    CREATE_REMOTE_SERVICE_BINDING|  
|10047|176|&#124;    &#124;    &#124;    &#124;    DROP_REMOTE_SERVICE_BINDING|  
|10041|10046|&#124;    &#124;    &#124;    DDL_ROUTE_EVENTS|  
|10046|165|&#124;    &#124;    &#124;    &#124;    ALTER_ROUTE|  
|10046|164|&#124;    &#124;    &#124;    &#124;    CREATE_ROUTE|  
|10046|166|&#124;    &#124;    &#124;    &#124;    DROP_ROUTE|  
|10041|10045|&#124;    &#124;    &#124;    DDL_SERVICE_EVENTS|  
|10045|162|&#124;    &#124;    &#124;    &#124;    ALTER_SERVICE|  
|10045|161|&#124;    &#124;    &#124;    &#124;    CREATE_SERVICE|  
|10045|163|&#124;    &#124;    &#124;    &#124;    DROP_SERVICE|  
|10016|10022|&#124;    &#124;    DDL_SYNONYM_EVENTS|  
|10022|34|&#124;    &#124;    &#124;    CREATE_SYNONYM|  
|10022|36|&#124;    &#124;    &#124;    DROP_SYNONYM|  
|10016|10017|&#124;    &#124;    DDL_TABLE_VIEW_EVENTS|  
|10017|10020|&#124;    &#124;    &#124;    DDL_INDEX_EVENTS|  
|10020|213|&#124;    &#124;    &#124;    &#124;    ALTER_FULLTEXT_INDEX|  
|10020|25|&#124;    &#124;    &#124;    &#124;    ALTER_INDEX|  
|10020|224|&#124;    &#124;    &#124;    &#124;    CREATE_FULLTEXT_INDEX|  
|10020|24|&#124;    &#124;    &#124;    &#124;    CREATE_INDEX|  
|10020|274|&#124;    &#124;    &#124;    &#124;    CREATE_SPATIAL_INDEX|  
|10020|206|&#124;    &#124;    &#124;    &#124;    CREATE_XML_INDEX|  
|10020|235|&#124;    &#124;    &#124;    &#124;    DROP_FULLTEXT_INDEX|  
|10020|26|&#124;    &#124;    &#124;    &#124;    DROP_INDEX|  
|10017|10021|&#124;    &#124;    &#124;    DDL_STATISTICS_EVENTS|  
|10021|27|&#124;    &#124;    &#124;    &#124;    CREATE_STATISTICS|  
|10021|29|&#124;    &#124;    &#124;    &#124;    DROP_STATISTICS|  
|10021|28|&#124;    &#124;    &#124;    &#124;    UPDATE_STATISTICS|  
|10017|10018|&#124;    &#124;    &#124;    DDL_TABLE_EVENTS|  
|10018|22|&#124;    &#124;    &#124;    &#124;    ALTER_TABLE|  
|10018|21|&#124;    &#124;    &#124;    &#124;    CREATE_TABLE|  
|10018|23|&#124;    &#124;    &#124;    &#124;    DROP_TABLE|  
|10017|10019|&#124;    &#124;    &#124;    DDL_VIEW_EVENTS|  
|10019|42|&#124;    &#124;    &#124;    &#124;    ALTER_VIEW|  
|10019|41|&#124;    &#124;    &#124;    &#124;    CREATE_VIEW|  
|10019|43|&#124;    &#124;    &#124;    &#124;    DROP_VIEW|  
|10016|10025|&#124;    &#124;    DDL_TRIGGER_EVENTS|  
|10025|72|&#124;    &#124;    &#124;    ALTER_TRIGGER|  
|10025|71|&#124;    &#124;    &#124;    CREATE_TRIGGER|  
|10025|73|&#124;    &#124;    &#124;    DROP_TRIGGER|  
|10016|10028|&#124;    &#124;    DDL_TYPE_EVENTS|  
|10028|91|&#124;    &#124;    &#124;    CREATE_TYPE|  
|10028|93|&#124;    &#124;    &#124;    DROP_TYPE|  
|10016|10048|&#124;    &#124;    DDL_XML_SCHEMA_COLLECTION_EVENTS|  
|10048|178|&#124;    &#124;    &#124;    ALTER_XML_SCHEMA_COLLECTION|  
|10048|177|&#124;    &#124;    &#124;    CREATE_XML_SCHEMA_COLLECTION|  
|10048|179|&#124;    &#124;    &#124;    DROP_XML_SCHEMA_COLLECTION|  
|10016|241|&#124;    &#124;    RENAME|  
|10001|10002|&#124;    DDL_SERVER_LEVEL_EVENTS|  
|10002|214|&#124;    &#124;    ALTER_INSTANCE|  
|10002|10071|&#124;    &#124;    DDL_AVAILABILITY_GROUP_EVENTS|  
|10071|307|&#124;    &#124;    &#124;    ALTER_AVAILABILITY_GROUP|  
|10071|306|&#124;    &#124;    &#124;    CREATE_AVAILABILITY_GROUP|  
|10071|308|&#124;    &#124;    &#124;    DROP_AVAILABILITY_GROUP|  
|10002|10004|&#124;    &#124;    DDL_DATABASE_EVENTS|  
|10004|202|&#124;    &#124;    &#124;    ALTER_DATABASE|  
|10004|201|&#124;    &#124;    &#124;    CREATE_DATABASE|  
|10004|203|&#124;    &#124;    &#124;    DROP_DATABASE|  
|10002|10003|&#124;    &#124;    DDL_ENDPOINT_EVENTS|  
|10003|182|&#124;    &#124;    &#124;    ALTER_ENDPOINT|  
|10003|181|&#124;    &#124;    &#124;    CREATE_ENDPOINT|  
|10003|183|&#124;    &#124;    &#124;    DROP_ENDPOINT|  
|10002|10057|&#124;    &#124;    DDL_EVENT_SESSION_EVENTS|  
|10057|265|&#124;    &#124;    &#124;    ALTER_EVENT_SESSION|  
|10057|264|&#124;    &#124;    &#124;    CREATE_EVENT_SESSION|  
|10057|266|&#124;    &#124;    &#124;    DROP_EVENT_SESSION|  
|10002|10011|&#124;    &#124;    DDL_EXTENDED_PROCEDURE_EVENTS|  
|10011|221|&#124;    &#124;    &#124;    CREATE_EXTENDED_PROCEDURE|  
|10011|232|&#124;    &#124;    &#124;    DROP_EXTENDED_PROCEDURE|  
|10002|10012|&#124;    &#124;    DDL_LINKED_SERVER_EVENTS|  
|10012|263|&#124;    &#124;    &#124;    ALTER_LINKED_SERVER|  
|10012|225|&#124;    &#124;    &#124;    CREATE_LINKED_SERVER|  
|10012|10013|&#124;    &#124;    &#124;    DDL_LINKED_SERVER_LOGIN_EVENTS|  
|10013|226|&#124;    &#124;    &#124;    &#124;    CREATE_LINKED_SERVER_LOGIN|  
|10013|236|&#124;    &#124;    &#124;    &#124;    DROP_LINKED_SERVER_LOGIN|  
|10012|262|&#124;    &#124;    &#124;    DROP_LINKED_SERVER|  
|10002|10014|&#124;    &#124;    DDL_MESSAGE_EVENTS|  
|10014|215|&#124;    &#124;    &#124;    ALTER_MESSAGE|  
|10014|227|&#124;    &#124;    &#124;    CREATE_MESSAGE|  
|10014|237|&#124;    &#124;    &#124;    DROP_MESSAGE|  
|10002|10015|&#124;    &#124;    DDL_REMOTE_SERVER_EVENTS|  
|10015|217|&#124;    &#124;    &#124;    ALTER_REMOTE_SERVER|  
|10015|230|&#124;    &#124;    &#124;    CREATE_REMOTE_SERVER|  
|10015|240|&#124;    &#124;    &#124;    DROP_REMOTE_SERVER|  
|10002|10058|&#124;    &#124;    DDL_RESOURCE_GOVERNOR_EVENTS|  
|10058|273|&#124;    &#124;    &#124;    ALTER_RESOURCE_GOVERNOR_CONFIG|  
|10058|10059|&#124;    &#124;    &#124;    DDL_RESOURCE_POOL|  
|10059|268|&#124;    &#124;    &#124;    &#124;    ALTER_RESOURCE_POOL|  
|10059|267|&#124;    &#124;    &#124;    &#124;    CREATE_RESOURCE_POOL|  
|10059|269|&#124;    &#124;    &#124;    &#124;    DROP_RESOURCE_POOL|  
|10058|10060|&#124;    &#124;    &#124;    DDL_WORKLOAD_GROUP|  
|10060|271|&#124;    &#124;    &#124;    &#124;    ALTER_WORKLOAD_GROUP|  
|10060|270|&#124;    &#124;    &#124;    &#124;    CREATE_WORKLOAD_GROUP|  
|10060|272|&#124;    &#124;    &#124;    &#124;    DROP_WORKLOAD_GROUP|  
|10002|10005|&#124;    &#124;    DDL_SERVER_SECURITY_EVENTS|  
|10005|209|&#124;    &#124;    &#124;    ADD_SERVER_ROLE_MEMBER|  
|10005|301|&#124;    &#124;    &#124;    ALTER_SERVER_ROLE|  
|10005|300|&#124;    &#124;    &#124;    CREATE_SERVER_ROLE|  
|10005|10008|&#124;    &#124;    &#124;    DDL_AUTHORIZATION_SERVER_EVENTS|  
|10008|204|&#124;    &#124;    &#124;    &#124;    ALTER_AUTHORIZATION_SERVER|  
|10005|10009|&#124;    &#124;    &#124;    DDL_CREDENTIAL_EVENTS|  
|10009|260|&#124;    &#124;    &#124;    &#124;    ALTER_CREDENTIAL|  
|10009|259|&#124;    &#124;    &#124;    &#124;    CREATE_CREDENTIAL|  
|10009|261|&#124;    &#124;    &#124;    &#124;    DROP_CREDENTIAL|  
|10005|10061|&#124;    &#124;    &#124;    DDL_CRYPTOGRAPHIC_PROVIDER_EVENTS|  
|10061|276|&#124;    &#124;    &#124;    &#124;    ALTER_CRYPTOGRAPHIC_PROVIDER|  
|10061|275|&#124;    &#124;    &#124;    &#124;    CREATE_CRYPTOGRAPHIC_PROVIDER|  
|10061|277|&#124;    &#124;    &#124;    &#124;    DROP_CRYPTOGRAPHIC_PROVIDER|  
|10005|10007|&#124;    &#124;    &#124;    DDL_GDR_SERVER_EVENTS|  
|10007|168|&#124;    &#124;    &#124;    &#124;    DENY_SERVER|  
|10007|167|&#124;    &#124;    &#124;    &#124;    GRANT_SERVER|  
|10007|169|&#124;    &#124;    &#124;    &#124;    REVOKE_SERVER|  
|10005|10006|&#124;    &#124;    &#124;    DDL_LOGIN_EVENTS|  
|10006|145|&#124;    &#124;    &#124;    &#124;    ALTER_LOGIN|  
|10006|144|&#124;    &#124;    &#124;    &#124;    CREATE_LOGIN|  
|10006|146|&#124;    &#124;    &#124;    &#124;    DROP_LOGIN|  
|10005|10064|&#124;    &#124;    &#124;    DDL_SERVER_AUDIT_EVENTS|  
|10064|285|&#124;    &#124;    &#124;    &#124;    ALTER_SERVER_AUDIT|  
|10064|284|&#124;    &#124;    &#124;    &#124;    CREATE_SERVER_AUDIT|  
|10064|286|&#124;    &#124;    &#124;    &#124;    DROP_SERVER_AUDIT|  
|10005|10065|&#124;    &#124;    &#124;    DDL_SERVER_AUDIT_SPECIFICATION_EVENTS|  
|10065|288|&#124;    &#124;    &#124;    &#124;    ALTER_SERVER_AUDIT_SPECIFICATION|  
|10065|287|&#124;    &#124;    &#124;    &#124;    CREATE_SERVER_AUDIT_SPECIFICATION|  
|10065|289|&#124;    &#124;    &#124;    &#124;    DROP_SERVER_AUDIT_SPECIFICATION|  
|10005|10010|&#124;    &#124;    &#124;    DDL_SERVICE_MASTER_KEY_EVENTS|  
|10010|251|&#124;    &#124;    &#124;    &#124;    ALTER_SERVICE_MASTER_KEY|  
|10005|302|&#124;    &#124;    &#124;    DROP_SERVER_ROLE|  
|10005|210|&#124;    &#124;    &#124;    DROP_SERVER_ROLE_MEMBER|  
  
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
 [Event Notifications](../service-broker/event-notifications.md)   
 [DDL Triggers](ddl-triggers.md)   
 [DDL Events](ddl-events.md)  
  
  
