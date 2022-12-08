---
title: CREATE ENDPOINT (Transact-SQL)
description: CREATE ENDPOINT (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ENDPOINT"
  - "CREATE ENDPOINT"
  - "ENDPOINT_TSQL"
  - "CREATE_ENDPOINT_TSQL"
helpviewer_keywords:
  - "database mirroring [SQL Server], endpoint"
  - "HTTP SOAP support [SQL Server]"
  - "CREATE ENDPOINT statement"
  - "Availability Groups [SQL Server], configuring"
  - "endpoints [SQL Server], creating"
  - "SOAP [SQL Server built-in support], endpoints"
  - "SOAP [SQL Server built-in support], sqlbatch"
  - "DATABASE_MIRRORING option"
  - "HTTP protocol option [SQL Server]"
  - "SOAP [SQL Server built-in support], ad hoc"
  - "TCP protocol option [SQL Server]"
  - "SERVICE_BROKER option"
  - "Availability Groups [SQL Server], endpoint"
dev_langs:
  - "TSQL"
---

# CREATE ENDPOINT (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates endpoints and defines their properties, including the methods available to client applications. For related permissions information, see [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md).  
  
 The syntax for CREATE ENDPOINT can logically be broken into two parts:  
  
-   The first part starts with AS and ends before the FOR clause.  
  
     In this part, you provide information specific to the transport protocol (TCP) and set a listening port number for the endpoint, as well as the method of endpoint authentication and/or a list of IP addresses (if any) that you want to restrict from accessing the endpoint.  
  
-   The second part starts with the FOR clause.  
  
     In this part, you define the payload that is supported on the endpoint. The payload can be one of several supported types: [!INCLUDE[tsql](../../includes/tsql-md.md)], service broker, database mirroring. In this part, you also include language-specific information.  
  
> [!NOTE]  
> Native XML Web Services (SOAP/HTTP endpoints) was removed in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE ENDPOINT endPointName [ AUTHORIZATION login ]  
[ STATE = { STARTED | STOPPED | DISABLED } ]  
AS { TCP } (  
   <protocol_specific_arguments>  
        )  
FOR { TSQL | SERVICE_BROKER | DATABASE_MIRRORING } (  
   <language_specific_arguments>  
        )  
  
<AS TCP_protocol_specific_arguments> ::=  
AS TCP (  
  LISTENER_PORT = listenerPort  
  [ [ , ] LISTENER_IP = ALL | ( xx.xx.xx.xx IPv4 address ) | ( '__:__1' IPv6 address ) ]  
  
)  
  
<FOR SERVICE_BROKER_language_specific_arguments> ::=  
FOR SERVICE_BROKER (  
   [ AUTHENTICATION = {   
            WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]   
      | CERTIFICATE certificate_name   
      | WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ] CERTIFICATE certificate_name   
      | CERTIFICATE certificate_name WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]   
    } ]  
   [ [ , ] ENCRYPTION = { DISABLED | { { SUPPORTED | REQUIRED }   
       [ ALGORITHM { AES | RC4 | AES RC4 | RC4 AES } ] }   
   ]  
   [ [ , ] MESSAGE_FORWARDING = { ENABLED | DISABLED } ]  
   [ [ , ] MESSAGE_FORWARD_SIZE = forward_size ]  
)  
  

