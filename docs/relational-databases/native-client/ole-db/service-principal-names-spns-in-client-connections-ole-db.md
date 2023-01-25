---
description: "Service Principal Names (SPNs) in Client Connections (OLE DB)"
title: "OLE DB Service Principal Names connections"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: e212010e-a5b6-4ad1-a3c0-575327d3ffd3
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Service Principal Names (SPNs) in Client Connections (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-only](../../../includes/snac-removed-oledb-only.md)]

  This topic describes OLE DB properties and member functions that support service principal names (SPNs) in client applications. For more information about SPNs in client applications, see [Service Principal Name &#40;SPN&#41; Support in Client Connections](../../../relational-databases/native-client/features/service-principal-name-spn-support-in-client-connections.md). For a sample, see [Integrated Kerberos Authentication &#40;OLE DB&#41;](../../../relational-databases/native-client-ole-db-how-to/integrated-kerberos-authentication-ole-db.md).  
  
## Provider Initialization String Keywords  
 The following provider initialization string keywords support SPNs in OLE DB applications. In the following table, the values in the keyword column are used for the provider string of IDBInitialize::Initialize. The values in the description column are used in initialization strings when connecting using ADO or IDataInitialize::GetDataSource.  
  
|Keyword|Description|Value|  
|-------------|-----------------|-----------|  
|ServerSPN|Server SPN|The SPN for the server. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|FailoverPartnerSPN|Failover Partner SPN|The SPN for the failover partner. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
  
## Data Source Initialization Properties  
 The following properties in the **DBPROPSET_SQLSERVERDBINIT** property set allow applications to specify SPNs.  
  
|Name|Type|Usage|  
|----------|----------|-----------|  
|SSPROP_INIT_SERVERSPN|VT_BSTR, read/write|Specifies the SPN for the server. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
|SSPROP_INIT_FAILOVERPARTNERSPN|VT_BSTR, read/write|Specifies the SPN for the failover partner. The default value is an empty string, which causes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client to use the default, provider-generated SPN.|  
  
## Data Source Properties  
 The following properties in the **DBPROPSET_SQLSERVERDATASOURCEINFO** property set allow applications to discover the authentication method.  
  
|Name|Type|Usage|  
|----------|----------|-----------|  
|SSPROP_INTEGRATEDAUTHENTICATIONMETHOD|VT_BSTR, readonly|Returns the authentication method used for the connection. The value returned to the application is the value that Windows returns to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. The following are possible values: <br />"NTLM", which is returned when a connection is opened using NTLM authentication.<br />"Kerberos", which is returned when a connection is opened using Kerberos authentication.<br /><br /> If a connection has been opened and the authentication method cannot be determined, VT_EMPTY is returned.<br /><br /> This property can only be read when a data source has been initialized. If you attempt to read the property before a data source has been initialized, IDBProperties::GetProperies will return DB_S_ERRORSOCCURRED or DB_E_ERRORSOCCURRED, as appropriate, and DBPROPSTATUS_NOTSUPPORTED will be set in DBPROPSET_PROPERTIESINERROR for this property. This behavior is in accordance with the OLE DB core specification.|  
|SSPROP_MUTUALLYAUTHENICATED|VT_BOOL, readonly|Returns VARIANT_TRUE if the servers on the connection were mutually authenticated; otherwise, returns VARIANT_FALSE.<br /><br /> This property can only be read when a data source has been initialized. If there is an attempt to read the property before a data source has been initialized, IDBProperties::GetProperies will return DB_S_ERRORSOCCURRED or DB_E_ERRORSOCCURRED, as appropriate, and DBPROPSTATUS_NOTSUPPORTED will be set in DBPROPSET_PROPERTIESINERROR for this property. This behavior is in accordance with the OLE DB core specification<br /><br /> If this attribute is queried for a connection that did not use Windows Authentication, VARIANT_FALSE is returned.|  
  
## OLE DB API Support for SPNs  
 The following table describes the OLE DB member functions that support SPNs in client connections:  
  
|Member function|Description|  
|---------------------|-----------------|  
|IDataInitialize::GetDataSource|*pwszInitializationString* can contain the new keywords **ServerSPN** and **FailoverPartnerSPN**.|  
|IDataInitialize::GetInitializationString|If SSPROP_INIT_SERVERSPN and SSPROP_INIT_FAILOVERPARTNERSPN have non-default values, they will be included in the initialization string through *ppwszInitString* as keyword values for **ServerSPN** and **FailoverPartnerSPN**. Otherwise, these keywords will not be included in the initialization string.|  
|IDBInitialize::Initialize|If prompting is enabled by setting DBPROP_INIT_PROMPT in the data source initialization properties, the OLE DB Login dialog box will be displayed. This allows SPNs to be entered for both the principal server and its failover partner.<br /><br /> The provider string in DPPROP_INIT_PROVIDERSTRING, if set, will recognize the new keywords **ServerSPN** and **FailoverPartnerSPN** and use their values, if present, to initialize SSPROP_INIT_SERVER_SPN and SSPROP_INIT_FAILOVER_PARTNER_SPN.<br /><br /> IDBProperties::SetProperties can be called to set the properties SSPROP_INIT_SERVER_SPN and SSPROP_INIT_FAILOVER_PARTNER_SPN before IDBInitialize::Initialize is called. This is an alternative to using a provider string.<br /><br /> If a property is set in more than one place, a value set programmatically takes precedence over a value set in the provider string. A value set in an initialization string takes precedence over a value set in a login dialog box.<br /><br /> If the same keyword appears more than once in the provider string, the value from first occurrence takes precedence.|  
|IDBProperties::GetProperties|IDBProperties::GetProperties can be called to get the values of the new data source initialization properties SSPROP_INIT_SERVERSPN and SSPROP_INIT_FAILOVERPARTNERSPN, and of the new data source properties SSPROP_AUTHENTICATIONMETHOD and SSPROP_MUTUALLYAUTHENTICATED.|  
|IDBProperties::GetPropertyInfo|IdbProperties::GetPropertyInfo will include the new data source initialization properties SSPROP_INIT_SERVERSPN and SSPROP_INIT_FAILOVERPARTNERSPN, or the new data source properties SSPROP_AUTHENTICATION_METHOD and SSPROP_MUTUALLYAUTHENTICATED.|  
|IDBProperties::SetProperties|IDBProperties::SetProperties can be called to set the values of the new data source initialization properties SSPROP_INITSERVERSPN and SSPROP_INIT_FAILOVERPARTNERSPN.<br /><br /> These properties can be set at any time, but if the data source is already open, the following error will be returned: DB_E_ERRORSOCCURRED, "Multiple-step OLE DB operation generated errors. Check each OLE DB status value, if available. No work was done."|  
  
## See Also  
 [SQL Server Native Client &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/sql-server-native-client-ole-db.md)  
  
  
