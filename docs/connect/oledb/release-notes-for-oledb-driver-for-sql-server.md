---
title: Release notes for OLE DB Driver
description: This release notes article describes the changes in each release of the Microsoft OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 10/26/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Release notes for the Microsoft OLE DB Driver for SQL Server

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This page discusses what was added in each version of the Microsoft OLE DB Driver for SQL Server.

<!--
USE THE TABLE FORMAT!
Hello, from now on, please use the table-based format standard for all new Release Notes content.
See section "## 18.2.1" for a live example in this article.
Thank you. For questions, contact GeneMi. (2019/03/16)
-->

## 19.2.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2212594)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2212470)  

Released: October 26, 2022

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2212594&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2212470&clcid=0x40a)

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for TLS 1.3 | TDS 8.0 connections can now be configured to use TLS 1.3. For more details, see [TLS 1.3 support](../../relational-databases/security/networking/tds-8-and-tls-1-3.md). |
| Support for the `Server Certificate` connection string keyword, and the `SSPROP_INIT_SERVER_CERTIFICATE` initialization property | The user may now specify the path to a certificate file to match against the SQL Server TLS/SSL certificate. <br/><br/>For more information, see: [Using connection string keywords](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |

## Previous releases

## 19.1.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2206472)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2206473)  

Released: August 31, 2022

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2206472&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2206473&clcid=0x40a)

### Features added

| Feature added | Details |
| :------------ | :------ |
| Application-Layer Protocol Negotiation (ALPN) extension | The driver implements the Application-Layer Protocol Negotiation (ALPN) extension when the TDS 8.0 protocol is used. |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed an issue where the `SSPROP_INIT_AUTOTRANSLATE` property was ignored for `SSVARIANT` narrow string values. | Fixed a bug where setting `SSPROP_INIT_AUTOTRANSLATE` to `VARIANT_FALSE` would result in character translations for `SSVARIANT` narrow string values. |
| Fixed an issue with missing digital signatures. | Added digital signatures for the installer custom action dynamic-link libraries. |
| Fixed an issue with HostnameInCertificate property being passed through the Server Name Indication (SNI) TLS extension. | The HostnameInCertificate value is no longer present in the Server Name Indication (SNI) TLS extension. |
| Restored the functionality of the Protocol Order and the TCP Keep-Alive registry properties. | The driver now adjusts the TCP Keep-Alive and the Protocol Order properties based on the values set for the corresponding [registry entries](features/registry-settings.md#tcp-keep-alive-and-protocol-order-registry-properties). |

## 19.0.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2186934)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2186855)  

Released: February 15, 2022

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2186934&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2186855&clcid=0x40a)

### Features added

| Feature added | Details |
| :------------ | :------ |
| TDS 8.0 support | The encryption connection string keyword/property now includes the option for strict encryption, which encrypts the whole connection (including PRELOGIN packets). |
| Secure by default | **BREAKING CHANGE**<br />The driver now defaults to secure-by-default options. Encrypted connections are enabled by default. The server certificate is now validated when client-side encryption is off but the server requires encryption.<br /><br />To restore previous version behavior, you need to opt-in to non-encrypted connections (`Encrypt` or `Use Encryption for Data` option) and trust the server certificate (`Trust Server Certificate` option), if the server uses a self-signed certificate. For more information, see [Encryption and certificate validation](features/encryption-and-certificate-validation.md). |
| Support for the `Host Name In Certificate` connection string keyword, and the `SSPROP_INIT_HOST_NAME_CERTIFICATE` initialization property. | The user may now specify the host name to be used when validating the SQL Server TLS/SSL certificate. |

## 18.6.4

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2206347)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2206348)  

Released: August 31, 2022

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2206347&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2206348&clcid=0x40a)

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed an issue where the `SSPROP_INIT_AUTOTRANSLATE` property was ignored for `SSVARIANT` narrow string values. | Fixed a bug where setting `SSPROP_INIT_AUTOTRANSLATE` to `VARIANT_FALSE` would result in character translations for `SSVARIANT` narrow string values. |
| Fixed an issue with missing digital signatures. | Added digital signatures for the installer custom action dynamic-link libraries. |

## 18.6.3

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2183083)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2183084)  

Released: December 15, 2021

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2183083&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2183084&clcid=0x40a)

### Features added

| Feature added | Details |
| :------------ | :------ |
| Removal of dependency on vcruntime140_1.dll | To remove the requirement to install Visual Studio in some scenarios, we removed all dependencies to vcruntime140_1.dll. |
| Enable querying server SPN from connection | During a connection attempt where Azure AD authentication has been selected, the server will send a FEDAUTHINFO packet. This packet contains the server's SPN that must be used to generate the access token. Clients can query this value after a connection attempt is made (on success or failure) through the SSPROP_INIT_DISCOVERDSERVERSPN property in DBPROPSET_SQLSERVERDBINIT. |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed an issue where string values were being padded with zeros. | Fixed a bug, which resulted in empty fixed char fields being padded with zeroes during BCP import. |