<FOR DATABASE_MIRRORING_language_specific_arguments> ::=  
FOR DATABASE_MIRRORING (  
   [ AUTHENTICATION = {   
            WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]   
      | CERTIFICATE certificate_name   
      | WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ] CERTIFICATE certificate_name   
      | CERTIFICATE certificate_name WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]   
   [ [ [ , ] ] ENCRYPTION = { DISABLED | { { SUPPORTED | REQUIRED }   
       [ ALGORITHM { AES | RC4 | AES RC4 | RC4 AES } ] }   
  
    ]   
   [ , ] ROLE = { WITNESS | PARTNER | ALL }  
)  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *endPointName*  
 Is the assigned name for the endpoint you are creating. Use when updating or deleting the endpoint.  
  
 AUTHORIZATION *login*  
 Specifies a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Windows login that is assigned ownership of the newly created endpoint object. If AUTHORIZATION is not specified, by default, the caller becomes owner of the newly created object.  
  
 To assign ownership by specifying AUTHORIZATION, the caller must have IMPERSONATE permission on the specified *login*.  
  
 To reassign ownership, see [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md).  
  
 STATE **=** { STARTED | **STOPPED** | DISABLED }  
 Is the state of the endpoint when it is created. If the state is not specified when the endpoint is created, STOPPED is the default.  
  
 STARTED  
 Endpoint is started and is actively listening for connections.  
  
 DISABLED  
 Endpoint is disabled. In this state, the server listens to port requests but returns errors to clients.  
  
 **STOPPED**  
 Endpoint is stopped. In this state, the server does not listen to the endpoint port or respond to any attempted requests to use the endpoint.  
  
 To change the state, use [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md).  
  
 AS { TCP }  
 Specifies the transport protocol to use.  
  
 FOR { TSQL | SERVICE_BROKER | DATABASE_MIRRORING }  
 Specifies the payload type.  
  
 Currently, there are no [!INCLUDE[tsql](../../includes/tsql-md.md)] language-specific arguments to pass in the `<language_specific_arguments>` parameter.  
  
 **TCP Protocol Option**  
  
 The following arguments apply only to the TCP protocol option.  
  
 LISTENER_PORT **=**_listenerPort_  
 Specifies the port number listened to for connections by the service broker TCP/IP protocol. By convention, 5022 is used but any number between 1024 and 32767 is valid.  
  
 LISTENER_IP **=** ALL | **(**_4-part-ip_ **)** | **(** "*ip_address_v6*" **)**  
 Specifies the IP address that the endpoint will listen on. The default is ALL. This means that the listener will accept a connection on any valid IP address.  
  
 If you configure database mirroring with an IP address instead of a fully-qualified domain name (`ALTER DATABASE SET PARTNER = partner_IP_address` or `ALTER DATABASE SET WITNESS = witness_IP_address`), you have to specify `LISTENER_IP =IP_address` instead of `LISTENER_IP=ALL` when you create mirroring endpoints.  
  
 **SERVICE_BROKER and DATABASE_MIRRORING Options**  
  
 The following AUTHENTICATION and ENCRYPTION arguments are common to the SERVICE_BROKER and DATABASE_MIRRORING options.  
  
> [!NOTE]  
>  For options that are specific to SERVICE_BROKER, see "SERVICE_BROKER Options," later in this section. For options that are specific to DATABASE_MIRRORING, see "DATABASE_MIRRORING Options," later in this section.  
  
 AUTHENTICATION **=** \<authentication_options>  
 Specifies the TCP/IP authentication requirements for connections for this endpoint. The default value is `WINDOWS`.
  
 The supported authentication methods include NTLM and or Kerberos or both.  
  
> [!IMPORTANT]  
>  All mirroring connections on a server instance use a single database mirroring endpoint. Any attempt to create an additional database mirroring endpoint will fail.  
  
 **\<authentication_options> ::=**  
  
 **WINDOWS** [ { NTLM \| KERBEROS \| **NEGOTIATE** } ]  
 Specifies that the endpoint is to connect using Windows Authentication protocol to authenticate the endpoints. This is the default.  
  
 If you specify an authorization method (NTLM or KERBEROS), that method is always used as the authentication protocol. The default value, NEGOTIATE, causes the endpoint to use the Windows negotiation protocol to choose either NTLM or Kerberos.  
  
 CERTIFICATE *certificate_name*  
 Specifies that the endpoint is to authenticate the connection using the certificate specified by *certificate_name* to establish identity for authorization. The far endpoint must have a certificate with the public key matching the private key of the specified certificate.  
  
 WINDOWS [ { NTLM \| KERBEROS \| **NEGOTIATE** } ] CERTIFICATE *certificate_name*  
 Specifies that endpoint is to try to connect by using Windows Authentication and, if that attempt fails, to then try using the specified certificate.  
  
 CERTIFICATE *certificate_name* WINDOWS [ { NTLM \| KERBEROS \| **NEGOTIATE** } ]  
 Specifies that endpoint is to try to connect by using the specified certificate and, if that attempt fails, to then try using Windows Authentication.  
  
 ENCRYPTION = { DISABLED \| SUPPORTED \| **REQUIRED** } [ALGORITHM { **AES** \| RC4 \| AES RC4 \| RC4 AES } ]  
 Specifies whether encryption is used in the process. The default is REQUIRED.  
  
 DISABLED  
 Specifies that data sent over a connection is not encrypted.  
  
 SUPPORTED  
 Specifies that the data is encrypted only if the opposite endpoint specifies either SUPPORTED or REQUIRED.  
  
 REQUIRED  
 Specifies that connections to this endpoint must use encryption. Therefore, to connect to this endpoint, another endpoint must have ENCRYPTION set to either SUPPORTED or REQUIRED.  
  
 Optionally, you can use the ALGORITHM argument to specify the form of encryption used by the endpoint, as follows:  
  
 **AES**  
 Specifies that the endpoint must use the AES algorithm. This is the default in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
 RC4  
 Specifies that the endpoint must use the RC4 algorithm. This is the default through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
  
 AES RC4  
 Specifies that the two endpoints will negotiate for an encryption algorithm with this endpoint giving preference to the AES algorithm.  
  
 RC4 AES  
 Specifies that the two endpoints will negotiate for an encryption algorithm with this endpoint giving preference to the RC4 algorithm.  
  
