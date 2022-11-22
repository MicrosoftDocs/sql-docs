---
title: "Using Always Encrypted with secure enclaves with the JDBC Driver"
description: "Using Always Encrypted with secure enclaves with the JDBC Driver"
author: DavidEngel
ms.author: v-davidengel
ms.reviewer: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using Always Encrypted with secure enclaves with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This page provides information on how to develop Java applications using [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) and the Microsoft JDBC Driver 8.2 (or higher) for SQL Server.

The secure enclaves feature is an addition to the existing [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature. The purpose of secure enclaves is to address limitations when working with Always Encrypted data. Previously, users could only do equality comparisons on Always Encrypted data, and had to retrieve and decrypt the data to do other operations. Secure enclaves address this limitation by allowing computations on plaintext data inside a secure enclave on the server side. A secure enclave is a protected region of memory within the SQL Server process. It acts as a trusted execution environment for processing sensitive data inside the SQL Server engine. A secure enclave appears as a black box to the rest of the SQL Server and other processes on the hosting machine. There's no way to view any data or code inside the enclave from the outside, even with a debugger.

## Prerequisites

- Make sure Microsoft JDBC Driver 8.2 (or higher) for SQL Server is installed on your development machine.
- Verify environment dependencies such as DLLs, KeyStores, and so on, are in the correct paths. Always Encrypted with secure enclaves is an add-on to the existing [Always Encrypted](using-always-encrypted-with-the-jdbc-driver.md) feature and share similar prerequisites.

> [!Note]
> If you are using an older version of JDK 8, you may need to download and install the Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files. Be sure to read the Readme included in the zip file for installation instructions and relevant details on possible export/import issues.  
>
> The policy files can be downloaded from  [Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files 8 Download](https://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html)

## Setting up secure enclaves

Follow [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../../relational-databases/security/tutorial-getting-started-with-always-encrypted-enclaves.md) or [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started) to get started with secure enclaves. For more in-depth information, refer to [Always encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

## Connection String properties

To enable enclave computations for a database connection, you need to set the following connection string keywords, in addition to enabling Always Encrypted.

- **enclaveAttestationProtocol** - specifies an attestation protocol.
  - If you're using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), the value of this keyword should be `HGS`.
  - If you're using [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and Microsoft Azure Attestation, the value of this keyword should be `AAS`.
  - If you're using secure enclaves in an environment where attestation services aren't available, the value of this keyword should be `NONE`.

- **enclaveAttestationUrl:** - specifies an attestation URL (an attestation service endpoint). You need to obtain an attestation URL for your environment from your attestation service administrator.
  - If you're using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Host Guardian Service (HGS), see [Determine and share the HGS attestation URL](../../relational-databases/security/encryption/always-encrypted-enclaves-host-guardian-service-deploy.md#step-6-determine-and-share-the-hgs-attestation-url).
  - If you're using [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and Microsoft Azure Attestation, see [Determine the attestation URL for your attestation policy](../../relational-databases/security/encryption/always-encrypted-enclaves.md#secure-enclave-attestation).
  - If you're using `NONE` attestation protocol, this property can be left blank.

Users must enable **columnEncryptionSetting** and correctly set **both** of the above connection string properties to enable Always Encrypted with secure enclaves from the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)].

## Working with secure enclaves

When the enclave connection properties are set properly, the feature will work transparently. The driver will determine whether the query requires the use of a secure enclave automatically. The following are examples of queries that trigger enclave computations. You can find the database and table setup in [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../../relational-databases/security/tutorial-getting-started-with-always-encrypted-enclaves.md) or [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started).

Rich queries will trigger enclave computations:

```java
private static final String URL = "jdbc:sqlserver://<server>:<port>;encrypt=true;user=<username>;password=<password>;databaseName=ContosoHR;columnEncryptionSetting=enabled;enclaveAttestationUrl=<attestation-url>;enclaveAttestationProtocol=<attestation-protocol>;";
try (Connection c = DriverManager.getConnection(URL)) {
    try (PreparedStatement p = c.prepareStatement("SELECT * FROM Employees WHERE SSN LIKE ?")) {
        p.setString(1, "%6818");
        try (ResultSet rs = p.executeQuery()) {
            while (rs.next()) {
                // Do work with data
            }
        }
    }
    
    try (PreparedStatement p = c.prepareStatement("SELECT * FROM Employees WHERE SALARY > ?")) {
        ((SQLServerPreparedStatement) p).setMoney(1, new BigDecimal(0));
        try (ResultSet rs = p.executeQuery()) {
            while (rs.next()) {
                // Do work with data
            }
        }
    }
}
```

Toggling encryption on a column will also trigger enclave computations:

```java
private static final String URL = "jdbc:sqlserver://<server>:<port>;encrypt=true;user=<username>;password=<password>;databaseName=ContosoHR;columnEncryptionSetting=enabled;enclaveAttestationUrl=<attestation-url>;enclaveAttestationProtocol=<attestation-protocol>;";
try (Connection c = DriverManager.getConnection(URL);Statement s = c.createStatement()) {
    s.executeUpdate("ALTER TABLE Employees ALTER COLUMN SSN CHAR(11) NULL WITH (ONLINE = ON)");
}
```

## Java 8 users

This feature requires the RSASSA-PSA signature algorithm. This algorithm was added in JDK 11, but not back-ported to JDK 8. Users who wish to use this feature with the JDK 8 version of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] must either load their own provider, which supports the RSASSA-PSA signature algorithm, or include the `BouncyCastleProvider` optional dependency. The dependency will be removed at a later date if JDK 8 back ports the signature algorithm or if the support lifecycle of JDK 8 ends.

## See also

[Using Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md)
