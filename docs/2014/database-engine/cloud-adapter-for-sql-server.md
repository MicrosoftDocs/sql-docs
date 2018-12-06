---
title: "Cloud Adapter for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Cloud adapter"
  - "Deploy to Windows Azure"
ms.assetid: 82ed0d0f-952d-4d49-aa36-3855a3ca9877
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Cloud Adapter for SQL Server
  The Cloud Adapter service is created as part of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provisioning on a Windows Azure VM. The Cloud Adapter service generates a self-signed SSL certificate as part of its first run, and then runs as a **Local System** account. It generates a configuration file that is used to configure itself. The Cloud Adapter also creates a Windows Firewall rule to allow its incoming TCP connections at default port 11435.  
  
 The Cloud Adapter is a stateless, synchronous service that receives messages from the on-premise instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. When the Cloud Adapter service is stopped, it stops the remote access Cloud Adapter, unbinds the SSL certificate, and disables the Windows Firewall rule.  
  
## Cloud Adapter Requirements  
 Note the following requirements to install, enable, and run the Cloud Adapter for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   Cloud Adapter is supported with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2012 and higher. On [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2012, the Cloud Adapter for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] requires SQL Management Objects for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2012.  
  
-   Cloud Adapter web service runs as a **Local System** account and verifies client credentials before executing any task. Credentials supplied by the client must belong to the use account that is a member of the local **Administrators** group on the remote machine.  
  
-   Cloud Adapter supports only [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication.  
  
-   Cloud Adapter uses VM local administrator account to execute commands on the local machine, not an sa account.  
  
-   Cloud Adapter listens on TCP/IP. The default port is 11435.  
  
-   Cloud Adapter must have permissions to create and modify Windows Firewall rules.  
  
## Cloud Adapter Configuration Settings  
 Use the following Cloud Adapter configuration details to modify settings for a Cloud Adapter.  
  
-   **Default path for the configuration file** - C:\Program Files\Microsoft SQL Server\120\Tools\CloudAdapter\  
  
-   **Configuration file parameters** -  
  
    -   \<configuration>  
  
        -   \<appSettings>  
  
            -   \<add key="WebServicePort" value="" />  
  
            -   \<add key="WebServiceCertificate" value="GUID" />  
  
            -   \<add key="ExposeExceptionDetails" value="true" />  
  
        -   \</appSettings>  
  
    -   \</configuration>  
  
-   **Certificate details** - The certificate has the following values:  
  
    -   Subject - "CN=CloudAdapter\<VMName>, DC=SQL Server, DC=Microsoft"  
  
    -   The certificate should have only Server Authentication EKU enabled.  
  
    -   The certificate key length is 2048.  
  
 **Configuration file values**:  
  
|Setting|Values|Default|Comments|  
|-------------|------------|-------------|--------------|  
|WebServicePort|1-65535|11435|If not specified, use 11435.|  
|WebServiceCertificate|Thumbprint|Empty|If empty, a new self-signed certificate is generated.|  
|ExposeExceptionDetails|True/False|False||  
  
## Cloud Adapter Troubleshooting  
 Use the following information to troubleshoot the Cloud Adapter for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   **Error handling and logging** - Errors and status messages are written to the Application Event Log.  
  
-   **Tracing, Events** - All events are written to the Application Event Log.  
  
-   **Control, configuration** - Use the configuration file located in:  C:\Program Files\Microsoft SQL Server\120\Tools\CloudAdapter\\.  
  
|Error|Error ID|Cause|Resolution|  
|-----------|--------------|-----------|----------------|  
|There was an exception while adding the certificate to the certificate store. {Exception text}.|45560|Machine certificate store permissions|Ensure that the Cloud Adapter service has permissions to add certificates to the machine certificate store.|  
|There was an exception while trying to configure the SSL binding for port {Port number} and certificate {Thumbprint}. {Exception}.|45561|Another application has already used the port or bound a certificate to it.|Remove existing bindings or change Cloud Adapter port in the configuration file.|  
|Failed to find SSL certificate [{Thumbprint}] in the certificate store.|45564|Certificate thumbprint is in the configuration file, but personal certificate store for the service does not contain certificate.<br /><br /> Insufficient permissions.|Make sure the certificate is in the personal certificate store for the service.<br /><br /> Make sure the service has correct permissions for the store.|  
|Failed to start the web service. {Exception text}.|45570|Described in the exception.|Enable ExposeExceptionDetails and use extended information from the exception.|  
|Certificate [{Thumbprint}] has expired.|45565|Expired certificate referenced from the configuration file.|Add a valid certificate and update the configuration file with its thumbprint.|  
|Web service error: {0}.|45571|Described in the exception.|Enable ExposeExceptionDetails and use extended information from the exception.|  
  
## See Also  
 [Deploy a SQL Server Database to a Microsoft Azure Virtual Machine](../relational-databases/databases/deploy-a-sql-server-database-to-a-microsoft-azure-virtual-machine.md)  
  
  
