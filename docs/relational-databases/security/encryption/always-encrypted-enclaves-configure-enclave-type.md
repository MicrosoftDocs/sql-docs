---
title: "Configure the secure enclave in SQL Server"
description: "Configure the secure enclave for Always Encrypted with secure enclaves in SQL Server."
author: jaszymas
ms.author: jaszymas
ms.reviewer: "vanto"
ms.date: 02/15/2023
ms.service: sql
ms.subservice: security
ms.topic: conceptual
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---

# Configure the secure enclave in SQL Server

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

Before you can use [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) in SQL Server, you need to configure your instance to initialize the secure enclave during startup. By default, SQL Server doesn't initialize the secure enclave. You can change that by setting the  **column encryption enclave type** Server Configuration Option to the value that represents a valid enclave type for your environment.

> [!NOTE]
> The role responsible for configuring the secure enclave is the DBA. See [Roles and responsibilities when configuring attestation with HGS](always-encrypted-enclaves-host-guardian-service-plan.md#roles-and-responsibilities-when-configuring-attestation-with-hgs).

The supported enclave type for [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] or later is virtualization based security (VBS). Before configuring the VBS enclave type, make sure the computer hosting your instance meets the requirements stated in:

- [Plan for Always Encrypted with secure enclaves in SQL Server without attestation](always-encrypted-enclaves-no-attestation-plan.md) (if you're using Always Encrypted with secure enclaves without attestation)
- [Plan for Host Guardian Service attestation](always-encrypted-enclaves-host-guardian-service-plan.md) (if you're using the feature with attestation).

For detailed instructions on how to configure the enclave type, see [Configure the enclave type for Always Encrypted Server Configuration Option](../../../database-engine/configure-windows/configure-column-encryption-enclave-type.md).

## Next steps

 [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)

## See also  

[Server Configuration Options (SQL Server)](../../../database-engine/configure-windows/server-configuration-options-sql-server.md)
