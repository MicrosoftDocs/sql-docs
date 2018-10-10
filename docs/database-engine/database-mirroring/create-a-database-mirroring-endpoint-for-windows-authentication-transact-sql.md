---
title: "Create a Database Mirroring Endpoint for Windows Authentication (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], deployment"
  - "database mirroring [SQL Server], endpoint"
  - "endpoints [SQL Server], database mirroring"
  - "Windows authentication [SQL Server]"
  - "database mirroring [SQL Server], security"
ms.assetid: baf1a4b1-6790-4275-b261-490bca33bdb9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create a Database Mirroring Endpoint for Windows Authentication (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to create a database mirroring endpoint that uses Windows Authentication in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. To support database mirroring or [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires a database mirroring endpoint. A server instance can have only one database mirroring endpoint, which has a single port. A database mirroring endpoint can use any port that is available on the local system when the endpoint is created. All database mirroring sessions on a server instance listen on that port, and all incoming connections for database mirroring use that port.  
  
> [!IMPORTANT]  
>  If a database mirroring endpoint exists and is already in use, we recommend that you use that endpoint. Dropping an in-use endpoint disrupts existing sessions.  
  
 **In This Topic**  
  
-   **Before you begin:**  [Security](#Security)  
  
-   **To create a database mirroring endpoint, using:**  [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 The authentication and encryption methods of the server instance are established by the system administrator.  
  
> [!IMPORTANT]  
>  The RC4 algorithm is deprecated. [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] We recommend that you use AES.  
  
####  <a name="Permissions"></a> Permissions  
 Requires CREATE ENDPOINT permission, or membership in the sysadmin fixed server role. For more information, see [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To Create a Database Mirroring Endpoint That Uses Windows Authentication  
  
1.  Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on which you want to create a database mirroring endpoint.  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Determine if a database mirroring endpoint already exists by using the following statement:  
  
    ```  
    SELECT name, role_desc, state_desc FROM sys.database_mirroring_endpoints;   
    ```  
  
    > [!IMPORTANT]  
    >  If a database mirroring endpoint already exists for the server instance, use that endpoint for any other sessions you establish on the server instance.  
  
4.  To use Transact-SQL to create an endpoint to use with Windows Authentication, use a CREATE ENDPOINT statement. The statement takes the following general form:  
  
     CREATE ENDPOINT *\<endpointName>*  
  
     STATE=STARTED  
  
     AS TCP ( LISTENER_PORT = *\<listenerPortList>* )  
  
     FOR DATABASE_MIRRORING  
  
     (  
  
     [ AUTHENTICATION = **WINDOWS** [ *\<authorizationMethod>* ]  
  
     ]  
  
     [ [**,**] ENCRYPTION = **REQUIRED**  
  
     [ ALGORITHM { *\<algorithm>* } ]  
  
     ]  
  
     [**,**] ROLE = *\<role>*  
  
     )  
  
     where  
  
    -   *\<endpointName>* is a unique name for the database mirroring endpoint of the server instance.  
  
    -   STARTED specifies that the endpoint is to be started and to begin listening for connections. A database mirroring endpoint typically is created in the STARTED state. Alternatively, you can start a session in a STOPPED state (the default) or DISABLED state.  
  
    -   *\<listenerPortList>* is a single port number (*nnnn*) on which you want the server to listen for database mirroring messages. Only TCP is allowed; specifying any other protocol causes an error.  
  
         A port number can be used only once per computer system. A database mirroring endpoint can use any port that is available on the local system when the endpoint is created. To identify the ports currently being used by TCP endpoints on the system, use the following Transact-SQL statement:  
  
        ```  
        SELECT name, port FROM sys.tcp_endpoints;  
        ```  
  
        > [!IMPORTANT]  
        >  Each server instance requires one and only one unique listener port.  
  
    -   For Windows Authentication, the AUTHENTICATION option is optional, unless you want the endpoint to use only NTLM or Kerberos to authenticate connections. *\<authorizationMethod>* specifies the method used to authenticate connections as one of the following: NTLM, KERBEROS, or NEGOTIATE. The default, NEGOTIATE, causes the endpoint to use the Windows negotiation protocol to choose either NTLM or Kerberos. Negotiation enables connections with or without authentication, depending on the authentication level of the opposite endpoint.  
  
    -   ENCRYPTION is set to REQUIRED by default. This means that all connections to this endpoint must use encryption. However, you can disable encryption or make it optional on an endpoint. The alternatives are as follows:  
  
        |Value|Definition|  
        |-----------|----------------|  
        |DISABLED|Specifies that data sent over a connection is not encrypted.|  
        |SUPPORTED|Specifies that the data is encrypted only if the opposite endpoint specifies either SUPPORTED or REQUIRED.|  
        |REQUIRED|Specifies that data sent over a connection must be encrypted.|  
  
         If an endpoint requires encryption, the other endpoint must have ENCRYPTION set to either SUPPORTED or REQUIRED.  
  
    -   *\<algorithm>* provides the option of specifying the encryption standards for the endpoint. The value of *\<algorithm>* can be one following algorithms or combinations of algorithms: RC4, AES, AES RC4, or RC4 AES.  
  
         AES RC4 specifies that this endpoint will negotiate for the encryption algorithm, giving preference to the AES algorithm. RC4 AES specifies that this endpoint will negotiate for the encryption algorithm, giving preference to the RC4 algorithm. If both endpoints specify both algorithms but in different orders, the endpoint accepting the connection wins.  
  
        > [!NOTE]  
        >  The RC4 algorithm is deprecated. [!INCLUDE[ssNoteDepFutureDontUse](../../includes/ssnotedepfuturedontuse-md.md)] We recommend that you use AES.  
  
    -   *\<role>* defines the role or roles that the server can perform. Specifying ROLE is required. However, the role of the endpoint is relevant only for database mirroring. For [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], the role of the endpoint is ignored.  
  
         To allow a server instance to serve as one role for one database mirroring session and different role for another session, specify ROLE=ALL. To restrict a server instance to being either a partner or a witness, specify ROLE=PARTNER or ROLE=WITNESS, respectively.  
  
        > [!NOTE]  
        >  For more information about Database Mirroring options for different editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
     For a complete description of the CREATE ENDPOINT syntax, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md).  
  
    > [!NOTE]  
    >  To change an existing endpoint, use [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md).  
  
###  <a name="TsqlExample"></a> Example: Creating Endpoints to Support for Database Mirroring (Transact-SQL)  
 The following example creates database mirroring endpoints for the default server instances on three separate computer systems:  
  
|Role of server instance|Name of host computer|  
|-----------------------------|---------------------------|  
|Partner (initially in the principal role)|`SQLHOST01\.`|  
|Partner (initially in the mirror role)|`SQLHOST02\.`|  
|Witness|`SQLHOST03\.`|  
  
 In this example, all three endpoints use port number 7022, though any available port number would work. The AUTHENTICATION option is unnecessary, because the endpoints use the default type, Windows Authentication. The ENCRYPTION option is also unnecessary, because the endpoints are all intended to negotiate the authentication method for a connection, which is the default behavior for Windows Authentication. Also, all of the endpoints require the encryption, which is the default behavior.  
  
 Each server instance is limited to serving as either a partner or a witness, and the endpoint of each server expressly specifies which role (ROLE=PARTNER or ROLE=WITNESS).  
  
> [!IMPORTANT]  
>  Each server instance can have only one endpoint. Therefore, if you want a server instance to be a partner in some sessions and the witness in others, specify ROLE=ALL.  
  
```  
--Endpoint for initial principal server instance, which  
--is the only server instance running on SQLHOST01.  
CREATE ENDPOINT endpoint_mirroring  
    STATE = STARTED  
    AS TCP ( LISTENER_PORT = 7022 )  
    FOR DATABASE_MIRRORING (ROLE=PARTNER);  
GO  
--Endpoint for initial mirror server instance, which  
--is the only server instance running on SQLHOST02.  
CREATE ENDPOINT endpoint_mirroring  
    STATE = STARTED  
    AS TCP ( LISTENER_PORT = 7022 )  
    FOR DATABASE_MIRRORING (ROLE=PARTNER);  
GO  
--Endpoint for witness server instance, which  
--is the only server instance running on SQLHOST03.  
CREATE ENDPOINT endpoint_mirroring  
    STATE = STARTED  
    AS TCP ( LISTENER_PORT = 7022 )  
    FOR DATABASE_MIRRORING (ROLE=WITNESS);  
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To Configure a Database Mirroring Endpoint**  
  
-   [Create a Database Mirroring Endpoint for Always On Availability Groups &#40;SQL Server PowerShell&#41;](../../database-engine/availability-groups/windows/database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Use Certificates for a Database Mirroring Endpoint &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/use-certificates-for-a-database-mirroring-endpoint-transact-sql.md)  
  
    -   [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md)  
  
    -   [Allow a Database Mirroring Endpoint to Use Certificates for Inbound Connections &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/database-mirroring-use-certificates-for-inbound-connections.md)  
  
-   [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
 **To View Information About the Database Mirroring Endpoint**  
  
-   [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)  
  
## See Also  
 [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md)   
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md)   
 [Example: Setting Up Database Mirroring Using Windows Authentication &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/example-setting-up-database-mirroring-using-windows-authentication-transact-sql.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
  
  

