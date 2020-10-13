---
title: "Plan for SGX enclaves and attestation in Azure SQL Database"
ms.custom: ""
ms.date: 11/18/2020
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "vanto"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Plan for Intel SGX enclaves and attestation in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] 

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] uses [Intel Software Guard Extensions (Intel SGX)](https://itpeernetwork.intel.com/microsoft-azure-confidential-computing/) enclaves and requires [Microsoft Azure Attestation](https://docs.microsoft.com/azure/attestation/overview) for [enclave attestation](always-encrypted-enclaves.md#secure-enclave-attestation).

## Plan for Intel SGX for in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]

Intel SGX is a hardware-based trusted execution environment technology. Intel SGX is available for databases that use the [vCore model](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal) and one specific [hardware generation](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#hardware-generations) - [DC-series](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series). Therefore, to ensure you can use Always Encrypted with secure enclaves in your database, you need to either select the DC-series hardware generation when you create the database, or you can update your existing database to use the DC-series hardware generation.

> [!NOTE]
> Intel SGX is not available in hardware generations other than DC-series, for example, Gen5, and it is not available for databases using the [DTU model](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-dtu).

> [!IMPORTANT]
> Before you configure the DC-series hardware generation for your database, check the regional availability of DC-series and make sure you understand its performance limitations. For details, see [DC-series](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series).

For detailed instructions for how to configure a new or existing database to use a specific hardware generation, see [Selecting a hardware generation](
https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#selecting-a-hardware-generation).


## Plan for attestation in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]

[!INCLUDE [asdb](../../../includes/applies-to-version/asdb.md)]

[Microsoft Azure Attestation](https://docs.microsoft.com/azure/attestation/overview) (preview) is a solution for attesting Trusted Execution Environments (TEEs), including Intel SGX enclaves in Azure SQL databases using the DC-series hardware configuration.

To use Azure Attestation for attesting Intel SGX enclaves in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], you need to:

1. Create an [attestation provider](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-provider) and configure it with an attestation policy. 

2. Grant your Azure SQL database server access to the created attestation provider.

## Next steps

- [Enable Intel SGX for your Azure SQL database](./always-encrypted-enclaves-sqldbmi-enable-sgx.md)
