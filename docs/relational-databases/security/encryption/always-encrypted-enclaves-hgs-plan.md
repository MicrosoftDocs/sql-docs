---
title: "Plan for Host Guardian Service attestation | Microsoft Docs"
ms.custom: ""
ms.date: "10/12/2019"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
author: rpsqrd
ms.author: ryanpu
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Plan for Host Guardian Service attestation

When you run SQL Server Always Encrypted with VBS Enclaves, you want to make sure that the client is talking to a trustworthy SQL Server instance.
Remote attestation achieves this goal by introducing a third party that can validate the identity (and optionally, the configuration) of the SQL Server.
Before SQL Server Always Encrypted can run an encrypted query, it must provide information to the attestation service about its operating environment to obtain a certificate of health.
This certificate of health is then sent to the client, which can independently verify its authenticity with the attestation service.
Once the client trusts the certificate of health, it knows it is talking to a trustworthy SQL Server instance and will issue the encrypted query to a VBS enclave running on that server.

The Host Guardian Service (HGS) role in Windows Server 2019 provides remote attestation capabilities for SQL Server Always Encrypted with VBS Enclaves.
This article will guide you through the pre-deployment decisions and requirements to use SQL Server Always Encrypted with VBS Enclaves and HGS attestation.

## Architecture overview

The Host Guardian Service (HGS) is a clustered web service that runs on Windows Server 2019.
In a typical deployment, there will be 1-3 HGS servers, at least 1 computer running SQL Server Always Encrypted with VBS Enclaves, and a computer running a client application such as SQL Server Management Studio.
Since the HGS is responsible for determining which machines running SQL Server are trustworthy, it requires both physical and logical isolation from the SQL Server it is protecting.
If the same admins have access to HGS and a malicious server, they could configure the attestation service to allow a malicious server to run SQL Server Always Encrypted with VBS Enclaves.

<< SEE SLIDE 1 FOR DIAGRAM -- WOULD BE NICE TO GET A PROPER ONE CREATED >>

### HGS Domain

HGS setup will automatically create a new Active Directory domain for the HGS servers, failover cluster resources, and administrator accounts.
If you already have an isolated domain in your enterprise (including, but not limited to, a bastion forest, high risk environment, or Enhanced Security Administrative Environment), you can join HGS to that domain instead of creating a new domain.

The computer running SQL Server does not need to be joined to a domain, but if it is, it should be a different domain than the one the HGS server uses.

### High availability

