---
title: Troubleshooting Activation Stored Procedures
description: "Activated stored procedures run on a background session."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Troubleshooting Activation Stored Procedures

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Activated stored procedures run on a background session. Therefore, the techniques for troubleshooting an activation stored procedure differ slightly those used for troubleshooting stored procedures that are part of an interactive session.

## Technique: Analyzing the Service Broker Configuration

If activated stored procedures do not run successfully, use the **ssbdiagnose** utility to look for configuration errors in the associated services. For more information, see [Ssbdiagnose Utility](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md).

## Technique: Viewing Activation Stored Procedure Output

If the activated stored procedure produces incorrect results, or does not read from the queue, check the SQL Server error log for errors and messages that help locate the problem. Activated stored procedures are not associated with any application. Information that is normally returned to the calling application is instead put in the SQL Server error log. This includes errors, messages, and the output of PRINT and RAISERROR statements.

## Technique: Running the Stored Procedure From an Interactive Session

To troubleshoot an activation stored procedure, you can turn off activation on the queue, and then run the stored procedure from SQL Server Management Studio or the **sqlcmd** utility. If you run the stored procedure from an interactive session, you can see any errors that are returned by the stored procedure.

However, you might see different results if the security context and database settings are different in the interactive session than when the stored procedure is activated by the Database Engine. Before you run the procedure, do the following:

- Use EXECUTE AS to set the user for the interactive session to the user specified for activation.

- Set the options for the session to the database defaults.

For more information, see [Internal Activation Context](internal-activation-context.md).

## Symptom: Activation Stored Procedures Do Not Run

The following are common causes of this symptom:

- The settings for the queue might have been changed. Use the catalog view **sys.service_queues** to confirm the settings for the queue. Ensure that activation for the queue is enabled, that the queue specifies the correct stored procedure, and that the queue specifies the correct security principal. Confirm that the security principal has Execute permissions on the stored procedure.

- The stored procedure might not start, or might exit immediately after it starts. In this case, check the SQL Server error log for errors from the stored procedure. You can also run the stored procedure from SQL Server Management Studio and check the results.

## Symptom: Messages Remain on the Queue

Make sure that activation stored procedures are correctly started:

- Check the dynamic management view **sys.dm_broker_queue_monitors** to ensure that a queue monitor is active for the queue. If not, use the ALTER QUEUE statement to turn activation ON.

- The state of the queue monitor for the queue should be RECEIVES_OCCURRING. If the queue monitor is not in this state, check the dynamic management view **sys.dm_broker_activated_tasks** to ensure that activated tasks for the queue are currently running. If there are no activated tasks, activation is failing. For more information, see the "Symptom: Activation Stored Procedures Do Not Run" section earlier in this topic.

If activated tasks are running, but messages remain on the queue, the task is either failing to RECEIVE, or it is failing to commit transactions. Check the SQL Server error log for errors from the stored procedure. Stopping activation and running the stored procedure by hand might help troubleshoot the problem.

## See also

- [sys.service_queues (Transact-SQL)](../../relational-databases/system-catalog-views/sys-service-queues-transact-sql.md)
- [sys.dm_broker_queue_monitors (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-queue-monitors-transact-sql.md)
- [sys.dm_broker_activated_tasks (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-activated-tasks-transact-sql.md)
