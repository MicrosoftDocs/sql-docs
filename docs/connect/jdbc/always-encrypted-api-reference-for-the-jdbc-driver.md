---
title: Always Encrypted API reference
description: Learn about the Always Encrypted APIs in the JDBC driver and how you can use them to encrypt and secure data in your Java application.
author: David-Engel
ms.author: v-davidengel
ms.date: 01/29/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Always Encrypted API reference for the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Always Encrypted allows clients to encrypt sensitive data inside client applications and never reveal the encryption keys to the server. An Always Encrypted enabled driver installed on the client computer achieves this functionality by automatically encrypting and decrypting sensitive data in the client application.

The driver encrypts the data in sensitive columns before passing the data to SQL Server, and automatically rewrites queries to preserve the semantics to the application. Similarly, the driver transparently decrypts data stored in encrypted database columns that are in query results. For more information, see [Always Encrypted (Database Engine)](../../relational-databases/security/encryption/always-encrypted-database-engine.md) and [Using Always Encrypted with the JDBC Driver](using-always-encrypted-with-the-jdbc-driver.md).

> [!NOTE]
> Always Encrypted is supported only by Microsoft JDBC Driver 6.0 or higher for SQL Server with Azure SQL Database and SQL Server 2016 and higher.

## Always Encrypted API references

There are several new additions and modifications to the JDBC driver API for use in client applications that use Always Encrypted.

### SQLServerConnection class

|Name|Description|
|----------|-----------------|
|New connection string keyword:<br /><br /> columnEncryptionSetting|columnEncryptionSetting=Enabled enables Always Encrypted functionality for the connection and columnEncryptionSetting=Disabled disables it. Accepted values are Enabled/Disabled. The default is Disabled.|
|New connection string keyword:(MS JDBC 7.4 onwards)<br /><br /> keyVaultProviderClientId <br /><br /> keyVaultProviderClientKey |keyVaultProviderClientId=\<ClientID>;keyVaultProviderClientKey=\<ClientKey> <br/><br/> Registers SQLServerColumnEncryptionAzureKeyVaultProvider and uses ClientID and ClientKey values to retrieve Column Master Key from Azure Key Vault|
|New methods:<br /><br />`public static void setColumnEncryptionTrustedMasterKeyPaths(Map<String, List\<String>> trustedKeyPaths)`<br /><br />`public static void updateColumnEncryptionTrustedMasterKeyPaths(String server, List\<String> trustedKeyPaths)`<br /><br />`public static void removeColumnEncryptionTrustedMasterKeyPaths(String server)`|Allows you to set/update/remove a list of trusted key paths for a database server. If while processing an application query the driver receives a key path that's not on the list, the query will fail. This property provides extra protection against security attacks that involve a compromised server sending fake key paths, which may lead to leaking key store credentials.|
|New method:<br /><br />`public static Map<String, List\<String>> getColumnEncryptionTrustedMasterKeyPaths()`|Returns a list of trusted key paths for a database server.|
|New method:<br /><br />`public static void registerColumnEncryptionKeyStoreProviders (Map\<String, SQLServerColumnEncryptionKeyStoreProvider> clientKeyStoreProviders)`|Allows you to register custom key store providers. It's a dictionary that maps key store provider names to key store provider implementations.<br /><br /> To use the JVM key store, you need to instantiate a SQLServerColumnEncryptionJVMKeyStoreProvider object with JVM keystore credentials and register it with the driver. The name for this provider must be 'MSSQL_JVM_KEYSTORE'.<br /><br /> To use the Azure Key Vault store, you need to instantiate a SQLServerColumnEncryptionAzureKeyStoreProvider object and register it with the driver. The name for this provider must be 'AZURE_KEY_VAULT'.|
|New method:<br /><br />`public static void unregisterColumnEncryptionKeyStoreProviders (Map\<String, SQLServerColumnEncryptionKeyStoreProvider> clientKeyStoreProviders)`|Allows you to unregister all the custom key store providers by clearing the dictionary that maps key store provider names to key store provider implementations.|
|`public final boolean getSendTimeAsDatetime()`|Returns the setting of the sendTimeAsDatetime connection property.|
|`public void setSendTimeAsDatetime(boolean sendTimeAsDateTimeValue)`|Modifies the setting of the sendTimeAsDatetime connection property.|

