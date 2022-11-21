---
title: "Configure Always Encrypted using SSMS"
description: Describes tasks for configuring and managing Always Encrypted databases with SQL Server Management Studio (SSMS).
ms.custom: seo-lt-2019
ms.date: 10/31/2019
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Always Encrypted, configure with SSMS"
ms.assetid: 29816a41-f105-4414-8be1-070675d62e84
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure Always Encrypted using SQL Server Management Studio
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes tasks for configuring Always Encrypted and managing databases that use Always Encrypted with [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md).

## Security Considerations when using SSMS to Configure Always Encrypted

When you use SSMS to configure Always Encrypted, SSMS handles both Always Encrypted keys and sensitive data, so both the keys and the data appear in plaintext inside the SSMS process. Therefore, it's important you run SSMS on a secure computer. If your database is hosted in SQL Server, make sure SSMS runs on a different computer than the computer hosting your SQL Server instance. As the primary goal of Always Encrypted is to ensure encrypted sensitive data is safe even if the database system gets compromised, executing a PowerShell script that processes keys or sensitive data on the SQL Server computer can reduce or defeat the benefits of the feature. For additional recommendations, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

SSMS doesn't support role separation between those who manage the database (DBAs) and those who manage cryptographic secrets and have access to plaintext data (Security Administrators and/or Application Administrators). If your organization enforces role separation, you should use PowerShell to configure Always Encrypted. For more information, see [Overview of Key Management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md) and [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md). 

## Always Encrypted Tasks using SSMS

- [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md)
- [Rotate Always Encrypted keys using SQL Server Management Studio](rotate-always-encrypted-keys-using-ssms.md)
- [Configure column encryption using Always Encrypted Wizard](always-encrypted-wizard.md)
- [Configure column encryption using Always Encrypted with a DAC package](configure-always-encrypted-using-dacpac.md)
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Export and import databases using Always Encrypted](always-encrypted-migrate-using-bacpac.md)
- [Back up and restore databases using Always Encrypted](always-encrypted-migrate-using-backup-restore.md)
- [Migrate data to or from columns using Always Encrypted with SQL Server Import and Export Wizard](always-encrypted-migrate-using-import-export-wizard.md)

## See Also
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)
- [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
