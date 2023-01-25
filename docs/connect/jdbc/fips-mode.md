---
title: FIPS mode
description: Learn how to use the JDBC Driver for SQL Server in FIPS mode to keep your application FIPS 140 compliant.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: mikeray
ms.date: 03/21/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# FIPS mode

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The Microsoft JDBC Driver for SQL Server supports running in JVMs configured to be *FIPS 140 Compliant*.

## Prerequisites

- FIPS configured JVM
- Appropriate TLS/SSL Certificate
- Appropriate policy files
- Appropriate Configuration parameters

## FIPS configured JVM

Generally, applications can configure the `java.security` file to use FIPS-compliant crypto providers. See the documentation specific to your JVM for how to configure FIPS 140 compliance.

To see the approved modules for FIPS Configuration, refer to [Validated Modules in the Cryptographic Module Validation Program](https://csrc.nist.gov/Projects/cryptographic-module-validation-program/Validated-Modules).

Vendors may have some more steps to configure a JVM with FIPS.

## Appropriate TLS certificate

To connect to SQL Server in FIPS mode, a valid TLS/SSL Certificate is required. Install or import it into the Java Key Store on the client machine (JVM) where FIPS is enabled.

### Importing TLS certificate in Java keyStore

For FIPS, most likely you need to import the certificate (.cert) in either PKCS or a provider-specific format.
Use the following snippet to import the TLS/SSL certificate and store it in a working directory with the appropriate KeyStore format. _TRUST\_STORE\_PASSWORD_ is your password for Java KeyStore.

```java
public void saveGenericKeyStore(
        String provider,
        String trustStoreType,
        String certName,
        String certPath
        ) throws KeyStoreException, CertificateException,
            NoSuchAlgorithmException, NoSuchProviderException,
            IOException
{
    KeyStore ks = KeyStore.getInstance(trustStoreType, provider);
    FileOutputStream os = new FileOutputStream("./MyTrustStore_" + trustStoreType);
    ks.load(null, null);
    ks.setCertificateEntry(certName, getCertificate(certPath));
    ks.store(os, TRUST_STORE_PASSWORD.toCharArray());
    os.flush();
    os.close();
}

private Certificate getCertificate(String pathName)
        throws FileNotFoundException, CertificateException
{
    FileInputStream fis = new FileInputStream(pathName);
    CertificateFactory cf = CertificateFactory.getInstance("X.509");
    return cf.generateCertificate(fis);
}
```

The following example is importing an Azure TLS/SSL Certificate in PKCS12 format with the BouncyCastle Provider. The certificate is imported in the working directory named _MyTrustStore\_PKCS12_ by using the following snippet:

`saveGenericKeyStore(BCFIPS, PKCS12, "SQLAzure SSL Certificate Name", "SQLAzure.cer");`

## Appropriate policy files

For some FIPS Providers, unrestricted Policy jars are needed. In such cases, for Sun / Oracle, download the Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files for [JRE 8](https://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html) or [JRE 7](https://www.oracle.com/technetwork/java/javase/downloads/jce-7-download-432124.html).

## Appropriate configuration parameters

To run the JDBC Driver in FIPS-compliant mode, configure connection properties as shown in following table.

### Properties

|Property|Type|Default|Description|Notes|
|---|---|---|---|---|
|`encrypt`|boolean ["true / false"]|"false"|For FIPS enabled JVM encrypt property should be **true**||
|`TrustServerCertificate`|boolean ["true / false"]|"false"|For FIPS, the user needs to validate certificate chain, so the user should use **"false"** value for this property. ||
|`trustStore`|String|null|Your Java Keystore file path where you imported your certificate. If you install certificate on your system, then no need to pass anything. Driver uses cacerts or jssecacerts files.||
|`trustStorePassword`|String|null|The password used to check the integrity of the trustStore data.||
|`fips`|boolean ["true / false"]|"false"|For FIPS enabled JVM this property should be **true**|Added in 6.1.4 (Stable release 6.2.2)|
|`fipsProvider`|String|null|FIPS provider configured in JVM. For example, BCFIPS or SunPKCS11-NSS |Added in 6.1.2 (Stable release 6.2.2), deprecated in 6.4.0 - see the details [Here](https://github.com/Microsoft/mssql-jdbc/pull/460).|
|`trustStoreType`|String|JKS|For FIPS mode set trust store type either PKCS12 or type defined by FIPS provider |Added in 6.1.2 (Stable release 6.2.2)|