### SQLServerConnectionPoolProxy class

|Name|Description|
|----------|-----------------|
|`public final boolean getSendTimeAsDatetime()` | Returns the setting of the sendTimeAsDatetime connection property.|
|`public void setSendTimeAsDatetime(boolean sendTimeAsDateTimeValue)` | Modifies the setting of the sendTimeAsDatetime connection property.|

### SQLServerDataSource class

|Name|Description|
|----------|-----------------|
|`public void setColumnEncryptionSetting(String columnEncryptionSetting)`|Enables/disables Always Encrypted functionality for the data source object.<br /><br /> The default is Disabled.|
|`public String getColumnEncryptionSetting()`|Retrieves the Always Encrypted functionality setting for the data source object.|
|`public void setKeyStoreAuthentication(String keyStoreAuthentication)`|Sets the name that identifies a key store. Only value supported is the **JavaKeyStorePassword** for identifying the Java Key Store.<br/><br/>The default is null.|
|`public String getKeyStoreAuthentication()`|Gets the value of the keyStoreAuthentication setting for the data source object.|
|`public void setKeyStoreSecret(String keyStoreSecret)`|Sets the password for the Java keystore. The password for the keystore and the key must be the same. keyStoreAuthentication must be set with **JavaKeyStorePassword**.|
|`public void setKeyStoreLocation(String keyStoreLocation)`|Sets the location including the file name for the Java keystore. keyStoreAuthentication must be set with **JavaKeyStorePassword**.|
|`public String getKeyStoreLocation()`|Retrieves the keyStoreLocation for the Java Key Store.|

### SQLServerColumnEncryptionJavaKeyStoreProvider class

The implementation of the key store provider for Java Key Store. This class enables using certificates stored in the Java keystore as column master keys.

**Constructors:**

|Name|Description|
|----------|-----------------|
|`public SQLServerColumnEncryptionJavaKeyStoreProvider (String keyStoreLocation, char[] keyStoreSecret)`|Key store provider for the Java Key Store.|

**Methods:**

|Name|Description|
|----------|-----------------|
|`public byte[] decryptColumnEncryptionKey (String masterKeyPath, String encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)`|Decrypts the specified encrypted value of a column encryption key. The encrypted value is expected to be encrypted using the certificate with the specified key path and using the specified algorithm.<br /><br /> **The key path should be in one of the following formats:**<br /><br /> Thumbprint:<certificate_thumbprint><br /><br /> Alias:<certificate_alias><br /><br /> (Overrides `SQLServerColumnEncryptionKeyStoreProvider`. decryptColumnEncryptionKey(String, String, Byte[]).)|
|`public byte[] encryptColumnEncryptionKey (String masterKeyPath, String encryptionAlgorithm, byte[] plainTextColumnEncryptionKey)`|Encrypts a column encryption key using the certificate with the specified key path and using the specified algorithm.<br /><br /> **The key path should be in one of the following formats:**<br /><br /> Thumbprint:<certificate_thumbprint><br /><br /> Alias:<certificate_alias><br /><br /> (Overrides `SQLServerColumnEncryptionKeyStoreProvider`. encryptColumnEncryptionKey(String, String, Byte[]).)|
|`public boolean verifyColumnEncryptionKey (String masterKeyPath, boolean allowEnclaveComputations, byte[] signature)`|Verifies the signature of the column encryption key using the certificate.<br /><br /> **The key path should be in one of the following formats:**<br /><br /> Thumbprint:<certificate_thumbprint><br /><br /> Alias:<certificate_alias><br /><br /> (Overrides `SQLServerColumnEncryptionKeyStoreProvider`. verifyColumnEncryptionKey(String, boolean, Byte[]).)|
|`public void setName (String name)`|Sets the name of this key store provider.|
|`public String getName ()`|Gets the name of this key store provider.|

### SQLServerColumnEncryptionAzureKeyVaultProvider class

