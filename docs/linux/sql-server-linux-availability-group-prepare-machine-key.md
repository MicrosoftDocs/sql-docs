---
title: Prepare machine key for contained availability groups on Linux and containers
description: Learn how to prepare a machine key in a contained availability group, for SQL Server on Linux and containers.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/05/2024
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
---

# Prepare machine key for contained availability groups on Linux and containers

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides an example of how to prepare a machine key for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on Linux in a contained availability group (AG).

A [contained AG](../database-engine/availability-groups/windows/contained-availability-groups-overview.md) is an availability group that supports:

- Managing metadata objects (users, logins, permissions, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent jobs, and so on) at the AG level in addition to the instance level.

- Specialized contained system databases within the AG.

The examples in this article target [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] in Linux containers, but you can use the same steps for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, running on physical machines, virtual machines, and in a Kubernetes-based deployment.

> [!CAUTION]  
> These instructions should only be used for *contained availability groups*. When you configure a contained AG with a common machine key across all replicas, first ensure there's no existing encryption hierarchy (for example, transparent data encryption, column-level encryption, or any other security-related feature that requires key management). Changing the machine key could break the encryption and cause data loss. After configuration, avoid creating or modifying the encryption hierarchy for security reasons.

## Overview of the machine key

In [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, the machine key plays a vital role in securing communication and data.
The following table describes its primary functions.

| Function | Description |
| --- | --- |
| **Encryption and decryption** | The machine key is used to encrypt and decrypt data exchanged between nodes in an AG |
| **Authentication** | It helps in authenticating communication between the primary and secondary replicas in an AG |
| **Security** | The machine key and associated certificates must be protected to prevent unauthorized access |

When you work with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Linux and contained AGs, you must synchronize the machine keys between [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] replicas. This process allows the service master key (SMK) in the contained `master` database to be used for decrypt and encrypt operations.

## Prepare SQL Server container on standalone host to run custom machine key

The following instructions show an example of generating a new machine key in `base64` format.

1. Generate a machine key file using OpenSSL (into a file called `machine-key.bin`) using the following script.

   ```bash
   openssl rand -out machine-key.bin 44
   printf '\x01\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00\x00' | dd of=machine-key.bin bs=1 seek=0 count=12 conv=notrunc
   cat machine-key.bin | base64
   ```

   The previous script performs these steps:

   1. Create a 44-byte binary file called `machine-key.bin`, containing random data.
   1. Overwrite the first 12 bytes with a fixed header. This might change in the future.
   1. Encode `machine-key.bin` in `base64` format and print it to the console.

1. Run the container by mounting the file to the machine key of the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container.

   ```bash
   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourpassword" -p 14331:1433 --name sqlcontainer --hostname sqlcontainer -v ~/machine-key.bin:/var/opt/mssql/secrets/machine-key -d mcr.microsoft.com/mssql/server:2022-latest
   ```

1. Verify that the `machine-key.bin` on the host machine is the same as the `machine-key.bin` used by the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container deployed in the previous step.

   ```bash
   cat machine-key.bin | base64
   ```

   Here's the sample output:

   ```output
   AQAAAAAAAAAAAAAA//////////////////////////////////////////8=
   ```

1. Inside the container, verify the key. First, connect to an interactive terminal in the container.

   ```bash
   docker exec -it sqlcontainer "bash"
   ```

   Then, verify the key.

   ```bash
   cat machine-key | base64
   ```

   Here's the sample output:

   ```output
   AQAAAAAAAAAAAAAA//////////////////////////////////////////8=
   ```

You can use the same steps across all the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container deployments that you intend to use as replicas in your contained AG. You must use the same machine key, and don't generate different machine keys for each of the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container deployments.

## Related content

- [Deploy and connect to SQL Server Linux containers](sql-server-linux-docker-container-deployment.md)
- [Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts](sql-server-linux-containers-deploy-helm-charts-kubernetes.md)
