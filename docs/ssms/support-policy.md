---
title: "SQL Server Management Studio (SSMS) support policy"
description: "SQL Server Management Studio (SSMS) support policy"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 08/03/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Management Studio"
---
# SQL Server Management Studio (SSMS) support policy

The support policy for SQL Server Management Studio is stated as follows.

## Support policy for SSMS

- Starting with SSMS 17.0, the SQL Tools team has adopted the [Microsoft Modern Lifecycle Policy](/lifecycle/policies/modern).
- Read the original [Modern Lifecycle Policy announcement](/lifecycle/announcements/modern-policy).
- For more information, see [Modern Policy FAQs](/lifecycle/faq/modern-policy).

For the best experience, we recommend that you install the most recent version via <https://aka.ms/ssms>.

## SSMS updates

Beginning with version 18.0, all security updates, critical updates, hotfixes, and any new features, are released only in the latest point release of a major version. Once a new version of SSMS is released to the public, whether it's a point release within a major version or a major version itself, we recommend that you update to the latest release.

For example, SSMS 18.12.1 was released in June 2022, and SSMS 19.1 was released in May 2023. If a customer is using SSMS 18.12.1 and encounters an issue, the customer must upgrade to SSMS 19.1 and determine if the issue still exists. If the issue still exists in SSMS 19.1, it should be reported to the SSMS team. A fix may be then provided in a later release of SSMS.

The SSMS team doesn't actively back-port fixes to an earlier release.

**Full support** servicing phase: When running the latest current branch version of SQL Server Management Studio, you receive all updates, such as security updates, critical updates, and new features.

Refer to the following table for SQL Server Management Studio servicing support.

| Version | November 2018 | June 2022 | January 2023 | May 2023 |
| ---: | :---: | :---: | :---: | :---: |
| 17.9.1 | Full support | Upgrade | Upgrade | Upgrade |
| 18.12.1 | | Full support | Upgrade | Upgrade |
| 19.0 | | | Full support | Upgrade |
| 19.1 | | | | Full support |

> [!NOTE]  
> The latest current branch version is always in the **Full support** servicing phase. This support statement means that if you encounter a code defect that warrants a critical update, you must have the latest current branch version installed in order to receive a fix.