The implementation of the key store provider for Azure Key Vault. This class enables using keys stored in the Azure Key Vault as column master keys.

**Constructors:**

|Name|Description|
|----------|-----------------|
|`public SQLServerColumnEncryptionAzureKeyVaultProvider ()`|Constructs a SQLServerColumnEncryptionAzureKeyVaultProvider to authenticate to Azure Key Vault.|
|`public SQLServerColumnEncryptionAzureKeyVaultProvider (String clientId)`|Constructs a SQLServerColumnEncryptionAzureKeyVaultProvider to authenticate to Azure Key Vault using the identifier of the client requesting the token.|
|`public SQLServerColumnEncryptionAzureKeyVaultProvider (String clientId, String clientKey)`|Constructs a SQLServerColumnEncryptionAzureKeyVaultProvider to authenticate to Azure Key Vault using the identifier and the key of the client requesting the token.|
|`public SQLServerColumnEncryptionAzureKeyVaultProvider (TokenCredential tokenCredential)`|Constructs a SQLServerColumnEncryptionAzureKeyVaultProvider to authenticate to Azure Key Vault using provided TokenCredential.|

**Methods:**

|Name|Description|
|----------|-----------------|
|`public byte[] decryptColumnEncryptionKey (String masterKeyPath, String encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)` | Decrypts an encrypted column encryption key (CEK). This decryption is accomplished with an RSA encryption algorithm that uses the asymmetric key specified by the master key path.<br />(Overrides `SQLServerColumnEncryptionKeyStoreProvider`. decryptColumnEncryptionKey(String, String, Byte[]).) |
| `public byte[] encryptColumnEncryptionKey (String masterKeyPath, String encryptionAlgorithm, byte[] columnEncryptionKey)` | Encrypts a column encryption key, by giving the specified column master key to the specified algorithm.<br />(Overrides `SQLServerColumnEncryptionKeyStoreProvider`. encryptColumnEncryptionKey(String, String, Byte[]).) |
|`public void setName (String name)`|Sets the name of this key store provider.|
|`public String getName ()`|Gets the name of this key store provider.|

### SQLServerKeyVaultAuthenticationCallback interface

This interface contains one method for Azure Key Vault authentication, which is to be implemented by user.

**Methods:**

|Name|Description|
|----------|-----------------|
|`public String getAccessToken(String authority, String resource, String scope);`|The method must be overridden. The method is used to get an access token to Azure Key Vault.|

### SQLServerColumnEncryptionKeyStoreProvider class

Extend this class to implement a custom key store provider.

|Name|Description|
|----------|-----------------|
|SQLServerColumnEncryptionKeyStoreProvider|Base class for all key store providers. A custom provider must derive from this class and override its member functions and then register it using SQLServerConnection. registerColumnEncryptionKeyStoreProviders().|

**Methods:**

|Name|Description|
|----------|-----------------|
|`public abstract byte[] decryptColumnEncryptionKey (String masterKeyPath, String encryptionAlgorithm, byte [] encryptedColumnEncryptionKey)`|Base class method for decrypting the specified encrypted value of a column encryption key. The encrypted value is expected to be encrypted using the column master key with the specified key path and the specified algorithm.|
|`public abstract byte[] encryptColumnEncryptionKey (String masterKeyPath, String encryptionAlgorithm, byte[]  columnEncryptionKey)`|Base class method for encrypting a column encryption key using the column master key with the specified key path and using the specified algorithm.|
|`public abstract void setName(String name)`|Sets the name of this key store provider.|
|`public abstract String getName()`|Gets the name of this key store provider.|

### New or overloaded methods in SQLServerPreparedStatement class

