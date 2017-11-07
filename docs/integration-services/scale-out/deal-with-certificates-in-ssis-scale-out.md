---
title: "Deal with certificates in Sql Server Integration Services Scale Out | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 1
author: "haoqian"
ms.author: "haoqian"
manager: "jhubbard"
---
# Deal with certificates in SQL Server Integration Services Scale Out

To secure the communication between Scale Out Master and Scale Out Worker, two certificates are used in Scale Out, i.e. Scale Out Master certificate and Scale Out Worker certificate. 

## Scale Out Master certificate

In most cases, Scale Out Master certificate is configured during the installation of Scale Out Master.

In the **Integration Services Scale Out Configuration - Master Node** page of SQL Server Installation wizard, you can choose to create a new self-signed SSL certificate or use an existing SSL certificate.

![Master Config](media/master-config.PNG)

If you don't have special requirements on certificates, you can choose to create a new self-signed SSL certificate. You can further specify the CNs in the certificate. Make sure the host name of the master endpoint used by Scale Out Worker later is included in the CNs. By default, the machine name and ip of master node are included. 

If you choose to use an existing certificate, click "Browse..." to select a SSL certificate from **Root** certificate store of local computer.

### Change Scale Out Master certificate

You may want to change your Scale Out Master certificate due to certificate expiration or other reasons. Follow the steps below:

#### 1. Create a SSL certificate.
Create and install a SSL certificate on Master node with the following command:
```dos
MakeCert.exe -n CN={master endpoint host} SSISScaleOutMaster.cer -r -ss Root -sr LocalMachine
```
Example:
```dos
MakeCert.exe -n CN=MasterMachine SSISScaleOutMaster.cer -r -ss Root -sr LocalMachine
```

#### 2. Bind the certificate to Master port
Check the original binding with the command:
```dos
netsh http show sslcert ipport=0.0.0.0:{Master port}
```
Example:
```dos
netsh http show sslcert ipport=0.0.0.0:8391
```
Delete the original binding and set up the new binding with the following commands:
```dos
netsh http delete sslcert ipport=0.0.0.0:{Master port}
netsh http add sslcert ipport=0.0.0.0:{Master port} certhash={SSL Certificate Thumbprint} certstorename=Root appid={original appid}
```
Example:
```dos
netsh http delete sslcert ipport=0.0.0.0:8391
netsh http add sslcert ipport=0.0.0.0:8391 certhash=01d207b300ca662f479beb884efe6ce328f77d53 certstorename=Root appid={a1f96506-93e0-4c91-9171-05a2f6739e13}
```
#### 3. Update Scale Out Master service configuration file
Update Scale Out Master service configuration file, \<driver\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\MasterSettings.config, on Master node. Update **SSLCertThumbprint** to the thumbprint of the new SSL certificate.

#### 4. Restart Scale Out Master service

#### 5. Reconnect Scale Out Worker to Scale Out Master
For each Scale Out Worker, either delete the Worker and re-add it with [Scale Out Manager](integration-services-ssis-scale-out-manager.md) or follow the steps 5.1 to 5.3 below:

5.1 Install the client SSL certificate to the Root store of local machine on the Worker node

5.2 Update Scale Out Worker service configuration file
Update  Scale Out Worker service configuration file, \<driver\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\WorkerSettings.config, on Worker node. Update **MasterHttpsCertThumbprint** to the thumbprint of the new SSL certificate.

5.3 Restart Scale Out Worker service


## Scale Out Worker certificate

Scale Out Worker certificate is generated automatically during the installation of Scale Out Worker. 

### Change Scale Out Worker certificate

In cases you want to change Scale Out Worker certificate, follow the steps below.

#### 1. Create a certificate
Create and install a certificate with the following command:
```dos
MakeCert.exe -n CN={worker machine name};CN={worker machine ip} SSISScaleOutWorker.cer -r -ss My -sr LocalMachine
```
Example:
```dos
MakeCert.exe -n CN=WorkerMachine;CN=10.0.2.8 SSISScaleOutWorker.cer -r -ss My -sr LocalMachine
```
#### 2. Install the client certificate to the Root Store of local machine on Worker Node

#### 3. Give service access to the certificate
Delete the old certificate and give Scale Out Worker service access to the new certificate with command:
```dos
certmgr.exe /del /c /s /r localmachine My /n {CN of the old certificate}
winhttpcertcfg.exe -g -c LOCAL_MACHINE\My -s {CN of the new certificate} -a {the account running Scale Out Worker service}
```
Example:
```dos
certmgr.exe /del /c /s /r localmachine My /n WorkerMachine
winhttpcertcfg.exe -g -c LOCAL_MACHINE\My -s WorkerMachine -a SSISScaleOutWorker140
```
#### 4. Update Scale Out Worker configuration file
Update  Scale Out Worker service configuration file, \<driver\>:\Program Files\Microsoft SQL Server\140\DTS\Binn\WorkerSettings.config, on Worker node. Update **WorkerHttpsCertThumbprint** to the thumbprint of the new certificate.

#### 5. Install the client certificate to the Root store of local machine on Master node

#### 6. Restart Scale Out Worker service