## 18.6.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2164384)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2164408)  

Released: June 18, 2021

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2164384&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2164408&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for Idle Connection Resiliency | [Idle Connection Resiliency in the OLE DB Driver](features/idle-connection-resiliency.md) |
| Removal of dependency on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools while reading BCP XML format files | [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools is no longer required in order to read BCP XML format files. For more information, see the `BCP_OPTION_FMTXML` option for [IBCPSession::BCPControl](ole-db-interfaces\ibcpsession-bcpcontrol-ole-db.md). |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed an issue with endianness of port numbers in data access trace logs. | Fixed a bug, which resulted in port numbers logged having incorrect endianness while doing [Data Access Tracing](/previous-versions/sql/sql-server-2008/cc765421(v=sql.100)). |
| Fixed an accessibility issue. | Fixed an accessibility issue in the user interface of [Universal Data Link (UDL)](help-topics\data-link-pages.md). This accessibility issue resulted in the *Browse* button not being announced by screen reader software. |
| Fixed crash in scenarios involving Multiple Active Result Sets. | Fixed a bug, which could result in the driver crashing in some scenarios involving [Multiple Active Result Sets (MARS)](features/using-multiple-active-result-sets-mars.md).|

## 18.5.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2135577)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2135722)  

Released: December 1, 2020

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
    For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2135577&clcid=0x40a)  
    For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2135722&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for [SQL Data Discovery and Classification](../../relational-databases/security/sql-data-discovery-and-classification.md) | [Using data classification](features/using-data-classification.md) |
| Azure Active Directory Service Principal authentication support (`ActiveDirectoryServicePrincipal`) | [Using Azure Active Directory](features/using-azure-active-directory.md) |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed an issue with embedded NUL characters. | Fixed a bug, which resulted in the driver returning an incorrect length of strings with embedded NUL characters. |
| Fixed a memory leak in the [IBCPSession](ole-db-interfaces/ibcpsession-ole-db.md) interface. | Fixed a memory leak in the [IBCPSession](ole-db-interfaces/ibcpsession-ole-db.md) interface involving bulk copy operations of `sql_variant` data type. |
| Fixed bugs, which resulted in incorrect values being returned for `SSPROP_INTEGRATEDAUTHENTICATIONMETHOD` and `SSPROP_MUTUALLYAUTHENTICATED` properties. | Previous versions of the driver returned truncated values of the `SSPROP_INTEGRATEDAUTHENTICATIONMETHOD` property. Also, in the `ActiveDirectoryIntegrated` authentication case, the returned value of the `SSPROP_MUTUALLYAUTHENTICATED` property was `VARIANT_FALSE` even when both sides were mutually authenticated.|
| Fixed a linked server remote table insert bug. | Fixed a bug which caused a linked server remote table insert to fail if the [NOCOUNT server configuration option](../../database-engine/configure-windows/configure-the-user-options-server-configuration-option.md) has been enabled. |

## 18.4.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2129954)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2131003)  

Released: May 2020

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2129954&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2131003&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Support for Transparent Network IP Resolution (TNIR) |[Transparent Network IP Resolution (TNIR)](features/using-transparent-network-ip-resolution.md)|
| Support for UTF-8 client encoding | [UTF-8 Support in OLE DB Driver for SQL Server](features/utf-8-support-in-oledb-driver-for-sql-server.md) |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed various bugs in the [ISequentialStream](/previous-versions/windows/desktop/ms718035(v=vs.85)) interface | A few bugs affecting multibyte code pages resulted in the interface prematurely reporting the end of the stream during the read operation.|
| Fixed a memory leak in the [IOpenRowset::OpenRowset](/previous-versions/windows/desktop/ms716724(v=vs.85)) interface | Fixed a memory leak in the [IOpenRowset::OpenRowset](/previous-versions/windows/desktop/ms716724(v=vs.85)) interface when the `SSPROP_IRowsetFastLoad` property was enabled. |
| Fixed a bug in scenarios involving a `sql_variant` data type and non-ASCII strings. | Executing certain scenarios involving a `sql_variant` data type and non-ASCII strings may result in data corruption. For details, see: [Known issues](ole-db-data-types/ssvariant-structure.md#known-issues). |
| Fixed issues with the *Test Connection* button in the [UDL configuration dialog](help-topics/data-link-pages.md). | The *Test Connection* button in the [UDL configuration dialog](help-topics/data-link-pages.md) now honors initialization properties set in the *All* tab. |
| Fixed the `SSPROP_INIT_PACKETSIZE` property default value handling. | Fixed an unexpected error when the `SSPROP_INIT_PACKETSIZE` property was set to its default value of `0`. For details about this property, see [Initialization and Authorization Properties](ole-db-data-source-objects/initialization-and-authorization-properties.md). |
| Fixed buffer overflow issues in [IBCPSession](ole-db-interfaces/ibcpsession-ole-db.md). | Fixed buffer overflow issues when using malformed data files. |
| Fixed accessibility issues. | Fixed accessibility issues in the installer UI and the [SQL Server Login dialog](help-topics/sql-server-login-dialog.md) (reading content, tab stops). |

## 18.3.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2117515)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2117517)  

Released: October 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2117515&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2117517&clcid=0x40a)  