|Name|Description|
|----------|-----------------|
|`public void setBigDecimal(int parameterIndex, BigDecimal x, int precision, int scale)`<br /><br /> `public void setObject(int parameterIndex, Object x, int targetSqlType, Integer precision, int scale)`<br /><br /> `public void setObject(int parameterIndex, Object x, SQLType targetSqlType, Integer precision, Integer scale)`<br /><br /> `public void setTime(int parameterIndex, java.sql.Time x, int scale)`<br /><br /> `public void setTimestamp(int parameterIndex, java.sql.Timestamp x, int scale)` <br />`public void setDateTimeOffset(int parameterIndex, microsoft.sql.DateTimeOffset x, int scale)`|These methods are overloaded with a precision or a scale argument or both to support Always Encrypted for specific data types that require precision and scale information.|
|`public void setMoney(int parameterIndex, BigDecimal x)`<br /><br /> `public void setSmallMoney(int parameterIndex, BigDecimal x)`<br /><br /> `public void setUniqueIdentifier(int parameterIndex, String guid)`<br /><br /> `public void setDateTime(int parameterIndex, java.sql.Timestamp x)`<br /><br /> `public void setSmallDateTime(int parameterIndex, java.sql.Timestamp x)`|These methods add support for Always Encrypted for the data types money, smallmoney, uniqueidentifier, datetime and smalldatetime. <br/><br/>The existing `setTimestamp()` method is used for sending parameter values to the encrypted datetime2 column. For encrypted datetime and smalldatetime columns, use the new methods `setDateTime()` and `setSmallDateTime()` respectively.|
|`public final void setBigDecimal(int parameterIndex, BigDecimal x, int precision, int scale, boolean forceEncrypt)`<br /><br /> `public final void setMoney(int parameterIndex, BigDecimal x, boolean forceEncrypt)`<br /><br /> `public final void setSmallMoney(int parameterIndex, BigDecimal x, boolean forceEncrypt)`<br /><br /> `public final void setBoolean(int parameterIndex, boolean x, boolean forceEncrypt)`<br /><br /> `public final void setByte(int parameterIndex, byte x, boolean forceEncrypt)`<br /><br /> `public final void setBytes(int parameterIndex, byte x[], boolean forceEncrypt)`<br /><br /> `public final void setUniqueIdentifier(int parameterIndex, String guid, boolean forceEncrypt)`<br /><br /> `public final void setDouble(int parameterIndex, double x, boolean forceEncrypt)`<br /><br /> `public final void setFloat(int parameterIndex, float x, boolean forceEncrypt)`<br /><br /> `public final void setInt(int parameterIndex, int value, boolean forceEncrypt)`<br /><br /> `public final void setLong(int parameterIndex, long x, boolean forceEncrypt)`<br /><br /> `public final setObject(int parameterIndex, Object x, int targetSqlType, Integer precision, int scale, boolean forceEncrypt)`<br /><br /> `public final void setObject(int parameterIndex, Object x, SQLType targetSqlType, Integer precision, Integer scale, boolean forceEncrypt)`<br /><br /> `public final void setShort(int parameterIndex, short x, boolean forceEncrypt)`<br /><br /> `public final void setString(int parameterIndex, String str, boolean forceEncrypt)`<br /><br /> `public final void setNString(int parameterIndex, String value, boolean forceEncrypt)`<br /><br /> `public final void setTime(int parameterIndex, java.sql.Time x, int scale, boolean forceEncrypt)`<br /><br /> `public final void setTimestamp(int parameterIndex, java.sql.Timestamp x, int scale, boolean forceEncrypt)`<br /><br /> `public final void setDateTimeOffset(int parameterIndex, microsoft.sql.DateTimeOffset x, int scale, boolean forceEncrypt)`<br /><br /> `public final void setDateTime(int parameterIndex, java.sql.Timestamp x, boolean forceEncrypt)`<br /><br /> `public final void setSmallDateTime(int parameterIndex, java.sql.Timestamp x, boolean forceEncrypt)`<br /><br /> `public final void setDate(int parameterIndex, java.sql.Date x, java.util.Calendar cal, boolean forceEncrypt)`<br /><br /> `public final void setTime(int parameterIndex, java.sql.Time x, java.util.Calendar cal, boolean forceEncrypt)`<br /><br /> `public final void setTimestamp(int parameterIndex, java.sql.Timestamp x, java.util.Calendar cal, boolean forceEncrypt)`|Sets the named parameter to the given java value.<br /><br /> If the boolean `forceEncrypt` is set to true, the query parameter will only be set if the designated column is encrypted and Always Encrypted is enabled on the connection or on the statement.<br /><br /> If the boolean forceEncrypt is set to false, the driver won't force encryption on parameters.|

