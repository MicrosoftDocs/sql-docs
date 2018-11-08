---
title: "Capture Logon Trigger Event Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
ms.assetid: e05b1ab4-c10b-402a-9591-f6ec1e3db8c0
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Capture Logon Trigger Event Data
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]
  To capture XML data about LOGON events for use inside logon triggers, use the [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md) function. The LOGON event returns the following event data schema:  
  
 `<EVENT_INSTANCE>`  
  
 `<EventType>event_type</EventType>`  
  
 `<PostTime>post_time</PostTime>`  
  
 `<SPID>spid</SPID>`  
  
 `<ServerName>server_name</ServerName>`  
  
 `<LoginName>login_name</LoginName>`  
  
 `<LoginType>login_type</LoginType>`  
  
 `<SID>sid</SID>`  
  
 `<ClientHost>client_host</ClientHost>`  
  
 `<IsPooled>is_pooled</IsPooled>`  
  
 `</EVENT_INSTANCE>`  
  
 `<EventType>`  
 Contains `LOGON`.  
  
 `<PostTime>`  
 Contains the time when a session is requested to be established.  
  
 `<SID>`  
 Contains the base 64-encoded binary stream of the security identification number (SID) for the specified login name.  
  
 `<ClientHost>`  
 Contains the host name of the client from where the connection is made. The value is '`<local_machine>`' if the client and server name are the same. Otherwise, the value is the IP address of the client.  
  
 `<IsPooled>`  
 Is `1` if the connection is reused by using connection pooling. Otherwise, the value is `0`.  
  
  
