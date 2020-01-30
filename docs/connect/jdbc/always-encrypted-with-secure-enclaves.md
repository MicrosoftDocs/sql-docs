---
title: "Using Always Encrypted with secure enclaves | Microsoft Docs"
ms.custom: ""
ms.date: "01/16/2020"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 271c0438-8af1-45e5-b96a-4b1cabe32707
author: reneye
ms.author: v-reye
---
# Using Always Encrypted with the secure enclaves
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This page provides information on how to develop Java applications using [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md) and the Microsoft JDBC Driver 8.2 (or higher) for SQL Server.

Secure enclaves are an addition to the existing [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md) feature. The purpose of secure enclaves is to address limitations when working with Always Encrypted data. Currently, users can only perform equality comparisons on Always Encrypted data, and must retrieve and decrypt the data in order to perform other operations. Secure enclaves addresses this limitation y allowing computations on plaintext data inside a secure enclave on the server side. A secure enclave is a protected region of memory within the SQL Server process, and acts as a trusted execution environment for processing sensitive data inside the SQL Server engine. A secure enclave appears as a black box to the rest of the SQL Server and other processes on the hosting machine. There is no way to view any data or code inside the enclave from the outside, even with a debugger.

## Prerequisites
- Make sure Microsoft JDBC Driver 8.2 (or higher) for SQL Server is installed on your development machine. 
- Download and install the Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files.  Be sure to read the Readme included in the zip file for installation instructions and relevant details on possible export/import issues.  

    - If using the mssql-jdbc-X.X.X.jre8.jar or sqljdbc42.jar, the policy files can be downloaded from  [Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files 8 Download](https://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html)

    - If using the mssql-jdbc-X.X.X.jre9.jar or above, no policy file needs to be downloaded. The jurisdiction policy in Java 9 defaults to unlimited strength encryption.

## Setting up secure enclaves
Follow this [tutorial](../../relational-databases/security/encryption/tutorial-getting-started-with-always-encrypted-enclaves.md) to get started with secure enclaves. For more in depth information, please refer to [Always encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md).

## Connection String properties
**enclaveAttestationUrl:** the endpoint url of the attestation service.

**enclaveAttestationProtocol:** the protocol of the attestation service. The only 2 supported values are **HGS**(Host Guardian Service) and **AAS**(Azure Attestation Service).

Users must enable **columnEncryptionSetting** and correctly set **both** of the above connection string properties in order to enable Always Encrypted with secure enclaves from the Microsoft SQL JDBC Driver.

## Working with secure enclaves
As long as the connection properties are set properly, the feature will work transparently. The driver's logic will change depending on whether the user's query requires a secure enclave or not, but no action is required from the user. The following code examples trigger enclave computations. You can find the database and table setup at [getting started with always encrypted enclaves](../../relational-databases/security/encryption/tutorial-getting-started-with-always-encrypted-enclaves.md).

Rich queries will trigger enclave computations:
```java
	private static final String URL = "jdbc:sqlserver://{server}:{port};user={username};password={password};databaseName=ContosoHR;columnEncryptionSetting=enabled;enclaveAttestationUrl={attestation-url};enclaveAttestationProtocol={attestation-protocol};";
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

Disabling encryption on a column will also trigger enclave computations:
```java
	private static final String URL = "jdbc:sqlserver://{server}:{port};user={username};password={password};databaseName=ContosoHR;columnEncryptionSetting=enabled;enclaveAttestationUrl={attestation-url};enclaveAttestationProtocol={attestation-protocol};";
	try (Connection c = DriverManager.getConnection(URL);Statement s = c.createStatement()) {
		s.executeUpdate("ALTER TABLE Employees ALTER COLUMN SSN CHAR(11) NULL WITH (ONLINE = ON)");
	}
```

## Java 8 Users

This feature requires the RSASSA-PSA signature algorithm. This algorithm was added in JDK 11, but not backported to JDK 8. Users who wish to use this feature with the JDK 8 version of the Microsoft SQL Server JDBC Driver must either load their own provider which supports the RSASSA-PSA signature algorithm, or include the BouncyCastleProvider optional dependency. The dependency will be removed in a later date if JDK 8 backports the signature algorithm or if the support lifecycle of JDK 8 ends.

## See also

[Using Always Encrypted with the JDBC driver](../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md)  
