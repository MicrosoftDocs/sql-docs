---
title: "Configure and use Always Encrypted with secure enclaves| Microsoft Docs"
description: Learn how to configure and use Always Encrypted with secure enclaves in SQL Server and Azure SQL Database, which enables richer functionality on sensitive data.
author: jaszymas
ms.author: jaszymas
ms.reviewer: "vanto"
ms.date: 04/05/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
---
# Configure and use Always Encrypted with secure enclaves

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) extends the existing [Always Encrypted](always-encrypted-database-engine.md) feature to enable richer functionality on sensitive data while keeping the data confidential. This article lists common tasks for configuring and using the feature.

For tutorials that show you how to quickly get started with Always Encrypted with secure enclaves, see:

- [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)

## Set up the secure enclave and attestation

Before you can use Always Encrypted with secure enclaves, you need to configure your environment to ensure the secure enclave is available for the database. You may also need to set up [enclave attestation](always-encrypted-enclaves.md#secure-enclave-attestation), if applicable.

The process for setting up your environment depends on whether you're using [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] or [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)].

### Set up the secure enclave and attestation in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]

To set up Always Encrypted with secure enclaves without attestation, see:

- [Plan for Always Encrypted with secure enclaves in SQL Server without attestation](always-encrypted-enclaves-no-attestation-plan.md)
- [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md)

To set up Always Encrypted with secure enclaves and attestation, see:

- [Plan for Host Guardian Service attestation](./always-encrypted-enclaves-host-guardian-service-plan.md)
- [Deploy the Host Guardian Service for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]](./always-encrypted-enclaves-host-guardian-service-deploy.md)
- [Register  computer with the Host Guardian Service](./always-encrypted-enclaves-host-guardian-service-register.md)
- [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md)

### Set up the secure enclave and attestation in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)]

For details, see the following articles:

- [Plan for secure enclaves in [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)]](/azure/azure-sql/database/always-encrypted-enclaves-plan)
- [Enable Always Encrypted with secure enclaves for your [!INCLUDE [ssazure-sqldb](../../../includes/ssazure-sqldb.md)]](/azure/azure-sql/database/always-encrypted-enclaves-enable)
- [Configure Azure Attestation for your Azure SQL Database logical server](/azure/azure-sql/database/always-encrypted-enclaves-configure-attestation)

> [!IMPORTANT]
> VBS enclaves in Azure SQL Database (in preview) currently do not support attestation. Configuring Azure Attestation only applies to Intel SGX enclaves.

## Manage keys for Always Encrypted with secure enclaves

- [Manage keys for Always Encrypted with secure enclaves - overview](always-encrypted-enclaves-manage-keys.md)
- [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md)
- [Rotate enclave-enabled keys](always-encrypted-enclaves-rotate-keys.md)

## Configure columns with Always Encrypted with secure enclaves

- [Configure column encryption in-place using Always Encrypted with secure enclaves - overview](always-encrypted-enclaves-configure-encryption.md)
- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Configure column encryption in-place with PowerShell](always-encrypted-enclaves-configure-encryption-powershell.md)
- [Configure column encryption in-place with DAC Package](always-encrypted-enclaves-configure-encryption-dacpac.md)
- [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md)

## Run Transact-SQL statements using secure enclaves

- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)
- [Troubleshoot common issues for Always Encrypted with secure enclaves](always-encrypted-enclaves-troubleshooting.md)

## Create and use indexes on enclave-enabled columns

- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)
  
## Develop applications using Always Encrypted with secure enclaves

- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See also

- [Getting started using Always Encrypted with secure enclaves](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