The HGS feature automatically installs and configures a failover cluster.
In a production environment it is recommended to use 3 HGS servers for high availability.
Refer to the [failover cluster documentation](https://docs.microsoft.com/en-us/windows-server/failover-clustering/manage-cluster-quorum) for details on how cluster quorum is determined and alternative configurations, including 2 node clusters with an external witness.

Shared storage is not required between the HGS nodes -- a copy of the attestation database is stored on each HGS server and is automatically replicated by the cluster service.

### Network connectivity

Both the SQL client and SQL Server will need to be able to communicate with HGS over HTTP.
You can optionally configure HGS with a TLS certificate to support encrypted HTTPS connections.

HGS servers require connectivity between each node in the cluster to ensure the attestation service database stays in sync.
It is a failover cluster best practice to connect the HGS nodes on one network for cluster communication and use a separate network for other clients to communicate with HGS.

### Attestation modes

When a computer running SQL Server Always Encrypted with VBS Enclaves tries to attest with HGS, it will first ask HGS how it should attest.
HGS supports 2 attestation modes for use with SQL Server:

| Attestation mode | Explanation |
| ---------------- | ------- |
| TPM | TPM attestation provides the strongest assurance about the identity and integrity of the computer attesting with HGS. It requires the computers running SQL Server to have a Trusted Platform Module (TPM) version 2.0 installed. Each TPM chip contains a unique and immutable identity (Endorsement Key) that can be used to identify a particular machine. TPMs also measure the boot process of the computer, storing hashes of security-sensitive measurements in Platform Control Registers (PCRs) that can be read, but not modified by, the operating system. These measurements are used during attestation to provide cryptographic proof that a machine is in the security configuration it claims to be. |
| Host Key | Host key attestation is a simpler form of attestation that only verifies the identity of a machine by using an asymmetric key pair. The private key is stored on the computer running SQL Server and the public key is provided to HGS. The security configuration of the machine is not measured and a TPM 2.0 chip is not required on the computer running SQL Server. Host key attestation is only as strong as the protection of the private key on the computer running SQL Server. Anyone who obtains this key can impersonate a legitimate SQL Server. |

### Trust model

In the VBS enclave trust model, the encrypted queries and data are evaluated in a software-based enclave to protect it from the host OS.
Access to this enclave is protected by the hypervisor in the same way two virtual machines running on the same computer cannot access each other's memory.
In order for a client to trust that it is talking to a legitimate instance of VBS, you must use TPM-based attestation which establishes a hardware root of trust on the SQL Server.
The TPM measurements captured during the boot process capture the unique identity key of the VBS instance, ensuring that the certificate of health is only valid on that exact machine.
Further, when a TPM is available on a machine running VBS, the private part of the identity key is protected by the TPM, preventing anyone from trying to impersonate that VBS instance.

Secure Boot is required with TPM attestation to ensure UEFI loaded a legitimate, Microsoft-signed bootloader and that no rootkits or bootkits intercepted the hypervisor boot process.
Additionally, an IOMMU device is required by default to ensure any hardware devices with direct memory access cannot inspect or modify the enclave memory.

These protections all assume that the computer running SQL Server is a physical machine.
If you virtualize the computer running SQL Server, you can no longer guarantee that the memory of the VM is safe from inspection by the hypervisor or hypervisor admin.
A hypervisor admin could, for example, dump the memory of the VM and gain access to the plaintext version of the query and data in the enclave.
Similarly, even if the VM has a virtual TPM, it can only measure the state and integrity of the VM operating system and boot environment.
It cannot measure the state of the hypervisor controlling the VM.

However, even when SQL Server is virtualized, the enclave is still protected from attacks originating within the VM operating system.
If you trust your hypervisor or cloud provider and are primarily worried about database admin and OS admin attacks on sensitive data, a virtualized SQL Server running Always Encrypted with VBS Enclaves may meet your requirements.

Similarly, Host Key attestation is still valuable in situations where a TPM 2.0 module is not installed on the computer running SQL Server or in dev/test scenarios where security is not paramount.
You can still use many of the security features mentioned above, including Secure Boot and a TPM 1.2 module, to better protect VBS and the operating system as a whole.
However, since there is no way for HGS to verify that the computer actually has these settings enabled with Host Key attestation, the client is not assured that the host is indeed using all available protections.

## Prerequisites

### HGS Server prerequisites

The computer(s) running the Host Guardian Service role should meet the following requirements:

| Component | Requirement |
| --------- | ----------- |
| Operating System | Windows Server 2019 Standard or Datacenter edition |
| CPU | 2 cores (min), 4 cores (recommended) |
| RAM | 8 GB (min) |
| NICs | 2 NICs recommended (1 for cluster traffic, 1 for HGS service) |

HGS is a CPU-bound role due to the number of actions that require encryption and decryption.
Using modern processors with cryptographic acceleration capabilities will improve HGS performance.
Storage requirements for attestation data are minimal, on the range of 10KB to 1MB per unique computer attesting.

The HGS computer(s) should not be joined to a domain before you start.

### SQL Server computer prerequisites

The computer(s) running SQL Server Always Encrypted with VBS Enclaves must meet both the [SQL Server 2019 hardware requirements](NEEDSALINK) and the [Hyper-V hardware requirements](https://docs.microsoft.com/virtualization/hyper-v-on-windows/reference/hyper-v-requirements#hardware-requirements).

These requirements include:

- SQL Server 2019 (15.x) or later
- Windows 10 Enterprise version 1809 or later; or Windows Server 2019 Datacenter edition. Other editions of Windows 10 and Windows Server do not support attestation with HGS.
- CPU support for virtualization technologies:
  - Intel VT-x with Extended Page Tables
  - AMD-V with Rapid Virtualization Indexing
  - If you're running SQL Server in a VM, the hypervisor and physical CPU must offer nested virtualization capabilities. See the [trust model](#trust-model) section for information on the assurances when running VBS enclaves in a VM.
    - On Hyper-V 2016 or later, [enable nested virtualization extensions on the VM processor](https://docs.microsoft.com/virtualization/hyper-v-on-windows/user-guide/nested-virtualization#configure-nested-virtualization).
    - In Azure, select a VM size that supports nested virtualization. This includes all v3 series VMs, for example Dv3 and Ev3. See [Create a nesting capable Azure VM](https://docs.microsoft.com/azure/virtual-machines/windows/nested-virtualization#create-a-nesting-capable-azure-vm).
    - On VMWare vSphere 6.7 or later, enable Virtualization Based Security support for the VM as described in the [VMware documentation](https://docs.vmware.com/en/VMware-vSphere/6.7/com.vmware.vsphere.vm_admin.doc/GUID-C2E78F3E-9DE2-44DB-9B0A-11440800AADD.html).
    - Other hypervisors and public clouds may support nested virtualization capabilities that enable Always Encrypted with VBS Enclaves as well. Check your virtualization solution's documentation for compatibility and configuration instructions.
- If you plan to use TPM attestation, you will need a TPM 2.0 rev 1.16 chip ready for use in the server. At this time, HGS attestation does not work with TPM 2.0 rev 1.38 chips. Additionally, the TPM must have a valid Endorsement Key Certificate.
- [SQL Server Management Studio (SSMS) 18.0 or later](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15). As an alternative, you can install SSMS on another machine.

> [!WARNING]
> In production environments, you should never use SSMS or other tools to manage Always Encrypted keys or run queries on encrypted data on the SQL Server computer, as this may reduce or completely defeat the purpose of using Always Encrypted. See [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md) for details.

## Dev/Test Environment Considerations

If you are using SQL Server Always Encrypted with VBS Enclaves in a development or test environment and do not require high availability or strong protection of the computer running SQL Server, you can make any or all of the following concessions for a simplified deployment:

- Deploy only one node of HGS. Even though HGS installs a failover cluster, you do not need to add additional nodes if high availability is not a concern.
- Join HGS to an existing domain, even if it is the same domain where SQL Server is installed. Managing 2 domains can add extra complexity not required in a dev or test environment.
- Virtualize HGS and/or the SQL Server to save physical resources.
- Run SSMS on the same machine as SQL Server. This will leave the column master keys on the same machine as SQL Server, so don't do this in a production environment.

## Next steps

- [Deploy HGS for Always Encrypted with VBS Enclaves](always-encrypted-hgs-deploy.md)
