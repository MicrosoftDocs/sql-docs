---
title: "Plan for Host Guardian Service attestation"
description: "Plan Host Guardian Service attestation for SQL Server Always Encrypted with secure enclaves."
ms.custom:
- event-tier1-build-2022
ms.date: "05/24/2022"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Plan for Host Guardian Service attestation

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

When you use [Always Encrypted with secure enclaves](always-encrypted-enclaves.md), make sure that the client application is talking to a trustworthy enclave within the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] process. For a virtualization-based security (VBS) enclave, this requirement includes verifying both the code inside the enclave is valid and the computer hosting [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is trustworthy. Remote attestation achieves this goal by introducing a third party that can validate the identity (and optionally, the configuration) of the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer. Before [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] can use an enclave to run a query, it must provide information to the attestation service about its operating environment to obtain a health certificate. This health certificate is then sent to the client, which can independently verify its authenticity with the attestation service. Once the client trusts the health certificate, it knows it's talking to a trustworthy VBS enclave and will issue the query that will use that enclave.

The Host Guardian Service (HGS) role in Windows Server 2019 or later provides remote attestation capabilities for Always Encrypted with VBS enclaves.
This article will guide you through the pre-deployment decisions and requirements to use Always Encrypted with VBS enclaves and HGS attestation.

## Architecture overview

The Host Guardian Service (HGS) is a clustered web service that runs on Windows Server 2019 or later.
In a typical deployment, there will be 1-3 HGS servers, at least one computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], and a computer running a client application or tools, such as SQL Server Management Studio.
Since the HGS is responsible for determining which computers running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] are trustworthy, it requires both physical and logical isolation from the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance it's protecting.
If the same admins have access to HGS and a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer, they could configure the attestation service to allow a malicious computer to run [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], enabling them to compromise the VBS enclave.

### HGS domain

HGS setup will automatically create a new Active Directory domain for the HGS servers, failover cluster resources, and administrator accounts.

The computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] doesn't need to be in a domain, but if it is, it should be a different domain than the one the HGS server uses.

### High availability

The HGS feature automatically installs and configures a failover cluster.
In a production environment, it's recommended to use three HGS servers for high availability. Refer to the [failover cluster documentation](/windows-server/failover-clustering/manage-cluster-quorum) for details on how cluster quorum is determined and alternative configurations, including two node clusters with an external witness.

Shared storage isn't required between the HGS nodes. A copy of the attestation database is stored on each HGS server and is automatically replicated over the network by the cluster service.

### Network connectivity

Both the SQL client and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] need to be able to communicate with HGS over HTTP.
Configure HGS with a TLS certificate to encrypt all communications between the SQL Client and HGS, as well as between SQL Server and HGS.
This configuration helps protect from man-in-the-middle attacks and ensures you're talking to the correct HGS server.

HGS servers require connectivity between each node in the cluster to ensure the attestation service database stays in sync.
It's a failover cluster best practice to connect the HGS nodes on one network for cluster communication and use a separate network for other clients to communicate with HGS.

### Attestation modes

When a computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] tries to attest with HGS, it will first ask HGS how it should attest.
HGS supports two attestation modes for use with [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]:

| Attestation mode | Explanation |
| ---------------- | ------- |
| TPM | Trusted Platform Module (TPM) attestation provides the strongest assurance about the identity and integrity of the computer attesting with HGS. It requires the computers running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to have TPM version 2.0 installed. Each TPM chip contains a unique and immutable identity (Endorsement Key) that can be used to identify a particular computer. TPMs also measure the boot process of the computer, storing hashes of security-sensitive measurements in Platform Control Registers (PCRs) that can be read, but not modified by the operating system. These measurements are used during attestation to provide cryptographic proof that a computer is in the security configuration it claims to be. |
| Host Key | Host key attestation is a simpler form of attestation that only verifies the identity of a computer by using an asymmetric key pair. The private key is stored on the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and the public key is provided to HGS. The security configuration of the computer isn't measured and a TPM 2.0 chip isn't required on the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. It's important to protect the private key installed on the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer because anyone who obtains this key can impersonate a legitimate [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer and the VBS enclave running inside [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. |

