---
title: "Configure and use Always Encrypted with secure enclaves| Microsoft Docs"
description: Learn how to configure and use Always Encrypted with secure enclaves in SQL Server and Azure SQL Database, which enables richer functionality on sensitive data.
ms.custom: ""
ms.date: 10/18/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "vanto"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Configure and use Always Encrypted with secure enclaves 

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]
[!INCLUDE [asdb](../../../includes/applies-to-version/asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) extends the existing [Always Encrypted](always-encrypted-database-engine.md) feature to enable richer functionality on sensitive data while keeping the data confidential. This article lists common tasks for configuring and using the feature.

For a tutorial that shows you how to quickly get started with Always Encrypted with secure enclaves, see [Tutorial: Getting started with Always Encrypted with secure enclaves using SSMS](../tutorial-getting-started-with-always-encrypted-enclaves.md).

## Set up the secure enclave and attestation

Before you can use Always Encrypted with secure enclaves in your database, you need to configure your environment to ensure the secure enclave is available for the database. You also you need set up [enclave attestation](always-encrypted-enclaves.md#secure-enclave-attestation). 

The process for setting up your environment depends on whether you are using [!INCLUDE[sql-server-2019](../../../includes/sssqlv15-md.md)] or [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)].

### Set up the secure enclave and attestation in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]

For details, see the following articles:
- [Plan for Host Guardian Service attestation](./always-encrypted-enclaves-host-guardian-service-plan.md)
- [Deploy the Host Guardian Service for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]](./always-encrypted-enclaves-host-guardian-service-deploy.md)
- [Register  computer with the Host Guardian Service](./always-encrypted-enclaves-host-guardian-service-register.md)
- [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md)

### Set up the secure enclave and attestation in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]

For details, see the following articles:
- [Plan for Intel SGX enclaves and attestation in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]](always-encrypted-enclaves-sqldbmi-plan.md)
- [Enable Intel SGX for your Azure SQL database](./always-encrypted-enclaves-sqldbmi-enable-sgx.md)
- [Configure Azure Attestation for your Azure SQL database server](./always-encrypted-enclaves-sqldbmi-configure-attestation.md)

## Manage keys for Always Encrypted with secure enclaves
See the following articles for details:
- [Manage keys for Always Encrypted with secure enclaves - overview](always-encrypted-enclaves-manage-keys.md)
- [Provision enclave-enabled keys](always-encrypted-enclaves-provision-keys.md)
- [Rotate enclave-enabled keys](always-encrypted-enclaves-rotate-keys.md)

## Configure columns with Always Encrypted with secure enclaves
See the following articles for details:
- [Configure column encryption in-place using Always Encrypted with secure enclaves - overview](always-encrypted-enclaves-configure-encryption.md)
- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md)

> [!NOTE]
> For a step-by-step tutorial on how to set up a test environment and try the functionality of Always Encrypted with secure enclaves in SSMS, see [Tutorial: Getting started with Always Encrypted with secure enclaves using SSMS](../tutorial-getting-started-with-always-encrypted-enclaves.md).

## Query columns using Always Encrypted with secure enclaves
See the following articles for details:
- [Query columns using Always Encrypted with secure enclaves - overview](always-encrypted-enclaves-query-columns.md)
- [Query columns using Always Encrypted with secure enclaves with SSMS](always-encrypted-enclaves-query-columns-ssms.md)

## Create and use indexes on enclave-enabled columns
See the following articles for details:
- [Create and use indexes on columns using Always Encrypted with secure enclaves](always-encrypted-enclaves-create-use-indexes.md)

## Develop applications using Always Encrypted with secure enclaves
See the following articles for details:
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)
