---
title: "Enable Intel SGX for Always Encrypted"
description: Learn how to enable Intel SGX for Always Encrypted with secure enclaves in Azure SQL Database by selecting SGX-enabled hardware.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 04/06/2022
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
---
# Enable Always Encrypted with secure enclaves for your Azure SQL Database 

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

# [Intel SGX enclaves](#tab/IntelSGXenclaves)

[Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/always-encrypted-enclaves) in Azure SQL Database can use [Intel Software Guard Extensions (Intel SGX)](https://itpeernetwork.intel.com/microsoft-azure-confidential-computing/) enclaves. For Intel SGX to be available, the database must use the [vCore model](service-tiers-vcore.md) and [DC-series](service-tiers-sql-database-vcore.md#dc-series) hardware.

Configuring the DC-series hardware to enable Intel SGX enclaves is the responsibility of the Azure SQL Database administrator. See [Roles and responsibilities when configuring Intel SGX enclaves and attestation](always-encrypted-enclaves-plan.md#roles-and-responsibilities-when-configuring-intel-sgx-enclaves-and-attestation).

> [!NOTE]
> Intel SGX is not available in hardware configurations other than DC-series. For example, Intel SGX is not available for standard-series (Gen5) hardware, and it is not available for databases using the [DTU model](service-tiers-dtu.md).

> [!IMPORTANT]
> Before you configure the DC-series hardware for your database, check the regional availability of DC-series and make sure you understand its performance limitations. For more information, see [DC-series](service-tiers-sql-database-vcore.md#dc-series).

For detailed instructions for how to configure a new or existing database to use a specific hardware configuration, see [Hardware configuration](service-tiers-sql-database-vcore.md#hardware-configuration).
   

# [VBS enclaves](#tab/VBSenclaves)
Customer can specify PreferredEnclaveType value as “VBS” or “Default” or skip this property in API call 
(customer can either specify “Default” as the value OR if property is not specified then this is the default value taken by the system)

If customer has not explicitly selected “VBS” as value for this property, the system default behavior will kick in:
- Depending on capacity constraints and Gen2 VM availability in the selected region, database will be placed in Gen2 VM and VBS enclave will be initialized 
OR
- Database will be placed on Gen1 VM and no enclave initialization will occur


## Next steps

- [Configure Azure Attestation for your Azure SQL database server](always-encrypted-enclaves-configure-attestation.md)

## See also

- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](always-encrypted-enclaves-getting-started-sgx.md)
