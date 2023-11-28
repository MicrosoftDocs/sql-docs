---
title: Extended Security Updates
description: Learn how SQL Server enabled by Azure Arc enables Extended Security Updates
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 08/15/2023
ms.topic: conceptual
ms.custom: references_regions
---

# SQL Server enabled by Azure Arc supports Extended Security Updates

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] supports Extended Security Updates for eligible products and licenses. For details, see [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md).


## Extended Security Updates

Once [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] has reached the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years. When you upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], your ESU subscription is automatically canceled. When you [migrate to Azure SQL](/azure/azure-sql/migration-guides/), the ESU charges automatically stop but you continue to have access to the ESUs.

## Supported regions

[!INCLUDE [azure-arc-data-regions](../azure-arc/includes/azure-arc-data-regions.md)]

## Next steps

- [Learn about the prerequisites to connect your SQL Server to Azure Arc](prerequisites.md)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Learn more about Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-sql-usage)
- [Lean more about Microsoft Purview](/azure/purview/register-scan-azure-arc-enabled-sql-server)
