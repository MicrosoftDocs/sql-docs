---
title: "Troubleshooting SQL Server Integration Services (SSIS) Scale Out | Microsoft Docs"
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
# Troubleshooting Scale Out

SSIS Scale Out involves communtication among SSISDB, Scale Out Master service and Scale Out Worker service. Sometimes, the communication is broken due to configuration mistakes, lack of access permissions and other reasons. This document helps you to troubleshoot your Scale Out configuration.

To investigate the symptoms you encounter, follow the steps below one by one until your problem is resolved.

### **Symptoms** 
Scale Out Master cannot connect to SSISDB. 

Master properties cannot show in Scale Out Manager.

Master properties are not filled in [SSISDB].[catalog].[master_properties]

### **Solution**
Step 1: Check if Scale Out is enabled.

Right-click **SSISDB** node in the object explorer of SSMS and check **Scale Out feature is enabled**.

![Is Scale Out enabled](media\isenabled.PNG)

If the property value is False, enable Scale Out by calling stored procedure [SSISDB].[catalog].[enable_scaleout].

Step 2: Check if Sql Server name specified in Scale Out Master configuration file is correct and restart Scale Out Master service.

### **Symptoms** 
Scale Out Worker cannot connect to Scale Out Master

Scale Out Worker does not show after adding it in Scale Out Manager

Scale Out Worker does not show in [SSISDB].[catalog].[worker_agents]

Scale Out Worker service is running, while Scale Out Worker is offline

### **Solutions** 
Check the error messages in Scale Out Worker service log under \<driver\>:\Users\\*[account running worker service]*\AppData\Local\SSIS\Cluster\Agent.

**Case** 

System.ServiceModel.EndpointNotFoundException: There was no endpoint listening at https://*[MachineName]:[Port]*/ClusterManagement/ that could accept the message.

Step 1: Check if the port number specified in Scale Out Master service configuration file is correct and restart Scale Out Master service. 

Step 2: Check if the master endpoint specified in Scale Out Worker service configuration is correct and restart Scale Out Worker service.

Step 3: Check if firewall port is open on Scale Out Master node.

Step 4: Resolve any other connection issues between Scale Out Master node and Scale Out Worker node.

**Case**

System.ServiceModel.Security.SecurityNegotiationException: Could not establish trust relationship for the SSL/TLS secure channel with authority '*[Machine Name]:[Port]*'. ---> System.Net.WebException: The underlying connection was closed: Could not establish trust relationship for the SSL/TLS secure channel. ---> System.Security.Authentication.AuthenticationException: The remote certificate is invalid according to the validation procedure.

Step 1: Install Scale Out Master certificate to Root certificate store of local machine on Scale Out Worker node if not installed yet and restart Scale Out Worker service.

Step 2: Check if the host name in master endpoint is included in the CNs of Scale Out Master certificate. If not, reset the master endpoint in Scale Out Worker configuration file and restart Scale Out Worker service. 

> [!Note]
> If it is not possible to change the host name of master endpoint due to DNS settings, you have to change the Scale Out Master certificate. See [Deal with certificates in SSIS Scale Out](deal-with-certificates-in-ssis-scale-out.md).

Step 3: Check if the master thumbprint specified in Scale Out Worker configuration matches the thumbprint of Scale Out Master certificate. 

**Case**

System.ServiceModel.Security.SecurityNegotiationException: Could not establish secure channel for SSL/TLS with authority '*[Machine Name]:[Port]*'. ---> System.Net.WebException: The request was aborted: Could not create SSL/TLS secure channel.

Step 1: Check if the account running Scale Out Worker service has access to Scale Out Worker certificate by the command below.

```dos
winhttpcertcfg.exe -l -c LOCAL_MACHINE\MY -s {CN of the worker certificate}
```

If the account does not have access, grant by the command below and restart Scale Out Worker service.

```dos
winhttpcertcfg.exe -g -c LOCAL_MACHINE\My -s {CN of the worker certificate} -a {the account running Scale Out Worker service}
```

**Case**

System.ServiceModel.Security.MessageSecurityException: The HTTP request was forbidden with client authentication scheme 'Anonymous'. ---> System.Net.WebException: The remote server returned an error: (403) Forbidden.

Step 1: Install Scale Out Worker certificate to Root certificate store of local machine on Scale Out Master node if not installed yet and restart Scale Out Worker service.

Step 2: Clean up useless certificates in the Root certificate store of local machine on Scale Out Master node.

Step 3: Configure Schannel to no longer send the list of trusted root certification authorities during the TLS/SSL handshake process, by adding the registry entry below on Scale Out Master node.

HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL

Value name: SendTrustedIssuerList 

Value type: REG_DWORD 

Value data: 0 (False)

**Case**

System.ServiceModel.CommunicationException: An error occurred while making the HTTP request to https://*[Machine Name]:[Port]*/ClusterManagement/. This could be due to the fact that the server certificate is not configured properly with HTTP.SYS in the HTTPS case. This could also be caused by a mismatch of the security binding between the client and the server. 

Step 1: Check if Scale Out Master certificate is bound to the port in master endpoint correctly on master node with the command below. Check if the certificate hash displayed is matched with Scale Out Master certificate thumbprint.

```dos
netsh http show sslcert ipport=0.0.0.0:{Master port}
```

If the binding is not correct, reset it with following commands and restart Scale Out Worker service.

```dos
netsh http delete sslcert ipport=0.0.0.0:{Master port}
netsh http add sslcert ipport=0.0.0.0:{Master port} certhash={Master certificate thumbprint} certstorename=Root appid={random guid}
```

### **Symptoms**
Execution in Scale Out does not start.

### **Solution**

Check the status of the machines you selected to run the package in [SSISDB].[catalog].[worker_agents]. At least one worker must be online and enabled.

### **Symptoms** 
Packages run successfully, but there is no message logged.

### **Solution**

Check if SQL Server Authentication is allowed by the Sql Server hosting SSISDB.

> [!Note]  
> If you have changed the account for Scale Out logging, see [Change the Account for Scale Out Logging](change-logdb-account.md) and verify the connection string used for logging.

### **Symptoms**
The error messages in package execution report are not enough for troubleshooting.

### **Solution**
More execution logs can be found under TasksRootFolder configured in WorkerSettings.config. By default, it is \<driver\>:\Users\\*[account]*\AppData\Local\SSIS\ScaleOut\Tasks. The *[account]* is the account running Scale Out Worker service with default value SSISScaleOutWorker140.

To locate the log for the package execution with *[execution id]*, execute the T-SQL command below to get the *[task id]*. Then, find the subfolder named with *[task id]* under TasksRootFolder.<sup>1<sup>

```sql
SELECT [TaskId]
FROM [SSISDB].[internal].[tasks] tasks, [SSISDB].[internal].[executions] executions 
WHERE executions.execution_id = *Your Execution Id* AND tasks.JobId = executions.job_id
```
<sup>1</sup> This query is for troubleshooting purpose only and open to change when the logging/diagnostic scenario for Scale Out Worker is improved in the future. 