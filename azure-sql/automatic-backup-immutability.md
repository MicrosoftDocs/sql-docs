---
title: Automatic backup immutability
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn how automatic backup immutability protects up to the most recent seven days of point-in-time restore backups in Azure SQL Database and Azure SQL Managed Instance.
author: dnethi
ms.author: dinethi
ms.reviewer: mathoma
ms.date: 08/04/2026
ms.service: azure-sql
ms.subservice: backup-restore
ms.topic: concept-article

---

# Automatic backup immutability for Azure SQL Database and Azure SQL Managed Instance

This article describes immutability protection for automatic backups in Azure SQL Database and Azure SQL Managed Instance. The service applies immutability protection for up to the most recent seven days of point-in-time restore (PITR) backups, at no extra cost. The service enables this protection by default as a native service capability without any necessary user configuration.

## What this feature provides

Automatic backup immutability has the following characteristics:

- Up to the most recent seven days of PITR backups are protected with immutability.
- All [supported](#limitations) Azure SQL Database and Azure SQL Managed Instance databases have protection enabled by default.
- No onboarding or manual configuration is required.
- Protection is applied regardless of your configured PITR retention period.
- There's no extra cost.

Because this capability is integrated into the managed backup service, existing backup and restore workflows continue to work without change.

## Why this feature matters

Backup immutability helps strengthen recovery readiness by reducing the risk that recent recovery points are altered or deleted. This protection is especially important in the following scenarios:

- Ransomware or malicious activity that targets backup data
- Accidental deletion
- Compromised administrative credentials

By preserving recent restore points, organizations can improve confidence in recovery operations during incidents.

## Compliance and governance considerations

Many organizations need tamper-resistant data protections to support governance and regulatory obligations.

Azure SQL backup immutability uses Azure Storage's immutable storage capabilities to enforce protection. Azure Storage immutable storage capabilities are validated for scenarios that include the following requirements:

- SEC Rule 17a-4(f)
- CFTC Rule 1.31(d)
- FINRA record-retention requirements

Automatic backup immutability can also support broader resilience and governance programs.

> [!IMPORTANT]
> Compliance requirements vary by regulation, jurisdiction, and implementation. Evaluate your specific legal and regulatory obligations with your compliance and legal teams.

## Operational impact

This feature reduces operational overhead.

You don't need to perform any of the following actions:

- Create immutability policies
- Configure storage accounts
- Manage retention locks
- Extract data to apply protection

The managed backup service automatically applies immutability protection.

## Related capabilities

Automatic backup immutability complements existing data protection features, including the following features:

- Automated backups
- Point-in-time restore
- Geo-redundant backup options
- Soft-delete protection for Azure SQL logical servers

## Limitations

Automatic backup immutability isn't currently available for the Hyperscale service tier of Azure SQL Database.


## Related content

- [Immutable storage in Azure](/azure/storage/blobs/immutable-storage-overview)
