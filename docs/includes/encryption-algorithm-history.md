---
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.topic: include
ms.custom:
  - ignite-2025
---

Beginning with [!INCLUDE [sssql11-md](sssql11-md.md)], SQL Server and Azure SQL Database used a SHA-512 hash combined with a 32-bit random and unique salt. This method made it statistically infeasible for attackers to deduce passwords.

[!INCLUDE [sssql25-md](sssql25-md.md)] introduces an iterated hash algorithm, RFC2898, also known as a *password-based key derivation function* (PBKDF). This algorithm still uses SHA-512 but hashes the password multiple times (100,000 iterations), significantly slowing down brute-force attacks. This change enhances password protection in response to evolving security threats and helps customers comply with NIST SP 800-63b guidelines. This security enhancement uses a stronger hashing algorithm, which can slightly increase login time for SQL Authentication logins. The impact is generally lower in environments with connection pooling, but might be more noticeable in scenarios without pooling or where login latency is closely monitored.