### Features added

| Feature added | Details |
| :------------ | :------ |
| Azure Active Directory authentication support (`ActiveDirectoryInteractive`, `ActiveDirectoryMSI`) | [Using Azure Active Directory](features/using-azure-active-directory.md) |
| Include Azure Active Directory Authentication Library (adal.dll) in the installer | Now included in the base driver installation, the OLE DB installer will upgrade existing installations of the Microsoft Active Directory Authentication Library for SQL Server, removing it from the list of installed applications in Windows. |

### Bugs fixed

| Bug fixed | Details |
| :-------- | :------ |
| Fixed drop index logic in [IIndexDefinition::DropIndex](/previous-versions/windows/desktop/ms722733(v=vs.85)). | Previous versions of the OLE DB driver can't drop a primary key index when the schema ID and the user ID of the owner of the index aren't equal. |

Download previous OLE DB Driver versions by clicking the download links in the following sections:

## 18.2.3

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2119554)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2119738)  

Released: June 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2119554&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2119738&clcid=0x40a)  

### Features added in 18.2.3

| Feature added | Details |
| :------------ | :------ |
| Support for driver upgrades from the SQL Server removable media | This improvement allows driver upgrades directly from the SQL Server removable media. |

## 18.2.2

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118512)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118415)  

Released: May 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118512&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118415&clcid=0x40a)  

### Bugs fixed in 18.2.2

| Bug fixed | Details |
| :-------- | :------ |
| Fixed non-interactive Azure Active Directory authentication in multithreaded apartment (MTA). | The OLE DB Driver 18.2.1 incorrectly tries to change the COM concurrency model on an apartment that was previously initialized as multithreaded (MTA). As a result, in an application that makes more than one subsequent call to [CoInitialize](/windows/win32/api/objbase/nf-objbase-coinitialize) or [CoInitializeEx](/windows/win32/api/combaseapi/nf-combaseapi-coinitializeex) prior to calling the [IDBInitialize::Initialize](/previous-versions/windows/desktop/ms718026(v=vs.85)) interface, the driver fails to connect when using any of the Azure Active Directory authentication modes. |

## 18.2.1

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118511)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118278)  

Released: February 2019

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118511&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118278&clcid=0x40a)  

### Features added in 18.2.1

| Feature added | Details |
| :------------ | :------ |
| Support for UTF-8 server encoding | [UTF-8 Support in OLE DB Driver for SQL Server](features/utf-8-support-in-oledb-driver-for-sql-server.md) |
| Azure Active Directory authentication support | [Using Azure Active Directory](features/using-azure-active-directory.md) |

## 18.1.0

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118506)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118509)  

Released: July 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118506&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118509&2118509=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118509&clcid=0x40a)  

### Features added in 18.1.0

| Feature added | Details |
| :------------ | :------ |
| Support for the `UseFMTONLY` connection string keyword, and for the `SSPROP_INIT_USEFMTONLY` initialization property | `UseFMTONLY` controls how metadata is retrieved when connecting to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and newer.<br/><br/>For more information, see: [Using Connection String Keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |

### Bugs fixed in 18.1.0

| Bug fixed | Details |
| :-------- | :------ |
| Fixed incorrect version of the BCP format file. | The OLE DB Driver 18.0 incorrectly sets the version of the BCP format file to 18.0, instead of to 11.0.<br/>Format files generated by the OLE DB Driver 18.0 cannot be read by the OLE DB Driver 18.1.<br/>If you need to use format files generated by the previous version of the driver with the new driver, you can manually edit the files to change the version to 11.0. |

## 18.0.2

:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x64 installer](https://go.microsoft.com/fwlink/?linkid=2118504)  
:::image type="icon" source="../../includes/media/download.svg" border="false"::: [Download x86 installer](https://go.microsoft.com/fwlink/?linkid=2118277)  

Released: March 2018

If you need to download the installer in a language other than the one detected for you, you can use these direct links.  
For the x64 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118504&clcid=0x40a)  
For the x86 driver: [Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2118277&clcid=0x40a)  

### Features added in 18.0.2

| Feature added | Details |
| :------------ | :------ |
| Support for the `MultiSubnetFailover` connection string keyword, and the `SSPROP_INIT_MULTISUBNETFAILOVER` initialization property. | For more information, see:<br/>&bull; &nbsp; [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md),<br/>&bull; &nbsp; [Using Connection String Keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md). |

## See also

[Microsoft OLE DB Driver for SQL Server](oledb-driver-for-sql-server.md)  
[MSOLEDBSQL major version differences](major-version-differences.md)  
  
