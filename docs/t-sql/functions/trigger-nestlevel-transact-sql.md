---
title: "TRIGGER_NESTLEVEL (Transact-SQL)"
description: "TRIGGER_NESTLEVEL (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "TRIGGER_NESTLEVEL"
  - "TRIGGER_NESTLEVEL_TSQL"
helpviewer_keywords:
  - "triggers [SQL Server], number executed"
  - "number of triggers"
  - "TRIGGER_NESTLEVEL function"
dev_langs:
  - "TSQL"
---
# TRIGGER_NESTLEVEL (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the number of triggers executed for the statement that fired the trigger. TRIGGER_NESTLEVEL is used in DML and DDL triggers to determine the current level of nesting.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
TRIGGER_NESTLEVEL ( [ object_id ] , [ 'trigger_type' ] , [ 'trigger_event_category' ] )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *object_id*  
 Is the object ID of a trigger. If *object_id* is specified, the number of times the specified trigger has been executed for the statement is returned. If *object_id* is not specified, the number of times all triggers have been executed for the statement is returned.  
  
 **'** *trigger_type* **'**  
 Specifies whether to apply TRIGGER_NESTLEVEL to AFTER triggers or INSTEAD OF triggers. Specify **AFTER** for AFTER triggers. Specify **IOT** for INSTEAD OF triggers. If *trigger_type* is specified, *trigger_event_category* must also be specified.  
  
 **'** *trigger_event_category* **'**  
 Specifies whether to apply TRIGGER_NESTLEVEL to DML or DDL triggers. Specify **DML** for DML triggers. Specify **DDL** for DDL triggers. If *trigger_event_category* is specified, *trigger_type* must also be specified. Note that only **AFTER** can be specified with **DDL**, because DDL triggers can only be AFTER triggers.  
  
## Remarks  
 When no parameters are specified, TRIGGER_NESTLEVEL returns the total number of triggers on the call stack. This includes itself. Omission of parameters can occur when a trigger executes commands causing another trigger to be fired or creates a succession of firing triggers.  
  
 To return the total number of triggers on the call stack for a particular trigger type and event category, specify *object_id* = 0.  
  
 TRIGGER_NESTLEVEL returns 0 if it is executed outside a trigger and any parameters are not NULL.  
  
 When any parameters are explicitly specified as NULL, a value of NULL is returned regardless of whether TRIGGER_NESTLEVEL was used within or external to a trigger.  
  
## Examples  
  
### A. Testing the nesting level of a specific DML trigger  
  
```sql
IF ( (SELECT TRIGGER_NESTLEVEL( OBJECT_ID('xyz') , 'AFTER' , 'DML' ) ) > 5 )  
   RAISERROR('Trigger xyz nested more than 5 levels.',16,-1)  
```  
  
### B. Testing the nesting level of a specific DDL trigger  
  
```sql
IF ( ( SELECT TRIGGER_NESTLEVEL ( ( SELECT object_id FROM sys.triggers  
WHERE name = 'abc' ), 'AFTER' , 'DDL' ) ) > 5 )  
   RAISERROR ('Trigger abc nested more than 5 levels.',16,-1)  
```  
  
### C. Testing the nesting level of all triggers executed  
  
```sql
IF ( (SELECT trigger_nestlevel() ) > 5 )  
   RAISERROR  
      ('This statement nested over 5 levels of triggers.',16,-1)  
```  
  
## See Also  
 [CREATE TRIGGER &#40;Transact-SQL&#41;](../../t-sql/statements/create-trigger-transact-sql.md)  
  
  