### New or overloaded methods in SQLServerCallableStatement class

|Name|Description|
|----------|-----------------|
|`public void registerOutParameter(int parameterIndex, int sqlType, int precision, int scale)`<br /><br /> `public void registerOutParameter(int parameterIndex, SQLType sqlType, int precision, int scale)`<br /><br /> `public void registerOutParameter(String parameterName, int sqlType, int precision, int scale)`<br /><br /> `public void registerOutParameter(String parameterName, SQLType sqlType, int precision, int scale)`<br />`public void setBigDecimal(String parameterName, BigDecimal bd, int precision, int scale)`<br /><br /> `public void setTime(String parameterName, java.sql.Time t, int scale)`<br /><br /> `public void setTimestamp(String parameterName, java.sql.Timestamp t, int scale)`<br /><br /> `public void setDateTimeOffset(String parameterName, microsoft.sql.DateTimeOffset t, int scale)`<br/><br/>`public final void setObject(String sCol, Object x, int targetSqlType, Integer precision, int scale)`|These methods are overloaded with a precision or a scale argument or both to support Always Encrypted for specific data types that require precision and scale information.|
|`public void setDateTime(String parameterName, java.sql.Timestamp x)`<br /><br /> `public void setSmallDateTime(String parameterName, java.sql.Timestamp x)`<br /><br /> `public void setUniqueIdentifier(String parameterName, String guid)`<br /><br /> `public void setMoney(String parameterName, BigDecimal bd)`<br /><br /> `public void setSmallMoney(String parameterName, BigDecimal bd)`<br/><br/>`public Timestamp getDateTime(int index)`<br/><br/>`public Timestamp getDateTime(String sCol)`<br/><br/>`public Timestamp getDateTime(int index, Calendar cal)`<br/><br/>`public Timestamp getSmallDateTime(int index)`<br/><br/>`public Timestamp getSmallDateTime(String sCol)`<br/><br/>`public Timestamp getSmallDateTime(int index, Calendar cal)`<br/><br/>`public Timestamp getSmallDateTime(String name, Calendar cal)`<br/><br/>`public BigDecimal getMoney(int index)`<br/><br/>`public BigDecimal getMoney(String sCol)`<br/><br/>`public BigDecimal getSmallMoney(int index)`<br/><br/>`public BigDecimal getSmallMoney(String sCol)`|These methods add support for Always Encrypted for the data types money, smallmoney, uniqueidentifier, datetime and smalldatetime. <br/><br/>The existing `setTimestamp()` method is used for sending parameter values to the encrypted datetime2 column. For encrypted datetime and smalldatetime columns, use the new methods `setDateTime()` and `setSmallDateTime()` respectively.|
|`public void setObject(String parameterName, Object o, int n, int m, boolean forceEncrypt)`<br /><br /> `public void setObject(String parameterName, Object obj, SQLType jdbcType, int scale, boolean forceEncrypt)`<br /><br /> `public void setDate(String parameterName, java.sql.Date x, Calendar c, boolean forceEncrypt)`<br /><br /> `public void setTime(String parameterName, java.sql.Time t, int scale, boolean forceEncrypt)`<br /><br /> `public void setTime(String parameterName, java.sql.Time x, Calendar c, boolean forceEncrypt)`<br /><br /> `public void setDateTime(String parameterName, java.sql.Timestamp x, boolean forceEncrypt)`<br /><br /> `public void setDateTimeOffset(String parameterName, microsoft.sql.DateTimeOffset t, int scale, boolean forceEncrypt)`<br /><br /> `public void setSmallDateTime(String parameterName, java.sql.Timestamp x, boolean forceEncrypt)`<br /><br /> `public void setTimestamp(String parameterName, java.sql.Timestamp t, int scale, boolean forceEncrypt)`<br /><br /> `public void setTimestamp(String parameterName, java.sql.Timestamp x, boolean forceEncrypt)`<br /><br /> `public void setUniqueIdentifier(String parameterName, String guid, boolean forceEncrypt)`<br /><br /> `public void setBytes(String parameterName, byte[] b, boolean forceEncrypt)`<br /><br /> `public void setByte(String parameterName, byte b, boolean forceEncrypt)`<br /><br /> `public void setString(String parameterName, String s, boolean forceEncrypt)`<br /><br /> `public final void setNString(String parameterName, String value, boolean forceEncrypt)<br /><br /> public void setMoney(String parameterName, BigDecimal bd, boolean forceEncrypt)`<br /><br /> `public void setSmallMoney(String parameterName, BigDecimal bd, boolean forceEncrypt)`<br /><br /> `public void setBigDecimal(String parameterName, BigDecimal bd, int precision, int scale, boolean forceEncrypt)`<br /><br /> `public void setDouble(String parameterName, double d, boolean forceEncrypt)`<br /><br /> `public void setFloat(String parameterName, float f, boolean forceEncrypt)`<br /><br /> `public void setInt(String parameterName, int i, boolean forceEncrypt)`<br /><br /> `public void setLong(String parameterName, long l, boolean forceEncrypt)`<br /><br /> `public void setShort(String parameterName, short s, boolean forceEncrypt)`<br /><br /> `public void setBoolean(String parameterNames, boolean b, boolean forceEncrypt)`<br/><br/>`public void setTimeStamp(String sCol, java.sql.Timestamp x, Calendar c, Boolean forceEncrypt)`|Sets the named parameter to the given java value.<br /><br /> If the boolean forceEncrypt is set to true, the query parameter will only be set if the designated column is encrypted and Always Encrypted is enabled on the connection or on the statement.<br /><br /> If the boolean forceEncrypt is set to false, the driver won't force encryption on parameters.|

