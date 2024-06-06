---
title: Trace the network authentication process to SQL Server
description: Learn about the various handshakes and authentication sequences during the connection process to the SQL Server Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/05/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "network trace [SQL Server]"
---

# Trace the network authentication process to the Database Engine

This article presents several examples of a network trace that captures various handshakes and authentication sequences during the Transmission Control Protocol (TCP) connection establishment process between a client application and the SQL Server Database Engine (the server).

For information about closing connections, see [Trace the network connection close sequence on the Database Engine](database-engine-connection-close-network-trace.md).

## Authentication types

You can connect to the [!INCLUDE [ssde-md](../includes/ssde-md.md)] with **Windows** authentication (using **Kerberos** or **NTLM** authentication), or **SQL** authentication.

This article also describes Multiple Active Result Sets (MARS) connections. MARS is a feature of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], introduced with [!INCLUDE [ssversion2005-md](../includes/ssversion2005-md.md)], that allows multiple commands to be executed on a connection without having to clean up the results from the first command, before running the second command. MARS is achieved through session multiplexing (SMUX).

## [SQL authentication](#tab/sql)

This process describes a normal login process using SQL authentication, showing each step of the conversation between the client and server through a detailed network trace analysis. The example network trace delineates the following steps:

1. TCP three-way handshake
1. Driver handshake
1. SSL/TLS handshake
1. Login packet exchange
1. Login confirmation
1. Execute a command and read the response
1. TCP four-way closing handshake

### Example network trace

This exchange is allocated 1 second regardless of the `Login Timeout` setting in the connection string.

- The client IP address is `10.10.10.10`
- The server IP address is `10.10.10.120`

#### Step 1. TCP three-way handshake

All TCP conversations start with a `SYN` packet (`S` flag set) sent from the client to the server. In Frame `6127`, the client uses an ephemeral port (dynamically assigned by the operating system) and connects to the server port, in this case port `1433`. The server replies with its own `SYN` packet with the `ACK` flag also set. Finally, the client responds with an `ACK` packet to let the server know it received its `SYN` packet.

This step establishes a basic TCP connection, the same way a `telnet` command would. The operating system mediates this part of the conversation. At this point, the client and server know nothing about each other.

