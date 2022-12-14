---
title: "SET DEADLOCK_PRIORITY (Transact-SQL)"
description: SET DEADLOCK_PRIORITY (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/15/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SET DEADLOCK_PRIORITY"
  - "DEADLOCK_PRIORITY_TSQL"
  - "SET_DEADLOCK_PRIORITY_TSQL"
  - "DEADLOCK_PRIORITY"
helpviewer_keywords:
  - "deadlocks [SQL Server], priority settings"
  - "DEADLOCK_PRIORITY option"
  - "locking [SQL Server], deadlocks"
  - "priority deadlock settings [SQL Server]"
  - "SET DEADLOCK_PRIORITY statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SET DEADLOCK_PRIORITY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-svrless-poolonly](../../includes/applies-to-version/sql-asdb-asdbmi-asa-svrless-poolonly.md)]

  Specifies the relative importance that the current session continue processing if it is deadlocked with another session.

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SET DEADLOCK_PRIORITY { LOW | NORMAL | HIGH | <numeric-priority> | @deadlock_var | @deadlock_intvar }
  
<numeric-priority> ::= { -10 | -9 | -8 | ... | 0 | ... | 8 | 9 | 10 }
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### LOW  
 Specifies that the current session will be the deadlock victim if it is involved in a deadlock and other sessions involved in the deadlock chain have deadlock priority set to either NORMAL or HIGH or to an integer value greater than -5. The current session will not be the deadlock victim if the other sessions have deadlock priority set to an integer value less than -5. It also specifies that the current session is eligible to be the deadlock victim if another session has set deadlock priority set to LOW or to an integer value equal to -5.

#### NORMAL  
 Specifies that the current session will be the deadlock victim if other sessions involved in the deadlock chain have deadlock priority set to HIGH or to an integer value greater than 0, but will not be the deadlock victim if the other sessions have deadlock priority set to LOW or to an integer value less than 0. It also specifies that the current session is eligible to be the deadlock victim if another other session has set deadlock priority to NORMAL or to an integer value equal to 0. NORMAL is the default priority.

#### HIGH  
 Specifies that the current session will be the deadlock victim if other sessions involved in the deadlock chain have deadlock priority set to an integer value greater than 5, or is eligible to be the deadlock victim if another session has also set deadlock priority to HIGH or to an integer value equal to 5.

#### \<numeric-priority>  
 Is an integer value range (-10 to 10) to provide 21 levels of deadlock priority. It specifies that the current session will be the deadlock victim if other sessions in the deadlock chain are running at a higher deadlock priority value, but will not be the deadlock victim if the other sessions are running at a deadlock priority value lower than the value of the current session. It also specifies that the current session is eligible to be the deadlock victim if another session is running with a deadlock priority value that is the same as the current session. LOW maps to -5, NORMAL to 0, and HIGH to 5.

#### @*deadlock_var*  
 Is a character variable specifying the deadlock priority. The variable must be set to a value of 'LOW', 'NORMAL' or 'HIGH'. The variable must be large enough to hold the entire string.

#### @*deadlock_intvar*  
 Is an integer variable specifying the deadlock priority. The variable must be set to an integer value in the range (-10 to 10).

## Remarks

 Deadlocks arise when two sessions are both waiting for access to resources locked by the other. When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detects that two sessions are deadlocked, it resolves the deadlock by choosing one of the sessions as a deadlock victim. The current transaction of the victim is rolled back and deadlock error message 1205 is returned to the client. This releases all of the locks held by that session, allowing the other session to proceed.

 Which session is chosen as the deadlock victim depends on each session's deadlock priority:

- If both sessions have the same deadlock priority, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] chooses the session that is less expensive to roll back as the deadlock victim. For example, if both sessions have set their deadlock priority to HIGH, the instance will choose as a victim the session it estimates is less costly to roll back. The cost is determined by comparing the number of log bytes written to that point in each transaction. (You can see this value as "Log Used" in a deadlock graph).

- If the sessions have different deadlock priorities, the session with the lowest deadlock priority is chosen as the deadlock victim.

 SET DEADLOCK_PRIORITY is set at execute or run time and not at parse time.

 This syntax is not supported in dedicated SQL pools in Azure Synapse Analytics. It is supported in serverless SQL pools in Azure Synapse Analytics.

## Permissions

 Requires membership in the **public** role.

## Examples

 The following example uses a variable to set the deadlock priority to `LOW`.

```sql
DECLARE @deadlock_var NCHAR(3);
SET @deadlock_var = N'LOW';
  
SET DEADLOCK_PRIORITY @deadlock_var;
GO
```

 The following example sets the deadlock priority to `NORMAL`.

```sql
SET DEADLOCK_PRIORITY NORMAL;
GO
```

## Next steps

- [@@LOCK_TIMEOUT (Transact-SQL)](../../t-sql/functions/lock-timeout-transact-sql.md)
- [SET Statements (Transact-SQL)](../../t-sql/statements/set-statements-transact-sql.md)
- [SET LOCK_TIMEOUT (Transact-SQL)](../../t-sql/statements/set-lock-timeout-transact-sql.md)
