---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/12/2026
ms.service: sql
ms.topic: include
---

## Login state values

The following table describes `login_state` and `login_state_desc`.

| `login_state` | `login_state_desc` | Details |
| --- | --- | --- |
| `0` | `INITIAL` | Connection handshake is initializing. |
| `1` | `WAIT LOGIN NEGOTIATE` | Connection handshake is waiting for Login Negotiate message. |
| `2` | `ONE ISC` | Connection handshake initialized and sent security context for authentication. |
| `3` | `ONE ASC` | Connection handshake received and accepted security context for authentication. |
| `4` | `TWO ISC` | Connection handshake initialized and sent security context for authentication. There's an optional mechanism available for authenticating the peers. |
| `5` | `TWO ASC` | Connection handshake received and sent accepted security context for authentication. There's an optional mechanism available for authenticating the peers. |
| `6` | `WAIT ISC Confirm` | Connection handshake is waiting for Initialize Security Context Confirmation message. |
| `7` | `WAIT ASC Confirm` | Connection handshake is waiting for Accept Security Context Confirmation message. |
| `8` | `WAIT REJECT` | Connection handshake is waiting for SSPI rejection message for failed authentication. |
| `9` | `WAIT PRE-MASTER SECRET` | Connection handshake is waiting for Pre-Master Secret message. |
| `10` | `WAIT VALIDATION` | Connection handshake is waiting for Validation message. |
| `11` | `WAIT ARBITRATION` | Connection handshake is waiting for Arbitration message. |
| `12` | `ONLINE` | Connection handshake is complete and is online (ready) for message exchange. |
| `13` | `ERROR` | Connection is in error. |