### New or overloaded methods in SQLServerResultSet class

|Name|Description|
|----------|-----------------|
|`public String getUniqueIdentifier(int columnIndex)`<br/><br/>`public String getUniqueIdentifier(String columnLabel)`<br/><br/>   `public java.sql.Timestamp getDateTime(int columnIndex)` <br/><br/> `public java.sql.Timestamp getDateTime(String columnName)`   <br/><br/> `public java.sql.Timestamp getDateTime(int columnIndex, Calendar cal)`   <br/><br/>`public java.sql.Timestamp getDateTime(String colName, Calendar cal)`    <br/><br/>`public java.sql.Timestamp getSmallDateTime(int columnIndex)`    <br/><br/> `public java.sql.Timestamp getSmallDateTime(String columnName)`   <br/><br/> `public java.sql.Timestamp getSmallDateTime(int columnIndex, Calendar cal)`   <br/><br/> `public java.sql.Timestamp getSmallDateTime(String colName, Calendar cal)`   <br/><br/>  `public BigDecimal getMoney(int columnIndex)`  <br/><br/> `public BigDecimal getMoney(String columnName)`   <br/><br/> `public BigDecimal getSmallMoney(int columnIndex)`   <br/><br/>  `public BigDecimal getSmallMoney(String columnName)`  <br/><br/>`public void updateMoney(String columnName, BigDecimal x)`    <br/><br/>  `public void updateSmallMoney(String columnName, BigDecimal x)`  <br/><br/>     `public void updateDateTime(int index, java.sql.Timestamp x)` <br/><br/> `public void updateSmallDateTime(int index, java.sql.Timestamp x)` |These methods add support for Always Encrypted for the data types money, smallmoney, uniqueidentifier, datetime, and smalldatetime. <br/><br/>The existing `updateTimestamp()` method is used for updating encrypted datetime2 columns. For encrypted datetime and smalldatetime columns, use the new methods `updateDateTime()` and `updateSmallDateTime()` respectively.|
|`public void updateBoolean(int index, boolean x, boolean forceEncrypt)`  <br/><br/>  `public void updateByte(int index, byte x, boolean forceEncrypt)`  <br/><br/>  `public void updateShort(int index, short x, boolean forceEncrypt)`  <br/><br/> `public void updateInt(int index, int x, boolean forceEncrypt)`   <br/><br/>  `public void updateLong(int index, long x, boolean forceEncrypt)`  <br/><br/> `public void updateFloat(int index, float x, boolean forceEncrypt)`   <br/><br/> `public void updateDouble(int index, double x, boolean forceEncrypt)`   <br/><br/> `public void updateMoney(int index, BigDecimal x, boolean forceEncrypt)`   <br/><br/>  `public void updateMoney(String columnName, BigDecimal x, boolean forceEncrypt)`  <br/><br/> `public void updateSmallMoney(int index, BigDecimal x, boolean forceEncrypt)`   <br/><br/>  `public void updateSmallMoney(String columnName, BigDecimal x, boolean forceEncrypt)`  <br/><br/> `public void updateBigDecimal(int index, BigDecimal x, Integer precision, Integer scale, boolean forceEncrypt)`   <br/><br/>  `public void updateString(int columnIndex, String stringValue, boolean forceEncrypt)`  <br/><br/>  `public void updateNString(int columnIndex, String nString, boolean forceEncrypt)`  <br/><br/>  `public void updateNString(String columnLabel, String nString, boolean forceEncrypt)`  <br/><br/> `public void updateBytes(int index, byte x[], boolean forceEncrypt)   <br/><br/>  public void updateDate(int index, java.sql.Date x, boolean forceEncrypt)`  <br/><br/> `public void updateTime(int index, java.sql.Time x, Integer scale, boolean forceEncrypt)`   <br/><br/> `public void updateTimestamp(int index, java.sql.Timestamp x, int scale, boolean forceEncrypt)`   <br/><br/> `public void updateDateTime(int index, java.sql.Timestamp x, Integer scale, boolean forceEncrypt)`   <br/><br/> `public void updateSmallDateTime(int index, java.sql.Timestamp x, Integer scale, boolean forceEncrypt)`   <br/><br/>  `public void updateDateTimeOffset(int index, microsoft.sql.DateTimeOffset x, Integer scale, boolean forceEncrypt)`  <br/><br/> `public void updateUniqueIdentifier(int index, String x, boolean forceEncrypt)`    <br/><br/>  `public void updateObject(int index, Object x, int precision, int scale, boolean forceEncrypt)`  <br/><br/>  `public void updateObject(int index, Object obj, SQLType targetSqlType, int scale, boolean forceEncrypt)`  <br/><br/> `public void updateBoolean(String columnName, boolean x, boolean forceEncrypt)`    <br/><br/>  `public void updateByte(String columnName, byte x, boolean forceEncrypt)`  <br/><br/>  `public void updateShort(String columnName, short x, boolean forceEncrypt)`  <br/><br/> `public void updateInt(String columnName, int x, boolean forceEncrypt)`   <br/><br/>   `public void updateLong(String columnName, long x, boolean forceEncrypt)` <br/><br/>  `public void updateFloat(String columnName, float x, boolean forceEncrypt)`  <br/><br/>  `public void updateDouble(String columnName, double x, boolean forceEncrypt)  <br/><br/> public void updateBigDecimal(String columnName, BigDecimal x, boolean forceEncrypt)`   <br/><br/>  `public void updateBigDecimal(String columnName, BigDecimal x, Integer precision, Integer scale, boolean forceEncrypt)`  <br/><br/> `public void updateString(String columnName, String x, boolean forceEncrypt)`   <br/><br/>  `public void updateBytes(String columnName, byte x[], boolean forceEncrypt)`  <br/><br/> `public void updateDate(String columnName, java.sql.Date x, boolean forceEncrypt)`   <br/><br/>  `public void updateTime(String columnName, java.sql.Time x, int scale, boolean forceEncrypt)`  <br/><br/>  `public void updateTimestamp(String columnName, java.sql.Timestamp x, int scale, boolean forceEncrypt)`  <br/><br/> `public void updateDateTime(String columnName, java.sql.Timestamp x, int scale, boolean forceEncrypt)`   <br/><br/>  `public void updateSmallDateTime(String columnName, java.sql.Timestamp x, int scale, boolean forceEncrypt)`  <br/><br/>  `public void updateDateTimeOffset(String columnName, microsoft.sql.DateTimeOffset x, int scale, boolean forceEncrypt)`  <br/><br/>  `public void updateUniqueIdentifier(String columnName, String x, boolean forceEncrypt)`<br/><br/>`public void updateObject(String columnName, Object x, int precision, int scale, boolean forceEncrypt)`<br/><br/>`public void updateObject(String columnName, Object obj, SQLType targetSqlType, int scale, boolean forceEncrypt)`|Updates the named column to the given java value.<br/><br/>If the boolean `forceEncrypt` is set to true, the column will only be set if it's encrypted and Always Encrypted is enabled on the connection or on the statement.<br/><br/>If the boolean `forceEncrypt` is set to false, the driver won't force encryption on parameters.|

