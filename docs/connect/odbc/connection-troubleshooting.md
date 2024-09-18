---
title: Connection encryption troubleshooting
description: Learn how to troubleshoot common connection issues related to connection encryption.
author: David-Engel
ms.author: davidengel
ms.reviewer: vanto
ms.date: 09/16/2024
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connection encryption troubleshooting in the ODBC driver

## Certificate chain errors

**If you see "SSL Provider: The certificate chain was issued by an authority that is not trusted." or "SSL routines::certificate verify failed: unable to get local issuer certificate" in your error:**

- Connection encryption is enabled by default in version 18 and newer. Users switching from previous versions of ODBC might see these errors if connection encryption was previously not used.
- Users can choose to set the `Encrypt` connection string keyword to `no`/`optional` to disable connection encryption to match the default behavior prior to version 18. In the DSN Configuration UI, this option is set using the `Connection Encryption` dropdown list.
- If connection encryption is desired, `TrustServerCertificate` can also be set to `yes` to skip server certificate validation.

## Certificate name errors

**If you see "SSL Provider: The target principal name is incorrect." or "SSL routines::certificate verify failed:subject name does not match host name" in your error:**

- Users might see this error if the host name in the certificate returned by the server doesn't match what is expected. By default, the server name is used to check against the certificate.
- The `HostNameInCertificate` keyword can be used to specify the name expected from the server certificate.
- Alternatively, a certificate can also be specified to match and verify the returned server certificate against by using the `ServerCertificate` keyword (v18.1+).
- You might also use `TrustServerCertificate` to skip server certificate validation.

For more information, see [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md).

--------------------------------------------------

## Related content

- [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md)
- [Data Source Wizard Screen 4](windows/dsn-wizard-4.md)
