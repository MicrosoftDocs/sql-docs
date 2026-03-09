---
title: "Manage Certificates for SQL Server Integration Services Scale Out"
description: This article describes how to manage certificates to secure communications between SSIS Scale Out Master and Scale Out Workers.
author: chugugrace
ms.author: chugu
ms.reviewer: randolphwest
ms.date: 01/23/2025
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
ms.custom:
  - performance
  - sfi-image-nochange
---
# Manage certificates for SQL Server Integration Services Scale Out

[!INCLUDE [sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

To secure the communication between Scale Out Master and Scale Out Workers, SSIS Scale Out uses two certificates: one for the Master and one for the Workers.

## Scale Out Master certificate

In most cases, the Scale Out Master certificate is configured during the installation of Scale Out Master.

In the **Integration Services Scale Out Configuration - Master Node** page of the SQL Server Installation wizard, you can choose to create a new self-signed TLS/SSL certificate or use an existing TLS/SSL certificate.

- **New certificate**. If you don't have special requirements for certificates, you can choose to create a new self-signed TLS/SSL certificate. You can further specify the CNs in the certificate. Make sure the host name of the master endpoint to be used later by Scale Out Workers is included in the CNs. By default, the computer name and IP address of the master node are included.

- **Existing certificate**. If you choose to use an existing certificate, select **Browse** to select a TLS/SSL certificate from the **Root** certificate store of the local computer.

### Change the Scale Out Master certificate

You might want to change your Scale Out Master certificate due to certificate expiration or for other reasons. To change the Scale Out Master certificate, do the following things:

#### 1. Create a TLS/SSL certificate

Create and install a new TLS/SSL certificate on the Master node with the following PowerShell script.

1. Create the certificate in the `LocalMachine\Root` store. Replace `<master-endpoint-host>` with the appropriate value.

   ```powershell
   $newSelfSignedCertificateParams = @{
       Subject = "CN=<master-endpoint-host>"
       HashAlgorithm = "SHA1"
       CertStoreLocation = "Cert:\LocalMachine\Root"
       FriendlyName = "SSISScaleOutMaster"
       KeyExportPolicy = "Exportable"
   }

   $cert = New-SelfSignedCertificate @newSelfSignedCertificateParams
   ```

1. Export the certificate to a `.cer` file. Make sure the folder path for the file exists.

   ```powershell
   $exportCertificateParams = @{
       Cert = $cert
       FilePath = "C:\Path\To\SSISScaleOutMaster.cer"
   }

   Export-Certificate @exportCertificateParams
   ```

#### 2. Bind the certificate to the Master port

Check the original binding with the following command:

```cmd
netsh http show sslcert ipport=0.0.0.0:<master-port>
```

For example:

```cmd
netsh http show sslcert ipport=0.0.0.0:8391
```

Delete the original binding and set up the new binding with the following commands:

```cmd
netsh http delete sslcert ipport=0.0.0.0:<master-port>
netsh http add sslcert ipport=0.0.0.0:<master-port> certhash=<tls-certificate-thumbprint> certstorename=Root appid=<original-appid>
```

For example:

```cmd
netsh http delete sslcert ipport=0.0.0.0:8391
netsh http add sslcert ipport=0.0.0.0:8391 certhash=0011001100110011001100110011001100110011 certstorename=Root appid=<00001111-aaaa-2222-bbbb-3333cccc4444>
```

#### 3. Update the Scale Out Master service configuration file

Update the Scale Out Master service configuration file, `<drive>:\Program Files\Microsoft SQL Server\140\DTS\Binn\MasterSettings.config`, on the Master node. Update **SSLCertThumbprint** to the thumbprint of the new TLS/SSL certificate.

#### 4. Restart the Scale Out Master service

#### 5. Reconnect Scale Out Workers to Scale Out Master

For each Scale Out Worker, either delete the Worker and then add it back with [Scale Out Manager](integration-services-ssis-scale-out-manager.md), or do the following things:

a. Install the client TLS/SSL certificate to the Root store of the local computer on the Worker node.

b. Update the Scale Out Worker service configuration file.

Update the Scale Out Worker service configuration file, `<drive>:\Program Files\Microsoft SQL Server\140\DTS\Binn\WorkerSettings.config`, on the Worker node. Update **MasterHttpsCertThumbprint** to the thumbprint of the new TLS/SSL certificate.

c. Restart the Scale Out Worker service.

## Scale Out Worker certificate

Scale Out Worker certificate is generated automatically during the installation of Scale Out Worker.

### Change the Scale Out Worker certificate

If you want to change Scale Out Worker certificate, perform the following steps.

#### 1. Create a certificate

Create and install a certificate with the following commands.

1. Create the certificate in the `LocalMachine\My` store. Replace `<worker-machine-name>` and `<worker-machine-ip>` as appropriate.

   ```powershell
   $newSelfSignedCertificateParams = @{
       Subject = "CN=<worker-machine-name>"
       DnsName = "<worker-machine-name>", "<worker-machine-ip>"
       CertStoreLocation = "Cert:\LocalMachine\My"
       FriendlyName = "SSISScaleOutWorker"
       KeyExportPolicy = "Exportable"
       HashAlgorithm = "SHA1"
   }
   
   $cert = New-SelfSignedCertificate @newSelfSignedCertificateParams
   ```

1. Export the certificate to a `.cer` file. Make sure the folder path for the file exists.

   ```powershell
   $exportCertificateParams = @{
       Cert     = $cert
       FilePath = "C:\Path\To\SSISScaleOutWorker.cer"
   }
   
   Export-Certificate @exportCertificateParams
   ```

#### 2. Install the client certificate to the Root store of the local computer on the Worker node

#### 3. Grant service access to the certificate

Delete the old certificate and grant Scale Out Worker service access to the new certificate with following command:

```cmd
certmgr.exe /del /c /s /r localmachine My /n <CN of the old certificate>
winhttpcertcfg.exe -g -c LOCAL_MACHINE\My -s <CN of the new certificate> -a <the account running Scale Out Worker service>
```

For example:

```cmd
certmgr.exe /del /c /s /r localmachine My /n WorkerMachine
winhttpcertcfg.exe -g -c LOCAL_MACHINE\My -s WorkerMachine -a SSISScaleOutWorker140
```

#### 4. Update the Scale Out Worker service configuration file

Update the Scale Out Worker service configuration file, `<drive>:\Program Files\Microsoft SQL Server\140\DTS\Binn\WorkerSettings.config`, on the Worker node. Update **WorkerHttpsCertThumbprint** to the thumbprint of the new certificate.

#### 5. Install the client certificate to the Root store of the local computer on the Master node

#### 6. Restart the Scale Out Worker service

## Related content

- [Integration Services (SSIS) Scale Out Master](integration-services-ssis-scale-out-master.md)
- [Integration Services (SSIS) Scale Out Worker](integration-services-ssis-scale-out-worker.md)
