---
title: "srv_pfield (Extended Stored Procedure API)"
description: Learn how srv_pfield in the Extended Stored Procedure API returns information about a database connection.
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
helpviewer_keywords:
  - "srv_pfield"
dev_langs:
  - "C++"
apilocation: opends60.dll
apiname: srv_pfield
apitype: "DLLExport"
ms.assetid: a61e4c1f-e65b-48ea-a7d1-3e1544af389d
---
# srv_pfield (Extended Stored Procedure API)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
    
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use CLR integration instead.  
  
 Returns information about a database connection.  
  
## Syntax  
  
```  
  
DBCHAR * srv_pfield (  
SRV_PROC *  
srvproc  
,  
int   
field  
,  
int *  
len  
);  
```  
  
## Arguments  
 *srvproc*  
 Pointer identifying a database connection.  
  
 *field*  
 Specifies data on the connection to return.  
  
|Value|Returns|  
|-----------|-------------|  
|SRV_APPLNAME|The application name provided by the client when it established the connection.|  
|SRV_BCPFLAG|A flag that is TRUE if the client is preparing for a bulk copy operation; otherwise, FALSE.|  
|SRV_CLIB|The name of the library that enables the client to talk to a server.|  
|SRV_CPID|The client process ID on the client source computer.|  
|SRV_HOST|The name of the client's machine provided by the client when it established the connection.|  
|SRV_LIBVERS|The version of the client library.|  
|SRV_LSECURE|A flag. TRUE if the connection used integrated security to login.|  
|SRV_NETWORK_MODULE|The name of the Net-Library DLL used by the connection.|  
|SRV_NETWORK_VERSION|The version of the Net-Library DLL used by the connection.|  
|SRV_NETWORK_CONNECTION|The connection string passed to the Net-Library DLL used for the current *srvproc* connection.|  
|SRV_PIPEHANDLE|A string containing the pipe handle of a connected client, or NULL if the client is connected on a network that does not use named pipes. To use this handle as a valid pipe handle with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows, convert this string to an integer.|  
|SRV_RMTSERVER|The server from which the client process is logged in. If the login is from a client, this value is an empty string.|  
|SRV_ROWSENT|The number of rows already sent by *srvproc* for the current set of results.|  
|SRV_SPID|The server thread ID of the *srvproc*. For extended stored procedures, this value is the same as the **kpid** column of **sys.sysprocesses**, and it can change over time.|  
|SRV_SPROC_CODEPAGE|Codepage that the server uses to interpret multbyte data.|  
|SRV_STATUS|The current status of *srvproc*: running or closed|  
|SRV_TYPE|The connection type of *srvproc*. If server is returned, *srvproc* is from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If client is returned, *srvproc* is from a DB-Library or ODBC client.|  
|SRV_USER|The user name of the connection.|  
  
 *len*  
 Is a pointer to an **int** variable that contains the length of the returned *field* value. If *len* is NULL, the length of the string is not returned.  
  
## Returns  
 A pointer to a null-terminated string containing the current value for the specified field in the SRV_PROC structure. If the field is empty, a valid pointer to an empty string is returned and *len* contains 0. If the field is unknown, NULL is returned and *len* contains the value -1.  
  
> [!IMPORTANT]  
>  You should thoroughly review the source code of extended stored procedures, and you should test the compiled DLLs before you install them on a production server. For information about security review and testing, see the [Security Developer Center](https://go.microsoft.com/fwlink/?LinkID=54761&amp;clcid=0x409https://msdn.microsoft.com/security/).  
  
  
