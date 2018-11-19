---
title: "SQL Server Management Studio (SSMS) Support Policy| Microsoft Docs"
ms.date: "11/13/2018"
ms.prod: sql
ms.technology: ssms
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Management Studio"
ms.assetid: 66a6b7b1-de6a-4161-82bd-98ded486947b
author: "dnethi"
ms.author: "dinethi"
manager: craigg
---
# SQL Server Management Studio (SSMS) Support Policy

The support policy for SQL Server Management Studio is stated as follows.

## Support Policy for SSMS
- Starting with SSMS 17.0, the SQL Tools team has adopted the [Microsoft Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy).
- Read the original [Modern Lifecycle Policy announcement](https://support.microsoft.com/help/447912/announcing-microsoft-modern-lifecycle-policy).
- For additional information, see [Modern Policy FAQs](https://support.microsoft.com/help/30882/modern-lifecycle-policy-faq).

## SSMS Updates 

Microsoft plans to release updates for SQL Server Management Studio (SSMS) a few times per year. Beginning with version 18.0, all security updates, critical updates, hotfixes, as well as any new features will be released only in the latest point version of the major version. Once a new major version is released for public, only security updates would be applied to the most recent release of the previous version. Versions older than the most recent release of the previous version are out of support.

For instance, SSMS 17.7 was released in May 2018. 17.7 got full support until June 2018 when 17.8.1 was released. At this point, 17.7 would only receive Security updates. SSMS 18.0 was released in December 2018. This puts 17.8.1 at Security Updates only phase and all prior versions of 17.x out of support.
For best experience, it is recommended for customers to install the most recent version via https://aka.ms/ssms  

- **Full Support** servicing phase: When running the latest current branch version of SQL Server Management Studio, you receive all updates – Security Updates, Critical Updates, new features etc.

- **Security Updates (Only)** servicing phase: After the release of a new current branch version, Microsoft only supports security updates to the most recent release of the previous version. Any versions prior to this version are out of support.

![Support-matrix](./media/ssms-supportpolicy/support-policy.png)


> [!NOTE]
> The latest current branch version is always in the **Full Support** servicing phase. This support statement means that if you encounter a code defect that warrants a critical update, you must have the latest current branch version installed in order to receive a fix.
