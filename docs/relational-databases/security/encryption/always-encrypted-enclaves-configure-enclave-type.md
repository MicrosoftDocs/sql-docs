---
title: "Configure the secure enclave in SQL Server"
description: "Configure the secure enclave for Always Encrypted with secure enclaves in SQL Server."
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: "vanto"
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---

# Configure the secure enclave in SQL Server

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

Before you can use [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) in SQL Server, you need to configure your instance to initialize the secure enclave during startup. By default, SQL Server doesn't initialize the secure enclave. You can change that by setting the  **column encryption enclave type** Server Configuration Option to the value that represents a valid enclave type for your environment.

> [!NOTE]
> The role responsible for configuring the secure enclave is the DBA. See [Roles and responsibilities when configuring attestation with HGS](always-encrypted-enclaves-host-guardian-service-plan.md#roles-and-responsibilities-when-configuring-attestation-with-hgs).

The supported enclave type for [!INCLUDE[sql-server-2019](../../../includes/sssql19-md.md)] or later is virtualization based security (VBS). Before configuring the VBS enclave type, we recommend you set up attestation with Host Guardian Service (HGS) for the computer hosting your instance. To get started with HGS, see [Plan for Host Guardian Service attestation](always-encrypted-enclaves-host-guardian-service-plan.md). Setting up attestation also enables virtualization based security, which is required for a VBS enclave to get initialized properly. For more information, see [Verify virtualization-based security is running](always-encrypted-enclaves-host-guardian-service-register.md#step-2-verify-virtualization-based-security-is-running).

For detailed instructions on how to configure the enclave type, see [Configure the enclave type for Always Encrypted Server Configuration Option](../../../database-engine/configure-windows/configure-column-encryption-enclave-type.md).

## Next Steps

 [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)

## See Also  
 
 [Server Configuration Options (SQL Server)](../../../database-engine/configure-windows/server-configuration-options-sql-server.md)
