---
title: "SQL Server Management Studio (SSMS) Support Policy"
description: "SQL Server Management Studio (SSMS) Support Policy"
ms.date: "03/08/2023"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Management Studio"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
---

# SQL Server Management Studio (SSMS) Support Policy

The support policy for SQL Server Management Studio is stated as follows.

## Support Policy for SSMS
- Starting with SSMS 17.0, the SQL Tools team has adopted the [Microsoft Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy).
- Read the original [Modern Lifecycle Policy announcement](https://support.microsoft.com/help/447912/announcing-microsoft-modern-lifecycle-policy).
- For additional information, see [Modern Policy FAQs](https://support.microsoft.com/help/30882/modern-lifecycle-policy-faq).

## SSMS Updates 

Beginning with version 18.0, all security updates, critical updates, hotfixes, as well as any new features will be released only in the latest point version of the major version. Once a new version of SSMS is released for public, whether it's a point version within a major version or a major version itself, all prior versions are out of support, as per the [Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy).


For instance, SSMS 18.12.1 was released in June 2022. 18.12.1 had full support until January 2023 when 19.0 was released. At this point, 18.12.1 would be out of support. When 19.0.1 was released in February 2023, all prior versions were out of support.

For best experience, it is recommended for customers to install the most recent version via https://aka.ms/ssms  

**Full Support** servicing phase: When running the latest current branch version of SQL Server Management Studio, you receive all updates - Security Updates, Critical Updates, new features etc.

:::image type="content" source="media/support-policy/ssms-support-policy.png" alt-text="Screenshot of SSMS support life cycle.":::

> [!NOTE]
> The latest current branch version is always in the **Full Support** servicing phase. This support statement means that if you encounter a code defect that warrants a critical update, you must have the latest current branch version installed in order to receive a fix.
