---
title: "ALTER ENDPOINT (Transact-SQL)"
description: ALTER ENDPOINT (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER ENDPOINT"
  - "ALTER_ENDPOINT_TSQL"
helpviewer_keywords:
  - "ALTER ENDPOINT statement"
  - "modifying endpoints"
  - "endpoints [SQL Server], modifying"
dev_langs:
  - "TSQL"
---
# ALTER ENDPOINT (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Enables modifying an existing endpoint in the following ways:  
  
-   By adding a new method to an existing endpoint.  
  
-   By modifying or dropping an existing method from the endpoint.  
  
-   By changing the properties of an endpoint.  
  
> [!NOTE]  
>  This topic describes the syntax and arguments that are specific to ALTER ENDPOINT. For descriptions of the arguments that are common to both CREATE ENDPOINT and ALTER ENDPOINT, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md).  
  
 Native XML Web Services (SOAP/HTTP endpoints) is removed beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
ALTER ENDPOINT endPointName [ AUTHORIZATION login ]  
[ STATE = { STARTED | STOPPED | DISABLED } ]  
[ AS { TCP } ( <protocol_specific_items> ) ]  
[ FOR { TSQL | SERVICE_BROKER | DATABASE_MIRRORING } (  
   <language_specific_items>  
        ) ]  
  
<AS TCP_protocol_specific_arguments> ::=  
AS TCP (  
  LISTENER_PORT = listenerPort  
  [ [ , ] LISTENER_IP = ALL | ( 4-part-ip ) | ( "ip_address_v6" ) ]  
)  
<FOR SERVICE_BROKER_language_specific_arguments> ::=  
FOR SERVICE_BROKER (  
   [ AUTHENTICATION = {   
      WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]  
      | CERTIFICATE certificate_name   
      | WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ] CERTIFICATE certificate_name   
      | CERTIFICATE certificate_name WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]   
    } ]  
   [ , ENCRYPTION = { DISABLED   
       |   
         {{SUPPORTED | REQUIRED }   
       [ ALGORITHM { RC4 | AES | AES RC4 | RC4 AES } ] }   
   ]  
  
  [ , MESSAGE_FORWARDING = {ENABLED | DISABLED} ]  
  [ , MESSAGE_FORWARD_SIZE = forwardSize  
)  
  
<FOR DATABASE_MIRRORING_language_specific_arguments> ::=  
FOR DATABASE_MIRRORING (  
   [ AUTHENTICATION = {   
      WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]  
      | CERTIFICATE certificate_name   
      | WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ] CERTIFICATE certificate_name   
      | CERTIFICATE certificate_name WINDOWS [ { NTLM | KERBEROS | NEGOTIATE } ]   
    } ]  
   [ , ENCRYPTION = { DISABLED   
       |   
         {{SUPPORTED | REQUIRED }   
       [ ALGORITHM { RC4 | AES | AES RC4 | RC4 AES } ] }   
    ]   
   [ , ] ROLE = { WITNESS | PARTNER | ALL }  
)  
  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
  
> [!NOTE]  
>  The following arguments are specific to ALTER ENDPOINT. For descriptions of the remaining arguments, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md).  
  
 **AS** { **TCP** }  
 You cannot change the transport protocol with **ALTER ENDPOINT**.  
  
 **AUTHORIZATION** *login*  
 The **AUTHORIZATION** option is not available in **ALTER ENDPOINT**. Ownership can only be assigned when the endpoint is created.  
  
 **FOR** { **TSQL** | **SERVICE_BROKER** | **DATABASE_MIRRORING** }  
 You cannot change the payload type with **ALTER ENDPOINT**.  
  
## Remarks  
 When you use ALTER ENDPOINT, specify only those parameters that you want to update. All properties of an existing endpoint remain the same unless you explicitly change them.  
  
 The ENDPOINT DDL statements cannot be executed inside a user transaction.  
  
 For information on choosing an encryption algorithm for use with an endpoint, see [Choose an Encryption Algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md).  
  
> [!NOTE]  
>  The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100. (Not recommended.) Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later versions, material encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  
>   
>  RC4 is a relatively weak algorithm, and AES is a relatively strong algorithm. But AES is considerably slower than RC4. If security is a higher priority for you than speed, we recommend you use AES.  
  
## Permissions  
 User must be a member of the **sysadmin** fixed server role, the owner of the endpoint, or have been granted ALTER ANY ENDPOINT permission.  
  
 To change ownership of an existing endpoint, you must use the ALTER AUTHORIZATION statement. For more information, see [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md).  
  
 For more information, see [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md).  
  
## See Also  
 [DROP ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/drop-endpoint-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