### New types in microsoft.sql.Types class

|Name|Description|
|----------|-----------------|
|DATETIME, SMALLDATETIME, MONEY, SMALLMONEY, GUID|Use these types as the target SQL types when sending parameter values to **encrypted** datetime, smalldatetime, money, smallmoney, uniqueidentifier columns using `setObject()/updateObject()` API methods.|

### SQLServerStatementColumnEncryptionSetting Enum

Specifies how data will be sent and received when reading and writing encrypted columns. Depending on your specific query, performance impact may be reduced by bypassing the Always Encrypted driver's processing when non-encrypted columns are being used. These settings can't be used to bypass encryption and gain access to plaintext data.

**Syntax:**

```java
Public enum  SQLServerStatementColumnEncryptionSetting
```

**Members:**

|Name|Description|
|----------|-----------------|
|UseConnectionSetting|Specifies that the command should default to the Always Encrypted setting in the connection string.|
|Enabled|Enables Always Encrypted for the query.|
|ResultSetOnly|Specifies that only the results of the command should be processed by the Always Encrypted routine in the driver. Use this value when the command has no parameters that require encryption.|
|Disabled|Disables Always Encrypted for the query.|

The statement level setting for AE is added to the SQLServerConnection class and to the SQLServerConnectionPoolProxy class. The following methods in these classes are overloaded with the new setting.

