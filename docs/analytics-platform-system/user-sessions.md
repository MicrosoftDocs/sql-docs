---
title: User sessions in Analytics Platform System | Microsoft Docs"
description: User sessions in Analytics Platform System's Parallel Data Warehouse.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# User Sessions in Analytics Platform System
A login with the appropriate permissions can manage the sessions of all logins on a SQL Server PDW appliance, including performing these actions:  
  
-   View the current sessions on the appliance, including both active and idle sessions.  
  
-   View the active and recent queries for a session.  
  
-   End active sessions.  
  
These actions can be performed by using either the [Monitor the Appliance by Using the Admin Console](monitor-the-appliance-by-using-the-admin-console.md) or [System Views](tsql-system-views.md) through SQL commands, as shown below.  
  
The permissions required to manage sessions by using either method are the same, and are described in [Grant Permissions to Manage Logins, Users, and Database Roles](grant-permissions.md#grant-permissions-to-manage-logins-users-and-database-roles).  
  
## Manage Sessions by Using the Admin Console  
  
### To view current sessions by using the Admin Console  
  
1.  On the top menu, click **Sessions**.  
  
2.  The resulting list displays all recent sessions. To view only 'Active' or 'Idle' sessions, click the **Status** column header to sort results by status.  
  
### To view active and recent queries for a session by using the Admin Console  
  
1.  On the top menu, click **Sessions**.  
  
2.  In the results list, click the session ID of the desired session.  
  
3.  The resulting queries list shows the recent queries for the session. For information on viewing query details, see [Monitoring Active Queries](monitoring-active-queries.md).  
  
### To end sessions by using the Admin Console  
  
1.  On the top menu, click **Sessions**.  
  
2.  Find the session ID for the session to cancel.  
  
3.  Click the red **X** to the left of the session ID to end the session. Only sessions with a status of 'Active' or 'Idle' will have a red **X**; only these sessions can be ended.  
  
## Manage Sessions by Using System Views and SQL Commands  
  
### To View Current Sessions by Using System Views  
Use [sys.dm_pdw_exec_sessions](../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md) to generate a list of current sessions.  
  
This example returns the session_id, login_name, and status for all sessions with a status of 'Active' or 'Idle'.  
  
```sql  
SELECT session_id, login_name, status FROM sys.dm_pdw_exec_sessions WHERE status='Active' OR status='Idle';  
```  
  
### To View Active and Recent Queries for a Session by Using System Views  
To see the active and recently completed queries associated with a session, you use the [sys.dm_pdw_exec_sessions](../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md) and [sys.dm_pdw_exec_requests](../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-requests-transact-sql.md) views. This query returns a list of all active or idle sessions, plus any active or recent queries associated with each session ID.  
  
```sql  
SELECT es.session_id, es.login_name, es.status AS sessionStatus,   
er.request_id, er.status AS requestStatus, er.command   
FROM sys.dm_pdw_exec_sessions es   
LEFT OUTER JOIN sys.dm_pdw_exec_requests er   
ON (es.session_id=er.session_id)   
WHERE (es.status='Active' OR es.status='Idle') AND   
(er.status!= 'Completed' AND er.status!= 'Failed' AND er.status!= 'Cancelled');  
```  
  
### To End Sessions by Using SQL Commands  
Use the [KILL](../t-sql/language-elements/kill-transact-sql.md) command to end a current session. You will need the session ID for the process to terminate, which can be obtained using the [sys.dm_pdw_exec_sessions](../relational-databases/system-dynamic-management-views/sys-dm-pdw-exec-sessions-transact-sql.md) view.  
  
In this example, select the login_name, session_id, and status values to find a session based on the login name.  
  
```sql  
SELECT session_id, login_name, status FROM sys.dm_pdw_exec_sessions;  
```  
  
Sessions with an 'Active' or 'Idle' status can be ended by using the KILL command.  
  
```sql  
KILL 'SID137';  
```  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