In general, we make the following recommendations:

- For **physical production servers**, we recommend using TPM attestation for the additional assurances it provides.
- For **virtual production servers**, we recommend host key attestation since most virtual machines don't have virtual TPMs or Secure Boot. If you're using a security-enhanced VM like an [on-premises shielded VM](/windows-server/security/guarded-fabric-shielded-vm/guarded-fabric-and-shielded-vms-top-node), you may choose to use TPM mode. In all virtualized deployments, the attestation process only analyzes your VM environment, and not the virtualization platform underneath the VM.
- For **dev/test scenarios**, we recommend host key attestation because it's easier to set up.

### Trust model

In the VBS enclave trust model, the encrypted queries and data are evaluated in a software-based enclave to protect it from the host OS.
Access to this enclave is protected by the hypervisor in the same way two virtual machines running on the same computer can't access each other's memory.
In order for a client to trust that it's talking to a legitimate instance of VBS, you must use TPM-based attestation that establishes a hardware root of trust on the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer.
The TPM measurements captured during the boot process include the unique identity key of the VBS instance, ensuring the health certificate is only valid on that exact computer.
Further, when a TPM is available on a computer running VBS, the private part of the VBS identity key is protected by the TPM, preventing anyone from trying to impersonate that VBS instance.

Secure Boot is required with TPM attestation to ensure UEFI loaded a legitimate, Microsoft-signed bootloader and that no rootkits intercepted the hypervisor boot process.
Additionally, an IOMMU device is required by default to ensure any hardware devices with direct memory access can't inspect or modify enclave memory.

These protections all assume the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is a physical machine.
If you virtualize the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], you can no longer guarantee that the memory of the VM is safe from inspection by the hypervisor or hypervisor admin.
A hypervisor admin could, for example, dump the memory of the VM and gain access to the plaintext version of the query and data in the enclave.
Similarly, even if the VM has a virtual TPM, it can only measure the state and integrity of the VM operating system and boot environment.
It can't measure the state of the hypervisor controlling the VM.