|Name|Description|
|----------|-----------------|
|`public Statement createStatement(int nType, int nConcur, int statementHoldability, SQLServerStatementColumnEncryptionSetting stmtColEncSetting)`|Creates a Statement object that will generate ResultSet objects with the given type, concurrency, holdability, and column encryption setting.|
|`public CallableStatement prepareCall(String sql, int nType, int nConcur, int statementHoldability, SQLServerStatementColumnEncryptionSetting stmtColEncSetiing)`|Creates a CallableStatement object with the given column encryption setting that will generate ResultSet objects with the given type, concurrency, and holdability.|
|`public PreparedStatement prepareStatement(String sql, int autogeneratedKeys, SQLServerStatementColumnEncryptionSetting stmtColEncSetting)`|Creates a PreparedStatement object with the given column encryption setting that has the capability to retrieve autogenerated keys.|
|`public PreparedStatement prepareStatement(String sql, String[] columnNames, SQLServerStatementColumnEncryptionSetting stmtColEncSetting)`|Creates a PreparedStatement object with the given column encryption setting that will generate ResultSet objects with the given column names.|
|`public PreparedStatement prepareStatement(String sql, int[] columnIndexes, SQLServerStatementColumnEncryptionSetting stmtColEncSetting`|Creates a PreparedStatement object with the given column encryption setting that will generate ResultSet objects with the given column indexes.|
|`public PreparedStatement prepareStatement(String sql, int nType, int nConcur, int nHold, SQLServerStatementColumnEncryptionSetting stmtColEncSetting)`|Creates a PreparedStatement object with the given column encryption setting that will generate ResultSet objects with the given type, concurrency, and holdability.|

> [!NOTE]
> If Always Encrypted is disabled for a query and the query has parameters that need to be encrypted (parameters that correspond to encrypted columns), the query will fail.
>
> If Always Encrypted is disabled for a query and the query returns results from encrypted columns, the query will return encrypted values. The encrypted values will have the varbinary datatype.

## See also

[Using Always Encrypted with the JDBC driver](using-always-encrypted-with-the-jdbc-driver.md)
