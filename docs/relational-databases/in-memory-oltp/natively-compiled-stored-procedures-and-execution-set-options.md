---
title: "Natively Compiled Stored Procedures and Execution Set Options | Microsoft Docs"
ms.custom: ""
ms.date: "10/26/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: c1869cf7-9030-4d18-85d6-0e419a4e9af7
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Natively Compiled Stored Procedures and Execution Set Options
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

Session options are fixed in atomic blocks, as described in [Atomic Blocks](atomic-blocks-in-native-procedures.md). A stored procedure's execution is not affected by a session's SET options, since atomic blocks are required. However, certain SET options, such as SET NOEXEC and SET SHOWPLAN_XML, cause stored procedures (including natively compiled stored procedures) to not execute.   
  
 When a natively compiled stored procedure is executed with any STATISTICS option turned on, statistics are gathered for the procedure as a whole and not per statement. For more information, see [SET STATISTICS IO &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-io-transact-sql.md), [SET STATISTICS PROFILE &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-profile-transact-sql.md), [SET STATISTICS TIME &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-time-transact-sql.md), and [SET STATISTICS XML &#40;Transact-SQL&#41;](../../t-sql/statements/set-statistics-xml-transact-sql.md). To obtain execution statistics on a per-statement level in natively compiled stored procedures, use an Extended Event session on the sp_statement_completed event, which starts when each individual query in a stored procedures execution completes. For more information on creating Extended Event sessions, see [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md).  
  
 **SHOWPLAN_XML** is supported for natively compiled stored procedures. **SHOWPLAN_ALL** and **SHOWPLAN_TEXT** are not supported with natively compiled stored procedures.  
  
 **SET FMTONLY** in not supported with natively compiled stored procedures. Use [sp_describe_first_result_set &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) instead.  
  
## See Also  
 [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md)  
  
  