> [!NOTE]  
>  The RC4 algorithm is deprecated. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use AES.  
  
 If both endpoints specify both algorithms but in different orders, the endpoint accepting the connection wins.  
  
 **SERVICE_BROKER Options**  
  
 The following arguments are specific to the SERVICE_BROKER option.  
  
 MESSAGE_FORWARDING **=** { ENABLED | **DISABLED** }  
 Determines whether messages received by this endpoint that are for services located elsewhere will be forwarded.  
  
 ENABLED  
 Forwards messages if a forwarding address is available.  
  
 DISABLED  
 Discards messages for services located elsewhere. This is the default.  
  
 MESSAGE_FORWARD_SIZE **=**_forward_size_  
 Specifies the maximum amount of storage in megabytes to allocate for the endpoint to use when storing messages that are to be forwarded.  
  
 **DATABASE_MIRRORING Options**  
  
 The following argument is specific to the DATABASE_MIRRORING option.  
  
 ROLE **=** { WITNESS | PARTNER | ALL }  
 Specifies the database mirroring role or roles that the endpoint supports.  
  
 WITNESS  
 Enables the endpoint to perform in the role of a witness in the mirroring process.  
  
> [!NOTE]  
>  For [!INCLUDE[ssExpressEd2005](../../includes/ssexpressed2005-md.md)], WITNESS is the only option available.  
  
 PARTNER  
 Enables the endpoint to perform in the role of a partner in the mirroring process.  
  
 ALL  
 Enables the endpoint to perform in the role of both a witness and a partner in the mirroring process.  
  
 For more information about these roles, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
> [!NOTE]  
>  There is no default port for DATABASE_MIRRORING.  
  
## Remarks  
 ENDPOINT DDL statements cannot be executed inside a user transaction. ENDPOINT DDL statements do not fail even if an active snapshot isolation level transaction is using the endpoint being altered.  
  
 Requests can be executed against an ENDPOINT by the following:  
  
-   Members of **sysadmin** fixed server role  
  
-   The owner of the endpoint  
  
-   Users or groups that have been granted CONNECT permission on the endpoint  
  
## Permissions  
 Requires CREATE ENDPOINT permission, or membership in the **sysadmin** fixed server role. For more information, see [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md).  
  
## Example  
  
### Creating a database mirroring endpoint  
 The following example creates a database mirroring endpoint. The endpoint uses port number `7022`, although any available port number would work. The endpoint is configured to use Windows Authentication using only Kerberos. The `ENCRYPTION` option is configured to the nondefault value of `SUPPORTED` to support encrypted or unencrypted data. The endpoint is being configured to support both the partner and witness roles.  
  
```sql  
CREATE ENDPOINT endpoint_mirroring  
    STATE = STARTED  
    AS TCP ( LISTENER_PORT = 7022 )  
    FOR DATABASE_MIRRORING (  
       AUTHENTICATION = WINDOWS KERBEROS,  
       ENCRYPTION = SUPPORTED,  
       ROLE=ALL);  
GO  
```  

### Create a new endpoint pointing to a specific IPv4 address and port

```sql
CREATE ENDPOINT ipv4_endpoint_special
STATE = STARTED
AS TCP (
    LISTENER_PORT = 55555, LISTENER_IP = (10.0.75.1)
)
FOR TSQL ();

GRANT CONNECT ON ENDPOINT::[TSQL Default TCP] TO public; -- Keep existing public permission on default endpoint for demo purpose
GRANT CONNECT ON ENDPOINT::ipv4_endpoint_special
TO login_name;
```

### Create a new endpoint pointing to a specific IPv6 address and port

```sql
CREATE ENDPOINT ipv6_endpoint_special
STATE = STARTED
AS TCP (
    LISTENER_PORT = 55555, LISTENER_IP = ('::1')
)
FOR TSQL ();

GRANT CONNECT ON ENDPOINT::[TSQL Default TCP] TO public;
GRANT CONNECT ON ENDPOINT::ipv6_endpoint_special

```
  
## See also  
 [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md)   
 [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)   
 [DROP ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-endpoint-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
