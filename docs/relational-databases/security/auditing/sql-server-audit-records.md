---
title: "SQL Server Audit Records"
description: SQL Server audits consist of audit action items, which are recorded to an audit target. Check this summary for the records that can be sent to a target.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: vanto
ms.date: 10/20/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
helpviewer_keywords:
  - "audit records [SQL Server]"
ai-usage: ai-assisted
---
# SQL Server Audit Records

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Audit feature enables you to audit server-level and database-level groups of events and events. For more information, see [SQL Server Audit (Database Engine)](../../../relational-databases/security/auditing/sql-server-audit-database-engine.md). [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

Audits consist of zero or more audit action items, which are recorded to an audit *target*. The audit target can be a binary file, the Windows Application event log, or the Windows Security event log. The records sent to the target can contain the elements described in the following table:

| Column name | Description | Type | Always available |
| --- | --- | --- | --- |
| **event_time** | Date and time when the auditable action is fired. | **datetime2** | Yes |
| **sequence_number** | Tracks the sequence of records within a single audit record that was too large to fit in the write buffer for audits. | **int** | Yes |
| **action_id** | ID of the action<br /><br />Tip: To use **action_id** as a predicate it must be converted from a character string to a numeric value. For more information, see [Filter SQL Server Audit on action_id / class_type predicate](/archive/blogs/sqlsecurity/filter-sql-server-audit-on-action_id-class_type-predicate). | **varchar(4)** | Yes |
| **succeeded** | Indicates whether the action that triggered the event succeeded. For all events other than login events, this only reports whether the permission check succeeded or failed, not the operation.<br />- 1 = Success<br />- 0 = Fail | **bit** | Yes |
| **permission_bitmask** | In some actions, this is the permissions that were granted, denied, or revoked. | **varbinary(16)** | No |
| **is_column_permission** | Flag indicating if this is a column level permission. Returns 0 when the permission_bitmask = 0.<br />- 1 = True<br />- 0 = False | **bit** | No |
| **session_id** | ID of the session on which the event occurred. | **smallint** | Yes |
| **server_principal_id** | ID of the login context that the action is performed in. | **int** | Yes |
| **database_principal_id** | ID of the database user context that the action is performed in. Returns 0 if this doesn't apply. For example, a server operation. | **int** | No |
| **target_server_principal_id** | Server principal that the GRANT/DENY/REVOKE operation is performed on. Returns 0 if not applicable. | **int** | Yes |
| **target_database_principal_id** | The database principal the GRANT/DENY/REVOKE operation is performed on. Returns 0 if not applicable. | **int** | No |
| **object_id** | The ID of the entity on which the audit occurred. This includes the:<br />server objects<br />databases<br />database objects<br />schema objects<br />Returns 0 if the entity is the Server itself or if the audit isn't performed at an object level. For example, Authentication. | **int** | No |
| **class_type** | The type of auditable entity that the audit occurs on. | **varchar(2)** | Yes |
| **session_server_principal_name** | Server principal for the session. | **sysname** | Yes |
| **server_principal_name** | Current login. | **sysname** | Yes |
| **server_principal_sid** | Current login SID. | **varbinary(85)** | Yes |
| **database_principal_name** | Current user. | **sysname** | No |
| **target_server_principal_name** | Target login of the action. Returns NULL if not applicable. | **sysname** | No |
| **target_server_principal_sid** | SID of the target login. Returns NULL if not applicable. | **varbinary(85)** | No |
| **target_database_principal_name** | Target user of the action. Returns NULL if not applicable. | **sysname** | No |
| **server_instance_name** | Name of the server instance where the audit occurred. The standard server\instance format is used. | **sysname** | Yes |
| **database_name** | The database context in which the action occurred. Nullable. Returns NULL for audits occurring at the server level. | **sysname** | No |
| **schema_name** | The schema context in which the action occurred. | **sysname** | No |
| **object_name** | The name of the entity on which the audit occurred. This includes the:<br />server objects<br />databases<br />database objects<br />schema objects<br />Nullable. Returns NULL if the entity is the Server itself or if the audit isn't performed at an object level. For example, Authentication. | **sysname** | No |
| **statement** | T-SQL statement if it exists. Returns NULL if not applicable. | **nvarchar(4000)** | No |
| **additional_information** | Unique information that only applies to a single event is returned as XML. A few auditable actions contain this kind of information.<br /><br />One level of TSQL stack will be displayed in XML format for actions that have TSQL stack associated with them. The XML format will be:<br />`<tsql_stack><frame nest_level = '%u' database_name = '%.*s' schema_name = '%.*s' object_name = '%.*s' /></tsql_stack>`<br />Frame nest_level indicates the current nesting level of the frame. Module name is represented in three part format (database_name, schema_name, and object_name). The module name will be parsed to escape invalid xml characters like `'\<'`, `'>'`, `'/'`, `'_x'`. They'll be escaped as `_xHHHH\_`. The HHHH stands for the four-digit hexadecimal UCS-2 code for the character<br />Nullable. Returns NULL when there's no additional information reported by the event. | **nvarchar(4000)** | No |
| **file_name** | The path and name of the audit log file that the record came from. | **varchar(260)** | Yes |
| **audit_file_offset** | **Applies to**: SQL Server only<br /><br />The buffer offset in the file that contains the audit record. | **bigint** | No |
| **user_defined_event_id** | **Applies to**: [!INCLUDE [ssSQL11](../../../includes/sssql11-md.md)] and later, Azure SQL Database, and SQL Managed Instance<br /><br />User defined event ID passed as an argument to `sp_audit_write` **NULL** for system events (default) and nonzero for user-defined event. For more information, see [sp_audit_write (Transact-SQL)](../../../relational-databases/system-stored-procedures/sp-audit-write-transact-sql.md). | **smallint** | No |
| **user_defined_information** | **Applies to**: [!INCLUDE [ssSQL11](../../../includes/sssql11-md.md)] and later, Azure SQL Database, and SQL Managed Instance<br /><br />Used to record any extra information the user wants to record in audit log by using the `sp_audit_write` stored procedure. | **nvarchar(4000)** | No |
| **audit_schema_version** | Always 1 | **int** | Yes |
| **sequence_group_id** | **Applies to**: SQL Server only<br /><br />Unique identifier | **varbinary(85)** | No |
| **transaction_id** | **Applies to**: SQL Server only (Starting with 2016)<br /><br />Unique identifier to identify multiple audit events in one transaction | **bigint** | No |
| **client_ip** | **Applies to**: Azure SQL Database + SQL Server (Starting with 2017)<br /><br />Source IP of the client application | **nvarchar(128)** | No |
| **application_name** | **Applies to**: Azure SQL Database + SQL Server (Starting with 2017)<br /><br />Name of the client application that executed the statement that caused the audit event | **nvarchar(128)** | No |
| **duration_milliseconds** | **Applies to**: Azure SQL Database and SQL Managed Instance<br /><br />Query execution duration in milliseconds | **bigint** | No |
| **response_rows** | **Applies to**: Azure SQL Database and SQL Managed Instance<br /><br />Number of rows returned in the result set. | **bigint** | No |
| **affected_rows** | **Applies to**: Azure SQL Database only<br /><br />Number of rows affected by the statement executed. | **bigint** | No |
| **connection_id** | **Applies to**: Azure SQL Database and SQL Managed Instance<br /><br />ID of the connection in the server | **GUID** | No |
| **data_sensitivity_information** | **Applies to**: Azure SQL Database only<br /><br />Information types and sensitivity labels returned by the audited query, based on the classified columns in the database. Learn more about [Azure SQL Database data discover and classification](/azure/azure-sql/database/data-discovery-and-classification-overview) | **nvarchar(4000)** | No |
| **host_name** | Host name of the client connection | **nvarchar(128)** | No |
| **session_context** | Session context information for the connection | **nvarchar(4000)** | No |
| **client_tls_version** | **Applies to**: SQL Server 2022 and later<br /><br />TLS version number used by the client connection | **int** | No |
| **client_tls_version_name** | **Applies to**: SQL Server 2022 and later<br /><br />TLS version name used by the client connection | **nvarchar(128)** | No |
| **database_transaction_id** | Database transaction identifier | **bigint** | No |
| **ledger_start_sequence_number** | **Applies to**: SQL Server 2022 and later<br /><br />Ledger start sequence number for ledger operations | **bigint** | No |
| **external_policy_permissions_checked** | External policy permissions that were checked during the operation | **nvarchar(4000)** | No |

## Remarks

Some actions don't populate a column's value because it might be nonapplicable to the action.

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Audit stores 4,000 characters of data for character fields in an audit record. When the **additional_information** and **statement** values returned from an auditable action return more than 4000 characters, the **sequence_number** column is used to write multiple records into the audit report for a single audit action to record this data. The process is as follows:

- The **statement** column is divided into 4,000 characters.

- [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Audit writes as the first row for the audit record with the partial data. All the other fields are duplicated in each row.

- The **sequence_number** value is incremented.

- This process is repeated until all the data is recorded.

You can connect the data by reading the rows sequentially using the **sequence_number** value, and the **event_Time**, **action_id** and **session_id** columns to identify the action.

## Related content

- [CREATE SERVER AUDIT (Transact-SQL)](../../../t-sql/statements/create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT (Transact-SQL)](../../../t-sql/statements/alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT (Transact-SQL)](../../../t-sql/statements/drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../../t-sql/statements/drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../../t-sql/statements/alter-authorization-transact-sql.md)
- [sys.fn_get_audit_file (Transact-SQL)](../../system-functions/sys-fn-get-audit-file-transact-sql.md)
- [sys.server_audits (Transact-SQL)](../../system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../system-catalog-views/sys-server-audit-specifications-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](../../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)
