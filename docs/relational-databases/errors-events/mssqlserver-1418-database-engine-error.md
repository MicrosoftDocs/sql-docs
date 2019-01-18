---
title: "MSSQLSERVER_1418 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1418 (Database Engine error)"
ms.assetid: 6e9c7241-0201-44e0-9f8b-b3c4e293f0f6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1418
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|1418|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_PARTNERNOTFOUND|  
|Message Text|The server network address "%.*ls" can not be reached or does not exist. Check the network address name and that the ports for the local and remote endpoints are operational.|  
  
## Explanation  
The server network endpoint did not respond because the specified server network address cannot be reached or does not exist.  
  
> [!NOTE]  
> By default, [!INCLUDE[msCoName](../../includes/msconame-md.md)] operating system blocks all ports.  
  
## User Action  
Verify the network address name and reissue the command.  
  
Corrective action might be required on both partners. For example, if this message is raised when you are trying to run SET PARTNER on the principal server instance, the message might imply that you only have to take corrective action on the mirror server instance. However, corrective actions might be required on both partners.  
  
### Additional Corrective Actions  
  
-   Make sure that the mirror database is ready for mirroring.  
  
-   Make sure that the name and port of the mirror server instance are correct.  
  
-   Make sure that the destination mirror server instance is not behind a firewall.  
  
-   Make sure that the principal server instance is not behind a firewall.  
  
-   Verify that the endpoints are started on the partners by using the **state** or **state_desc** column the of the **sys.database_mirroring_endpoints** catalog view. If either endpoint is not started, execute an ALTER ENDPOINT statement to start it.  
  
-   Make sure that the principal server instance is listening on the port assigned to its database mirroring endpoint and that and the mirror server instance is listening on its port. For more information, see "Verifying Port Availability," later in this topic. If a partner is not listening on its assigned port, modify the database mirroring endpoint to listen on a different port.  
  
    > [!IMPORTANT]  
    > Improperly configured security can cause a general setup error message. Typically, the server instance drops the bad connection request without responding. To the caller, a security-configuration error could appear to have occurred for a variety of other reasons, such as the mirror database in a bad state or does not exist, incorrect permissions, and so on.  
  
### Using the Error Log File for Diagnosis  
In some cases, only error log files are available for investigation. In these cases, determine whether the error log contains error message 26023 for the TCP port of the database mirroring endpoint. This error, which is severity 16, might indicate that the database mirroring endpoint is not started. This message can occur even if **sys.database_mirroring_endpoints** shows the endpoint state as started.  
  
After resolving any issues that you encounter, rerun the ALTER DATABASE *database_name* SET PARTNER statement on the principal server.  
  
### Verifying Port Availability  
When you are configuring the network for a database mirroring session, make sure the database mirroring endpoint of each server instance is used by only the database mirroring process. If another process is listening on the port assigned to a database mirroring endpoint, the database mirroring processes of the other server instances cannot connect to the endpoint.  
  
To display all the ports on which a Windows-based server is listening, use the **netstat** command-prompt utility. The syntax for **netstat** depends on the version of the Windows operating system. For more information, see the operating system documentation.  
  
#### Windows Server 2003 Service Pack 1 (SP1)  
To list listening ports and the processes that have those ports opened, enter the following command at the Windows command prompt:  
  
**netstat -abn**  
  
#### Windows Server 2003 (pre-SP1)  
To identify the listening ports and the processes that have those ports opened, follow these steps:  
  
1.  Obtain the process ID.  
  
    To learn the process ID of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], connect to that instance and use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT SERVERPROPERTY('ProcessID')   
    ```  
  
    For more information, see "SERVERPROPERTY (Transact-SQL)" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
2.  Match the process ID with the output of the following **netstat** command:  
  
    **netstat -ano**  
  
## See Also  
[ALTER ENDPOINT &#40;Transact-SQL&#41;](~/t-sql/statements/alter-endpoint-transact-sql.md)  
[The Database Mirroring Endpoint &#40;SQL Server&#41;](~/database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
[Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](~/database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md)  
[SERVERPROPERTY &#40;Transact-SQL&#41;](~/t-sql/functions/serverproperty-transact-sql.md)  
[Specify a Server Network Address &#40;Database Mirroring&#41;](~/database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md)  
[sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)  
[Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](~/database-engine/database-mirroring/troubleshoot-database-mirroring-configuration-sql-server.md)  
  
