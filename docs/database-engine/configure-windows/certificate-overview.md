---
title: Transport Layer Security and digital certificates
description: This article describes Transport Layer Security and digital certificates.
author: sevend2
ms.author: v-sidong
ms.reviewer: sureshka, randolphwest
ms.date: 12/08/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Transport Layer Security and digital certificates

This article describes details about the protocol Transport Layer Security (TLS) and digital certificates.

## Transport Layer Security (TLS)

The [TLS and SSL protocols](/windows-server/security/tls/transport-layer-security-protocol) are located between the application protocol layer and the TCP/IP layer, where they can secure and send application data to the transport layer. TLS/SSL protocols use algorithms from a [cipher suite](/windows/win32/secauthn/cipher-suites-in-schannel) to create keys and encrypt information. The client and server negotiate the protocol version and cipher suite to be used for encryption during the initial connection (pre-login) phase of connection establishment. The highest supported TLS version is always preferred in the TLS handshake. To check the TLS protocols versions supported by different version of Windows operating systems, see [Protocols in TLS/SSL (Schannel SSP)](/windows/win32/secauthn/protocols-in-tls-ssl--schannel-ssp-#tls-protocol-version-support). Several known vulnerabilities have been reported against SSL and earlier versions of TLS. We recommend that you upgrade to TLS 1.2 for secure communication.

SQL Server can use TLS to encrypt data that is transmitted across a network between an instance of SQL Server and a client application. TLS uses a certificate to implement encryption.

Enabling TLS encryption increases the security of data transmitted across networks between instances of SQL Server and applications. However, when all traffic between SQL Server and a client application is encrypted by using TLS, the following extra processing is required:

- An extra network roundtrip is required at connect time.
- Packets sent from the application to the instance of SQL Server must be encrypted by the client TLS stack and decrypted by the server TLS stack.
- Packets sent from the instance of SQL Server to the application must be encrypted by the server TLS stack and decrypted by the client TLS stack.

> [!IMPORTANT]  
> Starting with SQL Server 2016 (13.x), Secure Sockets Layer (SSL) has been discontinued. Use TLS (TLS 1.2 is recommended) instead. For more information, see [KB3135244 - TLS 1.2 support for Microsoft SQL Server](https://support.microsoft.com/topic/kb3135244-tls-1-2-support-for-microsoft-sql-server-e4472ef8-90a9-13c1-e4d8-44aad198cdbe). SQL Server 2022 introduces support for TLS 1.3. For more information, see [TLS 1.3 support](../../relational-databases/security/networking/tls-1-3.md).
> If no matching protocols exist between the client and server computer, you can run into the error described in [An existing connection was forcibly closed by the remote host](/troubleshoot/sql/connect/tls-exist-connection-closed).

## Digital certificate overview

Digital certificates are electronic files that work like an online password to verify the identity of a user or a computer. They're used to create the encrypted channel that's used for client communications. A certificate is a digital statement that's issued by a certification authority (CA) that vouches for the identity of the certificate holder and enables the parties to communicate securely by using encryption.

Digital certificates provide the following services:

- **Encryption**: They help protect the data that's exchanged from theft or tampering.
- **Authentication**: They verify that their holders (people, web sites, and even network devices such as routers) are truly who or what they claim to be. Typically, the authentication is one-way, where the source verifies the identity of the target, but mutual TLS authentication is also possible.

A certificate contains a public key and attaches that public key to the identity of a person, computer, or service that holds the corresponding private key. The public and private keys are used by the client and the server to encrypt data before it's transmitted. For Windows users, computers, and services, trust in the CA is established when the root certificate is defined in the trusted root certificate store, and the certificate contains a valid certification path. A certificate is considered valid if it hasn't been revoked (it isn't in the CA's certificate revocation list or CRL) or expired.

The three primary types of digital certificates are described in the following table:

| Type | Description | Advantages | Disadvantages |
| --- | --- | --- | --- |
| Self-signed certificate | The certificate is signed by the application that created it or is created by using [New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate). | Cost (free) | - The certificate isn't automatically trusted by client computers and mobile devices. The certificate needs to be manually added to the trusted root certificate store on all client computers and devices, but not all mobile devices allow changes to the trusted root certificate store.<br /><br />- Not all services work with self-signed certificates.<br /><br />- Difficult to establish an infrastructure for certificate lifecycle management. For example, self-signed certificates can't be revoked. |
| Certificate issued by an internal CA | The certificate is issued by a public key infrastructure (PKI) in your organization. An example is Active Directory Certificate Services (AD CS). For more information, see [Active Directory Certificate Services Overview](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/hh831740(v=ws.11)). | - Allows organizations to issue their own certificates.<br /><br />- Less expensive than certificates from a commercial CA. | - Increased complexity to deploy and maintain the PKI.<br /><br />- The certificate isn't automatically trusted by client computers and mobile devices. The certificate needs to be manually added to the trusted root certificate store on all client computers and devices, but not all mobile devices allow changes to the trusted root certificate store. |
| Certificate issued by a commercial CA | The certificate is purchased from a trusted commercial CA. | Certificate deployment is simplified because all clients, devices, and servers automatically trust the certificates. | Cost. You need to plan ahead to minimize the number of certificates that are required. |

To prove that a certificate holder is who they claim to be, the certificate must accurately identify the certificate holder to other clients, devices, or servers. The three basic methods to do this are described in the following table:

| Method | Description | Advantages | Disadvantages |
| --- | --- | --- | --- |
| Certificate subject match | The certificate's **Subject** field contains the common name (CN) of the host. For example, the certificate that's issued to `www.contoso.com` can be used for the web site `https://www.contoso.com`. | - Compatible with all clients, devices, and services.<br /><br />- Compartmentalization. Revoking the certificate for a host doesn't affect other hosts. | - Number of certificates required. You can only use the certificate for the specified host. For example, you can't use the `www.contoso.com` certificate for `ftp.contoso.com`, even when the services are installed on the same server.<br /><br />- Complexity. On a web server, each certificate requires its own IP address binding. |
| Certificate subject alternative name (SAN) match | In addition to the **Subject** field, the certificate's **Subject Alternative Name** field contains a list of multiple host names. For example:<br />`www.contoso.com`<br />`ftp.contoso.com`<br />`ftp.eu.fabirkam.net` | - Convenience. You can use the same certificate for multiple hosts in multiple, separate domains.<br /><br />- Most clients, devices, and services support SAN certificates.<br /><br />- Auditing and security. You know exactly which hosts are capable of using the SAN certificate. | - More planning required. You need to provide the list of hosts when you create the certificate.<br /><br />- Lack of compartmentalization. You can't selectively revoke certificates for some of the specified hosts without affecting all of the hosts in the certificate. |
| Wildcard certificate match | The certificate's **Subject** field contains the common name as the wildcard character (*) plus a single domain or subdomain. For example, `*.contoso.com` or `*.eu.contoso.com`. The `*.contoso.com` wildcard certificate can be used for:<br />`www.contoso.com`<br />`ftp.contoso.com`<br />`mail.contoso.com` | Flexibility. You don't need to provide a list of hosts when you request the certificate, and you can use the certificate on any number of hosts that you may need in the future. | - You can't use wildcard certificates with other top-level domains (TLDs). For example, you can't use the `*.contoso.com` wildcard certificate for `*.contoso.net` hosts.<br /><br />- You can only use wildcard certificates for host names at the level of the wildcard. For example, you can't use the `*.contoso.com` certificate for `www.eu.contoso.com`. Or, you can't use the `*.eu.contoso.com` certificate for `www.uk.eu.contoso.com`.<br /><br />- Older clients, devices, applications, or services might not support wildcard certificates.<br /><br />- Wildcards aren't available with Extended Validation (EV) certificates.<br /><br />- Careful auditing and control are required. If the wildcard certificate is compromised, it affects every host in the specified domain. |

## Related content

- [Connect to SQL Server with strict encryption](../../relational-databases/security/networking/connect-with-strict-encryption.md)
- [Configure TLS 1.3](../../relational-databases/security/networking/connect-with-tls-1-3.md)
- [TLS 1.3 support](../../relational-databases/security/networking/tls-1-3.md)
