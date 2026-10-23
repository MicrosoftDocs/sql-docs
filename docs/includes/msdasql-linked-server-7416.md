---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/26/2026
ms.service: sql
ms.topic: include
---

Linked server queries that use the Microsoft OLE DB Provider for ODBC (`MSDASQL`) and specify a provider string (*@provstr*) might fail with the following error:

```output
Msg 7416, Level 16
Access to the remote server is denied because no login-mapping exists.
```

This issue affects logins that aren't members of the **sysadmin** fixed server role. It can occur even when the linked server and login mappings are configured correctly.

In certain linked server configurations that use the `MSDASQL` provider, a stricter connection validation check in the Database Engine can reject connections that were allowed in previous builds.

For more information, see [Linked server queries that use MSDASQL fail with error 7416](/troubleshoot/sql/database-engine/linked-servers/msdasql-query-error-7416).
