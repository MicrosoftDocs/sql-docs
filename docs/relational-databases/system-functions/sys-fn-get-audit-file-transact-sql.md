---
title: "sys.fn_get_audit_file (Transact-SQL)"
description: "The sys.fn_get_audit_file system function returns information from an audit file created by a server audit in SQL Server."
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: wiassaf, randolphwest
ms.date: 1/9/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_get_audit_file_TSQL"
  - "sys.fn_get_audit_file_TSQL"
  - "fn_get_audit_file"
  - "sys.fn_get_audit_file"
helpviewer_keywords:
  - "sys.fn_get_audit_file function"
  - "fn_get_audit_file function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest"
---
# sys.fn_get_audit_file (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Returns information from an audit file created by a server audit in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
fn_get_audit_file ( file_pattern ,
    { default | initial_file_name | NULL } ,
    { default | audit_record_offset | NULL } )
```

## Arguments

#### file_pattern

Specifies the directory or path and file name for the audit file set to be read. Type is **nvarchar(260)**.

Passing a path without a file name pattern will generate an error.

- **SQL Server**:

  This argument must include both a path (drive letter or network share) and a file name that can include a wildcard. A single asterisk (*) can be used to collect multiple files from an audit file set. For example:

  - `\<path>\*` - Collect all audit files in the specified location.

  - `<path>\LoginsAudit_{GUID}*` - Collect all audit files that have the specified name and GUID pair.

  - `<path>\LoginsAudit_{GUID}_00_29384.sqlaudit` - Collect a specific audit file.

- **Azure SQL Database**:

  This argument is used to specify a blob URL (including the storage endpoint and container). While it doesn't support an asterisk wildcard, you can use a partial file (blob) name prefix (instead of the full blob name) to collect multiple files (blobs) that begin with this prefix. For example:

  - `<Storage_endpoint>/<Container>/<ServerName>/<DatabaseName>/` - collects all audit files (blobs) for the specific database.

  - `<Storage_endpoint>/<Container>/<ServerName>/<DatabaseName>/<AuditName>/<CreationDate>/<FileName>.xel` - collects a specific audit file (blob).

#### initial_file_name

Specifies the path and name of a specific file in the audit file set to start reading audit records from. Type is **nvarchar(260)**.

The *initial_file_name* argument must contain valid entries or must contain either the default or NULL value.

#### audit_record_offset

Specifies a known location with the file specified for the initial_file_name. When this argument is used the function will start reading at the first record of the Buffer immediately following the specified offset.

The *audit_record_offset* argument must contain valid entries or must contain either the default or NULL value. Type is **bigint**.

## Tables returned

The following table describes the audit file content that can be returned by this function.

| Column name | Type | Description |
| --- | --- | --- |
| action_id | **varchar(4)** | ID of the action. Not nullable. |
| additional_information | **nvarchar(4000)** | Unique information that only applies to a single event is returned as XML. A few auditable actions contain this kind of information.<br /><br />One level of TSQL stack will be displayed in XML format for actions that have TSQL stack associated with them. The XML format will be:<br /><br />`<tsql_stack><frame nest_level = '%u' database_name = '%.*s' schema_name = '%.*s' object_name = '%.*s' /></tsql_stack>`<br /><br />Frame nest_level indicates the current nesting level of the frame. The Module name is represented in three part format (database_name, schema_name and object_name). The module name will be parsed to escape invalid xml characters like `'\<'`, `'>'`, `'/'`, `'_x'`. They will be escaped as `_xHHHH\_`. The HHHH stands for the four-digit hexadecimal UCS-2 code for the character<br /><br />Is nullable. Returns NULL when there is no additional information reported by the event. |
| affected_rows | **bigint** | **Applies to**: Azure SQL Database only<br /><br />Number of rows affected by the executed statement. |
| application_name | **nvarchar(128)** | **Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and Azure SQL Database<br /><br />Name of client application that executed the statement that caused the audit event |
| audit_file_offset | **bigint** | **Applies to**: SQL Server only<br /><br />The buffer offset in the file that contains the audit record. Not nullable. |
| audit_schema_version | **int** | Always 1 |
| class_type | **varchar(2)** | The type of auditable entity that the audit occurs on. Not nullable. |
| client_ip | **nvarchar(128)** | **Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and Azure SQL Database<br /><br />	Source IP of the client application |
| connection_id | GUID | **Applies to**: Azure SQL Database and SQL Managed Instance<br /><br />ID of the connection in the server |
| data_sensitivity_information | nvarchar(4000) | **Applies to**: Azure SQL Database only<br /><br />Information types and sensitivity labels returned by the audited query, based on the classified columns in the database. Learn more about [Azure SQL Database data discover and classification](/azure/sql-database/sql-database-data-discovery-and-classification) |
| database_name | **sysname** | The database context in which the action occurred. Is nullable. Returns NULL for audits occurring at the server level. |
| database_principal_id | **int** | ID of the database user context that the action is performed in. Not nullable. Returns 0 if this doesn't apply. For example, a server operation. |
| database_principal_name | **sysname** | Current user. Is nullable. Returns NULL if not available. |
| duration_milliseconds | **bigint** | **Applies to**: Azure SQL Database and SQL Managed Instance<br /><br />Query execution duration in milliseconds |
| event_time | **datetime2** | Date and time when the auditable action is fired. Not nullable. |
| file_name | **varchar(260)** | The path and name of the audit log file that the record came from. Not nullable. |
| is_column_permission | **bit** | Flag indicating if this is a column level permission. Not nullable. Returns 0 when the permission_bitmask = 0.<br />1 = true<br />0 = false |
| object_id | **int** | The ID of the entity on which the audit occurred. This includes the following:<br />Server objects<br />Databases<br />Database objects<br />Schema objects<br />Not nullable. Returns 0 if the entity is the Server itself or if the audit isn't performed at an object level. For example, Authentication. |
| object_name | **sysname** | The name of the entity on which the audit occurred. This includes the following:<br />Server objects<br />Databases<br />Database objects<br />Schema objects<br />Is nullable. Returns NULL if the entity is the Server itself or if the audit isn't performed at an object level. For example, Authentication. |
| permission_bitmask | **varbinary(16)** | In some actions, this is the permissions that were grant, denied, or revoked. |
| response_rows | **bigint** | **Applies to**: Azure SQL Database and SQL Managed Instance<br /><br />Number of rows returned in the result set. |
| schema_name | **sysname** | The schema context in which the action occurred. Is nullable. Returns NULL for audits occurring outside a schema. |
| sequence_group_id | **varbinary** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions<br /><br />	Unique identifier |
| sequence_number | **int** | Tracks the sequence of records within a single audit record that was too large to fit in the write buffer for audits. Not nullable. |
| server_instance_name | **sysname** | Name of the server instance where the audit occurred. The standard server\instance format is used. |
| server_principal_id | **int** | ID of the login context that the action is performed in. Not nullable. |
| server_principal_name | **sysname** | Current login. Is nullable. |
| server_principal_sid | **varbinary** | Current login SID. Is nullable. |
| session_id | **smallint** | ID of the session on which the event occurred. Not nullable. |
| session_server_principal_name | **sysname** | Server principal for session. Is nullable. Returns the identity of the original login that was connected to the instance of SQL Server in case there were explicit or implicit context switches. |
| statement | **nvarchar(4000)** | TSQL statement if it exists. Is nullable. Returns NULL if not applicable. |
| succeeded | **bit** | Indicates whether the action that triggered the event succeeded. Not nullable. For all events other than login events, this only reports whether the permission check succeeded or failed, not the operation.<br />1 = success<br />0 = fail |
| target_database_principal_id | **int** | The database principal the GRANT/DENY/REVOKE operation is performed on. Not nullable. Returns 0 if not applicable. |
| target_database_principal_name | **sysname** | Target user of action. Is nullable. Returns NULL if not applicable. |
| target_server_principal_id | **int** | Server principal that the GRANT/DENY/REVOKE operation is performed on. Not nullable. Returns 0 if not applicable. |
| target_server_principal_name | **sysname** | Target login of action. Is nullable. Returns NULL if not applicable. |
| target_server_principal_sid | **varbinary** | SID of target login. Is nullable. Returns NULL if not applicable. |
| transaction_id | **bigint** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions<br /><br />Unique identifier to identify multiple audit events in one transaction |
| user_defined_event_id | **smallint** | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later, Azure SQL Database and SQL Managed Instance<br /><br />User defined event ID passed as an argument to `sp_audit_write`. **NULL** for system events (default) and non-zero for user-defined event. For more information, see [sp_audit_write (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-audit-write-transact-sql.md). |
| user_defined_information | **nvarchar(4000)** | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, Azure SQL Database, and SQL Managed Instance<br /><br />Used to record any extra information the user wants to record in audit log by using the `sp_audit_write` stored procedure. |

## Remarks

- If the *file_pattern* argument passed to `fn_get_audit_file` references a path or file that doesn't exist, or if the file isn't an audit file, the **MSG_INVALID_AUDIT_FILE** error message is returned.
- `fn_get_audit_file` can't be used when the audit is created with the **APPLICATION_LOG**, **SECURITY_LOG**, or **EXTERNAL_MONITOR** options.

## Permissions

- **SQL Server**: Requires the **CONTROL SERVER** permission.
- **Azure SQL Database**: Requires the **CONTROL DATABASE** permission.
  - Server admins can access audit logs of all databases on the server.
  - Non server admins can only access audit logs from the current database.
  - Blobs that don't meet the above criteria will be skipped (a list of skipped blobs will be displayed in the query output message), and the function will return logs only from blobs for which access is allowed.

## Examples

### SQL Server

This example reads from a file that is named `\\serverName\Audit\HIPAA_AUDIT.sqlaudit`.

```sql
SELECT *
FROM sys.fn_get_audit_file('\\serverName\Audit\HIPAA_AUDIT.sqlaudit', DEFAULT, DEFAULT);
GO
```

### Azure SQL Database

This example reads from a file that is named `ShiraServer/MayaDB/SqlDbAuditing_Audit/2017-07-14/10_45_22_173_1.xel`:

```sql
SELECT *
FROM sys.fn_get_audit_file('https://mystorage.blob.core.windows.net/sqldbauditlogs/ShiraServer/MayaDB/SqlDbAuditing_Audit/2017-07-14/10_45_22_173_1.xel', DEFAULT, DEFAULT);
GO
```

This example reads from the same file as above, but with additional T-SQL clauses (TOP, ORDER BY, and WHERE clause for filtering the audit records returned by the function):

```sql
SELECT TOP 10 *
FROM sys.fn_get_audit_file('https://mystorage.blob.core.windows.net/sqldbauditlogs/ShiraServer/MayaDB/SqlDbAuditing_Audit/2017-07-14/10_45_22_173_1.xel', DEFAULT, DEFAULT)
WHERE server_principal_name = 'admin1'
ORDER BY event_time;
GO
```

For a full example about how to create an audit, see [SQL Server Audit (Database Engine)](../../relational-databases/security/auditing/sql-server-audit-database-engine.md).

For information on setting up Azure SQL Database auditing, see [Get Started with SQL Database auditing](/azure/sql-database/sql-database-auditing).

## Limitations

Selecting rows from `sys.fn_get_audit_file` within a Create Table As Select (CTAS) or INSERT INTO is a limitation when running on Azure Synapse Analytics. Although the query completes successfully and no error messages appear, there are no rows present in the table created using CTAS or INSERT INTO.

## See also

- [CREATE SERVER AUDIT (Transact-SQL)](../../t-sql/statements/create-server-audit-transact-sql.md)
- [ALTER SERVER AUDIT (Transact-SQL)](../../t-sql/statements/alter-server-audit-transact-sql.md)
- [DROP SERVER AUDIT (Transact-SQL)](../../t-sql/statements/drop-server-audit-transact-sql.md)
- [CREATE SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-server-audit-specification-transact-sql.md)
- [ALTER SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/alter-server-audit-specification-transact-sql.md)
- [DROP SERVER AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/drop-server-audit-specification-transact-sql.md)
- [CREATE DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/create-database-audit-specification-transact-sql.md)
- [ALTER DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/alter-database-audit-specification-transact-sql.md)
- [DROP DATABASE AUDIT SPECIFICATION (Transact-SQL)](../../t-sql/statements/drop-database-audit-specification-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)
- [sys.server_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
- [sys.database_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
- [sys.database_audit_specification_details (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
- [sys.dm_server_audit_status (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-server-audit-status-transact-sql.md)
- [sys.dm_audit_actions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-audit-actions-transact-sql.md)
- [sys.dm_audit_class_type_map (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-audit-class-type-map-transact-sql.md)
- [Create a Server Audit and Server Audit Specification](../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md)

## Next steps

- [sys.server_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
- [sys.server_file_audits (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
- [sys.server_audit_specifications (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
