---
title: Secure - best practices
description: Provide an overview and describe best practices to secure SQL Server enabled by Azure Arc.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: nikitatakru
ms.topic: how-to
ms.date: 05/20/2024

---

# Secure SQL Server enabled by Azure Arc - best practices

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article lists best practices that you can follow to secure SQL Server enabled by Azure Arc.

## Best practices

Implement the following configurations to comply with current best practices to secure instances of SQL Server enabled by Azure Arc:

* Enable [least privilege mode (preview)](configure-least-privilege.md).
* Run [SQL best practices assessment](assess.md).
* Enable [Microsoft Entra authentication](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md).
* Enable [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage) and resolve the issues pointed out by Defender for SQL.
* Don't enable SQL authentication. It's disabled by default. Review [SQL Server security best practices](../../relational-databases/security/sql-server-security-best-practices.md).

## Related content

- [Configure advanced data security for your SQL Server instance](configure-advanced-data-security.md)
- [Configure best practices assessment on a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance](assess.md)
