---
title: "Known issues: SQL Server enabled by Azure Arc"
description: "Describes current behavior that isn't expected for SQL Server enabled by Azure Arc."
author: MikeRayMSFT
ms.author: mikeray
# ms.service: [the approved service name] Defined in the docfx.json
ms.topic: troubleshooting-known-issue #Don't change.
ms.date: 10/08/2024

--- 

# Known issues: SQL Server enabled by Azure Arc

This article provides information about known issues associated with SQL Server enabled by Azure Arc.

## Databases

If a database isn't online and updatable, required permissions won't be assigned to that database. Features requiring permissions to that database will be impacted.

### Resolution

- Verify that the databases are online and updateable. Review [Verify state of user databases](prerequisites.md#verify-state-of-user-databases).
- Check the error logs. The error log may show Microsoft SQL Server error 945:

   ```log
   Database cannot be opened due to inaccessible files or insufficient memory or disk space
   ```

   To resolve, follow the steps at [MSSQLSERVER_945](../../relational-databases/errors-events/mssqlserver-945-database-engine-error.md).

## Related content

- [Troubleshoot Azure extension for SQL Server deployment](troubleshoot-deployment.md)
- [Troubleshoot Azure extension for SQL Server](troubleshoot-extension.md)
