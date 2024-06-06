---
title: Trace the network connection close process on SQL Server
description: Learn about the sequence when a connection to the SQL Server Database Engine is closed.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/05/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "network trace [SQL Server]"
---

# Trace the network connection close sequence on the Database Engine

This article presents examples of a network trace that captures the sequence during when a Transmission Control Protocol (TCP) connection between a client application and the SQL Server Database Engine (the server) is closed. Understanding these patterns is crucial for diagnosing network behavior, identifying pooling strategies, and optimizing connection management in web or service applications.

## Closing connection types

This article provides examples for normal TCP connections, and Multiple Active Result Sets (MARS) connections. MARS is a feature of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], introduced with [!INCLUDE [ssversion2005-md](../includes/ssversion2005-md.md)], that allows multiple commands to be executed on a connection without having to clean up the results from the first command, before running the second command. MARS is achieved through session multiplexing (SMUX).

## [Normal connections](#tab/normal)

This section describes several examples of closing a network connection.

- The client IP address is `10.10.10.104`
- The server IP address is `10.10.10.22`

#### Closing packets

This example shows a normal connection close sequence. Note the low frame numbers and time offsets. This sequence is most likely a pooled connection closure. This should occur within 30 seconds of the beginning of the trace, or you might also see keep-alive packets.

:::image type="content" source="media/database-engine-connection-open-network-trace/four-way-close.png" alt-text="Diagram of four-way TCP session close.":::

```output
Frame Offset    Source IP    Dest IP      Description
----- --------- ------------ ------------ ---------------------------------------------------------------------------
   50 4.1529661 10.10.10.104  10.10.10.22 TCP:Flags=...A...F, SrcPort=4657, DstPort=1433, PayloadLen=0, Seq=413460761
   51 4.1529661  10.10.10.22 10.10.10.104 TCP:Flags=...A...., SrcPort=1433, DstPort=4657, PayloadLen=0, Seq=280398321
   52 4.1529661  10.10.10.22 10.10.10.104 TCP:Flags=...A...F, SrcPort=1433, DstPort=4657, PayloadLen=0, Seq=280398321
   54 4.2330441 10.10.10.104  10.10.10.22 TCP:Flags=...A...., SrcPort=4657, DstPort=1433, PayloadLen=0, Seq=413460761
```

#### Transact-SQL statements and closing packets

This example shows closing a nonpooled connection, after two Transact-SQL statements. If this connection was nonpooled, you could also see keep-alive packets associated with sending the connection back into the connection pool, instead of the closing packets immediately following the last response from the server. We recommend pooling connections in any sort of web or service application to allow connection reuse. Connection pooling reduces the number of connections to the server, and minimizes the cost and delay associate with new connections.

```output
Frame Offset    Source IP    Dest IP      Description
----- --------- ------------ ------------ ---------------------------------------------------------------------------
  364 9.1949581 10.10.10.104  10.10.10.22 TDS:SQLBatch, Version = 7.300000, SPID = 0, PacketID = 1, Flags=...AP..., S
  365 9.1949581  10.10.10.22 10.10.10.104 TDS:Response, Version = 7.300000, SPID = 130, PacketID = 1, Flags=...AP...,
  366 9.3043331 10.10.10.104  10.10.10.22 TDS:SQLBatch, Version = 7.300000, SPID = 0, PacketID = 1, Flags=...AP..., S
  367 9.3072631  10.10.10.22 10.10.10.104 TDS:Response, Version = 7.300000, SPID = 130, PacketID = 1, Flags=...AP...,
  375 9.4078491 10.10.10.104  10.10.10.22 TCP:Flags=...A...F, SrcPort=4647, DstPort=1433, PayloadLen=0, Seq=157672648
  376 9.4078491  10.10.10.22 10.10.10.104 TCP:Flags=...A...., SrcPort=1433, DstPort=4647, PayloadLen=0, Seq=192890973
  379 9.4078491  10.10.10.22 10.10.10.104 TCP:Flags=...A...F, SrcPort=1433, DstPort=4647, PayloadLen=0, Seq=192890973
  397 9.5221071 10.10.10.104  10.10.10.22 TCP:Flags=...A...., SrcPort=4647, DstPort=1433, PayloadLen=0, Seq=157672649
```

#### Idle or pooled connection being closed

The connection is closed 10 seconds after the previous keep-alive exchange (see `Delta` column).

> [!NOTE]  
> The parser mistakenly marks the initial `ACK+FIN` packet (Frame 1881) as a keep-alive `ACK` packet, because the previous keep-alive packet. However, it is initializing the connection closure.

