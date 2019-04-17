---
title: "Audit Broker Login Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Audit Broker Login event class"
ms.assetid: af9b1153-2791-40ef-a95c-50923cd0cc97
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Audit Broker Login Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates an **Audit Broker Login** event to report audit messages related to Service Broker transport security.  
  
## Audit Broker Login Event Class Data Columns  
  
|Data column|Type|Description|Column number|Filterable|  
|-----------------|----------|-----------------|-------------------|----------------|  
|**ApplicationName**|**nvarchar**|Unused in this event class.|10|Yes|  
|**ClientProcessID**|**int**|Unused in this event class.|9|Yes|  
|**DatabaseID**|**int**|[!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the **ServerName** data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|**EventClass**|**int**|The type of event class captured. Always **159** for **Audit Broker Login**.|27|No|  
|**EventSequence**|**int**|Sequence number for this event.|51|No|  
|**EventSubClass**|**int**|The type of event subclass, providing further information about each event class. The table below lists the event subclass values for this event.|21|Yes|  
|**FileName**|**nvarchar**|Remote broker authentication level. Supported authentication method configured on the remote broker endpoint. When more than one method is available, the accepting (target) endpoint determines which method is tried first. Possible values are:<br /><br /> **None**. No authentication method is configured.<br /><br /> **NTLM**. Requires NTLM authentication.<br /><br /> **KERBEROS**. Requires Kerberos authentication.<br /><br /> **NEGOTIATE**. Windows negotiates the authentication method.<br /><br /> **CERTIFICATE**. Requires the certificate configured for the endpoint, which is stored in the **master** database.<br /><br /> **NTLM, CERTIFICATE**. Accepts NTLM or SSL certificate authentication.<br /><br /> **KERBEROS, CERTIFICATE**. Accepts Kerberos or the endpoint certificate authentication.<br /><br /> **NEGOTIATE, CERTIFICATE**. Windows negotiates the authentication method or an endpoint certificate can be used for authentication.<br /><br /> **CERTIFICATE, NTLM**. Accepts an endpoint certificate or NTLM for authentication.<br /><br /> **CERTIFICATE, KERBEROS**. Accepts an endpoint certificate or Kerberos for authentication.<br /><br /> **CERTIFICATE, NEGOTIATE**. Accepts an endpoint certificate for authentication or Windows negotiates the authentication method..|36|No|  
|**HostName**|**nvarchar**|Unused in this event class.|8|Yes|  
|**IsSystem**|**int**|Indicates whether the event occurred on a system process or a user process. 1 = system, 0 = user.|60|No|  
|**LoginSid**|**image**|The security identification number (SID) of the logged-in user. Each SID is unique for each login in the server.|41|Yes|  
|**NTDomainName**|**nvarchar**|The Windows domain to which the user belongs.|7|Yes|  
|**NTUserName**|**nvarchar**|The name of the user that owns the connection that generated this event.|6|Yes|  
|**ObjectName**|**nvarchar**|The connect string used for this connection.|34|No|  
|**OwnerName**|**nvarchar**|Supported authentication method configured on the local broker endpoint. When more than one method is available, the accepting (target) endpoint determines which method is tried first. Possible values are:<br /><br /> **None**. No authentication method is configured.<br /><br /> **NTLM**. Requires NTLM authentication.<br /><br /> **KERBEROS**. Requires Kerberos authentication.<br /><br /> **NEGOTIATE**. Windows negotiates the authentication method.<br /><br /> **CERTIFICATE**. Requires the certificate configured for the endpoint, which is stored in the **master** database.<br /><br /> **NTLM, CERTIFICATE**. Accepts NTLM or SSL certificate authentication.<br /><br /> **KERBEROS, CERTIFICATE**. Accepts Kerberos or the endpoint certificate authentication.<br /><br /> **NEGOTIATE, CERTIFICATE**. Windows negotiates the authentication method or an endpoint certificate can be used for authentication.<br /><br /> **CERTIFICATE, NTLM**. Accepts an endpoint certificate or for NTLM authentication.<br /><br /> **CERTIFICATE, KERBEROS**. Accepts an endpoint certificate or Kerberos for authentication.<br /><br /> **CERTIFICATE, NEGOTIATE**. Accepts an endpoint certificate for authentication or Windows negotiates the authentication method..|37|No|  
|**ProviderName**|**nvarchar**|The authentication method used for this connection|46|No|  
|**RoleName**|**nvarchar**|The role of the connection. This is either **initiator** or **target**.|38|No|  
|**ServerName**|**nvarchar**|The name of the instance of SQL Server being traced.|26|No|  
|**SPID**|**int**|The server process ID assigned by SQL Server to the process associated with the client.|12|Yes|  
|**StartTime**|**datetime**|The time at which the event started, when available.|14|Yes|  
|**State**|**int**|Indicates the location within the SQL Server source code that produced the event. Each location that may produce this event has a different state code. A Microsoft support engineer can use this state code to find where the event was produced.|30|No|  
|**TargetUserName**|**nvarchar**|Login state. One of:<br /><br /> INITIAL<br /><br /> WAIT LOGIN NEGOTIATE<br /><br /> ONE ISC<br /><br /> ONE ASC<br /><br /> TWO ISC<br /><br /> TWO ASC<br /><br /> WAIT ISC Confirm<br /><br /> WAIT ASC Confirm<br /><br /> WAIT REJECT<br /><br /> WAIT PRE-MASTER SECRET<br /><br /> WAIT VALIDATION<br /><br /> WAIT ARBITRATION<br /><br /> ONLINE<br /><br /> ERROR<br /><br /> <br /><br /> **Note**: ISC = Initiate Security Context. ASC = Accept Security Context|39|No|  
|**TransactionID**|**bigint**|The system-assigned ID of the transaction.|4|No|  
  
 The table below lists the subclass values for this event class.  
  
|ID|Subclass|Description|  
|--------|--------------|-----------------|  
|1|Login Success|A Login Success event reports that the adjacent broker login process has finished successfully.|  
|2|Login Protocol Error|A Login Protocol Error event reports that the broker receives a message that is well-formed but not valid for the current state of the of the login process. The message may have been lost or sent out-of-sequence.|  
|3|Message Format Error|A Message Format Error event reports that the broker received a message that does not match the expected format. The message may have been corrupted, or a program other than SQL Server may be sending messages to the port that Service Broker uses.|  
|4|Negotiate Failure|A Negotiate Failure event reports that the local broker and the remote broker support mutually exclusive levels of authentication.|  
|5|Authentication Failure|An Authentication Failure event reports that Service Broker cannot perform authentication for the connection due to an error. For Windows Authentication, this event reports that Service Broker is unable to use Windows Authentication. For certificate-based authentication, this event reports that Service Broker is unable to access the certificate.|  
|6|Authorization Failure|An Authorization Failure event reports that Service Broker denied authorization for the connection. For Windows Authentication, this event reports that the security identifier for the connection does not match a database user. For certificate-based authentication, this event reports that the public key delivered in the message does not correspond to a certificate in the database.|  
  
## See Also  
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [ALTER ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/alter-endpoint-transact-sql.md)   
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  
