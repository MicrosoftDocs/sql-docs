---
title: "View a SQL Server Audit Log"
description: Learn how to view a SQL Server audit log in SQL Server by using SQL Server Management Studio. Viewing requires CONTROL SERVER permission.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: vanto
ms.date: 05/15/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "audits [SQL Server], viewing logs"
  - "viewing audit logs"
  - "logs [SQL Server], audit"
---
# View a SQL Server Audit Log

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes how to view a SQL Server audit log in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].

## <a name="Permissions"></a> Permissions

Requires the **CONTROL SERVER** permission.

## <a name="SSMSProcedure"></a> Using SQL Server Management Studio

### To view a SQL Server audit log

1. In Object Explorer, expand the **Security** folder.

1. Expand the **Audits** folder.

1. Right-click the audit log that you want to view and select **View Audit Logs**. The **Log File Viewer -** _server\_name_ dialog box opens. For more information, see [Log File Viewer F1 Help](../../../relational-databases/logs/log-file-viewer-f1-help.md).

1. When finished, select **Close**.

[!INCLUDE [msCoName](../../../includes/msconame-md.md)] recommends viewing the audit log by using the Log File Viewer. However, if you're creating an automated monitoring system, the information in the audit file can be read directly by using the `sys.fn_get_audit_file` function. Reading the file directly returns data in a slightly different (unprocessed) format. For more information, see [sys.fn_get_audit_file (Transact-SQL)](../../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md).

## Related content

- [SQL Server Audit (Database Engine)](../../../relational-databases/security/auditing/sql-server-audit-database-engine.md)
- [Write SQL Server Audit Events to the Security Log](../../../relational-databases/security/auditing/write-sql-server-audit-events-to-the-security-log.md)
