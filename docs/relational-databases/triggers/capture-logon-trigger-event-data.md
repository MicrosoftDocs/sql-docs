---
title: "Capture Logon Trigger Event Data"
description: Learn how to capture XML data about LOGON events for use inside logon triggers.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 08/27/2024
ms.service: sql
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Capture logon trigger event data

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To capture XML data about `LOGON` events for use inside logon triggers, use the [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md) function. The `LOGON` event returns the following event data schema:

```xml
<EVENT_INSTANCE>
   <EventType>event_type</EventType>
   <PostTime>post_time</PostTime>
   <SPID>spid</SPID>
   <ServerName>server_name</ServerName>
   <LoginName>login_name</LoginName>
   <LoginType>login_type</LoginType>
   <SID>sid</SID>
   <ClientHost>client_host</ClientHost>
   <IsPooled>is_pooled</IsPooled>
</EVENT_INSTANCE>
```

#### \<EventType>

Contains `LOGON`.

#### \<PostTime>

Contains the time when a session requests to be established.

#### \<LoginType>

The type of login, such as [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] login, Windows account, certificate, server role, or Microsoft Entra ID.

#### \<SID>

Contains the base 64-encoded binary stream of the security identification number (SID) for the specified login name.

#### \<ClientHost>

Contains the host name of the client from where the connection is made. The value is `<local_machine>` if the client and server name are the same. Otherwise, the value is the IP address of the client.

#### \<IsPooled>

`1` if the connection is reused by using connection pooling. Otherwise, the value is `0`.