:::image type="content" source="media/database-engine-connection-open-network-trace/three-way-handshake.png" alt-text="Diagram of three-way handshake.":::

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6127  116.5776698 10.10.10.10  10.10.10.120 TCP:Flags=......S., SrcPort=60123, DstPort=1433, PayloadLen=0, Seq=4050702293, Ack=0, Win=8192 ( Ne
6128  116.5776698 10.10.10.120 10.10.10.10  TCP:Flags=...A..S., SrcPort=1433, DstPort=60123, PayloadLen=0, Seq=4095166896, Ack=4050702294, Win=
6129  116.5786458 10.10.10.10  10.10.10.120 TCP:Flags=...A...., SrcPort=60123, DstPort=1433, PayloadLen=0, Seq=4050702294, Ack=4095166897, Win=
```

In this step, the `[Bad CheckSum]` warnings are benign and are an indicator that [checksum offload](/windows-hardware/drivers/netcx/checksum-offload) is enabled. That is, they're added at a lower level in the network stack than the trace is taken. In the absence of other information, this warning indicates whether the network trace was taken on the client or the server. In this case, it appears on the initial `SYN` packet, so the trace was taken on the client.

#### Step 2. Driver handshake

Both the client driver and SQL Server need to know a bit about each other. In this handshake, the driver sends some information and requirements to the server. This information includes whether to encrypt data packets, whether to use Multiple Active Result Sets (MARS), its version number, whether to use federated authentication, the connection GUID, and so on.

The server responds with its information, such as whether it requires authentication. This sequence happens before any sort of security negotiation is performed.

:::image type="content" source="media/database-engine-connection-open-network-trace/driver-handshake.png" alt-text="Diagram of driver handshake.":::

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6130  116.5786458 10.10.10.10  10.10.10.120 TDS:Prelogin, Version = 7.1 (0x71000001), SPID = 0, PacketID = 0, Flags=...AP..., SrcPort=60123, Ds
6131  116.5805998 10.10.10.120 10.10.10.10  TDS:Response, Version = 7.1 (0x71000001), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=1433, Dst
```

#### Step 3. SSL/TLS handshake

The SSL/TLS handshake begins with the Client Hello packet and then the Server Hello packet, plus some extra packets related to Secure Channel. This step is where the security key is negotiated for encrypting packets. Normally, just the login packet is encrypted, but the client or the server could require data packets to be encrypted as well. Choosing the version of TLS happens at this stage of the login. The client or server can close the connection at this stage if the TLS versions don't line up, or they don't have any cipher suites in common.

:::image type="content" source="media/database-engine-connection-open-network-trace/ssl-tls-handshake.png" alt-text="Diagram of SSL/TLS handshake.":::

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6132  116.5835288 10.10.10.10  10.10.10.120 TLS:TLS Rec Layer-1 HandShake: Client Hello. {TLS:328, SSLVersionSelector:327, TDS:326, TCP:325, IP
6133  116.5845058 10.10.10.120 10.10.10.10  TLS:TLS Rec Layer-1 HandShake: Server Hello. Certificate. Server Hello Done. {TLS:328, SSLVersionSe
6134  116.5864588 10.10.10.10  10.10.10.120 TLS:TLS Rec Layer-1 HandShake: Client Key Exchange.; TLS Rec Layer-2 Cipher Change Spec; TLS Rec La
6135  116.5923178 10.10.10.120 10.10.10.10  TLS:TLS Rec Layer-1 Cipher Change Spec; TLS Rec Layer-2 HandShake: Encrypted Handshake Message. {TL
```

#### Step 4. Login packet

This packet is encrypted and might show as `SSL Application Data` or `TDS:Data`, depending on your network parser. If all the packets after this step also show as `SSL Application Data`, the connection is encrypted.

:::image type="content" source="media/database-engine-connection-open-network-trace/sql-login.png" alt-text="Diagram of SQL login.":::

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6136  116.5932948 10.10.10.10  10.10.10.120 TLS:TLS Rec Layer-1 SSL Application Data {TLS:328, SSLVersionSelector:327, TDS:326, TCP:325, IPv4:3
```

#### Step 5. Login confirmation

Otherwise, you see a response packet, which either confirms the login (has the login `ACK` token), or returns a `Login Failed` error message to the client.

Here's an example of what you might see in the packet hexadecimal data for a successful login:

`.C.h.a.n.g.e.d. .d.a.t.a.b.a.s.e. .c.o.n.t.e.x.t. .t.o. .'.A.d.v.e.n.t.u.r.e.W.o.r.ks'`

:::image type="content" source="media/database-engine-connection-open-network-trace/sql-login-confirmation.png" alt-text="Diagram of SQL login confirmation.":::

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6137  116.5962248 10.10.10.120 10.10.10.10  TDS:Response, Version = 7.1 (0x71000001), SPID = 96, PacketID = 1, Flags=...AP..., SrcPort=1433, Ds
```

#### Step 6. Execute a command and read the response

Commands are sent as either a `TDS:SQLBatch` or a `TDS:RPCRequest` packet. The former executes plain Transact-SQL statements, and the latter executes stored procedures. You might see TCP continuation packets if the command is lengthy, or in the Response packet if more than a few rows are returned.

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6138  116.5991538 10.10.10.10  10.10.10.120 TDS:SQLBatch, Version = 7.1 (0x71000001), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=60123, Ds
6139  116.5991538 10.10.10.120 10.10.10.10  TDS:Response, Version = 7.1 (0x71000001), SPID = 96, PacketID = 1, Flags=...AP..., SrcPort=1433, Ds
6266  116.8032558 10.10.10.10  10.10.10.120 TCP:Flags=...A...., SrcPort=60123, DstPort=1433, PayloadLen=0, Seq=4050702956, Ack=4095168204, Win=
```

#### Step 7. TCP four-way closing handshake

Microsoft drivers use the four-way handshake to close connections. Many third-party drivers just reset the connection to close it, making it more difficult to distinguish between a normal and abnormal close.

The four-way handshake consists of the client sending a `FIN` packet to the server, which the server responds to with an `ACK`. The server then sends its own `FIN` packet, which the client acknowledges (`ACK`).

If the server sends a `FIN` packet first, it's an abnormal closing, most commonly seen in the SSL/TLS handshake if the client and server can't negotiate the secure channel.

:::image type="content" source="media/database-engine-connection-open-network-trace/four-way-close.png" alt-text="Diagram of four-way closing handshake.":::

```output
Frame Time Offset Source IP    Dest IP      Description
----- ----------- ------------ ------------ ---------------------------------------------------------------------------------------------------
6362  116.9097008 10.10.10.10  10.10.10.120 TCP:Flags=...A...F, SrcPort=60123, DstPort=1433, PayloadLen=0, Seq=4050702956, Ack=4095168204, Win=
6363  116.9097008 10.10.10.120 10.10.10.10  TCP:Flags=...A...., SrcPort=1433, DstPort=60123, PayloadLen=0, Seq=4095168204, Ack=4050702957, Win=
6364  116.9097008 10.10.10.120 10.10.10.10  TCP:Flags=...A...F, SrcPort=1433, DstPort=60123, PayloadLen=0, Seq=4095168204, Ack=4050702957, Win=
6366  116.9106778 10.10.10.10  10.10.10.120 TCP:Flags=...A...., SrcPort=60123, DstPort=1433, PayloadLen=0, Seq=4050702957, Ack=4095168205, Win=
```

## [Windows (Kerberos)](#tab/kerberos)

This process describes the authentication process, specifically comparing the SQL authentication sequence with the addition of an SSPI packet. The example network trace delineates the following steps:

1. TCP three-way handshake
1. Driver handshake
1. SSL/TLS handshake
1. Login packet exchange
1. SSPI packet transmission
1. Login confirmation
1. TCP four-way closing handshake

### Example network trace

This login sequence looks similar to the SQL authentication sequence. The addition of the SSPI packet is the big difference, but is hard to tell unless you expand its properties. On occasion, you might see more than one SSPI packet.

The client then responds with credentials, which the server confirms with the domain controller on its end.

This exchange is allocated 1 second regardless of the `Login Timeout` setting in the connection string.

- The client IP address is `10.10.10.10`
- The server IP address is `10.10.10.20`

#### Step 1. TCP three-way handshake

This step is where a connection is established between the client and the server via TCP. It involves `SYN`, `SYN-ACK`, and `ACK` packets to synchronize sequence numbers and establish communication.

:::image type="content" source="media/database-engine-connection-open-network-trace/three-way-handshake.png" alt-text="Diagram of three-way handshake.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  216  16.9554967 10.10.10.10 10.10.10.20 TCP:Flags=......S., SrcPort=49299, DstPort=1433, PayloadLen=0, Seq=2243174743, Ack=0, Win=64240 ( N
  217  16.9561482 10.10.10.20 10.10.10.10 TCP:Flags=...A..S., SrcPort=1433, DstPort=49299, PayloadLen=0, Seq=329197620, Ack=2243174744, Win=8
  218  16.9562004 10.10.10.10 10.10.10.20 TCP:Flags=...A...., SrcPort=49299, DstPort=1433, PayloadLen=0, Seq=2243174744, Ack=329197621, Win=8
```

In this step, the `[Bad CheckSum]` warnings are benign and are an indicator that [checksum offload](/windows-hardware/drivers/netcx/checksum-offload) is enabled. That is, they're added at a lower level in the network stack than the trace is taken. In the absence of other information, this warning indicates whether the network trace was taken on the client or the server. In this case, it appears on the initial `SYN` packet, so the trace was taken on the client.

#### Step 2. Driver handshake

This step involves the exchange of TDS (Tabular Data Stream) packets between the client and server to establish parameters for communication.

:::image type="content" source="media/database-engine-connection-open-network-trace/driver-handshake.png" alt-text="Diagram of driver handshake.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  219  16.9567950 10.10.10.10 10.10.10.20 TDS:Prelogin, Version = 7.4 (0x74000004), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=49299, Ds
  220  17.0035332 10.10.10.20 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=1433, Dst
```

#### Step 3. SSL/TLS handshake

Here, the client and server engage in a secure handshake to establish an encrypted connection. This process involves multiple packets including Client Hello, Server Hello, certificate exchange, and cipher change specifications.

:::image type="content" source="media/database-engine-connection-open-network-trace/ssl-tls-handshake.png" alt-text="Diagram of SSL/TLS handshake.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  221  17.0041297 10.10.10.10 10.10.10.20 TLS:TLS Rec Layer-1 HandShake: Client Hello. {TLS:37, SSLVersionSelector:36, TDS:35, TCP:34, IPv4:3
  222  17.0081618 10.10.10.20 10.10.10.10 TLS:TLS Rec Layer-1 HandShake: Server Hello. Certificate. Server Key Exchange. Server Hello Done. {
  223  17.0102991 10.10.10.10 10.10.10.20 TLS:TLS Rec Layer-1 HandShake: Client Key Exchange.; TLS Rec Layer-2 Cipher Change Spec; TLS Rec La
  224  17.0120222 10.10.10.20 10.10.10.10 TLS:TLS Rec Layer-1 Cipher Change Spec; TLS Rec Layer-2 HandShake: Encrypted Handshake Message. {TL
```

#### Step 4. Login packet

The client sends a TDS packet containing login information to the server.

:::image type="content" source="media/database-engine-connection-open-network-trace/kerberos-login.png" alt-text="Diagram of Kerberos login packet.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  236  17.0264540 10.10.10.10 10.10.10.20 TDS:Data, Version = 7.4 (0x74000004), Reassembled Packet {TDS:35, TCP:34, IPv4:33}
  237  17.0268945 10.10.10.20 10.10.10.10 TCP:Flags=...A...., SrcPort=1433, DstPort=49299, PayloadLen=0, Seq=329198912, Ack=2243178543, Win=8
```

#### Step 5. SSPI packet transmission

This packet carries Security Support Provider Interface (SSPI) token, indicating authentication using Kerberos. Its properties are expanded to reveal token data.

The following output shows the SSPI packet, with packet properties expanded.

:::image type="content" source="media/database-engine-connection-open-network-trace/kerberos-sspi-packet.png" alt-text="Diagram of Kerberos SSPI packet.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  238  17.0280923 10.10.10.20 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 0, PacketID = 0, Flags=...AP..., SrcPort=1433, Dst

  Frame: Number = 238, Captured Frame Length = 250, MediaType = ETHERNET
+ Ethernet: Etype = Internet IP (IPv4),DestinationAddress:[00-15-5D-03-F6-03],SourceAddress:[00-15-5D-03-F6-00]
+ Ipv4: Src = 10.10.10.20, Dest = 10.10.10.10, Next Protocol = TCP, Packet ID = 26363, Total IP Length = 236
+ Tcp: Flags=...AP..., SrcPort=1433, DstPort=49299, PayloadLen=196, Seq=329198912 - 329199108, Ack=2243178543, Win=8212 (scale factor 0x8) = 
- Tds: Response, Version = 7.4 (0x74000004), SPID = 0, PacketID = 0, Flags=...AP..., SrcPort=1433, DstPort=49299, PayloadLen=196, Seq=3291989
  + PacketHeader: SPID = 0, Size = 196, PacketID = 0, Window = 0
  - TDSServerResponseData: 
     TokenType: SSPI   <---- SSPI Token
   + TokenData: 
```

#### Step 6. Login confirmation and acknowledgment

The server responds with a TDS packet confirming the login and acknowledging the receipt of data.

:::image type="content" source="media/database-engine-connection-open-network-trace/kerberos-acknowledgement.png" alt-text="Diagram of Kerberos login and acknowledgement.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  239  17.0294294 10.10.10.20 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 57, PacketID = 1, Flags=...AP..., SrcPort=1433, Ds
  240  17.0294472 10.10.10.10 10.10.10.20 TCP:Flags=...A...., SrcPort=49299, DstPort=1433, PayloadLen=0, Seq=2243178543, Ack=329199489, Win=8
```

#### Step 7. TCP four-way closing handshake

Finally, the client and server close the connection gracefully using `FIN` and `ACK` packets.

:::image type="content" source="media/database-engine-connection-open-network-trace/four-way-close.png" alt-text="Diagram of four-way closing handshake.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------------------------------
  242  18.5494518 10.10.10.10 10.10.10.20 TCP:Flags=...A...F, SrcPort=49299, DstPort=1433, PayloadLen=0, Seq=2243178543, Ack=329199489, Win=8
  243  18.5501180 10.10.10.20 10.10.10.10 TCP:Flags=...A...., SrcPort=1433, DstPort=49299, PayloadLen=0, Seq=329199489, Ack=2243178544, Win=8
  244  18.5502723 10.10.10.20 10.10.10.10 TCP:Flags=...A...F, SrcPort=1433, DstPort=49299, PayloadLen=0, Seq=329199489, Ack=2243178544, Win=8
  245  18.5502896 10.10.10.10 10.10.10.20 TCP:Flags=...A...., SrcPort=49299, DstPort=1433, PayloadLen=0, Seq=2243178544, Ack=329199490, Win=8
```

### Remarks

The presence of the SSPI packet distinguishes this login process from SQL authentication. However, identification might require expanding packet properties, particularly when multiple SSPI packets are exchanged.

## [Windows (NTLM)](#tab/ntlm)

This process describes the authentication process, specifically comparing the SQL authentication sequence with the addition of NTLM challenge and response packets. The example network trace delineates the following steps:

1. TCP three-way handshake
1. Driver handshake
1. SSL/TLS handshake
1. Login packet exchange
1. NTLM challenge and response packets
1. Login confirmation
1. Command execution

### Example network trace

The difference between this login sequence and the SQL authentication sequence is the addition of two packets.

The client then responds with credentials, which the server confirms with the domain controller on its end.

This exchange is allocated 1 second regardless of the `Login Timeout` setting in the connection string.

- The client IP address is `10.10.10.120`
- The server IP address is `10.10.10.55`

#### Step 1. TCP three-way handshake

This step is where a connection is established between the client and the server via TCP. It involves `SYN`, `SYN-ACK`, and `ACK` packets to synchronize sequence numbers and establish communication.

:::image type="content" source="media/database-engine-connection-open-network-trace/three-way-handshake.png" alt-text="Diagram of three-way handshake.":::

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76078 1181.9915832 10.10.10.120 10.10.10.55  TCP: [Bad CheckSum]Flags=......S., SrcPort=64444, DstPort=57139, PayloadLen=0, Seq=2766542083, Ack=
76079 1181.9922255 10.10.10.55  10.10.10.120 TCP:Flags=...A..S., SrcPort=57139, DstPort=64444, PayloadLen=0, Seq=3862866646, Ack=2766542084, Win
76080 1181.9924272 10.10.10.120 10.10.10.55  TCP: [Bad CheckSum]Flags=...A...., SrcPort=64444, DstPort=57139, PayloadLen=0, Seq=2766542084, Ack=
```

In this step, the `[Bad CheckSum]` warnings are benign and are an indicator that [checksum offload](/windows-hardware/drivers/netcx/checksum-offload) is enabled. That is, they're added at a lower level in the network stack than the trace is taken. In the absence of other information, this warning indicates whether the network trace was taken on the client or the server. In this case, it appears on the initial `SYN` packet, so the trace was taken on the client.

#### Step 2. Driver handshake

This step involves the exchange of TDS (Tabular Data Stream) packets between the client and server to establish parameters for communication.

:::image type="content" source="media/database-engine-connection-open-network-trace/driver-handshake.png" alt-text="Diagram of driver handshake.":::

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76081 1181.9936195 10.10.10.120 10.10.10.55  TDS:Prelogin, Version = 7.3 (0x730a0003), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=64444, Ds
76082 1181.9945238 10.10.10.55  10.10.10.120 TDS:Response, Version = 7.3 (0x730a0003), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=57139, Ds
```

#### Step 3. SSL/TLS handshake

Here, the client and server engage in a secure handshake to establish an encrypted connection. This process involves multiple packets including Client Hello, Server Hello, certificate exchange, and cipher change specifications.

:::image type="content" source="media/database-engine-connection-open-network-trace/ssl-tls-handshake.png" alt-text="Diagram of SSL/TLS handshake.":::

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76083 1181.9953108 10.10.10.120 10.10.10.55  TLS:TLS Rec Layer-1 HandShake: Client Hello. {TLS:3192, SSLVersionSelector:3191, TDS:3190, TCP:3189
76084 1181.9967001 10.10.10.55  10.10.10.120 TLS:TLS Rec Layer-1 HandShake: Server Hello. Certificate. Server Hello Done. {TLS:3192, SSLVersionS
76085 1181.9978947 10.10.10.120 10.10.10.55  TLS:TLS Rec Layer-1 HandShake: Client Key Exchange.; TLS Rec Layer-2 Cipher Change Spec; TLS Rec La
76086 1182.0010146 10.10.10.55  10.10.10.120 TLS:TLS Rec Layer-1 Cipher Change Spec; TLS Rec Layer-2 HandShake: Encrypted Handshake Message. {TL
```

#### Step 4. Login packet

The client sends a TDS packet containing login information to the server.

:::image type="content" source="media/database-engine-connection-open-network-trace/ntlm-login.png" alt-text="Diagram of NTLM login packet.":::

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76096 1182.0069763 10.10.10.120 10.10.10.55  TLS:TLS Rec Layer-1 SSL Application Data {TLS:3192, SSLVersionSelector:3191, TDS:3190, TCP:3189, IP
```

#### Step 5. NTLM challenge and response packets

The NTLM challenge packet is sent from the server to the client after the client sends the login packet.

:::image type="content" source="media/database-engine-connection-open-network-trace/ntlm-challenge.png" alt-text="Diagram of NTLM challenge and response.":::

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76097 1182.0093903 10.10.10.55  10.10.10.120 NLMP:NTLM CHALLENGE MESSAGE {TDS:3190, TCP:3189, IPv4:3187}
76098 1182.0102507 10.10.10.120 10.10.10.55  NLMP:NTLM AUTHENTICATE MESSAGEVersion:v2, Domain: CONTOSO, User: joe33, Workstation: 10.10.10.120 {
```

#### Step 6. Login confirmation

The server responds with a TDS packet confirming the login and acknowledging the receipt of data.

:::image type="content" source="media/database-engine-connection-open-network-trace/ntlm-confirmation.png" alt-text="Diagram of NTLM login confirmation.":::

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76100 1182.0274716 10.10.10.55  10.10.10.120 TDS:Response, Version = 7.3 (0x730a0003), SPID = 315, PacketID = 1, Flags=...AP..., SrcPort=57139, 
```

#### Step 7. Execute a command

```output
Frame Time Offset  Source IP    Dest IP      Description
----- ------------ ------------ ------------ ---------------------------------------------------------------------------------------------------
76102 1182.0324639 10.10.10.120 10.10.10.55  TDS:SQLBatch, Version = 7.3 (0x730a0003), SPID = 0, PacketID = 1, Flags=...AP..., SrcPort=64444, Ds
```

### Remarks

The connection can fail with a timeout, resulting from any of the following reasons:

- a thread-starved SQL Server
- several simultaneous login requests resulting in queued authentication with the domain controller
- a domain controller in a different geographical location
- a slow or nonresponsive domain controller

## [MARS](#tab/mars)

This section describes how you can determine a MARS connection from the following packets:

- `SMP:SYN` starts a new session
- `SMP:ACK` acknowledges data packets
- `SMP:FIN` terminates a session

The following trace examples show the various packets.

- The client IP address is `10.10.10.10`
- The server IP address is `10.10.10.22`

#### Open a new MARS connection

The following example output assumes that the following steps are already complete, using NTLM authentication. MARS can also be used with Kerberos and SQL authentication.

1. TCP three-way handshake
1. Driver handshake
1. SSL/TLS handshake
1. Login packet exchange
1. NTLM challenge and response packets
1. Login confirmation

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------
 6704 568.0608108 10.10.10.10 10.10.10.22 TCP:Flags=CE....S., SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66183290
 6713 568.0608483 10.10.10.22 10.10.10.10 TCP: [Bad CheckSum]Flags=.E.A..S., SrcPort=1433, DstPort=52965, PayloadLen=
 6754 568.0613015 10.10.10.10 10.10.10.22 TCP:Flags=...A...., SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66183290
 6777 568.0615479 10.10.10.10 10.10.10.22 TDS:Prelogin, Version = 7.4 (0x74000004), SPID = 0, PacketID = 1, Flags=...
 6786 568.0616817 10.10.10.22 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 0, PacketID = 1, Flags=...
 6833 568.0622426 10.10.10.10 10.10.10.22 TLS:TLS Rec Layer-1 HandShake: Client Hello. {TLS:165, SSLVersionSelector:1
 6873 568.0627953 10.10.10.22 10.10.10.10 TLS:TLS Rec Layer-1 HandShake: Server Hello. Certificate. Server Key Exchan
 6900 568.0632639 10.10.10.10 10.10.10.22 TCP:Flags=...A...., SrcPort=52965, DstPort=1433, PayloadLen=0, Seq=66183319
 6977 568.0643795 10.10.10.10 10.10.10.22 TLS:TLS Rec Layer-1 HandShake: Client Key Exchange.; TLS Rec Layer-2 Cipher
 7045 568.0655160 10.10.10.22 10.10.10.10 TLS:TLS Rec Layer-1 Cipher Change Spec; TLS Rec Layer-2 HandShake: Encrypte
 7233 568.0679639 10.10.10.10 10.10.10.22 TDS:Data, Version = 7.4 (0x74000004), Reassembled Packet {TDS:162, TCP:160,
 7275 568.0684467 10.10.10.22 10.10.10.10 NLMP:NTLM CHALLENGE MESSAGE {TDS:162, TCP:160, IPv4:1}
 7331 568.0692389 10.10.10.10 10.10.10.22 NLMP:NTLM AUTHENTICATE MESSAGE Version:NTLM v2, Domain: CONTOSO, User: joe1
11791 568.1295675 10.10.10.22 10.10.10.10 TCP: [Bad CheckSum]Flags=...A...., SrcPort=1433, DstPort=52965, PayloadLen=
17978 568.2162145 10.10.10.22 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 255, PacketID = 1, Flags=.
```

#### Create a new MARS session

After the connection is established, create a new MARS session (`Sid = 0`).

:::image type="content" source="media/database-engine-connection-open-network-trace/mars-created.png" alt-text="Diagram of new MARS session.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------
18024 568.2170301 10.10.10.10 10.10.10.22 SMP:SYN, Sid = 0, Length = 16, SeqNum = 0, Wndw = 4 {SMP:190, TCP:160, IPv4
```

#### Execute various commands on the session

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------
18028 568.2170301 10.10.10.10 10.10.10.22 TDS:SQLBatch, Version = 7.4 (0x74000004), SPID = 0, PacketID = 1, Flags=...
18031 568.2170676 10.10.10.22 10.10.10.10 TCP: [Bad CheckSum]Flags=...A...., SrcPort=1433, DstPort=52965, PayloadLen=
18038 568.2173641 10.10.10.22 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 255, PacketID = 1, Flags=.
18079 568.2178650 10.10.10.10 10.10.10.22 TDS:SQLBatch, Version = 7.4 (0x74000004), SPID = 0, PacketID = 1, Flags=...
```

#### Example of the SMP:ACK packet

:::image type="content" source="media/database-engine-connection-open-network-trace/mars-packet.png" alt-text="Diagram of MARS SMP:ACK packet.":::

```output
Frame Time Offset Source IP   Dest IP     Description
----- ----------- ----------- ----------- ---------------------------------------------------------------------------
40874 568.5121135 10.10.10.22 10.10.10.10 TDS:Response, Version = 7.4 (0x74000004), SPID = 255, PacketID = 1, Flags=.
40876 568.5121237 10.10.10.22 10.10.10.10 TDS:Continuous Response, Version = 7.4 (0x74000004), SPID = 255, PacketID =
40911 568.5124644 10.10.10.10 10.10.10.22 SMP:ACK, Sid = 0, Length = 16, SeqNum = 34, Wndw = 40 {SMP:190, TCP:160, IP
40950 568.5128422 10.10.10.22 10.10.10.10 TDS:Continuous Response, Version = 7.4 (0x74000004), SPID = 255, PacketID =
```

---

## Related content

- [Trace the network connection close sequence on the Database Engine](database-engine-connection-close-network-trace.md)
- [Connect to the Database Engine](../sql-server/connect-to-database-engine.md)
- [Configure SQL Server to listen on a specific TCP port](../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md)
