---
title: "Deploy Host Guardian Service"
description: "Deploy the Host Guardian Service for Always Encrypted with Secure Enclaves."
ms.custom:
- intro-deployment
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

# Deploy the Host Guardian Service for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]

[!INCLUDE [sqlserver2019-windows-only](../../../includes/applies-to-version/sqlserver2019-windows-only.md)]

This article describes how to deploy the Host Guardian Service (HGS) as an attestation service for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
Before you start, make sure to read the [Plan for Host Guardian Service attestation](./always-encrypted-enclaves-host-guardian-service-plan.md) article for a full list of prerequisites and architectural guidance.

> [!NOTE]
> The HGS administrator is responsible for executing all steps described in this article. See [Roles and responsibilities when configuring attestation with HGS](always-encrypted-enclaves-host-guardian-service-plan.md#roles-and-responsibilities-when-configuring-attestation-with-hgs).

## Step 1: Set up the first HGS computer

The Host Guardian Service (HGS) runs as a clustered service on one or more computers.
In this step, you'll set up a new HGS cluster on the first computer.
If you already have an HGS cluster and are adding additional computers to it for high availability, skip to [Step 2: Add more HGS computers to the cluster](#step-2-add-more-hgs-computers-to-the-cluster).

Before you start, ensure the computer you're using is running Windows Server 2019 or later - Standard or Datacenter edition, you have local administrator privileges, and the computer isn't already joined to an Active Directory domain.

1. Sign in to the first HGS computer as a local administrator and open an elevated Windows PowerShell console. Run the following command to install the Host Guardian Service role. The computer will automatically restart to apply the changes.

    ```powershell
    Install-WindowsFeature -Name HostGuardianServiceRole -IncludeManagementTools -Restart
    ```

2. After the HGS computer restarts, run the following commands in an elevated Windows PowerShell console to install the new Active Directory forest:

    ```powershell
    # Select the name for your new Active Directory root domain.
    # Make sure the name does not conflict with, and is not subordinate to, any existing domains on your network.
    $HGSDomainName = 'bastion.local'

    # Specify a Directory Services Restore Mode password that can be used to recover your domain in safe mode.
    # This password is not, and will not change, your admin account password.
    # Save this password somewhere safe and include it in your disaster recovery plan.
    $DSRMPassword = Read-Host -AsSecureString -Prompt "Directory Services Restore Mode Password"

    Install-HgsServer -HgsDomainName $HgsDomainName -SafeModeAdministratorPassword $DSRMPassword -Restart
    ```

    Your HGS computer will restart again to finish configuring the Active Directory forest. The next time you log in, your administrator account will be a domain admin account. We recommend reviewing the [Active Directory Domain Services Operations docs](/windows-server/identity/ad-ds/manage/component-updates/ad-ds-operations) for more information about managing and securing your new forest.

3. Next, you'll set up the HGS cluster and install the attestation service by running the following command in an elevated Windows PowerShell console:

    ```powershell
    # Note: the name you provide here will be shared by all HGS nodes and used to point your SQL Server computers to the HGS cluster.
    # For example, if you provide "attsvc" here, a DNS record for "attsvc.yourdomain.com" will be created for every HGS computer.
    Initialize-HgsAttestation -HgsServiceName 'hgs'
    ```

## Step 2: Add more HGS computers to the cluster

Once your first HGS computer and cluster is set up, you can add additional HGS servers to provide high availability.
If you're only setting up one HGS server (in a dev/test environment, for example), you can skip to Step 3.

As with the first HGS computer, ensure the computer you're joining to the cluster is running Windows Server 2019 or later - Standard or Datacenter edition, you have local administrator privileges, and the computer isn't already joined to an Active Directory domain.

1. Sign in to the computer as a local administrator and open an elevated Windows PowerShell console. Run the following command to install the Host Guardian Service role. The computer will restart automatically to apply the changes.

    ```powershell
    Install-WindowsFeature -Name HostGuardianServiceRole -IncludeManagementTools -Restart
    ```

2. Check the DNS client configuration on your computer to ensure it can resolve the HGS domain. The following command should return an IP address for your HGS server. If you can't resolve the HGS domain, you may need to update the DNS server information on your network adapter to use the HGS DNS server for name resolution.

    ```powershell
    # Change 'bastion.local' to the domain name you specified in Step 1.2
    nslookup bastion.local

    # If it fails, use sconfig.exe, option 8, to set the first HGS computer as your preferred DNS server.
    ```

3. After the computer restarts, run the following command in an elevated Windows PowerShell console to join the computer to the Active Directory domain created by the first HGS server. You'll need domain administrator credentials for the AD domain to run this command. When the command completes, the computer will restart and the machine will become an Active Directory domain controller for the HGS domain.

    ```powershell
    # Provide the fully qualified HGS domain name
    $HGSDomainName = 'bastion.local'

    # Provide a domain administrator's credential for the HGS domain
    $DomainAdminCred = Get-Credential

    # Specify a Directory Services Restore Mode password that can be used to recover your domain in safe mode.
    # This password is not, and will not change, your admin account password.
    # Save this password somewhere safe and include it in your disaster recovery plan.
    $DSRMPassword = Read-Host -AsSecureString -Prompt "Directory Services Restore Mode Password"

    Install-HgsServer -HgsDomainName $HgsDomainName -HgsDomainCredential $DomainAdminCred -SafeModeAdministratorPassword $DSRMPassword -Restart
    ```

4. Sign in with domain administrator credentials after the computer restarts. Open an elevated Windows PowerShell console and run the following commands to configure the attestation service. Since the attestation service is cluster-aware, it will replicate its configuration from other cluster members. Changes made to the attestation policies on any HGS node will apply to all other nodes.

    ```powershell
    # Provide the IP address of an existing, initialized HGS server
    # If you are using separate networks for cluster and application traffic, choose an IP address on the cluster network.
    # You can find the IP address of your HGS server by signing in and running "ipconfig /all"
    Initialize-HgsAttestation -HgsServerIPAddress '172.16.10.20'
    ```

5. Repeat Step 2 for every computer you wish to add to your HGS cluster.

## Step 3: Configure a DNS forwarder to your HGS cluster

HGS runs its own DNS server, which contains the name records needed to resolve the attestation service.
Your SQL Server computers won't be able to resolve these records until you configure your network's DNS server to forward requests to the HGS DNS servers.

The process for configuring DNS forwarders is vendor-specific, so we recommend contacting your network administrator for the correct guidance for your particular network.

If you're using the Windows Server DNS Server role for your corporate network, your DNS administrator can create a conditional forwarder to the HGS domain so that only requests for the HGS domain are forwarded.
For example, if your HGS servers use the "bastion.local" domain name and have IP addresses of 172.16.10.20, 172.16.10.21, and 172.16.10.22, you can run the following command on the corporate DNS server to configure a conditional forwarder:

```powershell
# Tip: make sure to provide every HGS server's IP address
# If you use separate NICs for cluster and application traffic, use the application traffic NIC IP addresses here
Add-DnsServerConditionalForwarderZone -Name 'bastion.local' -ReplicationScope "Forest" -MasterServers "172.16.10.20", "172.16.10.21", "172.16.10.22"
```

## Step 4: Configure the attestation service

HGS supports two attestation modes: TPM attestation for cryptographic verification of the integrity and identity of each [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer and Host Key attestation for simple verification of the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer's identity.
If you haven't already selected an attestation mode, check out the attestation information in the [planning guide](./always-encrypted-enclaves-host-guardian-service-plan.md#attestation-modes) for more details about the security assurances and use cases for each mode.

The steps in this section will configure the basic attestation policies for a particular attestation mode.
You'll register host-specific information in Step 4.
If you need to change your attestation mode in the future, repeat Step 3 and 4 using the desired attestation mode.

### Switch to TPM attestation

To configure TPM attestation on HGS, you'll need a computer with Internet access and at least one [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer with a TPM 2.0 rev 1.16 chip.

To configure HGS to use TPM mode, open an elevated PowerShell console and run the following command:

```powershell
Set-HgsServer -TrustTpm
```

All HGS computers in your cluster will now use TPM mode when a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer tries to attest.

Before you can register TPM information from your SQL Server computers with HGS, you need to install the endorsement key (EK) root certificates from your TPM vendor(s).
Each physical TPM is configured in the factory with a unique endorsement key that is accompanied by an endorsement key certificate that identifies the manufacturer.
This certificate ensures that your TPM is genuine.
HGS verifies the endorsement key certificate when you register a new TPM with HGS by comparing the certificate chain with a list of trusted root certificates.

Microsoft publishes a list of known-good TPM vendor root certificates that you can import into the HGS Trusted TPM Root Certificates store.
If your SQL Server computers are virtualized, you'll need to contact your cloud service provider or virtualization platform vendor for information on how to obtain the certificate chain for your virtual TPM's endorsement key.

To download the trusted TPM root certificates package from Microsoft for physical TPMs, complete the following steps:

1. On a computer with internet access, download the latest TPM root certificates package from [https://go.microsoft.com/fwlink/?linkid=2097925](https://go.microsoft.com/fwlink/?linkid=2097925)

2. Verify the signature of the cab file to ensure it's authentic.

    ```powershell
    # Note: replace the path below with the correct one to the file you downloaded in step 1
    Get-AuthenticodeSignature ".\TrustedTpm.cab"
    ```

    > [!WARNING]
    > Do not proceed if the signature is invalid and contact Microsoft support for assistance.

3. Expand the cab file to a new directory.

    ```powershell
    mkdir .\TrustedTpmCertificates
    expand.exe -F:* ".\TrustedTpm.cab" ".\TrustedTpmCertificates"
    ```

4. In the new directory, you'll see directories for each TPM vendor. You can delete the directories for vendors you don't use.

5. Copy the entire "TrustedTpmCertificates" directory to your HGS server.

6. Open an elevated PowerShell console on the HGS server and run the following command to import all the TPM root and intermediate certificates:

    ```powershell
    # Note: replace the path below with the correct location of the TrustedTpmCertificates folder on your HGS computer
    cd "C:\scratch\TrustedTpmCertificates"
    .\setup.cmd
    ```

7. Repeat steps 5 and 6 for every HGS computer.

If you have obtained intermediate and root CA certificates from your OEM, cloud service provider, or virtualization platform vendor, you can directly import the certificates to the respective local machine certificate store: `TrustedHgs_RootCA` or `TrustedHgs_IntermediateCA`. For example, in PowerShell:

```powershell
# Imports MyCustomTpmVendor_Root.cer to the local machine's "TrustedHgs_RootCA" store
Import-Certificate -FilePath ".\MyCustomTpmVendor_Root.cer" -CertStoreLocation "Cert:\LocalMachine\TrustedHgs_RootCA"
```

### Switch to host key attestation

To use host key attestation, run the following command on an HGS server in an elevated PowerShell console:

```powershell
Set-HgsServer -TrustHostKey
```

All HGS computers in your cluster will now use host key mode when a [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computer tries to attest.

## Step 5: Configure the HGS HTTPS binding

In a default installation, HGS only exposes an HTTP (port 80) binding.
You can configure an HTTPS (port 443) binding to encrypt all communications between [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers and HGS.
It's recommended that all production instances of HGS use an HTTPS binding.

1. Obtain a TLS certificate from your certificate authority, using the fully qualified HGS service name from Step 1.3 as the subject name. If you don't know your service name, you can find it by running `Get-HgsServer` on any HGS computer. You can add alternative DNS names to the Subject Alternate Name list if your [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] computers use a different DNS name to reach your HGS cluster (for example, if HGS is behind a network load balancer with a different address).

2. On the HGS computer, use [Set-HgsServer](/powershell/module/hgsserver/set-hgsserver) to enable the HTTPS binding and specify the TLS certificate obtained in the previous step. If your certificate is already installed on the computer in the local certificate store, use the following command to register it with HGS:

    ```powershell
    # Note: you'll need to know the thumbprint for your certificate to configure HGS this way
    Set-HgsServer -Http -Https -HttpsCertificateThumbprint "54A043386555EB5118DB367CFE38776F82F4A181"
    ```

    If you've exported your certificate (with a private key) to a password protected PFX file, you can register it with HGS by running the following command:

    ```powershell
    $PFXPassword = Read-Host -AsSecureString -Prompt "PFX Password"
    Set-HgsServer -Http -Https -HttpsCertificatePath "C:\path\to\hgs_tls.pfx" -HttpsCertificatePassword $PFXPassword
    ```

3. Repeat steps 1 and 2 for each HGS computer in the cluster. TLS certificates aren't automatically replicated between HGS nodes. Additionally, each HGS computer can have its own unique TLS certificate so long as the subject matches the HGS service name.

## Step 6: Determine and share the HGS attestation URL

As the HGS administrator you need share the attestation URL of HGS with both SQL server computer administrators and application administrators in your organization. The SQL Server computers administrators will need the attestation URL to verify SQL Server computers can attest with HGS. Application administrators will need the attestation URL to configure how their apps connect to SQL Server.

To determine the attestation URL, run the following cmdlet.

```powershell
Get-HGSServer
```
The output of the command will look similar to below:

```
Name                           Value                                                                         
----                           -----                                                                         
AttestationOperationMode       HostKey                                                                       
AttestationUrl                 {http://hgs.bastion.local/Attestation}                                        
KeyProtectionUrl               {}         
```

The attestation URL for your HGS is the value of the AttestationUrl property.

## Next steps

- [Register computers with HGS](./always-encrypted-enclaves-host-guardian-service-register.md)