```output
Frame Offset     Delta      Source IP   Dest IP     Description
----- ---------- ---------- ----------- ----------- -----------------------------------------------------------------
 1314 16.3641802  0.0000000 10.10.10.45 10.10.10.51 TCP:[Keep alive]Flags=...A...., SrcPort=51708, DstPort=1433, Payl
 1317 16.3677083  0.0035281 10.10.10.51 10.10.10.45 TCP:[Keep alive ack]Flags=...A...., SrcPort=1433, DstPort=51708, 
 1327 16.4269375  0.0592292 10.10.10.51 10.10.10.45 TCP:[Keep alive]Flags=...A...., SrcPort=1433, DstPort=51708, Payl
 1328 16.4269637  0.0000262 10.10.10.45 10.10.10.51 TCP:[Keep alive ack]Flags=...A...., SrcPort=51708, DstPort=1433, 
 1881 26.7918499 10.3648862 10.10.10.45 10.10.10.51 TCP:[Keep alive ack]Flags=...A...F, SrcPort=51708, DstPort=1433, 
 1886 26.7929474  0.0010975 10.10.10.51 10.10.10.45 TCP:Flags=...A...., SrcPort=1433, DstPort=51708, PayloadLen=0, Se
 1888 26.7929474  0.0000000 10.10.10.51 10.10.10.45 TCP:Flags=...A...F, SrcPort=1433, DstPort=51708, PayloadLen=0, Se
 1890 26.7929947  0.0000473 10.10.10.45 10.10.10.51 TCP:Flags=...A...., SrcPort=51708, DstPort=1433, PayloadLen=0, Se
```

## [MARS connections](#tab/mars)

As described in the [Network authentication process](database-engine-connection-open-network-trace.md?tabs=mars), you can determine MARS connection from the following packets:

- `SMP:SYN` starts a new session
- `SMP:ACK` acknowledges data packets
- `SMP:FIN` terminates a session

The following trace examples show the various packets involved in terminating a connection. In the last example, you see the closing results in a `RESET` packet, due to a consistent timing issue on the client. This reset is benign.

- The client IP address is `10.10.10.10`
- The server IP address is `10.10.10.22`

#### Example of the SMP:FIN packet

:::image type="content" source="media/database-engine-connection-open-network-trace/mars-close.png" alt-text="Diagram of a MARS SMP:FIN packet.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------
93689 569.9170056 10.10.10.10 10.10.10.22 SMP:FIN, Sid = 0, Length = 16, SeqNum = 559, Wndw = 614 {SMP:190, TCP:160, 
93691 569.9170640 10.10.10.22 10.10.10.10 SMP:FIN, Sid = 0, Length = 16, SeqNum = 610, Wndw = 563 {SMP:190, TCP:160, 
93692 569.9173178 10.10.10.10 10.10.10.22 TCP:Flags=...A...F, SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66189274
93704 569.9173178 10.10.10.10 10.10.10.22 TCP:Flags=...A.R.., SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66189274
```

#### Reset due to SMP:FIN

The previous trace example was taken on the server. In the connection close, you can see the `SMP:FIN` packet from the client closing the MARS session and the server sends its corresponding `SMP:FIN` packet. Then you see the `ACK+FIN` from the client followed immediately by a `RESET` packet. This only makes sense by looking at what the client sees.

On the client side, the packets appear in a different order, giving rise to the `RESET` packet:

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------
12346 569.9170056 10.10.10.10 10.10.10.22 SMP:FIN, Sid = 0, Length = 16, SeqNum = 559, Wndw = 614 {SMP:190, TCP:160, 
12352 569.9173178 10.10.10.10 10.10.10.22 TCP:Flags=...A...F, SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66189274
12361 569.9170640 10.10.10.22 10.10.10.10 SMP:FIN, Sid = 0, Length = 16, SeqNum = 610, Wndw = 563 {SMP:190, TCP:160, 
12374 569.9173178 10.10.10.10 10.10.10.22 TCP:Flags=...A.R.., SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66189274
```

The client sends the `SMP:FIN` followed immediately by the `ACK+FIN`. The next packet should be an `ACK` and the server's `ACK+FIN` packet per TCP rules, but we get the server's `SMP:FIN` instead. This sequence results in the `RESET`, because the TCP layer isn't expecting what it considers to be a data packet arriving after the close sequence was initiated. Occasionally, you might also see the server's `ACK+FIN` packet, but that gets reset, as well.

This closure is benign.

---

## Related content

- [Enabling Multiple Active Result Sets](../connect/ado-net/sql/enable-multiple-active-result-sets.md)
- [Using MARS in ADO.NET](../connect/ado-net/sql/multiple-active-result-sets-mars.md)
- [Using MARS in OLE DB](../connect/oledb/features/using-multiple-active-result-sets-mars.md)
- [Trace the network authentication process to the Database Engine](database-engine-connection-open-network-trace.md)
