---
title: Certificate rotation
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn about the upcoming changes of root certificate changes that will affect Azure SQL Database & Azure SQL Managed Instance
author: tameikal-msft
ms.author: talawren
ms.reviewer: mathoma, vanto
ms.date: 04/24/2025
ms.service: azure-sql
ms.subservice: security
ms.topic: concept-article
---

# Understanding the changes in the Root CA change for Azure SQL Database & Azure SQL Managed Instance

Azure SQL Database & Azure SQL Managed Instance will be changing the root certificate for the client application/driver enabled with Secured Sockets Layer (SSL) or Transport Layer Security (TLS), used to establish secure TDS connection. To get the latest list of root certificate authority details, see [Azure Certificate Authority details](/azure/security/fundamentals/azure-ca-details?tabs=root-and-subordinate-cas-list). This article gives you more details about the upcoming changes, the resources that will be affected, and the steps needed to ensure that your application maintains connectivity to your database server.

## What update is going to happen?

[Certificate Authority (CA) Browser forum](https://cabforum.org/) recently published reports of multiple certificates issued by CA vendors to be noncompliant.

As per the industry's compliance requirements, CA vendors began revoking CA certificates for noncompliant CAs, requiring servers to use certificates issued by compliant CAs, and signed by CA certificates from those compliant CAs. Since SQL Database & SQL Managed Instance currently use one of these noncompliant certificates, which client applications use to validate their TLS connections, we need to ensure that appropriate actions are taken to minimize the potential impact to your Azure SQL servers.

If you use full validation of the server certificate when connecting from a SQL client (`TrustServerCertificate=false`), you need to ensure that your SQL client would be able to validate new root certificate listed in [Root Certificate Authorities](/azure/security/fundamentals/azure-ca-details?tabs=root-and-subordinate-cas-list#root-certificate-authorities).

## How do I know if my application might be affected?

All applications that use SSL/TLS and verify the root certificate needs to update the root certificate in order to connect to Azure SQL Database & Azure SQL Managed Instance. 

If you aren't using SSL/TLS currently, there's no impact to your application availability. You can verify if your client application is trying to verify root certificate by looking at the connection string. If `TrustServerCertificate` is explicitly set to true, then you aren't affected.

If your client driver utilizes OS certificate store, as most drivers do, and your OS is regularly maintained this change will likely not affect you, as the root certificate we're switching to should be already available in your Trusted Root Certificate Store. Check the [Root Certificate Authorities](/azure/security/fundamentals/azure-ca-details?tabs=root-and-subordinate-cas-list#root-certificate-authorities) list and validate one of the certificates is present in your Trusted Root Certificate Store.

If your client driver utilizes local file certificate store, to avoid your application's availability being interrupted due to certificates being unexpectedly revoked, or to update a certificate, which has been revoked, refer to the [**What do I need to do to maintain connectivity**](./ssl-root-certificate-expiring.md#what-do-i-need-to-do-to-maintain-connectivity) section.

## What do I need to do to maintain connectivity

To avoid your application's availability being interrupted due to certificates being unexpectedly revoked, or to update a certificate which has been revoked, follow these steps:

- Download the following certificates from [Root Certificate Authorities](/azure/security/fundamentals/azure-ca-details?tabs=root-and-subordinate-cas-list#root-certificate-authorities).
  - DigiCert Global Root G2
  - Microsoft ECC Root Certificate Authority 2017
  - Microsoft RSA Root Certificate Authority 2017

- Import the certificates into the Trusted Root Certificate Store on the client machine. The steps to import the certificate depend on the operating system and the client driver you're using. For example, if you're using Windows, you can use the Microsoft Management Console (MMC) to import the certificate into the Trusted Root Certification Authorities store.

## What can be the impact?

If you're validating server certificates as documented here, your application's availability might be interrupted since the database won't be reachable. Depending on your application, you can receive various error messages, including but not limited to:

- Invalid certificate/revoked certificate
- Connection timed out
- Error if applicable

## Frequently asked questions

### If I'm not using SSL/TLS, do I still need to update the root CA?

No actions regarding this change are required if you aren't using SSL/TLS. Still you should set a plan for start using latest TLS version as we plan for TLS enforcement in near future.

### What will happen if I don't update the root certificate when a certificate has expired?

If you don't update the root certificate before it expires, your applications that connect via SSL/TLS and does verification for the root certificate will be unable to communicate to SQL Database & SQL Managed Instance and application will experience connectivity issues to your SQL Database & SQL Managed Instance.

### Do I need to plan a maintenance downtime for this change?

No. Since the change is only on the client side to connect to the server, there's no maintenance downtime needed here for this change.

### What if I can't get a scheduled downtime for this change?

Since the clients used for connecting to the server needs to be updating the certificate information as described in the [fix section](ssl-root-certificate-expiring.md#what-do-i-need-to-do-to-maintain-connectivity), we don't need to a downtime for the server in this case.

<a id="if-i-create-a-new-server-after-november-30-2020-will-i-be-affected"></a>

### If I create a new server after November 30, 2020, will I be affected?

For new servers, you can use the newly issued certificate for your applications to connect using SSL/TLS.

### How often does Microsoft update their certificates or what is the expiry policy?

These certificates used by SQL Database & SQL Managed Instance are provided by trusted Certificate Authorities (CA). So the support of these certificates on SQL Database & SQL Managed Instance is tied to the support of these certificates by CA. However, as in this case, there can be unforeseen bugs in these predefined certificates, which need to be fixed at the earliest.

### If I'm using read replicas, do I need to perform this update only on primary server or all the read replicas?

Since this update is a client-side change, if the client used to read data from the replica server, we need to apply the changes for those clients as well. 

### Do we have server-side query to verify if SSL/TLS is being used?

Since this configuration is client-side, information isn't available on server side.

### What if I have further questions?

If you have a support plan and you need technical help, create Azure support request, see [How to create Azure support request](/azure/azure-portal/supportability/how-to-create-azure-support-request).
