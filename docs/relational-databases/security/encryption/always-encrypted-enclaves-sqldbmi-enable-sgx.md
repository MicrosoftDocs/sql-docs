---
title: "Enable Intel SGX for your database"
description: 
ms.custom: ""
ms.date: 12/18/2020
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "vanto"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Enable Intel SGX for your database 

[!INCLUDE [asdb](../../../includes/applies-to-version/asdb.md)]

[Always Encrypted with secure enclaves](always-encrypted-enclaves.md) in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] uses [Intel Software Guard Extensions (Intel SGX)](https://itpeernetwork.intel.com/microsoft-azure-confidential-computing/) enclaves. For Intel SGX to be available, the database must use the [vCore model](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal) and the [DC-series](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series) hardware generation.

> [!NOTE]
> Intel SGX is not available in hardware generations other than DC-series, for example, Gen5, and it is not available for databases using the [DTU model](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-dtu).

> [!IMPORTANT]
> Before you configure the DC-series hardware generation for your database, check the regional availability of DC-series and make sure you understand its performance limitations. For details, see [DC-series](https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#dc-series).

For detailed instructions for how to configure a new or existing database to use a specific hardware generation, see [Selecting a hardware generation](
https://docs.microsoft.com/azure/azure-sql/database/service-tiers-vcore?tabs=azure-portal#selecting-a-hardware-generation).

## Next steps

- [Configure Azure Attestation for your Azure SQL database server](./always-encrypted-enclaves-sqldbmi-configure-attestation.md)