However, even when [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is virtualized, the enclave is still protected from attacks originating within the VM operating system.
If you trust your hypervisor or cloud provider, and are primarily worried about database admin and OS admin attacks on sensitive data, a virtualized [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] may meet your requirements.

Similarly, Host Key attestation is still valuable in situations where a TPM 2.0 module isn't installed on the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] or in dev/test scenarios where security isn't paramount.
You can still use many of the security features mentioned above, including Secure Boot and a TPM 1.2 module, to better protect VBS and the operating system as a whole.
But, since there's no way for HGS to verify the computer actually has these settings enabled with Host Key attestation, the client isn't assured the host is indeed using all available protections.

## Prerequisites

### HGS server prerequisites

The computer(s) running the Host Guardian Service role should meet the following requirements:

| Component | Requirement |
| --------- | ----------- |
| Operating System | Windows Server 2019 or later, Standard or Datacenter edition |
| CPU | 2 cores (min), 4 cores (recommended) |
| RAM | 8 GB (min) |
| NICs | 2 NICs with static IPs recommended (1 for cluster traffic, 1 for HGS service) |

HGS is a CPU-bound role because of the number of actions that require encryption and decryption.
Using modern processors with cryptographic acceleration capabilities will improve HGS performance.
Storage requirements for attestation data are minimal, on the range of 10 KB to 1 MB per unique computer attesting.

Don't join the HGS computer(s) to a domain before you start.

### [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer prerequisites

The computer(s) running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] must meet both the [Requirements for Installing SQL Server](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md) and the [Hyper-V hardware requirements](/virtualization/hyper-v-on-windows/reference/hyper-v-requirements#hardware-requirements).

These requirements include:

- [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] or later
- Windows 10, version 1809 or later - Enterprise edition, Windows 11 or later - Enterprise edition, Windows Server 2019 or later - Datacenter edition. Other editions of Windows 10/11 and Windows Server don't support attestation with HGS.
- CPU support for virtualization technologies:
  - Intel VT-x with Extended Page Tables.
  - AMD-V with Rapid Virtualization Indexing.
  - If you're running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] in a VM:
    - In Azure, use a [Generation 2 VM size](/azure/virtual-machines/generation-2#generation-2-vm-sizes) (recommended) or use a Generation 1 VM size with nested virtualization enabled. Check the [individual VM sizes documentation](/azure/virtual-machines/sizes) to determine which Generation 1 VM sizes support nested virtualization.
    - On Hyper-V 2016 or later (outside of Azure), make sure your VM is a Generation 2 VM (recommended) or it's a Generation 1 VM with nested virtualization enabled. For more information, see [Should I create a generation 1 or 2 virtual machine in Hyper-V?](/windows-server/virtualization/hyper-v/plan/should-i-create-a-generation-1-or-2-virtual-machine-in-hyper-v) and [Configure nested virtualization](/virtualization/hyper-v-on-windows/user-guide/nested-virtualization#configure-nested-virtualization).
    - On VMware vSphere 6.7 or later, enable virtualization-based security support for the VM as described in the [VMware documentation](https://docs.vmware.com/en/VMware-vSphere/6.7/com.vmware.vsphere.vm_admin.doc/GUID-C2E78F3E-9DE2-44DB-9B0A-11440800AADD.html).
    - Other hypervisors and public clouds may support nested virtualization capabilities that enable Always Encrypted with VBS Enclaves as well. Check your virtualization solution's documentation for compatibility and configuration instructions.
- If you plan to use TPM attestation, you'll need a TPM 2.0 rev 1.16 chip ready for use in the server. At this time, HGS attestation doesn't work with TPM 2.0 rev 1.38 chips. Additionally, the TPM must have a valid Endorsement Key Certificate.

## Roles and responsibilities when configuring attestation with HGS

Setting up attestation with HGS involves configuring components of different types: HGS, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances, and applications that trigger enclave attestation. Configuring components of each type is performed by users assuming one of the below distinct roles:

- HGS administrator - deploys HGS, registers [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers with HGS, and shares the HGS attestation URL with [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer administrators and client application administrators.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer administrator - installs attestation client components, enables VBS on [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers, provides the HGS administrator with the information required to register the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers with HGS, configures the attestation URL on [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers, and verifies [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers can successfully attest with HGS.
- DBA - configures secure enclaves in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instances.
- Application administrator - configures application with the attestation URL obtained from the HGS administrator.

In production environments (handling real sensitive data), it's important your organization adheres to role separation when configuring attestation, where each distinct role is assumed by different people. In particular, if the goal of deploying Always Encrypted in your organization is to reduce the attack surface area by ensuring [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer administrators and DBAs can't access sensitive data, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] administrators and DBAs shouldn't control the HGS servers.

## Dev/test environment considerations

If you're using Always Encrypted with VBS enclaves in a development or test environment and don't require high availability or strong protection of the computer running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], you can make any or all of the following concessions for a simplified deployment:

- Deploy only one node of HGS. Even though HGS installs a failover cluster, you don't need to add additional nodes if high availability isn't a concern.
- Use host key mode instead of TPM mode for a simpler setup experience.
- Virtualize HGS and/or the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to save physical resources.
- Run SSMS or other tools for configuring Always Encrypted with secure enclaves on the same computer as [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. This leaves the column master keys on the same computer as [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], so don't do this in a production environment.

## Next steps

- [Deploy the Host Guardian Service for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]](./always-encrypted-enclaves-host-guardian-service-deploy.md)
