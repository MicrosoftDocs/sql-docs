---
title: "Plan for Always Encrypted with secure enclaves in SQL Server without attestation"
description: "Plan for Always Encrypted with secure enclaves in SQL Server without attestation."
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: "02/01/2023"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
monikerRange: ">= sql-server-ver15"
---

# Plan for Always Encrypted with secure enclaves in SQL Server without attestation

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

Setting up [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] without attestation provides an easy way to start with the feature. However, when you use secure enclaves in a production environment, keep in mind the level of protection against OS administrators is reduced without attestation. For example, if a malicious OS admin tampered with the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] library running inside the enclave, a client application would be unable to detect it. If you're concerned about such attacks, consider setting up attestation with Host Guardian Service. For more information, see [Plan for Host Guardian Service attestation](always-encrypted-enclaves-host-guardian-service-plan.md).

In [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], Always Encrypted with secure enclaves uses [virtualization-based Security (VBS) enclaves](https://www.microsoft.com/security/blog/2018/06/05/virtualization-based-security-vbs-memory-enclaves-data-protection-through-isolation/) (also known as Virtual Secure Mode, or VSM enclaves) - a software-based technology that relies on Windows hypervisor and doesn't require any special hardware.

> [!NOTE]
> When [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is deployed in a VM, VBS enclaves help protect your data from attacks inside the VM. However, they do not provide any protection from attacks using privileged system accounts originating from the host. For example, a memory dump of the VM generated on the host machine may contain the memory of the enclave.

## Prerequisites

The computer(s) running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] must meet both the [Requirements for Installing SQL Server](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md) and the [Hyper-V hardware requirements](/virtualization/hyper-v-on-windows/reference/hyper-v-requirements#hardware-requirements).

These requirements include:

- [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] or later
- Windows 10 or later or Windows Server 2019 or later.
- CPU support for virtualization technologies:

  - Intel VT-x with Extended Page Tables.
  - AMD-V with Rapid Virtualization Indexing.
  - If you're running [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] in a VM:
    - In Azure, use a [Generation 2 VM size](/azure/virtual-machines/generation-2#generation-2-vm-sizes) (recommended) or use a Generation 1 VM size with nested virtualization enabled. Check the [individual VM sizes documentation](/azure/virtual-machines/sizes) to determine which Generation 1 VM sizes support nested virtualization.
    - On Hyper-V 2016 or later (outside of Azure), make sure your VM is a Generation 2 VM (recommended) or that it's a Generation 1 VM with nested virtualization enabled. For more information, see [Should I create a generation 1 or 2 virtual machine in Hyper-V?](/windows-server/virtualization/hyper-v/plan/should-i-create-a-generation-1-or-2-virtual-machine-in-hyper-v) and [Configure nested virtualization](/virtualization/hyper-v-on-windows/user-guide/nested-virtualization#configure-nested-virtualization).
    - On VMware vSphere 6.7 or later, enable virtualization-based security support for the VM as described in the [VMware documentation](https://docs.vmware.com/en/VMware-vSphere/6.7/com.vmware.vsphere.vm_admin.doc/GUID-C2E78F3E-9DE2-44DB-9B0A-11440800AADD.html).
    - Other hypervisors and public clouds may support nested virtualization capabilities that enable Always Encrypted with VBS Enclaves as well. Check your virtualization solution's documentation for compatibility and configuration instructions.

- Virtualization-based security (VBS) must be enabled and running.

### Tooling requirements

- [SQL Server Management Studio (SSMS) 19 or later](../../../ssms/download-sql-server-management-studio-ssms.md).

### Client driver requirements

For information about client driver versions that support using secure enclaves without attestation, see [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md).

## Verify VBS is running

> [!NOTE]
> This step should be performed by the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer administrator.

To check if VBS is running, open the System Information tool by running `msinfo32.exe` and find the `Virtualization-based security` items towards the bottom of the System Summary.

![Screenshot of System Information showing virtualization-based security status and configuration.](./media/always-encrypted-enclaves/msinfo32-vbs-status.png)

The first item to check is `Virtualization-based security`, which can have the following three values:

- `Running` means VBS is configured correctly and was able to start successfully.
- `Enabled but not running` means VBS is configured to run, but the hardware doesn't have the minimum security requirements to run VBS. You may need to change the configuration of the hardware in BIOS or UEFI to enable optional processor features like an IOMMU or, if the hardware truly doesn't support the required features, you may need to lower the VBS security requirements. Continue reading this section to learn more.
- `Not enabled` means VBS isn't configured to run.

## Enable VBS

If VBS isn't enabled, run the following command in an elevated PowerShell console to enable it.

```powershell
Set-ItemProperty -Path HKLM:\SYSTEM\CurrentControlSet\Control\DeviceGuard -Name EnableVirtualizationBasedSecurity -Value 1
```

After changing the registry, restart the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer and check if VBS is running again.

For other ways to enable VBS, see [Enable virtualization-based protection of code integrity](/windows/security/threat-protection/device-guard/enable-virtualization-based-protection-of-code-integrity).

## Run VBS if it's not running

If VBS is enabled by not running on the computer, check `Virtualization-based security` properties. Compare the values in the `Required Security Properties` item to the values in the `Available Security Properties` item. The required properties must be equal to or a subset of the available security properties for VBS to run. The security properties have the following importance:

- `Base virtualization support` is always required, as it represents the minimum hardware features needed to run a hypervisor.
- `Secure Boot` is recommended but not required. Secure Boot protects against rootkits by requiring a Microsoft-signed bootloader to run immediately after UEFI initialization completes.
- `DMA Protection` is recommended but not required. DMA protection uses an IOMMU to protect VBS and enclave memory from direct memory access attacks. In a production environment, you should always use computers with DMA protection. In a dev or test environment, it's okay to remove the requirement for DMA protection. If the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance is virtualized, you'll most likely not have DMA protection available and will need to remove the requirement for VBS to run.

Before lowering the VBS required security features, check with your OEM or cloud service provider to confirm if there's a way to enable the missing platform requirements in UEFI or BIOS (for example, enabling Secure Boot, Intel VT-d or AMD IOV).

To change the required platform security features for VBS, run the following command in an elevated PowerShell console:

```powershell
# Value 1 = Only Secure Boot is required
# Value 2 = Only DMA protection is required (default configuration)
# Value 3 = Both Secure Boot and DMA protection are required
Set-ItemProperty -Path HKLM:\SYSTEM\CurrentControlSet\Control\DeviceGuard -Name RequirePlatformSecurityFeatures -Value 1
```

After changing the registry, restart the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer and check if VBS is running again.

If the computer is managed by your company, Group Policy or Microsoft Endpoint Manager may override any changes you make to these registry keys after restarting. Contact your IT help desk to see if they deploy policies that manage your VBS configuration.

## Next steps

- Once you've made sure your environment meets the above prerequisites, see [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md).
