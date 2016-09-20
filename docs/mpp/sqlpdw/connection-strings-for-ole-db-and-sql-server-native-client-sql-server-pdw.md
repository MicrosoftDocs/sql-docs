---
title: "Connection Strings for OLE DB and SQL Server Native Client (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 286c67e2-34aa-4c65-aee5-ac385864d92a
caps.latest.revision: 15
author: BarbKess
---
# Connection Strings for OLE DB and SQL Server Native Client (SQL Server PDW)
This topic lists the supported connection string keywords used for connecting to SQL Server PDW with OLE DB by using the SQL Server Native Client 10.0 (SNAC).  
  
Developers use these connection strings to programmatically connect to SQL Server PDW. Application users specify some of these connection string keywords, as required by the application, when connecting to SQL Server PDW.  
  
## Contents  
  
-   [Before You Begin](#BeforeBegin)  
  
-   [Connection String Keywords](#ConnectionString)  
  
-   [Establish a Secure Connection](#SecureConnection)  
  
## <a name="BeforeBegin"></a>Before You Begin  
**Software Requirements**  
  
-   SQL Server Native Client. For the correct version, see [Install SQL Server Native Client &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/install-sql-server-native-client-sql-server-pdw.md).  
  
## <a name="ConnectionString"></a>Connection String Keywords  
This section lists keywords that can be used with the SQL Server Native Client OLE DB driver to create connection strings for SQL Server PDW. This is a subset of keywords supported when connecting to SQL Server. Also included are additional instructions when keyword behavior with SQL Server PDW differs from SQL Server.  
  
For full descriptions of each string keyword, connection string creation information, and usage of the APIs, see the OLE BC section in [Using Connection String Keywords with SQL Server Native Client](http://msdn.microsoft.com/library/ms130822.aspx).  
  
There are two ways OLE DB applications can initialize data source objects for connecting to SQL Server PDW:  
  
-   **IDBInitialize::Initialize**  
  
-   **IDataInitialize::GetDataSource**  
  
The following table describes the keywords that may be used with **IDBInitialize::Initialize** when connecting to SQL Server PDW.  
  
### Keywords for IDBInitialize::Initialize  
The following table describes the keywords that may be used with **IDBInitialize::Initialize** when connecting to SQL Server PDW.  
  
|Keyword|Initialization property|SQL Server PDW Support Notes|  
|-----------|---------------------------|-------------------------------------------------------|  
|**Addr**|SSPROP_INIT_NETWORKADDRESS||  
|**Address**|SSPROP_INIT_NETWORKADDRESS||  
|**APP**|SSPROP_INIT_APPNAME||  
|**AttachDBFileName**|SSPROP_INIT_FILENAME||  
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE||  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE||  
|**Database**|DBPROP_INIT_CATALOG||  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY||  
|**Encrypt**|SSPROP_INIT_ENCRYPT||  
|**Language**|SSPROPT_INIT_CURRENTLANGUAGE|‘English’ only.|  
|**MarsConn**|SSPROP_INIT_MARSCONNECTION|‘no’ only.|  
|**Net**|SSPROP_INIT_NETWORKLIBRARY||  
|**Network**|SSPROP_INIT_NETWORKLIBRARY|‘dbmssocn’ only (TCP)|  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY||  
|**PacketSize**|SSPROP_INIT_PACKETSIZE||  
|**PersistSensitive**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO||  
|**PWD**|DBPROP_AUTH_PASSWORD||  
|**Timeout**|DBPROP_INIT_TIMEOUT||  
|**Trusted_Connection**|DBPROP_AUTH_INTEGRATED|‘no’ only.|  
|**TrustServerCertificate**|SSPROP_INIT_TRUST_SERVER_CERTIFICATE||  
|**UID**|DBPROP_AUTH_USERID||  
|**UseProcForPrepare**|SSPROP_INIT_USEPROCFORPREP||  
|**WSID**|SSPROP_INIT_WSID||  
  
### Keywords for IDataInitialize::GetDataSource  
The following table describes the keywords that may be used with **IDataInitialize::GetDataSource** when connecting to SQL Server PDW:  
  
|Keyword|Initialization property|ssDW Usage Notes|  
|-----------|---------------------------|--------------------|  
|**Application Name**|SSPROP_INIT_APPNAME||  
|**Auto Translate**|SSPROP_INIT_AUTOTRANSLATE||  
|**AutoTranslate**|SSPROP_INIT_AUTOTRANSLATE||  
|**Connect Timeout**|DBPROP_INIT_TIMEOUT||  
|**Current Language**|SSPROPT_INIT_CURRENTLANGUAGE|‘English’ only.|  
|**DataTypeCompatibility**|SSPROP_INIT_DATATYPECOMPATIBILITY||  
|**Initial Catalog**|DBPROP_INIT_CATALOG||  
|**MARS Connection**|SSPROP_INIT_MARSCONNECTION|‘False’ only.|  
|**Network Address**|SSPROP_INIT_NETWORKADDRESS||  
|**Network Library**|SSPROP_INIT_NETWORKLIBRARY||  
|**Packet Size**|SSPROP_INIT_PACKETSIZE|‘0’ only.|  
|**Password**|DBPROP_AUTH_PASSWORD||  
|**Persist Security Info**|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO||  
|**Provider**|||  
|**Trust Server Certificate**|SSPROP_INIT_TRUST_SERVER_CERTIFICATE||  
|**Use Encryption for Data**|SSPROP_INIT_ENCRYPT||  
|**User ID**|DBPROP_AUTH_USERID||  
|**Workstation ID**|SSPROP_INIT_WSID||  
  
## <a name="SecureConnection"></a>Establish a Secure Conection  
To establish a secure connection to SQL Server PDW via an ODBC or OLE DB connection, the connection string must contain the following:  
  
1.  The *ValidateServerCertificate* value set to **true**.  
  
2.  The *HostNameInCertificate* (HNIC) value is optional. It can be used as an extra validation, and when used, complies with the following HNIC values:  
  
    -   When the certificate has a *dnsName* value in the *subjectAltName* value, the *HostNameInCertificate* value in the connection string must match the *dnsName*  
  
    -   When the certificate does not have a *dnsName* value in the *subjectAltName* field or no *subjectAltName* value is present, the *HostNameInCertificate* value in the connection string must match the *commonName* (CN) part in the *Subject name* of the certificate. If multiple *commonName* parts exist in the *Subject name* of the certificate, the connection succeeds if the HNIC value matches any of the *Common Name* parts.  
  
